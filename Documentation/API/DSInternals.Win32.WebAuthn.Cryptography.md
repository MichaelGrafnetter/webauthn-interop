# <a id="DSInternals_Win32_WebAuthn_Cryptography"></a> Namespace DSInternals.Win32.WebAuthn.Cryptography

### Classes

 [BitwardenCleartextVaultExport](DSInternals.Win32.WebAuthn.Cryptography.BitwardenCleartextVaultExport.md)

Represents the top-level structure of a cleartext Bitwarden vault export.

 [BitwardenEncryptedVaultExport](DSInternals.Win32.WebAuthn.Cryptography.BitwardenEncryptedVaultExport.md)

Represents the top-level structure of an encrypted Bitwarden vault export.

 [BitwardenFido2Credential](DSInternals.Win32.WebAuthn.Cryptography.BitwardenFido2Credential.md)

Represents a FIDO2 credential entry embedded in a Bitwarden login item.

 [BitwardenLogin](DSInternals.Win32.WebAuthn.Cryptography.BitwardenLogin.md)

Represents the login section of a Bitwarden vault item.

 [BitwardenVaultExportHeader](DSInternals.Win32.WebAuthn.Cryptography.BitwardenVaultExportHeader.md)

Represents the minimal Bitwarden export header used to choose a parser.

 [BitwardenVaultItem](DSInternals.Win32.WebAuthn.Cryptography.BitwardenVaultItem.md)

Represents a Bitwarden vault item that may contain login passkeys.

 [CredentialExchangeAccount](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeAccount.md)

Represents a single user account in a FIDO Credential Exchange Format payload.

 [CredentialExchangeCredential](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeCredential.md)

Base type for entries inside a CXF item's credentials array.

 [CredentialExchangeFido2Extensions](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeFido2Extensions.md)

Represents the FIDO2 extension state recorded for a CXF passkey credential.

 [CredentialExchangeFido2HmacCredentials](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeFido2HmacCredentials.md)

Represents the FIDO2 hmac-secret extension state captured in a CXF passkey.

 [CredentialExchangeFido2LargeBlob](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeFido2LargeBlob.md)

Represents the FIDO2 largeBlob extension payload stored on a CXF passkey.

 [CredentialExchangeFile](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeFile.md)

Represents the top-level Header of a FIDO Credential Exchange Format (CXF) payload.

 [CredentialExchangeItem](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeItem.md)

Represents a single CXF item that may bundle one or more credentials.

 [CredentialExchangePasskey](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangePasskey.md)

Represents a CXF credential of type <code>passkey</code>.

 [CredentialExchangeVersion](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeVersion.md)

Represents the version of the FIDO Credential Exchange Format payload.

 [Ed25519](DSInternals.Win32.WebAuthn.Cryptography.Ed25519.md)

An <xref href="System.Security.Cryptography.AsymmetricAlgorithm" data-throw-if-not-resolved="false"></xref> wrapper around an NSec Ed25519 private key.

 [ExportedPasskey](DSInternals.Win32.WebAuthn.Cryptography.ExportedPasskey.md)

Represents a passkey imported from an external authenticator export.

 [KeePassXCPasskey](DSInternals.Win32.WebAuthn.Cryptography.KeePassXCPasskey.md)

Represents a passkey exported from KeePassXC (.passkey JSON format).

 [SoftwareAuthenticator](DSInternals.Win32.WebAuthn.Cryptography.SoftwareAuthenticator.md)

Software-based authenticator that signs WebAuthn requests using a PEM private key file,
without requiring a hardware authenticator or the Windows WebAuthn API.

