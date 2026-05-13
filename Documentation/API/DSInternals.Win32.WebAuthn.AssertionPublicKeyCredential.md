# <a id="DSInternals_Win32_WebAuthn_AssertionPublicKeyCredential"></a> Class AssertionPublicKeyCredential

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents the public key credential returned by a WebAuthn assertion operation.

```csharp
public sealed class AssertionPublicKeyCredential : PublicKeyCredential<AuthenticatorAssertionResponse, AuthenticationExtensionsClientAssertionOutputs>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PublicKeyCredential<AuthenticatorAssertionResponse, AuthenticationExtensionsClientAssertionOutputs\>](DSInternals.Win32.WebAuthn.PublicKeyCredential\-2.md) ← 
[AssertionPublicKeyCredential](DSInternals.Win32.WebAuthn.AssertionPublicKeyCredential.md)

#### Inherited Members

[PublicKeyCredential<AuthenticatorAssertionResponse, AuthenticationExtensionsClientAssertionOutputs\>.Id](DSInternals.Win32.WebAuthn.PublicKeyCredential\-2.md\#DSInternals\_Win32\_WebAuthn\_PublicKeyCredential\_2\_Id), 
[PublicKeyCredential<AuthenticatorAssertionResponse, AuthenticationExtensionsClientAssertionOutputs\>.RawId](DSInternals.Win32.WebAuthn.PublicKeyCredential\-2.md\#DSInternals\_Win32\_WebAuthn\_PublicKeyCredential\_2\_RawId), 
[PublicKeyCredential<AuthenticatorAssertionResponse, AuthenticationExtensionsClientAssertionOutputs\>.Type](DSInternals.Win32.WebAuthn.PublicKeyCredential\-2.md\#DSInternals\_Win32\_WebAuthn\_PublicKeyCredential\_2\_Type), 
[PublicKeyCredential<AuthenticatorAssertionResponse, AuthenticationExtensionsClientAssertionOutputs\>.AuthenticatorAttachment](DSInternals.Win32.WebAuthn.PublicKeyCredential\-2.md\#DSInternals\_Win32\_WebAuthn\_PublicKeyCredential\_2\_AuthenticatorAttachment), 
[PublicKeyCredential<AuthenticatorAssertionResponse, AuthenticationExtensionsClientAssertionOutputs\>.Response](DSInternals.Win32.WebAuthn.PublicKeyCredential\-2.md\#DSInternals\_Win32\_WebAuthn\_PublicKeyCredential\_2\_Response), 
[PublicKeyCredential<AuthenticatorAssertionResponse, AuthenticationExtensionsClientAssertionOutputs\>.ClientExtensionResults](DSInternals.Win32.WebAuthn.PublicKeyCredential\-2.md\#DSInternals\_Win32\_WebAuthn\_PublicKeyCredential\_2\_ClientExtensionResults), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="DSInternals_Win32_WebAuthn_AssertionPublicKeyCredential_FromJson_System_String_"></a> FromJson\(string\)

Deserializes a JSON string into an assertion public key credential.

```csharp
public static AssertionPublicKeyCredential? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of an assertion public key credential.

#### Returns

 [AssertionPublicKeyCredential](DSInternals.Win32.WebAuthn.AssertionPublicKeyCredential.md)?

An assertion credential if deserialization is successful; otherwise, null.

### <a id="DSInternals_Win32_WebAuthn_AssertionPublicKeyCredential_ToString"></a> ToString\(\)

Serializes the credential to JSON.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of this credential.

