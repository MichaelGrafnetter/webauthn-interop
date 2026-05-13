# <a id="DSInternals_Win32_WebAuthn_Cryptography_ExportedPasskey"></a> Class ExportedPasskey

Namespace: [DSInternals.Win32.WebAuthn.Cryptography](DSInternals.Win32.WebAuthn.Cryptography.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents a passkey imported from an external authenticator export.

```csharp
public sealed class ExportedPasskey
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ExportedPasskey](DSInternals.Win32.WebAuthn.Cryptography.ExportedPasskey.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_Cryptography_ExportedPasskey_AaGuid"></a> AaGuid

The AAGUID used by the authenticator.

```csharp
public required Guid AaGuid { get; init; }
```

#### Property Value

 [Guid](https://learn.microsoft.com/dotnet/api/system.guid)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_ExportedPasskey_CredentialId"></a> CredentialId

The WebAuthn credential ID, encoded as Base64Url.

```csharp
public required byte[] CredentialId { get; init; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_Cryptography_ExportedPasskey_Discoverable"></a> Discoverable

Indicates whether the export marked this passkey as discoverable.

```csharp
public bool Discoverable { get; init; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_ExportedPasskey_KeyAlgorithm"></a> KeyAlgorithm

The COSE algorithm.

```csharp
public Algorithm KeyAlgorithm { get; init; }
```

#### Property Value

 [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_ExportedPasskey_KeyCurve"></a> KeyCurve

The elliptic curve, when applicable.

```csharp
public EllipticCurve? KeyCurve { get; init; }
```

#### Property Value

 [EllipticCurve](DSInternals.Win32.WebAuthn.COSE.EllipticCurve.md)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_ExportedPasskey_KeyType"></a> KeyType

The COSE key type.

```csharp
public KeyType KeyType { get; init; }
```

#### Property Value

 [KeyType](DSInternals.Win32.WebAuthn.COSE.KeyType.md)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_ExportedPasskey_PrivateKey"></a> PrivateKey

The imported private key.

```csharp
[JsonIgnore]
public required AsymmetricAlgorithm PrivateKey { get; init; }
```

#### Property Value

 [AsymmetricAlgorithm](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_ExportedPasskey_RelyingParty"></a> RelyingParty

The relying party ID associated with this passkey.

```csharp
public string? RelyingParty { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_ExportedPasskey_RelyingPartyName"></a> RelyingPartyName

The relying party display name associated with this passkey.

```csharp
public string? RelyingPartyName { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_ExportedPasskey_SignatureCounter"></a> SignatureCounter

The signature counter stored with this passkey.

```csharp
public uint SignatureCounter { get; init; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_ExportedPasskey_Url"></a> Url

The URL associated with this passkey, when provided by the export.

```csharp
public string? Url { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_ExportedPasskey_UserDisplayName"></a> UserDisplayName

The display name associated with this passkey.

```csharp
public string? UserDisplayName { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_ExportedPasskey_UserHandle"></a> UserHandle

The WebAuthn user handle, encoded as Base64Url.

```csharp
public byte[]? UserHandle { get; init; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_ExportedPasskey_Username"></a> Username

The username associated with this passkey.

```csharp
public string? Username { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

