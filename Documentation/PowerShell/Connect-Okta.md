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
In order to use this module with Okta, you will need to create an application within your Okta tenant.  You may use the authorization code flow with PKCE (highly recommended for interactive login), or the client credentials flow (for headless use).  If you are unsure which to choose, select the authorization code flow, as it is the most secure option.

### Setup to use the authorization code flow with PKCE
To use the authorization code flow, you will need to create a Single Page App (SPA).  In the application area in the Okta admin console, click `Create App Integration`, then select `OIDC - OpenID Connect` for the sign-in method, then `Single-Page Application` for the application type, and click next. Note:  This process is nearly identical to [User-based API access setup](https://developer.okta.com/docs/reference/rest/#user-based-api-access-setup) in the Okta developer documentation.
<br><br>
<img width="559" alt="Create SPA" src="https://github.com/user-attachments/assets/9a85d754-8e3c-4756-80e5-1dca4635f8f4">
<br><br>
On the next page, give the app integration a meaningful name.  Under assignments, select either `Limit access to selected groups` if you already have a group you want to assign this application to, or select `Skip group assignment for now`.  You can change this later, but you are required to select an option. Everything else can be left at the defaults.  Click save.
<br><br>
<img width="559" alt="SPA options" src="https://github.com/user-attachments/assets/2c83f1b7-8f50-49c3-bfe2-fb77aec50aa6">
<br><br>
WARNING: Do NOT, under ANY circumstances, select `Allow everyone in your organization to access`.  Use one of the other options.  This is your warning.  Seriously.  Do not do this.
<br><br>
<img width="559" alt="Do not select allow everyone" src="https://github.com/user-attachments/assets/bb6cebbd-a9a6-4e12-b198-1040163bf962">
<br><br>
On the next page, ensure that Proof Key for Code Exchange (PKCE) is selected, and note the client ID, you will need this to connect.  
<br><br>
<img width="517" alt="Take note of the Client ID and ensure require PKCE is selected" src="https://github.com/user-attachments/assets/35455c7e-07e8-4112-aefc-e55ba1f56e91">
<br><br>
On the Assignments tab, assing the application to the appropriate user(s).  On the Okta API Scopes tab, scroll down to the okta.users.manage scope and click the `Grant` button to the right of the scope name.  This is the minimal permission required to use this library, since the whole point of the library is to register a passkey on behalf of someone else.  This scope allows the app to create new users and to manage all users' profile and credentials information.  Be very, very careful assigning this app.  

Okta API scopes should look approximately like this:
<br><br>
<img width="506" alt="Okta API scopes: okta.users.manage" src="https://github.com/user-attachments/assets/434bdeb4-fe12-4578-b1c6-dc5f313936f6">
<br><br>
Assignments tab should look something like this:
<br><br>
<img width="506" alt="Okta application assignments" src="https://github.com/user-attachments/assets/c977cb67-05b1-4ccc-98d1-875fcd86a3ca">
<br><br>
### Setup to use the client credentials flow
If you intend to use the client credentials flow, you must set up an API service application as described below.  Note: This process is nearly identical to the [Service-based API access setup](https://developer.okta.com/docs/reference/rest/#service-based-api-access-setup) in the Okta developer documentation.
<img width="559" alt="" src="https://github.com/user-attachments/assets/2a62bb79-7c93-4af8-9527-c7073ae450d5">
<br><br>
On the next page, give the app integration a meaningful name. Click save.
<br><br>
<img width="559" alt="" src="https://github.com/user-attachments/assets/101c98ad-6077-4182-be5d-630c54cfa38b">
<br><br>
On the next page, note the client ID, you will need this to connect. Switch the client authentication configuration from the default client secret to public key/private key by clicking edit, then selecting the radio button.
<br><br>
<img width="572" alt="image" src="https://github.com/user-attachments/assets/96aa533d-bf16-4920-8690-be4ebf703d90">
<br><br>
The public keys configuration section will open. For a simple test configuration, leave the radio button for save keys to Okta selected. You will need to add at least one key here, by clicking the add key button.
<br><br>
<img width="508" alt="image" src="https://github.com/user-attachments/assets/e5539794-1e79-43e9-ac83-a47dc0bef4e6">
<br><br>
The add a public key dialog will appear. You have the option to paste in your own public key, or let Okta generate one based on RS256 for you by clicking generate new key.
<br><br>
<img width="508" alt="image" src="https://github.com/user-attachments/assets/e9c0a690-c40b-418d-b7bb-3f83dbf418be">
<br><br>
Leave the private key format at the default JSON, and click the copy to clipboard button. Save the contents in a safe place, as this key is a sensitive credential, then click save.
<br><br>
<img width="539" alt="image" src="https://github.com/user-attachments/assets/10ec207b-859d-483a-9c6b-2d821d2d4f60">
<br><br>
I typically compress the output to avoid issues later.  In the compressed format the key is ready to use as a parameter value to the [-JsonWebKey parameter](https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Connect-Okta.md#-jsonwebkey)
<br><br>
```powershell
PS C:\> $jwk = @'
{
  "kty": "RSA",
  "e": "AQAB",
  "kid": "EE3QB0WvhuOwR9DuR6717OERKbDrBemrDKOK4Xvbf8c",
  "d": "TmljZSB0cnkhICBCdXQgdGhpcyBpc24ndCBhIHJlYWwga2V5Lg",
  "p": "wqFCdWVuIGludGVudG8hIFBlcm8gZXN0YSBubyBlcyB1bmEgY2xhdmUgcmVhbC4",
  "q": "TmV0dGVyIFZlcnN1Y2ghIEFiZXIgZGFzIGlzdCBrZWluIGVjaHRlciBTY2hsw7xzc2VsLg",
  "dp": "QmVsbGUgdGVudGF0aXZlICEgTWFpcyBjZSBuJ2VzdCBwYXMgdW5lIHZyYWllIGNsw6ku",
  "dq": "5LiN6ZSZ55qE5bCd6K-V77yB5L2G6L-Z5LiN5piv55yf5q2j55qE6ZKl5YyZ44CC",
  "qi": "7KKL7J2AIOyLnOuPhOuEpOyalCEg7ZWY7KeA66eMIOydtOqxtCDsp4Tsp5wg7Je07Ieg6rCAIOyVhOuLiOyXkOyalC4",
  "n": "uT64MHF-qH5F-jLAoWxuwI8_YJ2cdkMIjvlyazJnB0xjvtddS14O8cfDHW2lxHWiOor-jarCK282WC1BukjnizCeUl7YoWDWnVNOcQd1hi__nC5cjhgnjVPNgBiRQhctj8e6HWgKfMHTxZ_vaMsqQZJ9QwYy1NzkxchqcI9pZN1zlA2LHdXsXzvJeRxzUg8EjGxaDKuLPHPUfZxf0Wr4Jv-lNVYzwg84XtzOoZ0v9Hb91ZISmVKKtMegXX2ahGRgMeI1pgX8oCG8TCgx7x-SmViJfi9iNfznJWyA_lZiUKu3qJMsFJdgfRp9egCZtUl6P_nnpqe-_-MNmy55WXhc0Q"
}
'@
| ConvertFrom-Json | ConvertTo-Json -Compress
```
On the Okta API Scopes tab, scroll down to the okta.users.manage scope and click the `Grant` button to the right of the scope name.  This is the minimal permission required to use this library, since the whole point of the library is to register a passkey on behalf of someone else.  This scope allows the app to create new users and to manage all users' profile and credentials information. Okta API scopes should look approximately like this:
<br><br>
Okta API scopes: okta.users.manage" src="https://github.com/user-attachments/assets/434bdeb4-fe12-4578-b1c6-dc5f313936f6">
<br><br>
On the Admin roles tab, you will need to assign an admin role to this app. The `Help Desk Administrator` role seems to work well for this module.
<br><br>
<img width="506" alt="Admin assignment help desk administrator role" src="https://github.com/user-attachments/assets/8ffb2acb-67cb-4cb4-9aef-6747540a0fdd">
<br>
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

### -JsonWebKey
The JSON Web Key used to authenticate to the Okta application, in order to obtain access token using the client credentials OAuth flow.

```yaml
Parameter Sets: ClientCredentials
Aliases: jwk

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS
https://developer.okta.com/docs/reference/rest/
