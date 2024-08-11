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

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_AuthenticatorGetAssertion_System_String_System_Byte___DSInternals_Win32_WebAuthn_UserVerificationRequirement_DSInternals_Win32_WebAuthn_AuthenticatorAttachment_System_Int32_System_Collections_Generic_IReadOnlyList_DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor__DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientInputs_DSInternals_Win32_WebAuthn_CredentialLargeBlobOperation_System_Byte___System_Boolean_DSInternals_Win32_WebAuthn_Interop_HybridStorageLinkedData_DSInternals_Win32_WebAuthn_WindowHandle_"></a> AuthenticatorGetAssertion\(string, byte\[\], UserVerificationRequirement, AuthenticatorAttachment, int, IReadOnlyList<PublicKeyCredentialDescriptor\>, AuthenticationExtensionsClientInputs, CredentialLargeBlobOperation, byte\[\], bool, HybridStorageLinkedData, WindowHandle\)

Produces an assertion signature representing an assertion by the authenticator that the user has consented to a specific transaction, such as logging in or completing a purchase.

```csharp
public AuthenticatorAssertionResponse AuthenticatorGetAssertion(string rpId, byte[] challenge, UserVerificationRequirement userVerificationRequirement, AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any, int timeoutMilliseconds = 60000, IReadOnlyList<PublicKeyCredentialDescriptor> allowCredentials = null, AuthenticationExtensionsClientInputs extensions = null, CredentialLargeBlobOperation largeBlobOperation = CredentialLargeBlobOperation.None, byte[] largeBlob = null, bool browserInPrivateMode = false, HybridStorageLinkedData linkedDevice = null, WindowHandle windowHandle = default)
```

#### Parameters

