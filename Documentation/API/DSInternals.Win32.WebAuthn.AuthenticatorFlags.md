# <a id="DSInternals_Win32_WebAuthn_AuthenticatorFlags"></a> Enum AuthenticatorFlags

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Authenticator data flags

```csharp
[Flags]
public enum AuthenticatorFlags : byte
```

## Fields

`UserPresent = 1` 

User Present indicates that the user presence test has completed successfully.



`RFU1 = 2` 

Reserved for future use (RFU1)



`UserVerified = 4` 

User Verified indicates that the user verification process has completed successfully.



`RFU2 = 8` 

Reserved for future use (RFU2)



`RFU3 = 16` 

Reserved for future use (RFU3)



`RFU4 = 32` 

Reserved for future use (RFU4)



`AttestationData = 64` 

Attested credential data included indicates that the authenticator added attested credential data to the authenticator data.



`ExtensionData = 128` 

Extension data included indicates that the authenticator added extension data to the authenticator data.



