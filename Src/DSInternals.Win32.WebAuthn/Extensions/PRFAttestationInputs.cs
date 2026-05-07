using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// prf inputs for WebAuthn credential creation.
/// </summary>
public sealed class PRFAttestationInputs
{
    /// <summary>
    /// PRF inputs to evaluate for the created credential.
    /// </summary>
    [JsonPropertyName("eval")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PRFValues? Eval { get; set; }

    /// <summary>
    /// Deserializes a JSON string into prf attestation inputs.
    /// </summary>
    /// <param name="json">JSON representation of prf attestation inputs.</param>
    /// <returns>prf attestation inputs if deserialization is successful; otherwise, null.</returns>
    public static PRFAttestationInputs? FromJson(string json)
    {
        return WebAuthnJsonModel.FromJson(json, WebAuthnJsonContext.Default.PRFAttestationInputs);
    }

    /// <summary>
    /// Serializes the prf attestation inputs to JSON.
    /// </summary>
    /// <returns>JSON representation of these prf attestation inputs.</returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.PRFAttestationInputs);
    }
}
