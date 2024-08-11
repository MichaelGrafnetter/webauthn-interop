# <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData"></a> Class AuthenticatorData

Namespace: [DSInternals.Win32.WebAuthn.FIDO](DSInternals.Win32.WebAuthn.FIDO.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Contains information from the authenticator about the processing of a credential creation or authentication request.

```csharp
public class AuthenticatorData
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AuthenticatorData](DSInternals.Win32.WebAuthn.FIDO.AuthenticatorData.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData__ctor_System_Byte___"></a> AuthenticatorData\(byte\[\]\)

Parses the binary form of the authenticator data.

```csharp
public AuthenticatorData(byte[] authData)
```

#### Parameters

`authData` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

 [ArgumentOutOfRangeException](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception)

 [ArgumentException](https://learn.microsoft.com/dotnet/api/system.argumentexception)

## Properties

### <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData_AttestedCredentialData"></a> AttestedCredentialData

Attested credential data is a variable-length byte array added to the 
authenticator data when generating an attestation object for a given credential.

```csharp
public AttestedCredentialData AttestedCredentialData { get; }
```

#### Property Value

 [AttestedCredentialData](DSInternals.Win32.WebAuthn.FIDO.AttestedCredentialData.md)

### <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData_Extensions"></a> Extensions

Optional extensions to suit particular use cases.

```csharp
public CBORObject Extensions { get; }
```

#### Property Value

 CBORObject

### <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData_Flags"></a> Flags

Flags contains information from the authenticator about the authentication 
and whether or not certain data is present in the authenticator data.

```csharp
public AuthenticatorFlags Flags { get; }
```

#### Property Value

 [AuthenticatorFlags](DSInternals.Win32.WebAuthn.AuthenticatorFlags.md)

### <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData_RelyingPartyIdHash"></a> RelyingPartyIdHash

SHA-256 hash of the RP ID the credential is scoped to.

```csharp
public byte[] RelyingPartyIdHash { get; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData_SignatureCount"></a> SignatureCount

Signature counter, 32-bit unsigned big-endian integer.

```csharp
public uint SignatureCount { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

