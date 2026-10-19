using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using DSInternals.Win32.WebAuthn.Interop;
using Prism.Commands;
using Prism.Dialogs;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

/// <summary>Coordinates security-key management through a single explicitly selected session.</summary>
internal sealed class SecurityKeyManagementViewModel : BindableBase, IAsyncDisposable
{
    /// <summary>Displays operation errors and confirmations.</summary>
    private readonly IDialogService _dialogs;

    /// <summary>Collects secure PINs and displays cancelable device prompts.</summary>
    private readonly ISecurityKeyInteractionService _interaction;

    /// <summary>Owns the currently selected authenticator.</summary>
    private AuthenticatorManagementSession? _session;

    /// <summary>Signals cancellation without releasing resources still used by native code.</summary>
    [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed",
        Justification = "RunAsync disposes each operation's source in finally; DisposeAsync waits for that cleanup.")]
    private CancellationTokenSource? _cancellation;

    /// <summary>Completes after the current operation and its cleanup have finished.</summary>
    private TaskCompletionSource? _operationCompletion;

    /// <summary>Prevents new work while the application is shutting down.</summary>
    private bool _closing;

    /// <summary>Initializes management commands without selecting an authenticator.</summary>
    /// <param name="dialogs">Application dialog service.</param>
    /// <param name="interaction">PIN and touch dialog service.</param>
    public SecurityKeyManagementViewModel(IDialogService dialogs, ISecurityKeyInteractionService interaction)
    {
        _dialogs = dialogs;
        _interaction = interaction;
        ReadAuthenticatorInfoCommand = new AsyncDelegateCommand(ReadAuthenticatorInfoAsync, () => CanListRoamingCredentials);
        CancelCommand = new DelegateCommand(Cancel, () => IsBusy);
        SetPinPolicyCommand = new AsyncDelegateCommand(SetPinPolicyAsync, () => CanUseSession);
        SetAlwaysUvCommand = new AsyncDelegateCommand<bool>(SetAlwaysUvAsync,
            _ => CanUseSession && Capabilities?.PinPolicy.AlwaysUvEnabled.HasValue == true);
        EnableEnterpriseAttestationCommand = new AsyncDelegateCommand(EnableEnterpriseAttestationAsync,
            () => CanUseSession && Capabilities?.PinPolicy.EnterpriseAttestationEnabled == false);
        DeleteRoamingCredentialCommand = new AsyncDelegateCommand<DiscoverableCredentialDetails>(DeleteRoamingCredentialAsync,
            credential => CanUseSession && credential != null && Credentials.Contains(credential));
        TestRoamingCredentialCommand = new AsyncDelegateCommand<DiscoverableCredentialDetails>(TestRoamingCredentialAsync,
            credential => CanListRoamingCredentials && !string.IsNullOrWhiteSpace(credential?.RelyingPartyId));
        SetPinCommand = new AsyncDelegateCommand(SetPinAsync,
            () => CanUseSession && Capabilities?.PinState == AuthenticatorPinState.Unset);
        ChangePinCommand = new AsyncDelegateCommand(ChangePinAsync,
            () => CanUseSession && Capabilities?.PinState == AuthenticatorPinState.Set);
        SelectAndListCredentialsCommand = new AsyncDelegateCommand(SelectAndListCredentialsAsync, () => CanListRoamingCredentials);
    }

    /// <summary>Whether this Windows API version exposes authenticator management.</summary>
    public bool IsSupported { get; } = WebAuthnApi.IsAuthenticatorManagementSupported;

    /// <summary>Whether the current process has the administrator rights required for device access.</summary>
    public bool IsElevated { get; } = GetIsElevated();

    /// <summary>Explains why device selection is disabled, or returns empty text when available.</summary>
    public string AvailabilityMessage => !IsSupported
        ? "Security key management is not supported by this Windows WebAuthn API version."
        : !IsElevated
            ? "Restart Passkey UI as administrator to manage a security key."
            : string.Empty;

    /// <summary>Whether the one-button roaming workflow can select or reselect a security key.</summary>
    public bool CanListRoamingCredentials => IsSupported && IsElevated && !IsBusy && !_closing;

    /// <summary>Whether an operation can use the selected device.</summary>
    public bool CanUseSession => _session != null && !IsBusy && !_closing;

    /// <summary>Whether selection or a management operation is in progress.</summary>
    public bool IsBusy
    {
        get;
        private set
        {
            if (SetProperty(ref field, value))
            {
                UpdateCommandStates();
            }
        }
    }

    /// <summary>Last operation result, including cancellation and errors.</summary>
    public string Status
    {
        get;
        private set => SetProperty(ref field, value);
    } = "No security key selected.";

    /// <summary>Last copied native capability and policy snapshot.</summary>
    public AuthenticatorManagementCapabilities? Capabilities
    {
        get;
        private set
        {
            if (SetProperty(ref field, value))
            {
                UpdateCommandStates();
            }
        }
    }

    /// <summary>Credentials returned by the last enumeration.</summary>
    public ObservableCollection<DiscoverableCredentialDetails> Credentials { get; } = [];

    /// <summary>Replaces the cached session and reads security-key information using the default transport.</summary>
    public AsyncDelegateCommand ReadAuthenticatorInfoCommand { get; }

    /// <summary>Requests cancellation of the pending native operation.</summary>
    public DelegateCommand CancelCommand { get; }

    /// <summary>Opens the context-prefilled PIN-policy dialog and applies its accepted request.</summary>
    public AsyncDelegateCommand SetPinPolicyCommand { get; }

    /// <summary>Confirms and sets Always Require User Verification.</summary>
    public AsyncDelegateCommand<bool> SetAlwaysUvCommand { get; }

    /// <summary>Confirms and enables enterprise attestation.</summary>
    public AsyncDelegateCommand EnableEnterpriseAttestationCommand { get; }

    /// <summary>Deletes the credential supplied by a table row after confirmation.</summary>
    public AsyncDelegateCommand<DiscoverableCredentialDetails> DeleteRoamingCredentialCommand { get; }

    /// <summary>Tests the credential supplied by a table row using a roaming WebAuthn assertion.</summary>
    public AsyncDelegateCommand<DiscoverableCredentialDetails> TestRoamingCredentialCommand { get; }

    /// <summary>Prompts for an initial PIN.</summary>
    public AsyncDelegateCommand SetPinCommand { get; }

    /// <summary>Prompts for the current and replacement PINs.</summary>
    public AsyncDelegateCommand ChangePinCommand { get; }

    /// <summary>Selects a key, requests its PIN, and lists credentials.</summary>
    public AsyncDelegateCommand SelectAndListCredentialsCommand { get; }

    /// <summary>Reports a completed assertion test to the main window for display and retesting.</summary>
    public event Action<AssertionPublicKeyCredential, string, byte[]>? CredentialTestCompleted;

    /// <summary>Installs an initial PIN after validated entry and confirmation in the PIN dialog.</summary>
    /// <returns>A task completing after the initial PIN attempt.</returns>
    public Task SetPinAsync() => RunAsync(async token =>
    {
        using SecureString? pin = _interaction.RequestNewPin(token);
        if (pin == null)
        {
            Status = "Initial PIN setup declined.";
            return;
        }
        var session = RequireSession();
        var result = await WithTouchAsync(() => session.SetPinAsync(pin, token), token);
        Capabilities = session.GetCapabilities(token);
        ReportResult(FormatPinResult(result, null));
    });

    /// <summary>Changes the PIN directly without a separate authentication attempt.</summary>
    /// <returns>A task completing after the PIN-change attempt.</returns>
    public Task ChangePinAsync() => RunAsync(async token =>
    {
        var session = RequireSession();
        if (await PromptForPinChangeAsync(session.GetCapabilities(token).PinRetries,
            input => WithTouchAsync(() => session.ChangePinAsync(input.CurrentPin, input.NewPin, token), token), token))
        {
            Capabilities = session.GetCapabilities(token);
            ReportResult("PIN change completed.");
        }
    });

    /// <summary>Replaces the previous selection and retains the authenticated session for credential deletion.</summary>
    /// <returns>A task completing after enumeration and session ownership transfer.</returns>
    public Task SelectAndListCredentialsAsync() => RunAsync(async token =>
    {
        CloseSession();
        AuthenticatorManagementSession? session = await SelectRoamingSessionAsync(token);
        try
        {
            if (!await AuthenticateRoamingSessionAsync(session, token))
            {
                return;
            }
            var credentials = await session.GetCredentialsAsync(token);
            foreach (var credential in credentials)
            {
                Credentials.Add(credential);
            }
            _session = session;
            session = null;
            Status = $"{credentials.Count} credential(s) returned.";
        }
        finally
        {
            session?.Dispose();
        }
    });

    /// <summary>Selects a roaming device using the API's default transport restriction.</summary>
    /// <param name="token">Selection cancellation token.</param>
    /// <returns>A caller-owned session to retain after successful listing or dispose on failure.</returns>
    private Task<AuthenticatorManagementSession> SelectRoamingSessionAsync(CancellationToken token) =>
        WithTouchAsync(() => Task.Run(() => AuthenticatorManagementSession.Create(cancellationToken: token), token),
            token, "Connect your security key and touch it to select it.");

    /// <summary>Prompts for the selected key's PIN using its context retry count and repeats after a rejected PIN.</summary>
    /// <param name="session">Selected session to authenticate.</param>
    /// <param name="token">Operation cancellation token.</param>
    /// <returns>True when authentication returns without a PIN diagnostic; false after cancellation or a reported diagnostic.</returns>
    private Task<bool> AuthenticateRoamingSessionAsync(AuthenticatorManagementSession session, CancellationToken token) =>
        PromptForPinAsync(session.GetCapabilities(token).PinRetries,
            pin => session.AuthenticatePinAsync(pin, token), token);

    /// <summary>Repeats PIN entry only after an explicit rejection, never automatically resubmitting a PIN.</summary>
    /// <param name="retries">Initial context retry count; zero may mean unreported.</param>
    /// <param name="submit">Submits each newly entered PIN once.</param>
    /// <param name="token">Cancels prompting and submission.</param>
    /// <returns>True if no PIN diagnostic was returned; false on dismissal or a terminal diagnostic.</returns>
    private Task<bool> PromptForPinAsync(uint retries,
        Func<SecureString, Task<(PinAuthenticationStatus Status, uint Retries)>> submit, CancellationToken token) =>
        PromptForPinInputAsync(retries,
            (remaining, rejected) => _interaction.RequestPin("Security Key PIN", remaining, rejected, token), submit, token);

    /// <summary>Repeats the complete PIN-change dialog after an incorrect current PIN.</summary>
    /// <param name="retries">Initial context retry count; zero may mean unreported.</param>
    /// <param name="submit">Submits each newly entered PIN pair once.</param>
    /// <param name="token">Cancels prompting and submission.</param>
    /// <returns>True after success; false after dismissal or a terminal diagnostic.</returns>
    private Task<bool> PromptForPinChangeAsync(uint retries,
        Func<PinChangeInput, Task<(PinAuthenticationStatus Status, uint Retries)>> submit, CancellationToken token) =>
        PromptForPinInputAsync(retries,
            (remaining, rejected) => _interaction.RequestPinChange(remaining, rejected, token), submit, token);

    /// <summary>Coordinates secure input ownership and retry diagnostics for PIN operations.</summary>
    /// <typeparam name="T">Disposable secure input type.</typeparam>
    /// <param name="retries">Initial retry count; zero may mean unreported.</param>
    /// <param name="request">Requests fresh input with retry feedback.</param>
    /// <param name="submit">Submits the accepted input once.</param>
    /// <param name="token">Cancels prompting and submission.</param>
    /// <returns>True after success; false after dismissal or a terminal diagnostic.</returns>
    private async Task<bool> PromptForPinInputAsync<T>(uint retries, Func<uint?, bool, T?> request,
        Func<T, Task<(PinAuthenticationStatus Status, uint Retries)>> submit, CancellationToken token) where T : class, IDisposable
    {
        bool rejected = false;
        while (true)
        {
            token.ThrowIfCancellationRequested();
            using T? pin = request(retries > 0 ? retries : null, rejected);
            if (pin == null)
            {
                Status = "PIN submission declined.";
                return false;
            }
            var result = await submit(pin);
            if (result.Status == PinAuthenticationStatus.Unspecified)
            {
                return true;
            }
            retries = result.Retries;
            if (result.Status == PinAuthenticationStatus.PinVerificationFailed)
            {
                rejected = true;
                continue;
            }
            ReportResult(FormatPinResult(result.Status, result.Retries));
            return false;
        }
    }

    /// <summary>Confirms deletion and reuses the authenticated session from the last successful listing.</summary>
    /// <param name="credential">Exact credential selected by the row action.</param>
    /// <returns>A task completing after deletion and removal of the displayed record.</returns>
    private Task DeleteRoamingCredentialAsync(DiscoverableCredentialDetails? credential) => RunAsync(async token =>
    {
        ArgumentNullException.ThrowIfNull(credential);
        var session = RequireSession();
        if (!Credentials.Contains(credential))
        {
            throw new ArgumentException("The credential must belong to the current listing.", nameof(credential));
        }
        if (!await ConfirmCredentialDeletionAsync(credential))
        {
            Status = "Credential deletion declined.";
            return;
        }
        await session.DeleteCredentialAsync(credential.CredentialId, token);
        Credentials.Remove(credential);
        Status = "Credential deletion completed.";
    });

    /// <summary>Requests an assertion for the exact credential using a new random challenge and roaming attachment.</summary>
    /// <param name="credential">Credential supplied by the selected table row.</param>
    /// <returns>A task completing after the WebAuthn test and publication of its assertion.</returns>
    private Task TestRoamingCredentialAsync(DiscoverableCredentialDetails? credential) => RunAsync(async token =>
    {
        if (credential == null || string.IsNullOrWhiteSpace(credential.RelyingPartyId))
        {
            throw new ArgumentException("The credential must have a relying-party ID.");
        }
        var api = new WebAuthnApi();
        byte[] challenge = RandomNumberGenerator.GetBytes(ApiConstants.DefaultChallengeLength);
        var response = await api.AuthenticatorGetAssertionAsync(
            credential.RelyingPartyId,
            challenge,
            UserVerificationRequirement.Preferred,
            AuthenticatorAttachment.CrossPlatform,
            ApiConstants.DefaultTimeoutMilliseconds,
            [new PublicKeyCredentialDescriptor(credential.CredentialId, AuthenticatorTransport.USB | AuthenticatorTransport.NFC)],
            windowHandle: WindowHandle.MainWindow,
            cancellationToken: token);
        Status = "Credential test returned an assertion. No server-side verification was performed.";
        CredentialTestCompleted?.Invoke(response, credential.RelyingPartyId, challenge);
    });

    /// <summary>Cancels and drains pending work before releasing the session at application shutdown.</summary>
    /// <returns>A task completing when no native operation still owns session resources.</returns>
    public async ValueTask DisposeAsync()
    {
        _closing = true;
        UpdateCommandStates();
        Cancel();
        if (_operationCompletion != null)
        {
            await _operationCompletion.Task;
        }
        CloseSession();
    }

    /// <summary>Checks the current process token without requesting elevation.</summary>
    /// <returns>True when the process belongs to the enabled administrators role.</returns>
    private static bool GetIsElevated()
    {
        using WindowsIdentity identity = WindowsIdentity.GetCurrent();
        return new WindowsPrincipal(identity).IsInRole(WindowsBuiltInRole.Administrator);
    }

    /// <summary>Formats PIN diagnostics without treating the absence of a diagnostic as proof of authentication.</summary>
    /// <param name="status">Native PIN diagnostic.</param>
    /// <param name="retries">Native retry count, when supplied by the operation.</param>
    /// <returns>Diagnostic text suitable for a result dialog.</returns>
    private static string FormatPinResult(PinAuthenticationStatus status, uint? retries) =>
        $"PIN operation returned: {status}." +
        (retries.HasValue ? $" Remaining retries: {retries.Value} (zero may mean unreported)." : string.Empty) +
        " No automatic retry was made.";

    /// <summary>Releases the previous selection before reading a new key's information and caching its session.</summary>
    /// <returns>A task completing after selection, snapshot display, and ownership transfer.</returns>
    public Task ReadAuthenticatorInfoAsync() => RunAsync(async token =>
    {
        CloseSession();
        AuthenticatorManagementSession? session = await SelectRoamingSessionAsync(token);
        try
        {
            Capabilities = session.GetCapabilities(token);
            _session = session;
            session = null;
            Status = "Security key information read. No PIN has been submitted.";
        }
        finally
        {
            session?.Dispose();
        }
    });

    /// <summary>Releases selection only after pending work has completed.</summary>
    private void CloseSession()
    {
        _session?.Dispose();
        _session = null;
        Capabilities = null;
        Credentials.Clear();
        Status = "No security key selected.";
        UpdateCommandStates();
    }

    /// <summary>Requests native cancellation without disposing the session.</summary>
    private void Cancel()
    {
        if (_cancellation != null)
        {
            _cancellation.Cancel();
            Status = "Cancellation requested. Waiting for the operation to finish; a write may already have applied.";
        }
    }

    /// <summary>Requests confirmation before irreversible credential deletion.</summary>
    /// <param name="credential">Credential targeted by the operation.</param>
    /// <returns>True only after explicit confirmation.</returns>
    private Task<bool> ConfirmCredentialDeletionAsync(DiscoverableCredentialDetails credential)
    {
        string? account = string.IsNullOrWhiteSpace(credential.UserDisplayName)
            ? credential.UserName
            : string.IsNullOrWhiteSpace(credential.UserName)
                ? credential.UserDisplayName
                : $"{credential.UserDisplayName} ({credential.UserName})";
        return ConfirmAsync("Delete Security Key Credential",
            $"Permanently delete this credential?\n\nRelying party: {credential.RelyingPartyId}\nAccount: {account}\n\nThis cannot be undone.");
    }

    /// <summary>Requests policy changes with defaults copied from the current context.</summary>
    /// <returns>A task completing after policy dispatch or dismissal of the editing dialog.</returns>
    private Task SetPinPolicyAsync() => RunAsync(async token =>
    {
        var session = RequireSession();
        var policy = session.GetPinPolicy(token);
        var input = _interaction.RequestPinPolicyChange(policy.MinimumPinLength, policy.ForcePinChangeRequired,
            policy.MaximumRpIds, token);
        if (input == null)
        {
            Status = "PIN policy change declined.";
            return;
        }
        if (!await AuthenticateRoamingSessionAsync(session, token))
        {
            return;
        }
        await WithTouchAsync(() => session.SetPinPolicyAsync(input.MinimumPinLength, input.ForcePinChange == true,
            input.RelyingPartyIds, token), token);
        Capabilities = session.GetCapabilities(token);
        ReportResult("PIN policy request completed. Read security key info again to reload.");
    });

    /// <summary>Confirms and applies the requested user-verification state.</summary>
    /// <param name="enabled">Whether to require user verification.</param>
    /// <returns>A task completing after configuration dispatch.</returns>
    private Task SetAlwaysUvAsync(bool enabled) => RunAsync(async token =>
    {
        if (Capabilities?.PinPolicy.AlwaysUvEnabled.HasValue != true)
        {
            throw new InvalidOperationException("User-verification information is unavailable. Read security key info first.");
        }
        if (!await ConfirmAsync("Always Require User Verification",
            $"{(enabled ? "Enable" : "Disable")} Always Require User Verification on this security key?"))
        {
            Status = "User-verification change declined.";
            return;
        }
        var session = RequireSession();
        if (!await AuthenticateRoamingSessionAsync(session, token))
        {
            return;
        }
        await WithTouchAsync(() => session.SetAlwaysUvAsync(enabled, token), token);
        Capabilities = session.GetCapabilities(token);
        ReportResult("User-verification request completed. Read security key info again to reload.");
    });

    /// <summary>Warns about privacy and irreversibility before enabling enterprise attestation.</summary>
    /// <returns>A task completing after configuration dispatch.</returns>
    private Task EnableEnterpriseAttestationAsync() => RunAsync(async token =>
    {
        if (!await ConfirmAsync("Enable Enterprise Attestation",
            "Enable enterprise attestation? It can expose identifying information. This API cannot disable it."))
        {
            Status = "Enterprise attestation change declined.";
            return;
        }
        var session = RequireSession();
        if (!await AuthenticateRoamingSessionAsync(session, token))
        {
            return;
        }
        await WithTouchAsync(() => session.EnableEnterpriseAttestationAsync(token), token);
        Capabilities = session.GetCapabilities(token);
        ReportResult("Enterprise attestation request completed. Read security key info again to reload.");
    });

    /// <summary>Shows a touch prompt only while device work is pending, closing it before result dialogs.</summary>
    /// <typeparam name="T">Operation result type.</typeparam>
    /// <param name="operation">Pending device work.</param>
    /// <param name="token">Operation cancellation token.</param>
    /// <param name="prompt">Instructions for the current device interaction.</param>
    /// <returns>The native operation result.</returns>
    private Task<T> WithTouchAsync<T>(Func<Task<T>> operation, CancellationToken token,
        string prompt = "Touch your security key if it requests user presence. Waiting for the operation to complete.") =>
        _interaction.WaitAsync(prompt, operation, Cancel, token);

    /// <summary>Adapts a result-free device operation to the shared touch dialog lifecycle.</summary>
    /// <param name="operation">Pending device work.</param>
    /// <param name="token">Operation cancellation token.</param>
    /// <returns>A task completing after device work and prompt cleanup.</returns>
    private Task WithTouchAsync(Func<Task> operation, CancellationToken token) =>
        WithTouchAsync(async () => { await operation(); return true; }, token);

    /// <summary>Displays results that are not otherwise visible in a credential table or capability snapshot.</summary>
    /// <param name="message">Operation result or diagnostic.</param>
    private void ReportResult(string message)
    {
        Status = message;
        if (!_closing)
        {
            _dialogs.ShowNotificationDialog(message, "Security Key");
        }
    }

    /// <summary>Serializes UI operations and reports all native, validation, and file errors.</summary>
    /// <param name="operation">Operation that owns caller resources until its returned task completes.</param>
    /// <returns>A task completing after reporting and cleanup.</returns>
    [SuppressMessage("Design", "CA1031:Do not catch general exception types",
        Justification = "This UI boundary reports operation failures through status text and notification dialogs.")]
    private async Task RunAsync(Func<CancellationToken, Task> operation)
    {
        if (IsBusy || _closing)
        {
            throw new InvalidOperationException("Another operation is pending or the application is closing.");
        }
        _cancellation = new CancellationTokenSource();
        var completion = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        _operationCompletion = completion;
        IsBusy = true;
        Status = "Operation in progress. Touch the selected security key if requested.";
        try
        {
            await operation(_cancellation.Token);
        }
        catch (OperationCanceledException)
        {
            Status = "Operation canceled.";
        }
        catch (Exception ex)
        {
            Status = $"Operation failed: {ex.Message}";
            if (!_closing)
            {
                _dialogs.ShowNotificationDialog(Status);
            }
        }
        finally
        {
            _cancellation.Dispose();
            _cancellation = null;
            IsBusy = false;
            completion.SetResult();
            _operationCompletion = null;
        }
    }

    /// <summary>Requires an existing selection rather than silently opening a different device.</summary>
    /// <returns>The selected session.</returns>
    private AuthenticatorManagementSession RequireSession() =>
        _session ?? throw new InvalidOperationException("Select a security key first.");

    /// <summary>Adapts the existing application confirmation dialog to an awaitable result.</summary>
    /// <param name="title">Dialog title.</param>
    /// <param name="message">Operation-specific warning.</param>
    /// <returns>True only when the user explicitly confirms.</returns>
    private Task<bool> ConfirmAsync(string title, string message)
    {
        var completion = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        _dialogs.ShowConfirmationDialog(new ConfirmationDialogParameters { Title = title, Message = message },
            confirmed => completion.SetResult(confirmed));
        return completion.Task;
    }

    /// <summary>Updates bindings and all commands after a selection, busy-state, or shutdown transition.</summary>
    private void UpdateCommandStates()
    {
        RaisePropertyChanged(nameof(CanListRoamingCredentials));
        RaisePropertyChanged(nameof(CanUseSession));
        ReadAuthenticatorInfoCommand.RaiseCanExecuteChanged();
        CancelCommand.RaiseCanExecuteChanged();
        SetPinPolicyCommand.RaiseCanExecuteChanged();
        SetAlwaysUvCommand.RaiseCanExecuteChanged();
        EnableEnterpriseAttestationCommand.RaiseCanExecuteChanged();
        DeleteRoamingCredentialCommand.RaiseCanExecuteChanged();
        TestRoamingCredentialCommand.RaiseCanExecuteChanged();
        SetPinCommand.RaiseCanExecuteChanged();
        ChangePinCommand.RaiseCanExecuteChanged();
        SelectAndListCredentialsCommand.RaiseCanExecuteChanged();
    }
}
