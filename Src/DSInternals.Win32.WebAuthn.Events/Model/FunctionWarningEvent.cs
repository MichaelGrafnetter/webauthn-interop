namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents a CTAP function warning event (Event IDs 2105, 2270).
/// </summary>
/// <remarks>
/// <para>Sample event data for FunctionWarning (2105):</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="Function"&gt;HidSilentGetAssertion&lt;/Data&gt;
///   &lt;Data Name="Location"&gt;SendRequest&lt;/Data&gt;
///   &lt;Data Name="Error"&gt;0x8009030d&lt;/Data&gt;
///   &lt;Data Name="Win32Error"&gt;0x8009030d&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// <para>Sample event data for BleFunctionWarning (2270):</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="Function"&gt;BleGetAssertion&lt;/Data&gt;
///   &lt;Data Name="Location"&gt;ConnectDevice&lt;/Data&gt;
///   &lt;Data Name="Error"&gt;0x80070005&lt;/Data&gt;
///   &lt;Data Name="Win32Error"&gt;0x80070005&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// </remarks>
public sealed class FunctionWarningEvent : WebAuthnEvent
{
    /// <summary>
    /// The name of the function that generated the warning.
    /// </summary>
    public string? Function { get; set; }

    /// <summary>
    /// The location within the function where the warning occurred.
    /// </summary>
    public string? Location { get; set; }
}
