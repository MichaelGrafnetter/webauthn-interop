using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Base type for WebAuthn client extension outputs.
/// </summary>
public abstract class AuthenticationExtensionsClientOutputs
{
    [JsonIgnore]
    internal abstract bool IsEmpty { get; }
}
