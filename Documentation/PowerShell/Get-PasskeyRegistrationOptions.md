---
external help file: DSInternals.Passkeys-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Get-PasskeyRegistrationOptions.md
schema: 2.0.0
---

# Get-PasskeyRegistrationOptions

## SYNOPSIS
Retrieves creation options required to generate and register a Microsoft Entra ID-compatible passkey.

## SYNTAX

```
Get-PasskeyRegistrationOptions [-UserId] <String> [[-ChallengeTimeout] <TimeSpan>]
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

## DESCRIPTION
{{ Fill in the Description }}

## EXAMPLES

### EXAMPLE 1
```powershell
Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
Get-PasskeyRegistrationOptions -UserId 'AdeleV@contoso.com'
```

### EXAMPLE 2
```powershell
Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
Get-PasskeyRegistrationOptions -UserId 'AdeleV@contoso.com' -ChallengeTimeout (New-TimeSpan -Minutes 10)
```

## PARAMETERS

### -ChallengeTimeout
Overrides the timeout of the server-generated challenge returned in the request.
The default value is 5 minutes, with the accepted range being between 5 minutes and 30 days.

```yaml
Type: TimeSpan
Parameter Sets: (All)
Aliases: Timeout

Required: False
Position: 2
Default value: (New-TimeSpan -Minutes 5)
Accept pipeline input: False
Accept wildcard characters: False
```

### -ProgressAction
{{ Fill ProgressAction Description }}

```yaml
Type: ActionPreference
Parameter Sets: (All)
Aliases: proga

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserId
The unique identifier of user.

```yaml
Type: String
Parameter Sets: (All)
Aliases: User

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### DSInternals.Win32.WebAuthn.MicrosoftGraphWebauthnCredentialCreationOptions
## NOTES
Self-service operations aren't supported.
More info at https://learn.microsoft.com/en-us/graph/api/fido2authenticationmethod-creationoptions

## RELATED LINKS
