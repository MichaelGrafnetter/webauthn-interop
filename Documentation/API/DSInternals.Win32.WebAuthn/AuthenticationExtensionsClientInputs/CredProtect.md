# AuthenticationExtensionsClientInputs.CredProtect property

This extension indicates that the authenticator supports enhanced protection mode for the credentials created on the authenticator. If present, verify that the credentialProtectionPolicy value is one of following values: userVerificationOptional, userVerificationOptionalWithCredentialIDList, userVerificationRequired

```csharp
public UserVerification? CredProtect { get; set; }
```

## See Also

* enum [UserVerification](../UserVerification.md)
* class [AuthenticationExtensionsClientInputs](../AuthenticationExtensionsClientInputs.md)
* namespace [DSInternals.Win32.WebAuthn](../../DSInternals.Win32.WebAuthn.md)

<!-- DO NOT EDIT: generated by xmldocmd for DSInternals.Win32.WebAuthn.dll -->