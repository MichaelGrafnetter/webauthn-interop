# <a id="DSInternals_Win32_WebAuthn_FIDO_CredentialPublicKey"></a> Class CredentialPublicKey

Namespace: [DSInternals.Win32.WebAuthn.FIDO](DSInternals.Win32.WebAuthn.FIDO.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

```csharp
public class CredentialPublicKey
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CredentialPublicKey](DSInternals.Win32.WebAuthn.FIDO.CredentialPublicKey.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="DSInternals_Win32_WebAuthn_FIDO_CredentialPublicKey__ctor_PeterO_Cbor_CBORObject_"></a> CredentialPublicKey\(CBORObject\)

```csharp
public CredentialPublicKey(CBORObject cpk)
```

#### Parameters

`cpk` CBORObject

## Properties

### <a id="DSInternals_Win32_WebAuthn_FIDO_CredentialPublicKey_Algorithm"></a> Algorithm

```csharp
public Algorithm Algorithm { get; }
```

#### Property Value

 [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)

### <a id="DSInternals_Win32_WebAuthn_FIDO_CredentialPublicKey_ECDsa"></a> ECDsa

```csharp
public ECDsa ECDsa { get; }
```

#### Property Value

 [ECDsa](https://learn.microsoft.com/dotnet/api/system.security.cryptography.ecdsa)

### <a id="DSInternals_Win32_WebAuthn_FIDO_CredentialPublicKey_EdDSAPublicKey"></a> EdDSAPublicKey

```csharp
public byte[] EdDSAPublicKey { get; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_FIDO_CredentialPublicKey_Padding"></a> Padding

```csharp
public RSASignaturePadding Padding { get; }
```

#### Property Value

 [RSASignaturePadding](https://learn.microsoft.com/dotnet/api/system.security.cryptography.rsasignaturepadding)

### <a id="DSInternals_Win32_WebAuthn_FIDO_CredentialPublicKey_RSA"></a> RSA

```csharp
public RSACng RSA { get; }
```

#### Property Value

 [RSACng](https://learn.microsoft.com/dotnet/api/system.security.cryptography.rsacng)

### <a id="DSInternals_Win32_WebAuthn_FIDO_CredentialPublicKey_Type"></a> Type

```csharp
public KeyType Type { get; }
```

#### Property Value

 [KeyType](DSInternals.Win32.WebAuthn.COSE.KeyType.md)

## Methods

### <a id="DSInternals_Win32_WebAuthn_FIDO_CredentialPublicKey_GetBytes"></a> GetBytes\(\)

```csharp
public byte[] GetBytes()
```

#### Returns

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_FIDO_CredentialPublicKey_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

