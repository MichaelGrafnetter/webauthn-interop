# FidoKeyMaterial class

```csharp
public class FidoKeyMaterial
```

## Public Members

| name | description |
| --- | --- |
| [FidoKeyMaterial](FidoKeyMaterial/FidoKeyMaterial.md)() | The default constructor. |
| [AttestationCertificates](FidoKeyMaterial/AttestationCertificates.md) { get; } | Attestation certificates can be helpful for establishing a chain of trust. |
| [AttestationCertificatesRaw](FidoKeyMaterial/AttestationCertificatesRaw.md) { get; } | X5c is an array of attestation certificates associated with the authenticator. |
| [AuthenticatorData](FidoKeyMaterial/AuthenticatorData.md) { get; } | Authenticator data contains information about the registered authenticator device. |
| [AuthenticatorDataRaw](FidoKeyMaterial/AuthenticatorDataRaw.md) { get; } | AuthData is a WebAuthn authenticator data structure. https://www.w3.org/TR/webauthn/#sec-authenticator-data |
| [DisplayName](FidoKeyMaterial/DisplayName.md) { get; } | Display name is a user provided string which can help the user differentiate between multiple registered authenticators. |
| [Version](FidoKeyMaterial/Version.md) { get; } | Version is an integer that specifies the version of the structure. |

## See Also

* namespace [DSInternals.Win32.WebAuthn.ActiveDirectory](../DSInternals.Win32.WebAuthn.md)
* [FidoKeyMaterial.cs](https://github.com/MichaelGrafnetter/webauthn-interop/tree/master/Src/DSInternals.Win32.WebAuthn/ActiveDirectory/FidoKeyMaterial.cs)

<!-- DO NOT EDIT: generated by xmldocmd for DSInternals.Win32.WebAuthn.dll -->