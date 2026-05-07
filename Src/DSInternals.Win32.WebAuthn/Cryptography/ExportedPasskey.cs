using System;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.COSE;
using CoseKeyType = DSInternals.Win32.WebAuthn.COSE.KeyType;

namespace DSInternals.Win32.WebAuthn.Cryptography;

/// <summary>
/// Represents a passkey imported from an external authenticator export.
/// </summary>
public sealed class ExportedPasskey
{
    /// <summary>
    /// The AAGUID used by the authenticator.
    /// </summary>
    public required Guid AaGuid { get; init; }

    /// <summary>
    /// The username associated with this passkey.
    /// </summary>
    public string? Username { get; init; }

    /// <summary>
    /// The display name associated with this passkey.
    /// </summary>
    public string? UserDisplayName { get; init; }

    /// <summary>
    /// The relying party ID associated with this passkey.
    /// </summary>
    public string? RelyingParty { get; init; }

    /// <summary>
    /// The relying party display name associated with this passkey.
    /// </summary>
    public string? RelyingPartyName { get; init; }

    /// <summary>
    /// The URL associated with this passkey, when provided by the export.
    /// </summary>
    public string? Url { get; init; }

    /// <summary>
    /// The WebAuthn credential ID, encoded as Base64Url.
    /// </summary>
    public required byte[] CredentialId { get; init; }

    /// <summary>
    /// The WebAuthn user handle, encoded as Base64Url.
    /// </summary>
    public byte[]? UserHandle { get; init; }

    /// <summary>
    /// The imported private key.
    /// </summary>
    [JsonIgnore]
    public required AsymmetricAlgorithm PrivateKey { get; init; }

    /// <summary>
    /// The COSE key type.
    /// </summary>
    public CoseKeyType KeyType { get; init; }

    /// <summary>
    /// The COSE algorithm.
    /// </summary>
    public Algorithm KeyAlgorithm { get; init; }

    /// <summary>
    /// The elliptic curve, when applicable.
    /// </summary>
    public EllipticCurve? KeyCurve { get; init; }

    /// <summary>
    /// The signature counter stored with this passkey.
    /// </summary>
    public uint SignatureCounter { get; init; }

    /// <summary>
    /// Indicates whether the export marked this passkey as discoverable.
    /// </summary>
    public bool Discoverable { get; init; }
}
