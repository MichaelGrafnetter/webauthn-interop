---
external help file: DSInternals.Passkeys-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Get-PasskeyRegistrationOptions.md
schema: 2.0.0
---

# Get-PasskeyRegistrationOptions

## SYNOPSIS
Retrieves creation options required to generate and register a Microsoft Entra ID or Okta compatible passkey.

## SYNTAX

### EntraID (Default)
```
Get-PasskeyRegistrationOptions -UserId <String> [-ChallengeTimeout <TimeSpan>]
```

### Okta
```
Get-PasskeyRegistrationOptions -Tenant <String> -UserId <String> [-ChallengeTimeout <TimeSpan>] -Token <String>
```

## EXAMPLES

### EXAMPLE 1 (Entra ID)
```
PS \> Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
PS \> Get-PasskeyRegistrationOptions -UserId 'AdeleV@contoso.com'
```

### EXAMPLE 2 (Entra ID)
```
PS \> Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
PS \> Get-PasskeyRegistrationOptions -UserId 'AdeleV@contoso.com' -ChallengeTimeout (New-TimeSpan -Minutes 10)
```

### EXAMPLE 3 (Okta)
```
PS \> Get-PasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -Tenant example.okta.com -Token your_okta_token
```

### EXAMPLE 4 (Okta)
```
PS \> Get-PasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -ChallengeTimeout (New-TimeSpan -Seconds 60) -Tenant example.okta.com -Token your_okta_token
```

## PARAMETERS

### -ChallengeTimeout
Overrides the timeout of the server-generated challenge returned in the request. 
For Entra ID, the default value is 5 minutes, with the accepted range being between 5 minutes and 30 days. 
For Okta, the default value is 300 seconds, with the accepted range being between 1 second and 1 day.

```yaml
Type: TimeSpan
Parameter Sets: (All)
Aliases: Timeout

Required: False
Position: Named
Default value: (New-TimeSpan -Minutes 5)
Accept pipeline input: False
Accept wildcard characters: False
```
### -Tenant
The unique identifier of the Okta tenant.

```yaml
Type: String
Parameter Sets: Okta
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Token
The SSWS or Bearer token from Okta with okta.users.manage permissions. You should uee a bearer token. 

```yaml
Type: String
Parameter Sets: Okta
Aliases:

Required: True
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

## INPUTS

## OUTPUTS

### DSInternals.Win32.WebAuthn.EntraID.MicrosoftGraphWebauthnCredentialCreationOptions

OR

### DSInternals.Win32.WebAuthn.Okta.OktaWebauthnCredentialCreationOptions

## NOTES
Self-service operations aren't supported.
For the Okta token, you should not use SSWS but instead use a bearer token.

## RELATED LINKS
More info about Entra ID at https://learn.microsoft.com/en-us/graph/api/fido2authenticationmethod-creationoptions
More info about Okta at https://developer.okta.com/docs/api/openapi/okta-management/management/tag/UserFactor/#tag/UserFactor/operation/enrollFactor