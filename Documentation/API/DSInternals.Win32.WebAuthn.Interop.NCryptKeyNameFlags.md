# <a id="DSInternals_Win32_WebAuthn_Interop_NCryptKeyNameFlags"></a> Enum NCryptKeyNameFlags

Namespace: [DSInternals.Win32.WebAuthn.Interop](DSInternals.Win32.WebAuthn.Interop.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Flags describing a key, as reported by <code>NCryptEnumKeys</code>.

```csharp
[Flags]
public enum NCryptKeyNameFlags : uint
```

## Fields

`None = 0` 

No flags.



`MachineKey = 32` 

The key is stored as a machine key.

Equivalent to <code>NCRYPT_MACHINE_KEY_FLAG</code> in <code>ncrypt.h</code>.

