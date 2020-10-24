using System.Collections.Generic;
using System.Text;
using Fido2NetLib;
using Fido2NetLib.Objects;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    public class AssertionOptionsViewModel : BindableBase, IAssertionOptionsViewModel
    {
        public AssertionOptionsViewModel()
        {
            // Configure default values
            Timeout = 60000;
        }

        public AssertionOptions Options
        {
            get
            {
                return new AssertionOptions()
                {
                    Challenge = string.IsNullOrEmpty(Challenge) ? null : Encoding.ASCII.GetBytes(Challenge),
                    RpId = RpId,
                    Timeout = Timeout,
                    UserVerification = SelectedUserVerificationRequirement
                };
            }
            set
            {
                RpId = value.RpId;
                Challenge = Encoding.ASCII.GetString(value.Challenge);
                SelectedUserVerificationRequirement = value.UserVerification;
                Timeout = value.Timeout;
                // TODO: Implement AllowCredentials in UI
            }
        }

        private string _challenge;
        public string Challenge
        {
            get => _challenge;
            set => SetProperty(ref _challenge, value);
        }

        private UserVerificationRequirement? _userVerification;
        public UserVerificationRequirement? SelectedUserVerificationRequirement
        {
            get => _userVerification;
            set => SetProperty(ref _userVerification, value);
        }

        public IList<KeyValuePair<UserVerificationRequirement?, string>> UserVerificationRequirements
        => EnumAdapter.GetComboBoxItems<UserVerificationRequirement>("Not specified");
 
        private AuthenticatorAttachment? _authenticatorAttachment;
        public AuthenticatorAttachment? SelectedAuthenticatorAttachment
        {
            get => _authenticatorAttachment;
            set => SetProperty(ref _authenticatorAttachment, value);
        }

        public IList<KeyValuePair<AuthenticatorAttachment?, string>> AuthenticatorAttachments
         => EnumAdapter.GetComboBoxItems<AuthenticatorAttachment>("Any type");


        private uint _timeout;
        public uint Timeout
        {
            get => _timeout;
            set => SetProperty(ref _timeout, value);
        }

        private string _rpId;
        public string RpId
        {
            get => _rpId;
            set => SetProperty(ref _rpId, value);
        }
    }
}
