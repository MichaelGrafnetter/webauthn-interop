# <a id="DSInternals_Win32_WebAuthn_FIDO_UserVerificationMethod"></a> Enum UserVerificationMethod

Namespace: [DSInternals.Win32.WebAuthn.FIDO](DSInternals.Win32.WebAuthn.FIDO.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

User verification methods reported by the authenticator in the WebAuthn uvm extension.

```csharp
[Flags]
public enum UserVerificationMethod : uint
```

## Fields

`None = 0` 

No user verification method is reported.



`Presence = 1` 

Test of user presence (USER_VERIFY_PRESENCE).



`Fingerprint = 2` 

Fingerprint check (USER_VERIFY_FINGERPRINT).



`Passcode = 4` 

PIN or passcode (USER_VERIFY_PASSCODE).



`Voiceprint = 8` 

Voiceprint (USER_VERIFY_VOICEPRINT).



`Faceprint = 16` 

Faceprint (USER_VERIFY_FACEPRINT).



`Location = 32` 

Geolocation (USER_VERIFY_LOCATION).



`Eyeprint = 64` 

Eyeprint / iris (USER_VERIFY_EYEPRINT).



`Pattern = 128` 

Pattern (USER_VERIFY_PATTERN).



`Handprint = 256` 

Handprint (USER_VERIFY_HANDPRINT).



`NoneVerified = 512` 

Silent / no verification performed (USER_VERIFY_NONE).



`All = 1024` 

All registered user verification methods are required (USER_VERIFY_ALL).



