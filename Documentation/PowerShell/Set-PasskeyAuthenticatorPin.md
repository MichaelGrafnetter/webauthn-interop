---
external help file: DSInternals.Passkeys.Core.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Set-PasskeyAuthenticatorPin.md
schema: 2.0.0
---

# Set-PasskeyAuthenticatorPin

## SYNOPSIS
Installs or changes the authenticator PIN.

## SYNTAX

### Initial (Default)
```
Set-PasskeyAuthenticatorPin [[-Session] <AuthenticatorManagementSession>] [-NewPin] <SecureString> [-WhatIf]
 [-Confirm] [<CommonParameters>]
```

### Change
```
Set-PasskeyAuthenticatorPin [[-Session] <AuthenticatorManagementSession>] [-NewPin] <SecureString>
 [-CurrentPin] <SecureString> [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Use the initial parameter set to install a PIN, or the change parameter set to replace an existing PIN. PINs are supplied explicitly as SecureString values. The cmdlet does not prompt, cache, or authenticate implicitly. `-WhatIf` prevents PIN submission. If the mutation outcome is uncertain, dispose and reopen the session before deciding whether to retry; same-session capability and PIN-policy snapshots may be stale.

## EXAMPLES

## PARAMETERS

### -CurrentPin
Existing PIN as a SecureString; required to change a PIN.

```yaml
Type: SecureString
Parameter Sets: Change
Aliases:

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -NewPin
New PIN as a SecureString.

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
High-impact confirmation is enabled by default.

## RELATED LINKS
