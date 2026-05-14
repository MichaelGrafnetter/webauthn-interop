# <a id="DSInternals_Win32_WebAuthn_Okta_OktaWebauthnAttestationResponse"></a> Class OktaWebauthnAttestationResponse

Namespace: [DSInternals.Win32.WebAuthn.Okta](DSInternals.Win32.WebAuthn.Okta.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Okta-specific payload used to submit a WebAuthn attestation result.

```csharp
public class OktaWebauthnAttestationResponse
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[OktaWebauthnAttestationResponse](DSInternals.Win32.WebAuthn.Okta.OktaWebauthnAttestationResponse.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaWebauthnAttestationResponse__ctor_DSInternals_Win32_WebAuthn_AttestationPublicKeyCredential_System_String_System_String_"></a> OktaWebauthnAttestationResponse\(AttestationPublicKeyCredential, string, string\)

Initializes a new Okta attestation response payload.

```csharp
public OktaWebauthnAttestationResponse(AttestationPublicKeyCredential publicKeyCredential, string userId, string factorId)
```

#### Parameters

`publicKeyCredential` [AttestationPublicKeyCredential](DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential.md)

WebAuthn credential returned by the authenticator.

`userId` [string](https://learn.microsoft.com/dotnet/api/system.string)

Okta user identifier (20-character Base64Url string).

`factorId` [string](https://learn.microsoft.com/dotnet/api/system.string)

Okta factor identifier.

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaWebauthnAttestationResponse__ctor_DSInternals_Win32_WebAuthn_AttestationPublicKeyCredential_System_Byte___System_String_"></a> OktaWebauthnAttestationResponse\(AttestationPublicKeyCredential, byte\[\], string\)

Initializes a new Okta attestation response payload from a raw user identifier.

```csharp
public OktaWebauthnAttestationResponse(AttestationPublicKeyCredential publicKeyCredential, byte[] userId, string factorId)
```

#### Parameters

`publicKeyCredential` [AttestationPublicKeyCredential](DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential.md)

WebAuthn credential returned by the authenticator.

`userId` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

Raw Okta user identifier bytes; encoded to a Base64Url string.

`factorId` [string](https://learn.microsoft.com/dotnet/api/system.string)

Okta factor identifier.

## Properties

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaWebauthnAttestationResponse_Attestation"></a> Attestation

Gets or sets the attestation object bytes.

```csharp
[JsonPropertyName("attestation")]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[] Attestation { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaWebauthnAttestationResponse_ClientData"></a> ClientData

Gets or sets the client data JSON bytes.

```csharp
[JsonPropertyName("clientData")]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[]? ClientData { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaWebauthnAttestationResponse_ClientDataJson"></a> ClientDataJson

The clientData value decoded as a UTF-8 string.

```csharp
[JsonIgnore]
public string? ClientDataJson { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaWebauthnAttestationResponse_CredentialId"></a> CredentialId

Gets or sets the credential identifier returned by the authenticator.

```csharp
[JsonIgnore]
public byte[] CredentialId { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaWebauthnAttestationResponse_FactorId"></a> FactorId

Gets or sets the Okta factor identifier.

```csharp
[JsonIgnore]
public string FactorId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaWebauthnAttestationResponse_UserId"></a> UserId

Gets or sets the Okta user identifier.

```csharp
[JsonIgnore]
public string UserId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Methods

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaWebauthnAttestationResponse_ToString"></a> ToString\(\)

Serializes this payload to JSON.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation expected by Okta APIs.

