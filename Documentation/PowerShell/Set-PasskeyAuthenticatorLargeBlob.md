---
external help file: DSInternals.Passkeys.Core.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Set-PasskeyAuthenticatorLargeBlob.md
schema: 2.0.0
---

# Set-PasskeyAuthenticatorLargeBlob

## SYNOPSIS
Replaces the complete serialized large-blob store.

## SYNTAX

```
Set-PasskeyAuthenticatorLargeBlob [[-Session] <AuthenticatorManagementSession>] [-Data] <Byte[]> [-WhatIf]
 [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Replaces the full store; this is not an append or per-credential edit. The caller is responsible for store schema, integrity, and device capacity validation. Use `-WhatIf` to avoid dispatch. A reported error after dispatch does not necessarily mean the write did not occur. If the outcome is uncertain, dispose and reopen the session before deciding whether to retry; same-session capability and PIN-policy snapshots may be stale.

## EXAMPLES

## PARAMETERS

### -Data
Complete serialized store bytes.

```yaml
Type: Byte[]
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
This destructive operation uses high-impact confirmation by default. Authenticate explicitly beforehand if required.

## RELATED LINKS
