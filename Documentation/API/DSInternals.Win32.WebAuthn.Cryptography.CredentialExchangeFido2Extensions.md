# <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeFido2Extensions"></a> Class CredentialExchangeFido2Extensions

Namespace: [DSInternals.Win32.WebAuthn.Cryptography](DSInternals.Win32.WebAuthn.Cryptography.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents the FIDO2 extension state recorded for a CXF passkey credential.

```csharp
public sealed class CredentialExchangeFido2Extensions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CredentialExchangeFido2Extensions](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeFido2Extensions.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeFido2Extensions_CredBlob"></a> CredBlob

The credBlob extension data.

```csharp
[JsonPropertyName("credBlob")]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[]? CredBlob { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeFido2Extensions_HmacCredentials"></a> HmacCredentials

The hmac-secret extension state.

```csharp
[JsonPropertyName("hmacCredentials")]
public CredentialExchangeFido2HmacCredentials? HmacCredentials { get; set; }
```

#### Property Value

 [CredentialExchangeFido2HmacCredentials](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeFido2HmacCredentials.md)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeFido2Extensions_LargeBlob"></a> LargeBlob

The largeBlob extension data.

```csharp
[JsonPropertyName("largeBlob")]
public CredentialExchangeFido2LargeBlob? LargeBlob { get; set; }
```

#### Property Value

 [CredentialExchangeFido2LargeBlob](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeFido2LargeBlob.md)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeFido2Extensions_Payments"></a> Payments

Indicates whether the passkey is enabled for the Secure Payment Confirmation extension.

```csharp
[JsonPropertyName("payments")]
public bool? Payments { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

