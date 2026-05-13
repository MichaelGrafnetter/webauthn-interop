# <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenCleartextVaultExport"></a> Class BitwardenCleartextVaultExport

Namespace: [DSInternals.Win32.WebAuthn.Cryptography](DSInternals.Win32.WebAuthn.Cryptography.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents the top-level structure of a cleartext Bitwarden vault export.

```csharp
public sealed class BitwardenCleartextVaultExport
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[BitwardenCleartextVaultExport](DSInternals.Win32.WebAuthn.Cryptography.BitwardenCleartextVaultExport.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenCleartextVaultExport_Encrypted"></a> Encrypted

Indicates whether the export content is encrypted.

```csharp
[JsonPropertyName("encrypted")]
public bool Encrypted { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenCleartextVaultExport_Items"></a> Items

The vault items contained in the export.

```csharp
[JsonPropertyName("items")]
public BitwardenVaultItem[]? Items { get; set; }
```

#### Property Value

 [BitwardenVaultItem](DSInternals.Win32.WebAuthn.Cryptography.BitwardenVaultItem.md)\[\]?

## Methods

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenCleartextVaultExport_GetPasskeys"></a> GetPasskeys\(\)

Returns all passkeys contained in this Bitwarden vault export.

```csharp
public IReadOnlyList<ExportedPasskey> GetPasskeys()
```

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[ExportedPasskey](DSInternals.Win32.WebAuthn.Cryptography.ExportedPasskey.md)\>

The passkeys contained in login items.

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenCleartextVaultExport_LoadFromFile_System_String_"></a> LoadFromFile\(string\)

Loads and parses a cleartext Bitwarden JSON vault export.

```csharp
public static BitwardenCleartextVaultExport LoadFromFile(string filePath)
```

#### Parameters

`filePath` [string](https://learn.microsoft.com/dotnet/api/system.string)

Path to the Bitwarden JSON export.

#### Returns

 [BitwardenCleartextVaultExport](DSInternals.Win32.WebAuthn.Cryptography.BitwardenCleartextVaultExport.md)

The parsed cleartext Bitwarden export.

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenCleartextVaultExport_LoadFromJson_System_String_"></a> LoadFromJson\(string\)

Parses a cleartext Bitwarden JSON vault export.

```csharp
public static BitwardenCleartextVaultExport LoadFromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

The Bitwarden JSON export content.

#### Returns

 [BitwardenCleartextVaultExport](DSInternals.Win32.WebAuthn.Cryptography.BitwardenCleartextVaultExport.md)

The parsed cleartext Bitwarden export.

