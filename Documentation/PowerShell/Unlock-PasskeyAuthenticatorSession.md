---
external help file: DSInternals.Passkeys.Core.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Unlock-PasskeyAuthenticatorSession.md
schema: 2.0.0
---

# Unlock-PasskeyAuthenticatorSession

## SYNOPSIS
Authenticates to the selected authenticator using a PIN.

## SYNTAX

```
Unlock-PasskeyAuthenticatorSession [[-Session] <AuthenticatorManagementSession>] [-Pin] <SecureString>
 [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Authentication is explicit; other commands never authenticate implicitly. Failed authentication can consume a PIN retry. The PIN must be supplied as `SecureString`; this command never prompts or caches the PIN. `-WhatIf` does not submit it.

## EXAMPLES

### EXAMPLE 1
```powershell
Unlock-PasskeyAuthenticatorSession -Session $session -Pin (Read-Host 'PIN' -AsSecureString)
```

## PARAMETERS

### -Pin
Caller-supplied PIN as a SecureString.

```yaml
Type: SecureString
Parameter Sets: (All)
Aliases:

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES
This is a high-impact operation and prompts for confirmation by default.

## RELATED LINKS
