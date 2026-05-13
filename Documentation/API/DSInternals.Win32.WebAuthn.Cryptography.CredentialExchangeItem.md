# <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeItem"></a> Class CredentialExchangeItem

Namespace: [DSInternals.Win32.WebAuthn.Cryptography](DSInternals.Win32.WebAuthn.Cryptography.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents a single CXF item that may bundle one or more credentials.

```csharp
public sealed class CredentialExchangeItem
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CredentialExchangeItem](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeItem.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeItem_CreatedAt"></a> CreatedAt

The Unix timestamp at which the item was created, in seconds.

```csharp
[JsonPropertyName("creationAt")]
public ulong? CreatedAt { get; set; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeItem_Credentials"></a> Credentials

The credentials bundled inside this item.

```csharp
[JsonPropertyName("credentials")]
public CredentialExchangeCredential[]? Credentials { get; set; }
```

#### Property Value

 [CredentialExchangeCredential](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeCredential.md)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeItem_Favorite"></a> Favorite

Indicates whether the user marked this item as a favorite.

```csharp
[JsonPropertyName("favorite")]
public bool Favorite { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeItem_Id"></a> Id

The probabilistically-unique identifier of the item.

```csharp
[JsonPropertyName("id")]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[]? Id { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeItem_ModifiedAt"></a> ModifiedAt

The Unix timestamp of the last modification of the item, in seconds.

```csharp
[JsonPropertyName("modifiedAt")]
public ulong? ModifiedAt { get; set; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeItem_Subtitle"></a> Subtitle

An optional secondary title for the item.

```csharp
[JsonPropertyName("subtitle")]
public string? Subtitle { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeItem_Tags"></a> Tags

Tags associated with this item.

```csharp
[JsonPropertyName("tags")]
public string[]? Tags { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeItem_Title"></a> Title

The display title of the item.

```csharp
[JsonPropertyName("title")]
public string? Title { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

