# <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeFido2LargeBlob"></a> Class CredentialExchangeFido2LargeBlob

Namespace: [DSInternals.Win32.WebAuthn.Cryptography](DSInternals.Win32.WebAuthn.Cryptography.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents the FIDO2 largeBlob extension payload stored on a CXF passkey.

```csharp
public sealed class CredentialExchangeFido2LargeBlob
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CredentialExchangeFido2LargeBlob](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeFido2LargeBlob.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeFido2LargeBlob_Data"></a> Data

The DEFLATE-compressed largeBlob data.

```csharp
[JsonPropertyName("data")]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[]? Data { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeFido2LargeBlob_UncompressedSize"></a> UncompressedSize

The size of the uncompressed blob, in bytes.

```csharp
[JsonPropertyName("uncompressedSize")]
public ulong UncompressedSize { get; set; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

