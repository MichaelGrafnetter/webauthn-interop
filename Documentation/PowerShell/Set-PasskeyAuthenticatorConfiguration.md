---
external help file: DSInternals.Passkeys.Core.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Set-PasskeyAuthenticatorConfiguration.md
schema: 2.0.0
---

# Set-PasskeyAuthenticatorConfiguration

## SYNOPSIS
Enables Always Require User Verification or enterprise attestation.

## SYNTAX

### AlwaysUv (Default)
```
Set-PasskeyAuthenticatorConfiguration [[-Session] <AuthenticatorManagementSession>] [-AlwaysUv] [-WhatIf]
 [-Confirm] [<CommonParameters>]
```

### EnterpriseAttestation
```
Set-PasskeyAuthenticatorConfiguration [[-Session] <AuthenticatorManagementSession>]
 [-EnableEnterpriseAttestation] [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Enables one of the supported authenticator configuration options. Enterprise attestation can expose identifying information. Authenticate explicitly beforehand if required. `-WhatIf` prevents dispatch. If the operation has an uncertain outcome, dispose and reopen the session before using a capability or policy snapshot to decide whether to retry; reads from the same session may return stale state.

## EXAMPLES

## PARAMETERS

### -AlwaysUv
Enable Always Require User Verification.

```yaml
Type: SwitchParameter
Parameter Sets: AlwaysUv
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnableEnterpriseAttestation
Enable enterprise attestation.

```yaml
Type: SwitchParameter
Parameter Sets: EnterpriseAttestation
Aliases:

Required: True
Position: Named
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
These high-impact configuration changes require confirmation by default and may be irreversible.

## RELATED LINKS
