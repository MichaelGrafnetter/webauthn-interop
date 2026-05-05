using System.Text.Json;
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

    /// <summary>
    /// Deserializes a JSON string into PRF values.
    /// </summary>
    /// <param name="json">JSON representation of PRF values.</param>
    /// <returns>PRF values if deserialization is successful; otherwise, null.</returns>
    public static PRFValues? FromJson(string json)
    {
        return WebAuthnJsonModel.FromJson(json, WebAuthnJsonContext.Default.PRFValues);
    }

    /// <summary>
    /// Serializes the PRF values to JSON.
    /// </summary>
    /// <returns>JSON representation of these PRF values.</returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.PRFValues);
    }
}
