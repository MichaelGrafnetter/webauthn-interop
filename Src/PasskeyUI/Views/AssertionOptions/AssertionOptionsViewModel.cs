using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows.Input;
using DSInternals.Win32.WebAuthn.Interop;
using Prism.Commands;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

public class AssertionOptionsViewModel : BindableBase, IAssertionOptionsViewModel
{
    public AssertionOptionsViewModel()
    {
        // Configure default values
        Timeout = ApiConstants.DefaultTimeoutMilliseconds;

        // Initialize commands
        ResetOptionsCommand = new DelegateCommand(OnResetOptions);
        GenerateChallengeCommand = new DelegateCommand(OnGenerateChallenge);
        GenerateHmacSecretSalt1Command = new DelegateCommand(OnGenerateHmacSecretSalt1);
        GenerateHmacSecretSalt2Command = new DelegateCommand(OnGenerateHmacSecretSalt2);
    }

    public ICommand ResetOptionsCommand { get; private set; }
    public ICommand GenerateChallengeCommand { get; private set; }
    public ICommand GenerateHmacSecretSalt1Command { get; private set; }
    public ICommand GenerateHmacSecretSalt2Command { get; private set; }

    private void OnResetOptions()
    {
        RelyingPartyId = string.Empty;
        Challenge = null;
        LargeBlob = null;
        UserVerificationRequirement = UserVerificationRequirement.Preferred;
        AuthenticatorAttachment = AuthenticatorAttachment.Any;
        LargeBlobOperation = CredentialLargeBlobOperation.None;
        Timeout = ApiConstants.DefaultTimeoutMilliseconds;
        AppId = null;
        GetCredentialBlob = false;
        HmacSecretSalt1 = null;
        HmacSecretSalt2 = null;
        IsBrowserPrivateMode = false;
        CredentialHint = PublicKeyCredentialHint.None;
        RemoteWebOrigin = null;
    }

    private void OnGenerateChallenge()
    {
        Challenge = GetRandomBytes(ApiConstants.DefaultChallengeLength);
    }

    private void OnGenerateHmacSecretSalt1()
    {
        HmacSecretSalt1 = GetRandomBytes(ApiConstants.CtapOneHmacSecretLength);
    }

    private void OnGenerateHmacSecretSalt2()
    {
        HmacSecretSalt2 = GetRandomBytes(ApiConstants.CtapOneHmacSecretLength);
    }

    public uint HmacSecretSaltStringLength => 2 * ApiConstants.CtapOneHmacSecretLength; // HEX length
    public string RelyingPartyId
    {
        get;
        set
        {
            if (SetProperty(ref field, value))
                RaisePropertyChanged(nameof(IsFormValid));
        }
    }

    private byte[] _challenge;
    public byte[] Challenge
    {
        get => _challenge;
        set
        {
            if (SetProperty(ref _challenge, value, nameof(Challenge)))
            {
                RaisePropertyChanged(nameof(ChallengeString));
                RaisePropertyChanged(nameof(IsFormValid));
            }
        }
    }

    public string ChallengeString
    {
        get => _challenge != null ? Base64UrlConverter.ToBase64UrlString(_challenge) : string.Empty;
        set
        {
            byte[] binaryValue = value != null ? Base64UrlConverter.FromBase64UrlString(value) : null;
            if (SetProperty(ref _challenge, binaryValue, nameof(Challenge)))
            {
                RaisePropertyChanged(nameof(ChallengeString));
            }
        }
    }

    private byte[] _largeBlob;
    public byte[] LargeBlob
    {
        get => _largeBlob;
        set
        {
            if (SetProperty(ref _largeBlob, value, nameof(LargeBlob)))
            {
                RaisePropertyChanged(nameof(LargeBlobString));
            }
        }
    }

    public string LargeBlobString
    {
        get => _largeBlob != null ? Base64UrlConverter.ToBase64UrlString(_largeBlob) : string.Empty;
        set
        {
            byte[] binaryValue = string.IsNullOrEmpty(value) ? null : Base64UrlConverter.FromBase64UrlString(value);
            if (SetProperty(ref _largeBlob, binaryValue, nameof(LargeBlob)))
            {
                RaisePropertyChanged(nameof(LargeBlobString));
            }
        }
    }

    public UserVerificationRequirement UserVerificationRequirement
    {
        get;
        set => SetProperty(ref field, value);
    }

    public IList<KeyValuePair<UserVerificationRequirement?, string>> UserVerificationRequirements
    => EnumAdapter.GetComboBoxItems<UserVerificationRequirement>();

    public AuthenticatorAttachment AuthenticatorAttachment
    {
        get;
        set => SetProperty(ref field, value);
    }

    public IList<KeyValuePair<AuthenticatorAttachment?, string>> AuthenticatorAttachments
     => EnumAdapter.GetComboBoxItems<AuthenticatorAttachment>();

