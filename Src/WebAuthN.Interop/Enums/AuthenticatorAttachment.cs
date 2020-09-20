
namespace WebAuthN.Interop
{
    internal enum AuthenticatorAttachment : int
    {
        /// <remarks>Corresponds to WEBAUTHN_AUTHENTICATOR_ATTACHMENT_ANY.</remarks>
        Any = 0,

        /// <remarks>Corresponds to WEBAUTHN_AUTHENTICATOR_ATTACHMENT_PLATFORM.</remarks>
        Platform = 1,

        /// <remarks>Corresponds to WEBAUTHN_AUTHENTICATOR_ATTACHMENT_CROSS_PLATFORM.</remarks>
        CrossPlatform = 2,

        /// <remarks>Corresponds to WEBAUTHN_AUTHENTICATOR_ATTACHMENT_CROSS_PLATFORM_U2F_V2.</remarks>
        CrossPlatformU2F = 3
    }
}
