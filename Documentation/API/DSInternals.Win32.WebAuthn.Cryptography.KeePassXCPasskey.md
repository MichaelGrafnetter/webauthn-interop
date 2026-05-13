# <a id="DSInternals_Win32_WebAuthn_Cryptography_KeePassXCPasskey"></a> Class KeePassXCPasskey

Namespace: [DSInternals.Win32.WebAuthn.Cryptography](DSInternals.Win32.WebAuthn.Cryptography.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents a passkey exported from KeePassXC (.passkey JSON format).

```csharp
public sealed class KeePassXCPasskey
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[KeePassXCPasskey](DSInternals.Win32.WebAuthn.Cryptography.KeePassXCPasskey.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_Cryptography_KeePassXCPasskey_CredentialId"></a> CredentialId

```csharp
[JsonPropertyName("credentialId")]
[JsonConverter(typeof(Base64UrlConverter))]
[JsonRequired]
public required byte[] CredentialId { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_Cryptography_KeePassXCPasskey_PrivateKey"></a> PrivateKey

```csharp
[JsonPropertyName("privateKey")]
[JsonRequired]
public required string PrivateKey { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_KeePassXCPasskey_RelyingParty"></a> RelyingParty

```csharp
[JsonPropertyName("relyingParty")]
public required string RelyingParty { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_KeePassXCPasskey_Url"></a> Url

```csharp
[JsonPropertyName("url")]
public string? Url { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_KeePassXCPasskey_UserHandle"></a> UserHandle

```csharp
[JsonPropertyName("userHandle")]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[]? UserHandle { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_KeePassXCPasskey_Username"></a> Username

```csharp
[JsonPropertyName("username")]
public string? Username { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

## Methods

### <a id="DSInternals_Win32_WebAuthn_Cryptography_KeePassXCPasskey_GetPasskeys"></a> GetPasskeys\(\)

Returns this KeePassXC passkey as a normalized exported passkey collection.

```csharp
public IReadOnlyList<ExportedPasskey> GetPasskeys()
```

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[ExportedPasskey](DSInternals.Win32.WebAuthn.Cryptography.ExportedPasskey.md)\>

### <a id="DSInternals_Win32_WebAuthn_Cryptography_KeePassXCPasskey_LoadFromFile_System_String_"></a> LoadFromFile\(string\)

Loads and deserializes a KeePassXC passkey from a JSON file.

```csharp
public static KeePassXCPasskey LoadFromFile(string filePath)
```

#### Parameters

`filePath` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 [KeePassXCPasskey](DSInternals.Win32.WebAuthn.Cryptography.KeePassXCPasskey.md)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_KeePassXCPasskey_LoadFromJson_System_String_"></a> LoadFromJson\(string\)

Deserializes a KeePassXC passkey from JSON.

```csharp
public static KeePassXCPasskey LoadFromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 [KeePassXCPasskey](DSInternals.Win32.WebAuthn.Cryptography.KeePassXCPasskey.md)

