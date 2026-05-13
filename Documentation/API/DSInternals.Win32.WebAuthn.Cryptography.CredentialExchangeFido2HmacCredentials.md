# <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeFido2HmacCredentials"></a> Class CredentialExchangeFido2HmacCredentials

Namespace: [DSInternals.Win32.WebAuthn.Cryptography](DSInternals.Win32.WebAuthn.Cryptography.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents the FIDO2 hmac-secret extension state captured in a CXF passkey.

```csharp
public sealed class CredentialExchangeFido2HmacCredentials
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CredentialExchangeFido2HmacCredentials](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeFido2HmacCredentials.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeFido2HmacCredentials_Algorithm"></a> Algorithm

The HMAC algorithm identifier (e.g. <code>hmac-sha256</code>).

```csharp
[JsonPropertyName("algorithm")]
public string? Algorithm { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeFido2HmacCredentials_CredentialWithUserVerification"></a> CredentialWithUserVerification

The HMAC credential to use when user verification was performed.

```csharp
[JsonPropertyName("credWithUV")]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[]? CredentialWithUserVerification { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeFido2HmacCredentials_CredentialWithoutUserVerification"></a> CredentialWithoutUserVerification

The HMAC credential to use when user verification was not performed.

```csharp
[JsonPropertyName("credWithoutUV")]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[]? CredentialWithoutUserVerification { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

