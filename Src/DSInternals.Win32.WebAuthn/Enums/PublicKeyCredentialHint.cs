using System.ComponentModel;
using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Known public key credential hints as defined in the WebAuthn specification.
/// </summary>
/// <remarks>
/// This enum is a convenience surface for callers that want the currently defined hint values. The WebAuthn data
/// model and Win32 API wrappers use raw strings so future and custom DOMString values are preserved.
/// </remarks>
[JsonConverter(typeof(JsonStringEnumConverter<PublicKeyCredentialHint>))]
public enum PublicKeyCredentialHint
{
    /// <summary>
    /// No hint specified.
    /// </summary>
    [Description("None")]
    None = 0,

    /// <summary>
    /// Indicates that the Relying Party believes that users will satisfy this request with a physical security key.
    /// </summary>
    [JsonStringEnumMemberName(ApiConstants.CredentialHintSecurityKey)]
    [Description("Security Key (Roaming)")]
    SecurityKey,

    /// <summary>
    /// Indicates that the Relying Party believes that users will satisfy this request with a platform authenticator built into the client device.
    /// </summary>
    [JsonStringEnumMemberName(ApiConstants.CredentialHintClientDevice)]
    [Description("Windows Hello (Platform)")]
    ClientDevice,

    /// <summary>
    /// Indicates that the Relying Party believes that users will satisfy this request with a general-purpose authenticator such as smartphone (hybrid transport).
    /// </summary>
    [JsonStringEnumMemberName(ApiConstants.CredentialHintHybrid)]
    [Description("QR Code (Hybrid)")]
    Hybrid
}
