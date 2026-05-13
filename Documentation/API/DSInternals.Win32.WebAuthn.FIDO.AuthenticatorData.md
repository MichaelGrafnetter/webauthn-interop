# <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData"></a> Class AuthenticatorData

Namespace: [DSInternals.Win32.WebAuthn.FIDO](DSInternals.Win32.WebAuthn.FIDO.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents parsed authenticator data from a WebAuthn assertion.

```csharp
public sealed class AuthenticatorData
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AuthenticatorData](DSInternals.Win32.WebAuthn.FIDO.AuthenticatorData.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

The authenticator data structure is defined in the WebAuthn specification:
https://www.w3.org/TR/webauthn/#sec-authenticator-data

## Constructors

### <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData__ctor_System_String_DSInternals_Win32_WebAuthn_AuthenticatorFlags_System_UInt32_DSInternals_Win32_WebAuthn_FIDO_AttestedCredentialData_System_Byte___"></a> AuthenticatorData\(string, AuthenticatorFlags, uint, AttestedCredentialData?, byte\[\]?\)

```csharp
[SetsRequiredMembers]
public AuthenticatorData(string relyingPartyId, AuthenticatorFlags flags, uint signatureCount, AttestedCredentialData? attestedCredentialData = null, byte[]? extensions = null)
```

#### Parameters

`relyingPartyId` [string](https://learn.microsoft.com/dotnet/api/system.string)

`flags` [AuthenticatorFlags](DSInternals.Win32.WebAuthn.AuthenticatorFlags.md)

`signatureCount` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

`attestedCredentialData` [AttestedCredentialData](DSInternals.Win32.WebAuthn.FIDO.AttestedCredentialData.md)?

`extensions` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData__ctor_System_Byte___DSInternals_Win32_WebAuthn_AuthenticatorFlags_System_UInt32_DSInternals_Win32_WebAuthn_FIDO_AttestedCredentialData_System_Byte___"></a> AuthenticatorData\(byte\[\], AuthenticatorFlags, uint, AttestedCredentialData?, byte\[\]?\)

```csharp
[SetsRequiredMembers]
public AuthenticatorData(byte[] rpIdHash, AuthenticatorFlags flags, uint signatureCount, AttestedCredentialData? attestedCredentialData = null, byte[]? extensions = null)
```

#### Parameters

`rpIdHash` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

`flags` [AuthenticatorFlags](DSInternals.Win32.WebAuthn.AuthenticatorFlags.md)

`signatureCount` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

`attestedCredentialData` [AttestedCredentialData](DSInternals.Win32.WebAuthn.FIDO.AttestedCredentialData.md)?

`extensions` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

## Properties

### <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData_AttestedCredentialData"></a> AttestedCredentialData

Attested credential data is a variable-length byte array added to the
authenticator data when generating an attestation object for a given credential.

```csharp
public AttestedCredentialData? AttestedCredentialData { get; init; }
```

#### Property Value

 [AttestedCredentialData](DSInternals.Win32.WebAuthn.FIDO.AttestedCredentialData.md)?

### <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData_Extensions"></a> Extensions

Optional extensions to suit particular use cases.

```csharp
public byte[]? Extensions { get; init; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData_Flags"></a> Flags

Flags contains information from the authenticator about the authentication
and whether or not certain data is present in the authenticator data.

```csharp
public required AuthenticatorFlags Flags { get; init; }
```

#### Property Value

 [AuthenticatorFlags](DSInternals.Win32.WebAuthn.AuthenticatorFlags.md)

### <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData_HasAttestedCredentialData"></a> HasAttestedCredentialData

HasAttestedCredentialData indicates that the authenticator added attested credential data to the authenticator data.

```csharp
[MemberNotNullWhen(true, "AttestedCredentialData")]
public bool HasAttestedCredentialData { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData_HasExtensionsData"></a> HasExtensionsData

HasExtensionsData indicates that the authenticator added extension data to the authenticator data.

```csharp
[MemberNotNullWhen(true, "Extensions")]
public bool HasExtensionsData { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData_IsBackedUp"></a> IsBackedUp

The current backup state of a multi-device credential as determined by the current managing authenticator.

```csharp
public bool IsBackedUp { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData_IsBackupEligible"></a> IsBackupEligible

Backup eligibility is signaled in authenticator data's flags along with the current backup state.

```csharp
public bool IsBackupEligible { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData_RelyingPartyIdHash"></a> RelyingPartyIdHash

SHA-256 hash of the RP ID the credential is scoped to.

```csharp
public required byte[] RelyingPartyIdHash { get; init; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData_SignatureCount"></a> SignatureCount

Signature counter, 32-bit unsigned big-endian integer.

```csharp
public required uint SignatureCount { get; init; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData_UserPresent"></a> UserPresent

UserPresent indicates that the user presence test has completed successfully.

```csharp
public bool UserPresent { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData_UserVerified"></a> UserVerified

UserVerified indicates that the user verification process has completed successfully.

```csharp
public bool UserVerified { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData_PackAttestation_System_Byte___DSInternals_Win32_WebAuthn_COSE_Algorithm_System_Byte___"></a> PackAttestation\(byte\[\], Algorithm, byte\[\]\)

```csharp
public static byte[] PackAttestation(byte[] authenticatorData, Algorithm algorithm, byte[] signature)
```

#### Parameters

`authenticatorData` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

`algorithm` [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)

`signature` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

#### Returns

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData_Parse_System_String_"></a> Parse\(string\)

Parses authenticator data from a Base64Url-encoded string.

```csharp
public static AuthenticatorData Parse(string base64UrlData)
```

#### Parameters

`base64UrlData` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 [AuthenticatorData](DSInternals.Win32.WebAuthn.FIDO.AuthenticatorData.md)

### <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData_Parse_System_ReadOnlyMemory_System_Byte__"></a> Parse\(ReadOnlyMemory<byte\>\)

Parses authenticator data from a memory buffer.

```csharp
public static AuthenticatorData Parse(ReadOnlyMemory<byte> data)
```

#### Parameters

`data` [ReadOnlyMemory](https://learn.microsoft.com/dotnet/api/system.readonlymemory\-1)<[byte](https://learn.microsoft.com/dotnet/api/system.byte)\>

#### Returns

 [AuthenticatorData](DSInternals.Win32.WebAuthn.FIDO.AuthenticatorData.md)

### <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData_ToByteArray"></a> ToByteArray\(\)

```csharp
public byte[] ToByteArray()
```

#### Returns

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorData_WriteTo_System_IO_Stream_"></a> WriteTo\(Stream\)

```csharp
public void WriteTo(Stream output)
```

#### Parameters

`output` [Stream](https://learn.microsoft.com/dotnet/api/system.io.stream)

