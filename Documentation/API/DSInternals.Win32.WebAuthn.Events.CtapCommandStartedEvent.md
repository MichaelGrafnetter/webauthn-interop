# <a id="DSInternals_Win32_WebAuthn_Events_CtapCommandStartedEvent"></a> Class CtapCommandStartedEvent

Namespace: [DSInternals.Win32.WebAuthn.Events](DSInternals.Win32.WebAuthn.Events.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents a CTAP platform command started event (Event ID 2100).

```csharp
public sealed class CtapCommandStartedEvent : WebAuthnEvent
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[WebAuthnEvent](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md) ← 
[CtapCommandStartedEvent](DSInternals.Win32.WebAuthn.Events.CtapCommandStartedEvent.md)

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
  &lt;Data Name="Command"&gt;GetAllPlatformCredentials&lt;/Data&gt;
  &lt;Data Name="TransactionId"&gt;{7177835f-89f9-4689-a7e9-839e62ce40bd}&lt;/Data&gt;
  &lt;Data Name="Flags"&gt;0x0&lt;/Data&gt;
  &lt;Data Name="TimeoutMilliseconds"&gt;0&lt;/Data&gt;
  &lt;Data Name="TicketLength"&gt;0&lt;/Data&gt;
  &lt;Data Name="Ticket"&gt;&lt;/Data&gt;
  &lt;Data Name="RequestLength"&gt;201&lt;/Data&gt;
  &lt;Data Name="Request"&gt;A967636F6D6D616E...&lt;/Data&gt;
&lt;/EventData&gt;</code></pre>
<p>Known command names: MakeCredential, GetAssertion, GetAllPlatformCredentials,
GetAuthenticatorList, GetPluginAuthenticatorList, CreateTicket, UpdatePluginAuthenticator,
RemoveAllPluginAuthenticatorCredentials, WebAuthN, command.</p>

## Properties

### <a id="DSInternals_Win32_WebAuthn_Events_CtapCommandStartedEvent_Command"></a> Command

The CTAP command name (e.g., "MakeCredential", "GetAssertion", "GetAllPlatformCredentials").

```csharp
public string? Command { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_CtapCommandStartedEvent_Flags"></a> Flags

Command flags.

```csharp
public int? Flags { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)?

### <a id="DSInternals_Win32_WebAuthn_Events_CtapCommandStartedEvent_Request"></a> Request

The CBOR-encoded request blob.

```csharp
public byte[]? Request { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Events_CtapCommandStartedEvent_Ticket"></a> Ticket

Authentication ticket blob.

```csharp
public byte[]? Ticket { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Events_CtapCommandStartedEvent_TimeoutMilliseconds"></a> TimeoutMilliseconds

Command timeout in milliseconds.

```csharp
public int? TimeoutMilliseconds { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)?

