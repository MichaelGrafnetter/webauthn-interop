#if NET5_0_OR_GREATER

using System;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.COSE;

namespace DSInternals.Win32.WebAuthn.Cryptography;

/// <summary>
/// Represents a CXF credential of type <c>passkey</c>.
/// </summary>
public sealed class CredentialExchangePasskey : CredentialExchangeCredential
{
    /// <summary>
    /// The WebAuthn Credential ID associated with the passkey.
    /// </summary>
    [JsonPropertyName("credentialId")]
    [JsonConverter(typeof(Base64UrlConverter))]
    public byte[]? CredentialId { get; set; }

    /// <summary>
    /// The WebAuthn Relying Party Identifier the passkey is bound to.
    /// </summary>
    [JsonPropertyName("rpId")]
    public string? RelyingPartyId { get; set; }

    /// <summary>
    /// A human-palatable identifier for the user account associated with the passkey.
    /// </summary>
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    /// <summary>
    /// A human-palatable display name for the user account associated with the passkey.
    /// </summary>
    [JsonPropertyName("userDisplayName")]
    public string? UserDisplayName { get; set; }

    /// <summary>
    /// The WebAuthn user handle bound to the passkey.
    /// </summary>
    [JsonPropertyName("userHandle")]
    [JsonConverter(typeof(Base64UrlConverter))]
    public byte[]? UserHandle { get; set; }

    /// <summary>
    /// The PKCS#8 ASN.1 DER encoded private key of the passkey.
    /// </summary>
    [JsonPropertyName("key")]
    [JsonConverter(typeof(Base64UrlConverter))]
    public byte[]? Key { get; set; }

    /// <summary>
    /// FIDO2 extension state captured for the passkey.
    /// </summary>
    [JsonPropertyName("fido2Extensions")]
    public CredentialExchangeFido2Extensions? Fido2Extensions { get; set; }

    /// <summary>
    /// Loads the passkey private key as an <see cref="AsymmetricAlgorithm"/>.
    /// The caller is responsible for disposing the returned key.
    /// </summary>
    public AsymmetricAlgorithm LoadPrivateKey()
    {
        if (Key is null || Key.Length == 0)
        {
            throw new InvalidOperationException("CXF passkey credential is missing a private key.");
        }

        return SoftwareAuthenticator.ImportPrivateKeyFromPkcs8(Key);
    }

    /// <summary>
    /// Converts this CXF passkey credential into a normalized <see cref="ExportedPasskey"/>.
    /// </summary>
    /// <param name="aaGuid">
    /// The AAGUID to associate with the imported passkey. CXF does not include an AAGUID,
    /// so importers may pick one that identifies the new platform authenticator.
    /// Defaults to <see cref="Guid.Empty"/>.
    /// </param>
    public ExportedPasskey ToPasskey(Guid? aaGuid = null)
    {
        if (CredentialId is null || CredentialId.Length == 0)
        {
            throw new InvalidOperationException("CXF passkey credential is missing the credentialId.");
        }

        AsymmetricAlgorithm privateKey = LoadPrivateKey();
        Algorithm keyAlgorithm = SoftwareAuthenticator.DetectAlgorithm(privateKey);

        return new ExportedPasskey
        {
            AaGuid = aaGuid ?? Guid.Empty,
            Username = Username,
            UserDisplayName = UserDisplayName,
            RelyingParty = RelyingPartyId,
            CredentialId = CredentialId,
            UserHandle = UserHandle,
            PrivateKey = privateKey,
            KeyType = keyAlgorithm.KeyType,
            KeyAlgorithm = keyAlgorithm,
            KeyCurve = keyAlgorithm.Curve,
            // CXF spec requires passkeys with a non-zero counter to be excluded from the export
            // and importers to set the counter to zero.
            SignatureCounter = 0,
            // CXF only carries discoverable passkeys, since they are the only type meaningful to import.
            Discoverable = true
        };
    }
}

#endif
