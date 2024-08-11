# <a id="DSInternals_Win32_WebAuthn_Adapter_WebAuthnApiAdapter"></a> Class WebAuthnApiAdapter

Namespace: [DSInternals.Win32.WebAuthn.Adapter](DSInternals.Win32.WebAuthn.Adapter.md)  
Assembly: DSInternals.Win32.WebAuthn.Adapter.dll  

```csharp
public class WebAuthnApiAdapter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[WebAuthnApiAdapter](DSInternals.Win32.WebAuthn.Adapter.WebAuthnApiAdapter.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="DSInternals_Win32_WebAuthn_Adapter_WebAuthnApiAdapter__ctor"></a> WebAuthnApiAdapter\(\)

```csharp
public WebAuthnApiAdapter()
```

## Methods

### <a id="DSInternals_Win32_WebAuthn_Adapter_WebAuthnApiAdapter_AuthenticatorGetAssertion_Fido2NetLib_AssertionOptions_System_Nullable_Fido2NetLib_Objects_AuthenticatorAttachment__"></a> AuthenticatorGetAssertion\(AssertionOptions, AuthenticatorAttachment?\)

Signs a challenge and other collected data into an assertion, which is used as a credential.

```csharp
public AuthenticatorAssertionRawResponse AuthenticatorGetAssertion(AssertionOptions options, AuthenticatorAttachment? authenticatorAttachment = null)
```

#### Parameters

`options` [AssertionOptions](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/b6f76420ba74f2a6a0486816f2cf36094b12fe9d/Src/Fido2.Models/AssertionOptions.cs)

Assertion options.

`authenticatorAttachment` [AuthenticatorAttachment](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/b6f76420ba74f2a6a0486816f2cf36094b12fe9d/Src/Fido2.Models/Objects/AuthenticatorAttachment.cs)?

Optionally filters the eligible authenticators by their attachment.

#### Returns

 [AuthenticatorAssertionRawResponse](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/b6f76420ba74f2a6a0486816f2cf36094b12fe9d/Src/Fido2.Models/AuthenticatorAssertionRawResponse.cs)

The cryptographically signed Authenticator Assertion Response object returned by an authenticator.

### <a id="DSInternals_Win32_WebAuthn_Adapter_WebAuthnApiAdapter_AuthenticatorGetAssertionAsync_Fido2NetLib_AssertionOptions_System_Nullable_Fido2NetLib_Objects_AuthenticatorAttachment__System_Threading_CancellationToken_"></a> AuthenticatorGetAssertionAsync\(AssertionOptions, AuthenticatorAttachment?, CancellationToken\)

```csharp
public Task<AuthenticatorAssertionRawResponse> AuthenticatorGetAssertionAsync(AssertionOptions options, AuthenticatorAttachment? authenticatorAttachment = null, CancellationToken cancellationToken = default)
```

#### Parameters

`options` [AssertionOptions](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/b6f76420ba74f2a6a0486816f2cf36094b12fe9d/Src/Fido2.Models/AssertionOptions.cs)

`authenticatorAttachment` [AuthenticatorAttachment](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/b6f76420ba74f2a6a0486816f2cf36094b12fe9d/Src/Fido2.Models/Objects/AuthenticatorAttachment.cs)?

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AuthenticatorAssertionRawResponse](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/b6f76420ba74f2a6a0486816f2cf36094b12fe9d/Src/Fido2.Models/AuthenticatorAssertionRawResponse.cs)\>

### <a id="DSInternals_Win32_WebAuthn_Adapter_WebAuthnApiAdapter_AuthenticatorMakeCredential_Fido2NetLib_CredentialCreateOptions_"></a> AuthenticatorMakeCredential\(CredentialCreateOptions\)

Creates a public key credential source bound to a managing authenticator.

```csharp
public AuthenticatorAttestationRawResponse AuthenticatorMakeCredential(CredentialCreateOptions options)
```

#### Parameters

`options` [CredentialCreateOptions](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/b6f76420ba74f2a6a0486816f2cf36094b12fe9d/Src/Fido2.Models/CredentialCreateOptions.cs)

Options for credential creation.

#### Returns

 [AuthenticatorAttestationRawResponse](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/b6f76420ba74f2a6a0486816f2cf36094b12fe9d/Src/Fido2.Models/AuthenticatorAttestationRawResponse.cs)

The credential public key associated with the credential private key.

### <a id="DSInternals_Win32_WebAuthn_Adapter_WebAuthnApiAdapter_AuthenticatorMakeCredentialAsync_Fido2NetLib_CredentialCreateOptions_System_Threading_CancellationToken_"></a> AuthenticatorMakeCredentialAsync\(CredentialCreateOptions, CancellationToken\)

```csharp
public Task<AuthenticatorAttestationRawResponse> AuthenticatorMakeCredentialAsync(CredentialCreateOptions options, CancellationToken cancellationToken = default)
```

#### Parameters

`options` [CredentialCreateOptions](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/b6f76420ba74f2a6a0486816f2cf36094b12fe9d/Src/Fido2.Models/CredentialCreateOptions.cs)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AuthenticatorAttestationRawResponse](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/b6f76420ba74f2a6a0486816f2cf36094b12fe9d/Src/Fido2.Models/AuthenticatorAttestationRawResponse.cs)\>

### <a id="DSInternals_Win32_WebAuthn_Adapter_WebAuthnApiAdapter_CancelCurrentOperation"></a> CancelCurrentOperation\(\)

```csharp
public void CancelCurrentOperation()
```