`rpId` [string](https://learn.microsoft.com/dotnet/api/system.string)

`challenge` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

`userVerificationRequirement` [UserVerificationRequirement](DSInternals.Win32.WebAuthn.UserVerificationRequirement.md)

`authenticatorAttachment` [AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)

`timeoutMilliseconds` [int](https://learn.microsoft.com/dotnet/api/system.int32)

`allowCredentials` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)\>

`extensions` [AuthenticationExtensionsClientInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientInputs.md)

`largeBlobOperation` [CredentialLargeBlobOperation](DSInternals.Win32.WebAuthn.CredentialLargeBlobOperation.md)

`largeBlob` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

`browserInPrivateMode` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

`linkedDevice` [HybridStorageLinkedData](DSInternals.Win32.WebAuthn.Interop.HybridStorageLinkedData.md)

`windowHandle` [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

#### Returns

 [AuthenticatorAssertionResponse](DSInternals.Win32.WebAuthn.AuthenticatorAssertionResponse.md)

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_AuthenticatorGetAssertion_System_String_DSInternals_Win32_WebAuthn_FIDO_CollectedClientData_DSInternals_Win32_WebAuthn_UserVerificationRequirement_DSInternals_Win32_WebAuthn_AuthenticatorAttachment_System_Int32_System_Collections_Generic_IReadOnlyList_DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor__DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientInputs_DSInternals_Win32_WebAuthn_CredentialLargeBlobOperation_System_Byte___System_Boolean_DSInternals_Win32_WebAuthn_Interop_HybridStorageLinkedData_DSInternals_Win32_WebAuthn_WindowHandle_"></a> AuthenticatorGetAssertion\(string, CollectedClientData, UserVerificationRequirement, AuthenticatorAttachment, int, IReadOnlyList<PublicKeyCredentialDescriptor\>, AuthenticationExtensionsClientInputs, CredentialLargeBlobOperation, byte\[\], bool, HybridStorageLinkedData, WindowHandle\)

Produces an assertion signature representing an assertion by the authenticator that the user has consented to a specific transaction, such as logging in or completing a purchase.

```csharp
public AuthenticatorAssertionResponse AuthenticatorGetAssertion(string rpId, CollectedClientData clientData, UserVerificationRequirement userVerificationRequirement, AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any, int timeoutMilliseconds = 60000, IReadOnlyList<PublicKeyCredentialDescriptor> allowCredentials = null, AuthenticationExtensionsClientInputs extensions = null, CredentialLargeBlobOperation largeBlobOperation = CredentialLargeBlobOperation.None, byte[] largeBlob = null, bool browserInPrivateMode = false, HybridStorageLinkedData linkedDevice = null, WindowHandle windowHandle = default)
```

#### Parameters

`rpId` [string](https://learn.microsoft.com/dotnet/api/system.string)

`clientData` [CollectedClientData](DSInternals.Win32.WebAuthn.FIDO.CollectedClientData.md)

`userVerificationRequirement` [UserVerificationRequirement](DSInternals.Win32.WebAuthn.UserVerificationRequirement.md)

`authenticatorAttachment` [AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)

`timeoutMilliseconds` [int](https://learn.microsoft.com/dotnet/api/system.int32)

`allowCredentials` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)\>

`extensions` [AuthenticationExtensionsClientInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientInputs.md)

`largeBlobOperation` [CredentialLargeBlobOperation](DSInternals.Win32.WebAuthn.CredentialLargeBlobOperation.md)

`largeBlob` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

`browserInPrivateMode` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

`linkedDevice` [HybridStorageLinkedData](DSInternals.Win32.WebAuthn.Interop.HybridStorageLinkedData.md)

`windowHandle` [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

#### Returns

 [AuthenticatorAssertionResponse](DSInternals.Win32.WebAuthn.AuthenticatorAssertionResponse.md)

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_AuthenticatorGetAssertionAsync_System_String_System_Byte___DSInternals_Win32_WebAuthn_UserVerificationRequirement_DSInternals_Win32_WebAuthn_AuthenticatorAttachment_System_Int32_System_Collections_Generic_IReadOnlyList_DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor__DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientInputs_DSInternals_Win32_WebAuthn_CredentialLargeBlobOperation_System_Byte___System_Boolean_DSInternals_Win32_WebAuthn_Interop_HybridStorageLinkedData_DSInternals_Win32_WebAuthn_WindowHandle_System_Threading_CancellationToken_"></a> AuthenticatorGetAssertionAsync\(string, byte\[\], UserVerificationRequirement, AuthenticatorAttachment, int, IReadOnlyList<PublicKeyCredentialDescriptor\>, AuthenticationExtensionsClientInputs, CredentialLargeBlobOperation, byte\[\], bool, HybridStorageLinkedData, WindowHandle, CancellationToken\)

Produces an assertion signature representing an assertion by the authenticator that the user has consented to a specific transaction, such as logging in or completing a purchase.

```csharp
public Task<AuthenticatorAssertionResponse> AuthenticatorGetAssertionAsync(string rpId, byte[] challenge, UserVerificationRequirement userVerificationRequirement, AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any, int timeoutMilliseconds = 60000, IReadOnlyList<PublicKeyCredentialDescriptor> allowCredentials = null, AuthenticationExtensionsClientInputs extensions = null, CredentialLargeBlobOperation largeBlobOperation = CredentialLargeBlobOperation.None, byte[] largeBlob = null, bool browserInPrivateMode = false, HybridStorageLinkedData linkedDevice = null, WindowHandle windowHandle = default, CancellationToken cancellationToken = default)
```

#### Parameters

`rpId` [string](https://learn.microsoft.com/dotnet/api/system.string)

`challenge` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

`userVerificationRequirement` [UserVerificationRequirement](DSInternals.Win32.WebAuthn.UserVerificationRequirement.md)

`authenticatorAttachment` [AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)

`timeoutMilliseconds` [int](https://learn.microsoft.com/dotnet/api/system.int32)

`allowCredentials` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)\>

`extensions` [AuthenticationExtensionsClientInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientInputs.md)

`largeBlobOperation` [CredentialLargeBlobOperation](DSInternals.Win32.WebAuthn.CredentialLargeBlobOperation.md)

`largeBlob` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

`browserInPrivateMode` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

`linkedDevice` [HybridStorageLinkedData](DSInternals.Win32.WebAuthn.Interop.HybridStorageLinkedData.md)

`windowHandle` [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AuthenticatorAssertionResponse](DSInternals.Win32.WebAuthn.AuthenticatorAssertionResponse.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_AuthenticatorGetAssertionAsync_System_String_DSInternals_Win32_WebAuthn_FIDO_CollectedClientData_DSInternals_Win32_WebAuthn_UserVerificationRequirement_DSInternals_Win32_WebAuthn_AuthenticatorAttachment_System_Int32_System_Collections_Generic_IReadOnlyList_DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor__DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientInputs_DSInternals_Win32_WebAuthn_CredentialLargeBlobOperation_System_Byte___System_Boolean_DSInternals_Win32_WebAuthn_Interop_HybridStorageLinkedData_DSInternals_Win32_WebAuthn_WindowHandle_System_Threading_CancellationToken_"></a> AuthenticatorGetAssertionAsync\(string, CollectedClientData, UserVerificationRequirement, AuthenticatorAttachment, int, IReadOnlyList<PublicKeyCredentialDescriptor\>, AuthenticationExtensionsClientInputs, CredentialLargeBlobOperation, byte\[\], bool, HybridStorageLinkedData, WindowHandle, CancellationToken\)

Produces an assertion signature representing an assertion by the authenticator that the user has consented to a specific transaction, such as logging in or completing a purchase.

```csharp
public Task<AuthenticatorAssertionResponse> AuthenticatorGetAssertionAsync(string rpId, CollectedClientData clientData, UserVerificationRequirement userVerificationRequirement, AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any, int timeoutMilliseconds = 60000, IReadOnlyList<PublicKeyCredentialDescriptor> allowCredentials = null, AuthenticationExtensionsClientInputs extenstions = null, CredentialLargeBlobOperation largeBlobOperation = CredentialLargeBlobOperation.None, byte[] largeBlob = null, bool browserInPrivateMode = false, HybridStorageLinkedData linkedDevice = null, WindowHandle windowHandle = default, CancellationToken cancellationToken = default)
```

#### Parameters

`rpId` [string](https://learn.microsoft.com/dotnet/api/system.string)

`clientData` [CollectedClientData](DSInternals.Win32.WebAuthn.FIDO.CollectedClientData.md)

`userVerificationRequirement` [UserVerificationRequirement](DSInternals.Win32.WebAuthn.UserVerificationRequirement.md)

`authenticatorAttachment` [AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)

`timeoutMilliseconds` [int](https://learn.microsoft.com/dotnet/api/system.int32)

`allowCredentials` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)\>

`extenstions` [AuthenticationExtensionsClientInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientInputs.md)

`largeBlobOperation` [CredentialLargeBlobOperation](DSInternals.Win32.WebAuthn.CredentialLargeBlobOperation.md)

`largeBlob` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

`browserInPrivateMode` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

`linkedDevice` [HybridStorageLinkedData](DSInternals.Win32.WebAuthn.Interop.HybridStorageLinkedData.md)

`windowHandle` [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[AuthenticatorAssertionResponse](DSInternals.Win32.WebAuthn.AuthenticatorAssertionResponse.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_AuthenticatorMakeCredential_DSInternals_Win32_WebAuthn_PublicKeyCredentialCreationOptions_"></a> AuthenticatorMakeCredential\(PublicKeyCredentialCreationOptions\)

Creates a public key credential source bound to a managing authenticator and returns the credential public key
associated with its credential private key.

```csharp
public PublicKeyCredential AuthenticatorMakeCredential(PublicKeyCredentialCreationOptions options)
```

#### Parameters

`options` [PublicKeyCredentialCreationOptions](DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions.md)

#### Returns

 [PublicKeyCredential](DSInternals.Win32.WebAuthn.PublicKeyCredential.md)

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_AuthenticatorMakeCredential_DSInternals_Win32_WebAuthn_RelyingPartyInformation_DSInternals_Win32_WebAuthn_UserInformation_System_Byte___DSInternals_Win32_WebAuthn_UserVerificationRequirement_DSInternals_Win32_WebAuthn_AuthenticatorAttachment_System_Boolean_DSInternals_Win32_WebAuthn_COSE_Algorithm___DSInternals_Win32_WebAuthn_AttestationConveyancePreference_System_Int32_System_Collections_Generic_IReadOnlyList_DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor__DSInternals_Win32_WebAuthn_EnterpriseAttestationType_DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientInputs_DSInternals_Win32_WebAuthn_LargeBlobSupport_System_Boolean_System_Boolean_System_Boolean_DSInternals_Win32_WebAuthn_Interop_HybridStorageLinkedData_DSInternals_Win32_WebAuthn_WindowHandle_"></a> AuthenticatorMakeCredential\(RelyingPartyInformation, UserInformation, byte\[\], UserVerificationRequirement, AuthenticatorAttachment, bool, Algorithm\[\], AttestationConveyancePreference, int, IReadOnlyList<PublicKeyCredentialDescriptor\>, EnterpriseAttestationType, AuthenticationExtensionsClientInputs, LargeBlobSupport, bool, bool, bool, HybridStorageLinkedData, WindowHandle\)

Creates a public key credential source bound to a managing authenticator and returns the credential public key
associated with its credential private key.

```csharp
public PublicKeyCredential AuthenticatorMakeCredential(RelyingPartyInformation rpEntity, UserInformation userEntity, byte[] challenge, UserVerificationRequirement userVerificationRequirement, AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any, bool requireResidentKey = false, Algorithm[] pubKeyCredParams = null, AttestationConveyancePreference attestationConveyancePreference = AttestationConveyancePreference.Any, int timeoutMilliseconds = 60000, IReadOnlyList<PublicKeyCredentialDescriptor> excludeCredentials = null, EnterpriseAttestationType enterpriseAttestation = EnterpriseAttestationType.None, AuthenticationExtensionsClientInputs extensions = null, LargeBlobSupport largeBlobSupport = LargeBlobSupport.None, bool preferResidentKey = false, bool browserInPrivateMode = false, bool enablePseudoRandomFunction = false, HybridStorageLinkedData linkedDevice = null, WindowHandle windowHandle = default)
```

#### Parameters

`rpEntity` [RelyingPartyInformation](DSInternals.Win32.WebAuthn.RelyingPartyInformation.md)

`userEntity` [UserInformation](DSInternals.Win32.WebAuthn.UserInformation.md)

`challenge` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

`userVerificationRequirement` [UserVerificationRequirement](DSInternals.Win32.WebAuthn.UserVerificationRequirement.md)

`authenticatorAttachment` [AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)

`requireResidentKey` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

`pubKeyCredParams` [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)\[\]

`attestationConveyancePreference` [AttestationConveyancePreference](DSInternals.Win32.WebAuthn.AttestationConveyancePreference.md)

`timeoutMilliseconds` [int](https://learn.microsoft.com/dotnet/api/system.int32)

`excludeCredentials` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)\>

`enterpriseAttestation` [EnterpriseAttestationType](DSInternals.Win32.WebAuthn.EnterpriseAttestationType.md)

`extensions` [AuthenticationExtensionsClientInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientInputs.md)

`largeBlobSupport` [LargeBlobSupport](DSInternals.Win32.WebAuthn.LargeBlobSupport.md)

`preferResidentKey` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

`browserInPrivateMode` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

`enablePseudoRandomFunction` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

`linkedDevice` [HybridStorageLinkedData](DSInternals.Win32.WebAuthn.Interop.HybridStorageLinkedData.md)

`windowHandle` [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

#### Returns

 [PublicKeyCredential](DSInternals.Win32.WebAuthn.PublicKeyCredential.md)

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_AuthenticatorMakeCredential_DSInternals_Win32_WebAuthn_RelyingPartyInformation_DSInternals_Win32_WebAuthn_UserInformation_DSInternals_Win32_WebAuthn_FIDO_CollectedClientData_DSInternals_Win32_WebAuthn_UserVerificationRequirement_DSInternals_Win32_WebAuthn_AuthenticatorAttachment_System_Boolean_DSInternals_Win32_WebAuthn_COSE_Algorithm___DSInternals_Win32_WebAuthn_AttestationConveyancePreference_System_Int32_System_Collections_Generic_IReadOnlyList_DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor__DSInternals_Win32_WebAuthn_EnterpriseAttestationType_DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientInputs_DSInternals_Win32_WebAuthn_LargeBlobSupport_System_Boolean_System_Boolean_System_Boolean_DSInternals_Win32_WebAuthn_Interop_HybridStorageLinkedData_DSInternals_Win32_WebAuthn_WindowHandle_"></a> AuthenticatorMakeCredential\(RelyingPartyInformation, UserInformation, CollectedClientData, UserVerificationRequirement, AuthenticatorAttachment, bool, Algorithm\[\], AttestationConveyancePreference, int, IReadOnlyList<PublicKeyCredentialDescriptor\>, EnterpriseAttestationType, AuthenticationExtensionsClientInputs, LargeBlobSupport, bool, bool, bool, HybridStorageLinkedData, WindowHandle\)

Creates a public key credential source bound to a managing authenticator and returns the credential public key
associated with its credential private key.

```csharp
public PublicKeyCredential AuthenticatorMakeCredential(RelyingPartyInformation rpEntity, UserInformation userEntity, CollectedClientData clientData, UserVerificationRequirement userVerificationRequirement, AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any, bool requireResidentKey = false, Algorithm[] pubKeyCredParams = null, AttestationConveyancePreference attestationConveyancePreference = AttestationConveyancePreference.Any, int timeoutMilliseconds = 60000, IReadOnlyList<PublicKeyCredentialDescriptor> excludeCredentials = null, EnterpriseAttestationType enterpriseAttestation = EnterpriseAttestationType.None, AuthenticationExtensionsClientInputs extensions = null, LargeBlobSupport largeBlobSupport = LargeBlobSupport.None, bool preferResidentKey = false, bool browserInPrivateMode = false, bool enablePseudoRandomFunction = false, HybridStorageLinkedData linkedDevice = null, WindowHandle windowHandle = default)
```

#### Parameters

`rpEntity` [RelyingPartyInformation](DSInternals.Win32.WebAuthn.RelyingPartyInformation.md)

`userEntity` [UserInformation](DSInternals.Win32.WebAuthn.UserInformation.md)

`clientData` [CollectedClientData](DSInternals.Win32.WebAuthn.FIDO.CollectedClientData.md)

`userVerificationRequirement` [UserVerificationRequirement](DSInternals.Win32.WebAuthn.UserVerificationRequirement.md)

`authenticatorAttachment` [AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)

`requireResidentKey` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

`pubKeyCredParams` [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)\[\]

`attestationConveyancePreference` [AttestationConveyancePreference](DSInternals.Win32.WebAuthn.AttestationConveyancePreference.md)

`timeoutMilliseconds` [int](https://learn.microsoft.com/dotnet/api/system.int32)

`excludeCredentials` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)\>

`enterpriseAttestation` [EnterpriseAttestationType](DSInternals.Win32.WebAuthn.EnterpriseAttestationType.md)

`extensions` [AuthenticationExtensionsClientInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientInputs.md)

`largeBlobSupport` [LargeBlobSupport](DSInternals.Win32.WebAuthn.LargeBlobSupport.md)

`preferResidentKey` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

`browserInPrivateMode` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

`enablePseudoRandomFunction` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

`linkedDevice` [HybridStorageLinkedData](DSInternals.Win32.WebAuthn.Interop.HybridStorageLinkedData.md)

`windowHandle` [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

#### Returns

 [PublicKeyCredential](DSInternals.Win32.WebAuthn.PublicKeyCredential.md)

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_AuthenticatorMakeCredentialAsync_DSInternals_Win32_WebAuthn_RelyingPartyInformation_DSInternals_Win32_WebAuthn_UserInformation_System_Byte___DSInternals_Win32_WebAuthn_UserVerificationRequirement_DSInternals_Win32_WebAuthn_AuthenticatorAttachment_System_Boolean_DSInternals_Win32_WebAuthn_COSE_Algorithm___DSInternals_Win32_WebAuthn_AttestationConveyancePreference_System_Int32_DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientInputs_System_Collections_Generic_IReadOnlyList_DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor__DSInternals_Win32_WebAuthn_EnterpriseAttestationType_DSInternals_Win32_WebAuthn_LargeBlobSupport_System_Boolean_System_Boolean_System_Boolean_DSInternals_Win32_WebAuthn_Interop_HybridStorageLinkedData_DSInternals_Win32_WebAuthn_WindowHandle_System_Threading_CancellationToken_"></a> AuthenticatorMakeCredentialAsync\(RelyingPartyInformation, UserInformation, byte\[\], UserVerificationRequirement, AuthenticatorAttachment, bool, Algorithm\[\], AttestationConveyancePreference, int, AuthenticationExtensionsClientInputs, IReadOnlyList<PublicKeyCredentialDescriptor\>, EnterpriseAttestationType, LargeBlobSupport, bool, bool, bool, HybridStorageLinkedData, WindowHandle, CancellationToken\)

Creates a public key credential source bound to a managing authenticator and returns the credential public key
associated with its credential private key.

```csharp
public Task<PublicKeyCredential> AuthenticatorMakeCredentialAsync(RelyingPartyInformation rpEntity, UserInformation userEntity, byte[] challenge, UserVerificationRequirement userVerificationRequirement, AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any, bool requireResidentKey = false, Algorithm[] pubKeyCredParams = null, AttestationConveyancePreference attestationConveyancePreference = AttestationConveyancePreference.Any, int timeoutMilliseconds = 60000, AuthenticationExtensionsClientInputs extensions = null, IReadOnlyList<PublicKeyCredentialDescriptor> excludeCredentials = null, EnterpriseAttestationType enterpriseAttestation = EnterpriseAttestationType.None, LargeBlobSupport largeBlobSupport = LargeBlobSupport.None, bool preferResidentKey = false, bool browserInPrivateMode = false, bool enablePseudoRandomFunction = false, HybridStorageLinkedData linkedDevice = null, WindowHandle windowHandle = default, CancellationToken cancellationToken = default)
```

#### Parameters

`rpEntity` [RelyingPartyInformation](DSInternals.Win32.WebAuthn.RelyingPartyInformation.md)

`userEntity` [UserInformation](DSInternals.Win32.WebAuthn.UserInformation.md)

`challenge` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

`userVerificationRequirement` [UserVerificationRequirement](DSInternals.Win32.WebAuthn.UserVerificationRequirement.md)

`authenticatorAttachment` [AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)

`requireResidentKey` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

`pubKeyCredParams` [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)\[\]

`attestationConveyancePreference` [AttestationConveyancePreference](DSInternals.Win32.WebAuthn.AttestationConveyancePreference.md)

`timeoutMilliseconds` [int](https://learn.microsoft.com/dotnet/api/system.int32)

`extensions` [AuthenticationExtensionsClientInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientInputs.md)

`excludeCredentials` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)\>

`enterpriseAttestation` [EnterpriseAttestationType](DSInternals.Win32.WebAuthn.EnterpriseAttestationType.md)

`largeBlobSupport` [LargeBlobSupport](DSInternals.Win32.WebAuthn.LargeBlobSupport.md)

`preferResidentKey` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

`browserInPrivateMode` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

`enablePseudoRandomFunction` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

`linkedDevice` [HybridStorageLinkedData](DSInternals.Win32.WebAuthn.Interop.HybridStorageLinkedData.md)

`windowHandle` [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PublicKeyCredential](DSInternals.Win32.WebAuthn.PublicKeyCredential.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_AuthenticatorMakeCredentialAsync_DSInternals_Win32_WebAuthn_RelyingPartyInformation_DSInternals_Win32_WebAuthn_UserInformation_DSInternals_Win32_WebAuthn_FIDO_CollectedClientData_DSInternals_Win32_WebAuthn_UserVerificationRequirement_DSInternals_Win32_WebAuthn_AuthenticatorAttachment_System_Boolean_DSInternals_Win32_WebAuthn_COSE_Algorithm___DSInternals_Win32_WebAuthn_AttestationConveyancePreference_System_Int32_System_Collections_Generic_IReadOnlyList_DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor__DSInternals_Win32_WebAuthn_EnterpriseAttestationType_DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientInputs_DSInternals_Win32_WebAuthn_LargeBlobSupport_System_Boolean_System_Boolean_System_Boolean_DSInternals_Win32_WebAuthn_Interop_HybridStorageLinkedData_DSInternals_Win32_WebAuthn_WindowHandle_System_Threading_CancellationToken_"></a> AuthenticatorMakeCredentialAsync\(RelyingPartyInformation, UserInformation, CollectedClientData, UserVerificationRequirement, AuthenticatorAttachment, bool, Algorithm\[\], AttestationConveyancePreference, int, IReadOnlyList<PublicKeyCredentialDescriptor\>, EnterpriseAttestationType, AuthenticationExtensionsClientInputs, LargeBlobSupport, bool, bool, bool, HybridStorageLinkedData, WindowHandle, CancellationToken\)

Creates a public key credential source bound to a managing authenticator and returns the credential public key
associated with its credential private key.

```csharp
public Task<PublicKeyCredential> AuthenticatorMakeCredentialAsync(RelyingPartyInformation rpEntity, UserInformation userEntity, CollectedClientData clientData, UserVerificationRequirement userVerificationRequirement, AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any, bool requireResidentKey = false, Algorithm[] pubKeyCredParams = null, AttestationConveyancePreference attestationConveyancePreference = AttestationConveyancePreference.Any, int timeoutMilliseconds = 60000, IReadOnlyList<PublicKeyCredentialDescriptor> excludeCredentials = null, EnterpriseAttestationType enterpriseAttestation = EnterpriseAttestationType.None, AuthenticationExtensionsClientInputs extensions = null, LargeBlobSupport largeBlobSupport = LargeBlobSupport.None, bool preferResidentKey = false, bool browserInPrivateMode = false, bool enablePseudoRandomFunction = false, HybridStorageLinkedData linkedDevice = null, WindowHandle windowHandle = default, CancellationToken cancellationToken = default)
```

#### Parameters

`rpEntity` [RelyingPartyInformation](DSInternals.Win32.WebAuthn.RelyingPartyInformation.md)

`userEntity` [UserInformation](DSInternals.Win32.WebAuthn.UserInformation.md)

`clientData` [CollectedClientData](DSInternals.Win32.WebAuthn.FIDO.CollectedClientData.md)

`userVerificationRequirement` [UserVerificationRequirement](DSInternals.Win32.WebAuthn.UserVerificationRequirement.md)

`authenticatorAttachment` [AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)

`requireResidentKey` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

`pubKeyCredParams` [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)\[\]

`attestationConveyancePreference` [AttestationConveyancePreference](DSInternals.Win32.WebAuthn.AttestationConveyancePreference.md)

`timeoutMilliseconds` [int](https://learn.microsoft.com/dotnet/api/system.int32)

`excludeCredentials` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)\>

`enterpriseAttestation` [EnterpriseAttestationType](DSInternals.Win32.WebAuthn.EnterpriseAttestationType.md)

`extensions` [AuthenticationExtensionsClientInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientInputs.md)

`largeBlobSupport` [LargeBlobSupport](DSInternals.Win32.WebAuthn.LargeBlobSupport.md)

`preferResidentKey` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

`browserInPrivateMode` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

`enablePseudoRandomFunction` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

`linkedDevice` [HybridStorageLinkedData](DSInternals.Win32.WebAuthn.Interop.HybridStorageLinkedData.md)

`windowHandle` [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PublicKeyCredential](DSInternals.Win32.WebAuthn.PublicKeyCredential.md)\>

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

Removes a Public Key Credential Source stored on a Virtual Authenticator.

```csharp
public static void DeletePlatformCredential(byte[] credentialId)
```

#### Parameters

`credentialId` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

The ID of the credential to be removed.

#### Exceptions

 [NotSupportedException](https://learn.microsoft.com/dotnet/api/system.notsupportedexception)

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

### <a id="DSInternals_Win32_WebAuthn_WebAuthnApi_GetPlatformCredentialList_System_String_System_Boolean_"></a> GetPlatformCredentialList\(string, bool\)

Gets the list of stored credentials.

```csharp
public static IList<CredentialDetails> GetPlatformCredentialList(string rpId = null, bool browserInPrivateMode = false)
```

#### Parameters

`rpId` [string](https://learn.microsoft.com/dotnet/api/system.string)

Optional Id of the relying party that is making the request.

`browserInPrivateMode` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Indicates whether the browser is in private mode.

#### Returns

 [IList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist\-1)<[CredentialDetails](DSInternals.Win32.WebAuthn.CredentialDetails.md)\>

#### Exceptions

 [NotSupportedException](https://learn.microsoft.com/dotnet/api/system.notsupportedexception)

