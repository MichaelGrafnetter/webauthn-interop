# <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants"></a> Class ApiConstants

Namespace: [DSInternals.Win32.WebAuthn.Interop](DSInternals.Win32.WebAuthn.Interop.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Contains WebAuthn API constants.

```csharp
public static class ApiConstants
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ApiConstants](DSInternals.Win32.WebAuthn.Interop.ApiConstants.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Fields

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_ClientDataCredentialCreate"></a> ClientDataCredentialCreate

WebAuthn operation type used when creating new FIDO2 credentials.

```csharp
public const string ClientDataCredentialCreate = "webauthn.create"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_ClientDataCredentialCreateU2F"></a> ClientDataCredentialCreateU2F

WebAuthn operation type used when creating new FIDO U2F credentials.

```csharp
public const string ClientDataCredentialCreateU2F = "navigator.id.finishEnrollment"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_ClientDataCredentialGet"></a> ClientDataCredentialGet

WebAuthn operation type used for authentication with FIDO2 credentials.

```csharp
public const string ClientDataCredentialGet = "webauthn.get"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_ClientDataCredentialGetU2F"></a> ClientDataCredentialGetU2F

WebAuthn operation type used for authentication with FIDO U2F credentials.

```csharp
public const string ClientDataCredentialGetU2F = "navigator.id.getAssertion"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_CredentialTypePublicKey"></a> CredentialTypePublicKey

```csharp
public const string CredentialTypePublicKey = "public-key"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_CREDENTIAL_TYPE_PUBLIC_KEY.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_CtapOneHmacSecretLength"></a> CtapOneHmacSecretLength

```csharp
public const uint CtapOneHmacSecretLength = 32
```

#### Field Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

#### Remarks

Corresponds to WEBAUTHN_CTAP_ONE_HMAC_SECRET_LENGTH.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_DefaultTimeoutMilliseconds"></a> DefaultTimeoutMilliseconds

Default timeut for WebAuthn operations.

```csharp
public const int DefaultTimeoutMilliseconds = 60000
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_ExtensionIdentifierCredBlob"></a> ExtensionIdentifierCredBlob

```csharp
public const string ExtensionIdentifierCredBlob = "credBlob"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_EXTENSIONS_IDENTIFIER_CRED_BLOB.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_ExtensionIdentifierCredProtect"></a> ExtensionIdentifierCredProtect

```csharp
public const string ExtensionIdentifierCredProtect = "credProtect"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_EXTENSIONS_IDENTIFIER_CRED_PROTECT.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_ExtensionIdentifierHmacSecret"></a> ExtensionIdentifierHmacSecret

```csharp
public const string ExtensionIdentifierHmacSecret = "hmac-secret"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_EXTENSIONS_IDENTIFIER_HMAC_SECRET.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_ExtensionIdentifierMinPinLength"></a> ExtensionIdentifierMinPinLength

```csharp
public const string ExtensionIdentifierMinPinLength = "minPinLength"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_EXTENSIONS_IDENTIFIER_MIN_PIN_LENGTH.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_HashAlgorithmSha256"></a> HashAlgorithmSha256

```csharp
public const string HashAlgorithmSha256 = "SHA-256"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_HASH_ALGORITHM_SHA_256.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_HashAlgorithmSha384"></a> HashAlgorithmSha384

```csharp
public const string HashAlgorithmSha384 = "SHA-384"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_HASH_ALGORITHM_SHA_384.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_HashAlgorithmSha512"></a> HashAlgorithmSha512

```csharp
public const string HashAlgorithmSha512 = "SHA-512"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_HASH_ALGORITHM_SHA_512.

