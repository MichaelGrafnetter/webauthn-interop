# <a id="DSInternals_Win32_WebAuthn_AuthenticatorAttestationResponse"></a> Class AuthenticatorAttestationResponse

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents an authenticator attestation response.

```csharp
public class AuthenticatorAttestationResponse : AuthenticatorResponse
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AuthenticatorResponse](DSInternals.Win32.WebAuthn.AuthenticatorResponse.md) ← 
[AuthenticatorAttestationResponse](DSInternals.Win32.WebAuthn.AuthenticatorAttestationResponse.md)

#### Inherited Members

[AuthenticatorResponse.ClientData](DSInternals.Win32.WebAuthn.AuthenticatorResponse.md\#DSInternals\_Win32\_WebAuthn\_AuthenticatorResponse\_ClientData), 
[AuthenticatorResponse.ClientDataJson](DSInternals.Win32.WebAuthn.AuthenticatorResponse.md\#DSInternals\_Win32\_WebAuthn\_AuthenticatorResponse\_ClientDataJson), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorAttestationResponse_AttestationObject"></a> AttestationObject

The attestation object returned by the authenticator (Base64Url encoded).

```csharp
[JsonPropertyName("attestationObject")]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[] AttestationObject { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorAttestationResponse_AuthenticatorData"></a> AuthenticatorData

The authenticator data if provided separately (Base64Url encoded).

```csharp
[JsonPropertyName("authenticatorData")]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[]? AuthenticatorData { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorAttestationResponse_AuthenticatorDataParsed"></a> AuthenticatorDataParsed

The parsed authenticator data structure.

```csharp
[JsonIgnore]
public AuthenticatorData? AuthenticatorDataParsed { get; }
```

#### Property Value

 [AuthenticatorData](DSInternals.Win32.WebAuthn.FIDO.AuthenticatorData.md)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorAttestationResponse_PublicKey"></a> PublicKey

Optional public key (Base64Url encoded).

```csharp
[JsonPropertyName("publicKey")]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[]? PublicKey { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorAttestationResponse_PublicKeyAlgorithm"></a> PublicKeyAlgorithm

Optional public key algorithm identifier.

```csharp
[JsonPropertyName("publicKeyAlgorithm")]
public int? PublicKeyAlgorithm { get; set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorAttestationResponse_Transports"></a> Transports

Optional transports hint provided by the authenticator.

```csharp
[JsonPropertyName("transports")]
public string[]? Transports { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]?

## Methods

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorAttestationResponse_FromJson_System_String_"></a> FromJson\(string\)

```csharp
public static AuthenticatorAttestationResponse? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 [AuthenticatorAttestationResponse](DSInternals.Win32.WebAuthn.AuthenticatorAttestationResponse.md)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorAttestationResponse_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

