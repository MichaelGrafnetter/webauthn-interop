#pragma warning disable CA1822 // Mark members as static

using Prism.Mvvm;

namespace DSInternals.Win32.WebAuthn.PasskeyUI
{

    internal sealed class ApiInformationViewModel : BindableBase
    {
        public bool IsApiAvailable => WebAuthnApi.IsAvailable;

        public string? ApiVersion => WebAuthnApi.ApiVersion?.ToString();

        public bool IsCredProtectExtensionSupported => WebAuthnApi.IsCredProtectExtensionSupported;

        public bool IsPlatformAuthenticatorAvailable => WebAuthnApi.IsUserVerifyingPlatformAuthenticatorAvailable;

        public bool IsLargeBlobSupported => WebAuthnApi.IsLargeBlobSupported;

        public bool IsCredBlobSupported => WebAuthnApi.IsCredBlobSupported;

        public bool IsEnterpriseAttestationSupported => WebAuthnApi.IsEnterpriseAttestationSupported;

        public bool IsPsuedoRandomFunctionSupported => WebAuthnApi.IsPsuedoRandomFunctionSupported;

        public bool IsMinPinLengthSupported => WebAuthnApi.IsMinPinLengthSupported;

        public bool IsPlatformCredentialManagementSupported => WebAuthnApi.IsPlatformCredentialManagementSupported;

        public bool IsUnsignedExtensionOutputSupported => WebAuthnApi.IsUnsignedExtensionOutputSupported;

        public bool IsHybridStorageLinkedDataSupported => WebAuthnApi.IsHybridStorageLinkedDataSupported;

        public bool IsPublicKeyCredentialHintSupported => WebAuthnApi.IsPublicKeyCredentialHintSupported;

        public bool IsAuthenticatorListSupported => WebAuthnApi.IsAuthenticatorListSupported;

        /// <summary>Whether Windows exposes security-key authenticator management APIs.</summary>
        public bool IsAuthenticatorManagementSupported => WebAuthnApi.IsAuthenticatorManagementSupported;
    }
}
