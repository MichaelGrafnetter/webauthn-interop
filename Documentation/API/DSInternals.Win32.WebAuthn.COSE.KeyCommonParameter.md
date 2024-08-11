# <a id="DSInternals_Win32_WebAuthn_COSE_KeyCommonParameter"></a> Enum KeyCommonParameter

Namespace: [DSInternals.Win32.WebAuthn.COSE](DSInternals.Win32.WebAuthn.COSE.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

COSE Key Common Parameters https://www.iana.org/assignments/cose/cose.xhtml#key-common-parameters

```csharp
public enum KeyCommonParameter
```

## Fields

`Reserved = 0` 

This value is reserved



`KeyType = 1` 

Identification of the key type



`KeyId = 2` 

Key identification value - match to kid in message



`Alg = 3` 

Key usage restriction to this algorithm



`KeyOps = 4` 

Restrict set of permissible operations



`BaseIV = 5` 

Base IV to be XORed with Partial IVs



