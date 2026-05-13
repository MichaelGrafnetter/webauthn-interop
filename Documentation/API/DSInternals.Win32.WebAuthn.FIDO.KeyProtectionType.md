# <a id="DSInternals_Win32_WebAuthn_FIDO_KeyProtectionType"></a> Enum KeyProtectionType

Namespace: [DSInternals.Win32.WebAuthn.FIDO](DSInternals.Win32.WebAuthn.FIDO.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Methods used by the authenticator to protect FIDO registration private key material, as reported in the WebAuthn uvm extension.

```csharp
[Flags]
public enum KeyProtectionType : ushort
```

## Fields

`None = 0` 

No key protection type is reported.



`Software = 1` 

Software-based key protection (KEY_PROTECTION_SOFTWARE).



`Hardware = 2` 

Hardware-based key protection (KEY_PROTECTION_HARDWARE).



`TEE = 4` 

Trusted Execution Environment (KEY_PROTECTION_TEE).



`SecureElement = 8` 

Secure Element (KEY_PROTECTION_SECURE_ELEMENT).



`RemoteHandle = 16` 

Remote handle key protection (KEY_PROTECTION_REMOTE_HANDLE).



