using System;

namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents a CBOR-encoded GetAssertion request event (Event ID 1103).
/// </summary>
/// <remarks>
/// <para>Sample event data:</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="TransactionId"&gt;{5ec89aa0-49a3-46f5-a486-b77df0a7555e}&lt;/Data&gt;
///   &lt;Data Name="RpId"&gt;login.microsoft.com&lt;/Data&gt;
///   &lt;Data Name="ClientDataHashAlgId"&gt;SHA-256&lt;/Data&gt;
///   &lt;Data Name="ClientDataLength"&gt;131&lt;/Data&gt;
///   &lt;Data Name="ClientDataHashLength"&gt;32&lt;/Data&gt;
///   &lt;Data Name="ClientDataHash"&gt;465746E9120D0C16...&lt;/Data&gt;
///   &lt;Data Name="CredentialCount"&gt;5&lt;/Data&gt;
///   &lt;Data Name="RequestLength"&gt;420&lt;/Data&gt;
///   &lt;Data Name="Request"&gt;02A4016A6C6F6769...&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// </remarks>
public sealed class GetAssertionRequestEvent : WebAuthnEvent
{
    /// <summary>
    /// The relying party identifier.
    /// </summary>
    public string? RpId { get; set; }

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
    /// Number of credentials in the allow list.
    /// </summary>
    public int? CredentialCount { get; set; }

    /// <summary>
    /// The raw CTAP2 CBOR-encoded request blob (prefixed with command byte 0x02).
    /// </summary>
    public byte[]? Request { get; set; }
}
