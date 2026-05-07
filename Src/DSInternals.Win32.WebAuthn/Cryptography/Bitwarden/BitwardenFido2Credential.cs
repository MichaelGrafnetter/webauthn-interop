#if NET5_0_OR_GREATER

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn.Cryptography;

/// <summary>
/// Represents a FIDO2 credential entry embedded in a Bitwarden login item.
/// </summary>
public sealed class BitwardenFido2Credential
{
    /// <summary>
    /// The Bitwarden credential ID value.
    /// </summary>
    [JsonPropertyName("credentialId")]
    [JsonRequired]
    public required string CredentialId { get; set; }

    /// <summary>
    /// The COSE key type recorded by Bitwarden.
    /// </summary>
    [JsonPropertyName("keyType")]
    public string? KeyType { get; set; }

    /// <summary>
    /// The COSE algorithm recorded by Bitwarden.
    /// </summary>
    [JsonPropertyName("keyAlgorithm")]
    public string? KeyAlgorithm { get; set; }

    /// <summary>
    /// The key curve recorded by Bitwarden, when applicable.
    /// </summary>
    [JsonPropertyName("keyCurve")]
    public string? KeyCurve { get; set; }

    /// <summary>
    /// The private key value decoded from Base64Url PKCS#8 DER.
    /// </summary>
    [JsonPropertyName("keyValue")]
    [JsonConverter(typeof(Base64UrlConverter))]
    [JsonRequired]
    public required byte[] KeyValue { get; set; }

    /// <summary>
    /// The relying party ID.
    /// </summary>
    [JsonPropertyName("rpId")]
    public string? RelyingPartyId { get; set; }

    /// <summary>
    /// The WebAuthn user handle decoded from Base64Url.
    /// </summary>
    [JsonPropertyName("userHandle")]
    [JsonConverter(typeof(Base64UrlConverter))]
    public byte[]? UserHandle { get; set; }

    /// <summary>
    /// The username associated with the credential.
    /// </summary>
    [JsonPropertyName("userName")]
    public string? UserName { get; set; }

