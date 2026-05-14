# <a id="DSInternals_Win32_WebAuthn_WebAuthnApi"></a> Class WebAuthnApi

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Windows WebAuthn API

```csharp
public class WebAuthnApi
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[WebAuthnApi](DSInternals.Win32.WebAuthn.WebAuthnApi.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Requires Windows 10 1903+ to work.

## Constructors

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi__ctor"></a> WebAuthnApi\(\)

Initializes a new instance of the <xref href="DSInternals.Win32.WebAuthn.WebAuthnApi" data-throw-if-not-resolved="false"></xref> class.

```csharp
public WebAuthnApi()
```

## Properties

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_ApiVersion"></a> ApiVersion

Gets the API version information.

```csharp
public static ApiVersion? ApiVersion { get; }
```

#### Property Value

 [ApiVersion](DSInternals.Win32.WebAuthn.ApiVersion.md)?

#### Remarks

Indicates the presence of APIs and features.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_IsAuthenticatorListSupported"></a> IsAuthenticatorListSupported

Indicates the availability of the authenticator list API.

```csharp
public static bool IsAuthenticatorListSupported { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Remarks

Support for the authenticator list API was added in V9 API.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_IsAvailable"></a> IsAvailable

Indicates the availability of the WebAuthn API.

```csharp
public static bool IsAvailable { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_IsCancellationSupported"></a> IsCancellationSupported

Indicates whether operation cancellation is supported by the API.

```csharp
public bool IsCancellationSupported { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_IsCredBlobSupported"></a> IsCredBlobSupported

Indicates the availability of the Credential Blob extension.

```csharp
public static bool IsCredBlobSupported { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Remarks

Support for the credBlob extension was added in V3 API.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_IsCredProtectExtensionSupported"></a> IsCredProtectExtensionSupported

Indicates the availability of the Credential Protection extension.

```csharp
public static bool IsCredProtectExtensionSupported { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Remarks

Support for the credProtect extension was added in V2 API.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_IsEnterpriseAttestationSupported"></a> IsEnterpriseAttestationSupported

Indicates the availability of enterprise attestation.

```csharp
public static bool IsEnterpriseAttestationSupported { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Remarks

Support for the enterprise attestation was added in V3 API.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_IsHybridStorageLinkedDataSupported"></a> IsHybridStorageLinkedDataSupported

Indicates the support for linked device data.

```csharp
public static bool IsHybridStorageLinkedDataSupported { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Remarks

Support for linked device data was added in V7 API.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_IsLargeBlobSupported"></a> IsLargeBlobSupported

Indicates the availability of the large blobs.

```csharp
public static bool IsLargeBlobSupported { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Remarks

Support for the large blobs was added in V5 API.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_IsMinPinLengthSupported"></a> IsMinPinLengthSupported

Indicates the availability of the minimum PIN length extension.

```csharp
public static bool IsMinPinLengthSupported { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Remarks

Support for the minPinLength extension was added in V3 API.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_IsPlatformCredentialManagementSupported"></a> IsPlatformCredentialManagementSupported

Indicates the availability of the API for platform credential management.

```csharp
public static bool IsPlatformCredentialManagementSupported { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Remarks

Support for platform credential management was added in V4 API.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_IsPrivateBrowserModeIndicatorSupported"></a> IsPrivateBrowserModeIndicatorSupported

Indicates the API can differentiate between browser modes.

```csharp
public static bool IsPrivateBrowserModeIndicatorSupported { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Remarks

Support for the browser mode indicator was added in V5 API.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_IsPsuedoRandomFunctionSupported"></a> IsPsuedoRandomFunctionSupported

Indicates the availability of the psuedo-random function (PRF) extension.

```csharp
public static bool IsPsuedoRandomFunctionSupported { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Remarks

Support for the prf extension was added in V6 API.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_IsPublicKeyCredentialHintSupported"></a> IsPublicKeyCredentialHintSupported

Indicates the availability of the public key credential hints extension.

```csharp
public static bool IsPublicKeyCredentialHintSupported { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Remarks

Support for credential hints was added in V8 API.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_IsRemoteWebOriginSupported"></a> IsRemoteWebOriginSupported

Indicates the availability of remote web origin support for proxied WebAuthn requests.

```csharp
public static bool IsRemoteWebOriginSupported { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Remarks

Support for remote web origin was added in V9 API.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_IsUnsignedExtensionOutputSupported"></a> IsUnsignedExtensionOutputSupported

Indicates the support for unsigned extension outputs.

```csharp
public static bool IsUnsignedExtensionOutputSupported { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Remarks

Support for the unsigned extension outputs was added in V7 API.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_IsUserVerifyingPlatformAuthenticatorAvailable"></a> IsUserVerifyingPlatformAuthenticatorAvailable

Indicates the availability of user-verifying platform authenticator (e.g. Windows Hello).

```csharp
public static bool IsUserVerifyingPlatformAuthenticatorAvailable { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_AuthenticatorGetAssertion_DSInternals_Win32_WebAuthn_PublicKeyCredentialRequestOptions_DSInternals_Win32_WebAuthn_WindowHandle_"></a> AuthenticatorGetAssertion\(PublicKeyCredentialRequestOptions, WindowHandle\)

Requests a signed assertion from the authenticator confirming the user's consent to a specific transaction, such as signing in or completing a purchase.

```csharp
public AssertionPublicKeyCredential AuthenticatorGetAssertion(PublicKeyCredentialRequestOptions options, WindowHandle windowHandle = default)
```

#### Parameters

`options` [PublicKeyCredentialRequestOptions](DSInternals.Win32.WebAuthn.PublicKeyCredentialRequestOptions.md)

The credential request options that describe the relying party, allowed credentials, and the desired authenticator behavior.

`windowHandle` [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

Handle to the window that will own the authenticator UI. When invalid, the foreground window is used.

#### Returns

 [AssertionPublicKeyCredential](DSInternals.Win32.WebAuthn.AssertionPublicKeyCredential.md)

The signed assertion public key credential produced by the authenticator.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_AuthenticatorGetAssertion_System_String_System_Byte___DSInternals_Win32_WebAuthn_UserVerificationRequirement_DSInternals_Win32_WebAuthn_AuthenticatorAttachment_System_UInt32_System_Collections_Generic_IReadOnlyList_DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor__DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionInputs_System_Boolean_DSInternals_Win32_WebAuthn_Interop_HybridStorageLinkedData_System_Boolean_System_String___System_Byte___System_Byte___DSInternals_Win32_WebAuthn_WindowHandle_"></a> AuthenticatorGetAssertion\(string, byte\[\], UserVerificationRequirement, AuthenticatorAttachment, uint, IReadOnlyList<PublicKeyCredentialDescriptor\>?, AuthenticationExtensionsClientAssertionInputs?, bool, HybridStorageLinkedData?, bool, string\[\]?, byte\[\]?, byte\[\]?, WindowHandle\)

Requests a signed assertion from the authenticator confirming the user's consent to a specific transaction, such as signing in or completing a purchase.

```csharp
public AssertionPublicKeyCredential AuthenticatorGetAssertion(string rpId, byte[] challenge, UserVerificationRequirement userVerificationRequirement, AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any, uint timeoutMilliseconds = 60000, IReadOnlyList<PublicKeyCredentialDescriptor>? allowCredentials = null, AuthenticationExtensionsClientAssertionInputs? extensions = null, bool browserInPrivateMode = false, HybridStorageLinkedData? linkedDevice = null, bool autoFill = false, string[]? credentialHints = null, byte[]? authenticatorId = null, byte[]? publicKeyCredentialRequestOptionsJson = null, WindowHandle windowHandle = default)
```

#### Parameters

`rpId` [string](https://learn.microsoft.com/dotnet/api/system.string)

Identifier of the relying party requesting the assertion.

`challenge` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

Cryptographic challenge produced by the relying party to be signed by the authenticator.

`userVerificationRequirement` [UserVerificationRequirement](DSInternals.Win32.WebAuthn.UserVerificationRequirement.md)

Indicates whether user verification is required, preferred, or discouraged.

`authenticatorAttachment` [AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)

Constrains the type of authenticator that may be used (platform, cross-platform, or any).

`timeoutMilliseconds` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Timeout, in milliseconds, that the client should wait for the authenticator to complete the operation.

`allowCredentials` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)\>?

Optional list of credentials acceptable to the relying party for the assertion.

`extensions` [AuthenticationExtensionsClientAssertionInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAssertionInputs.md)?

Client extension inputs for the assertion operation.

`browserInPrivateMode` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Indicates whether the request originates from a browser running in private/incognito mode.

`linkedDevice` [HybridStorageLinkedData](DSInternals.Win32.WebAuthn.Interop.HybridStorageLinkedData.md)?

Optional hybrid (cross-device) storage linked data for state-assisted transactions.

`autoFill` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Indicates whether the request is a conditional UI (autofill) request.

`credentialHints` [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]?

Optional ordered list of public key credential hints describing the modality the relying party prefers.

`authenticatorId` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

Optional identifier of a specific authenticator to target.

`publicKeyCredentialRequestOptionsJson` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

Optional UTF-8 encoded JSON representation of the original request options, forwarded to the authenticator.

`windowHandle` [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

Handle to the window that will own the authenticator UI. When invalid, the foreground window is used.

#### Returns

 [AssertionPublicKeyCredential](DSInternals.Win32.WebAuthn.AssertionPublicKeyCredential.md)

The signed assertion public key credential produced by the authenticator.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_AuthenticatorGetAssertion_System_String_DSInternals_Win32_WebAuthn_FIDO_CollectedClientData_DSInternals_Win32_WebAuthn_UserVerificationRequirement_DSInternals_Win32_WebAuthn_AuthenticatorAttachment_System_UInt32_System_Collections_Generic_IReadOnlyList_DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor__DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionInputs_System_Boolean_DSInternals_Win32_WebAuthn_Interop_HybridStorageLinkedData_System_Boolean_System_String___System_Byte___System_Byte___DSInternals_Win32_WebAuthn_WindowHandle_"></a> AuthenticatorGetAssertion\(string, CollectedClientData, UserVerificationRequirement, AuthenticatorAttachment, uint, IReadOnlyList<PublicKeyCredentialDescriptor\>?, AuthenticationExtensionsClientAssertionInputs?, bool, HybridStorageLinkedData?, bool, string\[\]?, byte\[\]?, byte\[\]?, WindowHandle\)

Requests a signed assertion from the authenticator confirming the user's consent to a specific transaction, such as signing in or completing a purchase.

```csharp
public AssertionPublicKeyCredential AuthenticatorGetAssertion(string rpId, CollectedClientData clientData, UserVerificationRequirement userVerificationRequirement, AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any, uint timeoutMilliseconds = 60000, IReadOnlyList<PublicKeyCredentialDescriptor>? allowCredentials = null, AuthenticationExtensionsClientAssertionInputs? extensions = null, bool browserInPrivateMode = false, HybridStorageLinkedData? linkedDevice = null, bool autoFill = false, string[]? credentialHints = null, byte[]? authenticatorId = null, byte[]? publicKeyCredentialRequestOptionsJson = null, WindowHandle windowHandle = default)
```

#### Parameters

`rpId` [string](https://learn.microsoft.com/dotnet/api/system.string)

Identifier of the relying party requesting the assertion.

`clientData` [CollectedClientData](DSInternals.Win32.WebAuthn.FIDO.CollectedClientData.md)

The client data that contains the challenge, type, origin, and related context to be signed by the authenticator.

`userVerificationRequirement` [UserVerificationRequirement](DSInternals.Win32.WebAuthn.UserVerificationRequirement.md)

Indicates whether user verification is required, preferred, or discouraged.

`authenticatorAttachment` [AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)

Constrains the type of authenticator that may be used (platform, cross-platform, or any).

`timeoutMilliseconds` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Timeout, in milliseconds, that the client should wait for the authenticator to complete the operation.

`allowCredentials` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)\>?

Optional list of credentials acceptable to the relying party for the assertion.

`extensions` [AuthenticationExtensionsClientAssertionInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAssertionInputs.md)?

Client extension inputs for the assertion operation.

`browserInPrivateMode` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Indicates whether the request originates from a browser running in private/incognito mode.

`linkedDevice` [HybridStorageLinkedData](DSInternals.Win32.WebAuthn.Interop.HybridStorageLinkedData.md)?

Optional hybrid (cross-device) storage linked data for state-assisted transactions.

`autoFill` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Indicates whether the request is a conditional UI (autofill) request.

`credentialHints` [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]?

Optional ordered list of public key credential hints describing the modality the relying party prefers.

`authenticatorId` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

Optional identifier of a specific authenticator to target.

`publicKeyCredentialRequestOptionsJson` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

Optional UTF-8 encoded JSON representation of the original request options, forwarded to the authenticator.

`windowHandle` [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

Handle to the window that will own the authenticator UI. When invalid, the foreground window is used.

#### Returns

 [AssertionPublicKeyCredential](DSInternals.Win32.WebAuthn.AssertionPublicKeyCredential.md)

The signed assertion public key credential produced by the authenticator.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_AuthenticatorGetAssertionAsync_DSInternals_Win32_WebAuthn_PublicKeyCredentialRequestOptions_DSInternals_Win32_WebAuthn_WindowHandle_System_Threading_CancellationToken_"></a> AuthenticatorGetAssertionAsync\(PublicKeyCredentialRequestOptions, WindowHandle, CancellationToken\)

Requests a signed assertion from the authenticator confirming the user's consent to a specific transaction, such as signing in or completing a purchase.

```csharp
public Task<AssertionPublicKeyCredential> AuthenticatorGetAssertionAsync(PublicKeyCredentialRequestOptions options, WindowHandle windowHandle = default, CancellationToken cancellationToken = default)
```

#### Parameters

`options` [PublicKeyCredentialRequestOptions](DSInternals.Win32.WebAuthn.PublicKeyCredentialRequestOptions.md)

The credential request options that describe the relying party, allowed credentials, and the desired authenticator behavior.

`windowHandle` [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

Handle to the window that will own the authenticator UI. When invalid, the foreground window is used.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Token that, when canceled, signals the underlying WebAuthn operation to be canceled.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AssertionPublicKeyCredential](DSInternals.Win32.WebAuthn.AssertionPublicKeyCredential.md)\>

A task that completes with the signed assertion public key credential produced by the authenticator.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_AuthenticatorGetAssertionAsync_System_String_System_Byte___DSInternals_Win32_WebAuthn_UserVerificationRequirement_DSInternals_Win32_WebAuthn_AuthenticatorAttachment_System_UInt32_System_Collections_Generic_IReadOnlyList_DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor__DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionInputs_System_Boolean_DSInternals_Win32_WebAuthn_Interop_HybridStorageLinkedData_System_Boolean_System_String___System_Byte___System_Byte___DSInternals_Win32_WebAuthn_WindowHandle_System_Threading_CancellationToken_"></a> AuthenticatorGetAssertionAsync\(string, byte\[\], UserVerificationRequirement, AuthenticatorAttachment, uint, IReadOnlyList<PublicKeyCredentialDescriptor\>?, AuthenticationExtensionsClientAssertionInputs?, bool, HybridStorageLinkedData?, bool, string\[\]?, byte\[\]?, byte\[\]?, WindowHandle, CancellationToken\)

Requests a signed assertion from the authenticator confirming the user's consent to a specific transaction, such as signing in or completing a purchase.

```csharp
public Task<AssertionPublicKeyCredential> AuthenticatorGetAssertionAsync(string rpId, byte[] challenge, UserVerificationRequirement userVerificationRequirement, AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any, uint timeoutMilliseconds = 60000, IReadOnlyList<PublicKeyCredentialDescriptor>? allowCredentials = null, AuthenticationExtensionsClientAssertionInputs? extensions = null, bool browserInPrivateMode = false, HybridStorageLinkedData? linkedDevice = null, bool autoFill = false, string[]? credentialHints = null, byte[]? authenticatorId = null, byte[]? publicKeyCredentialRequestOptionsJson = null, WindowHandle windowHandle = default, CancellationToken cancellationToken = default)
```

#### Parameters

`rpId` [string](https://learn.microsoft.com/dotnet/api/system.string)

Identifier of the relying party requesting the assertion.

`challenge` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

Cryptographic challenge produced by the relying party to be signed by the authenticator.

`userVerificationRequirement` [UserVerificationRequirement](DSInternals.Win32.WebAuthn.UserVerificationRequirement.md)

Indicates whether user verification is required, preferred, or discouraged.

`authenticatorAttachment` [AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)

Constrains the type of authenticator that may be used (platform, cross-platform, or any).

`timeoutMilliseconds` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Timeout, in milliseconds, that the client should wait for the authenticator to complete the operation.

`allowCredentials` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)\>?

Optional list of credentials acceptable to the relying party for the assertion.

`extensions` [AuthenticationExtensionsClientAssertionInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAssertionInputs.md)?

Client extension inputs for the assertion operation.

`browserInPrivateMode` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Indicates whether the request originates from a browser running in private/incognito mode.

`linkedDevice` [HybridStorageLinkedData](DSInternals.Win32.WebAuthn.Interop.HybridStorageLinkedData.md)?

Optional hybrid (cross-device) storage linked data for state-assisted transactions.

`autoFill` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Indicates whether the request is a conditional UI (autofill) request.

`credentialHints` [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]?

Optional ordered list of public key credential hints describing the modality the relying party prefers.

`authenticatorId` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

Optional identifier of a specific authenticator to target.

`publicKeyCredentialRequestOptionsJson` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

Optional UTF-8 encoded JSON representation of the original request options, forwarded to the authenticator.

`windowHandle` [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

Handle to the window that will own the authenticator UI. When invalid, the foreground window is used.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Token that, when canceled, signals the underlying WebAuthn operation to be canceled.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AssertionPublicKeyCredential](DSInternals.Win32.WebAuthn.AssertionPublicKeyCredential.md)\>

A task that completes with the signed assertion public key credential produced by the authenticator.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_AuthenticatorGetAssertionAsync_System_String_DSInternals_Win32_WebAuthn_FIDO_CollectedClientData_DSInternals_Win32_WebAuthn_UserVerificationRequirement_DSInternals_Win32_WebAuthn_AuthenticatorAttachment_System_UInt32_System_Collections_Generic_IReadOnlyList_DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor__DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionInputs_System_Boolean_DSInternals_Win32_WebAuthn_Interop_HybridStorageLinkedData_System_Boolean_System_String___System_Byte___System_Byte___DSInternals_Win32_WebAuthn_WindowHandle_System_Threading_CancellationToken_"></a> AuthenticatorGetAssertionAsync\(string, CollectedClientData, UserVerificationRequirement, AuthenticatorAttachment, uint, IReadOnlyList<PublicKeyCredentialDescriptor\>?, AuthenticationExtensionsClientAssertionInputs?, bool, HybridStorageLinkedData?, bool, string\[\]?, byte\[\]?, byte\[\]?, WindowHandle, CancellationToken\)

Requests a signed assertion from the authenticator confirming the user's consent to a specific transaction, such as signing in or completing a purchase.

```csharp
public Task<AssertionPublicKeyCredential> AuthenticatorGetAssertionAsync(string rpId, CollectedClientData clientData, UserVerificationRequirement userVerificationRequirement, AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any, uint timeoutMilliseconds = 60000, IReadOnlyList<PublicKeyCredentialDescriptor>? allowCredentials = null, AuthenticationExtensionsClientAssertionInputs? extensions = null, bool browserInPrivateMode = false, HybridStorageLinkedData? linkedDevice = null, bool autoFill = false, string[]? credentialHints = null, byte[]? authenticatorId = null, byte[]? publicKeyCredentialRequestOptionsJson = null, WindowHandle windowHandle = default, CancellationToken cancellationToken = default)
```

#### Parameters

`rpId` [string](https://learn.microsoft.com/dotnet/api/system.string)

Identifier of the relying party requesting the assertion.

`clientData` [CollectedClientData](DSInternals.Win32.WebAuthn.FIDO.CollectedClientData.md)

The client data that contains the challenge, type, origin, and related context to be signed by the authenticator.

`userVerificationRequirement` [UserVerificationRequirement](DSInternals.Win32.WebAuthn.UserVerificationRequirement.md)

Indicates whether user verification is required, preferred, or discouraged.

`authenticatorAttachment` [AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)

Constrains the type of authenticator that may be used (platform, cross-platform, or any).

`timeoutMilliseconds` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Timeout, in milliseconds, that the client should wait for the authenticator to complete the operation.

`allowCredentials` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)\>?

Optional list of credentials acceptable to the relying party for the assertion.

`extensions` [AuthenticationExtensionsClientAssertionInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAssertionInputs.md)?

Client extension inputs for the assertion operation.

`browserInPrivateMode` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Indicates whether the request originates from a browser running in private/incognito mode.

`linkedDevice` [HybridStorageLinkedData](DSInternals.Win32.WebAuthn.Interop.HybridStorageLinkedData.md)?

Optional hybrid (cross-device) storage linked data for state-assisted transactions.

`autoFill` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Indicates whether the request is a conditional UI (autofill) request.

`credentialHints` [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]?

Optional ordered list of public key credential hints describing the modality the relying party prefers.

`authenticatorId` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

Optional identifier of a specific authenticator to target.

`publicKeyCredentialRequestOptionsJson` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

Optional UTF-8 encoded JSON representation of the original request options, forwarded to the authenticator.

`windowHandle` [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

Handle to the window that will own the authenticator UI. When invalid, the foreground window is used.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Token that, when canceled, signals the underlying WebAuthn operation to be canceled.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AssertionPublicKeyCredential](DSInternals.Win32.WebAuthn.AssertionPublicKeyCredential.md)\>

A task that completes with the signed assertion public key credential produced by the authenticator.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_AuthenticatorMakeCredential_DSInternals_Win32_WebAuthn_PublicKeyCredentialCreationOptions_System_String_DSInternals_Win32_WebAuthn_WindowHandle_"></a> AuthenticatorMakeCredential\(PublicKeyCredentialCreationOptions, string?, WindowHandle\)

Creates a new public key credential on the authenticator and returns the attestation that conveys its public key to the relying party.

```csharp
public AttestationPublicKeyCredential AuthenticatorMakeCredential(PublicKeyCredentialCreationOptions options, string? hostName = null, WindowHandle windowHandle = default)
```

#### Parameters

`options` [PublicKeyCredentialCreationOptions](DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions.md)

The credential creation options that describe the relying party, the user, and the desired authenticator behavior.

`hostName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional host name used to derive the WebAuthn origin and to fill in a missing relying party identifier.
Useful for relying parties (such as Okta) that omit <code>rp.id</code> from server-issued creation options.
When <xref href="DSInternals.Win32.WebAuthn.RelyingPartyInformation.Id" data-throw-if-not-resolved="false"></xref> is null or empty, the host name is used as the relying party identifier sent to the authenticator.

`windowHandle` [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

Handle to the window that will own the authenticator UI. When invalid, the foreground window is used.

#### Returns

 [AttestationPublicKeyCredential](DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential.md)

The attestation public key credential produced by the authenticator.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_AuthenticatorMakeCredential_DSInternals_Win32_WebAuthn_RelyingPartyInformation_DSInternals_Win32_WebAuthn_UserInformation_System_Byte___DSInternals_Win32_WebAuthn_UserVerificationRequirement_DSInternals_Win32_WebAuthn_AuthenticatorAttachment_DSInternals_Win32_WebAuthn_ResidentKeyRequirement_DSInternals_Win32_WebAuthn_COSE_Algorithm___DSInternals_Win32_WebAuthn_AttestationConveyancePreference_System_UInt32_System_Collections_Generic_IReadOnlyList_DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor__DSInternals_Win32_WebAuthn_EnterpriseAttestationType_DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationInputs_System_Boolean_DSInternals_Win32_WebAuthn_Interop_HybridStorageLinkedData_System_String___System_Byte___System_Byte___System_String_DSInternals_Win32_WebAuthn_WindowHandle_"></a> AuthenticatorMakeCredential\(RelyingPartyInformation, UserInformation, byte\[\], UserVerificationRequirement, AuthenticatorAttachment, ResidentKeyRequirement, Algorithm\[\]?, AttestationConveyancePreference, uint, IReadOnlyList<PublicKeyCredentialDescriptor\>?, EnterpriseAttestationType, AuthenticationExtensionsClientAttestationInputs?, bool, HybridStorageLinkedData?, string\[\]?, byte\[\]?, byte\[\]?, string?, WindowHandle\)

Creates a new public key credential on the authenticator and returns the attestation that conveys its public key to the relying party.

```csharp
public AttestationPublicKeyCredential AuthenticatorMakeCredential(RelyingPartyInformation rpEntity, UserInformation userEntity, byte[] challenge, UserVerificationRequirement userVerificationRequirement, AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any, ResidentKeyRequirement residentKey = ResidentKeyRequirement.Preferred, Algorithm[]? pubKeyCredParams = null, AttestationConveyancePreference attestationConveyancePreference = AttestationConveyancePreference.Any, uint timeoutMilliseconds = 60000, IReadOnlyList<PublicKeyCredentialDescriptor>? excludeCredentials = null, EnterpriseAttestationType enterpriseAttestation = EnterpriseAttestationType.None, AuthenticationExtensionsClientAttestationInputs? extensions = null, bool browserInPrivateMode = false, HybridStorageLinkedData? linkedDevice = null, string[]? credentialHints = null, byte[]? authenticatorId = null, byte[]? publicKeyCredentialCreationOptionsJson = null, string? hostName = null, WindowHandle windowHandle = default)
```

#### Parameters

`rpEntity` [RelyingPartyInformation](DSInternals.Win32.WebAuthn.RelyingPartyInformation.md)

Information about the relying party for which the credential is being created.

`userEntity` [UserInformation](DSInternals.Win32.WebAuthn.UserInformation.md)

Information about the user account the credential will be bound to.

`challenge` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

Cryptographic challenge produced by the relying party to be signed by the authenticator.

`userVerificationRequirement` [UserVerificationRequirement](DSInternals.Win32.WebAuthn.UserVerificationRequirement.md)

Indicates whether user verification is required, preferred, or discouraged.

`authenticatorAttachment` [AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)

Constrains the type of authenticator that may be used (platform, cross-platform, or any).

`residentKey` [ResidentKeyRequirement](DSInternals.Win32.WebAuthn.ResidentKeyRequirement.md)

Indicates whether the credential should be created as a discoverable (resident) credential.

`pubKeyCredParams` [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)\[\]?

Ordered list of supported COSE algorithms for the new credential. Defaults to ES256 when null or empty.

`attestationConveyancePreference` [AttestationConveyancePreference](DSInternals.Win32.WebAuthn.AttestationConveyancePreference.md)

Specifies how the relying party wants attestation to be conveyed.

`timeoutMilliseconds` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Timeout, in milliseconds, that the client should wait for the authenticator to complete the operation.

`excludeCredentials` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)\>?

Credentials that the authenticator must not create a new credential for. Used to prevent duplicate registrations.

`enterpriseAttestation` [EnterpriseAttestationType](DSInternals.Win32.WebAuthn.EnterpriseAttestationType.md)

Indicates whether enterprise attestation is requested and at what level.

`extensions` [AuthenticationExtensionsClientAttestationInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAttestationInputs.md)?

Client extension inputs for the credential creation operation.

`browserInPrivateMode` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Indicates whether the request originates from a browser running in private/incognito mode.

`linkedDevice` [HybridStorageLinkedData](DSInternals.Win32.WebAuthn.Interop.HybridStorageLinkedData.md)?

Optional hybrid (cross-device) storage linked data for state-assisted transactions.

`credentialHints` [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]?

Optional ordered list of public key credential hints describing the modality the relying party prefers.

`authenticatorId` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

Optional identifier of a specific authenticator to target.

`publicKeyCredentialCreationOptionsJson` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

Optional UTF-8 encoded JSON representation of the original creation options, forwarded to the authenticator.

`hostName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional host name used for client data construction; defaults to the relying party identifier.

`windowHandle` [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

Handle to the window that will own the authenticator UI. When invalid, the foreground window is used.

#### Returns

 [AttestationPublicKeyCredential](DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential.md)

The attestation public key credential produced by the authenticator.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_AuthenticatorMakeCredential_DSInternals_Win32_WebAuthn_RelyingPartyInformation_DSInternals_Win32_WebAuthn_UserInformation_DSInternals_Win32_WebAuthn_FIDO_CollectedClientData_DSInternals_Win32_WebAuthn_UserVerificationRequirement_DSInternals_Win32_WebAuthn_AuthenticatorAttachment_DSInternals_Win32_WebAuthn_ResidentKeyRequirement_DSInternals_Win32_WebAuthn_COSE_Algorithm___DSInternals_Win32_WebAuthn_AttestationConveyancePreference_System_UInt32_System_Collections_Generic_IReadOnlyList_DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor__DSInternals_Win32_WebAuthn_EnterpriseAttestationType_DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationInputs_System_Boolean_DSInternals_Win32_WebAuthn_Interop_HybridStorageLinkedData_System_String___System_Byte___System_Byte___DSInternals_Win32_WebAuthn_WindowHandle_"></a> AuthenticatorMakeCredential\(RelyingPartyInformation, UserInformation, CollectedClientData, UserVerificationRequirement, AuthenticatorAttachment, ResidentKeyRequirement, Algorithm\[\]?, AttestationConveyancePreference, uint, IReadOnlyList<PublicKeyCredentialDescriptor\>?, EnterpriseAttestationType, AuthenticationExtensionsClientAttestationInputs?, bool, HybridStorageLinkedData?, string\[\]?, byte\[\]?, byte\[\]?, WindowHandle\)

Creates a new public key credential on the authenticator and returns the attestation that conveys its public key to the relying party.

```csharp
public AttestationPublicKeyCredential AuthenticatorMakeCredential(RelyingPartyInformation rpEntity, UserInformation userEntity, CollectedClientData clientData, UserVerificationRequirement userVerificationRequirement, AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any, ResidentKeyRequirement residentKey = ResidentKeyRequirement.Preferred, Algorithm[]? pubKeyCredParams = null, AttestationConveyancePreference attestationConveyancePreference = AttestationConveyancePreference.Any, uint timeoutMilliseconds = 60000, IReadOnlyList<PublicKeyCredentialDescriptor>? excludeCredentials = null, EnterpriseAttestationType enterpriseAttestation = EnterpriseAttestationType.None, AuthenticationExtensionsClientAttestationInputs? extensions = null, bool browserInPrivateMode = false, HybridStorageLinkedData? linkedDevice = null, string[]? credentialHints = null, byte[]? authenticatorId = null, byte[]? publicKeyCredentialCreationOptionsJson = null, WindowHandle windowHandle = default)
```

#### Parameters

`rpEntity` [RelyingPartyInformation](DSInternals.Win32.WebAuthn.RelyingPartyInformation.md)

Information about the relying party for which the credential is being created.

`userEntity` [UserInformation](DSInternals.Win32.WebAuthn.UserInformation.md)

Information about the user account the credential will be bound to.

`clientData` [CollectedClientData](DSInternals.Win32.WebAuthn.FIDO.CollectedClientData.md)

The client data that contains the challenge, type, origin, and related context to be signed by the authenticator.

`userVerificationRequirement` [UserVerificationRequirement](DSInternals.Win32.WebAuthn.UserVerificationRequirement.md)

Indicates whether user verification is required, preferred, or discouraged.

`authenticatorAttachment` [AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)

Constrains the type of authenticator that may be used (platform, cross-platform, or any).

`residentKey` [ResidentKeyRequirement](DSInternals.Win32.WebAuthn.ResidentKeyRequirement.md)

Indicates whether the credential should be created as a discoverable (resident) credential.

`pubKeyCredParams` [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)\[\]?

Ordered list of supported COSE algorithms for the new credential. Defaults to ES256 when null or empty.

`attestationConveyancePreference` [AttestationConveyancePreference](DSInternals.Win32.WebAuthn.AttestationConveyancePreference.md)

Specifies how the relying party wants attestation to be conveyed.

`timeoutMilliseconds` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Timeout, in milliseconds, that the client should wait for the authenticator to complete the operation.

`excludeCredentials` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)\>?

Credentials that the authenticator must not create a new credential for. Used to prevent duplicate registrations.

`enterpriseAttestation` [EnterpriseAttestationType](DSInternals.Win32.WebAuthn.EnterpriseAttestationType.md)

Indicates whether enterprise attestation is requested and at what level.

`extensions` [AuthenticationExtensionsClientAttestationInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAttestationInputs.md)?

Client extension inputs for the credential creation operation.

`browserInPrivateMode` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Indicates whether the request originates from a browser running in private/incognito mode.

`linkedDevice` [HybridStorageLinkedData](DSInternals.Win32.WebAuthn.Interop.HybridStorageLinkedData.md)?

Optional hybrid (cross-device) storage linked data for state-assisted transactions.

`credentialHints` [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]?

Optional ordered list of public key credential hints describing the modality the relying party prefers.

`authenticatorId` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

Optional identifier of a specific authenticator to target.

`publicKeyCredentialCreationOptionsJson` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

Optional UTF-8 encoded JSON representation of the original creation options, forwarded to the authenticator.

`windowHandle` [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

Handle to the window that will own the authenticator UI. When invalid, the foreground window is used.

#### Returns

 [AttestationPublicKeyCredential](DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential.md)

The attestation public key credential produced by the authenticator.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_AuthenticatorMakeCredentialAsync_DSInternals_Win32_WebAuthn_PublicKeyCredentialCreationOptions_System_String_DSInternals_Win32_WebAuthn_WindowHandle_System_Threading_CancellationToken_"></a> AuthenticatorMakeCredentialAsync\(PublicKeyCredentialCreationOptions, string?, WindowHandle, CancellationToken\)

Creates a new public key credential on the authenticator and returns the attestation that conveys its public key to the relying party.

```csharp
public Task<AttestationPublicKeyCredential> AuthenticatorMakeCredentialAsync(PublicKeyCredentialCreationOptions options, string? hostName = null, WindowHandle windowHandle = default, CancellationToken cancellationToken = default)
```

#### Parameters

`options` [PublicKeyCredentialCreationOptions](DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions.md)

The credential creation options that describe the relying party, the user, and the desired authenticator behavior.

`hostName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional host name used to derive the WebAuthn origin and to fill in a missing relying party identifier.
Useful for relying parties (such as Okta) that omit <code>rp.id</code> from server-issued creation options.
When <xref href="DSInternals.Win32.WebAuthn.RelyingPartyInformation.Id" data-throw-if-not-resolved="false"></xref> is null or empty, the host name is used as the relying party identifier sent to the authenticator.

`windowHandle` [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

Handle to the window that will own the authenticator UI. When invalid, the foreground window is used.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Token that, when canceled, signals the underlying WebAuthn operation to be canceled.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AttestationPublicKeyCredential](DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential.md)\>

A task that completes with the attestation public key credential produced by the authenticator.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_AuthenticatorMakeCredentialAsync_DSInternals_Win32_WebAuthn_RelyingPartyInformation_DSInternals_Win32_WebAuthn_UserInformation_System_Byte___DSInternals_Win32_WebAuthn_UserVerificationRequirement_DSInternals_Win32_WebAuthn_AuthenticatorAttachment_DSInternals_Win32_WebAuthn_ResidentKeyRequirement_DSInternals_Win32_WebAuthn_COSE_Algorithm___DSInternals_Win32_WebAuthn_AttestationConveyancePreference_System_UInt32_System_Collections_Generic_IReadOnlyList_DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor__DSInternals_Win32_WebAuthn_EnterpriseAttestationType_DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationInputs_System_Boolean_DSInternals_Win32_WebAuthn_Interop_HybridStorageLinkedData_System_String___System_Byte___System_Byte___System_String_DSInternals_Win32_WebAuthn_WindowHandle_System_Threading_CancellationToken_"></a> AuthenticatorMakeCredentialAsync\(RelyingPartyInformation, UserInformation, byte\[\], UserVerificationRequirement, AuthenticatorAttachment, ResidentKeyRequirement, Algorithm\[\]?, AttestationConveyancePreference, uint, IReadOnlyList<PublicKeyCredentialDescriptor\>?, EnterpriseAttestationType, AuthenticationExtensionsClientAttestationInputs?, bool, HybridStorageLinkedData?, string\[\]?, byte\[\]?, byte\[\]?, string?, WindowHandle, CancellationToken\)

Creates a new public key credential on the authenticator and returns the attestation that conveys its public key to the relying party.

```csharp
public Task<AttestationPublicKeyCredential> AuthenticatorMakeCredentialAsync(RelyingPartyInformation rpEntity, UserInformation userEntity, byte[] challenge, UserVerificationRequirement userVerificationRequirement, AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any, ResidentKeyRequirement residentKey = ResidentKeyRequirement.Preferred, Algorithm[]? pubKeyCredParams = null, AttestationConveyancePreference attestationConveyancePreference = AttestationConveyancePreference.Any, uint timeoutMilliseconds = 60000, IReadOnlyList<PublicKeyCredentialDescriptor>? excludeCredentials = null, EnterpriseAttestationType enterpriseAttestation = EnterpriseAttestationType.None, AuthenticationExtensionsClientAttestationInputs? extensions = null, bool browserInPrivateMode = false, HybridStorageLinkedData? linkedDevice = null, string[]? credentialHints = null, byte[]? authenticatorId = null, byte[]? publicKeyCredentialCreationOptionsJson = null, string? hostName = null, WindowHandle windowHandle = default, CancellationToken cancellationToken = default)
```

#### Parameters

`rpEntity` [RelyingPartyInformation](DSInternals.Win32.WebAuthn.RelyingPartyInformation.md)

Information about the relying party for which the credential is being created.

`userEntity` [UserInformation](DSInternals.Win32.WebAuthn.UserInformation.md)

Information about the user account the credential will be bound to.

`challenge` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

Cryptographic challenge produced by the relying party to be signed by the authenticator.

`userVerificationRequirement` [UserVerificationRequirement](DSInternals.Win32.WebAuthn.UserVerificationRequirement.md)

Indicates whether user verification is required, preferred, or discouraged.

`authenticatorAttachment` [AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)

Constrains the type of authenticator that may be used (platform, cross-platform, or any).

`residentKey` [ResidentKeyRequirement](DSInternals.Win32.WebAuthn.ResidentKeyRequirement.md)

Indicates whether the credential should be created as a discoverable (resident) credential.

`pubKeyCredParams` [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)\[\]?

Ordered list of supported COSE algorithms for the new credential. Defaults to ES256 when null or empty.

`attestationConveyancePreference` [AttestationConveyancePreference](DSInternals.Win32.WebAuthn.AttestationConveyancePreference.md)

Specifies how the relying party wants attestation to be conveyed.

`timeoutMilliseconds` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Timeout, in milliseconds, that the client should wait for the authenticator to complete the operation.

`excludeCredentials` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)\>?

Credentials that the authenticator must not create a new credential for. Used to prevent duplicate registrations.

`enterpriseAttestation` [EnterpriseAttestationType](DSInternals.Win32.WebAuthn.EnterpriseAttestationType.md)

Indicates whether enterprise attestation is requested and at what level.

`extensions` [AuthenticationExtensionsClientAttestationInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAttestationInputs.md)?

Client extension inputs for the credential creation operation.

`browserInPrivateMode` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Indicates whether the request originates from a browser running in private/incognito mode.

`linkedDevice` [HybridStorageLinkedData](DSInternals.Win32.WebAuthn.Interop.HybridStorageLinkedData.md)?

Optional hybrid (cross-device) storage linked data for state-assisted transactions.

`credentialHints` [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]?

Optional ordered list of public key credential hints describing the modality the relying party prefers.

`authenticatorId` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

Optional identifier of a specific authenticator to target.

`publicKeyCredentialCreationOptionsJson` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

Optional UTF-8 encoded JSON representation of the original creation options, forwarded to the authenticator.

`hostName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional host name used for client data construction; defaults to the relying party identifier.

`windowHandle` [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

Handle to the window that will own the authenticator UI. When invalid, the foreground window is used.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Token that, when canceled, signals the underlying WebAuthn operation to be canceled.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AttestationPublicKeyCredential](DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential.md)\>

A task that completes with the attestation public key credential produced by the authenticator.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_AuthenticatorMakeCredentialAsync_DSInternals_Win32_WebAuthn_RelyingPartyInformation_DSInternals_Win32_WebAuthn_UserInformation_DSInternals_Win32_WebAuthn_FIDO_CollectedClientData_DSInternals_Win32_WebAuthn_UserVerificationRequirement_DSInternals_Win32_WebAuthn_AuthenticatorAttachment_DSInternals_Win32_WebAuthn_ResidentKeyRequirement_DSInternals_Win32_WebAuthn_COSE_Algorithm___DSInternals_Win32_WebAuthn_AttestationConveyancePreference_System_UInt32_System_Collections_Generic_IReadOnlyList_DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor__DSInternals_Win32_WebAuthn_EnterpriseAttestationType_DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationInputs_System_Boolean_DSInternals_Win32_WebAuthn_Interop_HybridStorageLinkedData_System_String___System_Byte___System_Byte___DSInternals_Win32_WebAuthn_WindowHandle_System_Threading_CancellationToken_"></a> AuthenticatorMakeCredentialAsync\(RelyingPartyInformation, UserInformation, CollectedClientData, UserVerificationRequirement, AuthenticatorAttachment, ResidentKeyRequirement, Algorithm\[\]?, AttestationConveyancePreference, uint, IReadOnlyList<PublicKeyCredentialDescriptor\>?, EnterpriseAttestationType, AuthenticationExtensionsClientAttestationInputs?, bool, HybridStorageLinkedData?, string\[\]?, byte\[\]?, byte\[\]?, WindowHandle, CancellationToken\)

Creates a new public key credential on the authenticator and returns the attestation that conveys its public key to the relying party.

```csharp
public Task<AttestationPublicKeyCredential> AuthenticatorMakeCredentialAsync(RelyingPartyInformation rpEntity, UserInformation userEntity, CollectedClientData clientData, UserVerificationRequirement userVerificationRequirement, AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any, ResidentKeyRequirement residentKey = ResidentKeyRequirement.Preferred, Algorithm[]? pubKeyCredParams = null, AttestationConveyancePreference attestationConveyancePreference = AttestationConveyancePreference.Any, uint timeoutMilliseconds = 60000, IReadOnlyList<PublicKeyCredentialDescriptor>? excludeCredentials = null, EnterpriseAttestationType enterpriseAttestation = EnterpriseAttestationType.None, AuthenticationExtensionsClientAttestationInputs? extensions = null, bool browserInPrivateMode = false, HybridStorageLinkedData? linkedDevice = null, string[]? credentialHints = null, byte[]? authenticatorId = null, byte[]? publicKeyCredentialCreationOptionsJson = null, WindowHandle windowHandle = default, CancellationToken cancellationToken = default)
```

#### Parameters

`rpEntity` [RelyingPartyInformation](DSInternals.Win32.WebAuthn.RelyingPartyInformation.md)

Information about the relying party for which the credential is being created.

`userEntity` [UserInformation](DSInternals.Win32.WebAuthn.UserInformation.md)

Information about the user account the credential will be bound to.

`clientData` [CollectedClientData](DSInternals.Win32.WebAuthn.FIDO.CollectedClientData.md)

The client data that contains the challenge, type, origin, and related context to be signed by the authenticator.

`userVerificationRequirement` [UserVerificationRequirement](DSInternals.Win32.WebAuthn.UserVerificationRequirement.md)

Indicates whether user verification is required, preferred, or discouraged.

`authenticatorAttachment` [AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)

Constrains the type of authenticator that may be used (platform, cross-platform, or any).

`residentKey` [ResidentKeyRequirement](DSInternals.Win32.WebAuthn.ResidentKeyRequirement.md)

Indicates whether the credential should be created as a discoverable (resident) credential.

`pubKeyCredParams` [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)\[\]?

Ordered list of supported COSE algorithms for the new credential. Defaults to ES256 when null or empty.

`attestationConveyancePreference` [AttestationConveyancePreference](DSInternals.Win32.WebAuthn.AttestationConveyancePreference.md)

Specifies how the relying party wants attestation to be conveyed.

`timeoutMilliseconds` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

Timeout, in milliseconds, that the client should wait for the authenticator to complete the operation.

`excludeCredentials` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)\>?

Credentials that the authenticator must not create a new credential for. Used to prevent duplicate registrations.

`enterpriseAttestation` [EnterpriseAttestationType](DSInternals.Win32.WebAuthn.EnterpriseAttestationType.md)

Indicates whether enterprise attestation is requested and at what level.

`extensions` [AuthenticationExtensionsClientAttestationInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAttestationInputs.md)?

Client extension inputs for the credential creation operation.

`browserInPrivateMode` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Indicates whether the request originates from a browser running in private/incognito mode.

`linkedDevice` [HybridStorageLinkedData](DSInternals.Win32.WebAuthn.Interop.HybridStorageLinkedData.md)?

Optional hybrid (cross-device) storage linked data for state-assisted transactions.

`credentialHints` [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]?

Optional ordered list of public key credential hints describing the modality the relying party prefers.

`authenticatorId` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

Optional identifier of a specific authenticator to target.

`publicKeyCredentialCreationOptionsJson` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

Optional UTF-8 encoded JSON representation of the original creation options, forwarded to the authenticator.

`windowHandle` [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

Handle to the window that will own the authenticator UI. When invalid, the foreground window is used.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Token that, when canceled, signals the underlying WebAuthn operation to be canceled.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AttestationPublicKeyCredential](DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential.md)\>

A task that completes with the attestation public key credential produced by the authenticator.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_CancelCurrentOperation"></a> CancelCurrentOperation\(\)

Cancels the WebAuthn operation currently in progress.

```csharp
public void CancelCurrentOperation()
```

#### Remarks

When this operation is invoked by the client in an authenticator session,
it has the effect of terminating any AuthenticatorMakeCredential or AuthenticatorGetAssertion operation
currently in progress in that authenticator session.
The authenticator stops prompting for, or accepting, any user input related to authorizing the canceled operation. The client ignores any further responses from the authenticator for the canceled operation.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_DeletePlatformCredential_System_Byte___"></a> DeletePlatformCredential\(byte\[\]\)

Removes a public key credential stored on the platform authenticator.

```csharp
public static void DeletePlatformCredential(byte[] credentialId)
```

#### Parameters

`credentialId` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

The ID of the credential to be removed.

#### Exceptions

 [NotSupportedException](https://learn.microsoft.com/dotnet/api/system.notsupportedexception)

Thrown when the running OS does not support platform credential management (added in API V4).

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">credentialId</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_GetAuthenticatorList"></a> GetAuthenticatorList\(\)

Gets the list of available authenticators.

```csharp
public static IList<AuthenticatorDetails>? GetAuthenticatorList()
```

#### Returns

 [IList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist\-1)<[AuthenticatorDetails](DSInternals.Win32.WebAuthn.AuthenticatorDetails.md)\>?

The list of authenticators currently visible to the platform, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when none are present.

#### Exceptions

 [NotSupportedException](https://learn.microsoft.com/dotnet/api/system.notsupportedexception)

Thrown when the running OS does not support the authenticator list API (added in API V9).

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_GetPlatformCredentialList_System_String_System_Boolean_"></a> GetPlatformCredentialList\(string?, bool\)

Gets the list of stored credentials.

```csharp
public static IList<CredentialDetails>? GetPlatformCredentialList(string? rpId = null, bool browserInPrivateMode = false)
```

#### Parameters

`rpId` [string](https://learn.microsoft.com/dotnet/api/system.string)?

Optional Id of the relying party that is making the request.

`browserInPrivateMode` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Indicates whether the browser is in private mode.

#### Returns

 [IList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist\-1)<[CredentialDetails](DSInternals.Win32.WebAuthn.CredentialDetails.md)\>?

The list of platform credentials matching the filter, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when none are present.

#### Exceptions

 [NotSupportedException](https://learn.microsoft.com/dotnet/api/system.notsupportedexception)

Thrown when the running OS does not support platform credential management (added in API V4).

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_GetPluginAuthenticators"></a> GetPluginAuthenticators\(\)

Gets the list of registered authenticator plugins from the Windows registry.

```csharp
public static IList<AuthenticatorPluginInformation>? GetPluginAuthenticators()
```

#### Returns

 [IList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist\-1)<[AuthenticatorPluginInformation](DSInternals.Win32.WebAuthn.AuthenticatorPluginInformation.md)\>?

A list of authenticator plugin information, or null if no plugins are registered.

#### Remarks

Authenticator plugins (e.g., 1Password, Bitwarden) are registered under
HKLM\SOFTWARE\Microsoft\FIDO\{UserSID}\Plugins\{PluginGuid}.

