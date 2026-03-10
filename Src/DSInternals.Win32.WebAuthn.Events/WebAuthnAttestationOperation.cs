using System;

namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Aggregated data about a WebAuthn registration (MakeCredential) operation,
/// combining information from multiple related WebAuthN events.
/// </summary>
public sealed class WebAuthnAttestationOperation : WebAuthnOperation
{
    public override string OperationType => "Registration";

    /// <summary>
    /// Whether a resident (discoverable) key was required.
    /// </summary>
    public bool? RequireResidentKey { get; set; }

    /// <summary>
    /// The number of credential parameter types requested.
    /// </summary>
    public int? CredentialParameterCount { get; set; }

    /// <summary>
    /// The attestation format type (e.g., "none", "packed", "tpm").
    /// </summary>
    public string? AttestationFormatType { get; set; }

    /// <summary>
    /// The hash algorithm used for client data (e.g., SHA-256).
    /// </summary>
    public string? ClientDataHashAlgId { get; set; }

    /// <summary>
    /// The U2F public key, if available.
    /// </summary>
    public byte[]? U2fPublicKey { get; set; }

    /// <summary>
    /// The COSE-encoded public key.
    /// </summary>
    public byte[]? PublicKey { get; set; }
}
