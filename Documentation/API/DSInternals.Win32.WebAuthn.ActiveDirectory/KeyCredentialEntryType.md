# KeyCredentialEntryType enumeration

Key Credential Link Entry Identifier

```csharp
public enum KeyCredentialEntryType : byte
```

## Values

| name | value | description |
| --- | --- | --- |
| KeyID | `1` | A SHA256 hash of the Value field of the KeyMaterial entry. |
| KeyHash | `2` | A SHA256 hash of all entries following this entry. |
| KeyMaterial | `3` | Key material of the credential. |
| KeyUsage | `4` | Key Usage |
| KeySource | `5` | Key Source |
| DeviceId | `6` | Device Identifier |
| CustomKeyInformation | `7` | Custom key information. |
| KeyApproximateLastLogonTimeStamp | `8` | The approximate time this key was last used, in FILETIME format. |
| KeyCreationTime | `9` | The approximate time this key was created, in FILETIME format. |

## Remarks

Describes the data stored in the Value field.

## See Also

* namespace [DSInternals.Win32.WebAuthn.ActiveDirectory](../DSInternals.Win32.WebAuthn.md)
* [KeyCredentialEntryType.cs](https://github.com/MichaelGrafnetter/webauthn-interop/tree/master/Src/DSInternals.Win32.WebAuthn/ActiveDirectory/KeyCredentialEntryType.cs)

<!-- DO NOT EDIT: generated by xmldocmd for DSInternals.Win32.WebAuthn.dll -->