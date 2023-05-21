using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows.Input;
using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.Interop;
using Prism.Commands;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    public class AttestationOptionsViewModel : BindableBase, IAttestationOptionsViewModel
    {
        private const int RandomChallengeLength = 128;
        private const int RandomUserIdLength = 32;

        public AttestationOptionsViewModel()
        {
            // Configure default values
            SelectDefaultAlgorithms();
            Timeout = ApiConstants.DefaultTimeoutMilliseconds;

            // Initialize commands
            GenerateChallengeCommand = new DelegateCommand(OnGenerateChallenge);
            GenerateUserIdCommand = new DelegateCommand(OnGenerateUserId);
        }

        public ICommand GenerateChallengeCommand { get; private set; }
        public ICommand GenerateUserIdCommand { get; private set; }

        private void OnGenerateChallenge()
        {
            Challenge = GetRandomBytes(RandomChallengeLength);
        }

        private void OnGenerateUserId()
        {
            UserId = GetRandomBytes(RandomUserIdLength);
        }

        private string _rpId;
        public string RpId
        {
            get => _rpId;
            set => SetProperty(ref _rpId, value);
        }

        private string _rpName;
        public string RpName
        {
            get => _rpName;
            set => SetProperty(ref _rpName, value);
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private string _userDisplayName;
        public string UserDisplayName
        {
            get => _userDisplayName;
            set => SetProperty(ref _userDisplayName, value);
        }

        private byte[] _userId;

        public byte[] UserId
        {
            get => _userId;
            set
            {
                bool changed = SetProperty(ref _userId, value, nameof(UserId));

                if (changed)
                {
                    RaisePropertyChanged(nameof(UserIdString));
                }
            }
        }

        public string UserIdString
        {
            get => _userId != null ? Base64UrlConverter.ToBase64UrlString(_userId) : string.Empty;
            set
            {
                byte[] binaryValue = value != null ? Base64UrlConverter.FromBase64UrlString(value) : null;
                bool changed = SetProperty(ref _userId, binaryValue, nameof(UserId));

                if (changed)
                {
                    RaisePropertyChanged(nameof(UserIdString));
                }
            }
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

        private bool _requireResidentKey;
        public bool RequireResidentKey
        {
            get => _requireResidentKey;
            set => SetProperty(ref _requireResidentKey, value);
        }

        private AuthenticatorAttachment _authenticatorAttachment;
        public AuthenticatorAttachment AuthenticatorAttachment
        {
            get => _authenticatorAttachment;
            set => SetProperty(ref _authenticatorAttachment, value);
        }

        public IList<KeyValuePair<AuthenticatorAttachment?, string>> AuthenticatorAttachments
         => EnumAdapter.GetComboBoxItems<AuthenticatorAttachment>();


        private UserVerificationRequirement _userVerification;
        public UserVerificationRequirement UserVerificationRequirement
    {
            get => _userVerification;
            set => SetProperty(ref _userVerification, value);
        }

        public IList<KeyValuePair<UserVerificationRequirement?, string>> UserVerificationRequirements
        => EnumAdapter.GetComboBoxItems<UserVerificationRequirement>();


        private AttestationConveyancePreference _attestation;
        public AttestationConveyancePreference AttestationConveyancePreference
        {
            get => _attestation;
            set => SetProperty(ref _attestation, value);
        }

        public IList<KeyValuePair<AttestationConveyancePreference?, string>> AttestationTypes
        => EnumAdapter.GetComboBoxItems<AttestationConveyancePreference>();

        private int _timeout;
        public int Timeout
        {
            get => _timeout;
            set => SetProperty(ref _timeout, value);
        }

        private bool _hmacSecret;
        public bool HmacSecret
        {
            get => _hmacSecret;
            set => SetProperty(ref _hmacSecret, value);
        }

        public IList<KeyValuePair<UserVerification?, string>> CredProtectPolicies
        => EnumAdapter.GetComboBoxItems<UserVerification>();
        
        private UserVerification _credProtect;
        public UserVerification CredProtectPolicy
        {
            get => _credProtect;
            set
            {
                SetProperty(ref _credProtect, value);

                if(EnforceCredProtect && value == UserVerification.Any)
                {
                    // Uncheck Enforce CredProtect
                    this.EnforceCredProtect = false;
                }

                RaisePropertyChanged(nameof(EnforceCredProtectEnabled));
            }
        }

        private bool _enforceCredProtect;
        public bool EnforceCredProtect
        {
            get => _enforceCredProtect;
            set => SetProperty(ref _enforceCredProtect, value);
        }

        // Do not allow enforcement of credProtect if it is not enabled.
        public bool EnforceCredProtectEnabled => CredProtectPolicy != UserVerification.Any;

        private bool _algorithmRS512Enabled;
        public bool AlgorithmRS512Enabled
        {
            get => _algorithmRS512Enabled;
            set => SetProperty(ref _algorithmRS512Enabled, value);
        }

        private bool _algorithmRS384Enabled;
        public bool AlgorithmRS384Enabled
        {
            get => _algorithmRS384Enabled;
            set => SetProperty(ref _algorithmRS384Enabled, value);
        }

        private bool _algorithmRS256Enabled;
        public bool AlgorithmRS256Enabled
        {
            get => _algorithmRS256Enabled;
            set => SetProperty(ref _algorithmRS256Enabled, value);
        }

        private bool _algorithmPS512Enabled;
        public bool AlgorithmPS512Enabled
        {
            get => _algorithmPS512Enabled;
            set => SetProperty(ref _algorithmPS512Enabled, value);
        }

        private bool _algorithmPS384Enabled;
        public bool AlgorithmPS384Enabled
        {
            get => _algorithmPS384Enabled;
            set => SetProperty(ref _algorithmPS384Enabled, value);
        }

        private bool _algorithmPS256Enabled;
        public bool AlgorithmPS256Enabled
        {
            get => _algorithmPS256Enabled;
            set => SetProperty(ref _algorithmPS256Enabled, value);
        }

        private bool _algorithmES512Enabled;
        public bool AlgorithmES512Enabled
        {
            get => _algorithmES512Enabled;
            set => SetProperty(ref _algorithmES512Enabled, value);
        }

        private bool _algorithmES384Enabled;
        public bool AlgorithmES384Enabled
        {
            get => _algorithmES384Enabled;
            set => SetProperty(ref _algorithmES384Enabled, value);
        }

        private bool _algorithmES256Enabled;
        public bool AlgorithmES256Enabled
        {
            get => _algorithmES256Enabled;
            set => SetProperty(ref _algorithmES256Enabled, value);
        }

        public RelyingPartyInformation RelyingPartyEntity
        {
            get
            {
                return new RelyingPartyInformation()
                {
                    Id = RpId,
                    Name = RpName
                };
            }
            set
            {
                if (value != null)
                {
                    RpId = value.Id;
                    RpName = value.Name;
                }
                else
                {
                    // Load default values
                    RpId = null;
                    RpName = null;
                }
            }
        }

        public UserInformation UserEntity
        {
            get
            {
                return new UserInformation()
                {
                    Id = UserId,
                    DisplayName = UserDisplayName,
                    Name = UserName
                };
            }
            set
            {
                if (value != null)
                {
                    UserId = value.Id;
                    UserName = value.Name;
                    UserDisplayName = value.DisplayName;
                }
                else
                {
                    // Load default values
                    UserId = null;
                    UserName = null;
                    UserDisplayName = null;
                }
            }
        }

        public Algorithm[] PublicKeyCredentialParameters
        {
            get
            {
                // Convert checkboxes to PubKeyCredParam

                var result = new List<Algorithm>();

                if (AlgorithmES256Enabled)
                    result.Add(Algorithm.ES256);

                if (AlgorithmES384Enabled)
                    result.Add(Algorithm.ES384);

                if (AlgorithmES512Enabled)
                    result.Add(Algorithm.ES512);

                if (AlgorithmRS256Enabled)
                    result.Add(Algorithm.RS256);

                if (AlgorithmRS384Enabled)
                    result.Add(Algorithm.RS384);

                if (AlgorithmRS512Enabled)
                    result.Add(Algorithm.RS512);

                if (AlgorithmPS256Enabled)
                    result.Add(Algorithm.PS256);

                if (AlgorithmPS384Enabled)
                    result.Add(Algorithm.PS384);

                if (AlgorithmPS512Enabled)
                    result.Add(Algorithm.PS512);

                return result.ToArray();
            }
            set
            {
                // Convert PubKeyCredParam to checkboxes
                if (value == null)
                {
                    SelectDefaultAlgorithms();
                    return;
                }

                ClearSelectedAlgorithms();

                foreach (var algorithm in value)
                {
                    switch (algorithm)
                    {
                        case Algorithm.ES256:
                            AlgorithmES256Enabled = true;
                            break;
                        case Algorithm.ES384:
                            AlgorithmES384Enabled = true;
                            break;
                        case Algorithm.ES512:
                            AlgorithmES512Enabled = true;
                            break;
                        case Algorithm.RS256:
                            AlgorithmRS256Enabled = true;
                            break;
                        case Algorithm.RS384:
                            AlgorithmRS384Enabled = true;
                            break;
                        case Algorithm.RS512:
                            AlgorithmRS512Enabled = true;
                            break;
                        case Algorithm.PS256:
                            AlgorithmPS256Enabled = true;
                            break;
                        case Algorithm.PS384:
                            AlgorithmPS384Enabled = true;
                            break;
                        case Algorithm.PS512:
                            AlgorithmPS512Enabled = true;
                            break;
                    }
                }
            }
        }
        public AuthenticationExtensionsClientInputs ClientExtensions
        {
            get
            {
                if (CredProtectPolicy == UserVerification.Any && HmacSecret == false)
                {
                    // No extensions are set
                    return null;
                }

                return new AuthenticationExtensionsClientInputs()
                {
                    CredProtect = this.CredProtectPolicy,
                    EnforceCredProtect = this.EnforceCredProtect ? true : (bool?)null,
                    HmacSecret = this.HmacSecret ? true : (bool?)null
                };
            }
            set
            {
                if (value != null)
                {
                    HmacSecret = value.HmacSecret == true;
                    CredProtectPolicy = value.CredProtect ?? UserVerification.Any;
                    EnforceCredProtect = value.EnforceCredProtect == true;
                }
                else
                {
                    // Load default values
                    CredProtectPolicy = UserVerification.Any;
                    HmacSecret = false;
                }
            }
        }

        private void ClearSelectedAlgorithms()
        {
            AlgorithmES256Enabled = false;
            AlgorithmES384Enabled = false;
            AlgorithmES512Enabled = false;
            AlgorithmPS256Enabled = false;
            AlgorithmPS384Enabled = false;
            AlgorithmPS512Enabled = false;
            AlgorithmRS256Enabled = false;
            AlgorithmRS384Enabled = false;
            AlgorithmRS512Enabled = false;
        }

        private void SelectDefaultAlgorithms()
        {
            ClearSelectedAlgorithms();
            AlgorithmRS256Enabled = true;
            AlgorithmES256Enabled = true;
        }

        private static byte[] GetRandomBytes(int count)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[count];
                rng.GetBytes(randomBytes);
                return randomBytes;
            }
        }
    }
}
