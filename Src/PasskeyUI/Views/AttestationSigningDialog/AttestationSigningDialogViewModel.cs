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

public class AttestationSigningDialogViewModel : BindableBase, IDialogAware
{
    private static readonly JsonSerializerOptions IndentedJson = new() { WriteIndented = true };

    private const string FileFilter = "All supported files (*.pem;*.passkey)|*.pem;*.passkey|PEM files (*.pem)|*.pem|KeePassXC passkey (*.passkey)|*.passkey|All files (*.*)|*.*";

    // Dialog parameters received from caller
    private RelyingPartyInformation? _relyingParty;
    private UserInformation? _user;
    private byte[]? _challenge;

    // Loaded passkey (if file is .passkey)
    private KeePassXCPasskey? _loadedPasskey;

    private readonly IDialogService _dialogService;

    public AttestationSigningDialogViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;
        SignCommand = new DelegateCommand(OnSign, CanSign)
            .ObservesProperty(() => KeyFilePath)
            .ObservesProperty(() => AaGuidString);
        BrowseKeyFileCommand = new DelegateCommand(OnBrowseKeyFile);
        GenerateKeyPairCommand = new DelegateCommand(OnGenerateKeyPair);
        SelectPresetCommand = new DelegateCommand<AuthenticatorPreset>(OnSelectPreset);
        IncrementCounterCommand = new DelegateCommand(() => SignatureCounter++);
        DecrementCounterCommand = new DelegateCommand(() => { if (SignatureCounter > 0) SignatureCounter--; });
    }

    public string Title { get; set; } = "Sign Attestation";

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
        _relyingParty = parameters.GetValue<RelyingPartyInformation>("RelyingParty");
        _user = parameters.GetValue<UserInformation>("User");
        _challenge = parameters.GetValue<byte[]>("Challenge");

        // Use first preferred algorithm if available
        var algorithms = parameters.GetValue<List<Algorithm>>("Algorithms");
        if (algorithms?.Count > 0)
        {
            SelectedAlgorithm = algorithms[0];
        }

        // Pre-populate UV from the attestation options form
        var uvRequirement = parameters.GetValue<UserVerificationRequirement>("UserVerificationRequirement");
        UserVerified = uvRequirement is UserVerificationRequirement.Required or UserVerificationRequirement.Preferred;

        // Restore cached values from previous dialog invocations
        RestoreFromCache();
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
    }

    private void SaveToCache()
    {
        SigningDialogCache.AaGuidString = AaGuidString;
        SigningDialogCache.SelectedAlgorithm = SelectedAlgorithm;
        SigningDialogCache.SignatureCounter = SignatureCounter;
        SigningDialogCache.KeyFilePath = KeyFilePath;
        SigningDialogCache.LoadedPasskey = _loadedPasskey;
        SigningDialogCache.UserPresent = UserPresent;
    }

    private bool CanSign()
    {
        return !string.IsNullOrWhiteSpace(KeyFilePath)
            && Guid.TryParse(AaGuidString, out _);
    }

    private void OnSign()
    {
        try
        {
            if (_relyingParty == null || _user == null || _challenge == null)
            {
                throw new InvalidOperationException("Missing attestation options. Please fill in the registration form before signing.");
            }

            if (!Guid.TryParse(AaGuidString, out Guid aaGuid))
            {
                throw new FormatException("Invalid AAGUID format.");
            }

            using var privateKey = LoadPrivateKey();

            AuthenticatorFlags flags = 0;
            if (UserPresent) flags |= AuthenticatorFlags.UserPresent;
            if (UserVerified) flags |= AuthenticatorFlags.UserVerified;

            var credential = SoftwareAuthenticator.MakeCredential(
                _relyingParty,
                _user,
                _challenge,
                SelectedAlgorithm,
                aaGuid,
                SignatureCounter,
                flags,
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
        if (_relyingParty?.Id != null && passkey.EffectiveRpId != null
            && !string.Equals(_relyingParty.Id, passkey.EffectiveRpId, StringComparison.OrdinalIgnoreCase))
        {
            _dialogService.ShowDialog(nameof(NotificationDialog), new DialogParameters($"Message=The passkey's relying party \"{passkey.EffectiveRpId}\" does not match the current RP ID \"{_relyingParty.Id}\"."));
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

        // Auto-fill sign count
        if (passkey.SignCount.HasValue)
        {
            SignatureCounter = (uint)passkey.SignCount.Value;
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
