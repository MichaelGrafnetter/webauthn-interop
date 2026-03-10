using System.Windows;
using Prism.Ioc;

namespace DSInternals.Win32.WebAuthn.PasskeyUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // ViewModels
            containerRegistry.Register<IAssertionOptionsViewModel, AssertionOptionsViewModel>();
            containerRegistry.Register<IAttestationOptionsViewModel, AttestationOptionsViewModel>();
            containerRegistry.Register<ICredentialManagementViewModel, CredentialManagementViewModel>();
            containerRegistry.Register<IAlgorithmSelectorViewModel, AlgorithmSelectorViewModel>();
            containerRegistry.Register<IAuthenticatorListViewModel, AuthenticatorListViewModel>();
            containerRegistry.Register<IEventLogViewModel, EventLogViewModel>();

            // Dialogs
            containerRegistry.RegisterDialog<NotificationDialog>();
            containerRegistry.RegisterDialog<ConfirmationDialog>();

            // Services
            containerRegistry.RegisterSingleton<WebAuthnApi>();
        }
    }
}
