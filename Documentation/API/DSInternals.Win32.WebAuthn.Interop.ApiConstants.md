# <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants"></a> Class ApiConstants

Namespace: [DSInternals.Win32.WebAuthn.Interop](DSInternals.Win32.WebAuthn.Interop.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Contains WebAuthn API constants.

```csharp
public static class ApiConstants
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ApiConstants](DSInternals.Win32.WebAuthn.Interop.ApiConstants.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Fields

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_AttestationTypeNone"></a> AttestationTypeNone

```csharp
public const string AttestationTypeNone = "none"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_ATTESTATION_TYPE_NONE.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_AttestationTypePacked"></a> AttestationTypePacked

```csharp
public const string AttestationTypePacked = "packed"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_ATTESTATION_TYPE_PACKED.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_AttestationTypeTpm"></a> AttestationTypeTpm

```csharp
public const string AttestationTypeTpm = "tpm"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_ATTESTATION_TYPE_TPM.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_AttestationTypeU2F"></a> AttestationTypeU2F

```csharp
public const string AttestationTypeU2F = "fido-u2f"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_ATTESTATION_TYPE_U2F.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_AttestationVersionTpm20"></a> AttestationVersionTpm20

```csharp
public const string AttestationVersionTpm20 = "2.0"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_ATTESTATION_VER_TPM_2_0.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_AuthenticatorAttachmentCrossPlatform"></a> AuthenticatorAttachmentCrossPlatform

The authenticator attachment value for cross-platform (roaming) authenticators.

```csharp
public const string AuthenticatorAttachmentCrossPlatform = "cross-platform"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_AuthenticatorAttachmentPlatform"></a> AuthenticatorAttachmentPlatform

The authenticator attachment value for platform authenticators.

```csharp
public const string AuthenticatorAttachmentPlatform = "platform"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_BitwardenAaGuid"></a> BitwardenAaGuid

The AAGUID of the Bitwarden authenticator.

```csharp
public static readonly Guid BitwardenAaGuid
```

#### Field Value

 [Guid](https://learn.microsoft.com/dotnet/api/system.guid)

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_ClientDataCredentialCreate"></a> ClientDataCredentialCreate

WebAuthn operation type used when creating new FIDO2 credentials.

```csharp
public const string ClientDataCredentialCreate = "webauthn.create"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_ClientDataCredentialCreateU2F"></a> ClientDataCredentialCreateU2F

WebAuthn operation type used when creating new FIDO U2F credentials.

```csharp
public const string ClientDataCredentialCreateU2F = "navigator.id.finishEnrollment"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_ClientDataCredentialGet"></a> ClientDataCredentialGet

The client data type for WebAuthn assertion (authentication) operations.

```csharp
public const string ClientDataCredentialGet = "webauthn.get"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_ClientDataCredentialGetU2F"></a> ClientDataCredentialGetU2F

WebAuthn operation type used for authentication with FIDO U2F credentials.

```csharp
public const string ClientDataCredentialGetU2F = "navigator.id.getAssertion"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_CredentialHintClientDevice"></a> CredentialHintClientDevice

The credential hint value for client devices (platform authenticators).

```csharp
public const string CredentialHintClientDevice = "client-device"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_CREDENTIAL_HINT_CLIENT_DEVICE.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_CredentialHintHybrid"></a> CredentialHintHybrid

The credential hint value for hybrid (QR code) authenticators.

```csharp
public const string CredentialHintHybrid = "hybrid"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_CREDENTIAL_HINT_HYBRID.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_CredentialHintSecurityKey"></a> CredentialHintSecurityKey

The credential hint value for security keys (roaming authenticators).

```csharp
public const string CredentialHintSecurityKey = "security-key"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_CREDENTIAL_HINT_SECURITY_KEY.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_CtapOneHmacSecretLength"></a> CtapOneHmacSecretLength

```csharp
public const uint CtapOneHmacSecretLength = 32
```

#### Field Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

#### Remarks

Corresponds to WEBAUTHN_CTAP_ONE_HMAC_SECRET_LENGTH.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_CtapTransportBle"></a> CtapTransportBle

```csharp
public const string CtapTransportBle = "ble"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_CTAP_TRANSPORT_BLE_STRING.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_CtapTransportHybrid"></a> CtapTransportHybrid

```csharp
public const string CtapTransportHybrid = "hybrid"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_CTAP_TRANSPORT_HYBRID_STRING.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_CtapTransportInternal"></a> CtapTransportInternal

The transport value indicating a platform (internal) authenticator.

```csharp
public const string CtapTransportInternal = "internal"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_CTAP_TRANSPORT_INTERNAL_STRING.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_CtapTransportNfc"></a> CtapTransportNfc

```csharp
public const string CtapTransportNfc = "nfc"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_CTAP_TRANSPORT_NFC_STRING.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_CtapTransportSmartCard"></a> CtapTransportSmartCard

```csharp
public const string CtapTransportSmartCard = "smart-card"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_CTAP_TRANSPORT_SMART_CARD_STRING.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_CtapTransportUsb"></a> CtapTransportUsb

```csharp
public const string CtapTransportUsb = "usb"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_CTAP_TRANSPORT_USB_STRING.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_DefaultChallengeLength"></a> DefaultChallengeLength

Default length of the random challenges in bytes.

```csharp
public const int DefaultChallengeLength = 128
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_DefaultTimeoutMilliseconds"></a> DefaultTimeoutMilliseconds

Default timeout for WebAuthn operations.

```csharp
public const uint DefaultTimeoutMilliseconds = 60000
```

#### Field Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_ExtensionIdentifierCredBlob"></a> ExtensionIdentifierCredBlob

```csharp
public const string ExtensionIdentifierCredBlob = "credBlob"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_EXTENSIONS_IDENTIFIER_CRED_BLOB.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_ExtensionIdentifierCredProtect"></a> ExtensionIdentifierCredProtect

```csharp
public const string ExtensionIdentifierCredProtect = "credProtect"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_EXTENSIONS_IDENTIFIER_CRED_PROTECT.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_ExtensionIdentifierHmacSecret"></a> ExtensionIdentifierHmacSecret

```csharp
public const string ExtensionIdentifierHmacSecret = "hmac-secret"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_EXTENSIONS_IDENTIFIER_HMAC_SECRET.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_ExtensionIdentifierMinPinLength"></a> ExtensionIdentifierMinPinLength

```csharp
public const string ExtensionIdentifierMinPinLength = "minPinLength"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_EXTENSIONS_IDENTIFIER_MIN_PIN_LENGTH.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_HashAlgorithmSha256"></a> HashAlgorithmSha256

```csharp
public const string HashAlgorithmSha256 = "SHA-256"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_HASH_ALGORITHM_SHA_256.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_HashAlgorithmSha384"></a> HashAlgorithmSha384

```csharp
public const string HashAlgorithmSha384 = "SHA-384"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_HASH_ALGORITHM_SHA_384.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_HashAlgorithmSha512"></a> HashAlgorithmSha512

```csharp
public const string HashAlgorithmSha512 = "SHA-512"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_HASH_ALGORITHM_SHA_512.

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_KeePassXCAaGuid"></a> KeePassXCAaGuid

The AAGUID of the KeePassXC authenticator.

```csharp
public static readonly Guid KeePassXCAaGuid
```

#### Field Value

 [Guid](https://learn.microsoft.com/dotnet/api/system.guid)

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_MediationConditional"></a> MediationConditional

The mediation requirement value indicating conditional mediation (passkey autofill).

```csharp
public const string MediationConditional = "conditional"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_MediationOptional"></a> MediationOptional

The mediation requirement value indicating that the user agent may show UI to the user (default behavior).

```csharp
public const string MediationOptional = "optional"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_MediationRequired"></a> MediationRequired

The mediation requirement value indicating that the user agent must always show UI to the user.

```csharp
public const string MediationRequired = "required"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_MediationSilent"></a> MediationSilent

The mediation requirement value indicating that the user agent must not show any UI to the user.

```csharp
public const string MediationSilent = "silent"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Interop_ApiConstants_PublicKeyCredentialType"></a> PublicKeyCredentialType

The credential type for public-key credentials.

```csharp
public const string PublicKeyCredentialType = "public-key"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

Corresponds to WEBAUTHN_CREDENTIAL_TYPE_PUBLIC_KEY.

