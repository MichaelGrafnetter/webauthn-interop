---
external help file: DSInternals.Passkeys.Core.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/New-Passkey.md
schema: 2.0.0
---

# New-Passkey

## SYNOPSIS
Creates a new WebAuthn credential by driving the local authenticator.

## SYNTAX

### Default (Default)
```
New-Passkey -Options <PublicKeyCredentialCreationOptions> [-HostName <String>] [<CommonParameters>]
```

### Okta
```
New-Passkey -OktaOptions <OktaWebauthnCredentialCreationOptions> [<CommonParameters>]
```

## DESCRIPTION
Calls the Windows WebAuthn API to make a credential according to the provided PublicKeyCredentialCreationOptions, triggering the system passkey UI.
Returns the resulting AttestationPublicKeyCredential, which can be wrapped into a provider-specific attestation response and submitted to Microsoft Entra ID, Okta, or any other relying party.

## EXAMPLES

### EXAMPLE 1
```powershell
$credential = Get-EntraPasskeyRegistrationOptions -UserId 'AdeleV@contoso.com' | New-Passkey
```

Retrieves creation options from Microsoft Entra ID and creates a new passkey on the local authenticator without registering it.

### EXAMPLE 2
```powershell
Get-EntraPasskeyRegistrationOptions -UserId 'AdeleV@contoso.com' | New-Passkey | Register-EntraPasskey -UserId 'AdeleV@contoso.com' -DisplayName 'YubiKey 5 Nano'
```

Performs end-to-end passkey registration in Microsoft Entra ID in a single pipeline.

### EXAMPLE 3
```powershell
Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 | New-Passkey | Register-OktaPasskey
```

Performs end-to-end passkey registration in Okta in a single pipeline.

## PARAMETERS

### -HostName
Optional host name used to derive the WebAuthn origin when the server-issued options omit the relying party identifier.

```yaml
Type: String
Parameter Sets: Default
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OktaOptions
The Okta-specific credential creation options.
Returned by Get-OktaPasskeyRegistrationOptions.

```yaml
Type: OktaWebauthnCredentialCreationOptions
Parameter Sets: Okta
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Options
The WebAuthn public key credential creation options.
Returned directly by Get-EntraPasskeyRegistrationOptions.

```yaml
Type: PublicKeyCredentialCreationOptions
Parameter Sets: Default
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential
### DSInternals.Win32.WebAuthn.Okta.OktaWebauthnAttestationResponse
## NOTES

## RELATED LINKS

[Get-PasskeyCreationOptions](Get-PasskeyCreationOptions.md)

[Get-EntraPasskeyRegistrationOptions](Get-EntraPasskeyRegistrationOptions.md)

[Get-OktaPasskeyRegistrationOptions](Get-OktaPasskeyRegistrationOptions.md)

[Register-EntraPasskey](Register-EntraPasskey.md)

[Register-OktaPasskey](Register-OktaPasskey.md)

[Test-Passkey](Test-Passkey.md)

