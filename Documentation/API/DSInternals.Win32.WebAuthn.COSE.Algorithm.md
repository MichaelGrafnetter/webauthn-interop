# <a id="DSInternals_Win32_WebAuthn_COSE_Algorithm"></a> Enum Algorithm

Namespace: [DSInternals.Win32.WebAuthn.COSE](DSInternals.Win32.WebAuthn.COSE.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Well-known COSE algorithm specifying the algorithm to use for the credential. https://www.iana.org/assignments/cose/cose.xhtml#algorithms

```csharp
public enum Algorithm
```

## Fields

`ES256 = -7` 

ECDSA with SHA-256

Corresponds to WEBAUTHN_COSE_ALGORITHM_ECDSA_P256_WITH_SHA256.

`ES384 = -35` 

ECDSA with SHA-384

Corresponds to WEBAUTHN_COSE_ALGORITHM_ECDSA_P384_WITH_SHA384.

`ES512 = -36` 

ECDSA with SHA-512

Corresponds to WEBAUTHN_COSE_ALGORITHM_ECDSA_P521_WITH_SHA512.

`RS256 = -257` 

RSASSA-PKCS1-v1_5 with SHA-256

Corresponds to WEBAUTHN_COSE_ALGORITHM_RSASSA_PKCS1_V1_5_WITH_SHA256.

`RS384 = -258` 

RSASSA-PKCS1-v1_5 with SHA-384

Corresponds to WEBAUTHN_COSE_ALGORITHM_RSASSA_PKCS1_V1_5_WITH_SHA384.

`RS512 = -259` 

RSASSA-PKCS1-v1_5 with SHA-512

Corresponds to WEBAUTHN_COSE_ALGORITHM_RSASSA_PKCS1_V1_5_WITH_SHA512.

`PS256 = -37` 

RSASSA-PSS with SHA-256

Corresponds to WEBAUTHN_COSE_ALGORITHM_RSA_PSS_WITH_SHA256.

`PS384 = -38` 

RSASSA-PSS with SHA-384

Corresponds to WEBAUTHN_COSE_ALGORITHM_RSA_PSS_WITH_SHA384.

`PS512 = -39` 

RSASSA-PSS with SHA-512

Corresponds to WEBAUTHN_COSE_ALGORITHM_RSA_PSS_WITH_SHA512.

`RS1 = -65535` 

RSASSA-PKCS1-v1_5 with SHA-1



`EdDSA = -8` 

EdDSA



