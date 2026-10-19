# <a id="DSInternals_Win32_WebAuthn_AuthenticatorCredentialManagementState"></a> Enum AuthenticatorCredentialManagementState

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Describes the availability of credential management on an authenticator.

```csharp
public enum AuthenticatorCredentialManagementState
```

## Fields

`Unavailable = 0` 

Availability was not reported.



`Available = 1` 

Credential management is available.



`PinRequired = -2` 

PIN authentication is required to access credential management.



## Remarks

Windows management status combining availability and PIN-access information, not the CTAP2 credMgmt boolean.

