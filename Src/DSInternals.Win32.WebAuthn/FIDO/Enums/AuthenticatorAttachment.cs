
using System.Runtime.Serialization;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn.FIDO
{
    /// <summary>
    /// This enumeration’s values describe authenticators' attachment modalities.
    /// </summary>
    /// <see>https://www.w3.org/TR/webauthn-2/#enum-attachment</see>
    public enum AuthenticatorAttachment : int
    {
        /// <summary>
        /// No authenticator attachment filters are applied.
        /// </summary>
        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_ATTACHMENT_ANY.
        /// </remarks>
        [EnumMember(Value = "NULL")]
        Any = ApiConstants.AuthenticatorAttachmentAny,

        /// <summary>
        /// This value indicates platform attachment.
        /// </summary>
        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_ATTACHMENT_PLATFORM.
        /// </remarks>
        [EnumMember(Value = "platform")]
        Platform = ApiConstants.AuthenticatorAttachmentPlatform,

        /// <summary>
        /// This value indicates cross-platform attachment.
        /// </summary>
        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_ATTACHMENT_CROSS_PLATFORM.
        /// </remarks>
        [EnumMember(Value = "cross-platform")]
        CrossPlatform = ApiConstants.AuthenticatorAttachmentCrossPlatform,

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_ATTACHMENT_CROSS_PLATFORM_U2F_V2.
        /// </remarks>
        CrossPlatformU2F = ApiConstants.AuthenticatorAttachmentCrossPlatformU2fV2
    }
}
