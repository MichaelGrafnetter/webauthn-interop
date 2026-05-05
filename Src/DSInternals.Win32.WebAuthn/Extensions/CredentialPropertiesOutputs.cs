using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Outputs for the WebAuthn credProps client extension.
/// </summary>
public sealed class CredentialPropertiesOutputs
{
    /// <summary>
    /// Indicates whether the created credential is a discoverable credential.
    /// </summary>
    [JsonPropertyName("rk")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ResidentKey { get; set; }
}
