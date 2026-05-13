# <a id="DSInternals_Win32_WebAuthn_FIDO"></a> Namespace DSInternals.Win32.WebAuthn.FIDO

### Classes

 [AttestedCredentialData](DSInternals.Win32.WebAuthn.FIDO.AttestedCredentialData.md)

Attested credential data is a variable-length byte array added to the authenticator
data when generating an attestation object for a given credential.

 [AuthenticatorData](DSInternals.Win32.WebAuthn.FIDO.AuthenticatorData.md)

Represents parsed authenticator data from a WebAuthn assertion.

 [CollectedClientData](DSInternals.Win32.WebAuthn.FIDO.CollectedClientData.md)

The client data collected during a WebAuthn ceremony, serialized as the clientDataJSON field.
Property declaration order is significant: the WebAuthn spec requires type → challenge → origin → crossOrigin.

### Enums

 [KeyProtectionType](DSInternals.Win32.WebAuthn.FIDO.KeyProtectionType.md)

Methods used by the authenticator to protect FIDO registration private key material, as reported in the WebAuthn uvm extension.

 [MatcherProtectionType](DSInternals.Win32.WebAuthn.FIDO.MatcherProtectionType.md)

Methods used by the authenticator to protect the matcher that performs user verification, as reported in the WebAuthn uvm extension.

 [UserVerificationMethod](DSInternals.Win32.WebAuthn.FIDO.UserVerificationMethod.md)

User verification methods reported by the authenticator in the WebAuthn uvm extension.

