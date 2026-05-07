using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// WebAuthn attestation statement format identifiers registered with IANA.
/// </summary>
/// <see href="https://www.iana.org/assignments/webauthn/webauthn.xhtml#webauthn-attestation-statement-format-ids"/>
[JsonConverter(typeof(WebAuthnJsonEnumConverter<AttestationStatementFormatIdentifier>))]
public enum AttestationStatementFormatIdentifier : uint
{
    /// <summary>
    /// No attestation statement format identifier was specified, or the identifier is not recognized.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// WebAuthn-optimized packed attestation statement format.
    /// </summary>
    [JsonStringEnumMemberName(ApiConstants.AttestationTypePacked)]
    Packed = 1,

    /// <summary>
    /// Trusted Platform Module (TPM) attestation statement format.
    /// </summary>
    [JsonStringEnumMemberName(ApiConstants.AttestationTypeTpm)]
    TPM = 2,

    /// <summary>
    /// Android Key attestation statement format.
    /// </summary>
    [JsonStringEnumMemberName("android-key")]
    AndroidKey = 3,

    /// <summary>
    /// Android SafetyNet attestation statement format.
    /// </summary>
    [JsonStringEnumMemberName("android-safetynet")]
    AndroidSafetyNet = 4,

    /// <summary>
    /// FIDO U2F attestation statement format.
    /// </summary>
    [JsonStringEnumMemberName(ApiConstants.AttestationTypeU2F)]
    FIDOU2F = 5,

    /// <summary>
    /// Apple anonymous attestation statement format.
    /// </summary>
    [JsonStringEnumMemberName("apple")]
    Apple = 6,

    /// <summary>
    /// None attestation statement format.
    /// </summary>
    [JsonStringEnumMemberName(ApiConstants.AttestationTypeNone)]
    None = 7
}
