---
Module Name: DSInternals.Passkeys
Module Guid: 2561bb28-b9cd-4cf1-b35c-96ea07e13ca1
Help Version: 1.0
Locale: en-US
---

# DSInternals.Passkeys PowerShell Module

## Description

Allows administrative registration of passkeys (i.e. FIDO2 security keys, Windows Hello, and Microsoft Authenticator mobile app) in Microsoft Entra ID (formerly Azure Active Directory) and Okta, and management of compatible platform and roaming authenticators.

The Core module also provides management of discoverable credentials, PIN policy, authenticator configuration, and the complete large-blob store on compatible roaming authenticators. Management uses unstable private Windows APIs. See [Authenticator Management](#authenticator-management) before using these commands.

## Microsoft Entra ID Cmdlets

Creation options are retrieved using the native [Invoke-MgCreationUserAuthenticationFido2MethodOption](https://learn.microsoft.com/en-us/powershell/module/microsoft.graph.identity.signins/invoke-mgcreationuserauthenticationfido2methodoption) cmdlet from the Microsoft.Graph.Identity.SignIns module, for which the `Get-EntraPasskeyRegistrationOptions` and `Get-PasskeyRegistrationOptions` aliases are provided.

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

### [Test-PasskeyWindowsHelloForBusiness](Test-PasskeyWindowsHelloForBusiness.md)

Tests a Windows Hello for Business (WHfB) NGC-based passkey registered for Microsoft Entra ID sign-in.

### [New-PasskeyRandomChallenge](New-PasskeyRandomChallenge.md)

Generates a random challenge to be used by WebAuthn.

### [Get-PasskeyWindowsHello](Get-PasskeyWindowsHello.md)

Gets the list of platform credentials (passkeys) stored on the system.

### [Remove-PasskeyWindowsHello](Remove-PasskeyWindowsHello.md)

Removes a platform credential (passkey) from the system.

### [Get-PasskeyPlatformAuthenticator](Get-PasskeyPlatformAuthenticator.md)

Gets the list of available authenticators from the WebAuthn API.

### [Get-PasskeyAuthenticatorPlugin](Get-PasskeyAuthenticatorPlugin.md)

Gets the list of registered authenticator plugins from the Windows registry.

## Authenticator Management

Management commands use a selected-device session. Create it explicitly, use `-Session` for every operation, and close it in a `finally` block. Opening a session selects the authenticator but does not authenticate or request a PIN. Use `Unlock-PasskeyAuthenticatorSession` explicitly with a caller-supplied `SecureString`; authentication and other sensitive writes support `-WhatIf` and high-impact confirmation.

These features depend on undocumented private Windows APIs which may change with Windows servicing.
Avoid management writes unless you understand their impact, especially credential deletion, PIN policy changes, enterprise attestation, and replacing the entire serialized large-blob store. A write may have succeeded even if a subsequent refresh or transport error is reported. Capability and PIN-policy cmdlets return snapshots copied from the session's last native context refresh; they do not independently query the authenticator. After a mutation with an uncertain outcome, dispose and reopen the session to select the authenticator and obtain fresh state before deciding whether to retry. Reading policy again from the same session may return stale state.

```powershell
$session = Open-PasskeyAuthenticatorSession
try {
    Get-PasskeyAuthenticatorCapabilities -Session $session
    Unlock-PasskeyAuthenticatorSession -Session $session -Pin (Read-Host 'Authenticator PIN' -AsSecureString)
    Get-PasskeySecurityKey -Session $session
}
finally {
    Close-PasskeyAuthenticatorSession -Session $session
}
```

### [Open-PasskeyAuthenticatorSession](Open-PasskeyAuthenticatorSession.md)

Selects an authenticator and opens a disposable management session.

### [Close-PasskeyAuthenticatorSession](Close-PasskeyAuthenticatorSession.md)

Disposes an authenticator management session.

### [Unlock-PasskeyAuthenticatorSession](Unlock-PasskeyAuthenticatorSession.md)

Authenticates to the selected authenticator using a PIN.

### [Get-PasskeySupportedTransport](Get-PasskeySupportedTransport.md)

Gets transports supported by authenticator management on this system.

### [Get-PasskeyAuthenticatorCapabilities](Get-PasskeyAuthenticatorCapabilities.md)

Gets a snapshot of authenticator management capabilities and state.

### [Get-PasskeyAuthenticatorPinPolicy](Get-PasskeyAuthenticatorPinPolicy.md)

Gets PIN and configuration policy for the selected authenticator.

### [Get-PasskeySecurityKey](Get-PasskeySecurityKey.md)

Lists discoverable credentials on the selected authenticator.

### [Remove-PasskeySecurityKey](Remove-PasskeySecurityKey.md)

Deletes a resident credential by its exact credential ID.

### [Set-PasskeyAuthenticatorPin](Set-PasskeyAuthenticatorPin.md)

Installs or changes the authenticator PIN.

### [Set-PasskeyAuthenticatorPinPolicy](Set-PasskeyAuthenticatorPinPolicy.md)

Configures minimum PIN length, relying-party policy, and a future PIN change.

### [Set-PasskeyAuthenticatorConfiguration](Set-PasskeyAuthenticatorConfiguration.md)

Enables Always Require User Verification or enterprise attestation.

### [Get-PasskeyAuthenticatorLargeBlob](Get-PasskeyAuthenticatorLargeBlob.md)

Reads the complete serialized large-blob store.

### [Set-PasskeyAuthenticatorLargeBlob](Set-PasskeyAuthenticatorLargeBlob.md)

Replaces the complete serialized large-blob store.
