# <a id="DSInternals_Win32_WebAuthn"></a> Namespace DSInternals.Win32.WebAuthn

### Namespaces

 [DSInternals.Win32.WebAuthn.Adapter](DSInternals.Win32.WebAuthn.Adapter.md)

 [DSInternals.Win32.WebAuthn.COSE](DSInternals.Win32.WebAuthn.COSE.md)

 [DSInternals.Win32.WebAuthn.FIDO](DSInternals.Win32.WebAuthn.FIDO.md)

 [DSInternals.Win32.WebAuthn.Interop](DSInternals.Win32.WebAuthn.Interop.md)

### Classes

 [AuthenticationExtensionsClientInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientInputs.md)

Contains WebAuthn extensions that are actually supported by Windows 10.
These are currently defined in CTAP 2.1 Draft.

 [AuthenticationExtensionsClientOutputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientOutputs.md)

Contains WebAuthn extensions that are actually supported by Windows 10.
These are currently defined in CTAP 2.1 Draft.

 [AuthenticatorAssertionResponse](DSInternals.Win32.WebAuthn.AuthenticatorAssertionResponse.md)

The AuthenticatorAssertionResponse interface represents an authenticator's response
to a client’s request for generation of a new authentication assertion given
the WebAuthn Relying Party's challenge and OPTIONAL list of credentials it is aware of.
This response contains a cryptographic signature proving possession of the credential private key,
and optionally evidence of user consent to a specific transaction.

 [AuthenticatorAttestationResponse](DSInternals.Win32.WebAuthn.AuthenticatorAttestationResponse.md)

The AuthenticatorAttestationResponse class represents the authenticator's response
to a client’s request for the creation of a new public key credential.
It contains information about the new credential that can be used to identify it for later use,
and metadata that can be used by the WebAuthn Relying Party to assess the characteristics
of the credential during registration.

 [AuthenticatorResponse](DSInternals.Win32.WebAuthn.AuthenticatorResponse.md)

Authenticators respond to Relying Party requests by returning an object derived from the AuthenticatorResponse class.

 [AuthenticatorSelection](DSInternals.Win32.WebAuthn.AuthenticatorSelection.md)

 [Base64UrlConverter](DSInternals.Win32.WebAuthn.Base64UrlConverter.md)

Custom converter for encoding/decoding byte[] using Base64Url instead of default Base64.

 [CredentialDetails](DSInternals.Win32.WebAuthn.CredentialDetails.md)

Information about credential.

 [HMACGetSecretInput](DSInternals.Win32.WebAuthn.HMACGetSecretInput.md)

The inputs to the hmac secret if it was created during registration.

 [HMACGetSecretOutput](DSInternals.Win32.WebAuthn.HMACGetSecretOutput.md)

The response to a hmac get secret request.

 [HexConverter](DSInternals.Win32.WebAuthn.HexConverter.md)

 [JsonCustomEnumConverter<TEnum\>](DSInternals.Win32.WebAuthn.JsonCustomEnumConverter\-1.md)

 [MicrosoftGraphWebauthnAttestationResponse](DSInternals.Win32.WebAuthn.MicrosoftGraphWebauthnAttestationResponse.md)

 [MicrosoftGraphWebauthnCredentialCreationOptions](DSInternals.Win32.WebAuthn.MicrosoftGraphWebauthnCredentialCreationOptions.md)

 [PublicKeyCredential](DSInternals.Win32.WebAuthn.PublicKeyCredential.md)

 [PublicKeyCredentialCreationOptions](DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions.md)

 [PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)

 [PublicKeyCredentialParameter](DSInternals.Win32.WebAuthn.PublicKeyCredentialParameter.md)

 [RelyingPartyInformation](DSInternals.Win32.WebAuthn.RelyingPartyInformation.md)

Information about an RP Entity

 [UserInformation](DSInternals.Win32.WebAuthn.UserInformation.md)

Information about a user rntity.

 [WebAuthnApi](DSInternals.Win32.WebAuthn.WebAuthnApi.md)

Windows WebAuthn API

### Structs

 [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

Represents a window handle.

### Enums

 [ApiVersion](DSInternals.Win32.WebAuthn.ApiVersion.md)

WebAuthn API Version Information.

 [AttestationConveyancePreference](DSInternals.Win32.WebAuthn.AttestationConveyancePreference.md)

This enumeration contains attestation conveyance options for credential generation.

 [AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)

This enumeration’s values describe authenticators' attachment modalities.

 [AuthenticatorFlags](DSInternals.Win32.WebAuthn.AuthenticatorFlags.md)

Authenticator data flags

 [AuthenticatorTransport](DSInternals.Win32.WebAuthn.AuthenticatorTransport.md)

Defines hints as to how clients might communicate with a particular authenticator in order to obtain an assertion for a specific credential.

 [CredentialLargeBlobOperation](DSInternals.Win32.WebAuthn.CredentialLargeBlobOperation.md)

The operation to perform on the large blob.

 [EnterpriseAttestationType](DSInternals.Win32.WebAuthn.EnterpriseAttestationType.md)

Enterprise Attestation Capabilities

 [LargeBlobSupport](DSInternals.Win32.WebAuthn.LargeBlobSupport.md)

Large blob support options.

 [UserVerification](DSInternals.Win32.WebAuthn.UserVerification.md)

Defines the credential protection policy.

 [UserVerificationRequirement](DSInternals.Win32.WebAuthn.UserVerificationRequirement.md)

This enumeration describes the Relying Party's requirements regarding user verification for the create() operation.
Eligible authenticators are filtered to only those capable of satisfying this requirement.

