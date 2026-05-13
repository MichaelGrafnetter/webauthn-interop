# <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenLogin"></a> Class BitwardenLogin

Namespace: [DSInternals.Win32.WebAuthn.Cryptography](DSInternals.Win32.WebAuthn.Cryptography.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents the login section of a Bitwarden vault item.

```csharp
public sealed class BitwardenLogin
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[BitwardenLogin](DSInternals.Win32.WebAuthn.Cryptography.BitwardenLogin.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenLogin_Fido2Credentials"></a> Fido2Credentials

The FIDO2 credentials stored on the login item.

```csharp
[JsonPropertyName("fido2Credentials")]
public BitwardenFido2Credential[]? Fido2Credentials { get; set; }
```

#### Property Value

 [BitwardenFido2Credential](DSInternals.Win32.WebAuthn.Cryptography.BitwardenFido2Credential.md)\[\]?

