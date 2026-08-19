# <a id="DSInternals_Win32_WebAuthn_Interop_NCryptKeyName"></a> Class NCryptKeyName

Namespace: [DSInternals.Win32.WebAuthn.Interop](DSInternals.Win32.WebAuthn.Interop.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Identifies a CNG key returned by <code>NCryptEnumKeys</code>.

```csharp
public sealed class NCryptKeyName
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[NCryptKeyName](DSInternals.Win32.WebAuthn.Interop.NCryptKeyName.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Corresponds to NCryptKeyName.

## Fields

### <a id="DSInternals_Win32_WebAuthn_Interop_NCryptKeyName_AlgorithmGroup"></a> AlgorithmGroup

The identifier of the cryptographic algorithm associated with the key.

```csharp
public string? AlgorithmGroup
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Interop_NCryptKeyName_Flags"></a> Flags

Flags describing the key, e.g. whether it is a machine key.

```csharp
public NCryptKeyNameFlags Flags
```

#### Field Value

 [NCryptKeyNameFlags](DSInternals.Win32.WebAuthn.Interop.NCryptKeyNameFlags.md)

### <a id="DSInternals_Win32_WebAuthn_Interop_NCryptKeyName_LegacyKeySpec"></a> LegacyKeySpec

Legacy CryptoAPI key spec (AT_KEYEXCHANGE or AT_SIGNATURE), or zero for CNG-only keys.

```csharp
public KeyNumber LegacyKeySpec
```

#### Field Value

 [KeyNumber](https://learn.microsoft.com/dotnet/api/system.security.cryptography.keynumber)

### <a id="DSInternals_Win32_WebAuthn_Interop_NCryptKeyName_Name"></a> Name

The name of the key.

```csharp
public string? Name
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

