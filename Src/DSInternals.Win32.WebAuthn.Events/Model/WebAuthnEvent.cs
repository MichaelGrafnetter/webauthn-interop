using System;
using System.Diagnostics.Eventing.Reader;

namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Base class for all WebAuthN event log entries from the
/// <c>Microsoft-Windows-WebAuthN/Operational</c> event log.
/// </summary>
/// <remarks>
/// <para>Sample event XML:</para>
/// <code>
/// &lt;Event&gt;
///   &lt;System&gt;
///     &lt;Provider Name="Microsoft-Windows-WebAuthN" Guid="{3ae1ea61-c002-47fb-b06c-4022a8c98929}" /&gt;
///     &lt;EventID&gt;1003&lt;/EventID&gt;
///     &lt;Level&gt;4&lt;/Level&gt;
///     &lt;TimeCreated SystemTime="2026-03-09T15:07:24.2720199Z" /&gt;
///     &lt;Execution ProcessID="20656" ThreadID="21180" /&gt;
///   &lt;/System&gt;
///   &lt;EventData&gt;
///     &lt;Data Name="TransactionId"&gt;{aabbccdd-1234-5678-9abc-def012345678}&lt;/Data&gt;
///   &lt;/EventData&gt;
/// &lt;/Event&gt;
/// </code>
/// </remarks>
public class WebAuthnEvent
{
    /// <summary>
    /// The event ID identifying the event type.
    /// </summary>
    public WebAuthnEventId EventId { get; set; }

    /// <summary>
    /// The time the event was created.
    /// </summary>
    public DateTime? TimeCreated { get; set; }

    /// <summary>
    /// The process ID that generated the event.
    /// </summary>
    public int ProcessId { get; set; }

    /// <summary>
    /// The thread ID that generated the event.
    /// </summary>
    public int ThreadId { get; set; }

    /// <summary>
    /// The severity level of the event.
    /// </summary>
    public StandardEventLevel? Level { get; set; }

    /// <summary>
    /// The full event message text.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// The transaction ID correlating related events. May be null for events like API info queries.
    /// </summary>
    public Guid? TransactionId { get; set; }

    /// <summary>
    /// Error description, if present.
    /// </summary>
    public string? Error { get; set; }

    /// <summary>
    /// HRESULT or Win32 error code, if present.
    /// </summary>
    public int? HResult { get; set; }
}
