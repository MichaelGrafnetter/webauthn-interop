namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents a CTAP platform command started event (Event ID 2100).
/// </summary>
/// <remarks>
/// <para>Sample event data:</para>
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
/// <para>Known command names: MakeCredential, GetAssertion, GetAllPlatformCredentials,
/// GetAuthenticatorList, GetPluginAuthenticatorList, CreateTicket, UpdatePluginAuthenticator,
/// AddPluginAuthenticatorCredentials, RemoveAllPluginAuthenticatorCredentials, WebAuthN, command.</para>
/// </remarks>
public class CtapCommandStartedEvent : WebAuthnEvent
{
    /// <summary>
    /// The CTAP command name (e.g., "MakeCredential", "GetAssertion", "GetAllPlatformCredentials").
    /// </summary>
    public string? Command { get; set; }

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
}
