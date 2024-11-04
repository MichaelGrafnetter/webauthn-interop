---
external help file: DSInternals.Passkeys-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Register-Passkey.md
schema: 2.0.0
---

# Register-Passkey

## SYNOPSIS
Registers a new passkey in Microsoft Entra ID, or Okta.

## SYNTAX

### EntraIDNew (Default)
```
Register-Passkey -UserId <String> -DisplayName <String> [-ChallengeTimeout <TimeSpan>]
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

### OktaNew
```
Register-Passkey -UserId <String> [-ChallengeTimeout <TimeSpan>] [-ProgressAction <ActionPreference>]
 [<CommonParameters>]
```

### Existing
```
Register-Passkey -UserId <String> -Passkey <WebauthnAttestationResponse> [-ProgressAction <ActionPreference>]
 [<CommonParameters>]
```

## DESCRIPTION
Takes a created credential from New-Passkey and registers it as a usable authentication factor with Entra ID or Okta.

## EXAMPLES

### EXAMPLE 1 (Entra ID)
```
PS \> Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
PS \> Register-Passkey -UserId 'AdeleV@contoso.com' -DisplayName 'YubiKey 5 Nano'
```

### EXAMPLE 2 (Entra ID)
```
PS \> Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
PS \> Register-Passkey -UserId 'AdeleV@contoso.com' -DisplayName 'YubiKey 5 Nano' -ChallengeTimeout (New-TimeSpan -Minutes 10)
```

### EXAMPLE 3 (Entra ID)
```
PS \> Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
PS \> Get-PasskeyRegistrationOptions -UserId 'AdeleV@contoso.com' | New-Passkey -DisplayName 'YubiKey 5 Nano' | Register-Passkey -UserId 'AdeleV@contoso.com'
```

### EXAMPLE 4 (Okta)
```
PS \> Register-Passkey -UserId 00eDuihq64pgP1gVD0x7 -Tenant example.okta.com -Token your_okta_token
```

### EXAMPLE 5 (Okta)
```
PS \> Get-PasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -Tenant example.okta.com -Token your_okta_token | New-Passkey | Register-Passkey -Tenant example.okta.com -Token your_okta_token
```

## PARAMETERS

### -ChallengeTimeout
Overrides the timeout of the server-generated challenge returned in the request. 
For Entra ID, the default value is 5 minutes, with the accepted range being between 5 minutes and 30 days. 
For Okta, the default value is 300 seconds, with the accepted range being between 1 second and 1 day.

```yaml
Type: TimeSpan
Parameter Sets: EntraIDNew, OktaNew
Aliases: Timeout

Required: False
Position: Named
Default value: (New-TimeSpan -Minutes 5)
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayName
Custom name given to the Entra ID registered passkey.

```yaml
Type: String
Parameter Sets: EntraIDNew
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Passkey
The passkey to be registered.

```yaml
Type: WebauthnAttestationResponse
Parameter Sets: Existing
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```
<<<<<<< HEAD
### -Tenant
The unique identifier of the Okta tenant.
=======

### -ProgressAction
{{ Fill ProgressAction Description }}
>>>>>>> 4ac6f7f (Add Microsoft.Identity.Client)

```yaml
Type: ActionPreference
Parameter Sets: (All)
Aliases: proga

<<<<<<< HEAD
Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Token
The SSWS or Bearer token from Okta with okta.users.manage permissions. You should use a bearer token. 

```yaml
Type: String
Parameter Sets: OktaNew, OktaExisting
Aliases:

Required: True
=======
Required: False
>>>>>>> 4ac6f7f (Add Microsoft.Identity.Client)
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserId
The unique identifier of user. 
For Entra ID, this is the object id (GUID) or UPN. 
For Okta, this is the unique identifier of Okta user.

```yaml
Type: String
Parameter Sets: (All)
Aliases: User

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

### DSInternals.Win32.WebAuthn.Okta.OktaFido2AuthenticationMethod
### Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod
## NOTES
For the Okta token, you should not use SSWS but instead use a bearer token.

## RELATED LINKS

More info for Entra ID at https://learn.microsoft.com/en-us/graph/api/authentication-post-fido2methods
More info for Okta at https://developer.okta.com/docs/api/openapi/okta-management/management/tag/UserFactor/#tag/UserFactor/operation/activateFactor