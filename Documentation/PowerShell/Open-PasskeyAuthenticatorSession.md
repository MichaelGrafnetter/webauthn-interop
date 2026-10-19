---
external help file: DSInternals.Passkeys.Core.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Open-PasskeyAuthenticatorSession.md
schema: 2.0.0
---

# Open-PasskeyAuthenticatorSession

## SYNOPSIS
Selects an authenticator and opens a disposable management session.

## SYNTAX

```
Open-PasskeyAuthenticatorSession [[-Transports] <AuthenticatorTransport>] [<CommonParameters>]
```

## DESCRIPTION
Selects the authenticator once. Opening a session does not authenticate or prompt for a PIN. Management requires WebAuthnApi.IsAuthenticatorManagementSupported and uses private Windows APIs which may break after Windows servicing.

## EXAMPLES

### EXAMPLE 1
```powershell
$session = Open-PasskeyAuthenticatorSession
try { Get-PasskeyAuthenticatorCapabilities -Session $session } finally { Close-PasskeyAuthenticatorSession $session }
```

## PARAMETERS

### -Transports
Optional transport mask used during selection.

```yaml
Type: AuthenticatorTransport
Parameter Sets: (All)
Aliases:
Accepted values: NoRestrictions, USB, NFC, BLE, Test, Internal, Hybrid, SmartCard

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

### DSInternals.Win32.WebAuthn.AuthenticatorManagementSession
## NOTES

## RELATED LINKS

[Close-PasskeyAuthenticatorSession](Close-PasskeyAuthenticatorSession.md)
[Get-PasskeyAuthenticatorCapabilities](Get-PasskeyAuthenticatorCapabilities.md)
