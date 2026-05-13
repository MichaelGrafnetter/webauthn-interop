# <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenEncryptedVaultExport"></a> Class BitwardenEncryptedVaultExport

Namespace: [DSInternals.Win32.WebAuthn.Cryptography](DSInternals.Win32.WebAuthn.Cryptography.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents the top-level structure of an encrypted Bitwarden vault export.

```csharp
public sealed class BitwardenEncryptedVaultExport
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[BitwardenEncryptedVaultExport](DSInternals.Win32.WebAuthn.Cryptography.BitwardenEncryptedVaultExport.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenEncryptedVaultExport_Data"></a> Data

The encrypted cleartext vault export payload.

```csharp
[JsonPropertyName("data")]
public string? Data { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenEncryptedVaultExport_EncKeyValidation"></a> EncKeyValidation

Encrypted validation data used to verify the password-derived key.

```csharp
[JsonPropertyName("encKeyValidation_DO_NOT_EDIT")]
public string? EncKeyValidation { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenEncryptedVaultExport_Encrypted"></a> Encrypted

Indicates whether the export content is encrypted.

```csharp
[JsonPropertyName("encrypted")]
public bool Encrypted { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenEncryptedVaultExport_KdfIterations"></a> KdfIterations

The number of KDF iterations used to derive the export key.

```csharp
[JsonPropertyName("kdfIterations")]
public int KdfIterations { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenEncryptedVaultExport_KdfMemory"></a> KdfMemory

The memory parameter for KDFs that use one.

```csharp
[JsonPropertyName("kdfMemory")]
public int? KdfMemory { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenEncryptedVaultExport_KdfParallelism"></a> KdfParallelism

The parallelism parameter for KDFs that use one.

```csharp
[JsonPropertyName("kdfParallelism")]
public int? KdfParallelism { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenEncryptedVaultExport_KdfType"></a> KdfType

The Bitwarden KDF identifier.

```csharp
[JsonPropertyName("kdfType")]
public int KdfType { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenEncryptedVaultExport_PasswordProtected"></a> PasswordProtected

Indicates whether the export is protected by a user-supplied password.

```csharp
[JsonPropertyName("passwordProtected")]
public bool PasswordProtected { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenEncryptedVaultExport_Salt"></a> Salt

The Base64-encoded salt for Bitwarden's password-based key derivation.
Bitwarden uses the UTF-8 encoding of this string (not the decoded bytes) as the PBKDF2 salt.

```csharp
[JsonPropertyName("salt")]
public string? Salt { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

## Methods

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenEncryptedVaultExport_Decrypt_System_String_"></a> Decrypt\(string\)

Decrypts this encrypted export into a cleartext Bitwarden vault export.

```csharp
public BitwardenCleartextVaultExport Decrypt(string password)
```

#### Parameters

`password` [string](https://learn.microsoft.com/dotnet/api/system.string)

The password used to protect the export.

#### Returns

 [BitwardenCleartextVaultExport](DSInternals.Win32.WebAuthn.Cryptography.BitwardenCleartextVaultExport.md)

The decrypted cleartext Bitwarden export.

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenEncryptedVaultExport_LoadFromFile_System_String_"></a> LoadFromFile\(string\)

Loads and parses an encrypted Bitwarden JSON vault export.

```csharp
public static BitwardenEncryptedVaultExport LoadFromFile(string filePath)
```

#### Parameters

`filePath` [string](https://learn.microsoft.com/dotnet/api/system.string)

Path to the encrypted Bitwarden JSON export.

#### Returns

 [BitwardenEncryptedVaultExport](DSInternals.Win32.WebAuthn.Cryptography.BitwardenEncryptedVaultExport.md)

The parsed encrypted Bitwarden export.

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenEncryptedVaultExport_LoadFromJson_System_String_"></a> LoadFromJson\(string\)

Parses an encrypted Bitwarden JSON vault export.

```csharp
public static BitwardenEncryptedVaultExport LoadFromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

The encrypted Bitwarden JSON export content.

#### Returns

 [BitwardenEncryptedVaultExport](DSInternals.Win32.WebAuthn.Cryptography.BitwardenEncryptedVaultExport.md)

The parsed encrypted Bitwarden export.

