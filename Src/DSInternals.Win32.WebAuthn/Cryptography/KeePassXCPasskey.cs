#if NET5_0_OR_GREATER

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn.Cryptography;

/// <summary>
/// Represents a passkey exported from KeePassXC (.passkey JSON format).
/// </summary>
public sealed class KeePassXCPasskey
{
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("relyingParty")]
    public required string RelyingParty { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("userHandle")]
    [JsonConverter(typeof(Base64UrlConverter))]
    public byte[]? UserHandle { get; set; }

    [JsonPropertyName("credentialId")]
    [JsonConverter(typeof(Base64UrlConverter))]
    [JsonRequired]
    public required byte[] CredentialId { get; set; }

    [JsonPropertyName("privateKey")]
    [JsonRequired]
    public required string PrivateKey { get; set; }

    /// <summary>
    /// Loads and deserializes a KeePassXC passkey from a JSON file.
    /// </summary>
    public static KeePassXCPasskey LoadFromFile(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return LoadFromJson(json);
    }

    /// <summary>
    /// Deserializes a KeePassXC passkey from JSON.
    /// </summary>
    public static KeePassXCPasskey LoadFromJson(string json)
    {
        ArgumentNullException.ThrowIfNull(json);

        return JsonSerializer.Deserialize(json, WebAuthnJsonContext.Default.KeePassXCPasskey)
            ?? throw new JsonException("Failed to deserialize KeePassXC passkey file.");
    }

    /// <summary>
    /// Returns this KeePassXC passkey as a normalized exported passkey collection.
    /// </summary>
    public IReadOnlyList<ExportedPasskey> GetPasskeys()
    {
        AsymmetricAlgorithm privateKey = SoftwareAuthenticator.ImportPrivateKeyFromPem(PrivateKey);
        Algorithm keyAlgorithm = SoftwareAuthenticator.DetectAlgorithm(privateKey);

        return
        [
            new ExportedPasskey
            {
                AaGuid = ApiConstants.KeePassXCAaGuid,
                Username = Username,
                RelyingParty = RelyingParty,
                Url = Url,
                CredentialId = CredentialId,
                UserHandle = UserHandle,
                PrivateKey = privateKey,
                KeyType = keyAlgorithm.KeyType,
                KeyAlgorithm = keyAlgorithm,
                KeyCurve = keyAlgorithm.Curve,
                Discoverable = true
            }
        ];
    }
}

#endif
