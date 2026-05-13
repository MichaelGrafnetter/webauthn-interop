# <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionRequestEvent"></a> Class GetAssertionRequestEvent

Namespace: [DSInternals.Win32.WebAuthn.Events](DSInternals.Win32.WebAuthn.Events.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents a CBOR-encoded GetAssertion request event (Event ID 1103).

```csharp
public sealed class GetAssertionRequestEvent : WebAuthnEvent
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[WebAuthnEvent](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md) ← 
[GetAssertionRequestEvent](DSInternals.Win32.WebAuthn.Events.GetAssertionRequestEvent.md)

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
  &lt;Data Name="TransactionId"&gt;{5ec89aa0-49a3-46f5-a486-b77df0a7555e}&lt;/Data&gt;
  &lt;Data Name="RpId"&gt;login.microsoft.com&lt;/Data&gt;
  &lt;Data Name="ClientDataHashAlgId"&gt;SHA-256&lt;/Data&gt;
  &lt;Data Name="ClientDataLength"&gt;131&lt;/Data&gt;
  &lt;Data Name="ClientDataHashLength"&gt;32&lt;/Data&gt;
  &lt;Data Name="ClientDataHash"&gt;465746E9120D0C16...&lt;/Data&gt;
  &lt;Data Name="CredentialCount"&gt;5&lt;/Data&gt;
  &lt;Data Name="RequestLength"&gt;420&lt;/Data&gt;
  &lt;Data Name="Request"&gt;02A4016A6C6F6769...&lt;/Data&gt;
&lt;/EventData&gt;</code></pre>

## Properties

### <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionRequestEvent_ClientDataHash"></a> ClientDataHash

The hash of the client data.

```csharp
public byte[]? ClientDataHash { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionRequestEvent_ClientDataHashAlgId"></a> ClientDataHashAlgId

The hash algorithm used for client data (e.g., SHA-256).

```csharp
public string? ClientDataHashAlgId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionRequestEvent_ClientDataLength"></a> ClientDataLength

The length of the client data in bytes.

```csharp
public int? ClientDataLength { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)?

### <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionRequestEvent_CredentialCount"></a> CredentialCount

Number of credentials in the allow list.

```csharp
public int? CredentialCount { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)?

### <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionRequestEvent_Request"></a> Request

The raw CTAP2 CBOR-encoded request blob (prefixed with command byte 0x02).

```csharp
public byte[]? Request { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionRequestEvent_RpId"></a> RpId

The relying party identifier.

```csharp
public string? RpId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

