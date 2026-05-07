#if NET5_0_OR_GREATER

using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.Cryptography;

/// <summary>
/// Represents the login section of a Bitwarden vault item.
/// </summary>
public sealed class BitwardenLogin
{
    /// <summary>
    /// The FIDO2 credentials stored on the login item.
    /// </summary>
    [JsonPropertyName("fido2Credentials")]
    public BitwardenFido2Credential[]? Fido2Credentials { get; set; }
}

#endif
