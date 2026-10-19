# <a id="DSInternals_Win32_WebAuthn_FIDO_AuthenticatorFingerprintKind"></a> Enum AuthenticatorFingerprintKind

Namespace: [DSInternals.Win32.WebAuthn.FIDO](DSInternals.Win32.WebAuthn.FIDO.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Identifies how an authenticator's fingerprint sensor captures a fingerprint.

```csharp
[SuppressMessage("Design", "CA1028:Enum Storage should be Int32", Justification = "Matches the native DWORD sensor kind.")]
public enum AuthenticatorFingerprintKind : uint
```

## Fields

`Unavailable = 0` 

No kind was reported.

A Windows management sentinel, not a CTAP2 fingerprintKind value.

`Touch = 1` 

The fingerprint sensor captures a stationary finger.



`Swipe = 2` 

The fingerprint sensor captures a swiping finger.



## Remarks

The nonzero values correspond to fingerprintKind in the authenticatorBioEnrollment response.

## See Also

[https://fidoalliance.org/specs/fido\-v2.2\-rd\-20230321/fido\-client\-to\-authenticator\-protocol\-v2.2\-rd\-20230321.html\#getFingerprintSensorInfo](https://fidoalliance.org/specs/fido\-v2.2\-rd\-20230321/fido\-client\-to\-authenticator\-protocol\-v2.2\-rd\-20230321.html\#getFingerprintSensorInfo)

