using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// One or two WebAuthn PRF inputs or outputs.
/// </summary>
public sealed class PRFValues
{
    /// <summary>
    /// First PRF input or output value.
    /// </summary>
    [JsonPropertyName("first")]
    [JsonConverter(typeof(Base64UrlConverter))]
    [JsonRequired]
    public required byte[] First { get; set; } = [];

    /// <summary>
    /// Optional second PRF input or output value.
    /// </summary>
    [JsonPropertyName("second")]
    [JsonConverter(typeof(Base64UrlConverter))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public byte[]? Second { get; set; }
}
