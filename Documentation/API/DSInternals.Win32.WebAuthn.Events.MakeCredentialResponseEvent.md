# <a id="DSInternals_Win32_WebAuthn_Events_MakeCredentialResponseEvent"></a> Class MakeCredentialResponseEvent

Namespace: [DSInternals.Win32.WebAuthn.Events](DSInternals.Win32.WebAuthn.Events.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Base class for MakeCredential response events (<xref href="DSInternals.Win32.WebAuthn.Events.CborMakeCredentialResponseEvent" data-throw-if-not-resolved="false"></xref> and <xref href="DSInternals.Win32.WebAuthn.Events.NgcMakeCredentialResponseEvent" data-throw-if-not-resolved="false"></xref>).

```csharp
public abstract class MakeCredentialResponseEvent : WebAuthnEvent
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[WebAuthnEvent](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md) ← 
[MakeCredentialResponseEvent](DSInternals.Win32.WebAuthn.Events.MakeCredentialResponseEvent.md)

#### Derived

[CborMakeCredentialResponseEvent](DSInternals.Win32.WebAuthn.Events.CborMakeCredentialResponseEvent.md), 
[NgcMakeCredentialResponseEvent](DSInternals.Win32.WebAuthn.Events.NgcMakeCredentialResponseEvent.md)

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
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_Events_MakeCredentialResponseEvent_AAGuid"></a> AAGuid

Authenticator Attestation GUID identifying the authenticator model.

```csharp
public Guid? AAGuid { get; set; }
```

#### Property Value

 [Guid](https://learn.microsoft.com/dotnet/api/system.guid)?

### <a id="DSInternals_Win32_WebAuthn_Events_MakeCredentialResponseEvent_AttestationFormatType"></a> AttestationFormatType

The attestation format type (e.g., "none", "packed", "tpm").

```csharp
public string? AttestationFormatType { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_MakeCredentialResponseEvent_AuthenticatorFlags"></a> AuthenticatorFlags

Authenticator data flags byte (UP, UV, AT, ED, etc.).

```csharp
public AuthenticatorFlags? AuthenticatorFlags { get; set; }
```

#### Property Value

 [AuthenticatorFlags](DSInternals.Win32.WebAuthn.AuthenticatorFlags.md)?

### <a id="DSInternals_Win32_WebAuthn_Events_MakeCredentialResponseEvent_CredentialId"></a> CredentialId

The credential ID of the newly created credential.

```csharp
public byte[]? CredentialId { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Events_MakeCredentialResponseEvent_PublicKey"></a> PublicKey

The COSE-encoded public key of the newly created credential.

```csharp
public byte[]? PublicKey { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Events_MakeCredentialResponseEvent_Response"></a> Response

The raw CBOR-encoded response blob.

```csharp
public byte[]? Response { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Events_MakeCredentialResponseEvent_RpIdHash"></a> RpIdHash

SHA-256 hash of the relying party ID.

```csharp
public byte[]? RpIdHash { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Events_MakeCredentialResponseEvent_SignCount"></a> SignCount

Signature counter value.

```csharp
public uint? SignCount { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)?

### <a id="DSInternals_Win32_WebAuthn_Events_MakeCredentialResponseEvent_U2fPublicKey"></a> U2fPublicKey

The U2F public key, if the authenticator uses the legacy U2F protocol.

```csharp
public byte[]? U2fPublicKey { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

