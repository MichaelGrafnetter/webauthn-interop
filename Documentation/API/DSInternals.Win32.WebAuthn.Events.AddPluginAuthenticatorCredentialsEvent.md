# <a id="DSInternals_Win32_WebAuthn_Events_AddPluginAuthenticatorCredentialsEvent"></a> Class AddPluginAuthenticatorCredentialsEvent

Namespace: [DSInternals.Win32.WebAuthn.Events](DSInternals.Win32.WebAuthn.Events.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents an AddPluginAuthenticatorCredentials CTAP command started event (Event ID 2100).

```csharp
public sealed class AddPluginAuthenticatorCredentialsEvent : CtapCommandStartedEvent
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[WebAuthnEvent](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md) ← 
[CtapCommandStartedEvent](DSInternals.Win32.WebAuthn.Events.CtapCommandStartedEvent.md) ← 
[AddPluginAuthenticatorCredentialsEvent](DSInternals.Win32.WebAuthn.Events.AddPluginAuthenticatorCredentialsEvent.md)

#### Inherited Members

[CtapCommandStartedEvent.Command](DSInternals.Win32.WebAuthn.Events.CtapCommandStartedEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_CtapCommandStartedEvent\_Command), 
[CtapCommandStartedEvent.Flags](DSInternals.Win32.WebAuthn.Events.CtapCommandStartedEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_CtapCommandStartedEvent\_Flags), 
[CtapCommandStartedEvent.TimeoutMilliseconds](DSInternals.Win32.WebAuthn.Events.CtapCommandStartedEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_CtapCommandStartedEvent\_TimeoutMilliseconds), 
[CtapCommandStartedEvent.Ticket](DSInternals.Win32.WebAuthn.Events.CtapCommandStartedEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_CtapCommandStartedEvent\_Ticket), 
[CtapCommandStartedEvent.Request](DSInternals.Win32.WebAuthn.Events.CtapCommandStartedEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_CtapCommandStartedEvent\_Request), 
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

## Properties

### <a id="DSInternals_Win32_WebAuthn_Events_AddPluginAuthenticatorCredentialsEvent_Credentials"></a> Credentials

Credentials included in the plugin authenticator add request.

```csharp
public IReadOnlyList<CredentialDetails>? Credentials { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[CredentialDetails](DSInternals.Win32.WebAuthn.CredentialDetails.md)\>?

### <a id="DSInternals_Win32_WebAuthn_Events_AddPluginAuthenticatorCredentialsEvent_FilterHybridTransport"></a> FilterHybridTransport

Whether hybrid transport credentials are filtered.

```csharp
public bool? FilterHybridTransport { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="DSInternals_Win32_WebAuthn_Events_AddPluginAuthenticatorCredentialsEvent_PluginClassId"></a> PluginClassId

The plugin authenticator COM class ID.

```csharp
public Guid? PluginClassId { get; set; }
```

#### Property Value

 [Guid](https://learn.microsoft.com/dotnet/api/system.guid)?

