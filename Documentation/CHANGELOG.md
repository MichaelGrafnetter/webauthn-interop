# Changelog

All notable changes to this project will be documented in this file. The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

## [Unreleased]

## [3.2.0] - 2026-06-04

### Added

- Added the `-BrowserInPrivateMode` switch (aliases `-PrivateMode`, `-Private`) to the `Test-Passkey` and `Get-PasskeyWindowsHello` cmdlets, signaling that the request originates from a browser running in private/incognito mode. When this switch is used, no information about the operation is written to the Windows event log.

### Changed

- `WebAuthnApi.AuthenticatorGetAssertion` now prefers the native `pbAuthenticationResponseJSON` returned in `WEBAUTHN_ASSERTION_VERSION_6` when present, deserializing the full credential (including `clientExtensionResults`) verbatim from the OS-produced JSON. The legacy field-by-field assembly and per-extension recomputation remain as a fallback for older OS versions.
- `WebAuthnApi.AuthenticatorMakeCredential` now prefers the native `pbRegistrationResponseJSON` returned in `WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_8` when present, deserializing the full credential verbatim from the OS-produced JSON. The legacy field-by-field assembly and per-extension recomputation remain as a fallback for older OS versions.
- The `AuthenticatorDetails.AuthenticatorId` property (returned by `Get-PasskeyAuthenticator`) was renamed to `AaGuid` and its type changed from `byte[]` to `Guid?`. The binary identifier returned by the Win32 API is a big-endian encoded Authenticator Attestation GUID (AAGUID), so it is now decoded and surfaced as a `Guid` (or `null` when absent) instead of a Base64Url-encoded string.

## [3.1.0] - 2026-05-14

### Added

- Added the `Get-PasskeyCreationOptions` cmdlet for building WebAuthn credential creation options locally, without contacting a remote identity provider.
- Added the `Tenant` property to `OktaWebauthnCredentialCreationOptions`, carrying the live Okta tenant host out-of-band instead of mutating `rp.id` after deserialization.
- Added Pester integration tests covering the PowerShell module manifest, file layout, exported aliases, MAML help, and bundled assemblies.
- `DSInternals.Win32.WebAuthn.Adapter` now translates credential hints, credential protection policy, large-blob extension inputs/outputs, PRF extension inputs/outputs, and credential identifiers (`Id`/`RawId`) so attestation and assertion responses are fully populated for `Fido2.Models` callers.

### Changed

- **PowerShell module restructure.** The monolithic `DSInternals.Passkeys.psm1` was split into three nested modules — `DSInternals.Passkeys.Core.psm1`, `DSInternals.Passkeys.Entra.psm1`, and `DSInternals.Passkeys.Okta.psm1`. Provider-specific cmdlets were renamed:
  - `Get-PasskeyRegistrationOptions` → `Get-EntraPasskeyRegistrationOptions` / `Get-OktaPasskeyRegistrationOptions`
  - `Register-Passkey` → `Register-EntraPasskey` / `Register-OktaPasskey`

  The previous names are kept as aliases.
- `AuthenticatorMakeCredential` and `AuthenticatorMakeCredentialAsync` now accept an optional `hostName` argument that is used to derive the WebAuthn origin and to fill in a missing relying party identifier.
- `AuthenticatorSelectionCriteria.ResidentKey` is now nullable, allowing it to default to *Preferred* when not specified by the caller.

### Fixed

- Fixed Entra and Okta passkey registration.

## [3.0] - 2026-05-07

### Added

- Added Credential Exchange Format (CXF) parsing.
- Added Bitwarden vault export support, covering both encrypted and cleartext exports.
- Added KeePassXC passkey export parsing.
- Added a software authenticator with Ed25519 signing for offline testing without a physical authenticator.
- Added User Verification Method (UVM), Secure Payment Confirmation (SPC), PRF, Large Blob, Credential Properties, and Remote Desktop client override extension models.

### Changed

- Modernized the `WebAuthnApi` surface, including async cancellation handling and clearer marshaling of assertion/attestation options structs.

## [2.1.1] - 2026-04-05

### Fixed

- Fixed `CredentialId` and `AuthenticatorId` properties not being displayed as Base64Url strings in the output of `Get-PasskeyWindowsHello` and `Get-PasskeyAuthenticator` cmdlets.

## [2.1] - 2026-04-05

### Added

- Added the `-Hint` parameter to the `Test-Passkey` cmdlet for specifying which credential source to use (e.g., SecurityKey, ClientDevice, Hybrid).

### Changed

- Added `UnauthorizedAccessException` mapping for `RPC_E_ACCESS_DENIED` in HRESULT error handling. This error occurs when the WebAuthn API is currently being used by another application.
- Simplified `Base64UrlConverter` by using `Base64Url.DecodeFromUtf8` for decoding.

