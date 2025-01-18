---
external help file: DSInternals.Passkeys-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Connect-Okta.md
schema: 2.0.0
---

# Connect-Okta

## SYNOPSIS
Retrieves an access token to interact with Okta APIs.

## SYNTAX

### AuthorizationCode
```
Connect-Okta -Tenant <String> -ClientId <String> [-Scopes <String[]>] [<CommonParameters>]
```

### ClientCredentials
```
Connect-Okta -Tenant <String> -ClientId <String> [-Scopes <String[]>] -JsonWebKey <String> [<CommonParameters>]
```

## DESCRIPTION
In order to use this module with Okta, you will need to create an application within your Okta tenant.  You may use the authorization code flow with PKCE (highly recommended for interactive login), or the client credentials flow (for headless use).  If you are unsure which to choose, select the authorization code flow, as it is the most secure option.

## EXAMPLES

### Example 1
```powershell
PS C:\> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
```

Connects to `example.okta.com` tenant using application with client id `0oakmj8hvxvtvCy3P5d7` with the authentication code flow with PKCE

### Example 2
```powershell
PS C:\> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7 -Scopes @('okta.users.manage','okta.something.else')
```

Connects to `example.okta.com` tenant using application with client id `0oakmj8hvxvtvCy3P5d7` with the authentication code flow with PKCE, requesting scopes `'okta.users.manage'` and `'okta.something.else'`

### Example 3
```powershell
PS C:\> $jwk = '{"kty":"RSA","kid":"EE3QB0WvhuOwR9DuR6717OERKbDrBemrDKOK4Xvbf8c","d":"TmljZSB0cnkhICBCdXQgdGhpcyBpc...'
PS C:\> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7 -Scopes @('okta.users.manage','okta.something.else') -JsonWebKey $jwk
```

Connects to `example.okta.com` tenant using application with client id `0oakmj8hvxvtvCy3P5d7` with the client credentials flow using `$jwk` as the the client credential, requesting scopes `'okta.users.manage'` and `'okta.something.else'`

## PARAMETERS

### -ClientId
The client id of the Okta application used to obtain an access token.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -JsonWebKey
The JSON Web Key used to authenticate to the Okta application, in order to obtain access token using the client credentials OAuth flow.

```yaml
Type: String
Parameter Sets: ClientCredentials
Aliases: jwk

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Scopes
Scopes to request for the access token.  Defaults to `'okta.users.manage'`.

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

### -Tenant
The unique identifier of Okta tenant, like `'example.okta.com'`.

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

https://developer.okta.com/docs/reference/rest/
