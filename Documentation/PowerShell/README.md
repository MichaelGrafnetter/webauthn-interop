---
Module Name: DSInternals.Passkeys
Module Guid: 2561bb28-b9cd-4cf1-b35c-96ea07e13ca1
Help Version: 1.0
Locale: en-US
---

# DSInternals.Passkeys PowerShell Module

## Description

Allows administrative registration of passkeys (i.e. FIDO2 security keys and Microsoft Authenticator mobile app) in Microsoft Entra ID (formerly Azure Active Directory) and Okta.

## Microsoft Entra ID Cmdlets

### [Get-EntraPasskeyRegistrationOptions](Get-EntraPasskeyRegistrationOptions.md)

Retrieves creation options required to generate and register a Microsoft Entra ID compatible passkey.

### [Register-EntraPasskey](Register-EntraPasskey.md)

Registers a new passkey in Microsoft Entra ID.

## Okta Cmdlets

### [Connect-Okta](Connect-Okta.md)

Retrieves an access token to interact with Okta APIs. See [Okta Setup](../Okta.md) for tenant and application configuration prerequisites.

### [Disconnect-Okta](Disconnect-Okta.md)

Revokes Okta access token.

### [Get-OktaPasskeyRegistrationOptions](Get-OktaPasskeyRegistrationOptions.md)

Retrieves creation options required to generate and register an Okta compatible passkey.

### [Register-OktaPasskey](Register-OktaPasskey.md)

Registers a new passkey in Okta.

## Core Cmdlets

### [Get-PasskeyCreationOptions](Get-PasskeyCreationOptions.md)

Builds a PublicKeyCredentialCreationOptions object for use with New-Passkey.

### [New-Passkey](New-Passkey.md)

Creates a new WebAuthn credential by driving the local authenticator.

### [Test-Passkey](Test-Passkey.md)

Tests a passkey by performing an authentication assertion.

### [New-PasskeyRandomChallenge](New-PasskeyRandomChallenge.md)

Generates a random challenge to be used by WebAuthn.

### [Get-PasskeyWindowsHello](Get-PasskeyWindowsHello.md)

Gets the list of platform credentials (passkeys) stored on the system.

### [Remove-PasskeyWindowsHello](Remove-PasskeyWindowsHello.md)

Removes a platform credential (passkey) from the system.

### [Get-PasskeyAuthenticator](Get-PasskeyAuthenticator.md)

Gets the list of available authenticators from the WebAuthn API.

### [Get-PasskeyAuthenticatorPlugin](Get-PasskeyAuthenticatorPlugin.md)

Gets the list of registered authenticator plugins from the Windows registry.
