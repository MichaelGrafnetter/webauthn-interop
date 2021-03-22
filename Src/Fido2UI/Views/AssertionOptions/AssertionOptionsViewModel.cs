using System.Collections.Generic;
using System.Text;
using DSInternals.Win32.WebAuthn.FIDO;
using DSInternals.Win32.WebAuthn.Interop;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    public class AssertionOptionsViewModel : BindableBase, IAssertionOptionsViewModel
    {
        public AssertionOptionsViewModel()
        {
            // Configure default values
            Timeout = ApiConstants.DefaultTimeoutMilliseconds;
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
            set => SetProperty(ref _challenge, value);
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
    }
}
