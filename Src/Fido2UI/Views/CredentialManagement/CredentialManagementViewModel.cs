using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows.Input;
using DSInternals.Win32.WebAuthn.FIDO;
using DSInternals.Win32.WebAuthn.Interop;
using Prism.Commands;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    public class CredentialManagementViewModel : BindableBase, ICredentialManagementViewModel
    {
        private const int RandomChallengeLength = 128;

        public CredentialManagementViewModel()
        {
        }

        private string _relyingPartyId;
        public string RelyingPartyId
        {
            get => _relyingPartyId;
            set => SetProperty(ref _relyingPartyId, value);
        }

        private bool _isBrowserPrivateMode;
        public bool IsBrowserPrivateMode
        {
            get => _isBrowserPrivateMode;
            set => SetProperty(ref _isBrowserPrivateMode, value);
        }
    }
}
