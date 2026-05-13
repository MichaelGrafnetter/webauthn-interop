# <a id="DSInternals_Win32_WebAuthn"></a> Namespace DSInternals.Win32.WebAuthn

### Namespaces

 [DSInternals.Win32.WebAuthn.Adapter](DSInternals.Win32.WebAuthn.Adapter.md)

 [DSInternals.Win32.WebAuthn.COSE](DSInternals.Win32.WebAuthn.COSE.md)

 [DSInternals.Win32.WebAuthn.Cryptography](DSInternals.Win32.WebAuthn.Cryptography.md)

 [DSInternals.Win32.WebAuthn.Entra](DSInternals.Win32.WebAuthn.Entra.md)

 [DSInternals.Win32.WebAuthn.Events](DSInternals.Win32.WebAuthn.Events.md)

 [DSInternals.Win32.WebAuthn.FIDO](DSInternals.Win32.WebAuthn.FIDO.md)

 [DSInternals.Win32.WebAuthn.Interop](DSInternals.Win32.WebAuthn.Interop.md)

 [DSInternals.Win32.WebAuthn.Okta](DSInternals.Win32.WebAuthn.Okta.md)

### Classes

 [AssertionPublicKeyCredential](DSInternals.Win32.WebAuthn.AssertionPublicKeyCredential.md)

Represents the public key credential returned by a WebAuthn assertion operation.

 [AttestationPublicKeyCredential](DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential.md)

Represents the public key credential returned by a WebAuthn attestation operation.

 [AuthenticationExtensionsClientAssertionInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAssertionInputs.md)

Client extension inputs for WebAuthn assertions.

 [AuthenticationExtensionsClientAssertionOutputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAssertionOutputs.md)

Client extension outputs from WebAuthn assertions.

 [AuthenticationExtensionsClientAttestationInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAttestationInputs.md)

Client extension inputs for WebAuthn credential creation.

 [AuthenticationExtensionsClientAttestationOutputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAttestationOutputs.md)

Client extension outputs from WebAuthn credential creation.

 [AuthenticationExtensionsClientOutputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientOutputs.md)

Base type for WebAuthn client extension outputs.

 [AuthenticatorAssertionResponse](DSInternals.Win32.WebAuthn.AuthenticatorAssertionResponse.md)

Represents an authenticator assertion response.

 [AuthenticatorAttestationResponse](DSInternals.Win32.WebAuthn.AuthenticatorAttestationResponse.md)

Represents an authenticator attestation response.

 [AuthenticatorDetails](DSInternals.Win32.WebAuthn.AuthenticatorDetails.md)

Information about an authenticator.

 [AuthenticatorPluginInformation](DSInternals.Win32.WebAuthn.AuthenticatorPluginInformation.md)

Represents information about an authenticator plugin registered in Windows.

 [AuthenticatorResponse](DSInternals.Win32.WebAuthn.AuthenticatorResponse.md)

Authenticators respond to Relying Party requests by returning an object derived from the AuthenticatorResponse class.

 [AuthenticatorSelectionCriteria](DSInternals.Win32.WebAuthn.AuthenticatorSelectionCriteria.md)

Specifies the Relying Party's requirements regarding authenticator attributes.

 [Base64UrlConverter](DSInternals.Win32.WebAuthn.Base64UrlConverter.md)

Custom converter for encoding/decoding byte[] using Base64Url instead of default Base64.

 [BooleanConverter](DSInternals.Win32.WebAuthn.BooleanConverter.md)

Custom converter for reading Boolean values from either JSON Boolean or string tokens.

 [CredentialDetails](DSInternals.Win32.WebAuthn.CredentialDetails.md)

Information about credential.

 [CredentialPropertiesOutputs](DSInternals.Win32.WebAuthn.CredentialPropertiesOutputs.md)

Outputs for the WebAuthn credProps client extension.

 [GuidPolyfill](DSInternals.Win32.WebAuthn.GuidPolyfill.md)

Provides compatibility members for <xref href="System.Guid" data-throw-if-not-resolved="false"></xref> APIs added in newer .NET versions.

 [HMACGetSecretInput](DSInternals.Win32.WebAuthn.HMACGetSecretInput.md)

The inputs to the hmac secret if it was created during registration.

 [HMACGetSecretOutput](DSInternals.Win32.WebAuthn.HMACGetSecretOutput.md)

The response to a hmac get secret request.

 [LargeBlobAssertionInputs](DSInternals.Win32.WebAuthn.LargeBlobAssertionInputs.md)

largeBlob inputs for WebAuthn assertions.

 [LargeBlobAssertionOutputs](DSInternals.Win32.WebAuthn.LargeBlobAssertionOutputs.md)

largeBlob outputs from WebAuthn assertions.

 [LargeBlobAttestationInputs](DSInternals.Win32.WebAuthn.LargeBlobAttestationInputs.md)

largeBlob inputs for WebAuthn credential creation.

 [LargeBlobAttestationOutputs](DSInternals.Win32.WebAuthn.LargeBlobAttestationOutputs.md)

largeBlob outputs from WebAuthn credential creation.

 [MediationExtensions](DSInternals.Win32.WebAuthn.MediationExtensions.md)

Extension methods for known credential mediation requirements.

 [PRFAssertionInputs](DSInternals.Win32.WebAuthn.PRFAssertionInputs.md)

prf inputs for WebAuthn assertions.

 [PRFAssertionOutputs](DSInternals.Win32.WebAuthn.PRFAssertionOutputs.md)

