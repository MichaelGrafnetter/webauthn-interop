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

### If you are using the authorization code flow
```
Connect-Okta -Tenant <String> -ClientId <String> [-Scopes <String[]>] [-ProgressAction <ActionPreference>]
 [<CommonParameters>]
```

### If you are using the client credentials flow
```
Connect-Okta -Tenant <String> -ClientId <String> [-Scopes <String[]>]
 -JsonWebKey <ValidateNotNullOrEmptyAttribute> [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

## DESCRIPTION
In order to use this module with Okta, you will need to create an application within your Okta tenant.  You may use the authorization code flow with PKCE (highly recommended for interactive login), or the client credentials flow (for headless use).  If you are unsure which to choose, select the authentication flow, as it is the most secure option.

To use the authoriztion code flow, you will need to create a Single Page App (SPA).  In the application area in the Okta admin console, click "Create App Integration", then select "OIDC - OpenID Connect" for the sign-in method, then "Single-Page Application" for the application type, and click next.
<br><br>
<img width="559" alt="Create SPA" src="https://github.com/user-attachments/assets/9a85d754-8e3c-4756-80e5-1dca4635f8f4">
<br><br>
On the next page, give the app integration a meaningful name.  Under assignments, select either "Limit access to selected groups" if you already have a group you want to assign this application to, or select "Skip group assignment for now".  You can change this later, but you are required to select an option. Everything else can be left at the defaults.  Click save.
<br><br>
<img width="559" alt="SPA options" src="https://github.com/user-attachments/assets/2c83f1b7-8f50-49c3-bfe2-fb77aec50aa6">
<br><br>
WARNING: Do NOT, under ANY circumstances, select "Allow everyone in your organization to access".  Use one of the other options.  This is your warning.  Seriously.  Do not do this.
<br><br>
<img width="559" alt="Do not select allow everyone" src="https://github.com/user-attachments/assets/bb6cebbd-a9a6-4e12-b198-1040163bf962">
<br><br>
On the next page, ensure that Proof Key for Code Exchange (PKCE) is selected, and note the client ID, you will need this to connect.  On the Assignments tab, assing the application to the appropriate user(s).  On the Okta API Scopes tab, scroll down to the okta.users.manage scope and click the "Grant" button to the right of the scope name.  This is the minimal permission required to use this library, since the whole point of the library is to register a passkey on behalf of someone else.  This scope allows the app to create new users and to manage all users' profile and credentials information.  Be very, very careful assigning this app.  

Okta API scopes should look approximately like this:
<br><br>
<img width="506" alt="Okta API scopes: okta.users.manage" src="https://github.com/user-attachments/assets/434bdeb4-fe12-4578-b1c6-dc5f313936f6">
<br><br>
Assignments tab should look something like this:
<br><br>
<img width="506" alt="Okta application assignments" src="https://github.com/user-attachments/assets/c977cb67-05b1-4ccc-98d1-875fcd86a3ca">
<br><br>
If you are intending to use the client credentials flow

## EXAMPLES

### Example 1
```powershell
PS C:\> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
```

Connects to example.okta.com tenant using application with client id 0oakmj8hvxvtvCy3P5d7 with the authentication code flow with PKCE

### Example 2
```powershell
PS C:\> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7 -Scopes @('okta.users.manage','okta.something.else')
```

Connects to example.okta.com tenant using application with client id 0oakmj8hvxvtvCy3P5d7 with the authentication code flow with PKCE, requesting scopes 'okta.users.manage' and 'okta.something.else'

### Example 3
```powershell
PS C:\> $jwk = '{"kty":"RSA","kid":"EE3QB0WvhuOwR9DuR6717OERKbDrBemrDKOK4Xvbf8c","d":"TmljZSB0cnkhICBCdXQgdGhpcyBpc...'
PS C:\> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7 -Scopes @('okta.users.manage','okta.something.else') -JsonWebKey $jwk
```

Connects to example.okta.com tenant using application with client id 0oakmj8hvxvtvCy3P5d7 with the client credentials flow using $jwk as the the client credential, requesting scopes 'okta.users.manage' and 'okta.something.else'

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
Type: ValidateNotNullOrEmptyAttribute
Parameter Sets: ClientCredentials
Aliases: jwk

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```
### -Scopes
Scopes to request for the access token.  Defaults to 'okta.users.manage'.

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
The unique identifier of Okta tenant, like 'example.okta.com'.

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
