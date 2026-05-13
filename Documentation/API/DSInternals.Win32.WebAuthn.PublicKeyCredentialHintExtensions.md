# <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialHintExtensions"></a> Class PublicKeyCredentialHintExtensions

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Extension methods for known public key credential hints.

```csharp
public static class PublicKeyCredentialHintExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PublicKeyCredentialHintExtensions](DSInternals.Win32.WebAuthn.PublicKeyCredentialHintExtensions.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialHintExtensions_ToJsonString_DSInternals_Win32_WebAuthn_PublicKeyCredentialHint_"></a> ToJsonString\(PublicKeyCredentialHint\)

Converts a known credential hint enum value to the DOMString value accepted by WebAuthn.

```csharp
public static string? ToJsonString(this PublicKeyCredentialHint hint)
```

#### Parameters

`hint` [PublicKeyCredentialHint](DSInternals.Win32.WebAuthn.PublicKeyCredentialHint.md)

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

