using System.Collections.Generic;
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
}
