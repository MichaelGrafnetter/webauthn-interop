# <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnEvent"></a> Class WebAuthnEvent

Namespace: [DSInternals.Win32.WebAuthn.Events](DSInternals.Win32.WebAuthn.Events.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Base class for all WebAuthN event log entries from the
<code>Microsoft-Windows-WebAuthN/Operational</code> event log.

```csharp
public class WebAuthnEvent
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[WebAuthnEvent](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md)

#### Derived

[ApiVersionEvent](DSInternals.Win32.WebAuthn.Events.ApiVersionEvent.md), 
[BleFunctionWarningEvent](DSInternals.Win32.WebAuthn.Events.BleFunctionWarningEvent.md), 
[CancelCurrentOperationEvent](DSInternals.Win32.WebAuthn.Events.CancelCurrentOperationEvent.md), 
[CtapCommandCompletedEvent](DSInternals.Win32.WebAuthn.Events.CtapCommandCompletedEvent.md), 
[CtapCommandErrorEvent](DSInternals.Win32.WebAuthn.Events.CtapCommandErrorEvent.md), 
[CtapCommandStartedEvent](DSInternals.Win32.WebAuthn.Events.CtapCommandStartedEvent.md), 
[DeviceInfoEvent](DSInternals.Win32.WebAuthn.Events.DeviceInfoEvent.md), 
[FunctionWarningEvent](DSInternals.Win32.WebAuthn.Events.FunctionWarningEvent.md), 
[GetAssertionRequestEvent](DSInternals.Win32.WebAuthn.Events.GetAssertionRequestEvent.md), 
[GetAssertionResponseEvent](DSInternals.Win32.WebAuthn.Events.GetAssertionResponseEvent.md), 
[IsUserVerifyingPlatformAuthenticatorAvailableEvent](DSInternals.Win32.WebAuthn.Events.IsUserVerifyingPlatformAuthenticatorAvailableEvent.md), 
[MakeCredentialCompletedEvent](DSInternals.Win32.WebAuthn.Events.MakeCredentialCompletedEvent.md), 
[MakeCredentialRequestEvent](DSInternals.Win32.WebAuthn.Events.MakeCredentialRequestEvent.md), 
[MakeCredentialResponseEvent](DSInternals.Win32.WebAuthn.Events.MakeCredentialResponseEvent.md), 
[NameValueEvent](DSInternals.Win32.WebAuthn.Events.NameValueEvent.md), 
[UsbAddDeviceEvent](DSInternals.Win32.WebAuthn.Events.UsbAddDeviceEvent.md), 
[UsbDeviceCompletedEvent](DSInternals.Win32.WebAuthn.Events.UsbDeviceCompletedEvent.md), 
[UsbDeviceStartedEvent](DSInternals.Win32.WebAuthn.Events.UsbDeviceStartedEvent.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

<p>Sample event XML:</p>
<pre><code class="lang-csharp">&lt;Event&gt;
  &lt;System&gt;
    &lt;Provider Name="Microsoft-Windows-WebAuthN" Guid="{3ae1ea61-c002-47fb-b06c-4022a8c98929}" /&gt;
    &lt;EventID&gt;1003&lt;/EventID&gt;
    &lt;Level&gt;4&lt;/Level&gt;
    &lt;TimeCreated SystemTime="2026-03-09T15:07:24.2720199Z" /&gt;
    &lt;Execution ProcessID="20656" ThreadID="21180" /&gt;
  &lt;/System&gt;
  &lt;EventData&gt;
    &lt;Data Name="TransactionId"&gt;{aabbccdd-1234-5678-9abc-def012345678}&lt;/Data&gt;
  &lt;/EventData&gt;
&lt;/Event&gt;</code></pre>

## Properties

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnEvent_Error"></a> Error

Error description, if present.

```csharp
public string? Error { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnEvent_EventId"></a> EventId

The event ID identifying the event type.

```csharp
public WebAuthnEventId EventId { get; set; }
```

#### Property Value

 [WebAuthnEventId](DSInternals.Win32.WebAuthn.Events.WebAuthnEventId.md)

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnEvent_HResult"></a> HResult

HRESULT or Win32 error code, if present.

```csharp
public int? HResult { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnEvent_Level"></a> Level

The severity level of the event.

```csharp
public StandardEventLevel? Level { get; set; }
```

#### Property Value

 [StandardEventLevel](https://learn.microsoft.com/dotnet/api/system.diagnostics.eventing.reader.standardeventlevel)?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnEvent_Message"></a> Message

The full event message text.

```csharp
public string? Message { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnEvent_ProcessId"></a> ProcessId

The process ID that generated the event.

```csharp
public int ProcessId { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnEvent_ThreadId"></a> ThreadId

The thread ID that generated the event.

```csharp
public int ThreadId { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnEvent_TimeCreated"></a> TimeCreated

The time the event was created.

```csharp
public DateTime? TimeCreated { get; set; }
```

#### Property Value

 [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnEvent_TransactionId"></a> TransactionId

The transaction ID correlating related events. May be null for events like API info queries.

```csharp
public Guid? TransactionId { get; set; }
```

#### Property Value

 [Guid](https://learn.microsoft.com/dotnet/api/system.guid)?

