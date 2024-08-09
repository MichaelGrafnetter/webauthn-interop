---
Module Name: DSInternals.Passkeys
Module Guid: 2561bb28-b9cd-4cf1-b35c-96ea07e13ca1
Help Version: 1.0
Locale: en-US
---

# DSInternals.Passkeys PowerShell Module

## Description

This PowerShell module allows administrative registration of passkeys (i.e. FIDO2 security keys and Microsoft Authenticator mobile app) in Microsoft Entra ID (formerly Azure Active Directory).

## DSInternals.Passkeys Cmdlets

### [Register-Passkey](Register-Passkey.md)

Registers a new passkey in Microsoft Entra ID.

```powershell
Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
Register-Passkey -UserId 'AdeleV@contoso.com' -DisplayName 'YubiKey 5 Nano'
Disconnect-MgGraph
```

### [Get-PasskeyRegistrationOptions](Get-PasskeyRegistrationOptions.md)

Retrieves creation options required to generate and register a Microsoft Entra ID-compatible passkey.

### [New-Passkey](New-Passkey.md)

Creates a new Microsoft Entra ID-compatible passkey.