## [2.0] - 2026-04-03

### Added

- Added support for Okta as a passkey identity provider, including `Connect-Okta`, `Disconnect-Okta`, and extended `Get-PasskeyRegistrationOptions`/`Register-Passkey` cmdlets. (Thanks to @aseigler)
- Updated to WebAuthn API v9, adding support for new authenticator hints and large blobs.
- Added new PowerShell cmdlets: `Get-PasskeyAuthenticator`, `Get-PasskeyAuthenticatorPlugin`, `Get-PasskeyWindowsHello`, `New-PasskeyRandomChallenge`, `Remove-PasskeyWindowsHello`, and `Test-Passkey`.
- Added a software-based signing feature to Passkey UI, enabling offline testing without a physical authenticator.
- Added the Event Log tab to Passkey UI that displays successful passkey registration and authentication operations.

### Changed

- The `AuthenticatorGetAssertion` and `AuthenticatorGetAssertionAsync` methods now return `PublicKeyCredential` instead of `AuthenticatorAssertionResponse`.
- Migrated Passkey UI from .NET Framework 4.8 to .NET 10, now published as a self-contained single-file executable.
- Updated the application icon to a key emoji (🔑).
- Renamed the desktop application from "FIDO2 UI" to "Passkey UI".
- Migrated solution from `.sln` to `.slnx` format.
- Replaced the `PeterO.Cbor` library with `System.Formats.Cbor`.

### Fixed

- Improved JSON serialization.

## [1.0.6] - 2025-01-16

This is a bugfix release. Huge thanks to @aseigler for reporting and fixing the issues.

### Fixed

- Implemented a workaround for `PublicKeyCredentialCreationOptions` generated by Entra ID with corrupted BASE64 padding. (Issue #21)
- PowerShell cmdlets should now work even when executed with a service principal identity.
- Resolved the "InvalidOperation: Unable to find type [Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod]" error when trying to register a passkey.

## [1.0.5] - 2024-11-14

### Fixed

- Resolved a bug which prevented the `DeletePlatformCredential()` method from working. (Thanks @aseigler for reporting it.)
- Upgraded System.Text.Json to mitigate [CVE-2024-43485](https://msrc.microsoft.com/update-guide/vulnerability/CVE-2024-43485).

## [1.0.4] - 2024-10-06

### Added

- Implemented EdDSA support (Thanks to @aseigler)

### Changed

- Switched to EV code signing certificate hosted at Azure Trusted Signing.

## [1.0.3] - 2024-08-16

### Fixed

- Resolved issues with HMAC secret and linked device information processing.

## [1.0.2] - 2024-08-13

### Fixed

- All assemblies that are part of the PowerShell module and Nuget packages are now digitally signed.

## 1.0.1 - 2024-08-11

### Fixed

- Renamed the `New-MgUserAuthenticationFido2Method` alias to `Register-MgUserAuthenticationFido2Method` because of a naming conflict with `Microsoft Graph PowerShell SDK`.

## [1.0] - 2024-08-11

- Initial version

[Unreleased]: https://github.com/MichaelGrafnetter/webauthn-interop/compare/v3.2.0...HEAD
[3.2.0]: https://github.com/MichaelGrafnetter/webauthn-interop/compare/v3.1.0...v3.2.0
[3.1.0]: https://github.com/MichaelGrafnetter/webauthn-interop/compare/v3.0...v3.1.0
[3.0]: https://github.com/MichaelGrafnetter/webauthn-interop/compare/v2.1.1...v3.0
[2.1.1]: https://github.com/MichaelGrafnetter/webauthn-interop/compare/v2.1...v2.1.1
[2.1]: https://github.com/MichaelGrafnetter/webauthn-interop/compare/v2.0...v2.1
[2.0]: https://github.com/MichaelGrafnetter/webauthn-interop/compare/v1.0.6...v2.0
[1.0.6]: https://github.com/MichaelGrafnetter/webauthn-interop/compare/v1.0.5...v1.0.6
[1.0.5]: https://github.com/MichaelGrafnetter/webauthn-interop/compare/v1.0.4...v1.0.5
[1.0.4]: https://github.com/MichaelGrafnetter/webauthn-interop/compare/v1.0.3...v1.0.4
[1.0.3]: https://github.com/MichaelGrafnetter/webauthn-interop/compare/v1.0.2...v1.0.3
[1.0.2]: https://github.com/MichaelGrafnetter/webauthn-interop/compare/v1.0...v1.0.2
[1.0]: https://github.com/MichaelGrafnetter/webauthn-interop/releases/tag/v1.0
