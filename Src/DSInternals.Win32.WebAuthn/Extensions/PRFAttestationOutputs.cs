using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// prf outputs from WebAuthn credential creation.
/// </summary>
public sealed class PRFAttestationOutputs
{
    /// <summary>
    /// Indicates whether PRF is available for the created credential.
    /// </summary>
    [JsonPropertyName("enabled")]
    [JsonRequired]
    public required bool Enabled { get; set; }

    /// <summary>
    /// PRF evaluation results.
    /// </summary>
    [JsonPropertyName("results")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PRFValues? Results { get; set; }

    /// <summary>
    /// Deserializes a JSON string into prf attestation outputs.
    /// </summary>
    /// <param name="json">JSON representation of prf attestation outputs.</param>
    /// <returns>prf attestation outputs if deserialization is successful; otherwise, null.</returns>
    public static PRFAttestationOutputs? FromJson(string json)
    {
        return WebAuthnJsonModel.FromJson(json, WebAuthnJsonContext.Default.PRFAttestationOutputs);
    }

    /// <summary>
    /// Serializes the prf attestation outputs to JSON.
    /// </summary>
    /// <returns>JSON representation of these prf attestation outputs.</returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.PRFAttestationOutputs);
    }
}
