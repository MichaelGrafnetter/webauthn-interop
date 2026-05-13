---
external help file: DSInternals.Passkeys.Okta.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Connect-Okta.md
schema: 2.0.0
---

# Connect-Okta

## SYNOPSIS
Retrieves an access token to interact with Okta APIs.

## SYNTAX

### ApiToken
```
Connect-Okta -Tenant <String> -ApiToken <SecureString> [<CommonParameters>]
```

### AuthorizationCode
```
Connect-Okta -Tenant <String> -ClientId <String> [-Scopes <String[]>] [<CommonParameters>]
```

### ClientSecret
```
Connect-Okta -Tenant <String> -ClientId <String> [-Scopes <String[]>] -ClientSecret <SecureString>
 [<CommonParameters>]
```

### ClientCredentials
```
Connect-Okta -Tenant <String> -ClientId <String> [-Scopes <String[]>] -JsonWebKey <String> [<CommonParameters>]
```

## DESCRIPTION
Acquires an Okta access token via one of four authentication flows, depending on which parameters are supplied, and caches it for subsequent cmdlets in this module:
- Interactive authorization code (public client) when only -Tenant and -ClientId are supplied.
- Client credentials with private_key_jwt when -JsonWebKey is supplied.
- Client credentials with client_secret_post when -ClientSecret is supplied.
- Static API token (SSWS) when -ApiToken is supplied.

The cached token is reused by Get-OktaPasskeyRegistrationOptions, Register-OktaPasskey, and Disconnect-Okta.
Call Disconnect-Okta to revoke the token (for OAuth flows) or clear it from the session (for SSWS).

## EXAMPLES

### EXAMPLE 1
```powershell
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
```

Connects to the \`example.okta.com\` tenant using the application with client id \`0oakmj8hvxvtvCy3P5d7\` via the authorization code flow with PKCE.

### EXAMPLE 2
```powershell
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7 -Scopes @('okta.users.manage','okta.something.else')
```

Connects to the \`example.okta.com\` tenant using the application with client id \`0oakmj8hvxvtvCy3P5d7\` via the authorization code flow with PKCE, requesting scopes \`'okta.users.manage'\` and \`'okta.something.else'\`.

### EXAMPLE 3
```powershell
$jwk = '{"kty":"RSA","kid":"EE3QB0WvhuOwR9DuR6717OERKbDrBemrDKOK4Xvbf8c","d":"TmljZSB0cnkhICBCdXQgdGhpcyBpc...'
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7 -Scopes @('okta.users.manage','okta.something.else') -JsonWebKey $jwk
```

Connects to the \`example.okta.com\` tenant using the application with client id \`0oakmj8hvxvtvCy3P5d7\` via the client credentials flow with private_key_jwt, signing the client assertion with \`$jwk\` and requesting scopes \`'okta.users.manage'\` and \`'okta.something.else'\`.

### EXAMPLE 4
```powershell
$secret = Read-Host -AsSecureString -Prompt 'Client secret'
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7 -Scopes @('okta.users.manage') -ClientSecret $secret
```

Connects to the \`example.okta.com\` tenant using the application with client id \`0oakmj8hvxvtvCy3P5d7\` via the client credentials flow with client_secret_post, authenticating with the SecureString-protected \`$secret\`.

### EXAMPLE 5
```powershell
$apiToken = Read-Host -AsSecureString -Prompt 'API token'
Connect-Okta -Tenant example.okta.com -ApiToken $apiToken
```

Connects to the \`example.okta.com\` tenant using a static SSWS API token issued in the Okta admin console, bypassing the OAuth flow entirely.

## PARAMETERS

### -ApiToken
A static Okta API token (SSWS).
Issued from the Okta admin console under Security \> API \> Tokens.

```yaml
Type: SecureString
Parameter Sets: ApiToken
Aliases: ApiKey, SswsToken, SSWS

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -ClientId
The client id of the Okta application used to obtain an access token.

```yaml
Type: String
Parameter Sets: AuthorizationCode, ClientSecret, ClientCredentials
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -ClientSecret
The client secret used to authenticate to the Okta application, in order to obtain access token using the client credentials OAuth flow (client_secret_post).

```yaml
Type: SecureString
Parameter Sets: ClientSecret
Aliases: Secret

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -JsonWebKey
The JSON Web Key used to authenticate to the Okta application, in order to obtain access token using the client credentials OAuth flow (private_key_jwt).

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
Scopes to request for the access token.
Defaults to 'okta.users.manage'.

```yaml
Type: String[]
Parameter Sets: AuthorizationCode, ClientSecret, ClientCredentials
Aliases:

Required: False
Position: Named
Default value: @('okta.users.manage')
Accept pipeline input: False
Accept wildcard characters: False
```

### -Tenant
The unique identifier of Okta tenant, like 'example.okta.com'.

```yaml
Type: String
Parameter Sets: (All)
Aliases: Organization, OktaOrganization, OktaDomain

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

[Disconnect-Okta](Disconnect-Okta.md)

[Register-OktaPasskey](Register-OktaPasskey.md)

[Create an API token | Okta Developer](https://developer.okta.com/docs/guides/create-an-api-token/main/)

[Okta client authentication methods](https://developer.okta.com/docs/api/openapi/okta-oauth/guides/client-auth)

