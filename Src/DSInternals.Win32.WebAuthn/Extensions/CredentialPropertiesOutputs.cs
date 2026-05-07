using System.Text.Json;
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

    /// <summary>
    /// Deserializes a JSON string into credential properties extension outputs.
    /// </summary>
    /// <param name="json">JSON representation of credential properties extension outputs.</param>
    /// <returns>Credential properties extension outputs if deserialization is successful; otherwise, null.</returns>
    public static CredentialPropertiesOutputs? FromJson(string json)
    {
        return WebAuthnJsonModel.FromJson(json, WebAuthnJsonContext.Default.CredentialPropertiesOutputs);
    }

    /// <summary>
    /// Serializes the credential properties extension outputs to JSON.
    /// </summary>
    /// <returns>JSON representation of these credential properties extension outputs.</returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.CredentialPropertiesOutputs);
    }
}
