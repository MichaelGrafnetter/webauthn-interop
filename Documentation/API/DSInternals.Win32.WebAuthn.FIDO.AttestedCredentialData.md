# <a id="DSInternals_Win32_WebAuthn_FIDO_AttestedCredentialData"></a> Class AttestedCredentialData

Namespace: [DSInternals.Win32.WebAuthn.FIDO](DSInternals.Win32.WebAuthn.FIDO.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Attested credential data is a variable-length byte array added to the authenticator
data when generating an attestation object for a given credential.

```csharp
public sealed class AttestedCredentialData
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AttestedCredentialData](DSInternals.Win32.WebAuthn.FIDO.AttestedCredentialData.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="DSInternals_Win32_WebAuthn_FIDO_AttestedCredentialData__ctor_System_Guid_System_ReadOnlyMemory_System_Byte__System_ReadOnlyMemory_System_Byte__"></a> AttestedCredentialData\(Guid, ReadOnlyMemory<byte\>, ReadOnlyMemory<byte\>\)

Initializes a new instance of the <xref href="DSInternals.Win32.WebAuthn.FIDO.AttestedCredentialData" data-throw-if-not-resolved="false"></xref> class.

```csharp
[SetsRequiredMembers]
public AttestedCredentialData(Guid aaGuid, ReadOnlyMemory<byte> credentialId, ReadOnlyMemory<byte> credentialPublicKey)
```

#### Parameters

`aaGuid` [Guid](https://learn.microsoft.com/dotnet/api/system.guid)

The AAGUID of the authenticator.

`credentialId` [ReadOnlyMemory](https://learn.microsoft.com/dotnet/api/system.readonlymemory\-1)<[byte](https://learn.microsoft.com/dotnet/api/system.byte)\>

A probabilistically-unique byte sequence identifying a public key credential source and its authentication assertions.

`credentialPublicKey` [ReadOnlyMemory](https://learn.microsoft.com/dotnet/api/system.readonlymemory\-1)<[byte](https://learn.microsoft.com/dotnet/api/system.byte)\>

The credential public key encoded in COSE_Key format.

## Properties

### <a id="DSInternals_Win32_WebAuthn_FIDO_AttestedCredentialData_AaGuid"></a> AaGuid

The AAGUID of the authenticator. Can be used to identify the make and model of the authenticator.

```csharp
public required Guid AaGuid { get; init; }
```

#### Property Value

 [Guid](https://learn.microsoft.com/dotnet/api/system.guid)

### <a id="DSInternals_Win32_WebAuthn_FIDO_AttestedCredentialData_CredentialId"></a> CredentialId

A probabilistically-unique byte sequence identifying a public key credential source and its authentication assertions.

```csharp
public required ReadOnlyMemory<byte> CredentialId { get; init; }
```

#### Property Value

 [ReadOnlyMemory](https://learn.microsoft.com/dotnet/api/system.readonlymemory\-1)<[byte](https://learn.microsoft.com/dotnet/api/system.byte)\>

### <a id="DSInternals_Win32_WebAuthn_FIDO_AttestedCredentialData_CredentialPublicKey"></a> CredentialPublicKey

The credential public key encoded in COSE_Key format.

```csharp
public required ReadOnlyMemory<byte> CredentialPublicKey { get; init; }
```

#### Property Value

 [ReadOnlyMemory](https://learn.microsoft.com/dotnet/api/system.readonlymemory\-1)<[byte](https://learn.microsoft.com/dotnet/api/system.byte)\>

## Methods

### <a id="DSInternals_Win32_WebAuthn_FIDO_AttestedCredentialData_ToByteArray"></a> ToByteArray\(\)

```csharp
public byte[] ToByteArray()
```

#### Returns

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_FIDO_AttestedCredentialData_ToString"></a> ToString\(\)

Displays the data in a human-readable form.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_FIDO_AttestedCredentialData_WriteTo_System_IO_Stream_"></a> WriteTo\(Stream\)

```csharp
public void WriteTo(Stream output)
```

#### Parameters

`output` [Stream](https://learn.microsoft.com/dotnet/api/system.io.stream)

