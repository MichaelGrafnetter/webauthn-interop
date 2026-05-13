# <a id="DSInternals_Win32_WebAuthn_MediationExtensions"></a> Class MediationExtensions

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Extension methods for known credential mediation requirements.

```csharp
public static class MediationExtensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[MediationExtensions](DSInternals.Win32.WebAuthn.MediationExtensions.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="DSInternals_Win32_WebAuthn_MediationExtensions_ToJsonString_DSInternals_Win32_WebAuthn_Mediation_"></a> ToJsonString\(Mediation\)

Converts a known mediation enum value to the DOMString value accepted by the Credential Management API.

```csharp
public static string? ToJsonString(this Mediation mediation)
```

#### Parameters

`mediation` [Mediation](DSInternals.Win32.WebAuthn.Mediation.md)

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

