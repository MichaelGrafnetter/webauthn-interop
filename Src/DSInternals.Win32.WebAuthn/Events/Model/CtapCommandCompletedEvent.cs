namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents a CTAP platform command completed event (Event ID 2102).
/// </summary>
/// <remarks>
/// <para>Sample event data:</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="Command"&gt;GetAllPlatformCredentials&lt;/Data&gt;
///   &lt;Data Name="TransactionId"&gt;{7177835f-89f9-4689-a7e9-839e62ce40bd}&lt;/Data&gt;
///   &lt;Data Name="ResponseLength"&gt;29460&lt;/Data&gt;
///   &lt;Data Name="Response"&gt;8CAA000401582049...&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// <para>Known command names: MakeCredential, GetAssertion, GetAllPlatformCredentials,
/// GetAuthenticatorList, GetPluginAuthenticatorList, CreateTicket, UpdatePluginAuthenticator,
/// RemoveAllPluginAuthenticatorCredentials, WebAuthN, command.</para>
/// </remarks>
public class CtapCommandCompletedEvent : WebAuthnEvent
{
    /// <summary>
    /// The CTAP command name (e.g., "MakeCredential", "GetAssertion", "GetAllPlatformCredentials").
    /// </summary>
    public string? Command { get; set; }

    /// <summary>
    /// The CBOR-encoded response blob.
    /// </summary>
    public byte[]? Response { get; set; }
}
