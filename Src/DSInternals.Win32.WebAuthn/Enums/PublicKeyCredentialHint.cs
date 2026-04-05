using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Public key credential hints as defined in the WebAuthn specification.
/// </summary>
/// <remarks>
/// See https://w3c.github.io/webauthn/#enum-hints for more information.
/// </remarks>
[JsonConverter(typeof(JsonCustomEnumConverter<PublicKeyCredentialHint>))]
public enum PublicKeyCredentialHint
{
    /// <summary>
    /// No hint specified.
    /// </summary>
    [EnumMember(Value = null)]
    None = 0,

    /// <summary>
    /// Indicates that the Relying Party believes that users will satisfy this request with a physical security key.
    /// </summary>
    [EnumMember(Value = ApiConstants.CredentialHintSecurityKey)]
    SecurityKey,

    /// <summary>
    /// Indicates that the Relying Party believes that users will satisfy this request with a platform authenticator built into the client device.
    /// </summary>
    [EnumMember(Value = ApiConstants.CredentialHintClientDevice)]
    ClientDevice,

    /// <summary>
    /// Indicates that the Relying Party believes that users will satisfy this request with a general-purpose authenticator such as smartphone (hybrid transport).
    /// </summary>
    [EnumMember(Value = ApiConstants.CredentialHintHybrid)]
    Hybrid
}
