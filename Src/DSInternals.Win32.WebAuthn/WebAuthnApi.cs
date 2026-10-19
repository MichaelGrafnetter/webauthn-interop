using System;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Windows WebAuthn API
    /// </summary>
    /// <remarks>
    /// Requires Windows 10 1903+ to work.
    /// </remarks>
    public partial class WebAuthnApi
    {
        private Guid? _cancellationId;

        /// <summary>
        /// Gets the API version information.
        /// </summary>
        /// <remarks>
        /// Indicates the presence of APIs and features.
        /// </remarks>
        public static ApiVersion? ApiVersion
        {
            get
            {
                if (field.HasValue)
                {
                    // Cached value
                    return field;
                }
                else
                {
                    try
                    {
                        return field = NativeMethods.GetApiVersionNumber();
                    }
                    catch (TypeLoadException)
                    {
                        // The WebAuthNGetApiVersionNumber() function was added in Windows 10 1903.
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Indicates the availability of the WebAuthn API.
        /// </summary>
        public static bool IsAvailable => ApiVersion >= WebAuthn.ApiVersion.Version1;

        /// <summary>
        /// Indicates the availability of the Credential Protection extension.
        /// </summary>
        /// <remarks>
        /// Support for the credProtect extension was added in V2 API.
        /// </remarks>
        public static bool IsCredProtectExtensionSupported => ApiVersion >= WebAuthn.ApiVersion.Version2;

        /// <summary>
        /// Indicates the availability of enterprise attestation.
        /// </summary>
        /// <remarks>
        /// Support for the enterprise attestation was added in V3 API.
        /// </remarks>
        public static bool IsEnterpriseAttestationSupported => ApiVersion >= WebAuthn.ApiVersion.Version3;

        /// <summary>
        /// Indicates the availability of the Credential Blob extension.
        /// </summary>
        /// <remarks>
        /// Support for the credBlob extension was added in V3 API.
        /// </remarks>
        public static bool IsCredBlobSupported => ApiVersion >= WebAuthn.ApiVersion.Version3;

        /// <summary>
        /// Indicates the availability of the large blobs.
        /// </summary>
        /// <remarks>
        /// Support for the large blobs was added in V5 API.
        /// </remarks>
        public static bool IsLargeBlobSupported => ApiVersion >= WebAuthn.ApiVersion.Version5;

        /// <summary>
        /// Indicates the API can differentiate between browser modes.
        /// </summary>
        /// <remarks>
        /// Support for the browser mode indicator was added in V5 API.
        /// </remarks>
        public static bool IsPrivateBrowserModeIndicatorSupported => ApiVersion >= WebAuthn.ApiVersion.Version5;

        /// <summary>
        /// Indicates the availability of the API for platform credential management.
        /// </summary>
        /// <remarks>
        /// Support for platform credential management was added in V4 API.
        /// </remarks>
        public static bool IsPlatformCredentialManagementSupported => ApiVersion >= WebAuthn.ApiVersion.Version4;

        /// <summary>
        /// Indicates the availability of the minimum PIN length extension.
        /// </summary>
        /// <remarks>
        /// Support for the minPinLength extension was added in V3 API.
        /// </remarks>
        public static bool IsMinPinLengthSupported => ApiVersion >= WebAuthn.ApiVersion.Version3;

        /// <summary>
        /// Indicates the availability of the psuedo-random function (PRF) extension.
        /// </summary>
        /// <remarks>
        /// Support for the prf extension was added in V6 API.
        /// </remarks>
        public static bool IsPsuedoRandomFunctionSupported => ApiVersion >= WebAuthn.ApiVersion.Version6;

        /// <summary>
        /// Indicates the support for unsigned extension outputs.
        /// </summary>
        /// <remarks>
        /// Support for the unsigned extension outputs was added in V7 API.
        /// </remarks>
        public static bool IsUnsignedExtensionOutputSupported => ApiVersion >= WebAuthn.ApiVersion.Version7;

        /// <summary>
        /// Indicates the support for linked device data.
        /// </summary>
        /// <remarks>
        /// Support for linked device data was added in V7 API.
        /// </remarks>
        public static bool IsHybridStorageLinkedDataSupported => ApiVersion >= WebAuthn.ApiVersion.Version7;

        /// <summary>
        /// Indicates the availability of the public key credential hints extension.
        /// </summary>
        /// <remarks>
        /// Support for credential hints was added in V8 API.
        /// </remarks>
        public static bool IsPublicKeyCredentialHintSupported => ApiVersion >= WebAuthn.ApiVersion.Version8;

        /// <summary>
        /// Indicates the availability of the authenticator list API.
        /// </summary>
        /// <remarks>
        /// Support for the authenticator list API was added in V9 API.
        /// </remarks>
        public static bool IsAuthenticatorListSupported => ApiVersion >= WebAuthn.ApiVersion.Version9;

        /// <summary>
        /// Indicates the availability of remote web origin support for proxied WebAuthn requests.
        /// </summary>
        /// <remarks>
        /// Support for remote web origin was added in V9 API.
        /// </remarks>
        public static bool IsRemoteWebOriginSupported => ApiVersion >= WebAuthn.ApiVersion.Version9;

        /// <summary>Indicates whether Windows reports the public API version examined for private management.</summary>
        /// <remarks>Public API version does not guarantee private ABI compatibility.</remarks>
        public static bool IsAuthenticatorManagementSupported => ApiVersion == WebAuthn.ApiVersion.Version9;

        /// <summary>
        /// Indicates whether operation cancellation is supported by the API.
        /// </summary>
        public bool IsCancellationSupported => _cancellationId.HasValue;

        /// <summary>
        ///  Initializes a new instance of the <see cref="WebAuthnApi"/> class.
        /// </summary>
        public WebAuthnApi()
        {
            if (!IsAvailable)
            {
                throw new NotSupportedException("The WebAuthN API is not supported on this OS.");
            }

            _cancellationId = GetCancellationId();
        }

        /// <summary>
        /// Cancels the WebAuthn operation currently in progress.
        /// </summary>
        /// <remarks>
        /// When this operation is invoked by the client in an authenticator session,
        /// it has the effect of terminating any AuthenticatorMakeCredential or AuthenticatorGetAssertion operation
        /// currently in progress in that authenticator session.
        /// The authenticator stops prompting for, or accepting, any user input related to authorizing the canceled operation. The client ignores any further responses from the authenticator for the canceled operation.
        /// </remarks>
        public void CancelCurrentOperation()
        {
            if (_cancellationId.HasValue)
            {
                var result = NativeMethods.CancelCurrentOperation(_cancellationId.Value);
                ApiHelper.Validate(result);
            }
        }

        /// <summary>
        /// Obtains a new cancellation identifier that can later be passed to <see cref="CancelCurrentOperation"/> to abort an in-progress WebAuthn operation.
        /// </summary>
        /// <returns>The cancellation identifier, or <see langword="null"/> if the running OS does not expose the asynchronous cancellation API.</returns>
        private static Guid? GetCancellationId()
        {
            try
            {
                var result = NativeMethods.GetCancellationId(out var cancellationId);
                ApiHelper.Validate(result);
                return cancellationId;
            }
            catch (TypeLoadException)
            {
                // Async support is not present in earlier versions of Windows 10.
                return null;
            }
        }
    }
}
