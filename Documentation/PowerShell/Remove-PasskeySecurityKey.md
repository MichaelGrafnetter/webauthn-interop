---
external help file: DSInternals.Passkeys.Core.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Remove-PasskeySecurityKey.md
schema: 2.0.0
---

# Remove-PasskeySecurityKey

## SYNOPSIS
Deletes a resident credential by its exact credential ID.

## SYNTAX

### ByCredentialId (Default)
```
Remove-PasskeySecurityKey [[-Session] <AuthenticatorManagementSession>] [-CredentialId] <Object> [-WhatIf]
 [-Confirm] [<CommonParameters>]
```

### ByCredential
```
Remove-PasskeySecurityKey [[-Session] <AuthenticatorManagementSession>]
 -InputObject <DiscoverableCredentialDetails> [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Accepts an exact byte-array or Base64Url credential ID, or a credential object from the pipeline. Deletion is destructive. `-WhatIf` prevents dispatch to the authenticator. If the deletion outcome is uncertain, dispose and reopen the session before deciding whether to retry; capability and PIN-policy snapshots from the same session may be stale.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PasskeySecurityKey -Session $session | Where-Object RelyingPartyId -eq 'example.com' | Remove-PasskeySecurityKey -Session $session
```

## PARAMETERS

### -CredentialId
Credential ID as bytes or Base64Url text.

```yaml
Type: Object
Parameter Sets: ByCredentialId
Aliases:

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputObject
Credential object to remove.

```yaml
Type: DiscoverableCredentialDetails
Parameter Sets: ByCredential
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
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

[Remove-PasskeyWindowsHello](Remove-PasskeyWindowsHello.md)

[Get-PasskeySecurityKey](Get-PasskeySecurityKey.md)
