---
external help file: DSInternals.Passkeys.Okta.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Get-OktaPasskeyRegistrationOptions.md
schema: 2.0.0
---

# Get-OktaPasskeyRegistrationOptions

## SYNOPSIS
Retrieves creation options required to generate and register an Okta compatible passkey.

## SYNTAX

### UserId (Default)
```
Get-OktaPasskeyRegistrationOptions -UserId <String> [-ChallengeTimeout <TimeSpan>] [<CommonParameters>]
```

### Login
```
Get-OktaPasskeyRegistrationOptions -Login <String> [-ChallengeTimeout <TimeSpan>] [<CommonParameters>]
```

## DESCRIPTION
Retrieves a server-issued challenge and the associated WebAuthn parameters needed to register (attest) a new passkey for the specified Okta user.
The returned object can be piped to New-Passkey to drive the local authenticator and then to Register-OktaPasskey to complete enrollment.

For end-to-end passkey registration in Okta, calling Register-OktaPasskey directly is recommended; it performs the challenge request, authenticator ceremony, and activation in a single step.
Use Get-OktaPasskeyRegistrationOptions only when you need to inspect or customize the intermediate options.

Requires an active Okta connection (Connect-Okta).

## EXAMPLES

### EXAMPLE 1
```powershell
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7
```

Fetches default creation options for the specified Okta user, identified by their Okta id.

### EXAMPLE 2
```powershell
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
Get-OktaPasskeyRegistrationOptions -Login 'user@example.com'
```

Resolves the Okta user by login and then fetches creation options, avoiding the need to look up the Okta id manually.

### EXAMPLE 3
```powershell
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -ChallengeTimeout (New-TimeSpan -Minutes 1)
```

Fetches creation options with a shorter 1-minute challenge timeout to tighten the registration window.

### EXAMPLE 4
```powershell
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
Get-OktaPasskeyRegistrationOptions -Login 'user@example.com' | New-Passkey | Register-OktaPasskey
```

Performs end-to-end passkey registration in Okta in a single pipeline.

## PARAMETERS

### -ChallengeTimeout
Overrides the timeout of the server-generated challenge returned in the request.
The default value is 5 minutes, with the accepted range being between 1 second and 1 day.

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

### -Login
The Okta user login (typically an email address such as 'user@example.com').
Resolved to a UserId through an API call.

```yaml
Type: String
Parameter Sets: Login
Aliases: UserPrincipalName, UPN, UserName, Email

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserId
The unique identifier of the Okta user.

```yaml
Type: String
Parameter Sets: UserId
Aliases: User

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

### DSInternals.Win32.WebAuthn.Okta.OktaWebauthnCredentialCreationOptions
## NOTES

## RELATED LINKS

[Register-OktaPasskey](Register-OktaPasskey.md)

[New-Passkey](New-Passkey.md)

[Connect-Okta](Connect-Okta.md)

[Okta User Factors API](https://developer.okta.com/docs/api/openapi/okta-management/management/tag/UserFactor/#tag/UserFactor/operation/enrollFactor)

