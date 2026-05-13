# <a id="DSInternals_Win32_WebAuthn_Adapter_WebAuthnApiAdapter"></a> Class WebAuthnApiAdapter

Namespace: [DSInternals.Win32.WebAuthn.Adapter](DSInternals.Win32.WebAuthn.Adapter.md)  
Assembly: DSInternals.Win32.WebAuthn.Adapter.dll  

Adapter that bridges Fido2NetLib request/response models to the WebAuthn interop API.

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

Initializes a new instance of the adapter.

```csharp
public WebAuthnApiAdapter()
```

## Methods

### <a id="DSInternals_Win32_WebAuthn_Adapter_WebAuthnApiAdapter_AuthenticatorGetAssertion_Fido2NetLib_AssertionOptions_System_Nullable_Fido2NetLib_Objects_AuthenticatorAttachment__"></a> AuthenticatorGetAssertion\(AssertionOptions, AuthenticatorAttachment?\)

Requests a signed assertion from the authenticator confirming the user's consent to a specific transaction, such as signing in or completing a purchase.

```csharp
public AuthenticatorAssertionRawResponse AuthenticatorGetAssertion(AssertionOptions options, AuthenticatorAttachment? authenticatorAttachment = null)
```

#### Parameters

`options` [AssertionOptions](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/AssertionOptions.cs)

The Fido2NetLib assertion options describing the relying party, allowed credentials, and the desired authenticator behavior.

`authenticatorAttachment` [AuthenticatorAttachment](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/AuthenticatorAttachment.cs)?

Optionally constrains the eligible authenticators by attachment (platform or cross-platform).

#### Returns

 [AuthenticatorAssertionRawResponse](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/AuthenticatorAssertionRawResponse.cs)

The raw assertion response containing the authenticator data, signature, and user handle returned by the authenticator.

### <a id="DSInternals_Win32_WebAuthn_Adapter_WebAuthnApiAdapter_AuthenticatorGetAssertionAsync_Fido2NetLib_AssertionOptions_System_Nullable_Fido2NetLib_Objects_AuthenticatorAttachment__System_Threading_CancellationToken_"></a> AuthenticatorGetAssertionAsync\(AssertionOptions, AuthenticatorAttachment?, CancellationToken\)

Asynchronously requests a signed assertion from the authenticator confirming the user's consent to a specific transaction, such as signing in or completing a purchase.

```csharp
public Task<AuthenticatorAssertionRawResponse> AuthenticatorGetAssertionAsync(AssertionOptions options, AuthenticatorAttachment? authenticatorAttachment = null, CancellationToken cancellationToken = default)
```

#### Parameters

`options` [AssertionOptions](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/AssertionOptions.cs)

The Fido2NetLib assertion options describing the relying party, allowed credentials, and the desired authenticator behavior.

`authenticatorAttachment` [AuthenticatorAttachment](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/AuthenticatorAttachment.cs)?

Optionally constrains the eligible authenticators by attachment (platform or cross-platform).

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Token that, when canceled, signals the underlying WebAuthn operation to be canceled.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AuthenticatorAssertionRawResponse](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/AuthenticatorAssertionRawResponse.cs)\>

The raw assertion response containing the authenticator data, signature, and user handle returned by the authenticator.

### <a id="DSInternals_Win32_WebAuthn_Adapter_WebAuthnApiAdapter_AuthenticatorMakeCredential_Fido2NetLib_CredentialCreateOptions_"></a> AuthenticatorMakeCredential\(CredentialCreateOptions\)

Creates a new public key credential on the authenticator and returns the attestation that conveys its public key to the relying party.

```csharp
public AuthenticatorAttestationRawResponse AuthenticatorMakeCredential(CredentialCreateOptions options)
```

#### Parameters

`options` [CredentialCreateOptions](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/CredentialCreateOptions.cs)

The Fido2NetLib credential creation options describing the relying party, the user, and the desired authenticator behavior.

#### Returns

 [AuthenticatorAttestationRawResponse](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/AuthenticatorAttestationRawResponse.cs)

The raw attestation response that the relying party can persist to register the new credential.

### <a id="DSInternals_Win32_WebAuthn_Adapter_WebAuthnApiAdapter_AuthenticatorMakeCredentialAsync_Fido2NetLib_CredentialCreateOptions_System_Threading_CancellationToken_"></a> AuthenticatorMakeCredentialAsync\(CredentialCreateOptions, CancellationToken\)

Asynchronously creates a new public key credential on the authenticator and returns the attestation that conveys its public key to the relying party.

```csharp
public Task<AuthenticatorAttestationRawResponse> AuthenticatorMakeCredentialAsync(CredentialCreateOptions options, CancellationToken cancellationToken = default)
```

#### Parameters

`options` [CredentialCreateOptions](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/CredentialCreateOptions.cs)

The Fido2NetLib credential creation options describing the relying party, the user, and the desired authenticator behavior.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Token that, when canceled, signals the underlying WebAuthn operation to be canceled.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AuthenticatorAttestationRawResponse](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/AuthenticatorAttestationRawResponse.cs)\>

The raw attestation response that the relying party can persist to register the new credential.

### <a id="DSInternals_Win32_WebAuthn_Adapter_WebAuthnApiAdapter_CancelCurrentOperation"></a> CancelCurrentOperation\(\)

Requests cancellation of the currently running WebAuthn operation.

```csharp
public void CancelCurrentOperation()
```

