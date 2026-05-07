#if NET5_0_OR_GREATER

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.Cryptography;

/// <summary>
/// Represents the top-level Header of a FIDO Credential Exchange Format (CXF) payload.
/// </summary>
/// <remarks>
/// Implements the cleartext data model defined in
/// <see href="https://fidoalliance.org/specs/cx/cxf-v1.0-ps-errata-20260309.html">FIDO Credential Exchange Format v1.0</see>.
/// </remarks>
public sealed class CredentialExchangeFile
{
    /// <summary>
    /// The CXF credential type discriminator value used for passkeys.
    /// </summary>
    public const string PasskeyCredentialType = "passkey";

    /// <summary>
    /// The version of the CXF payload.
    /// </summary>
    [JsonPropertyName("version")]
    public CredentialExchangeVersion? Version { get; set; }

    /// <summary>
    /// The relying party identifier of the exporting credential provider.
    /// </summary>
    [JsonPropertyName("exporterRpId")]
    public string? ExporterRelyingPartyId { get; set; }

    /// <summary>
    /// The display name of the exporting credential provider.
    /// </summary>
    [JsonPropertyName("exporterDisplayName")]
    public string? ExporterDisplayName { get; set; }

    /// <summary>
    /// The Unix timestamp at which the export was generated, in seconds.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public ulong Timestamp { get; set; }

    /// <summary>
    /// The list of accounts contained in the export.
    /// </summary>
    [JsonPropertyName("accounts")]
    public CredentialExchangeAccount[]? Accounts { get; set; }

    /// <summary>
    /// Loads and parses a CXF JSON document from disk.
    /// </summary>
    /// <param name="filePath">Path to the CXF JSON file.</param>
    /// <returns>The parsed CXF payload.</returns>
    public static CredentialExchangeFile LoadFromFile(string filePath)
    {
        ArgumentNullException.ThrowIfNull(filePath);

        string json = File.ReadAllText(filePath);
        return LoadFromJson(json);
    }

    /// <summary>
    /// Parses a CXF JSON document.
    /// </summary>
    /// <param name="json">The CXF JSON content.</param>
    /// <returns>The parsed CXF payload.</returns>
    public static CredentialExchangeFile LoadFromJson(string json)
    {
        ArgumentNullException.ThrowIfNull(json);

        return JsonSerializer.Deserialize(json, WebAuthnJsonContext.Default.CredentialExchangeFile)
            ?? throw new JsonException("Failed to deserialize CXF payload.");
    }

    /// <summary>
    /// Returns all passkeys contained in this CXF payload.
    /// </summary>
    /// <param name="aaGuid">
    /// Optional AAGUID to assign to the imported passkeys. Defaults to
    /// <see cref="Guid.Empty"/> as CXF does not carry an AAGUID.
    /// </param>
    /// <returns>The passkeys contained in the export.</returns>
    public IReadOnlyList<ExportedPasskey> GetPasskeys(Guid? aaGuid = null)
    {
        List<ExportedPasskey> passkeys = [];

        foreach (var account in Accounts ?? [])
        {
            foreach (var item in account.Items ?? [])
            {
                foreach (var credential in item.Credentials ?? [])
                {
                    if (credential is CredentialExchangePasskey passkey)
                    {
                        passkeys.Add(passkey.ToPasskey(aaGuid));
                    }
                }
            }
        }

        return passkeys;
    }
}

#endif
