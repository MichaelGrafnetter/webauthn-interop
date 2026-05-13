# <a id="DSInternals_Win32_WebAuthn_Cryptography_Ed25519"></a> Class Ed25519

Namespace: [DSInternals.Win32.WebAuthn.Cryptography](DSInternals.Win32.WebAuthn.Cryptography.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

An <xref href="System.Security.Cryptography.AsymmetricAlgorithm" data-throw-if-not-resolved="false"></xref> wrapper around an NSec Ed25519 private key.

```csharp
public sealed class Ed25519 : AsymmetricAlgorithm, IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AsymmetricAlgorithm](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm) ← 
[Ed25519](DSInternals.Win32.WebAuthn.Cryptography.Ed25519.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable)

#### Inherited Members

[AsymmetricAlgorithm.Clear\(\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.clear), 
[AsymmetricAlgorithm.Create\(\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.create\#system\-security\-cryptography\-asymmetricalgorithm\-create), 
[AsymmetricAlgorithm.Create\(string\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.create\#system\-security\-cryptography\-asymmetricalgorithm\-create\(system\-string\)), 
[AsymmetricAlgorithm.Dispose\(\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.dispose\#system\-security\-cryptography\-asymmetricalgorithm\-dispose), 
[AsymmetricAlgorithm.ExportEncryptedPkcs8PrivateKey\(ReadOnlySpan<byte\>, PbeParameters\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.exportencryptedpkcs8privatekey\#system\-security\-cryptography\-asymmetricalgorithm\-exportencryptedpkcs8privatekey\(system\-readonlyspan\(\(system\-byte\)\)\-system\-security\-cryptography\-pbeparameters\)), 
[AsymmetricAlgorithm.ExportEncryptedPkcs8PrivateKey\(ReadOnlySpan<char\>, PbeParameters\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.exportencryptedpkcs8privatekey\#system\-security\-cryptography\-asymmetricalgorithm\-exportencryptedpkcs8privatekey\(system\-readonlyspan\(\(system\-char\)\)\-system\-security\-cryptography\-pbeparameters\)), 
[AsymmetricAlgorithm.ExportEncryptedPkcs8PrivateKeyPem\(ReadOnlySpan<byte\>, PbeParameters\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.exportencryptedpkcs8privatekeypem\#system\-security\-cryptography\-asymmetricalgorithm\-exportencryptedpkcs8privatekeypem\(system\-readonlyspan\(\(system\-byte\)\)\-system\-security\-cryptography\-pbeparameters\)), 
[AsymmetricAlgorithm.ExportEncryptedPkcs8PrivateKeyPem\(ReadOnlySpan<char\>, PbeParameters\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.exportencryptedpkcs8privatekeypem\#system\-security\-cryptography\-asymmetricalgorithm\-exportencryptedpkcs8privatekeypem\(system\-readonlyspan\(\(system\-char\)\)\-system\-security\-cryptography\-pbeparameters\)), 
[AsymmetricAlgorithm.ExportPkcs8PrivateKey\(\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.exportpkcs8privatekey), 
[AsymmetricAlgorithm.ExportPkcs8PrivateKeyPem\(\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.exportpkcs8privatekeypem), 
[AsymmetricAlgorithm.ExportSubjectPublicKeyInfo\(\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.exportsubjectpublickeyinfo), 
[AsymmetricAlgorithm.ExportSubjectPublicKeyInfoPem\(\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.exportsubjectpublickeyinfopem), 
[AsymmetricAlgorithm.FromXmlString\(string\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.fromxmlstring), 
[AsymmetricAlgorithm.ImportEncryptedPkcs8PrivateKey\(ReadOnlySpan<byte\>, ReadOnlySpan<byte\>, out int\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.importencryptedpkcs8privatekey\#system\-security\-cryptography\-asymmetricalgorithm\-importencryptedpkcs8privatekey\(system\-readonlyspan\(\(system\-byte\)\)\-system\-readonlyspan\(\(system\-byte\)\)\-system\-int32@\)), 
[AsymmetricAlgorithm.ImportEncryptedPkcs8PrivateKey\(ReadOnlySpan<char\>, ReadOnlySpan<byte\>, out int\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.importencryptedpkcs8privatekey\#system\-security\-cryptography\-asymmetricalgorithm\-importencryptedpkcs8privatekey\(system\-readonlyspan\(\(system\-char\)\)\-system\-readonlyspan\(\(system\-byte\)\)\-system\-int32@\)), 
[AsymmetricAlgorithm.ImportFromEncryptedPem\(ReadOnlySpan<char\>, ReadOnlySpan<byte\>\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.importfromencryptedpem\#system\-security\-cryptography\-asymmetricalgorithm\-importfromencryptedpem\(system\-readonlyspan\(\(system\-char\)\)\-system\-readonlyspan\(\(system\-byte\)\)\)), 
[AsymmetricAlgorithm.ImportFromEncryptedPem\(ReadOnlySpan<char\>, ReadOnlySpan<char\>\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.importfromencryptedpem\#system\-security\-cryptography\-asymmetricalgorithm\-importfromencryptedpem\(system\-readonlyspan\(\(system\-char\)\)\-system\-readonlyspan\(\(system\-char\)\)\)), 
[AsymmetricAlgorithm.ImportFromPem\(ReadOnlySpan<char\>\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.importfrompem), 
[AsymmetricAlgorithm.ImportPkcs8PrivateKey\(ReadOnlySpan<byte\>, out int\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.importpkcs8privatekey), 
[AsymmetricAlgorithm.ImportSubjectPublicKeyInfo\(ReadOnlySpan<byte\>, out int\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.importsubjectpublickeyinfo), 
[AsymmetricAlgorithm.ToXmlString\(bool\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.toxmlstring), 
[AsymmetricAlgorithm.TryExportEncryptedPkcs8PrivateKey\(ReadOnlySpan<byte\>, PbeParameters, Span<byte\>, out int\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.tryexportencryptedpkcs8privatekey\#system\-security\-cryptography\-asymmetricalgorithm\-tryexportencryptedpkcs8privatekey\(system\-readonlyspan\(\(system\-byte\)\)\-system\-security\-cryptography\-pbeparameters\-system\-span\(\(system\-byte\)\)\-system\-int32@\)), 
[AsymmetricAlgorithm.TryExportEncryptedPkcs8PrivateKey\(ReadOnlySpan<char\>, PbeParameters, Span<byte\>, out int\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.tryexportencryptedpkcs8privatekey\#system\-security\-cryptography\-asymmetricalgorithm\-tryexportencryptedpkcs8privatekey\(system\-readonlyspan\(\(system\-char\)\)\-system\-security\-cryptography\-pbeparameters\-system\-span\(\(system\-byte\)\)\-system\-int32@\)), 
[AsymmetricAlgorithm.TryExportEncryptedPkcs8PrivateKeyPem\(ReadOnlySpan<byte\>, PbeParameters, Span<char\>, out int\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.tryexportencryptedpkcs8privatekeypem\#system\-security\-cryptography\-asymmetricalgorithm\-tryexportencryptedpkcs8privatekeypem\(system\-readonlyspan\(\(system\-byte\)\)\-system\-security\-cryptography\-pbeparameters\-system\-span\(\(system\-char\)\)\-system\-int32@\)), 
[AsymmetricAlgorithm.TryExportEncryptedPkcs8PrivateKeyPem\(ReadOnlySpan<char\>, PbeParameters, Span<char\>, out int\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.tryexportencryptedpkcs8privatekeypem\#system\-security\-cryptography\-asymmetricalgorithm\-tryexportencryptedpkcs8privatekeypem\(system\-readonlyspan\(\(system\-char\)\)\-system\-security\-cryptography\-pbeparameters\-system\-span\(\(system\-char\)\)\-system\-int32@\)), 
[AsymmetricAlgorithm.TryExportPkcs8PrivateKey\(Span<byte\>, out int\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.tryexportpkcs8privatekey), 
[AsymmetricAlgorithm.TryExportPkcs8PrivateKeyPem\(Span<char\>, out int\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.tryexportpkcs8privatekeypem), 
[AsymmetricAlgorithm.TryExportSubjectPublicKeyInfo\(Span<byte\>, out int\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.tryexportsubjectpublickeyinfo), 
[AsymmetricAlgorithm.TryExportSubjectPublicKeyInfoPem\(Span<char\>, out int\)](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.tryexportsubjectpublickeyinfopem), 
[AsymmetricAlgorithm.KeyExchangeAlgorithm](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.keyexchangealgorithm), 
[AsymmetricAlgorithm.KeySize](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.keysize), 
[AsymmetricAlgorithm.LegalKeySizes](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.legalkeysizes), 
[AsymmetricAlgorithm.SignatureAlgorithm](https://learn.microsoft.com/dotnet/api/system.security.cryptography.asymmetricalgorithm.signaturealgorithm), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_Cryptography_Ed25519_SignatureAlgorithm"></a> SignatureAlgorithm

When implemented in a derived class, gets the name of the signature algorithm. Otherwise, always throws a <xref href="System.NotImplementedException" data-throw-if-not-resolved="false"></xref>.

```csharp
public override string? SignatureAlgorithm { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

## Methods

### <a id="DSInternals_Win32_WebAuthn_Cryptography_Ed25519_Create"></a> Create\(\)

Creates a new <xref href="DSInternals.Win32.WebAuthn.Cryptography.Ed25519" data-throw-if-not-resolved="false"></xref> instance. Import a key with <xref href="System.Security.Cryptography.AsymmetricAlgorithm.ImportFromPem(System.ReadOnlySpan%7bSystem.Char%7d)" data-throw-if-not-resolved="false"></xref>.

```csharp
public static Ed25519 Create()
```

#### Returns

 [Ed25519](DSInternals.Win32.WebAuthn.Cryptography.Ed25519.md)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_Ed25519_Dispose_System_Boolean_"></a> Dispose\(bool\)

Releases the unmanaged resources used by the <xref href="System.Security.Cryptography.AsymmetricAlgorithm" data-throw-if-not-resolved="false"></xref> class and optionally releases the managed resources.

```csharp
protected override void Dispose(bool disposing)
```

#### Parameters

`disposing` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> to release both managed and unmanaged resources; <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a> to release only unmanaged resources.

### <a id="DSInternals_Win32_WebAuthn_Cryptography_Ed25519_ImportPkcs8PrivateKey_System_ReadOnlySpan_System_Byte__System_Int32__"></a> ImportPkcs8PrivateKey\(ReadOnlySpan<byte\>, out int\)

Imports an Ed25519 private key from a PKCS#8 DER blob.
Called internally by <xref href="System.Security.Cryptography.AsymmetricAlgorithm.ImportFromPem(System.ReadOnlySpan%7bSystem.Char%7d)" data-throw-if-not-resolved="false"></xref> for "BEGIN PRIVATE KEY" blocks.

```csharp
public override void ImportPkcs8PrivateKey(ReadOnlySpan<byte> source, out int bytesRead)
```

#### Parameters

`source` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan\-1)<[byte](https://learn.microsoft.com/dotnet/api/system.byte)\>

`bytesRead` [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_Ed25519_SignData_System_ReadOnlySpan_System_Byte__System_Security_Cryptography_HashAlgorithmName_"></a> SignData\(ReadOnlySpan<byte\>, HashAlgorithmName\)

Signs the given data using Ed25519.

```csharp
public byte[] SignData(ReadOnlySpan<byte> data, HashAlgorithmName hashAlgorithm)
```

#### Parameters

`data` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan\-1)<[byte](https://learn.microsoft.com/dotnet/api/system.byte)\>

`hashAlgorithm` [HashAlgorithmName](https://learn.microsoft.com/dotnet/api/system.security.cryptography.hashalgorithmname)

#### Returns

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

