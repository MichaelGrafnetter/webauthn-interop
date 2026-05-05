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
}
