---
external help file: DSInternals.Passkeys-help.xml
Module Name: DSInternals.Passkeys
online version:
schema: 2.0.0
---

# Get-OktaPasskeyRegistrationOptions

## SYNOPSIS
Retrieves creation options required to generate and register an Okta-compatible passkey.

## SYNTAX

```
Get-OktaPasskeyRegistrationOptions [-Tenant] <String> [-UserId] <String> [[-ChallengeTimeout] <TimeSpan>]
 [-Token] <String> [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

## DESCRIPTION
{{ Fill in the Description }}

## EXAMPLES

### EXAMPLE 1
```
Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -Tenant example.okta.com -Token your_okta_token
```

### EXAMPLE 2
```
Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -ChallengeTimeout (New-TimeSpan -Seconds 60) -Tenant example.okta.com -Token your_okta_token
```

## PARAMETERS

### -ChallengeTimeout
Overrides the timeout of the server-generated challenge returned in the request.
The default value is 300 seconds

```yaml
Type: TimeSpan
Parameter Sets: (All)
Aliases: Timeout

Required: False
Position: 3
Default value: (New-TimeSpan -Seconds 300)
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

### -Tenant
The unique identifier of Okta tenant, like 'example.okta.com'

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Token
The SSWS or Bearer token from Okta with okta.users.manage permissions.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 4
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserId
The unique identifier of Okta user, like '00ub61wm1aqmawzRC5d7'.

```yaml
Type: String
Parameter Sets: (All)
Aliases: User

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### DSInternals.Win32.WebAuthn.OktaWebauthnCredentialCreationOptions
## NOTES
More info at https://developer.okta.com/docs/api/openapi/okta-management/management/tag/UserFactor/#tag/UserFactor/operation/enrollFactor

## RELATED LINKS
