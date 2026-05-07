#if NET5_0_OR_GREATER

using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.Cryptography;

/// <summary>
/// Represents the version of the FIDO Credential Exchange Format payload.
/// </summary>
public sealed class CredentialExchangeVersion
{
    /// <summary>
    /// The major version of the CXF payload.
    /// </summary>
    [JsonPropertyName("major")]
    public byte Major { get; set; }

    /// <summary>
    /// The minor version of the CXF payload.
    /// </summary>
    [JsonPropertyName("minor")]
    public byte Minor { get; set; }
}

#endif
