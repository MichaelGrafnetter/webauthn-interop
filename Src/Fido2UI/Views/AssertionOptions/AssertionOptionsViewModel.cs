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
        }

        public ICommand GenerateChallengeCommand { get; private set; }

        private void OnGenerateChallenge()
        {
            Challenge = GetRandomBytes(RandomChallengeLength);
        }

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
                return !string.IsNullOrEmpty(AppId) ? new AuthenticationExtensionsClientInputs() { AppID = this.AppId } : null;
            }
            set
            {
                AppId = value?.AppID;
            }
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
