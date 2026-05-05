using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// largeBlob outputs from WebAuthn credential creation.
/// </summary>
public sealed class LargeBlobAttestationOutputs
{
    /// <summary>
    /// Indicates whether the created credential supports large blobs.
    /// </summary>
    [JsonPropertyName("supported")]
    [JsonRequired]
    public required bool Supported { get; set; }

    /// <summary>
    /// Deserializes a JSON string into largeBlob attestation outputs.
    /// </summary>
    /// <param name="json">JSON representation of largeBlob attestation outputs.</param>
    /// <returns>largeBlob attestation outputs if deserialization is successful; otherwise, null.</returns>
    public static LargeBlobAttestationOutputs? FromJson(string json)
    {
        return WebAuthnJsonModel.FromJson(json, WebAuthnJsonContext.Default.LargeBlobAttestationOutputs);
    }

    /// <summary>
    /// Serializes the largeBlob attestation outputs to JSON.
    /// </summary>
    /// <returns>JSON representation of these largeBlob attestation outputs.</returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.LargeBlobAttestationOutputs);
    }
}
