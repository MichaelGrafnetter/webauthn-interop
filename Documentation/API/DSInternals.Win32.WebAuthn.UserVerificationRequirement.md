# <a id="DSInternals_Win32_WebAuthn_UserVerificationRequirement"></a> Enum UserVerificationRequirement

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

This enumeration describes the Relying Party's requirements regarding user verification for the create() operation.
Eligible authenticators are filtered to only those capable of satisfying this requirement.

```csharp
[JsonConverter(typeof(JsonStringEnumConverter<UserVerificationRequirement>))]
public enum UserVerificationRequirement : uint
```

## Fields

`Any = 0` 



Corresponds to WEBAUTHN_USER_VERIFICATION_REQUIREMENT_ANY.

`Required = 1` 

This value indicates that the Relying Party requires user verification for the operation
and will fail the operation if the response does not have the UV flag set.

Corresponds to WEBAUTHN_USER_VERIFICATION_REQUIREMENT_REQUIRED.

`Preferred = 2` 

This value indicates that the Relying Party prefers user verification for the operation if possible,
but will not fail the operation if the response does not have the UV flag set.

Corresponds to WEBAUTHN_USER_VERIFICATION_REQUIREMENT_PREFERRED.

`Discouraged = 3` 

This value indicates that the Relying Party does not want user verification employed during the operation
(e.g., in the interest of minimizing disruption to the user interaction flow).

Corresponds to WEBAUTHN_USER_VERIFICATION_REQUIREMENT_DISCOURAGED.

