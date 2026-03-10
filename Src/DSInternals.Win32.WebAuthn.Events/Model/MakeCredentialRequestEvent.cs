using System;

namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents a CBOR-encoded MakeCredential request event (Event ID 1101).
/// </summary>
/// <remarks>
/// <para>Sample event data:</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="TransactionId"&gt;{aabbccdd-1234-5678-9abc-def012345678}&lt;/Data&gt;
///   &lt;Data Name="RpId"&gt;login.microsoft.com&lt;/Data&gt;
///   &lt;Data Name="UserIdLength"&gt;16&lt;/Data&gt;
///   &lt;Data Name="UserId"&gt;A1B2C3D4E5F6A1B2C3D4E5F6A1B2C3D4&lt;/Data&gt;
///   &lt;Data Name="ClientDataHashAlgId"&gt;SHA-256&lt;/Data&gt;
///   &lt;Data Name="ClientDataLength"&gt;210&lt;/Data&gt;
///   &lt;Data Name="ClientDataHashLength"&gt;32&lt;/Data&gt;
///   &lt;Data Name="ClientDataHash"&gt;465746E9120D0C16...&lt;/Data&gt;
///   &lt;Data Name="RequireResidentKey"&gt;true&lt;/Data&gt;
///   &lt;Data Name="CredentialCount"&gt;0&lt;/Data&gt;
///   &lt;Data Name="CredentialParameterCount"&gt;2&lt;/Data&gt;
///   &lt;Data Name="RequestLength"&gt;181&lt;/Data&gt;
///   &lt;Data Name="Request"&gt;01A501786C6F67696E...&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// </remarks>
public sealed class MakeCredentialRequestEvent : WebAuthnEvent
{
    /// <summary>
    /// The relying party identifier.
    /// </summary>
    public string? RpId { get; set; }

    /// <summary>
    /// The user identifier provided by the relying party.
    /// </summary>
    public byte[]? UserId { get; set; }

    /// <summary>
    /// The hash algorithm used for client data (e.g., SHA-256).
    /// </summary>
    public string? ClientDataHashAlgId { get; set; }

    /// <summary>
    /// The length of the client data in bytes.
    /// </summary>
    public int? ClientDataLength { get; set; }

    /// <summary>
    /// The hash of the client data.
    /// </summary>
    public byte[]? ClientDataHash { get; set; }

    /// <summary>
    /// Whether a resident (discoverable) credential was required.
    /// </summary>
    public bool? RequireResidentKey { get; set; }

    /// <summary>
    /// Number of credentials in the exclude list.
    /// </summary>
    public int? CredentialCount { get; set; }

    /// <summary>
    /// The number of credential parameter types requested.
    /// </summary>
    public int? CredentialParameterCount { get; set; }

    /// <summary>
    /// The raw CTAP2 CBOR-encoded request blob (prefixed with command byte 0x01).
    /// </summary>
    public byte[]? Request { get; set; }
}
