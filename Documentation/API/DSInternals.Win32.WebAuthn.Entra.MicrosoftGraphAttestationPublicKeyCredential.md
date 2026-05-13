# <a id="DSInternals_Win32_WebAuthn_Entra_MicrosoftGraphAttestationPublicKeyCredential"></a> Class MicrosoftGraphAttestationPublicKeyCredential

Namespace: [DSInternals.Win32.WebAuthn.Entra](DSInternals.Win32.WebAuthn.Entra.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Microsoft Graph variant of the attestation public key credential.
Only carries the fields accepted by the fido2AuthenticationMethod schema
(id, response, clientExtensionResults); the standard WebAuthn type,
rawId, and authenticatorAttachment fields are intentionally omitted.

```csharp
public sealed class MicrosoftGraphAttestationPublicKeyCredential
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[MicrosoftGraphAttestationPublicKeyCredential](DSInternals.Win32.WebAuthn.Entra.MicrosoftGraphAttestationPublicKeyCredential.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="DSInternals_Win32_WebAuthn_Entra_MicrosoftGraphAttestationPublicKeyCredential__ctor_System_Byte___DSInternals_Win32_WebAuthn_AuthenticatorAttestationResponse_DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationOutputs_"></a> MicrosoftGraphAttestationPublicKeyCredential\(byte\[\], AuthenticatorAttestationResponse, AuthenticationExtensionsClientAttestationOutputs?\)

Initializes a new Microsoft Graph attestation public key credential.
Used by the JSON deserializer.

```csharp
[JsonConstructor]
[SetsRequiredMembers]
public MicrosoftGraphAttestationPublicKeyCredential(byte[] id, AuthenticatorAttestationResponse response, AuthenticationExtensionsClientAttestationOutputs? clientExtensionResults)
```

#### Parameters

`id` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

Credential identifier.

`response` [AuthenticatorAttestationResponse](DSInternals.Win32.WebAuthn.AuthenticatorAttestationResponse.md)

Authenticator attestation response.

`clientExtensionResults` [AuthenticationExtensionsClientAttestationOutputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAttestationOutputs.md)?

Outputs of client extension processing.

### <a id="DSInternals_Win32_WebAuthn_Entra_MicrosoftGraphAttestationPublicKeyCredential__ctor_DSInternals_Win32_WebAuthn_AttestationPublicKeyCredential_"></a> MicrosoftGraphAttestationPublicKeyCredential\(AttestationPublicKeyCredential\)

Initializes a new Microsoft Graph attestation public key credential by copying values
from a standard <xref href="DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential" data-throw-if-not-resolved="false"></xref> produced by the WebAuthn API.
Only the response fields defined by the Microsoft Graph
<code>webauthnAuthenticatorAttestationResponse</code> schema (attestationObject, clientDataJSON)
are forwarded; other WebAuthn-standard fields are dropped.

```csharp
[SetsRequiredMembers]
public MicrosoftGraphAttestationPublicKeyCredential(AttestationPublicKeyCredential source)
```

#### Parameters

`source` [AttestationPublicKeyCredential](DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential.md)

The source credential returned by the authenticator.

## Properties

### <a id="DSInternals_Win32_WebAuthn_Entra_MicrosoftGraphAttestationPublicKeyCredential_ClientExtensionResults"></a> ClientExtensionResults

Outputs of client extension processing.

```csharp
[JsonPropertyName("clientExtensionResults")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public AuthenticationExtensionsClientAttestationOutputs? ClientExtensionResults { get; init; }
```

#### Property Value

 [AuthenticationExtensionsClientAttestationOutputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAttestationOutputs.md)?

### <a id="DSInternals_Win32_WebAuthn_Entra_MicrosoftGraphAttestationPublicKeyCredential_Id"></a> Id

The credential identifier (Base64Url encoded).

```csharp
[JsonPropertyName("id")]
[JsonRequired]
[JsonConverter(typeof(Base64UrlConverter))]
public required byte[] Id { get; init; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_Entra_MicrosoftGraphAttestationPublicKeyCredential_Response"></a> Response

Authenticator response payload.

```csharp
[JsonPropertyName("response")]
[JsonRequired]
public required AuthenticatorAttestationResponse Response { get; init; }
```

#### Property Value

 [AuthenticatorAttestationResponse](DSInternals.Win32.WebAuthn.AuthenticatorAttestationResponse.md)

## Methods

### <a id="DSInternals_Win32_WebAuthn_Entra_MicrosoftGraphAttestationPublicKeyCredential_FromJson_System_String_"></a> FromJson\(string\)

Deserializes a JSON string into a Microsoft Graph attestation public key credential.

```csharp
public static MicrosoftGraphAttestationPublicKeyCredential? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of an attestation public key credential.

#### Returns

 [MicrosoftGraphAttestationPublicKeyCredential](DSInternals.Win32.WebAuthn.Entra.MicrosoftGraphAttestationPublicKeyCredential.md)?

An attestation credential if deserialization is successful; otherwise, null.

### <a id="DSInternals_Win32_WebAuthn_Entra_MicrosoftGraphAttestationPublicKeyCredential_ToString"></a> ToString\(\)

Serializes the credential to JSON in the shape accepted by Microsoft Graph.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of this credential.

