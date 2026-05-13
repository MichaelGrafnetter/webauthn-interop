# <a id="DSInternals_Win32_WebAuthn_Events_NgcMakeCredentialResponseEvent"></a> Class NgcMakeCredentialResponseEvent

Namespace: [DSInternals.Win32.WebAuthn.Events](DSInternals.Win32.WebAuthn.Events.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents an NGC MakeCredential response event (Event ID 1041).

```csharp
public sealed class NgcMakeCredentialResponseEvent : MakeCredentialResponseEvent
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[WebAuthnEvent](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md) ← 
[MakeCredentialResponseEvent](DSInternals.Win32.WebAuthn.Events.MakeCredentialResponseEvent.md) ← 
[NgcMakeCredentialResponseEvent](DSInternals.Win32.WebAuthn.Events.NgcMakeCredentialResponseEvent.md)

#### Inherited Members

[MakeCredentialResponseEvent.AttestationFormatType](DSInternals.Win32.WebAuthn.Events.MakeCredentialResponseEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_MakeCredentialResponseEvent\_AttestationFormatType), 
[MakeCredentialResponseEvent.RpIdHash](DSInternals.Win32.WebAuthn.Events.MakeCredentialResponseEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_MakeCredentialResponseEvent\_RpIdHash), 
[MakeCredentialResponseEvent.AuthenticatorFlags](DSInternals.Win32.WebAuthn.Events.MakeCredentialResponseEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_MakeCredentialResponseEvent\_AuthenticatorFlags), 
[MakeCredentialResponseEvent.SignCount](DSInternals.Win32.WebAuthn.Events.MakeCredentialResponseEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_MakeCredentialResponseEvent\_SignCount), 
[MakeCredentialResponseEvent.AAGuid](DSInternals.Win32.WebAuthn.Events.MakeCredentialResponseEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_MakeCredentialResponseEvent\_AAGuid), 
[MakeCredentialResponseEvent.CredentialId](DSInternals.Win32.WebAuthn.Events.MakeCredentialResponseEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_MakeCredentialResponseEvent\_CredentialId), 
[MakeCredentialResponseEvent.U2fPublicKey](DSInternals.Win32.WebAuthn.Events.MakeCredentialResponseEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_MakeCredentialResponseEvent\_U2fPublicKey), 
[MakeCredentialResponseEvent.PublicKey](DSInternals.Win32.WebAuthn.Events.MakeCredentialResponseEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_MakeCredentialResponseEvent\_PublicKey), 
[MakeCredentialResponseEvent.Response](DSInternals.Win32.WebAuthn.Events.MakeCredentialResponseEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_MakeCredentialResponseEvent\_Response), 
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
  &lt;Data Name="AttestationFormatType"&gt;packed&lt;/Data&gt;
  &lt;Data Name="RpIdHashLength"&gt;32&lt;/Data&gt;
  &lt;Data Name="RpIdHash"&gt;49960DE5880E8C68...&lt;/Data&gt;
  &lt;Data Name="Flags"&gt;0x45&lt;/Data&gt;
  &lt;Data Name="SignCount"&gt;0x0&lt;/Data&gt;
  &lt;Data Name="AAGuid"&gt;{de1e552d-db1d-4423-a439-09523d6f04a0}&lt;/Data&gt;
  &lt;Data Name="CredentialIdLength"&gt;32&lt;/Data&gt;
  &lt;Data Name="CredentialId"&gt;F2B3A1D4E5C67890...&lt;/Data&gt;
  &lt;Data Name="U2fPublicKey"&gt;&lt;/Data&gt;
  &lt;Data Name="PublicKeyLength"&gt;77&lt;/Data&gt;
  &lt;Data Name="PublicKey"&gt;A5010203262001...&lt;/Data&gt;
  &lt;Data Name="ResponseLength"&gt;350&lt;/Data&gt;
  &lt;Data Name="Response"&gt;A301667061636B65...&lt;/Data&gt;
&lt;/EventData&gt;</code></pre>

