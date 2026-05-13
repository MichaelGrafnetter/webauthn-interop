# <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenFido2Credential"></a> Class BitwardenFido2Credential

Namespace: [DSInternals.Win32.WebAuthn.Cryptography](DSInternals.Win32.WebAuthn.Cryptography.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents a FIDO2 credential entry embedded in a Bitwarden login item.

```csharp
public sealed class BitwardenFido2Credential
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[BitwardenFido2Credential](DSInternals.Win32.WebAuthn.Cryptography.BitwardenFido2Credential.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenFido2Credential_Counter"></a> Counter

The signature counter stored by Bitwarden.

```csharp
[JsonPropertyName("counter")]
[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString|JsonNumberHandling.WriteAsString)]
public uint Counter { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenFido2Credential_CredentialId"></a> CredentialId

The Bitwarden credential ID value.

```csharp
[JsonPropertyName("credentialId")]
[JsonRequired]
public required string CredentialId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenFido2Credential_Discoverable"></a> Discoverable

Indicates whether Bitwarden marked the credential as discoverable.

```csharp
[JsonPropertyName("discoverable")]
[JsonConverter(typeof(BooleanConverter))]
public bool Discoverable { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenFido2Credential_KeyAlgorithm"></a> KeyAlgorithm

The COSE algorithm recorded by Bitwarden.

```csharp
[JsonPropertyName("keyAlgorithm")]
public string? KeyAlgorithm { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenFido2Credential_KeyCurve"></a> KeyCurve

The key curve recorded by Bitwarden, when applicable.

```csharp
[JsonPropertyName("keyCurve")]
public string? KeyCurve { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenFido2Credential_KeyType"></a> KeyType

The COSE key type recorded by Bitwarden.

```csharp
[JsonPropertyName("keyType")]
public string? KeyType { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenFido2Credential_KeyValue"></a> KeyValue

The private key value decoded from Base64Url PKCS#8 DER.

```csharp
[JsonPropertyName("keyValue")]
[JsonConverter(typeof(Base64UrlConverter))]
[JsonRequired]
public required byte[] KeyValue { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenFido2Credential_RelyingPartyId"></a> RelyingPartyId

The relying party ID.

```csharp
[JsonPropertyName("rpId")]
public string? RelyingPartyId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenFido2Credential_RelyingPartyName"></a> RelyingPartyName

The relying party display name.

```csharp
[JsonPropertyName("rpName")]
public string? RelyingPartyName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenFido2Credential_UserDisplayName"></a> UserDisplayName

The user display name associated with the credential.

```csharp
[JsonPropertyName("userDisplayName")]
public string? UserDisplayName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenFido2Credential_UserHandle"></a> UserHandle

The WebAuthn user handle decoded from Base64Url.

```csharp
[JsonPropertyName("userHandle")]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[]? UserHandle { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenFido2Credential_UserName"></a> UserName

The username associated with the credential.

```csharp
[JsonPropertyName("userName")]
public string? UserName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

## Methods

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenFido2Credential_LoadPrivateKey"></a> LoadPrivateKey\(\)

Loads the Bitwarden private key from this credential as an AsymmetricAlgorithm.
The caller is responsible for disposing the returned key.

```csharp
public AsymmetricAlgorithm LoadPrivateKey()
```

#### Returns

 [AsymmetricAlgorithm](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm)

The imported private key.

### <a id="DSInternals_Win32_WebAuthn_Cryptography_BitwardenFido2Credential_ToPasskey"></a> ToPasskey\(\)

Converts this Bitwarden FIDO2 credential record into a normalized passkey model.

```csharp
public ExportedPasskey ToPasskey()
```

#### Returns

 [ExportedPasskey](DSInternals.Win32.WebAuthn.Cryptography.ExportedPasskey.md)

A normalized exported passkey.

