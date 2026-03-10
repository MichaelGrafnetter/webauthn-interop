namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents an API information query event (Event IDs 1070, 1071).
/// </summary>
/// <remarks>
/// <para>Sample event data for IsUserVerifyingPlatformAuthenticatorAvailable (1070):</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="value"&gt;true&lt;/Data&gt;
///   &lt;Data Name="Error"&gt;0x0&lt;/Data&gt;
///   &lt;Data Name="HResult"&gt;0&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// <para>Sample event data for ApiVersion (1071):</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="value"&gt;9&lt;/Data&gt;
///   &lt;Data Name="Error"&gt;0x0&lt;/Data&gt;
///   &lt;Data Name="HResult"&gt;0&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// </remarks>
public sealed class ApiInfoEvent : WebAuthnEvent
{
    /// <summary>
    /// The reported API value (e.g., "true" for authenticator availability, or the API version number).
    /// </summary>
    public string? Value { get; set; }
}
