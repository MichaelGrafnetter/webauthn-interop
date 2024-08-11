# <a id="DSInternals_Win32_WebAuthn_Interop_HybridStorageLinkedData"></a> Class HybridStorageLinkedData

Namespace: [DSInternals.Win32.WebAuthn.Interop](DSInternals.Win32.WebAuthn.Interop.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Information about linked devices

```csharp
public sealed class HybridStorageLinkedData : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[HybridStorageLinkedData](DSInternals.Win32.WebAuthn.Interop.HybridStorageLinkedData.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Corresponds to CTAPCBOR_HYBRID_STORAGE_LINKED_DATA.

## Properties

### <a id="DSInternals_Win32_WebAuthn_Interop_HybridStorageLinkedData_AuthenticatorName"></a> AuthenticatorName

Authenticator Name

```csharp
public string AuthenticatorName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Interop_HybridStorageLinkedData_ContactId"></a> ContactId

Contact Id

```csharp
public byte[] ContactId { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_Interop_HybridStorageLinkedData_EncodedTunnelServerDomain"></a> EncodedTunnelServerDomain

Tunnel server domain

```csharp
public short EncodedTunnelServerDomain { get; set; }
```

#### Property Value

 [short](https://learn.microsoft.com/dotnet/api/system.int16)

### <a id="DSInternals_Win32_WebAuthn_Interop_HybridStorageLinkedData_LinkId"></a> LinkId

Link Id

```csharp
public byte[] LinkId { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_Interop_HybridStorageLinkedData_LinkSecret"></a> LinkSecret

Link secret

```csharp
public byte[] LinkSecret { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_Interop_HybridStorageLinkedData_PublicKey"></a> PublicKey

Authenticator Public Key

```csharp
public byte[] PublicKey { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

## Methods

### <a id="DSInternals_Win32_WebAuthn_Interop_HybridStorageLinkedData_Dispose"></a> Dispose\(\)

Deallocates unmanaged resources.

```csharp
public void Dispose()
```

