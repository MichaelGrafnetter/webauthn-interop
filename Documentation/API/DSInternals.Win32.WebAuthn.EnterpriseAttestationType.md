# <a id="DSInternals_Win32_WebAuthn_EnterpriseAttestationType"></a> Enum EnterpriseAttestationType

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Enterprise Attestation Capabilities

```csharp
[Flags]
public enum EnterpriseAttestationType : uint
```

## Fields

`None = 0` 



Corresponds to WEBAUTHN_ENTERPRISE_ATTESTATION_NONE.

`VendorFacilitated = 1` 

Vendor-facilitated enterprise attestation

Corresponds to WEBAUTHN_ENTERPRISE_ATTESTATION_VENDOR_FACILITATED.

`PlatformManaged = 2` 

Platform-managed enterprise attestation

Corresponds to WEBAUTHN_ENTERPRISE_ATTESTATION_PLATFORM_MANAGED.

