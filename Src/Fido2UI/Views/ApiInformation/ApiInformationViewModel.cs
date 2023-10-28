using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    public class ApiInformationViewModel : BindableBase
    {
        public bool IsApiAvailable => WebAuthnApi.IsAvailable;

        public string ApiVersion => WebAuthnApi.ApiVersion.ToString();

        public bool IsCredProtectExtensionSupported => WebAuthnApi.IsCredProtectExtensionSupported;

        public bool IsPlatformAuthenticatorAvailable => WebAuthnApi.IsUserVerifyingPlatformAuthenticatorAvailable;

        public bool IsLargeBlobSupported => WebAuthnApi.IsLargeBlobSupported;

        public bool IsCredBlobSupported => WebAuthnApi.IsCredBlobSupported;

        public bool IsEnterpriseAttestationSupported => WebAuthnApi.IsEnterpriseAttestationSupported;

        public bool IsPsuedoRandomFunctionSupported => WebAuthnApi.IsPsuedoRandomFunctionSupported;

        public bool IsMinPinLengthSupported => WebAuthnApi.IsMinPinLengthSupported;

        public bool IsPlatformCredentialManagementSupported => WebAuthnApi.IsPlatformCredentialManagementSupported;

        public bool IsUnsignedExtensionOutputSupported => WebAuthnApi.IsUnsignedExtensionOutputSupported;
    }
}
