#if NET5_0_OR_GREATER

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.Cryptography;

/// <summary>
/// Represents the top-level structure of a cleartext Bitwarden vault export.
/// </summary>
public sealed class BitwardenCleartextVaultExport
{
    /// <summary>
    /// Indicates whether the export content is encrypted.
    /// </summary>
    [JsonPropertyName("encrypted")]
    public bool Encrypted
    {
        get;
        set
        {
            if (value)
            {
                throw new JsonException("Bitwarden cleartext vault export cannot be encrypted.");
            }

            field = value;
        }
    }

    /// <summary>
    /// The vault items contained in the export.
    /// </summary>
    [JsonPropertyName("items")]
    public BitwardenVaultItem[]? Items { get; set; }

    /// <summary>
    /// Loads and parses a cleartext Bitwarden JSON vault export.
    /// </summary>
    /// <param name="filePath">Path to the Bitwarden JSON export.</param>
    /// <returns>The parsed cleartext Bitwarden export.</returns>
    public static BitwardenCleartextVaultExport LoadFromFile(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return LoadFromJson(json);
    }

    /// <summary>
    /// Parses a cleartext Bitwarden JSON vault export.
    /// </summary>
    /// <param name="json">The Bitwarden JSON export content.</param>
    /// <returns>The parsed cleartext Bitwarden export.</returns>
    public static BitwardenCleartextVaultExport LoadFromJson(string json)
    {
        ArgumentNullException.ThrowIfNull(json);

        return JsonSerializer.Deserialize(json, WebAuthnJsonContext.Default.BitwardenCleartextVaultExport)
            ?? throw new JsonException("Failed to deserialize Bitwarden vault export.");
    }

    /// <summary>
    /// Returns all passkeys contained in this Bitwarden vault export.
    /// </summary>
    /// <returns>The passkeys contained in login items.</returns>
    public IReadOnlyList<ExportedPasskey> GetPasskeys()
    {
        List<ExportedPasskey> passkeys = [];

        foreach (var item in Items ?? [])
        {
            if (item.Login?.Fido2Credentials is null)
            {
                continue;
            }

            foreach (var credential in item.Login.Fido2Credentials)
            {
                passkeys.Add(credential.ToPasskey());
            }
        }

        return passkeys;
    }
}

#endif
