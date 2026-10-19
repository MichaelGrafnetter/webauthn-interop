---
external help file: DSInternals.Passkeys.Core.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Get-PasskeyAuthenticatorPinPolicy.md
schema: 2.0.0
---

# Get-PasskeyAuthenticatorPinPolicy

## SYNOPSIS
Gets PIN and configuration policy for the selected authenticator.

## SYNTAX

```
Get-PasskeyAuthenticatorPinPolicy [[-Session] <AuthenticatorManagementSession>] [<CommonParameters>]
```

## DESCRIPTION
Returns reported policy values, preserving unavailable values as null rather than inventing defaults. Values are copied from the session's last native context refresh; this cmdlet does not query the authenticator independently. After a mutation with an uncertain outcome, dispose and reopen the session to select the authenticator and obtain fresh state before deciding whether to retry. Calling this cmdlet again on the same session may return stale state.

## EXAMPLES

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

### DSInternals.Win32.WebAuthn.AuthenticatorPinPolicy
## NOTES

## RELATED LINKS
