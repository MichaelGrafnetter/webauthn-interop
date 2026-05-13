# <a id="DSInternals_Win32_WebAuthn_FIDO_MatcherProtectionType"></a> Enum MatcherProtectionType

Namespace: [DSInternals.Win32.WebAuthn.FIDO](DSInternals.Win32.WebAuthn.FIDO.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Methods used by the authenticator to protect the matcher that performs user verification, as reported in the WebAuthn uvm extension.

```csharp
[Flags]
public enum MatcherProtectionType : ushort
```

## Fields

`None = 0` 

No matcher protection type is reported.



`Software = 1` 

Software-based matcher protection (MATCHER_PROTECTION_SOFTWARE).



`TEE = 2` 

Trusted Execution Environment (MATCHER_PROTECTION_TEE).



`OnChip = 4` 

On-chip matcher protection (MATCHER_PROTECTION_ON_CHIP).



