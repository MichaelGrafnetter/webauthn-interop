# DSInternals.Win32.WebAuthn.Adapter

**Bridge between [Fido2.Models](https://www.nuget.org/packages/Fido2.Models/) and [DSInternals.Win32.WebAuthn](https://www.nuget.org/packages/DSInternals.Win32.WebAuthn/)**

This package lets applications already built on top of [fido2-net-lib](https://github.com/abergs/fido2-net-lib) drive the native Windows WebAuthn API. It accepts the `Fido2.Models` request/response types as input, translates them to the native structures consumed by `DSInternals.Win32.WebAuthn`, and maps the results back.

If you do not already use `Fido2.Models` in your codebase, use the [DSInternals.Win32.WebAuthn](https://www.nuget.org/packages/DSInternals.Win32.WebAuthn/) package directly instead.

## Requirements

- Windows 10 version 1903 or newer
- .NET 10

## Usage

The main entry point is the `WebAuthnApiAdapter` class in the `DSInternals.Win32.WebAuthn.Adapter` namespace. It exposes methods that accept `Fido2.Models` option types (e.g. `CredentialCreateOptions`, `AssertionOptions`) and return the corresponding `Fido2.Models` response types.

## Related Packages

- [DSInternals.Win32.WebAuthn](https://www.nuget.org/packages/DSInternals.Win32.WebAuthn/) — the underlying managed wrapper of the Windows WebAuthn API.
- [Fido2.Models](https://www.nuget.org/packages/Fido2.Models/) — the model types this adapter consumes and produces.

## Links

- [Project site and source code](https://github.com/MichaelGrafnetter/webauthn-interop)
- [API documentation](https://github.com/MichaelGrafnetter/webauthn-interop/blob/main/Documentation/API/DSInternals.Win32.WebAuthn.Adapter.md)
- [Changelog](https://github.com/MichaelGrafnetter/webauthn-interop/blob/main/Documentation/CHANGELOG.md)
- [Report an issue](https://github.com/MichaelGrafnetter/webauthn-interop/issues)

## License

Released under the [MIT License](https://github.com/MichaelGrafnetter/webauthn-interop/blob/main/LICENSE).
