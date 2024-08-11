# <a id="DSInternals_Win32_WebAuthn_AuthenticatorTransport"></a> Enum AuthenticatorTransport

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Defines hints as to how clients might communicate with a particular authenticator in order to obtain an assertion for a specific credential.

```csharp
[Flags]
[JsonConverter(typeof(JsonCustomEnumConverter<AuthenticatorTransport>))]
public enum AuthenticatorTransport : uint
```

## Fields

`NoRestrictions = 0` 

No transport restrictions.



`USB = 1` 

Universal Serial Bus (USB).

Corresponds to WEBAUTHN_CTAP_TRANSPORT_USB.

`NFC = 2` 

Near Field Communication (NFC).

Corresponds to WEBAUTHN_CTAP_TRANSPORT_NFC.

`BLE = 4` 

Bluetooth Low Energy (BLE).

Corresponds to WEBAUTHN_CTAP_TRANSPORT_BLE.

`Hybrid = 32` 

Hybrid Transport (QR Code).

Corresponds to WEBAUTHN_CTAP_TRANSPORT_HYBRID.

`Test = 8` 



Corresponds to WEBAUTHN_CTAP_TRANSPORT_TEST.

`Internal = 16` 

Client device-specific transport.

Corresponds to WEBAUTHN_CTAP_TRANSPORT_INTERNAL.

