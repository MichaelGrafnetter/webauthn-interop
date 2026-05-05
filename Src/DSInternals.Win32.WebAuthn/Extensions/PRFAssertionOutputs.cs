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
}
