using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows.Input;
using DSInternals.Win32.WebAuthn.Interop;
using Prism.Commands;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    public class AssertionOptionsViewModel : BindableBase, IAssertionOptionsViewModel
    {
        private const int RandomChallengeLength = 128;

        public AssertionOptionsViewModel()
        {
            // Configure default values
            Timeout = ApiConstants.DefaultTimeoutMilliseconds;

            // Initialize commands
            GenerateChallengeCommand = new DelegateCommand(OnGenerateChallenge);
            GenerateHmacSecretSalt1Command = new DelegateCommand(OnGenerateHmacSecretSalt1);
            GenerateHmacSecretSalt2Command = new DelegateCommand(OnGenerateHmacSecretSalt2);
        }

        public ICommand GenerateChallengeCommand { get; private set; }
        public ICommand GenerateHmacSecretSalt1Command { get; private set; }
        public ICommand GenerateHmacSecretSalt2Command { get; private set; }

        private void OnGenerateChallenge()
        {
            Challenge = GetRandomBytes(RandomChallengeLength);
        }

        private void OnGenerateHmacSecretSalt1()
        {
            HmacSecretSalt1 = GetRandomBytes(ApiConstants.CtapOneHmacSecretLength);
        }

        private void OnGenerateHmacSecretSalt2()
        {
            HmacSecretSalt2 = GetRandomBytes(ApiConstants.CtapOneHmacSecretLength);
        }

        public int HmacSecretSaltStringLength => 2 * ApiConstants.CtapOneHmacSecretLength; // HEX length

        private string _relyingPartyId;
        public string RelyingPartyId
        {
            get => _relyingPartyId;
            set => SetProperty(ref _relyingPartyId, value);
        }

        private byte[] _challenge;
        public byte[] Challenge
        {
            get => _challenge;
            set
            {
                bool changed = SetProperty(ref _challenge, value, nameof(Challenge));

                if (changed)
                {
                    RaisePropertyChanged(nameof(ChallengeString));
                }
            }
        }

        public string ChallengeString
        {
            get => _challenge != null ? Base64UrlConverter.ToBase64UrlString(_challenge) : string.Empty;
            set
            {
                byte[] binaryValue = value != null ? Base64UrlConverter.FromBase64UrlString(value) : null;
                bool changed = SetProperty(ref _challenge, binaryValue, nameof(Challenge));

                if (changed)
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
                bool changed = SetProperty(ref _largeBlob, value, nameof(LargeBlob));

                if (changed)
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
                bool changed = SetProperty(ref _largeBlob, binaryValue, nameof(LargeBlob));

                if (changed)
                {
                    RaisePropertyChanged(nameof(LargeBlobString));
                }
            }
        }

        private UserVerificationRequirement _userVerification;
        public UserVerificationRequirement UserVerificationRequirement
        {
            get => _userVerification;
            set => SetProperty(ref _userVerification, value);
        }

        public IList<KeyValuePair<UserVerificationRequirement?, string>> UserVerificationRequirements
        => EnumAdapter.GetComboBoxItems<UserVerificationRequirement>();
 
        private AuthenticatorAttachment _authenticatorAttachment;
        public AuthenticatorAttachment AuthenticatorAttachment
        {
            get => _authenticatorAttachment;
            set => SetProperty(ref _authenticatorAttachment, value);
        }

        public IList<KeyValuePair<AuthenticatorAttachment?, string>> AuthenticatorAttachments
         => EnumAdapter.GetComboBoxItems<AuthenticatorAttachment>();


        private CredentialLargeBlobOperation _largeBlobOperation;
        public CredentialLargeBlobOperation LargeBlobOperation
        {
            get => _largeBlobOperation;
            set => SetProperty(ref _largeBlobOperation, value);
        }

        public IList<KeyValuePair<CredentialLargeBlobOperation?, string>> LargeBlobOperations
         => EnumAdapter.GetComboBoxItems<CredentialLargeBlobOperation>();

        private int _timeout;
        public int Timeout
        {
            get => _timeout;
            set => SetProperty(ref _timeout, value);
        }

        private string _appId;
        public string AppId
        {
            get => _appId;
            set => SetProperty(ref _appId, value);
        }

        public AuthenticationExtensionsClientInputs ClientExtensions
        {
            get
            {
                if(string.IsNullOrEmpty(AppId) && GetCredentialBlob == false && HmacSecretSalt1 == null && HmacSecretSalt2 == null)
                {
                    // No extensions are set
                    return null;
                }

                return new AuthenticationExtensionsClientInputs()
                {
                    AppID = this.AppId,
                    GetCredentialBlob = this.GetCredentialBlob,
                    // TODO: HmacSecretSalt1
                    // TODO: HmacSecretSalt2
                };
            }
            set
            {
                if (value != null)
                {
                    AppId = value.AppID;
                    GetCredentialBlob = value.GetCredentialBlob == true;
                    // TODO: HmacSecretSalt1
                    // TODO: HmacSecretSalt2
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

        private bool _getCredentialBlob;
        public bool GetCredentialBlob
        {
            get => _getCredentialBlob;
            set => SetProperty(ref _getCredentialBlob, value);
        }

        private byte[] _hmacSecretSalt1;
        public byte[] HmacSecretSalt1
        {
            get => _hmacSecretSalt1;
            set
            {
                bool changed = SetProperty(ref _hmacSecretSalt1, value);

                if (changed)
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

                bool changed = SetProperty(ref _hmacSecretSalt1, binaryValue, nameof(HmacSecretSalt1));

                if (changed)
                {
                    RaisePropertyChanged(nameof(HmacSecretSalt1String));
                }
            }
        }

        private byte[] _hmacSecretSalt2;
        public byte[] HmacSecretSalt2
        {
            get => _hmacSecretSalt2;
            set
            {
                bool changed = SetProperty(ref _hmacSecretSalt2, value);

                if (changed)
                {
                    RaisePropertyChanged(nameof(HmacSecretSalt2String));
                }
            }
        }

        public string HmacSecretSalt2String
        {
            get => _hmacSecretSalt2?.ToHex(caps: true) ?? string.Empty;
            set
            {
                
                byte[] binaryValue = value?.HexToBinary();

                if( binaryValue != null && binaryValue.Length != HmacSecretSaltStringLength )
                {
                    throw new ArgumentOutOfRangeException(nameof(HmacSecretSalt2String));
                }

                bool changed = SetProperty(ref _hmacSecretSalt2, binaryValue, nameof(HmacSecretSalt2));

                if (changed)
                {
                    RaisePropertyChanged(nameof(HmacSecretSalt2String));
                }
            }
        }

        private bool _isBrowserPrivateMode;
        public bool IsBrowserPrivateMode
        {
            get => _isBrowserPrivateMode;
            set => SetProperty(ref _isBrowserPrivateMode, value);
        }

        private static byte[] GetRandomBytes(int count)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] randomBytes = new byte[count];
                rng.GetBytes(randomBytes);
                return randomBytes;
            }
        }
    }
}
