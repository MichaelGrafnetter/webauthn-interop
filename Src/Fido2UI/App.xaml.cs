using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Prism;
using Prism.Ioc;
using Prism.Modularity;

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
            containerRegistry.RegisterSingleton<AssertionOptionsViewModel, AssertionOptionsViewModel>();
            containerRegistry.RegisterSingleton<AttestationOptionsViewModel, AttestationOptionsViewModel>();
            containerRegistry.RegisterSingleton<WebAuthnApi, WebAuthnApi>();
        }
    }
}
