# Changelog

All notable changes to this project will be documented in this file. The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

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

[Unreleased]: https://github.com/MichaelGrafnetter/webauthn-interop/compare/v1.0.4...HEAD
[1.0.4]: https://github.com/MichaelGrafnetter/webauthn-interop/compare/v1.0.3...v1.0.4
[1.0.3]: https://github.com/MichaelGrafnetter/webauthn-interop/compare/v1.0.2...v1.0.3
[1.0.2]: https://github.com/MichaelGrafnetter/webauthn-interop/compare/v1.0...v1.0.2
[1.0]: https://github.com/MichaelGrafnetter/webauthn-interop/releases/tag/v1.0
