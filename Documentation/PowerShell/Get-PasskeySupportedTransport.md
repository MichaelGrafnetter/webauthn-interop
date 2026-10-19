---
external help file: DSInternals.Passkeys.Core.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Get-PasskeySupportedTransport.md
schema: 2.0.0
---

# Get-PasskeySupportedTransport

## SYNOPSIS
Gets transports supported by authenticator management on this system.

## SYNTAX

```
Get-PasskeySupportedTransport [<CommonParameters>]
```

## DESCRIPTION
Queries supported management transports without selecting or modifying an authenticator. Requires WebAuthnApi.IsAuthenticatorManagementSupported. These private APIs may change between Windows releases.

## EXAMPLES

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### DSInternals.Win32.WebAuthn.AuthenticatorTransport
## NOTES

## RELATED LINKS
