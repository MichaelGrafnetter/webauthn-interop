using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Base type for WebAuthn client extension outputs.
/// </summary>
public abstract class AuthenticationExtensionsClientOutputs
{
    /// <summary>
    /// Extension outputs not represented by a strongly typed property.
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? AdditionalProperties { get; set; }

    /// <summary>
    /// Gets whether this object contains no extension outputs.
    /// </summary>
    [JsonIgnore]
    internal virtual bool IsEmpty =>
        AdditionalProperties == null || AdditionalProperties.Count == 0;
}
