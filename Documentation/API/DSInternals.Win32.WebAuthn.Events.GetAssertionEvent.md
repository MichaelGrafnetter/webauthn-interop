# <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionEvent"></a> Class GetAssertionEvent

Namespace: [DSInternals.Win32.WebAuthn.Events](DSInternals.Win32.WebAuthn.Events.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents a GetAssertion CTAP command completed event (Event ID 2102).

```csharp
public sealed class GetAssertionEvent : CtapCommandCompletedEvent
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[WebAuthnEvent](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md) ← 
[CtapCommandCompletedEvent](DSInternals.Win32.WebAuthn.Events.CtapCommandCompletedEvent.md) ← 
[GetAssertionEvent](DSInternals.Win32.WebAuthn.Events.GetAssertionEvent.md)

#### Inherited Members

[CtapCommandCompletedEvent.Command](DSInternals.Win32.WebAuthn.Events.CtapCommandCompletedEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_CtapCommandCompletedEvent\_Command), 
[CtapCommandCompletedEvent.Response](DSInternals.Win32.WebAuthn.Events.CtapCommandCompletedEvent.md\#DSInternals\_Win32\_WebAuthn\_Events\_CtapCommandCompletedEvent\_Response), 
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

### <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionEvent_AAGuid"></a> AAGuid

Authenticator Attestation GUID identifying the authenticator model.

```csharp
public Guid? AAGuid { get; set; }
```

#### Property Value

 [Guid](https://learn.microsoft.com/dotnet/api/system.guid)?

### <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionEvent_AuthenticatorFlags"></a> AuthenticatorFlags

Authenticator data flags from shared authenticator data.

```csharp
public AuthenticatorFlags? AuthenticatorFlags { get; set; }
```

#### Property Value

 [AuthenticatorFlags](DSInternals.Win32.WebAuthn.AuthenticatorFlags.md)?

### <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionEvent_Credentials"></a> Credentials

Credentials returned by the CTAP GetAssertion response.

```csharp
public IReadOnlyList<CredentialDetails>? Credentials { get; set; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[CredentialDetails](DSInternals.Win32.WebAuthn.CredentialDetails.md)\>?

### <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionEvent_CtapStatus"></a> CtapStatus

CTAP status byte from the nested authenticator response.

```csharp
public int? CtapStatus { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)?

### <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionEvent_Manufacturer"></a> Manufacturer

The manufacturer of the authenticator device.

```csharp
public string? Manufacturer { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionEvent_Product"></a> Product

The product name of the authenticator device.

```csharp
public string? Product { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionEvent_ProviderName"></a> ProviderName

The name of the CTAP provider.

```csharp
public string? ProviderName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionEvent_ProviderType"></a> ProviderType

The CTAP provider type, such as Hid.

```csharp
public string? ProviderType { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionEvent_RpIdHash"></a> RpIdHash

SHA-256 hash of the relying party ID from shared authenticator data.

```csharp
public byte[]? RpIdHash { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionEvent_Status"></a> Status

Status value from the platform response wrapper.

```csharp
public int? Status { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)?

### <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionEvent_ThirdPartyPayment"></a> ThirdPartyPayment

Indicates whether the assertion is for a third-party payment credential.

```csharp
public bool? ThirdPartyPayment { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="DSInternals_Win32_WebAuthn_Events_GetAssertionEvent_Transports"></a> Transports

Applicable authenticator transports.

```csharp
public AuthenticatorTransport? Transports { get; set; }
```

#### Property Value

 [AuthenticatorTransport](DSInternals.Win32.WebAuthn.AuthenticatorTransport.md)?

