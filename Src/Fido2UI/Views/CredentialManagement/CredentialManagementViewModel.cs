using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    public class CredentialManagementViewModel : BindableBase, ICredentialManagementViewModel
    {
        public CredentialManagementViewModel()
        {
            // Initialize commands
            ResetFilterCommand = new DelegateCommand(OnResetFilter);
        }

        public ICommand ResetFilterCommand { get; private set; }

        private void OnResetFilter()
        {
            this.RelyingPartyId = null;
            this.IsBrowserPrivateMode = false;
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
