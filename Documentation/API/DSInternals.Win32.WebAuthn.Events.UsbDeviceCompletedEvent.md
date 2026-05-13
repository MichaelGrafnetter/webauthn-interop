# <a id="DSInternals_Win32_WebAuthn_Events_UsbDeviceCompletedEvent"></a> Class UsbDeviceCompletedEvent

Namespace: [DSInternals.Win32.WebAuthn.Events](DSInternals.Win32.WebAuthn.Events.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents a USB device completed event (Event ID 2211).

```csharp
public sealed class UsbDeviceCompletedEvent : WebAuthnEvent
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[WebAuthnEvent](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md) ← 
[UsbDeviceCompletedEvent](DSInternals.Win32.WebAuthn.Events.UsbDeviceCompletedEvent.md)

#### Inherited Members

[WebAuthnEvent.EventId](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnEvent\_EventId), 
[WebAuthnEvent.TimeCreated](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnEvent\_TimeCreated), 
[WebAuthnEvent.ProcessId](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnEvent\_ProcessId), 
[WebAuthnEvent.ThreadId](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnEvent\_ThreadId), 
[WebAuthnEvent.Level](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnEvent\_Level), 
[WebAuthnEvent.Message](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnEvent\_Message), 
[WebAuthnEvent.TransactionId](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnEvent\_TransactionId), 
[WebAuthnEvent.Error](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnEvent\_Error), 
[WebAuthnEvent.HResult](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnEvent\_HResult), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<p>Sample event data:</p>
<pre><code class="lang-csharp">&lt;EventData&gt;
  &lt;Data Name="TransactionId"&gt;{aabbccdd-1234-5678-9abc-def012345678}&lt;/Data&gt;
  &lt;Data Name="DevicePath"&gt;\\?\hid#vid_1050&amp;pid_0407#...&lt;/Data&gt;
  &lt;Data Name="Manufacturer"&gt;Yubico&lt;/Data&gt;
  &lt;Data Name="Product"&gt;YubiKey OTP+FIDO+CCID&lt;/Data&gt;
  &lt;Data Name="AAGuid"&gt;{2fc0579f-8113-47ea-b116-bb5a8db9202a}&lt;/Data&gt;
  &lt;Data Name="U2fProtocol"&gt;false&lt;/Data&gt;
&lt;/EventData&gt;</code></pre>

## Properties

### <a id="DSInternals_Win32_WebAuthn_Events_UsbDeviceCompletedEvent_AAGuid"></a> AAGuid

AAGUID of the USB authenticator.

```csharp
public Guid? AAGuid { get; set; }
```

#### Property Value

 [Guid](https://learn.microsoft.com/dotnet/api/system.guid)?

### <a id="DSInternals_Win32_WebAuthn_Events_UsbDeviceCompletedEvent_DevicePath"></a> DevicePath

The HID device path of the USB authenticator.

```csharp
public string? DevicePath { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_UsbDeviceCompletedEvent_Manufacturer"></a> Manufacturer

The manufacturer of the USB authenticator device.

```csharp
public string? Manufacturer { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_UsbDeviceCompletedEvent_Product"></a> Product

The product name of the USB authenticator device.

```csharp
public string? Product { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_UsbDeviceCompletedEvent_U2fProtocol"></a> U2fProtocol

Whether the device uses the U2F protocol.

```csharp
public bool? U2fProtocol { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

