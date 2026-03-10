using System.Collections.ObjectModel;
using System.Windows.Input;
using DSInternals.Win32.WebAuthn.Events;

namespace DSInternals.Win32.WebAuthn.PasskeyUI
{
    public interface IEventLogViewModel
    {
        ObservableCollection<WebAuthnOperation> Operations { get; }

        ICommand LoadEventsCommand { get; }
    }
}
