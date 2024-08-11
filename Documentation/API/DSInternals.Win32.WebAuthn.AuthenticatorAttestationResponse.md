# <a id="DSInternals_Win32_WebAuthn_AuthenticatorAttestationResponse"></a> Class AuthenticatorAttestationResponse

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

The AuthenticatorAttestationResponse class represents the authenticator's response
to a client’s request for the creation of a new public key credential.
It contains information about the new credential that can be used to identify it for later use,
and metadata that can be used by the WebAuthn Relying Party to assess the characteristics
of the credential during registration.

```csharp
public class AuthenticatorAttestationResponse : AuthenticatorResponse
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AuthenticatorResponse](DSInternals.Win32.WebAuthn.AuthenticatorResponse.md) ← 
[AuthenticatorAttestationResponse](DSInternals.Win32.WebAuthn.AuthenticatorAttestationResponse.md)

#### Inherited Members

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

This attribute contains an attestation object, which is opaque to,
and cryptographically protected against tampering by, the client.
The attestation object contains both authenticator data and an attestation statement.

```csharp
[JsonPropertyName("attestationObject")]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[] AttestationObject { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

