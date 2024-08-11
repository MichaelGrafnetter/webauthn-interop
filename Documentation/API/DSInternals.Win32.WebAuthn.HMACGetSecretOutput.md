# <a id="DSInternals_Win32_WebAuthn_HMACGetSecretOutput"></a> Class HMACGetSecretOutput

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

The response to a hmac get secret request.

```csharp
public class HMACGetSecretOutput
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[HMACGetSecretOutput](DSInternals.Win32.WebAuthn.HMACGetSecretOutput.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_HMACGetSecretOutput_Output1"></a> Output1

Output of HMAC-SHA-256(CredRandom, Salt1)

```csharp
public byte[] Output1 { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_HMACGetSecretOutput_Output2"></a> Output2

Output of HMAC-SHA-256(CredRandom, Salt2)

```csharp
public byte[] Output2 { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

