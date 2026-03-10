using System;

namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents a USB device event (Event IDs 2210, 2211, 2220).
/// </summary>
/// <remarks>
/// <para>Sample event data for UsbDeviceStarted (2210):</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="TransactionId"&gt;{aabbccdd-1234-5678-9abc-def012345678}&lt;/Data&gt;
///   &lt;Data Name="DevicePath"&gt;\\?\hid#vid_1050&amp;pid_0407#...&lt;/Data&gt;
///   &lt;Data Name="Manufacturer"&gt;Yubico&lt;/Data&gt;
///   &lt;Data Name="Product"&gt;YubiKey OTP+FIDO+CCID&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// <para>Sample event data for UsbDeviceCompleted (2211):</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="TransactionId"&gt;{aabbccdd-1234-5678-9abc-def012345678}&lt;/Data&gt;
///   &lt;Data Name="DevicePath"&gt;\\?\hid#vid_1050&amp;pid_0407#...&lt;/Data&gt;
///   &lt;Data Name="Manufacturer"&gt;Yubico&lt;/Data&gt;
///   &lt;Data Name="Product"&gt;YubiKey OTP+FIDO+CCID&lt;/Data&gt;
///   &lt;Data Name="AAGuid"&gt;{2fc0579f-8113-47ea-b116-bb5a8db9202a}&lt;/Data&gt;
///   &lt;Data Name="U2fProtocol"&gt;false&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// <para>Sample event data for UsbAddDevice (2220):</para>
/// <code>
/// &lt;EventData&gt;
///   &lt;Data Name="TransactionId"&gt;{aabbccdd-1234-5678-9abc-def012345678}&lt;/Data&gt;
///   &lt;Data Name="DevicePath"&gt;\\?\hid#vid_1050&amp;pid_0407#...&lt;/Data&gt;
///   &lt;Data Name="Manufacturer"&gt;Yubico&lt;/Data&gt;
///   &lt;Data Name="Product"&gt;YubiKey OTP+FIDO+CCID&lt;/Data&gt;
/// &lt;/EventData&gt;
/// </code>
/// </remarks>
public sealed class UsbDeviceEvent : WebAuthnEvent
{
    /// <summary>
    /// The HID device path of the USB authenticator.
    /// </summary>
    public string? DevicePath { get; set; }

    /// <summary>
    /// The manufacturer of the USB authenticator device.
    /// </summary>
    public string? Manufacturer { get; set; }

    /// <summary>
    /// The product name of the USB authenticator device.
    /// </summary>
    public string? Product { get; set; }

    /// <summary>
    /// AAGUID of the USB authenticator. Only available in completed events (2211).
    /// </summary>
    public Guid? AAGuid { get; set; }

    /// <summary>
    /// Whether the device uses the U2F protocol. Only available in completed events (2211).
    /// </summary>
    public bool? U2fProtocol { get; set; }
}
