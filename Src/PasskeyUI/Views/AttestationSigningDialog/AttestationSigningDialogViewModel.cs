using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.Cryptography;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Dialogs;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

internal sealed class AttestationSigningDialogViewModel : BindableBase, IDialogAware
{
    private static readonly JsonSerializerOptions IndentedJson = new() { WriteIndented = true };
    private static readonly WebAuthnJsonContext IndentedJsonContext = new(IndentedJson);

    private const string FileFilter = "All supported files (*.pem;*.passkey)|*.pem;*.passkey|PEM files (*.pem)|*.pem|KeePassXC passkey (*.passkey)|*.passkey|All files (*.*)|*.*";

    // Dialog parameters received from caller
    private RelyingPartyInformation? _relyingParty;
    private UserInformation? _user;
    private byte[]? _challenge;
    private Algorithm _defaultAlgorithm = Algorithm.ES256;

    // Loaded passkey (if file is .passkey)
    private KeePassXCPasskey? _loadedPasskey;

    private readonly IDialogService _dialogService;
    private readonly SigningDialogCache _cache;

    public AttestationSigningDialogViewModel(IDialogService dialogService, SigningDialogCache cache)
    {
        _dialogService = dialogService;
        _cache = cache;
        SignCommand = new DelegateCommand(OnSign, CanSign)
            .ObservesProperty(() => KeyFilePath)
            .ObservesProperty(() => CredentialId);
        BrowseKeyFileCommand = new DelegateCommand(OnBrowseKeyFile);
        GenerateKeyPairCommand = new DelegateCommand(OnGenerateKeyPair);
        GenerateCredentialIdCommand = new DelegateCommand(OnGenerateCredentialId);
        SelectPresetCommand = new DelegateCommand<AuthenticatorPreset>(preset => { AaGuid = preset.AaGuid; });
        IncrementCounterCommand = new DelegateCommand(() => SignatureCounter++);
        DecrementCounterCommand = new DelegateCommand(() => { if (SignatureCounter > 0) SignatureCounter--; });
    }

    public string Title { get; set; } = "Sign Attestation";

    public DialogCloseListener RequestClose { get; }

    public DelegateCommand SignCommand { get; }
    public DelegateCommand BrowseKeyFileCommand { get; }
    public DelegateCommand GenerateKeyPairCommand { get; }
    public DelegateCommand GenerateCredentialIdCommand { get; }
    public DelegateCommand<AuthenticatorPreset> SelectPresetCommand { get; }
    public DelegateCommand IncrementCounterCommand { get; }
    public DelegateCommand DecrementCounterCommand { get; }

    // Authenticator parameters
    public Guid AaGuid
    {
        get;
        set => SetProperty(ref field, value);
    } = Guid.Empty;

    public Algorithm? SelectedAlgorithm
    {
        get;
        private set => SetProperty(ref field, value);
    }

    public byte[]? CredentialId
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
        var p = AttestationSigningDialogParameters.From(parameters);
        _relyingParty = p.RelyingParty;
        _user = p.User;
        _challenge = p.Challenge;
        UserVerified = p.UserVerificationRequirement is UserVerificationRequirement.Required or UserVerificationRequirement.Preferred;
        _defaultAlgorithm = p.DefaultAlgorithm;

        // Priority 3: restore cached values (excluding key file)
        RestoreFromCache();

        // Priority 2: last attestation credential ID from caller
        if (p.LastCredentialId is { Length: > 0 })
            CredentialId = p.LastCredentialId;

