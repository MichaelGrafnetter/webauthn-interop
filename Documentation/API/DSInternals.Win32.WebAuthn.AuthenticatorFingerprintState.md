# <a id="DSInternals_Win32_WebAuthn_AuthenticatorFingerprintState"></a> Enum AuthenticatorFingerprintState

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Describes the authenticator fingerprint enrollment status.

```csharp
public enum AuthenticatorFingerprintState
```

## Fields

`Unavailable = 0` 

Fingerprint enrollment information is unavailable.

This does not indicate whether the authenticator has a fingerprint sensor.

`Empty = -1` 

No fingerprints are enrolled.



`Enrolled = 1` 

Fingerprints are enrolled.



`PinRequired = -2` 

A PIN is required to access fingerprint information.



## Remarks

Windows management status combining enrollment and PIN-access information, not the CTAP2 bioEnroll boolean.

