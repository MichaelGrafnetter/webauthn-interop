using System.Windows;
using Prism.Ioc;

namespace DSInternals.Win32.WebAuthn.Fido2UI
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

            // Dialogs
            containerRegistry.RegisterDialog<NotificationDialog>();

            // Services
            containerRegistry.RegisterSingleton<WebAuthnApi>();
        }
    }
}
