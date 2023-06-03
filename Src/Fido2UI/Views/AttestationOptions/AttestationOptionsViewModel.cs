using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows.Input;
using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.FIDO;
using DSInternals.Win32.WebAuthn.Interop;
using Prism.Commands;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    public class AttestationOptionsViewModel : BindableBase, IAttestationOptionsViewModel
    {
        private const int RandomChallengeLength = 128;

        private const int RandomUserIdLength = 32;

        public AttestationOptionsViewModel(IAlgorithmSelectorViewModel algorithmSelectorViewModel)
        {
            // Save dependencies
            AlgorithmSelectorViewModel = algorithmSelectorViewModel;

            // Configure default values
            Timeout = ApiConstants.DefaultTimeoutMilliseconds;

            // Initialize commands
            GenerateChallengeCommand = new DelegateCommand(OnGenerateChallenge);
            GenerateUserIdCommand = new DelegateCommand(OnGenerateUserId);
        }

        public IAlgorithmSelectorViewModel AlgorithmSelectorViewModel { get; private set; }

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

        private bool _preferResidentKey;
        public bool PreferResidentKey
        {
            get => _preferResidentKey;
            set => SetProperty(ref _preferResidentKey, value);
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

        private EnterpriseAttestationType _enterpriseAttestation;
        public EnterpriseAttestationType EnterpriseAttestation
        {
            get => _enterpriseAttestation;
            set => SetProperty(ref _enterpriseAttestation, value);
        }

        public IList<KeyValuePair<EnterpriseAttestationType?, string>> EnterpriseAttestationTypes
        => EnumAdapter.GetComboBoxItems<EnterpriseAttestationType>();

        private int _timeout;
        public int Timeout
        {
            get => _timeout;
            set => SetProperty(ref _timeout, value);
        }

        public IList<KeyValuePair<UserVerification?, string>> CredProtectPolicies
        => EnumAdapter.GetComboBoxItems<UserVerification>();

        public IList<KeyValuePair<LargeBlobSupport?, string>> LargeBlobSupportPolicies
        => EnumAdapter.GetComboBoxItems<LargeBlobSupport>();

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

        public List<Algorithm> PublicKeyCredentialParameters
        {
            get => AlgorithmSelectorViewModel.SelectedAlgorithms;
            set => AlgorithmSelectorViewModel.SelectedAlgorithms = value;
        }

        public AuthenticationExtensionsClientInputs ClientExtensions
        {
            get
            {
                if (CredProtectPolicy == UserVerification.Any && HmacSecret == false && MinPinLength == false && CredentialBlob == null)
                {
                    // No extensions are set
                    return null;
                }

                return new AuthenticationExtensionsClientInputs()
                {
                    CredProtect = this.CredProtectPolicy,
                    EnforceCredProtect = this.EnforceCredProtect ? true : (bool?)null,
                    HmacCreateSecret = this.HmacSecret ? true : (bool?)null,
                    MinPinLength = this.MinPinLength ? true : (bool?)null,
                    CredentialBlob = this.CredentialBlob?.Length > 0 ? this.CredentialBlob : null
                };
            }
            set
            {
                if (value != null)
                {
                    HmacSecret = value.HmacCreateSecret == true;
                    CredProtectPolicy = value.CredProtect ?? UserVerification.Any;
                    EnforceCredProtect = value.EnforceCredProtect == true;
                    MinPinLength = value.MinPinLength == true;
                    CredentialBlob = value.CredentialBlob;
                }
                else
                {
                    // Load default values
                    CredProtectPolicy = UserVerification.Any;
                    HmacSecret = false;
                    MinPinLength = false;
                    CredentialBlob = null;
                }
            }
        }

        private UserVerification _credProtect;
        public UserVerification CredProtectPolicy
        {
            get => _credProtect;
            set
            {
                SetProperty(ref _credProtect, value);

                if (EnforceCredProtect && value == UserVerification.Any)
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

        private bool _minPinLength;
        public bool MinPinLength
        {
            get => _minPinLength;
            set => SetProperty(ref _minPinLength, value);
        }

        private bool _hmacSecret;
        public bool HmacSecret
        {
            get => _hmacSecret;
            set => SetProperty(ref _hmacSecret, value);
        }

        private bool _enablePRF;
        public bool EnablePseudoRandomFunction
        {
            get => _enablePRF;
            set => SetProperty(ref _enablePRF, value);
        }

        private LargeBlobSupport _largeBlobSupport;
        public LargeBlobSupport LargeBlobSupport
        {
            get => _largeBlobSupport;
            set => SetProperty(ref _largeBlobSupport, value);
        }

        private byte[] _credentialBlob;
        public byte[] CredentialBlob
        {
            get => _credentialBlob;
            set
            {
                bool changed = SetProperty(ref _credentialBlob, value);

                if (changed)
                {
                    RaisePropertyChanged(nameof(CredentialBlobString));
                }
            }
        }

        public string CredentialBlobString
        {
            get => _credentialBlob != null ? Base64UrlConverter.ToBase64UrlString(_credentialBlob) : string.Empty;
            set
            {
                byte[] binaryValue = value != null ? Base64UrlConverter.FromBase64UrlString(value) : null;
                bool changed = SetProperty(ref _credentialBlob, binaryValue, nameof(CredentialBlob));

                if (changed)
                {
                    RaisePropertyChanged(nameof(CredentialBlobString));
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
