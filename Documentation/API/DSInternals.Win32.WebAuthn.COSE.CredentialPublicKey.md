# <a id="DSInternals_Win32_WebAuthn_COSE_CredentialPublicKey"></a> Class CredentialPublicKey

Namespace: [DSInternals.Win32.WebAuthn.COSE](DSInternals.Win32.WebAuthn.COSE.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents a parsed COSE <code>credentialPublicKey</code> structure.

```csharp
public class CredentialPublicKey
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CredentialPublicKey](DSInternals.Win32.WebAuthn.COSE.CredentialPublicKey.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="DSInternals_Win32_WebAuthn_COSE_CredentialPublicKey__ctor_System_Byte___"></a> CredentialPublicKey\(byte\[\]\)

Initializes a new instance from CBOR-encoded credential public key bytes.

```csharp
public CredentialPublicKey(byte[] cborBytes)
```

#### Parameters

`cborBytes` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

CBOR-encoded <code>credentialPublicKey</code> (COSE_Key).

## Properties

### <a id="DSInternals_Win32_WebAuthn_COSE_CredentialPublicKey_Algorithm"></a> Algorithm

Gets the COSE signature algorithm identifier.

```csharp
public Algorithm Algorithm { get; }
```

#### Property Value

 [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)

### <a id="DSInternals_Win32_WebAuthn_COSE_CredentialPublicKey_ECDsa"></a> ECDsa

Gets the ECDSA public key when the key type is EC2.

```csharp
public ECDsa? ECDsa { get; }
```

#### Property Value

 [ECDsa](https://learn.microsoft.com/dotnet/api/system.security.cryptography.ecdsa)?

### <a id="DSInternals_Win32_WebAuthn_COSE_CredentialPublicKey_EdDSAPublicKey"></a> EdDSAPublicKey

Gets the EdDSA public key bytes when the key type is OKP and algorithm is EdDSA.

```csharp
public byte[]? EdDSAPublicKey { get; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_COSE_CredentialPublicKey_Padding"></a> Padding

Gets the RSA signature padding implied by the selected algorithm.

```csharp
public RSASignaturePadding? Padding { get; }
```

#### Property Value

 [RSASignaturePadding](https://learn.microsoft.com/dotnet/api/system.security.cryptography.rsasignaturepadding)?

### <a id="DSInternals_Win32_WebAuthn_COSE_CredentialPublicKey_RSA"></a> RSA

Gets the RSA public key when the key type is RSA.

```csharp
public RSACng? RSA { get; }
```

#### Property Value

 [RSACng](https://learn.microsoft.com/dotnet/api/system.security.cryptography.rsacng)?

### <a id="DSInternals_Win32_WebAuthn_COSE_CredentialPublicKey_Type"></a> Type

Gets the COSE key type.

```csharp
public KeyType Type { get; }
```

#### Property Value

 [KeyType](DSInternals.Win32.WebAuthn.COSE.KeyType.md)

## Methods

### <a id="DSInternals_Win32_WebAuthn_COSE_CredentialPublicKey_Build_System_Security_Cryptography_AsymmetricAlgorithm_DSInternals_Win32_WebAuthn_COSE_Algorithm_"></a> Build\(AsymmetricAlgorithm, Algorithm\)

```csharp
public static byte[] Build(AsymmetricAlgorithm key, Algorithm algorithm)
```

#### Parameters

`key` [AsymmetricAlgorithm](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm)

`algorithm` [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)

#### Returns

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_COSE_CredentialPublicKey_GetBytes"></a> GetBytes\(\)

Encodes the credential public key to raw CBOR bytes.

```csharp
public byte[] GetBytes()
```

#### Returns

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

CBOR-encoded key bytes.

### <a id="DSInternals_Win32_WebAuthn_COSE_CredentialPublicKey_ToString"></a> ToString\(\)

Returns a textual representation of the credential public key.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

