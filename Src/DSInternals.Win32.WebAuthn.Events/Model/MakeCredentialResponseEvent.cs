using System;

namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents a CBOR-decoded MakeCredential response event (Event IDs 1102, 1041).
/// </summary>
/// <remarks>
/// <para>Sample event data for CborMakeCredentialResponse (1102):</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="TransactionId"&gt;{aabbccdd-1234-5678-9abc-def012345678}&lt;/Data&gt;
///   &lt;Data Name="AttestationFormatType"&gt;none&lt;/Data&gt;
///   &lt;Data Name="RpIdHashLength"&gt;32&lt;/Data&gt;
///   &lt;Data Name="RpIdHash"&gt;3AEB002460381C6F...&lt;/Data&gt;
///   &lt;Data Name="Flags"&gt;0x45&lt;/Data&gt;
///   &lt;Data Name="SignCount"&gt;0x0&lt;/Data&gt;
///   &lt;Data Name="AAGuid"&gt;{de1e552d-db1d-4423-a439-09523d6f04a0}&lt;/Data&gt;
///   &lt;Data Name="CredentialIdLength"&gt;32&lt;/Data&gt;
///   &lt;Data Name="CredentialId"&gt;90613158E4C99370...&lt;/Data&gt;
///   &lt;Data Name="U2fPublicKey"&gt;&lt;/Data&gt;
///   &lt;Data Name="PublicKeyLength"&gt;77&lt;/Data&gt;
///   &lt;Data Name="PublicKey"&gt;A5010203262001...&lt;/Data&gt;
///   &lt;Data Name="ResponseLength"&gt;240&lt;/Data&gt;
///   &lt;Data Name="Response"&gt;A401A26269645820...&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// <para>Sample event data for NgcMakeCredentialResponse (1041):</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="TransactionId"&gt;{aabbccdd-1234-5678-9abc-def012345678}&lt;/Data&gt;
///   &lt;Data Name="AttestationFormatType"&gt;packed&lt;/Data&gt;
///   &lt;Data Name="RpIdHashLength"&gt;32&lt;/Data&gt;
///   &lt;Data Name="RpIdHash"&gt;49960DE5880E8C68...&lt;/Data&gt;
///   &lt;Data Name="Flags"&gt;0x45&lt;/Data&gt;
///   &lt;Data Name="SignCount"&gt;0x0&lt;/Data&gt;
///   &lt;Data Name="AAGuid"&gt;{de1e552d-db1d-4423-a439-09523d6f04a0}&lt;/Data&gt;
///   &lt;Data Name="CredentialIdLength"&gt;32&lt;/Data&gt;
///   &lt;Data Name="CredentialId"&gt;F2B3A1D4E5C67890...&lt;/Data&gt;
///   &lt;Data Name="U2fPublicKey"&gt;&lt;/Data&gt;
///   &lt;Data Name="PublicKeyLength"&gt;77&lt;/Data&gt;
///   &lt;Data Name="PublicKey"&gt;A5010203262001...&lt;/Data&gt;
///   &lt;Data Name="ResponseLength"&gt;350&lt;/Data&gt;
///   &lt;Data Name="Response"&gt;A301667061636B65...&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// </remarks>
public sealed class MakeCredentialResponseEvent : WebAuthnEvent
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
    public byte? AuthenticatorFlags { get; set; }

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
