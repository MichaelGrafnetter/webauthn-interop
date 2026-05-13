# <a id="DSInternals_Win32_WebAuthn_AttestationStatementFormatIdentifier"></a> Enum AttestationStatementFormatIdentifier

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

WebAuthn attestation statement format identifiers registered with IANA.

```csharp
[JsonConverter(typeof(WebAuthnJsonEnumConverter<AttestationStatementFormatIdentifier>))]
public enum AttestationStatementFormatIdentifier : uint
```

## Fields

`Unknown = 0` 

No attestation statement format identifier was specified, or the identifier is not recognized.



`Packed = 1` 

WebAuthn-optimized packed attestation statement format.



`TPM = 2` 

Trusted Platform Module (TPM) attestation statement format.



`AndroidKey = 3` 

Android Key attestation statement format.



`AndroidSafetyNet = 4` 

Android SafetyNet attestation statement format.



`FIDOU2F = 5` 

FIDO U2F attestation statement format.



`Apple = 6` 

Apple anonymous attestation statement format.



`None = 7` 

None attestation statement format.



