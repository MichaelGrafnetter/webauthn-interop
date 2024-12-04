---
external help file: DSInternals.Passkeys-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/New-Passkey.md
schema: 2.0.0
---

# Register-Passkey

## SYNOPSIS
Registers a newly created passkey with Microsoft Entra ID or Okta.

## SYNTAX

### New (Default)
```
Register-Passkey -UserId <String> -DisplayName <String> [-ChallengeTimeout <TimeSpan>] [<CommonParameters>]
```

### Existing
```
Register-Passkey -UserId <String> -Passkey <WebauthnAttestationResponse> [<CommonParameters>]
```

## DESCRIPTION

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
PS \> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
PS \> Register-Passkey -UserId 00eDuihq64pgP1gVD0x7 
```

### EXAMPLE 5 (Okta)
```
PS \> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
PS \> Get-PasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 | New-Passkey | Register-Passkey 
```

## PARAMETERS

### -UserId
The unique identifier of user. 
For Entra ID, this is the object id (GUID) or UPN. 
For Okta, this is the unique identifier of Okta user.

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
### -Passkey
The passkey to be registered.

```yaml
Type: DSInternals.Win32.WebAuthn.WebauthnAttestationResponse
Parameter Sets: (Existing)
Aliases:

Required: True
Position: 2
Default value: None
Accept pipeline input: True
Accept wildcard characters: False
```

### -DisplayName
Custom name given to the Entra ID registered passkey.

```yaml
Type: String
Parameter Sets: (EntraIDNew)
Aliases:

Required: False
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ChallengeTimeout
Overrides the timeout of the server-generated challenge returned in the request. 
For Entra ID, the default value is 5 minutes, with the accepted range being between 5 minutes and 30 days. 
For Okta, the default value is 300 seconds, with the accepted range being between 1 second and 1 day.

```yaml
Type: TimeSpan
Parameter Sets: (EntraIDNew, OktaNew)
Aliases: Timeout

Required: False
Position: 3
Default value: (New-TimeSpan -Minutes 5)
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS
### DSInternals.Win32.WebAuthn.WebauthnAttestationResponse

## OUTPUTS

### Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod
### DSInternals.Win32.WebAuthn.Okta.OktaFido2AuthenticationMethod

## NOTES

## RELATED LINKS

[More info for Entra ID](https://learn.microsoft.com/en-us/graph/api/authentication-post-fido2methods)
<br>
[More info for Okta](https://developer.okta.com/docs/api/openapi/okta-management/management/tag/UserFactor/#tag/UserFactor/operation/activateFactor)
