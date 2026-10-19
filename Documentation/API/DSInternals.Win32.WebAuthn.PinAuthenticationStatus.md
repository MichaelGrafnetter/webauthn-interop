# <a id="DSInternals_Win32_WebAuthn_PinAuthenticationStatus"></a> Enum PinAuthenticationStatus

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Describes the PIN authentication or policy condition reported by an authenticator management operation.

```csharp
[SuppressMessage("Design", "CA1028:Enum Storage should be Int32", Justification = "Matches the native DWORD PIN diagnostic.")]
public enum PinAuthenticationStatus : uint
```

## Fields

`Unspecified = 0` 

No diagnostic was supplied.



`PinRequired = 1` 

A PIN is requested while attempts remain.



`PinNotSet = 2` 

No PIN is configured.



`PinVerificationFailed = 3` 

PIN verification failed.

The provider may also report this status for related user-verification failures.

`PinPolicyViolation = 4` 

The PIN violates policy.



`PinBlocked = 5` 

PIN use is blocked.



`PowerCycleRequired = 6` 

A power cycle is required.



## Remarks

These Windows management diagnostics supplement the operation result and remaining PIN retry count; they are not CTAP2 status codes.

