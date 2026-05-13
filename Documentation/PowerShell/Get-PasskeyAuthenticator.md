---
external help file: DSInternals.Passkeys.Core.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Get-PasskeyAuthenticator.md
schema: 2.0.0
---

# Get-PasskeyAuthenticator

## SYNOPSIS
Gets the list of available authenticators from the WebAuthn API.

## SYNTAX

```
Get-PasskeyAuthenticator [<CommonParameters>]
```

## DESCRIPTION
Retrieves a list of authenticators available on the system using the Windows WebAuthn API.
This includes information about authenticator IDs, names, logos, and lock status.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PasskeyAuthenticator
```

Lists all available authenticators.

### EXAMPLE 2
```powershell
Get-PasskeyAuthenticator | Where-Object { -not $PSItem.Locked }
```

Lists only unlocked authenticators.

### EXAMPLE 3
```powershell
Get-PasskeyAuthenticator | Format-Table -Prperty AuthenticatorName,Locked
```

Lists authenticator names and lock status in a table.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### DSInternals.Win32.WebAuthn.AuthenticatorDetails
## NOTES

## RELATED LINKS

[Get-PasskeyAuthenticatorPlugin](Get-PasskeyAuthenticatorPlugin.md)

[Get-PasskeyWindowsHello](Get-PasskeyWindowsHello.md)