        // Priority 1: load cached key file (.passkey credential ID wins)
        if (_cache.KeyFilePath != null)
        {
            if (File.Exists(_cache.KeyFilePath))
                LoadKeyFile(_cache.KeyFilePath);
            else
                _cache.KeyFilePath = null;
        }
    }

    private void RestoreFromCache()
    {
        if (_cache.AaGuid.HasValue)
            AaGuid = _cache.AaGuid.Value;

        if (_cache.SignatureCounter.HasValue)
            SignatureCounter = _cache.SignatureCounter.Value;

        if (_cache.UserPresent.HasValue)
            UserPresent = _cache.UserPresent.Value;

        if (_cache.CredentialId != null)
            CredentialId = _cache.CredentialId;
    }

    private void SaveToCache()
    {
        _cache.AaGuid = AaGuid;
        _cache.SignatureCounter = SignatureCounter;
        _cache.KeyFilePath = KeyFilePath;
        _cache.UserPresent = UserPresent;
        _cache.CredentialId = CredentialId;
    }

    private bool CanSign() => !string.IsNullOrWhiteSpace(KeyFilePath) && CredentialId is { Length: > 0 };

    private void OnSign()
    {
        try
        {
            if (_relyingParty == null || _user == null || _challenge == null)
            {
                throw new InvalidOperationException("Missing attestation options. Please fill in the registration form before signing.");
            }

            using var privateKey = LoadPrivateKey();

            AuthenticatorFlags flags = 0;
            if (UserPresent) flags |= AuthenticatorFlags.UserPresent;
            if (UserVerified) flags |= AuthenticatorFlags.UserVerified;

            var credential = SoftwareAuthenticator.MakeCredential(
                _relyingParty,
                _user,
                _challenge,
                SelectedAlgorithm ?? Algorithm.ES256,
                AaGuid,
                SignatureCounter,
                flags,
                privateKey,
                CredentialId);

            string json = JsonSerializer.Serialize(credential, IndentedJsonContext.AttestationPublicKeyCredential);

            SaveToCache();

            RequestClose.Invoke(new SigningDialogResult { Response = json }.ToDialogParameters(), ButtonResult.OK);
        }
        catch (Exception ex)
        {
            _dialogService.ShowNotificationDialog(ex.Message);
        }
    }

    private AsymmetricAlgorithm LoadPrivateKey()
    {
        if (_loadedPasskey != null)
        {
            return _loadedPasskey.GetPasskeys()[0].PrivateKey;
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
                DetectKeyAlgorithm();
            }
        }
        catch (Exception ex)
        {
            _dialogService.ShowNotificationDialog(ex.Message);
        }
    }

    private void LoadPasskeyFile(string filePath)
    {
        var passkey = KeePassXCPasskey.LoadFromFile(filePath);

        // Validate RP ID
        if (_relyingParty?.Id != null && passkey.RelyingParty != null
            && !string.Equals(_relyingParty.Id, passkey.RelyingParty, StringComparison.OrdinalIgnoreCase))
        {
            _dialogService.ShowNotificationDialog($"The passkey's relying party \"{passkey.RelyingParty}\" does not match the current RP ID \"{_relyingParty.Id}\".");
            return;
        }

        // Validate private key presence
        if (string.IsNullOrWhiteSpace(passkey.PrivateKey))
        {
            _dialogService.ShowNotificationDialog("No private key found in the passkey file.");
            return;
        }

        _loadedPasskey = passkey;
        KeyFilePath = filePath;

        // Auto-fill credential ID (priority 1)
        if (passkey.CredentialId is { Length: > 0 })
        {
            CredentialId = passkey.CredentialId;
        }

        // Auto-detect algorithm from the key
        try
        {
            var exportedPasskey = passkey.GetPasskeys()[0];
            using var key = exportedPasskey.PrivateKey;
            SelectedAlgorithm = exportedPasskey.KeyAlgorithm;
        }
        catch
        {
            // Keep current algorithm if detection fails
        }
    }

    private void DetectKeyAlgorithm()
    {
        try
        {
            using var key = SoftwareAuthenticator.LoadPrivateKeyFromPem(KeyFilePath!);
            SelectedAlgorithm = SoftwareAuthenticator.DetectAlgorithm(key);
        }
        catch (Exception ex)
        {
            _dialogService.ShowNotificationDialog(ex.Message);
        }
    }

    private void OnGenerateCredentialId()
    {
        CredentialId = RandomNumberGenerator.GetBytes(SoftwareAuthenticator.DefaultCredentialIdLength);
    }

    private void OnGenerateKeyPair()
    {
        _dialogService.ShowKeyGenerationDialog(_defaultAlgorithm, result =>
        {
            if (result != null)
            {
                _loadedPasskey = null;
                KeyFilePath = result.FilePath;
                SelectedAlgorithm = result.Algorithm;
            }
        });
    }
}
