---
external help file: DSInternals.Passkeys.Core.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Get-PasskeyAuthenticatorPlugin.md
schema: 2.0.0
---

# Get-PasskeyAuthenticatorPlugin

## SYNOPSIS
Gets the list of registered authenticator plugins from the Windows registry.

## SYNTAX

```
Get-PasskeyAuthenticatorPlugin [<CommonParameters>]
```

## DESCRIPTION
Retrieves information about third-party passkey providers (such as 1Password, Bitwarden, etc.) that are registered
as authenticator plugins in Windows.
These plugins are registered under HKLM\SOFTWARE\Microsoft\FIDO.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PasskeyAuthenticatorPlugin
```

Lists all registered authenticator plugins.

### EXAMPLE 2
```powershell
Get-PasskeyAuthenticatorPlugin | Where-Object Enabled -eq $true
```

Lists only enabled authenticator plugins.

### EXAMPLE 3
```powershell
Get-PasskeyAuthenticatorPlugin | Select-Object -Property Name,PackageFamilyName,Enabled
```

Lists authenticator plugins with selected properties.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### DSInternals.Win32.WebAuthn.AuthenticatorPluginInformation
## NOTES

## RELATED LINKS

[Get-PasskeyAuthenticator](Get-PasskeyAuthenticator.md)

