# <a id="DSInternals_Win32_WebAuthn_AuthenticatorPinState"></a> Enum AuthenticatorPinState

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Describes the authenticator PIN configuration status.

```csharp
public enum AuthenticatorPinState
```

## Fields

`Unavailable = 0` 

The state was not reported.



`Unset = -1` 

No PIN is configured.



`Set = 1` 

A PIN is configured.



`Blocked = 2` 

PIN use is blocked.



## Remarks

Windows management status combining PIN configuration and blocking information, not a CTAP2 option value.

