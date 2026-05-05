using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// prf inputs for WebAuthn credential creation.
/// </summary>
public sealed class PRFAttestationInputs
{
    /// <summary>
    /// PRF inputs to evaluate for the created credential.
    /// </summary>
    [JsonPropertyName("eval")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PRFValues? Eval { get; set; }
}
