
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.Interop;
using Windows.Win32;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// This enumeration’s values describe authenticators' attachment modalities.
/// </summary>
/// <see>https://www.w3.org/TR/webauthn-2/#enum-attachment</see>
[JsonConverter(typeof(WebAuthnJsonEnumConverter<AuthenticatorAttachment>))]
public enum AuthenticatorAttachment : uint
{
    /// <summary>
    /// No authenticator attachment filters are applied.
    /// </summary>
    /// <remarks>
    /// Corresponds to WEBAUTHN_AUTHENTICATOR_ATTACHMENT_ANY.
    /// </remarks>
    Any = PInvoke.WEBAUTHN_AUTHENTICATOR_ATTACHMENT_ANY,

    /// <summary>
    /// This value indicates platform attachment.
    /// </summary>
    /// <remarks>
    /// Corresponds to WEBAUTHN_AUTHENTICATOR_ATTACHMENT_PLATFORM.
    /// </remarks>
    [JsonStringEnumMemberName(ApiConstants.AuthenticatorAttachmentPlatform)]
    Platform = PInvoke.WEBAUTHN_AUTHENTICATOR_ATTACHMENT_PLATFORM,

    /// <summary>
    /// This value indicates cross-platform attachment.
    /// </summary>
    /// <remarks>
    /// Corresponds to WEBAUTHN_AUTHENTICATOR_ATTACHMENT_CROSS_PLATFORM.
    /// </remarks>
    [JsonStringEnumMemberName(ApiConstants.AuthenticatorAttachmentCrossPlatform)]
    CrossPlatform = PInvoke.WEBAUTHN_AUTHENTICATOR_ATTACHMENT_CROSS_PLATFORM,

    /// <remarks>
    /// Corresponds to WEBAUTHN_AUTHENTICATOR_ATTACHMENT_CROSS_PLATFORM_U2F_V2.
    /// </remarks>
    CrossPlatformU2F = PInvoke.WEBAUTHN_AUTHENTICATOR_ATTACHMENT_CROSS_PLATFORM_U2F_V2
}
