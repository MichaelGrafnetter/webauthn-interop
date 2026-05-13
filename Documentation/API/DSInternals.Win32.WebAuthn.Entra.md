# <a id="DSInternals_Win32_WebAuthn_Entra"></a> Namespace DSInternals.Win32.WebAuthn.Entra

### Classes

 [MicrosoftGraphAttestationPublicKeyCredential](DSInternals.Win32.WebAuthn.Entra.MicrosoftGraphAttestationPublicKeyCredential.md)

Microsoft Graph variant of the attestation public key credential.
Only carries the fields accepted by the fido2AuthenticationMethod schema
(id, response, clientExtensionResults); the standard WebAuthn type,
rawId, and authenticatorAttachment fields are intentionally omitted.

 [MicrosoftGraphWebauthnAttestationResponse](DSInternals.Win32.WebAuthn.Entra.MicrosoftGraphWebauthnAttestationResponse.md)

Microsoft Graph payload used to submit a WebAuthn attestation result.

 [MicrosoftGraphWebauthnCredentialCreationOptions](DSInternals.Win32.WebAuthn.Entra.MicrosoftGraphWebauthnCredentialCreationOptions.md)

Defines the options for creating a new WebAuthn credential in Microsoft Graph's API.

