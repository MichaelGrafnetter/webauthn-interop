using System;

namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Base class for MakeCredential response events (<see cref="CborMakeCredentialResponseEvent"/> and <see cref="NgcMakeCredentialResponseEvent"/>).
/// </summary>
public abstract class MakeCredentialResponseEvent : WebAuthnEvent
{
    /// <summary>
    /// The attestation format type (e.g., "none", "packed", "tpm").
    /// </summary>
    public string? AttestationFormatType { get; set; }

    /// <summary>
    /// SHA-256 hash of the relying party ID.
    /// </summary>
    public byte[]? RpIdHash { get; set; }

    /// <summary>
    /// Authenticator data flags byte (UP, UV, AT, ED, etc.).
    /// </summary>
    public AuthenticatorFlags? AuthenticatorFlags { get; set; }

    /// <summary>
    /// Signature counter value.
    /// </summary>
    public uint? SignCount { get; set; }

    /// <summary>
    /// Authenticator Attestation GUID identifying the authenticator model.
    /// </summary>
    public Guid? AAGuid { get; set; }

    /// <summary>
    /// The credential ID of the newly created credential.
    /// </summary>
    public byte[]? CredentialId { get; set; }

    /// <summary>
    /// The U2F public key, if the authenticator uses the legacy U2F protocol.
    /// </summary>
    public byte[]? U2fPublicKey { get; set; }

    /// <summary>
    /// The COSE-encoded public key of the newly created credential.
    /// </summary>
    public byte[]? PublicKey { get; set; }

    /// <summary>
    /// The raw CBOR-encoded response blob.
    /// </summary>
    public byte[]? Response { get; set; }
}
