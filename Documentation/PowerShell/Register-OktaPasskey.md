---
external help file: DSInternals.Passkeys-help.xml
Module Name: DSInternals.Passkeys
online version:
schema: 2.0.0
---

# Register-OktaPasskey

## SYNOPSIS
Registers a new passkey in Okta.

## SYNTAX

### New (Default)
```
Register-OktaPasskey -UserId <String> [-ChallengeTimeout <TimeSpan>] -Tenant <String> -Token <String>
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

### Existing
```
Register-OktaPasskey [-UserId <String>] -Passkey <OktaWebauthnAttestationResponse> -Tenant <String>
 -Token <String> [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

## DESCRIPTION
{{ Fill in the Description }}

## EXAMPLES

### EXAMPLE 1
```
Register-OktaPasskey -UserId 00eDuihq64pgP1gVD0x7 -Tenant example.okta.com -Token your_okta_token
```

### EXAMPLE 2
```
Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -Tenant example.okta.com -Token your_okta_token | New-OktaPasskey | Register-OktaPasskey -Tenant example.okta.com -Token your_okta_token
```

## PARAMETERS

### -ChallengeTimeout
Overrides the timeout of the server-generated challenge returned in the request.
The default value is 5 minutes, with the accepted range being between 5 minutes and 30 days.

```yaml
Type: TimeSpan
Parameter Sets: New
Aliases: Timeout

Required: False
Position: Named
Default value: (New-TimeSpan -Seconds 300)
Accept pipeline input: False
Accept wildcard characters: False
```

### -Passkey
The passkey to be registered.

```yaml
Type: OktaWebauthnAttestationResponse
Parameter Sets: Existing
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
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
{{ Fill Tenant Description }}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
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
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserId
The unique identifier of user.

```yaml
Type: String
Parameter Sets: New
Aliases: User

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

```yaml
Type: String
Parameter Sets: Existing
Aliases: User

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

### System.String
## NOTES
More info at https://developer.okta.com/docs/api/openapi/okta-management/management/tag/UserFactor/#tag/UserFactor/operation/activateFactor

## RELATED LINKS
