using System;

namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents a CBOR-decoded GetAssertion response event (Event ID 1104).
/// </summary>
/// <remarks>
/// <para>Sample event data:</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="TransactionId"&gt;{5ec89aa0-49a3-46f5-a486-b77df0a7555e}&lt;/Data&gt;
///   &lt;Data Name="RpIdHashLength"&gt;32&lt;/Data&gt;
///   &lt;Data Name="RpIdHash"&gt;3AEB002460381C6F...&lt;/Data&gt;
///   &lt;Data Name="Flags"&gt;0x5&lt;/Data&gt;
///   &lt;Data Name="SignCount"&gt;0x54&lt;/Data&gt;
///   &lt;Data Name="CredentialIdLength"&gt;32&lt;/Data&gt;
///   &lt;Data Name="CredentialId"&gt;90613158E4C99370...&lt;/Data&gt;
///   &lt;Data Name="ResponseLength"&gt;240&lt;/Data&gt;
///   &lt;Data Name="Response"&gt;A401A26269645820...&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// </remarks>
public sealed class GetAssertionResponseEvent : WebAuthnEvent
{
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
    /// The credential ID used for the assertion.
    /// </summary>
    public byte[]? CredentialId { get; set; }

    /// <summary>
    /// The raw CBOR-encoded response blob.
    /// </summary>
    public byte[]? Response { get; set; }
}
