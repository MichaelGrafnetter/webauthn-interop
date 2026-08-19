# <a id="DSInternals_Win32_WebAuthn_Events_CredentialEnumeration"></a> Class CredentialEnumeration

Namespace: [DSInternals.Win32.WebAuthn.Events](DSInternals.Win32.WebAuthn.Events.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Aggregated data about one credential discovered during platform or plugin credential enumeration.

```csharp
public sealed class CredentialEnumeration : WebAuthnOperation
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[WebAuthnOperation](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md) ← 
[CredentialEnumeration](DSInternals.Win32.WebAuthn.Events.CredentialEnumeration.md)

#### Inherited Members

[WebAuthnOperation.OperationType](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnOperation\_OperationType), 
[WebAuthnOperation.TransactionId](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnOperation\_TransactionId), 
[WebAuthnOperation.TimeStarted](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnOperation\_TimeStarted), 
[WebAuthnOperation.TimeCompleted](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnOperation\_TimeCompleted), 
[WebAuthnOperation.ProcessId](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnOperation\_ProcessId), 
[WebAuthnOperation.RpId](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnOperation\_RpId), 
[WebAuthnOperation.RpIdHash](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnOperation\_RpIdHash), 
[WebAuthnOperation.Flags](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnOperation\_Flags), 
[WebAuthnOperation.UserPresent](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnOperation\_UserPresent), 
[WebAuthnOperation.UserVerified](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnOperation\_UserVerified), 
[WebAuthnOperation.AttestationData](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnOperation\_AttestationData), 
[WebAuthnOperation.ExtensionData](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnOperation\_ExtensionData), 
[WebAuthnOperation.SignCount](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnOperation\_SignCount), 
[WebAuthnOperation.CredentialId](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnOperation\_CredentialId), 
[WebAuthnOperation.CredentialCount](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnOperation\_CredentialCount), 
[WebAuthnOperation.AAGuid](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnOperation\_AAGuid), 
[WebAuthnOperation.ProviderName](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnOperation\_ProviderName), 
[WebAuthnOperation.DevicePath](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnOperation\_DevicePath), 
[WebAuthnOperation.Manufacturer](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnOperation\_Manufacturer), 
[WebAuthnOperation.Product](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnOperation\_Product), 
[WebAuthnOperation.U2fProtocol](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnOperation\_U2fProtocol), 
[WebAuthnOperation.VendorId](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnOperation\_VendorId), 
[WebAuthnOperation.ProductId](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnOperation\_ProductId), 
[WebAuthnOperation.UserName](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md\#DSInternals\_Win32\_WebAuthn\_Events\_WebAuthnOperation\_UserName), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_Events_CredentialEnumeration_AuthenticatorLogo"></a> AuthenticatorLogo

The authenticator logo reported for the credential.

```csharp
public string? AuthenticatorLogo { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_CredentialEnumeration_AuthenticatorName"></a> AuthenticatorName

The authenticator name reported for the credential.

```csharp
public string? AuthenticatorName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_CredentialEnumeration_BackedUp"></a> BackedUp

Indicates whether the credential is backed up.

```csharp
public bool? BackedUp { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="DSInternals_Win32_WebAuthn_Events_CredentialEnumeration_FilterHybridTransport"></a> FilterHybridTransport

Whether hybrid transport credentials were filtered from the enumeration.

```csharp
public bool? FilterHybridTransport { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="DSInternals_Win32_WebAuthn_Events_CredentialEnumeration_OperationType"></a> OperationType

Gets the operation type.

```csharp
public override string OperationType { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Events_CredentialEnumeration_PluginClassId"></a> PluginClassId

The plugin authenticator COM class ID.

```csharp
public Guid? PluginClassId { get; set; }
```

#### Property Value

 [Guid](https://learn.microsoft.com/dotnet/api/system.guid)?

### <a id="DSInternals_Win32_WebAuthn_Events_CredentialEnumeration_RelyingPartyName"></a> RelyingPartyName

The relying party display name.

```csharp
public string? RelyingPartyName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_CredentialEnumeration_Removable"></a> Removable

Indicates whether the credential is removable.

```csharp
public bool? Removable { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="DSInternals_Win32_WebAuthn_Events_CredentialEnumeration_ThirdPartyPayment"></a> ThirdPartyPayment

Indicates whether the credential is a third-party payment credential.

```csharp
public bool? ThirdPartyPayment { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="DSInternals_Win32_WebAuthn_Events_CredentialEnumeration_Transports"></a> Transports

The transports reported for the credential.

```csharp
public AuthenticatorTransport? Transports { get; set; }
```

#### Property Value

 [AuthenticatorTransport](DSInternals.Win32.WebAuthn.AuthenticatorTransport.md)?

### <a id="DSInternals_Win32_WebAuthn_Events_CredentialEnumeration_UserDisplayName"></a> UserDisplayName

The user display name associated with the credential.

```csharp
public string? UserDisplayName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_CredentialEnumeration_UserId"></a> UserId

The user ID associated with the credential.

```csharp
public byte[]? UserId { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

