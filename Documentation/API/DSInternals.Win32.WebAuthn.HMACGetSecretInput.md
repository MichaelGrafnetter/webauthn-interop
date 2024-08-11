# <a id="DSInternals_Win32_WebAuthn_HMACGetSecretInput"></a> Class HMACGetSecretInput

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

The inputs to the hmac secret if it was created during registration.

```csharp
public class HMACGetSecretInput
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[HMACGetSecretInput](DSInternals.Win32.WebAuthn.HMACGetSecretInput.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_HMACGetSecretInput_Salt1"></a> Salt1

32-byte random data.

```csharp
[JsonPropertyName("salt1")]
public byte[] Salt1 { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_HMACGetSecretInput_Salt2"></a> Salt2

Optional additional 32-byte random data. Used when the platform wants to roll over the symmetric secret in one operation.

```csharp
[JsonPropertyName("salt2")]
public byte[] Salt2 { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