    /// <summary>
    /// The signature counter stored by Bitwarden.
    /// </summary>
    [JsonPropertyName("counter")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString)]
    public uint Counter { get; set; }

    /// <summary>
    /// The relying party display name.
    /// </summary>
    [JsonPropertyName("rpName")]
    public string? RelyingPartyName { get; set; }

    /// <summary>
    /// The user display name associated with the credential.
    /// </summary>
    [JsonPropertyName("userDisplayName")]
    public string? UserDisplayName { get; set; }

    /// <summary>
    /// Indicates whether Bitwarden marked the credential as discoverable.
    /// </summary>
    [JsonPropertyName("discoverable")]
    [JsonConverter(typeof(BooleanConverter))]
    public bool Discoverable { get; set; }

    /// <summary>
    /// Loads the Bitwarden private key from this credential as an AsymmetricAlgorithm.
    /// The caller is responsible for disposing the returned key.
    /// </summary>
    /// <returns>The imported private key.</returns>
    public AsymmetricAlgorithm LoadPrivateKey()
    {
        if (KeyValue is null)
        {
            throw new InvalidOperationException("No private key found in Bitwarden FIDO2 credential.");
        }

        return SoftwareAuthenticator.ImportPrivateKeyFromPkcs8(KeyValue);
    }

    /// <summary>
    /// Converts this Bitwarden FIDO2 credential record into a normalized passkey model.
    /// </summary>
    /// <returns>A normalized exported passkey.</returns>
    public ExportedPasskey ToPasskey()
    {
        byte[] credentialId = ToWebAuthnCredentialId(CredentialId!);
        AsymmetricAlgorithm privateKey = LoadPrivateKey();
        Algorithm keyAlgorithm = DetectKeyAlgorithm(privateKey);

        return new()
        {
            AaGuid = ApiConstants.BitwardenAaGuid,
            Username = UserName,
            UserDisplayName = UserDisplayName,
            RelyingParty = RelyingPartyId,
            RelyingPartyName = RelyingPartyName,
            CredentialId = credentialId,
            UserHandle = UserHandle,
            PrivateKey = privateKey,
            KeyType = keyAlgorithm.KeyType,
            KeyAlgorithm = keyAlgorithm,
            KeyCurve = EllipticCurve.TryParse(KeyCurve) ?? keyAlgorithm.Curve,
            SignatureCounter = Counter,
            Discoverable = Discoverable
        };
    }

    /// <summary>
    /// Detects the COSE algorithm from Bitwarden metadata or the imported key.
    /// </summary>
    /// <param name="privateKey">The imported private key.</param>
    /// <returns>The detected COSE algorithm.</returns>
    private Algorithm DetectKeyAlgorithm(AsymmetricAlgorithm privateKey)
    {
        if (TryParseAlgorithm(KeyAlgorithm, KeyCurve, out Algorithm algorithm))
        {
            return algorithm;
        }

        return SoftwareAuthenticator.DetectAlgorithm(privateKey);
    }

    /// <summary>
    /// Parses a Bitwarden algorithm and curve pair into a COSE algorithm.
    /// </summary>
    /// <param name="keyAlgorithm">The Bitwarden key algorithm text.</param>
    /// <param name="keyCurve">The Bitwarden key curve text.</param>
    /// <param name="algorithm">The parsed COSE algorithm.</param>
    /// <returns><see langword="true"/> when parsing succeeded; otherwise, <see langword="false"/>.</returns>
    private static bool TryParseAlgorithm(string? keyAlgorithm, string? keyCurve, out Algorithm algorithm)
    {
        algorithm = default;

        if (string.IsNullOrWhiteSpace(keyAlgorithm))
        {
            return false;
        }

        string normalizedAlgorithm = keyAlgorithm.Trim();
        if (Enum.TryParse(normalizedAlgorithm.Replace("-", string.Empty, StringComparison.Ordinal), ignoreCase: true, out algorithm))
        {
            return true;
        }

        if (string.Equals(normalizedAlgorithm, "ECDSA", StringComparison.OrdinalIgnoreCase))
        {
            EllipticCurve? curve = EllipticCurve.TryParse(keyCurve);
            if (curve is null)
            {
                return false;
            }

            algorithm = curve switch
            {
                EllipticCurve.P256 => Algorithm.ES256,
                EllipticCurve.P384 => Algorithm.ES384,
                EllipticCurve.P521 => Algorithm.ES512,
                EllipticCurve.P256K => Algorithm.ES256K,
                _ => default
            };

            return !EqualityComparer<Algorithm>.Default.Equals(algorithm, default);
        }

        if (string.Equals(normalizedAlgorithm, "EdDSA", StringComparison.OrdinalIgnoreCase))
        {
            algorithm = Algorithm.EdDSA;
            return true;
        }

        if (string.Equals(normalizedAlgorithm, "RSA", StringComparison.OrdinalIgnoreCase)
            || string.Equals(normalizedAlgorithm, "RSASSA-PKCS1-v1_5", StringComparison.OrdinalIgnoreCase))
        {
            algorithm = Algorithm.RS256;
            return true;
        }

        if (string.Equals(normalizedAlgorithm, "RSASSA-PSS", StringComparison.OrdinalIgnoreCase))
        {
            algorithm = Algorithm.PS256;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Normalizes Bitwarden's credential ID format into WebAuthn credential ID bytes.
    /// </summary>
    /// <param name="bitwardenCredentialId">The credential ID from the Bitwarden export.</param>
    /// <returns>The WebAuthn credential ID bytes.</returns>
    private static byte[] ToWebAuthnCredentialId(string bitwardenCredentialId)
    {
        if (Guid.TryParse(bitwardenCredentialId, out Guid credentialId))
        {
            return credentialId.ToByteArray(bigEndian: true);
        }

        // Future-proofing: if Bitwarden ever exports an already encoded credential ID,
        // accept it only if it is valid Base64Url/Base64 data.
        return Base64UrlConverter.FromBase64UrlString(bitwardenCredentialId);
    }
}

#endif
