# <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenVaultExportHeader"></a> Class BitwardenVaultExportHeader

Namespace: [DSInternals.Win32.WebAuthn.Cryptography](DSInternals.Win32.WebAuthn.Cryptography.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents the minimal Bitwarden export header used to choose a parser.

```csharp
public sealed class BitwardenVaultExportHeader
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[BitwardenVaultExportHeader](DSInternals.Win32.WebAuthn.Cryptography.BitwardenVaultExportHeader.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenVaultExportHeader_Encrypted"></a> Encrypted

Indicates whether the export content is encrypted.

```csharp
[JsonPropertyName("encrypted")]
public bool Encrypted { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenVaultExportHeader_PasswordProtected"></a> PasswordProtected

Indicates whether the export is protected by a user-supplied password.

```csharp
[JsonPropertyName("passwordProtected")]
public bool PasswordProtected { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenVaultExportHeader_LoadFromFileAsync_System_String_System_Threading_CancellationToken_"></a> LoadFromFileAsync\(string, CancellationToken\)

Asynchronously loads and parses a Bitwarden JSON vault export header.

```csharp
public static Task<BitwardenVaultExportHeader> LoadFromFileAsync(string filePath, CancellationToken cancellationToken = default)
```

#### Parameters

`filePath` [string](https://learn.microsoft.com/dotnet/api/system.string)

Path to the Bitwarden JSON export.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

A token that may be used to cancel the read operation.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[BitwardenVaultExportHeader](DSInternals.Win32.WebAuthn.Cryptography.BitwardenVaultExportHeader.md)\>

The parsed Bitwarden export header.

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenVaultExportHeader_LoadFromJson_System_String_"></a> LoadFromJson\(string\)

Parses a Bitwarden JSON vault export header.

```csharp
public static BitwardenVaultExportHeader LoadFromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

The Bitwarden JSON export content.

#### Returns

 [BitwardenVaultExportHeader](DSInternals.Win32.WebAuthn.Cryptography.BitwardenVaultExportHeader.md)

The parsed Bitwarden export header.

