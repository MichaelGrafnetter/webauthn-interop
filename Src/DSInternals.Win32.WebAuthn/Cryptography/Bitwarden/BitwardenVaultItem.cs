#if NET5_0_OR_GREATER

using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.Cryptography;

/// <summary>
/// Represents a Bitwarden vault item that may contain login passkeys.
/// </summary>
public sealed class BitwardenVaultItem
{
    /// <summary>
    /// The display name of the vault item.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// The login payload for the vault item.
    /// </summary>
    [JsonPropertyName("login")]
    public BitwardenLogin? Login { get; set; }
}

#endif
