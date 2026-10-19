---
external help file: DSInternals.Passkeys.Core.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Set-PasskeyAuthenticatorPinPolicy.md
schema: 2.0.0
---

# Set-PasskeyAuthenticatorPinPolicy

## SYNOPSIS
Configures minimum PIN length, relying-party policy, and a future PIN change.

## SYNTAX

```
Set-PasskeyAuthenticatorPinPolicy [[-Session] <AuthenticatorManagementSession>] [-MinimumPinLength <UInt32>]
 [-RelyingPartyId <String[]>] [-ForcePinChange] [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Changes authenticator policy. Use `-ForcePinChange` to require a PIN change at the next authentication. Authenticate explicitly beforehand when required. `-WhatIf` prevents dispatch. If the operation has an uncertain outcome, dispose and reopen the session before using a policy snapshot to decide whether to retry; reading policy from the same session may return stale state.

## EXAMPLES

## PARAMETERS

### -ForcePinChange
Require a PIN change at the next authentication.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MinimumPinLength
Requested minimum PIN length from 4 to 63 Unicode code points. Omit it to preserve the current minimum.

```yaml
Type: UInt32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RelyingPartyId
Optional relying-party IDs governed by this minimum-length policy.

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:

Required: False
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
High-impact confirmation is enabled by default.

## RELATED LINKS
