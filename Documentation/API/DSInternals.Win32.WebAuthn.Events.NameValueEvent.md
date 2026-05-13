# <a id="DSInternals_Win32_WebAuthn_Events_NameValueEvent"></a> Class NameValueEvent

Namespace: [DSInternals.Win32.WebAuthn.Events](DSInternals.Win32.WebAuthn.Events.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents a CTAP name-value pair event (Event ID 2106).
Contains JSON response data like authenticationResponseJSON or registrationResponseJSON.

```csharp
public sealed class NameValueEvent : WebAuthnEvent
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[WebAuthnEvent](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md) ← 
[NameValueEvent](DSInternals.Win32.WebAuthn.Events.NameValueEvent.md)

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
  &lt;Data Name="Name"&gt;authenticationResponseJSON&lt;/Data&gt;
  &lt;Data Name="Value"&gt;{"authenticatorAttachment":"platform","clientExtensionResults":{},...}&lt;/Data&gt;
&lt;/EventData&gt;</code></pre>

## Properties

### <a id="DSInternals_Win32_WebAuthn_Events_NameValueEvent_Name"></a> Name

The name of the value (e.g., authenticationResponseJSON, registrationResponseJSON).

```csharp
public string? Name { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_NameValueEvent_Value"></a> Value

The value, typically a JSON string containing the WebAuthn response.

```csharp
public string? Value { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

