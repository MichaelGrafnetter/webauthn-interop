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
}
