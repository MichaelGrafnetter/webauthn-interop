---
external help file: DSInternals.Passkeys.Core.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Remove-PasskeyWindowsHello.md
schema: 2.0.0
---

# Remove-PasskeyWindowsHello

## SYNOPSIS
Removes a platform credential (passkey) from the system.

## SYNTAX

### ByCredentialId (Default)
```
Remove-PasskeyWindowsHello [-CredentialId] <Object> [-WhatIf] [-Confirm] [<CommonParameters>]
```

### ByCredential
```
Remove-PasskeyWindowsHello -InputObject <CredentialDetails> [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Removes a Public Key Credential stored on a platform authenticator (such as Windows Hello).
This operation is irreversible - once deleted, the credential cannot be recovered.

## EXAMPLES

### EXAMPLE 1
```powershell
$cred = Get-PasskeyWindowsHello | Select-Object -First 1
Remove-PasskeyWindowsHello -CredentialId $cred.CredentialId
```

Removes a specific platform credential by ID.

### EXAMPLE 2
```powershell
Get-PasskeyWindowsHello | Where-Object { $PSItem.RelyingPartyInformation.Id -eq 'example.com' } | Remove-PasskeyWindowsHello
```

Removes all credentials for a specific relying party using pipeline input.

### EXAMPLE 3
```powershell
Remove-PasskeyWindowsHello -CredentialId 'dGVzdC1jcmVkZW50aWFsLWlk'
```

Removes a credential using a Base64Url encoded credential ID.

## PARAMETERS

### -CredentialId
The ID of the credential to be removed.
This can be obtained from Get-PasskeyWindowsHello.
Accepts either a byte array or a Base64Url encoded string.

```yaml
Type: Object
Parameter Sets: ByCredentialId
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputObject
{{ Fill InputObject Description }}

```yaml
Type: CredentialDetails
Parameter Sets: ByCredential
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
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
Shows what would happen if the cmdlet runs.
The cmdlet is not run.

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
Requires Windows with WebAuthn API version 4 or later (Windows 10 2004+).
This operation requires appropriate permissions and may trigger a Windows Security prompt.

## RELATED LINKS

[Get-PasskeyWindowsHello](Get-PasskeyWindowsHello.md)