    public CredentialLargeBlobOperation LargeBlobOperation
    {
        get;
        set => SetProperty(ref field, value);
    }

    public IList<KeyValuePair<CredentialLargeBlobOperation?, string>> LargeBlobOperations
     => EnumAdapter.GetComboBoxItems<CredentialLargeBlobOperation>();

    public uint Timeout
    {
        get;
        set => SetProperty(ref field, value);
    }

    public string AppId
    {
        get;
        set => SetProperty(ref field, value);
    }

    public AuthenticationExtensionsClientInputs? ClientExtensions
    {
        get
        {
            if (string.IsNullOrEmpty(AppId) && GetCredentialBlob == false && HmacSecretSalt1 == null && HmacSecretSalt2 == null)
            {
                // No extensions are set
                return null;
            }

            var result = new AuthenticationExtensionsClientInputs()
            {
                AppID = this.AppId,
                GetCredentialBlob = this.GetCredentialBlob
            };

            if (this.HmacSecretSalt1 != null || this.HmacSecretSalt2 != null)
            {
                result.HmacGetSecret = new HMACGetSecretInput()
                {
                    Salt1 = this.HmacSecretSalt1,
                    Salt2 = this.HmacSecretSalt2
                };
            }

            return result;
        }
        set
        {
            if (value != null)
            {
                AppId = value.AppID;
                GetCredentialBlob = value.GetCredentialBlob == true;

                if (value.HmacGetSecret != null)
                {
                    HmacSecretSalt1 = value.HmacGetSecret.Salt1;
                    HmacSecretSalt2 = value.HmacGetSecret.Salt2;
                }
            }
            else
            {
                // Load default values
                AppId = null;
                GetCredentialBlob = false;
                HmacSecretSalt1 = null;
                HmacSecretSalt2 = null;
            }
        }
    }

    public bool GetCredentialBlob
    {
        get;
        set => SetProperty(ref field, value);
    }

    private byte[] _hmacSecretSalt1;
    public byte[] HmacSecretSalt1
    {
        get => _hmacSecretSalt1;
        set
        {
            if (SetProperty(ref _hmacSecretSalt1, value))
            {
                RaisePropertyChanged(nameof(HmacSecretSalt1String));
            }
        }
    }

    public string HmacSecretSalt1String
    {
        get => _hmacSecretSalt1?.ToHex(caps: true) ?? string.Empty;
        set
        {
            byte[] binaryValue = value?.HexToBinary();

            if (binaryValue != null && binaryValue.Length != HmacSecretSaltStringLength)
            {
                throw new ArgumentOutOfRangeException(nameof(HmacSecretSalt1String));
            }

            if (SetProperty(ref _hmacSecretSalt1, binaryValue, nameof(HmacSecretSalt1)))
            {
                RaisePropertyChanged(nameof(HmacSecretSalt1String));
            }
        }
    }

    private byte[]? _hmacSecretSalt2;
    public byte[]? HmacSecretSalt2
    {
        get => _hmacSecretSalt2;
        set
        {
            if (SetProperty(ref _hmacSecretSalt2, value))
            {
                RaisePropertyChanged(nameof(HmacSecretSalt2String));
            }
        }
    }

    public string? HmacSecretSalt2String
    {
        get => _hmacSecretSalt2?.ToHex(caps: true) ?? string.Empty;
        set
        {
            byte[]? binaryValue = value?.HexToBinary();

            if (binaryValue != null && binaryValue.Length != HmacSecretSaltStringLength)
            {
                throw new ArgumentOutOfRangeException(nameof(HmacSecretSalt2String));
            }

            if (SetProperty(ref _hmacSecretSalt2, binaryValue, nameof(HmacSecretSalt2)))
            {
                RaisePropertyChanged(nameof(HmacSecretSalt2String));
            }
        }
    }

    public bool IsBrowserPrivateMode
    {
        get;
        set => SetProperty(ref field, value);
    }

    public PublicKeyCredentialHint CredentialHint
    {
        get;
        set => SetProperty(ref field, value);
    }

    public IList<KeyValuePair<PublicKeyCredentialHint?, string>> CredentialHints
    => EnumAdapter.GetComboBoxItems<PublicKeyCredentialHint>();

    public string? RemoteWebOrigin
    {
        get;
        set => SetProperty(ref field, value);
    }

    public bool IsFormValid =>
        !string.IsNullOrWhiteSpace(RelyingPartyId) &&
        _challenge is { Length: > 0 };

    private static byte[] GetRandomBytes(uint count)
    {
        using var rng = RandomNumberGenerator.Create();
        byte[] randomBytes = new byte[count];
        rng.GetBytes(randomBytes);
        return randomBytes;
    }
}
