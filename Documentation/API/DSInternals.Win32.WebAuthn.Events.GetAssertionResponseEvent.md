# <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionResponseEvent"></a> Class GetAssertionResponseEvent

Namespace: [DSInternals.Win32.WebAuthn.Events](DSInternals.Win32.WebAuthn.Events.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents a CBOR-decoded GetAssertion response event (Event ID 1104).

```csharp
public sealed class GetAssertionResponseEvent : WebAuthnEvent
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[WebAuthnEvent](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md) ← 
[GetAssertionResponseEvent](DSInternals.Win32.WebAuthn.Events.GetAssertionResponseEvent.md)

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
  &lt;Data Name="RpIdHashLength"&gt;32&lt;/Data&gt;
  &lt;Data Name="RpIdHash"&gt;3AEB002460381C6F...&lt;/Data&gt;
  &lt;Data Name="Flags"&gt;0x5&lt;/Data&gt;
  &lt;Data Name="SignCount"&gt;0x54&lt;/Data&gt;
  &lt;Data Name="CredentialIdLength"&gt;32&lt;/Data&gt;
  &lt;Data Name="CredentialId"&gt;90613158E4C99370...&lt;/Data&gt;
  &lt;Data Name="ResponseLength"&gt;240&lt;/Data&gt;
  &lt;Data Name="Response"&gt;A401A26269645820...&lt;/Data&gt;
&lt;/EventData&gt;</code></pre>

## Properties

### <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionResponseEvent_AuthenticatorFlags"></a> AuthenticatorFlags

Authenticator data flags byte (UP, UV, AT, ED, etc.).

```csharp
public AuthenticatorFlags? AuthenticatorFlags { get; set; }
```

#### Property Value

 [AuthenticatorFlags](DSInternals.Win32.WebAuthn.AuthenticatorFlags.md)?

### <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionResponseEvent_CredentialId"></a> CredentialId

The credential ID used for the assertion.

```csharp
public byte[]? CredentialId { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionResponseEvent_Response"></a> Response

The raw CBOR-encoded response blob.

```csharp
public byte[]? Response { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionResponseEvent_RpIdHash"></a> RpIdHash

SHA-256 hash of the relying party ID.

```csharp
public byte[]? RpIdHash { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionResponseEvent_SignCount"></a> SignCount

Signature counter value.

```csharp
public uint? SignCount { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)?

