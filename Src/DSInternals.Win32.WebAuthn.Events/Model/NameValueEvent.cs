namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents a CTAP name-value pair event (Event ID 2106).
/// Contains JSON response data like authenticationResponseJSON or registrationResponseJSON.
/// </summary>
/// <remarks>
/// <para>Sample event data:</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="Name"&gt;authenticationResponseJSON&lt;/Data&gt;
///   &lt;Data Name="Value"&gt;{"authenticatorAttachment":"platform","clientExtensionResults":{},...}&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// </remarks>
public sealed class NameValueEvent : WebAuthnEvent
{
    /// <summary>
    /// The name of the value (e.g., authenticationResponseJSON, registrationResponseJSON).
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The value, typically a JSON string containing the WebAuthn response.
    /// </summary>
    public string? Value { get; set; }
}
