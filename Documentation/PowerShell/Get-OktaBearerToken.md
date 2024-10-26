---
external help file: DSInternals.Passkeys-help.xml
Module Name: DSInternals.Passkeys
online version:
schema: 2.0.0
---

# Get-OktaBearerToken

## SYNOPSIS
Authenticates user using the OAuth2 authorization code flow with PKCE, returning a bearer token with scope "okta.users.manage".

## SYNTAX

```
Get-OktaBearerToken [-Tenant] <String> [-Client_Id] <String> [-ProgressAction <ActionPreference>]
 [<CommonParameters>]
```

## DESCRIPTION
{{ Fill in the Description }}

## EXAMPLES

### EXAMPLE 1
```
$token = Get-OktaBearerToken -Tenant example.okta.com -Client_Id 0oakmj8hvxvtvCy3P5d7
```

## PARAMETERS

### -Client_Id
The unique identifier of an Okta native or single-page application within the supplied Okta tenant, with the (default) grant type "Authorization Code" enabled, Proof Key for Code Exchange (PKCE) enabled, the (default) sign-in redirect URI containing "http://localhost:8080/login/callback", the Okta API scope "okta.users.manage" granted, and the application assigned to the user running the script.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 2
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
The unique identifier of Okta tenant, like 'example.okta.com'

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES
More info at https://developer.okta.com/docs/guides/implement-grant-type/authcodepkce/main/

## RELATED LINKS
