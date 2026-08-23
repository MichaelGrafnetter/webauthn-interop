#if NET5_0_OR_GREATER

using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

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

    /// <summary>
    /// Indicates whether the export is protected by a user-supplied password.
    /// </summary>
    [JsonPropertyName("passwordProtected")]
    public bool PasswordProtected { get; set; }

    /// <summary>
    /// Asynchronously loads and parses a Bitwarden JSON vault export header.
    /// </summary>
    /// <param name="filePath">Path to the Bitwarden JSON export.</param>
    /// <param name="cancellationToken">A token that may be used to cancel the read operation.</param>
    /// <returns>The parsed Bitwarden export header.</returns>
    public static async Task<BitwardenVaultExportHeader> LoadFromFileAsync(string filePath, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filePath);

        await using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, FileOptions.Asynchronous | FileOptions.SequentialScan);
        return await JsonSerializer.DeserializeAsync(stream, WebAuthnJsonContext.Default.BitwardenVaultExportHeader, cancellationToken).ConfigureAwait(false)
            ?? throw new JsonException("Failed to deserialize Bitwarden vault export header.");
    }

    /// <summary>
    /// Parses a Bitwarden JSON vault export header.
    /// </summary>
    /// <param name="json">The Bitwarden JSON export content.</param>
    /// <returns>The parsed Bitwarden export header.</returns>
    public static BitwardenVaultExportHeader LoadFromJson(string json)
    {
        ArgumentNullException.ThrowIfNull(json);

        return JsonSerializer.Deserialize(json, WebAuthnJsonContext.Default.BitwardenVaultExportHeader)
            ?? throw new JsonException("Failed to deserialize Bitwarden vault export header.");
    }
}

#endif
