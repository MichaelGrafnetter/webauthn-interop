# DSInternals.Win32.WebAuthn

**Passkeys / FIDO2 / W3C Web Authentication .NET Library for Windows Desktop and CLI Applications**

`DSInternals.Win32.WebAuthn` is a managed wrapper of the low-level [Windows 10+ WebAuthn API](https://learn.microsoft.com/en-us/windows/win32/api/_webauthn/) (defined in `webauthn.h` and implemented in `webauthn.dll`). It allows .NET applications to directly interact with passkeys and FIDO2 authenticators — including Windows Hello, Microsoft Authenticator, YubiKey, Feitian, and Crayonic — on Windows.

The same API is used by browsers such as Chromium and Firefox to implement passwordless web authentication, and can also be used by any .NET desktop or CLI application.

## Requirements

- Windows 10 version 1903 or newer
- One of: .NET 10, .NET 8, or .NET Framework 4.8

## Usage

The main entry point is the `WebAuthnApi` class in the `DSInternals.Win32.WebAuthn` namespace.

### Registration (Attestation)

```csharp
using DSInternals.Win32.WebAuthn;

var rp = new RelyingPartyInformation()
{
    Id = "login.microsoft.com",
    Name = "Microsoft"
};

var user = new UserInformation()
{
    Name = "john.doe@outlook.com",
    DisplayName = "John Doe",
    Id = Base64UrlConverter.FromBase64UrlString("TUY65dH-Otl4jMdTRvlFQ1aApACYsuqGKSPQDQc1Bd4WVyw")
};

var challenge = new byte[] { 0, 1, 2, 3 };
var api = new WebAuthnApi();

var response = api.AuthenticatorMakeCredential(
    rp,
    user,
    challenge,
    UserVerificationRequirement.Required,
    AuthenticatorAttachment.Any);
```

### Authentication (Assertion)

```csharp
using DSInternals.Win32.WebAuthn;

var api = new WebAuthnApi();
var challenge = new byte[] { 0, 1, 2, 3 };

var response = api.AuthenticatorGetAssertion(
    "login.microsoft.com",
    challenge,
    UserVerificationRequirement.Required,
    AuthenticatorAttachment.CrossPlatform);
```

Async variants (`AuthenticatorMakeCredentialAsync` and `AuthenticatorGetAssertionAsync`) are also available and support cancellation.

> The samples above are illustrative and not production-ready: they omit validation and use hardcoded values. In particular, the `challenge` must be generated with a cryptographically secure random number generator.

## Related Packages

- [DSInternals.Win32.WebAuthn.Adapter](https://www.nuget.org/packages/DSInternals.Win32.WebAuthn.Adapter/) — bridge that exposes this library through the [Fido2.Models](https://www.nuget.org/packages/Fido2.Models/) types from [fido2-net-lib](https://github.com/abergs/fido2-net-lib).
- [DSInternals.Passkeys](https://www.powershellgallery.com/packages/DSInternals.Passkeys) — PowerShell module for registering passkeys on behalf of Microsoft Entra ID and Okta users.

## Links

- [Project site and source code](https://github.com/MichaelGrafnetter/webauthn-interop)
- [API documentation](https://github.com/MichaelGrafnetter/webauthn-interop/blob/main/Documentation/API/DSInternals.Win32.WebAuthn.md)
- [Changelog](https://github.com/MichaelGrafnetter/webauthn-interop/blob/main/Documentation/CHANGELOG.md)
- [Report an issue](https://github.com/MichaelGrafnetter/webauthn-interop/issues)

## License

Released under the [MIT License](https://github.com/MichaelGrafnetter/webauthn-interop/blob/main/LICENSE).