prf outputs from WebAuthn assertions.

 [PRFAttestationInputs](DSInternals.Win32.WebAuthn.PRFAttestationInputs.md)

prf inputs for WebAuthn credential creation.

 [PRFAttestationOutputs](DSInternals.Win32.WebAuthn.PRFAttestationOutputs.md)

prf outputs from WebAuthn credential creation.

 [PRFValues](DSInternals.Win32.WebAuthn.PRFValues.md)

One or two WebAuthn PRF inputs or outputs.

 [PaymentAssertionInputs](DSInternals.Win32.WebAuthn.PaymentAssertionInputs.md)

Inputs for the Secure Payment Confirmation (SPC) <code>payment</code> WebAuthn extension during assertion.

 [PaymentAttestationInputs](DSInternals.Win32.WebAuthn.PaymentAttestationInputs.md)

Inputs for the Secure Payment Confirmation (SPC) <code>payment</code> WebAuthn extension during credential creation.

 [PaymentCredentialInstrument](DSInternals.Win32.WebAuthn.PaymentCredentialInstrument.md)

Payment instrument descriptor displayed during a Secure Payment Confirmation ceremony.

 [PaymentCurrencyAmount](DSInternals.Win32.WebAuthn.PaymentCurrencyAmount.md)

Currency and amount displayed during a Secure Payment Confirmation ceremony.

 [PublicKeyCredential<TResponse, TClientExtensionResults\>](DSInternals.Win32.WebAuthn.PublicKeyCredential\-2.md)

Represents a WebAuthn public key credential.

 [PublicKeyCredentialCreationOptions](DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions.md)

Represents the options for a WebAuthn credential creation (navigator.credentials.create()).

 [PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)

Identifies an existing public key credential.

 [PublicKeyCredentialHintExtensions](DSInternals.Win32.WebAuthn.PublicKeyCredentialHintExtensions.md)

Extension methods for known public key credential hints.

 [PublicKeyCredentialParameter](DSInternals.Win32.WebAuthn.PublicKeyCredentialParameter.md)

Describes an acceptable public key credential algorithm and type pair.

 [PublicKeyCredentialRequestOptions](DSInternals.Win32.WebAuthn.PublicKeyCredentialRequestOptions.md)

Represents the options for a WebAuthn credential request (navigator.credentials.get()).

 [RelyingPartyInformation](DSInternals.Win32.WebAuthn.RelyingPartyInformation.md)

Information about an RP Entity

 [RemoteDesktopClientOverride](DSInternals.Win32.WebAuthn.RemoteDesktopClientOverride.md)

Inputs for Chromium's remoteDesktopClientOverride WebAuthn extension.

 [UserInformation](DSInternals.Win32.WebAuthn.UserInformation.md)

Information about a user rntity.

 [UvmEntry](DSInternals.Win32.WebAuthn.UvmEntry.md)

A single user verification method factor reported by the authenticator in the WebAuthn uvm extension output.

 [UvmEntryConverter](DSInternals.Win32.WebAuthn.UvmEntryConverter.md)

Converts a <xref href="DSInternals.Win32.WebAuthn.UvmEntry" data-throw-if-not-resolved="false"></xref> to and from a 3-element JSON array of unsigned numbers, as defined by the WebAuthn uvm extension.

 [WebAuthnApi](DSInternals.Win32.WebAuthn.WebAuthnApi.md)

Windows WebAuthn API

 [WebAuthnJsonContext](DSInternals.Win32.WebAuthn.WebAuthnJsonContext.md)

Source-generated JSON serialization metadata for WebAuthn models.

 [WebAuthnJsonEnumConverter<TEnum\>](DSInternals.Win32.WebAuthn.WebAuthnJsonEnumConverter\-1.md)

Converts WebAuthn enum values with a uint underlying type to and from their JSON string representations.

### Structs

 [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

Represents a window handle.

### Enums

 [ApiVersion](DSInternals.Win32.WebAuthn.ApiVersion.md)

WebAuthn API Version Information.

 [AttestationConveyancePreference](DSInternals.Win32.WebAuthn.AttestationConveyancePreference.md)

This enumeration contains attestation conveyance options for credential generation.

 [AttestationStatementFormatIdentifier](DSInternals.Win32.WebAuthn.AttestationStatementFormatIdentifier.md)

WebAuthn attestation statement format identifiers registered with IANA.

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

 [Mediation](DSInternals.Win32.WebAuthn.Mediation.md)

Known credential mediation requirements as defined by the Credential Management specification.

 [PackageSignatureKind](DSInternals.Win32.WebAuthn.PackageSignatureKind.md)

The kind of package signature for an authenticator plugin.

 [PublicKeyCredentialHint](DSInternals.Win32.WebAuthn.PublicKeyCredentialHint.md)

Known public key credential hints as defined in the WebAuthn specification.

 [ResidentKeyRequirement](DSInternals.Win32.WebAuthn.ResidentKeyRequirement.md)

This enumeration's values describe the Relying Party's requirements for client-side discoverable credentials
(formerly known as resident credentials or resident keys).

 [UserVerification](DSInternals.Win32.WebAuthn.UserVerification.md)

Defines the credential protection policy.

 [UserVerificationRequirement](DSInternals.Win32.WebAuthn.UserVerificationRequirement.md)

This enumeration describes the Relying Party's requirements regarding user verification for the create() operation.
Eligible authenticators are filtered to only those capable of satisfying this requirement.

