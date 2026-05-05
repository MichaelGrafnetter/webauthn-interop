#if NET5_0_OR_GREATER

using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.Cryptography;

/// <summary>
/// Represents the minimal Bitwarden export header used to choose a parser.
/// </summary>
public sealed class BitwardenVaultExportHeader
{
    /// <summary>
    /// Indicates whether the export content is encrypted.
    /// </summary>
    [JsonPropertyName("encrypted")]
    public bool Encrypted { get; set; }
}

#endif
