using System;

namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents a CTAP platform command event (Event IDs 2100, 2102, 2103).
/// </summary>
/// <remarks>
/// <para>Sample event data for CtapCommandStarted (2100):</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="Command"&gt;GetAllPlatformCredentials&lt;/Data&gt;
///   &lt;Data Name="TransactionId"&gt;{7177835f-89f9-4689-a7e9-839e62ce40bd}&lt;/Data&gt;
///   &lt;Data Name="Flags"&gt;0x0&lt;/Data&gt;
///   &lt;Data Name="TimeoutMilliseconds"&gt;0&lt;/Data&gt;
///   &lt;Data Name="TicketLength"&gt;0&lt;/Data&gt;
///   &lt;Data Name="Ticket"&gt;&lt;/Data&gt;
///   &lt;Data Name="RequestLength"&gt;201&lt;/Data&gt;
///   &lt;Data Name="Request"&gt;A967636F6D6D616E...&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// <para>Sample event data for CtapCommandCompleted (2102):</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="Command"&gt;GetAllPlatformCredentials&lt;/Data&gt;
///   &lt;Data Name="TransactionId"&gt;{7177835f-89f9-4689-a7e9-839e62ce40bd}&lt;/Data&gt;
///   &lt;Data Name="ResponseLength"&gt;29460&lt;/Data&gt;
///   &lt;Data Name="Response"&gt;8CAA000401582049...&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// <para>Sample event data for CtapCommandError (2103):</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="Command"&gt;MakeCredential&lt;/Data&gt;
///   &lt;Data Name="TransactionId"&gt;{aabbccdd-1234-5678-9abc-def012345678}&lt;/Data&gt;
///   &lt;Data Name="Error"&gt;0x80090331&lt;/Data&gt;
///   &lt;Data Name="Win32Error"&gt;0x80090331&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// </remarks>
public sealed class CtapCommandEvent : WebAuthnEvent
{
    /// <summary>
    /// The CTAP command name or identifier.
    /// </summary>
    public int? Command { get; set; }

    /// <summary>
    /// Command flags.
    /// </summary>
    public int? Flags { get; set; }

    /// <summary>
    /// Command timeout in milliseconds.
    /// </summary>
    public int? TimeoutMilliseconds { get; set; }

    /// <summary>
    /// Authentication ticket blob.
    /// </summary>
    public byte[]? Ticket { get; set; }

    /// <summary>
    /// The CBOR-encoded request blob.
    /// </summary>
    public byte[]? Request { get; set; }

    /// <summary>
    /// The CBOR-encoded response blob.
    /// </summary>
    public byte[]? Response { get; set; }
}
