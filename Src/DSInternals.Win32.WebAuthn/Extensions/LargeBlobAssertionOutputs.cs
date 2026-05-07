using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// largeBlob outputs from WebAuthn assertions.
/// </summary>
public sealed class LargeBlobAssertionOutputs
{
    /// <summary>
    /// Large blob bytes returned during assertion.
    /// </summary>
    [JsonPropertyName("blob")]
    [JsonConverter(typeof(Base64UrlConverter))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public byte[]? Blob { get; set; }

    /// <summary>
    /// Indicates whether a large blob write operation succeeded.
    /// </summary>
    [JsonPropertyName("written")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Written { get; set; }

    /// <summary>
    /// Deserializes a JSON string into largeBlob assertion outputs.
    /// </summary>
    /// <param name="json">JSON representation of largeBlob assertion outputs.</param>
    /// <returns>largeBlob assertion outputs if deserialization is successful; otherwise, null.</returns>
    public static LargeBlobAssertionOutputs? FromJson(string json)
    {
        return WebAuthnJsonModel.FromJson(json, WebAuthnJsonContext.Default.LargeBlobAssertionOutputs);
    }

    /// <summary>
    /// Serializes the largeBlob assertion outputs to JSON.
    /// </summary>
    /// <returns>JSON representation of these largeBlob assertion outputs.</returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.LargeBlobAssertionOutputs);
    }
}
