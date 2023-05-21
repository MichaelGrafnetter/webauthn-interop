# DSInternals.Win32.WebAuthn assembly

## DSInternals.Win32.WebAuthn namespace

| public type | description |
| --- | --- |
| enum [ApiVersion](./DSInternals.Win32.WebAuthn/ApiVersion.md) | WebAuthn API Version Information. |
| enum [AttestationConveyancePreference](./DSInternals.Win32.WebAuthn/AttestationConveyancePreference.md) | This enumeration contains attestation conveyance options for credential generation. |
| class [AuthenticationExtensionsClientInputs](./DSInternals.Win32.WebAuthn/AuthenticationExtensionsClientInputs.md) | Contains WebAuthn extensions that are actually supported by Windows 10. These are currently defined in CTAP 2.1 Draft. |
| class [AuthenticationExtensionsClientOutputs](./DSInternals.Win32.WebAuthn/AuthenticationExtensionsClientOutputs.md) | Contains WebAuthn extensions that are actually supported by Windows 10. These are currently defined in CTAP 2.1 Draft. |
| class [AuthenticatorAssertionResponse](./DSInternals.Win32.WebAuthn/AuthenticatorAssertionResponse.md) | The AuthenticatorAssertionResponse interface represents an authenticator's response to a client’s request for generation of a new authentication assertion given the WebAuthn Relying Party's challenge and OPTIONAL list of credentials it is aware of. This response contains a cryptographic signature proving possession of the credential private key, and optionally evidence of user consent to a specific transaction. |
| enum [AuthenticatorAttachment](./DSInternals.Win32.WebAuthn/AuthenticatorAttachment.md) | This enumeration’s values describe authenticators' attachment modalities. |
| class [AuthenticatorAttestationResponse](./DSInternals.Win32.WebAuthn/AuthenticatorAttestationResponse.md) | The AuthenticatorAttestationResponse class represents the authenticator's response to a client’s request for the creation of a new public key credential. It contains information about the new credential that can be used to identify it for later use, and metadata that can be used by the WebAuthn Relying Party to assess the characteristics of the credential during registration. |
| abstract class [AuthenticatorResponse](./DSInternals.Win32.WebAuthn/AuthenticatorResponse.md) | Authenticators respond to Relying Party requests by returning an object derived from the AuthenticatorResponse class. |
| [Flags] enum [AuthenticatorTransport](./DSInternals.Win32.WebAuthn/AuthenticatorTransport.md) | Defines hints as to how clients might communicate with a particular authenticator in order to obtain an assertion for a specific credential. |
| class [Base64UrlConverter](./DSInternals.Win32.WebAuthn/Base64UrlConverter.md) | Custom converter for encoding/decoding byte[] using Base64Url instead of default Base64. |
| class [CredentialDetails](./DSInternals.Win32.WebAuthn/CredentialDetails.md) | Information about credential. |
| enum [CredentialLargeBlobOperation](./DSInternals.Win32.WebAuthn/CredentialLargeBlobOperation.md) | The operation to perform on the large blob. |
| [Flags] enum [EnterpriseAttestationType](./DSInternals.Win32.WebAuthn/EnterpriseAttestationType.md) | Enterprise Attestation Capabilities |
| class [PublicKeyCredentialDescriptor](./DSInternals.Win32.WebAuthn/PublicKeyCredentialDescriptor.md) |  |
| class [RelyingPartyInformation](./DSInternals.Win32.WebAuthn/RelyingPartyInformation.md) | Information about an RP Entity |
| class [UserInformation](./DSInternals.Win32.WebAuthn/UserInformation.md) | Information about a user rntity. |
| enum [UserVerification](./DSInternals.Win32.WebAuthn/UserVerification.md) | Defines the credential protection policy. |
| enum [UserVerificationRequirement](./DSInternals.Win32.WebAuthn/UserVerificationRequirement.md) | This enumeration describes the Relying Party's requirements regarding user verification for the create() operation. Eligible authenticators are filtered to only those capable of satisfying this requirement. |
| class [WebAuthnApi](./DSInternals.Win32.WebAuthn/WebAuthnApi.md) | Windows WebAuthn API |
| struct [WindowHandle](./DSInternals.Win32.WebAuthn/WindowHandle.md) | Represents a window handle. |

## DSInternals.Win32.WebAuthn.ActiveDirectory namespace

