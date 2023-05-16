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
            containerRegistry.RegisterSingleton<ApiInformationViewModel, ApiInformationViewModel>();
            containerRegistry.RegisterSingleton<IAssertionOptionsViewModel, AssertionOptionsViewModel>();
            containerRegistry.RegisterSingleton<AssertionOptionsViewModel, AssertionOptionsViewModel>();
            containerRegistry.RegisterSingleton<IAttestationOptionsViewModel, AttestationOptionsViewModel>();
            containerRegistry.RegisterSingleton<AttestationOptionsViewModel, AttestationOptionsViewModel>();
            containerRegistry.RegisterSingleton<ICredentialManagementViewModel, CredentialManagementViewModel>();
            containerRegistry.RegisterSingleton<CredentialManagementViewModel, CredentialManagementViewModel>();
            containerRegistry.RegisterSingleton<WebAuthnApi, WebAuthnApi>();

            containerRegistry.RegisterDialog<NotificationDialog, NotificationDialogViewModel>();
        }
    }
}
