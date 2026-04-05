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

Registers a newly created passkey with Microsoft Entra ID or Okta.

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

Retrieves creation options required to generate and register a Microsoft Entra ID or Okta compatible passkey.

### [New-Passkey](New-Passkey.md)

Creates a new Microsoft Entra ID or Okta-compatible passkey.

### [Test-Passkey](Test-Passkey.md)

Tests a passkey by performing an authentication assertion.

### [Get-PasskeyWindowsHello](Get-PasskeyWindowsHello.md)

Gets the list of platform credentials (passkeys) stored on the system.

### [Remove-PasskeyWindowsHello](Remove-PasskeyWindowsHello.md)

Removes a platform credential (passkey) from the system.

### [Get-PasskeyAuthenticator](Get-PasskeyAuthenticator.md)

Gets the list of available authenticators from the WebAuthn API.

### [Get-PasskeyAuthenticatorPlugin](Get-PasskeyAuthenticatorPlugin.md)

Gets the list of registered authenticator plugins from the Windows registry.

### [Connect-Okta](Connect-Okta.md)

Retrieves an access token to interact with Okta APIs.

### [Disconnect-Okta](Disconnect-Okta.md)

Revokes Okta access token.

### [New-PasskeyRandomChallenge](New-PasskeyRandomChallenge.md)

Generates a random challenge to be used by WebAuthn.
