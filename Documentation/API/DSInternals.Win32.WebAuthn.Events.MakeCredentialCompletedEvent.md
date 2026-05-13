# <a id="DSInternals_Win32_WebAuthn_Events_MakeCredentialCompletedEvent"></a> Class MakeCredentialCompletedEvent

Namespace: [DSInternals.Win32.WebAuthn.Events](DSInternals.Win32.WebAuthn.Events.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents a credential creation operation completed event (Event ID 1001).

```csharp
public sealed class MakeCredentialCompletedEvent : WebAuthnEvent
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[WebAuthnEvent](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md) ← 
[MakeCredentialCompletedEvent](DSInternals.Win32.WebAuthn.Events.MakeCredentialCompletedEvent.md)

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
  &lt;Data Name="TransactionId"&gt;{005e70a8-ebc3-4a48-a208-f470f2988c3e}&lt;/Data&gt;
&lt;/EventData&gt;</code></pre>

