using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using DSInternals.Win32.WebAuthn.COSE;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Dialogs;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

public class AssertionSigningDialogViewModel : BindableBase, IDialogAware
{
    private static readonly JsonSerializerOptions IndentedJson = new() { WriteIndented = true };

    private const string FileFilter = "All supported files (*.pem;*.passkey)|*.pem;*.passkey|PEM files (*.pem)|*.pem|KeePassXC passkey (*.passkey)|*.passkey|All files (*.*)|*.*";

    // Dialog parameters received from caller
    private string? _relyingPartyId;
    private byte[]? _challenge;

    // Loaded passkey (if file is .passkey)
    private KeePassXCPasskey? _loadedPasskey;

    private readonly IDialogService _dialogService;

    public AssertionSigningDialogViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;
        SignCommand = new DelegateCommand(OnSign, CanSign)
            .ObservesProperty(() => KeyFilePath)
            .ObservesProperty(() => CredentialIdString);
        BrowseKeyFileCommand = new DelegateCommand(OnBrowseKeyFile);
        GenerateKeyPairCommand = new DelegateCommand(OnGenerateKeyPair);
        SelectPresetCommand = new DelegateCommand<AuthenticatorPreset>(OnSelectPreset);
        IncrementCounterCommand = new DelegateCommand(() => SignatureCounter++);
        DecrementCounterCommand = new DelegateCommand(() => { if (SignatureCounter > 0) SignatureCounter--; });
    }

    public string Title { get; set; } = "Sign Assertion";

    public DialogCloseListener RequestClose { get; }

    public DelegateCommand SignCommand { get; }
    public DelegateCommand BrowseKeyFileCommand { get; }
    public DelegateCommand GenerateKeyPairCommand { get; }
    public DelegateCommand<AuthenticatorPreset> SelectPresetCommand { get; }
    public DelegateCommand IncrementCounterCommand { get; }
    public DelegateCommand DecrementCounterCommand { get; }

    private void OnSelectPreset(AuthenticatorPreset preset)
    {
        AaGuidString = preset.AaGuid.ToString();
        SelectedAlgorithm = preset.DefaultAlgorithm;
    }

    // Authenticator parameters
    public string? AaGuidString
    {
        get;
        set => SetProperty(ref field, value);
    } = Guid.Empty.ToString();

    public Algorithm SelectedAlgorithm
    {
        get;
        set => SetProperty(ref field, value);
    } = Algorithm.ES256;

    public IList<KeyValuePair<Algorithm, string>> Algorithms => AuthenticatorPreset.AlgorithmItems;

    public string? CredentialIdString
    {
        get;
        set => SetProperty(ref field, value);
    }

    public string? UserHandleString
    {
        get;
        set => SetProperty(ref field, value);
    }

    public uint SignatureCounter
    {
        get;
        set => SetProperty(ref field, value);
    }

    public bool UserPresent
    {
        get;
        set => SetProperty(ref field, value);
    } = true;

    public bool UserVerified
    {
        get;
        set => SetProperty(ref field, value);
    } = true;

    // Signing key
    public string? KeyFilePath
    {
        get;
        set => SetProperty(ref field, value);
    }

    public bool CanCloseDialog() => true;

    public void OnDialogClosed() { }

    public void OnDialogOpened(IDialogParameters parameters)
    {
        _relyingPartyId = parameters.GetValue<string>("RelyingPartyId");
        _challenge = parameters.GetValue<byte[]>("Challenge");

        // Pre-populate UV from the assertion options form
        var uvRequirement = parameters.GetValue<UserVerificationRequirement>("UserVerificationRequirement");
        UserVerified = uvRequirement is UserVerificationRequirement.Required or UserVerificationRequirement.Preferred;

        // Restore cached values from previous dialog invocations
        RestoreFromCache();

        // Pre-populate credential ID from attestation options if provided, otherwise use cached value
        var credentialId = parameters.GetValue<string>("CredentialId");
        if (!string.IsNullOrWhiteSpace(credentialId))
        {
            CredentialIdString = credentialId;
        }
    }

    private void RestoreFromCache()
    {
        if (SigningDialogCache.AaGuidString != null)
            AaGuidString = SigningDialogCache.AaGuidString;

        if (SigningDialogCache.SelectedAlgorithm.HasValue)
            SelectedAlgorithm = SigningDialogCache.SelectedAlgorithm.Value;

        if (SigningDialogCache.SignatureCounter.HasValue)
            SignatureCounter = SigningDialogCache.SignatureCounter.Value;

        if (SigningDialogCache.KeyFilePath != null)
        {
            _loadedPasskey = SigningDialogCache.LoadedPasskey;
            KeyFilePath = SigningDialogCache.KeyFilePath;
        }

        if (SigningDialogCache.UserPresent.HasValue)
            UserPresent = SigningDialogCache.UserPresent.Value;

        if (SigningDialogCache.CredentialIdString != null)
            CredentialIdString = SigningDialogCache.CredentialIdString;
    }

    private void SaveToCache()
    {
        SigningDialogCache.AaGuidString = AaGuidString;
        SigningDialogCache.SelectedAlgorithm = SelectedAlgorithm;
        SigningDialogCache.SignatureCounter = SignatureCounter;
        SigningDialogCache.KeyFilePath = KeyFilePath;
        SigningDialogCache.LoadedPasskey = _loadedPasskey;
        SigningDialogCache.UserPresent = UserPresent;
        SigningDialogCache.CredentialIdString = CredentialIdString;
    }

    private bool CanSign()
    {
        return !string.IsNullOrWhiteSpace(KeyFilePath)
            && !string.IsNullOrWhiteSpace(CredentialIdString);
    }

    private void OnSign()
    {
        try
        {
            if (_relyingPartyId == null || _challenge == null)
            {
                throw new InvalidOperationException("Missing assertion options. Please fill in the authentication form before signing.");
            }

            byte[] credentialId = Base64UrlConverter.FromBase64UrlString(CredentialIdString!);

            byte[]? userHandle = null;
            if (!string.IsNullOrWhiteSpace(UserHandleString))
            {
                userHandle = Base64UrlConverter.FromBase64UrlString(UserHandleString!);
            }

            using var privateKey = LoadPrivateKey();

            AuthenticatorFlags flags = 0;
            if (UserPresent) flags |= AuthenticatorFlags.UserPresent;
            if (UserVerified) flags |= AuthenticatorFlags.UserVerified;

            var credential = SoftwareAuthenticator.GetAssertion(
                _relyingPartyId,
                _challenge,
                SelectedAlgorithm,
                SignatureCounter,
                flags,
                credentialId,
                userHandle,
                privateKey);

            string json = JsonSerializer.Serialize(credential, IndentedJson);

            SaveToCache();

            var result = new DialogParameters
            {
                { "Response", json }
            };

            RequestClose.Invoke(result, ButtonResult.OK);
        }
        catch (Exception ex)
        {
            _dialogService.ShowDialog(nameof(NotificationDialog), new DialogParameters($"Message={ex.Message}"));
        }
    }

    private AsymmetricAlgorithm LoadPrivateKey()
    {
        if (_loadedPasskey != null)
        {
            return _loadedPasskey.LoadPrivateKey();
        }

        return SoftwareAuthenticator.LoadPrivateKeyFromPem(KeyFilePath!);
    }

    private void OnBrowseKeyFile()
    {
        var dialog = new OpenFileDialog
        {
            Title = "Select Key File",
            Filter = FileFilter,
            CheckFileExists = true
        };

        if (dialog.ShowDialog() == true)
        {
            LoadKeyFile(dialog.FileName);
        }
    }

    private void LoadKeyFile(string filePath)
    {
        _loadedPasskey = null;

        try
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();

            if (extension == ".passkey")
            {
                LoadPasskeyFile(filePath);
            }
            else
            {
                KeyFilePath = filePath;
                ValidateLoadedKey();
            }
        }
        catch (Exception ex)
        {
            _dialogService.ShowDialog(nameof(NotificationDialog), new DialogParameters($"Message={ex.Message}"));
        }
    }

    private void LoadPasskeyFile(string filePath)
    {
        var passkey = KeePassXCPasskey.LoadFromFile(filePath);

        // Validate RP ID
        if (_relyingPartyId != null && passkey.EffectiveRpId != null
            && !string.Equals(_relyingPartyId, passkey.EffectiveRpId, StringComparison.OrdinalIgnoreCase))
        {
            _dialogService.ShowDialog(nameof(NotificationDialog), new DialogParameters($"Message=The passkey's relying party \"{passkey.EffectiveRpId}\" does not match the current RP ID \"{_relyingPartyId}\"."));
            return;
        }

        // Validate private key presence
        if (passkey.GetPrivateKeyPem() == null)
        {
            _dialogService.ShowDialog(nameof(NotificationDialog), new DialogParameters("Message=No private key found in the passkey file."));
            return;
        }

        _loadedPasskey = passkey;
        KeyFilePath = filePath;

        // Auto-fill credential ID
        string? credIdBase64Url = passkey.GetCredentialIdBase64Url();
        if (!string.IsNullOrWhiteSpace(credIdBase64Url))
        {
            CredentialIdString = credIdBase64Url;
        }

        // Auto-fill user handle
        if (!string.IsNullOrWhiteSpace(passkey.UserHandle))
        {
            UserHandleString = passkey.UserHandle;
        }

        // Auto-fill sign count
        if (passkey.SignCount.HasValue)
        {
            SignatureCounter = (uint)passkey.SignCount.Value;
        }

        // Auto-detect algorithm from the key
        try
        {
            using var key = passkey.LoadPrivateKey();
            SelectedAlgorithm = SoftwareAuthenticator.DetectAlgorithm(key);
        }
        catch
        {
            // Keep current algorithm if detection fails
        }
    }

    private void ValidateLoadedKey()
    {
        try
        {
            using var key = SoftwareAuthenticator.LoadPrivateKeyFromPem(KeyFilePath!);
            string? error = SoftwareAuthenticator.ValidateKeyForAlgorithm(key, SelectedAlgorithm);
            if (error != null)
            {
                _dialogService.ShowDialog(nameof(NotificationDialog), new DialogParameters($"Message={error}"));
            }
        }
        catch (Exception ex)
        {
            _dialogService.ShowDialog(nameof(NotificationDialog), new DialogParameters($"Message={ex.Message}"));
        }
    }

    private void OnGenerateKeyPair()
    {
        try
        {
            string pem = SoftwareAuthenticator.GenerateKeyPairPem(SelectedAlgorithm);

            var dialog = new SaveFileDialog
            {
                Title = "Save Generated Private Key",
                Filter = "PEM files (*.pem)|*.pem",
                DefaultExt = ".pem",
                FileName = $"webauthn-{SelectedAlgorithm.ToString().ToLowerInvariant()}-key.pem"
            };

            if (dialog.ShowDialog() == true)
            {
                System.IO.File.WriteAllText(dialog.FileName, pem);
                _loadedPasskey = null;
                KeyFilePath = dialog.FileName;
            }
        }
        catch (Exception ex)
        {
            _dialogService.ShowDialog(nameof(NotificationDialog), new DialogParameters($"Message={ex.Message}"));
        }
    }
}