| public type | description |
| --- | --- |
| class [CustomKeyInformation](./DSInternals.Win32.WebAuthn.ActiveDirectory/CustomKeyInformation.md) | Represents the CUSTOM_KEY_INFORMATION structure. |
| class [CustomKeyInformationConverter](./DSInternals.Win32.WebAuthn.ActiveDirectory/CustomKeyInformationConverter.md) | Converts the CustomKeyInformation class to and from a base 64 string value. |
| class [FidoKeyMaterial](./DSInternals.Win32.WebAuthn.ActiveDirectory/FidoKeyMaterial.md) | Represents a FIDO2 key stored in AAD. |
| class [KeyCredential](./DSInternals.Win32.WebAuthn.ActiveDirectory/KeyCredential.md) | This class represents a single AD/AAD key credential. |
| enum [KeyCredentialEntryType](./DSInternals.Win32.WebAuthn.ActiveDirectory/KeyCredentialEntryType.md) | Key Credential Link Entry Identifier |
| enum [KeyCredentialVersion](./DSInternals.Win32.WebAuthn.ActiveDirectory/KeyCredentialVersion.md) | Key Credential Link Blob Structure Version |
| [Flags] enum [KeyFlags](./DSInternals.Win32.WebAuthn.ActiveDirectory/KeyFlags.md) | Custom Key Flags |
| enum [KeySource](./DSInternals.Win32.WebAuthn.ActiveDirectory/KeySource.md) | Key Source |
| enum [KeyStrength](./DSInternals.Win32.WebAuthn.ActiveDirectory/KeyStrength.md) | Specifies the strength of the NGC key. |
| enum [KeyUsage](./DSInternals.Win32.WebAuthn.ActiveDirectory/KeyUsage.md) | Key Usage |
| static class [RSAExtensions](./DSInternals.Win32.WebAuthn.ActiveDirectory/RSAExtensions.md) |  |
| enum [VolumeType](./DSInternals.Win32.WebAuthn.ActiveDirectory/VolumeType.md) | Specifies the volume type. |

## DSInternals.Win32.WebAuthn.COSE namespace

| public type | description |
| --- | --- |
| enum [Algorithm](./DSInternals.Win32.WebAuthn.COSE/Algorithm.md) | Well-known COSE algorithm specifying the algorithm to use for the credential. https://www.iana.org/assignments/cose/cose.xhtml#algorithms |
| enum [EllipticCurve](./DSInternals.Win32.WebAuthn.COSE/EllipticCurve.md) | COSE Elliptic Curves https://www.iana.org/assignments/cose/cose.xhtml#elliptic-curves |
| enum [KeyCommonParameter](./DSInternals.Win32.WebAuthn.COSE/KeyCommonParameter.md) | COSE Key Common Parameters https://www.iana.org/assignments/cose/cose.xhtml#key-common-parameters |
| enum [KeyType](./DSInternals.Win32.WebAuthn.COSE/KeyType.md) | COSE Key Types https://www.iana.org/assignments/cose/cose.xhtml#key-type |
| enum [KeyTypeParameter](./DSInternals.Win32.WebAuthn.COSE/KeyTypeParameter.md) | COSE Key Type Parameters |

## DSInternals.Win32.WebAuthn.FIDO namespace

| public type | description |
| --- | --- |
| class [AttestedCredentialData](./DSInternals.Win32.WebAuthn.FIDO/AttestedCredentialData.md) | Attested credential data is a variable-length byte array added to the authenticator data when generating an attestation object for a given credential. |
| class [AuthenticatorData](./DSInternals.Win32.WebAuthn.FIDO/AuthenticatorData.md) | Contains information from the authenticator about the processing of a credential creation or authentication request. |
| [Flags] enum [AuthenticatorFlags](./DSInternals.Win32.WebAuthn.FIDO/AuthenticatorFlags.md) | Authenticator data flags |
| class [CollectedClientData](./DSInternals.Win32.WebAuthn.FIDO/CollectedClientData.md) |  |
| class [CredentialPublicKey](./DSInternals.Win32.WebAuthn.FIDO/CredentialPublicKey.md) |  |
| enum [LargeBlobSupport](./DSInternals.Win32.WebAuthn.FIDO/LargeBlobSupport.md) | Large blob support options. |

## DSInternals.Win32.WebAuthn.Interop namespace

| public type | description |
| --- | --- |
| static class [ApiConstants](./DSInternals.Win32.WebAuthn.Interop/ApiConstants.md) | Contains WebAuthn API constants. Most of them are auto-generated from webauthn.h. |

<!-- DO NOT EDIT: generated by xmldocmd for DSInternals.Win32.WebAuthn.dll -->
