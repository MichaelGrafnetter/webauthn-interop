# <a id="DSInternals_Win32_WebAuthn_FIDO_AttestedCredentialData"></a> Class AttestedCredentialData

Namespace: [DSInternals.Win32.WebAuthn.FIDO](DSInternals.Win32.WebAuthn.FIDO.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Attested credential data is a variable-length byte array added to the authenticator 
data when generating an attestation object for a given credential.

```csharp
public class AttestedCredentialData
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AttestedCredentialData](DSInternals.Win32.WebAuthn.FIDO.AttestedCredentialData.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="DSInternals_Win32_WebAuthn_FIDO_AttestedCredentialData__ctor_System_IO_BinaryReader_"></a> AttestedCredentialData\(BinaryReader\)

Decodes attested credential data.

```csharp
public AttestedCredentialData(BinaryReader reader)
```

#### Parameters

`reader` [BinaryReader](https://learn.microsoft.com/dotnet/api/system.io.binaryreader)

## Properties

### <a id="DSInternals_Win32_WebAuthn_FIDO_AttestedCredentialData_AaGuid"></a> AaGuid

The AAGUID of the authenticator. Can be used to identify the make and model of the authenticator.

```csharp
public Guid AaGuid { get; }
```

#### Property Value

 [Guid](https://learn.microsoft.com/dotnet/api/system.guid)

### <a id="DSInternals_Win32_WebAuthn_FIDO_AttestedCredentialData_CredentialId"></a> CredentialId

A probabilistically-unique byte sequence identifying a public key credential source and its authentication assertions.

```csharp
public byte[] CredentialId { get; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_FIDO_AttestedCredentialData_CredentialPublicKey"></a> CredentialPublicKey

The credential public key encoded in COSE_Key format, as defined in 
Section 7 of RFC8152, using the CTAP2 canonical CBOR encoding form.

```csharp
public CredentialPublicKey CredentialPublicKey { get; }
```

#### Property Value

 [CredentialPublicKey](DSInternals.Win32.WebAuthn.FIDO.CredentialPublicKey.md)

## Methods

### <a id="DSInternals_Win32_WebAuthn_FIDO_AttestedCredentialData_ToString"></a> ToString\(\)

Displays the data in a human-readable form.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

