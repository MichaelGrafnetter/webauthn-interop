# <a id="DSInternals_Win32_WebAuthn_AuthenticatorFlags"></a> Enum AuthenticatorFlags

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Authenticator data flags

```csharp
[Flags]
[SuppressMessage("Naming", "CA1711:Identifiers should not have incorrect suffix", Justification = "AuthenticatorFlags is the standard WebAuthn term.")]
[SuppressMessage("Design", "CA1028:Enum Storage should be Int32", Justification = "The flags are defined as a byte in the WebAuthn specification.")]
public enum AuthenticatorFlags : byte
```

## Fields

`UserPresent = 1` 

User Present (UP) - Bit 0.



`UserVerified = 4` 

User Verified (UV) - Bit 2.



`BackupEligible = 8` 

Backup Eligibility (BE) - Bit 3.



`BackedUp = 16` 

Backup State (BS) - Bit 4.



`AttestationData = 64` 

Attested Credential Data (AT) - Bit 6.



`ExtensionData = 128` 

Extension Data (ED) - Bit 7.



