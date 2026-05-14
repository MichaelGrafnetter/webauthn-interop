using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Specifies the Relying Party's requirements regarding authenticator attributes.
/// </summary>
/// <remarks>
/// Used by Relying Parties to communicate their requirements for authenticator attributes during credential creation.
/// </remarks>
/// <see href="https://www.w3.org/TR/webauthn-3/#dictdef-authenticatorselectioncriteria"/>
public class AuthenticatorSelectionCriteria
{
    /// <summary>
    /// The preferred authenticator attachment modality.
    /// </summary>
    [JsonPropertyName("authenticatorAttachment")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public AuthenticatorAttachment AuthenticatorAttachment { get; set; }

    /// <summary>
    /// Requirement to verify the user is present during credential provisioning.
    /// </summary>
    [JsonPropertyName("userVerification")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public UserVerificationRequirement UserVerificationRequirement { get; set; }

    /// <summary>
    /// Specifies the extent to which the Relying Party desires to create a client-side discoverable credential.
    /// </summary>
    /// <remarks>
    /// This value is intentionally nullable so that it can default to Preferred when not specified.
    /// </remarks>
    [JsonPropertyName("residentKey")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ResidentKeyRequirement? ResidentKey { get; set; }

    /// <summary>
    /// This member is retained for backwards compatibility with WebAuthn Level 1.
    /// Relying Parties SHOULD set it to true if, and only if, residentKey is set to required.
    /// </summary>
    [JsonPropertyName("requireResidentKey")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool RequireResidentKey { get; set; }

    /// <summary>
    /// Serializes the authenticator selection criteria to JSON.
    /// </summary>
    /// <returns>JSON representation of these authenticator selection criteria.</returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.AuthenticatorSelectionCriteria);
    }
}
