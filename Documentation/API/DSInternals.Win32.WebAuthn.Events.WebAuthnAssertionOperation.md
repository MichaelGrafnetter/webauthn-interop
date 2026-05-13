# <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnAssertionOperation"></a> Class WebAuthnAssertionOperation

Namespace: [DSInternals.Win32.WebAuthn.Events](DSInternals.Win32.WebAuthn.Events.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Aggregated data about a WebAuthn authentication (GetAssertion) operation,
combining information from multiple related WebAuthN events.

```csharp
public sealed class WebAuthnAssertionOperation : WebAuthnOperation
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[WebAuthnOperation](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md) ← 
[WebAuthnAssertionOperation](DSInternals.Win32.WebAuthn.Events.WebAuthnAssertionOperation.md)

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

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnAssertionOperation_ClientDataHashAlgId"></a> ClientDataHashAlgId

The hash algorithm used for client data (e.g., SHA-256).

```csharp
public string? ClientDataHashAlgId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnAssertionOperation_OperationType"></a> OperationType

Gets the operation type.

```csharp
public override string OperationType { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

