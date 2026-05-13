# <a id="DSInternals_Win32_WebAuthn_AttestationPublicKeyCredential"></a> Class AttestationPublicKeyCredential

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents the public key credential returned by a WebAuthn attestation operation.

```csharp
public sealed class AttestationPublicKeyCredential : PublicKeyCredential<AuthenticatorAttestationResponse, AuthenticationExtensionsClientAttestationOutputs>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PublicKeyCredential<AuthenticatorAttestationResponse, AuthenticationExtensionsClientAttestationOutputs\>](DSInternals.Win32.WebAuthn.PublicKeyCredential\-2.md) ← 
[AttestationPublicKeyCredential](DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential.md)

#### Inherited Members

[PublicKeyCredential<AuthenticatorAttestationResponse, AuthenticationExtensionsClientAttestationOutputs\>.Id](DSInternals.Win32.WebAuthn.PublicKeyCredential\-2.md\#DSInternals\_Win32\_WebAuthn\_PublicKeyCredential\_2\_Id), 
[PublicKeyCredential<AuthenticatorAttestationResponse, AuthenticationExtensionsClientAttestationOutputs\>.RawId](DSInternals.Win32.WebAuthn.PublicKeyCredential\-2.md\#DSInternals\_Win32\_WebAuthn\_PublicKeyCredential\_2\_RawId), 
[PublicKeyCredential<AuthenticatorAttestationResponse, AuthenticationExtensionsClientAttestationOutputs\>.Type](DSInternals.Win32.WebAuthn.PublicKeyCredential\-2.md\#DSInternals\_Win32\_WebAuthn\_PublicKeyCredential\_2\_Type), 
[PublicKeyCredential<AuthenticatorAttestationResponse, AuthenticationExtensionsClientAttestationOutputs\>.AuthenticatorAttachment](DSInternals.Win32.WebAuthn.PublicKeyCredential\-2.md\#DSInternals\_Win32\_WebAuthn\_PublicKeyCredential\_2\_AuthenticatorAttachment), 
[PublicKeyCredential<AuthenticatorAttestationResponse, AuthenticationExtensionsClientAttestationOutputs\>.Response](DSInternals.Win32.WebAuthn.PublicKeyCredential\-2.md\#DSInternals\_Win32\_WebAuthn\_PublicKeyCredential\_2\_Response), 
[PublicKeyCredential<AuthenticatorAttestationResponse, AuthenticationExtensionsClientAttestationOutputs\>.ClientExtensionResults](DSInternals.Win32.WebAuthn.PublicKeyCredential\-2.md\#DSInternals\_Win32\_WebAuthn\_PublicKeyCredential\_2\_ClientExtensionResults), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="DSInternals_Win32_WebAuthn_AttestationPublicKeyCredential_FromJson_System_String_"></a> FromJson\(string\)

Deserializes a JSON string into an attestation public key credential.

```csharp
public static AttestationPublicKeyCredential? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of an attestation public key credential.

#### Returns

 [AttestationPublicKeyCredential](DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential.md)?

An attestation credential if deserialization is successful; otherwise, null.

### <a id="DSInternals_Win32_WebAuthn_AttestationPublicKeyCredential_ToString"></a> ToString\(\)

Serializes the credential to JSON.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of this credential.

