# <a id="DSInternals_Win32_WebAuthn_Cryptography_SoftwareAuthenticator"></a> Class SoftwareAuthenticator

Namespace: [DSInternals.Win32.WebAuthn.Cryptography](DSInternals.Win32.WebAuthn.Cryptography.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Software-based authenticator that signs WebAuthn requests using a PEM private key file,
without requiring a hardware authenticator or the Windows WebAuthn API.

```csharp
public static class SoftwareAuthenticator
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[SoftwareAuthenticator](DSInternals.Win32.WebAuthn.Cryptography.SoftwareAuthenticator.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Fields

### <a id="DSInternals_Win32_WebAuthn_Cryptography_SoftwareAuthenticator_DefaultCredentialIdLength"></a> DefaultCredentialIdLength

```csharp
public const int DefaultCredentialIdLength = 32
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

## Methods

### <a id="DSInternals_Win32_WebAuthn_Cryptography_SoftwareAuthenticator_DetectAlgorithm_System_Security_Cryptography_AsymmetricAlgorithm_"></a> DetectAlgorithm\(AsymmetricAlgorithm\)

Determines the COSE algorithm that matches the given private key.

```csharp
public static Algorithm DetectAlgorithm(AsymmetricAlgorithm key)
```

#### Parameters

`key` [AsymmetricAlgorithm](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm)

#### Returns

 [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_SoftwareAuthenticator_GenerateKeyPair_DSInternals_Win32_WebAuthn_COSE_Algorithm_"></a> GenerateKeyPair\(Algorithm\)

```csharp
public static AsymmetricAlgorithm GenerateKeyPair(Algorithm algorithm)
```

#### Parameters

`algorithm` [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)

#### Returns

 [AsymmetricAlgorithm](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_SoftwareAuthenticator_GenerateKeyPairPem_DSInternals_Win32_WebAuthn_COSE_Algorithm_"></a> GenerateKeyPairPem\(Algorithm\)

Generates a private key for the requested COSE algorithm and exports it as PKCS#8 PEM.

```csharp
public static string GenerateKeyPairPem(Algorithm algorithm)
```

#### Parameters

`algorithm` [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_SoftwareAuthenticator_GetAssertion_System_String_System_Byte___DSInternals_Win32_WebAuthn_COSE_Algorithm_System_UInt32_DSInternals_Win32_WebAuthn_AuthenticatorFlags_System_Byte___System_Byte___System_Security_Cryptography_AsymmetricAlgorithm_DSInternals_Win32_WebAuthn_AuthenticatorAttachment_"></a> GetAssertion\(string, byte\[\], Algorithm, uint, AuthenticatorFlags, byte\[\], byte\[\]?, AsymmetricAlgorithm, AuthenticatorAttachment\)

Builds a complete assertion response signed with the given private key.

```csharp
public static AssertionPublicKeyCredential GetAssertion(string relyingPartyId, byte[] challenge, Algorithm algorithm, uint signatureCounter, AuthenticatorFlags flags, byte[] credentialId, byte[]? userHandle, AsymmetricAlgorithm privateKey, AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any)
```

#### Parameters

`relyingPartyId` [string](https://learn.microsoft.com/dotnet/api/system.string)

`challenge` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

`algorithm` [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)

`signatureCounter` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

`flags` [AuthenticatorFlags](DSInternals.Win32.WebAuthn.AuthenticatorFlags.md)

`credentialId` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

`userHandle` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

`privateKey` [AsymmetricAlgorithm](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm)

`authenticatorAttachment` [AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)

#### Returns

 [AssertionPublicKeyCredential](DSInternals.Win32.WebAuthn.AssertionPublicKeyCredential.md)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_SoftwareAuthenticator_GetAssertion_System_String_System_String_System_Byte___DSInternals_Win32_WebAuthn_COSE_Algorithm_System_UInt32_DSInternals_Win32_WebAuthn_AuthenticatorFlags_System_Byte___System_Byte___System_Security_Cryptography_AsymmetricAlgorithm_DSInternals_Win32_WebAuthn_AuthenticatorAttachment_"></a> GetAssertion\(string, string?, byte\[\], Algorithm, uint, AuthenticatorFlags, byte\[\], byte\[\]?, AsymmetricAlgorithm, AuthenticatorAttachment\)

Builds a complete assertion response signed with the given private key.

```csharp
public static AssertionPublicKeyCredential GetAssertion(string hostName, string? relyingPartyId, byte[] challenge, Algorithm algorithm, uint signatureCounter, AuthenticatorFlags flags, byte[] credentialId, byte[]? userHandle, AsymmetricAlgorithm privateKey, AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any)
```

#### Parameters

`hostName` [string](https://learn.microsoft.com/dotnet/api/system.string)

`relyingPartyId` [string](https://learn.microsoft.com/dotnet/api/system.string)?

`challenge` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

`algorithm` [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)

`signatureCounter` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

`flags` [AuthenticatorFlags](DSInternals.Win32.WebAuthn.AuthenticatorFlags.md)

`credentialId` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

`userHandle` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

`privateKey` [AsymmetricAlgorithm](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm)

`authenticatorAttachment` [AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)

#### Returns

 [AssertionPublicKeyCredential](DSInternals.Win32.WebAuthn.AssertionPublicKeyCredential.md)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_SoftwareAuthenticator_ImportPrivateKeyFromPem_System_String_"></a> ImportPrivateKeyFromPem\(string\)

Imports a private key from a PEM string. The caller is responsible for disposing the returned key.

```csharp
public static AsymmetricAlgorithm ImportPrivateKeyFromPem(string pem)
```

#### Parameters

`pem` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 [AsymmetricAlgorithm](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_SoftwareAuthenticator_ImportPrivateKeyFromPkcs8_System_Byte___"></a> ImportPrivateKeyFromPkcs8\(byte\[\]\)

Imports a private key from DER-encoded PKCS#8 data. The caller is responsible for disposing the returned key.

```csharp
public static AsymmetricAlgorithm ImportPrivateKeyFromPkcs8(byte[] privateKey)
```

#### Parameters

`privateKey` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

#### Returns

 [AsymmetricAlgorithm](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_SoftwareAuthenticator_LoadPrivateKeyFromPem_System_String_"></a> LoadPrivateKeyFromPem\(string\)

Loads a private key from a PEM file. The caller is responsible for disposing the returned key.

```csharp
public static AsymmetricAlgorithm LoadPrivateKeyFromPem(string pemFilePath)
```

#### Parameters

`pemFilePath` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 [AsymmetricAlgorithm](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_SoftwareAuthenticator_MakeCredential_DSInternals_Win32_WebAuthn_RelyingPartyInformation_DSInternals_Win32_WebAuthn_UserInformation_System_Byte___DSInternals_Win32_WebAuthn_COSE_Algorithm_System_Guid_System_UInt32_DSInternals_Win32_WebAuthn_AuthenticatorFlags_System_Security_Cryptography_AsymmetricAlgorithm_System_Byte___"></a> MakeCredential\(RelyingPartyInformation, UserInformation, byte\[\], Algorithm, Guid, uint, AuthenticatorFlags, AsymmetricAlgorithm, byte\[\]?\)

Builds a complete attestation response (packed self-attestation) signed with the given private key.

```csharp
public static AttestationPublicKeyCredential MakeCredential(RelyingPartyInformation relyingParty, UserInformation user, byte[] challenge, Algorithm algorithm, Guid aaGuid, uint signatureCounter, AuthenticatorFlags flags, AsymmetricAlgorithm privateKey, byte[]? credentialId = null)
```

#### Parameters

`relyingParty` [RelyingPartyInformation](DSInternals.Win32.WebAuthn.RelyingPartyInformation.md)

`user` [UserInformation](DSInternals.Win32.WebAuthn.UserInformation.md)

`challenge` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

`algorithm` [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)

`aaGuid` [Guid](https://learn.microsoft.com/dotnet/api/system.guid)

`signatureCounter` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

`flags` [AuthenticatorFlags](DSInternals.Win32.WebAuthn.AuthenticatorFlags.md)

`privateKey` [AsymmetricAlgorithm](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm)

`credentialId` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

#### Returns

 [AttestationPublicKeyCredential](DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential.md)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_SoftwareAuthenticator_MakeCredential_DSInternals_Win32_WebAuthn_RelyingPartyInformation_System_String_DSInternals_Win32_WebAuthn_UserInformation_System_Byte___DSInternals_Win32_WebAuthn_COSE_Algorithm_System_Guid_System_UInt32_DSInternals_Win32_WebAuthn_AuthenticatorFlags_System_Security_Cryptography_AsymmetricAlgorithm_System_Byte___"></a> MakeCredential\(RelyingPartyInformation, string, UserInformation, byte\[\], Algorithm, Guid, uint, AuthenticatorFlags, AsymmetricAlgorithm, byte\[\]?\)

Builds a complete attestation response (packed self-attestation) signed with the given private key.

```csharp
public static AttestationPublicKeyCredential MakeCredential(RelyingPartyInformation relyingParty, string hostName, UserInformation user, byte[] challenge, Algorithm algorithm, Guid aaGuid, uint signatureCounter, AuthenticatorFlags flags, AsymmetricAlgorithm privateKey, byte[]? credentialId = null)
```

#### Parameters

`relyingParty` [RelyingPartyInformation](DSInternals.Win32.WebAuthn.RelyingPartyInformation.md)

`hostName` [string](https://learn.microsoft.com/dotnet/api/system.string)

`user` [UserInformation](DSInternals.Win32.WebAuthn.UserInformation.md)

`challenge` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

`algorithm` [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)

`aaGuid` [Guid](https://learn.microsoft.com/dotnet/api/system.guid)

`signatureCounter` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

`flags` [AuthenticatorFlags](DSInternals.Win32.WebAuthn.AuthenticatorFlags.md)

`privateKey` [AsymmetricAlgorithm](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm)

`credentialId` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

#### Returns

 [AttestationPublicKeyCredential](DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential.md)

