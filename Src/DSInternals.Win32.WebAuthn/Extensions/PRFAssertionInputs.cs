using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// prf inputs for WebAuthn assertions.
/// </summary>
public sealed class PRFAssertionInputs
{
    /// <summary>
    /// PRF inputs to evaluate for the selected credential.
    /// </summary>
    [JsonPropertyName("eval")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PRFValues? Eval { get; set; }

    /// <summary>
    /// PRF inputs keyed by base64url-encoded credential ID.
    /// </summary>
    [JsonPropertyName("evalByCredential")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, PRFValues>? EvalByCredential { get; set; }

    /// <summary>
    /// Deserializes a JSON string into prf assertion inputs.
    /// </summary>
    /// <param name="json">JSON representation of prf assertion inputs.</param>
    /// <returns>prf assertion inputs if deserialization is successful; otherwise, null.</returns>
    public static PRFAssertionInputs? FromJson(string json)
    {
        return WebAuthnJsonModel.FromJson(json, WebAuthnJsonContext.Default.PRFAssertionInputs);
    }

    /// <summary>
    /// Serializes the prf assertion inputs to JSON.
    /// </summary>
    /// <returns>JSON representation of these prf assertion inputs.</returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.PRFAssertionInputs);
    }
}
