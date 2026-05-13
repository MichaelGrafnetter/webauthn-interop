# <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation"></a> Class WebAuthnOperation

Namespace: [DSInternals.Win32.WebAuthn.Events](DSInternals.Win32.WebAuthn.Events.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Base class for aggregated WebAuthn operations, combining data from multiple related events
grouped by transaction ID.

```csharp
public abstract class WebAuthnOperation
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[WebAuthnOperation](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md)

#### Derived

[WebAuthnAssertionOperation](DSInternals.Win32.WebAuthn.Events.WebAuthnAssertionOperation.md), 
[WebAuthnAttestationOperation](DSInternals.Win32.WebAuthn.Events.WebAuthnAttestationOperation.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation_AAGuid"></a> AAGuid

Authenticator Attestation GUID identifying the authenticator model.

```csharp
public Guid? AAGuid { get; set; }
```

#### Property Value

 [Guid](https://learn.microsoft.com/dotnet/api/system.guid)?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation_AttestationData"></a> AttestationData

Attested Credential Data (AT) flag - authenticator data includes attested credential data.

```csharp
public bool? AttestationData { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation_CredentialCount"></a> CredentialCount

Number of credentials involved in the request.

```csharp
public int? CredentialCount { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation_CredentialId"></a> CredentialId

The credential ID.

```csharp
public byte[]? CredentialId { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation_DevicePath"></a> DevicePath

The device path of the authenticator.

```csharp
public string? DevicePath { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation_ExtensionData"></a> ExtensionData

Extension Data (ED) flag - authenticator data includes extension data.

```csharp
public bool? ExtensionData { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation_Flags"></a> Flags

Authenticator data flags (raw byte).

```csharp
public AuthenticatorFlags? Flags { get; set; }
```

#### Property Value

 [AuthenticatorFlags](DSInternals.Win32.WebAuthn.AuthenticatorFlags.md)?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation_Manufacturer"></a> Manufacturer

The manufacturer of the authenticator device.

```csharp
public string? Manufacturer { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation_OperationType"></a> OperationType

The type of operation: Attestation (registration) or Assertion (authentication).

```csharp
public abstract string OperationType { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation_ProcessId"></a> ProcessId

The process ID that initiated the WebAuthN operation (from the service).

```csharp
public int ProcessId { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation_Product"></a> Product

The product name of the authenticator device.

```csharp
public string? Product { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation_ProductId"></a> ProductId

USB Product ID parsed from the device path.

```csharp
public int? ProductId { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation_ProviderName"></a> ProviderName

The name of the CTAP provider (e.g., MicrosoftPlatformProvider).

```csharp
public string? ProviderName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation_RpId"></a> RpId

The relying party ID.

```csharp
public string? RpId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation_RpIdHash"></a> RpIdHash

SHA-256 hash of the relying party ID.

```csharp
public byte[]? RpIdHash { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation_SignCount"></a> SignCount

Signature counter.

```csharp
public uint? SignCount { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation_TimeCompleted"></a> TimeCompleted

Timestamp of the last event with this correlation/transaction ID.

```csharp
public DateTime? TimeCompleted { get; set; }
```

#### Property Value

 [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation_TimeStarted"></a> TimeStarted

When the operation started.

```csharp
public DateTime? TimeStarted { get; set; }
```

#### Property Value

 [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation_TransactionId"></a> TransactionId

The transaction ID correlating all events in this operation.

```csharp
public Guid TransactionId { get; set; }
```

#### Property Value

 [Guid](https://learn.microsoft.com/dotnet/api/system.guid)

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation_U2fProtocol"></a> U2fProtocol

Whether the authenticator uses the U2F protocol.

```csharp
public bool? U2fProtocol { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation_UserName"></a> UserName

The user name associated with the operation.
Populated from the CTAP2 CBOR request for registration; null for authentication.

```csharp
public string? UserName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation_UserPresent"></a> UserPresent

User Present (UP) flag - user presence test completed successfully.

```csharp
public bool? UserPresent { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation_UserVerified"></a> UserVerified

User Verified (UV) flag - user verification completed successfully.

```csharp
public bool? UserVerified { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperation_VendorId"></a> VendorId

USB Vendor ID parsed from the device path (e.g., 0x1050 for Yubico).

```csharp
public int? VendorId { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)?

