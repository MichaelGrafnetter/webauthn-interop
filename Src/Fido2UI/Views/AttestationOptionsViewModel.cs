using System;
using System.Collections.Generic;
using System.Text;
using Fido2NetLib;
using Fido2NetLib.Objects;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    public class AttestationOptionsViewModel : BindableBase
    {
        private const UserVerificationRequirement DefaultUserVerification = UserVerificationRequirement.Preferred;
        public AttestationOptionsViewModel()
        {
            // Configure default values
            SelectDefaultAlgorithms();
            SelectedUserVerificationRequirement = DefaultUserVerification;
            Timeout = 60000;
        }

        public CredentialCreateOptions Options
        {
            get
            {
                return new CredentialCreateOptions()
                {
                    // TODO: Test null challenge?
                    Challenge = string.IsNullOrEmpty(Challenge) ? null : Encoding.ASCII.GetBytes(Challenge),
                    Timeout = Timeout,
                    Rp = RelyingParty,
                    Attestation = SelectedAttestation,
                    AuthenticatorSelection = AuthenticatorSelection,
                    Extensions = Extensions,
                    PubKeyCredParams = PublicKeyCredentialParameters,
                    User = User
                };
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Options));
                }

                User = value.User;
                RelyingParty = value.Rp;
                Challenge = value.Challenge != null ? Encoding.ASCII.GetString(value.Challenge) : null;
                Timeout = checked((uint)value.Timeout);
                AuthenticatorSelection = value.AuthenticatorSelection;
                SelectedAttestation = value.Attestation;
                Extensions = value.Extensions;
                PublicKeyCredentialParameters = value.PubKeyCredParams;

                // TODO: Implement AllowCredentials in UI
            }
        }

        private bool _requireResidentKey;
        public bool RequireResidentKey
        {
            get => _requireResidentKey;
            set => SetProperty(ref _requireResidentKey, value);
        }

        private AuthenticatorAttachment? _authenticatorAttachment;
        public AuthenticatorAttachment? SelectedAuthenticatorAttachment
        {
            get => _authenticatorAttachment;
            set => SetProperty(ref _authenticatorAttachment, value);
        }

        public IList<KeyValuePair<AuthenticatorAttachment?, string>> AuthenticatorAttachments
         => EnumAdapter.GetComboBoxItems<AuthenticatorAttachment>("Any type");

        private UserVerificationRequirement _userVerification;
        public UserVerificationRequirement SelectedUserVerificationRequirement
        {
            get => _userVerification;
            set => SetProperty(ref _userVerification, value);
        }

        public IList<KeyValuePair<UserVerificationRequirement?, string>> UserVerificationRequirements
        => EnumAdapter.GetComboBoxItems<UserVerificationRequirement>();

        private AttestationConveyancePreference _attestation;
        public AttestationConveyancePreference SelectedAttestation
        {
            get => _attestation;
            set => SetProperty(ref _attestation, value);
        }

        public IList<KeyValuePair<AttestationConveyancePreference?, string>> AttestationTypes
        => EnumAdapter.GetComboBoxItems<AttestationConveyancePreference>();

        private uint _timeout;
        public uint Timeout
        {
            get => _timeout;
            set => SetProperty(ref _timeout, value);
        }

        private string _challenge;
        public string Challenge
        {
            get => _challenge;
            set => SetProperty(ref _challenge, value);
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

        private string _userId;
        public string UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
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
        public UserVerification SelectedCredProtectPolicy
        {
            get => _credProtect;
            set
            {
                SetProperty(ref _credProtect, value);

                if(EnforceCredProtect && value == UserVerification.Any)
                {
                    // Uncheck Enforce CredProtect
                    EnforceCredProtect = false;
                }

                // TODO: Do using propertyChanged monitoring
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
        public bool EnforceCredProtectEnabled => SelectedCredProtectPolicy != UserVerification.Any;

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

        private List<PubKeyCredParam> PublicKeyCredentialParameters
        {
            get
            {
                // Convert checkboxes to PubKeyCredParam

                var result = new List<PubKeyCredParam>();

                if (AlgorithmES256Enabled)
                    result.Add(new PubKeyCredParam() { Alg = -7 });

                if (AlgorithmES384Enabled)
                    result.Add(new PubKeyCredParam() { Alg = -35 });

                if (AlgorithmES512Enabled)
                    result.Add(new PubKeyCredParam() { Alg = -36 });

                if (AlgorithmRS256Enabled)
                    result.Add(new PubKeyCredParam() { Alg = -257 });

                if (AlgorithmES384Enabled)
                    result.Add(new PubKeyCredParam() { Alg = -258 });

                if (AlgorithmES512Enabled)
                    result.Add(new PubKeyCredParam() { Alg = -259 });

                if (AlgorithmPS256Enabled)
                    result.Add(new PubKeyCredParam() { Alg = -37 });

                if (AlgorithmPS384Enabled)
                    result.Add(new PubKeyCredParam() { Alg = -38 });

                if (AlgorithmPS512Enabled)
                    result.Add(new PubKeyCredParam() { Alg = -39 });

                return result;
            }
            set
            {
                // Convert PubKeyCredParam to checkboxes
                if(value == null)
                {
                    SelectDefaultAlgorithms();
                    return;
                }

                ClearSelectedAlgorithms();

                foreach (var credParam in value)
                {
                    // TODO: Change Alg to enum
                    switch (credParam.Alg)
                    {
                        case -7:
                            AlgorithmES256Enabled = true;
                            break;
                        case -35:
                            AlgorithmES384Enabled = true;
                            break;
                        case -36:
                            AlgorithmES512Enabled = true;
                            break;
                        case -257:
                            AlgorithmRS256Enabled = true;
                            break;
                        case -258:
                            AlgorithmRS384Enabled = true;
                            break;
                        case -289:
                            AlgorithmRS512Enabled = true;
                            break;
                        case -37:
                            AlgorithmPS256Enabled = true;
                            break;
                        case -38:
                            AlgorithmPS384Enabled = true;
                            break;
                        case -39:
                            AlgorithmPS512Enabled = true;
                            break;
                    }
                }
            }
        }

        private AuthenticationExtensionsClientInputs Extensions
        {
            get
            {
                if(SelectedCredProtectPolicy == UserVerification.Any && HmacSecret == false)
                {
                    // No extensions are set
                    return null;
                }

                return new WinExtensionsIn()
                {
                    CredProtect = SelectedCredProtectPolicy,
                    EnforceCredProtect = EnforceCredProtect ? true : (bool?)null,
                    HmacSecret = HmacSecret ? true : (bool?)null
                };
            }
            set
            {
                if (value is WinExtensionsIn extensions)
                {
                    HmacSecret = extensions.HmacSecret == true;
                    SelectedCredProtectPolicy = extensions.CredProtect ?? UserVerification.Any;
                    EnforceCredProtect = extensions.EnforceCredProtect == true;
                }
                else
                {
                    // Load default values
                    SelectedCredProtectPolicy = UserVerification.Any;
                    HmacSecret = false;
                }
            }
        }

        private Fido2User User
        {
            get
            {
                return new Fido2User()
                {
                    DisplayName = UserDisplayName,
                    Name = UserName,
                    // TODO: Validator
                    Id = Base64Url.Decode(UserId),
                };
            }
            set
            {
                if (value != null)
                {
                    UserDisplayName = value.DisplayName;
                    UserName = value.Name;
                    UserId = value.Id != null ? Base64Url.Encode(value.Id) : null;
                }
                else
                {
                    // Load default values
                    UserDisplayName = null;
                    UserName = null;
                    UserId = null;
                }
            }
        }

        private PublicKeyCredentialRpEntity RelyingParty
        {
            get
            {
                return new PublicKeyCredentialRpEntity(RpId, RpName, null);
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

        private AuthenticatorSelection AuthenticatorSelection
        {
            get
            {
                return new AuthenticatorSelection()
                {
                    AuthenticatorAttachment = SelectedAuthenticatorAttachment,
                    RequireResidentKey = RequireResidentKey,
                    UserVerification = SelectedUserVerificationRequirement
                };
            }
            set
            {
                if (value != null)
                {
                    SelectedUserVerificationRequirement = value.UserVerification;
                    SelectedAuthenticatorAttachment = value.AuthenticatorAttachment;
                    RequireResidentKey = value.RequireResidentKey;
                }
                else
                {
                    // Load default values
                    SelectedUserVerificationRequirement = DefaultUserVerification;
                    SelectedAuthenticatorAttachment = null;
                    RequireResidentKey = false;
                }
            }
        }
    }
}
