---
external help file: DSInternals.Passkeys.Core.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Close-PasskeyAuthenticatorSession.md
schema: 2.0.0
---

# Close-PasskeyAuthenticatorSession

## SYNOPSIS
Disposes an authenticator management session.

## SYNTAX

```
Close-PasskeyAuthenticatorSession [-Session] <AuthenticatorManagementSession> [<CommonParameters>]
```

## DESCRIPTION
Releases the selected authenticator session. Use `finally` to ensure the session is closed. Snapshot and credential data already returned by the session remains independent of the disposed native resources.

## EXAMPLES

### EXAMPLE 1
```powershell
Close-PasskeyAuthenticatorSession -Session $session
```

## PARAMETERS

### -Session
Session returned by `Open-PasskeyAuthenticatorSession`.

```yaml
Type: AuthenticatorManagementSession
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS
