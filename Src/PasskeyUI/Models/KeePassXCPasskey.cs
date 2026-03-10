using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn;
using DSInternals.Win32.WebAuthn.COSE;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

/// <summary>
/// Represents a passkey exported from KeePassXC (.passkey JSON format).
/// </summary>
public class KeePassXCPasskey
{
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("relyingParty")]
    public string? RelyingParty { get; set; }

    [JsonPropertyName("rpId")]
    public string? RpId { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("userHandle")]
    public string? UserHandle { get; set; }

    [JsonPropertyName("credentialId")]
    public string? CredentialId { get; set; }

    [JsonPropertyName("signCount")]
    public int? SignCount { get; set; }

    [JsonPropertyName("privateKey")]
    public string? PrivateKey { get; set; }

    [JsonPropertyName("keyValue")]
    public string? KeyValue { get; set; }

    /// <summary>
    /// Returns the effective relying party ID from either relyingParty or rpId fields.
    /// </summary>
    [JsonIgnore]
    public string? EffectiveRpId => RelyingParty ?? RpId;

    /// <summary>
    /// Returns the effective private key PEM string from either privateKey or keyValue fields.
    /// </summary>
    [JsonIgnore]
    public string? EffectivePrivateKey => PrivateKey ?? KeyValue;

    /// <summary>
    /// Returns the credential ID as a base64url string, converting from UUID format if necessary.
    /// </summary>
    public string? GetCredentialIdBase64Url()
    {
        if (string.IsNullOrWhiteSpace(CredentialId))
            return null;

        // Check if the credential ID is in UUID format (contains dashes like a GUID)
        if (Guid.TryParseExact(CredentialId, "D", out _))
        {
            // Parse as hex string (not as GUID, to preserve byte order)
            string hexString = CredentialId.Replace("-", string.Empty);
            byte[] rawBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length; i += 2)
            {
                rawBytes[i / 2] = byte.Parse(hexString.Substring(i, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }
            return Base64UrlConverter.ToBase64UrlString(rawBytes);
        }

        // Already in base64url format
        return CredentialId;
    }

    /// <summary>
    /// Returns the private key as a PEM string, wrapping with headers if necessary.
    /// </summary>
    public string? GetPrivateKeyPem()
    {
        string? key = EffectivePrivateKey;
        if (string.IsNullOrWhiteSpace(key))
            return null;

        key = key.Trim();

        // Already has PEM headers
        if (key.StartsWith("-----BEGIN", StringComparison.Ordinal))
            return key;

        // Wrap raw base64 content with PKCS#8 PEM headers
        return $"-----BEGIN PRIVATE KEY-----\n{key}\n-----END PRIVATE KEY-----";
    }

    /// <summary>
    /// Loads and deserializes a KeePassXC passkey from a JSON file.
    /// </summary>
    public static KeePassXCPasskey LoadFromFile(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize(json, PasskeyJsonContext.Default.KeePassXCPasskey)
            ?? throw new JsonException("Failed to deserialize passkey file.");
    }

    /// <summary>
    /// Loads the private key from this passkey as an AsymmetricAlgorithm.
    /// The caller is responsible for disposing the returned key.
    /// </summary>
    public AsymmetricAlgorithm LoadPrivateKey()
    {
        string? pem = GetPrivateKeyPem();
        if (pem == null)
            throw new InvalidOperationException("No private key found in passkey file.");

        return SoftwareAuthenticator.ImportPrivateKeyFromPem(pem);
    }
}

[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]
[JsonSerializable(typeof(KeePassXCPasskey))]
internal partial class PasskeyJsonContext : JsonSerializerContext
{
}
