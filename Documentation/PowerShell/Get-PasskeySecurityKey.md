---
external help file: DSInternals.Passkeys.Core.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Get-PasskeySecurityKey.md
schema: 2.0.0
---

# Get-PasskeySecurityKey

## SYNOPSIS
Lists discoverable credentials on the selected authenticator.

## SYNTAX

```
Get-PasskeySecurityKey [[-Session] <AuthenticatorManagementSession>] [<CommonParameters>]
```

## DESCRIPTION
Returns read-only credential records with the raw credential ID, relying-party ID, user name, and display name. Authentication is never performed implicitly.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PasskeySecurityKey -Session $session
```

## PARAMETERS

### -Session
An existing authenticator session. If omitted, a session is opened automatically and closed after the command.

```yaml
Type: AuthenticatorManagementSession
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### DSInternals.Win32.WebAuthn.DiscoverableCredentialDetails
## NOTES

## RELATED LINKS

[Get-PasskeyWindowsHello](Get-PasskeyWindowsHello.md)

[Remove-PasskeySecurityKey](Remove-PasskeySecurityKey.md)
