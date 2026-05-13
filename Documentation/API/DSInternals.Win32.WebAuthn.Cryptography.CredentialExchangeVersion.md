# <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeVersion"></a> Class CredentialExchangeVersion

Namespace: [DSInternals.Win32.WebAuthn.Cryptography](DSInternals.Win32.WebAuthn.Cryptography.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents the version of the FIDO Credential Exchange Format payload.

```csharp
public sealed class CredentialExchangeVersion
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CredentialExchangeVersion](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeVersion.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeVersion_Major"></a> Major

The major version of the CXF payload.

```csharp
[JsonPropertyName("major")]
public byte Major { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeVersion_Minor"></a> Minor

The minor version of the CXF payload.

```csharp
[JsonPropertyName("minor")]
public byte Minor { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)

