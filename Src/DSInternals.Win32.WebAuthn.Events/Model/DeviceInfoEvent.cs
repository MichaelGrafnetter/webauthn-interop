using System;

namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents a CTAP device info event (Event ID 2104).
/// </summary>
/// <remarks>
/// <para>Sample event data:</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="TransactionId"&gt;{5ec89aa0-49a3-46f5-a486-b77df0a7555e}&lt;/Data&gt;
///   &lt;Data Name="ProviderName"&gt;MicrosoftPlatformProvider&lt;/Data&gt;
///   &lt;Data Name="DevicePath"&gt;&lt;/Data&gt;
///   &lt;Data Name="Manufacturer"&gt;&lt;/Data&gt;
///   &lt;Data Name="Product"&gt;&lt;/Data&gt;
///   &lt;Data Name="AAGuid"&gt;{00000000-0000-0000-0000-000000000000}&lt;/Data&gt;
///   &lt;Data Name="U2fProtocol"&gt;false&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// </remarks>
public sealed class DeviceInfoEvent : WebAuthnEvent
{
    /// <summary>
    /// The name of the CTAP provider (e.g., MicrosoftPlatformProvider).
    /// </summary>
    public string? ProviderName { get; set; }

    /// <summary>
    /// The HID device path of the authenticator.
    /// </summary>
    public string? DevicePath { get; set; }

    /// <summary>
    /// The manufacturer of the authenticator device.
    /// </summary>
    public string? Manufacturer { get; set; }

    /// <summary>
    /// The product name of the authenticator device.
    /// </summary>
    public string? Product { get; set; }

    /// <summary>
    /// Authenticator Attestation GUID identifying the authenticator model.
    /// </summary>
    public Guid? AAGuid { get; set; }

    /// <summary>
    /// Whether the device uses the legacy U2F protocol.
    /// </summary>
    public bool? U2fProtocol { get; set; }
}
