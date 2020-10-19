
namespace DSInternals.Win32.WebAuthn
{
    internal enum AuthenticatorAttachment : int
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_ATTACHMENT_ANY.
        /// </remarks>
        Any = ApiConstants.AuthenticatorAttachmentAny,

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_ATTACHMENT_PLATFORM.
        /// </remarks>
        Platform = ApiConstants.AuthenticatorAttachmentPlatform,

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_ATTACHMENT_CROSS_PLATFORM.
        /// </remarks>
        CrossPlatform = ApiConstants.AuthenticatorAttachmentCrossPlatform,

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_ATTACHMENT_CROSS_PLATFORM_U2F_V2.
        /// </remarks>
        CrossPlatformU2F = ApiConstants.AuthenticatorAttachmentCrossPlatformU2fV2
    }
}
