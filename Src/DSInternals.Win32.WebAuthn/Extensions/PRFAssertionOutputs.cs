using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// prf outputs from WebAuthn assertions.
/// </summary>
public sealed class PRFAssertionOutputs
{
    /// <summary>
    /// PRF evaluation results.
    /// </summary>
    [JsonPropertyName("results")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PRFValues? Results { get; set; }

    /// <summary>
    /// Deserializes a JSON string into prf assertion outputs.
    /// </summary>
    /// <param name="json">JSON representation of prf assertion outputs.</param>
    /// <returns>prf assertion outputs if deserialization is successful; otherwise, null.</returns>
    public static PRFAssertionOutputs? FromJson(string json)
    {
        return WebAuthnJsonModel.FromJson(json, WebAuthnJsonContext.Default.PRFAssertionOutputs);
    }

    /// <summary>
    /// Serializes the prf assertion outputs to JSON.
    /// </summary>
    /// <returns>JSON representation of these prf assertion outputs.</returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.PRFAssertionOutputs);
    }
}
