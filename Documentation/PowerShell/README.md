---
Module Name: DSInternals.Passkeys
Module Guid: 2561bb28-b9cd-4cf1-b35c-96ea07e13ca1
Help Version: 1.0
Locale: en-US
---

# DSInternals.Passkeys PowerShell Module

## Description

This PowerShell module allows administrative registration of passkeys (i.e. FIDO2 security keys and Microsoft Authenticator mobile app) in Microsoft Entra ID (formerly Azure Active Directory) as well as Okta.

## DSInternals.Passkeys Cmdlets

### [Register-Passkey](Register-Passkey.md)

Registers a new passkey in Microsoft Entra ID or Okta.

```powershell
Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
Register-Passkey -UserId 'AdeleV@contoso.com' -DisplayName 'YubiKey 5 Nano'
Disconnect-MgGraph
```

```powershell
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
Register-Passkey -UserId 00ub61wm1aqmawzRC5d7'
Disconnect-Okta
```

### [Get-PasskeyRegistrationOptions](Get-PasskeyRegistrationOptions.md)

Retrieves creation options required to generate and register a Microsoft Entra ID or Okta-compatible passkey.

### [New-Passkey](New-Passkey.md)

Creates a new Microsoft Entra ID or Okta-compatible passkey.
