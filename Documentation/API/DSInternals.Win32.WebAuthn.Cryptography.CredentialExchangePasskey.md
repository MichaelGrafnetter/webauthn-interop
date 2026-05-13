# <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangePasskey"></a> Class CredentialExchangePasskey

Namespace: [DSInternals.Win32.WebAuthn.Cryptography](DSInternals.Win32.WebAuthn.Cryptography.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents a CXF credential of type <code>passkey</code>.

```csharp
public sealed class CredentialExchangePasskey : CredentialExchangeCredential
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CredentialExchangeCredential](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeCredential.md) ← 
[CredentialExchangePasskey](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangePasskey.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangePasskey_CredentialId"></a> CredentialId

The WebAuthn Credential ID associated with the passkey.

```csharp
[JsonPropertyName("credentialId")]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[]? CredentialId { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangePasskey_Fido2Extensions"></a> Fido2Extensions

FIDO2 extension state captured for the passkey.

```csharp
[JsonPropertyName("fido2Extensions")]
public CredentialExchangeFido2Extensions? Fido2Extensions { get; set; }
```

#### Property Value

 [CredentialExchangeFido2Extensions](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeFido2Extensions.md)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangePasskey_Key"></a> Key

The PKCS#8 ASN.1 DER encoded private key of the passkey.

```csharp
[JsonPropertyName("key")]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[]? Key { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangePasskey_RelyingPartyId"></a> RelyingPartyId

The WebAuthn Relying Party Identifier the passkey is bound to.

```csharp
[JsonPropertyName("rpId")]
public string? RelyingPartyId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangePasskey_UserDisplayName"></a> UserDisplayName

A human-palatable display name for the user account associated with the passkey.

```csharp
[JsonPropertyName("userDisplayName")]
public string? UserDisplayName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangePasskey_UserHandle"></a> UserHandle

The WebAuthn user handle bound to the passkey.

```csharp
[JsonPropertyName("userHandle")]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[]? UserHandle { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangePasskey_Username"></a> Username

A human-palatable identifier for the user account associated with the passkey.

```csharp
[JsonPropertyName("username")]
public string? Username { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

## Methods

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangePasskey_LoadPrivateKey"></a> LoadPrivateKey\(\)

Loads the passkey private key as an <xref href="System.Security.Cryptography.AsymmetricAlgorithm" data-throw-if-not-resolved="false"></xref>.
The caller is responsible for disposing the returned key.

```csharp
public AsymmetricAlgorithm LoadPrivateKey()
```

#### Returns

 [AsymmetricAlgorithm](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangePasskey_ToPasskey_System_Nullable_System_Guid__"></a> ToPasskey\(Guid?\)

Converts this CXF passkey credential into a normalized <xref href="DSInternals.Win32.WebAuthn.Cryptography.ExportedPasskey" data-throw-if-not-resolved="false"></xref>.

```csharp
public ExportedPasskey ToPasskey(Guid? aaGuid = null)
```

#### Parameters

`aaGuid` [Guid](https://learn.microsoft.com/dotnet/api/system.guid)?

The AAGUID to associate with the imported passkey. CXF does not include an AAGUID,
so importers may pick one that identifies the new platform authenticator.
Defaults to <xref href="System.Guid.Empty" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [ExportedPasskey](DSInternals.Win32.WebAuthn.Cryptography.ExportedPasskey.md)

