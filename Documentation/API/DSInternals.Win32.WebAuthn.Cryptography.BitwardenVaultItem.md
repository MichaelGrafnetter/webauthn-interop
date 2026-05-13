# <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenVaultItem"></a> Class BitwardenVaultItem

Namespace: [DSInternals.Win32.WebAuthn.Cryptography](DSInternals.Win32.WebAuthn.Cryptography.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents a Bitwarden vault item that may contain login passkeys.

```csharp
public sealed class BitwardenVaultItem
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[BitwardenVaultItem](DSInternals.Win32.WebAuthn.Cryptography.BitwardenVaultItem.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenVaultItem_Login"></a> Login

The login payload for the vault item.

```csharp
[JsonPropertyName("login")]
public BitwardenLogin? Login { get; set; }
```

#### Property Value

 [BitwardenLogin](DSInternals.Win32.WebAuthn.Cryptography.BitwardenLogin.md)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenVaultItem_Name"></a> Name

The display name of the vault item.

```csharp
[JsonPropertyName("name")]
public string? Name { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

