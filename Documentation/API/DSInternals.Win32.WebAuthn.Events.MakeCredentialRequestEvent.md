# <a id="DSInternals_Win32_WebAuthn_Events_MakeCredentialRequestEvent"></a> Class MakeCredentialRequestEvent

Namespace: [DSInternals.Win32.WebAuthn.Events](DSInternals.Win32.WebAuthn.Events.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents a CBOR-encoded MakeCredential request event (Event ID 1101).

```csharp
public sealed class MakeCredentialRequestEvent : WebAuthnEvent
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[WebAuthnEvent](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md) ← 
[MakeCredentialRequestEvent](DSInternals.Win32.WebAuthn.Events.MakeCredentialRequestEvent.md)

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
  &lt;Data Name="RpId"&gt;login.microsoft.com&lt;/Data&gt;
  &lt;Data Name="UserIdLength"&gt;16&lt;/Data&gt;
  &lt;Data Name="UserId"&gt;A1B2C3D4E5F6A1B2C3D4E5F6A1B2C3D4&lt;/Data&gt;
  &lt;Data Name="ClientDataHashAlgId"&gt;SHA-256&lt;/Data&gt;
  &lt;Data Name="ClientDataLength"&gt;210&lt;/Data&gt;
  &lt;Data Name="ClientDataHashLength"&gt;32&lt;/Data&gt;
  &lt;Data Name="ClientDataHash"&gt;465746E9120D0C16...&lt;/Data&gt;
  &lt;Data Name="RequireResidentKey"&gt;true&lt;/Data&gt;
  &lt;Data Name="CredentialCount"&gt;0&lt;/Data&gt;
  &lt;Data Name="CredentialParameterCount"&gt;2&lt;/Data&gt;
  &lt;Data Name="RequestLength"&gt;181&lt;/Data&gt;
  &lt;Data Name="Request"&gt;01A501786C6F67696E...&lt;/Data&gt;
&lt;/EventData&gt;</code></pre>

## Properties

### <a id="DSInternals_Win32_WebAuthn_Events_MakeCredentialRequestEvent_ClientDataHash"></a> ClientDataHash

The hash of the client data.

```csharp
public byte[]? ClientDataHash { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Events_MakeCredentialRequestEvent_ClientDataHashAlgId"></a> ClientDataHashAlgId

The hash algorithm used for client data (e.g., SHA-256).

```csharp
public string? ClientDataHashAlgId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_MakeCredentialRequestEvent_ClientDataLength"></a> ClientDataLength

The length of the client data in bytes.

```csharp
public int? ClientDataLength { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)?

### <a id="DSInternals_Win32_WebAuthn_Events_MakeCredentialRequestEvent_CredentialCount"></a> CredentialCount

Number of credentials in the exclude list.

```csharp
public int? CredentialCount { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)?

### <a id="DSInternals_Win32_WebAuthn_Events_MakeCredentialRequestEvent_CredentialParameterCount"></a> CredentialParameterCount

The number of credential parameter types requested.

```csharp
public int? CredentialParameterCount { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)?

### <a id="DSInternals_Win32_WebAuthn_Events_MakeCredentialRequestEvent_Request"></a> Request

The raw CTAP2 CBOR-encoded request blob (prefixed with command byte 0x01).

```csharp
public byte[]? Request { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Events_MakeCredentialRequestEvent_RequireResidentKey"></a> RequireResidentKey

Whether a resident (discoverable) credential was required.

```csharp
public bool? RequireResidentKey { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="DSInternals_Win32_WebAuthn_Events_MakeCredentialRequestEvent_RpId"></a> RpId

The relying party identifier.

```csharp
public string? RpId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_MakeCredentialRequestEvent_UserId"></a> UserId

The user identifier provided by the relying party.

```csharp
public byte[]? UserId { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

