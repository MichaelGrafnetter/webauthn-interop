using DSInternals.Win32.WebAuthn.Interop;
using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    public class ApiInformationViewModel : BindableBase
    {
        public bool IsApiAvailable => WebAuthnApi.IsAvailable;

        public string ApiVersion => WebAuthnApi.ApiVersion.ToString();

        public bool IsCredProtectExtensionSupported => WebAuthnApi.IsCredProtectExtensionSupported;

        public bool IsPlatformAuthenticatorAvailable => WebAuthnApi.IsUserVerifyingPlatformAuthenticatorAvailable;
    }
}
