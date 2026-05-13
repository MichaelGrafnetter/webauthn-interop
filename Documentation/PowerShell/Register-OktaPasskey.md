---
external help file: DSInternals.Passkeys.Okta.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Register-OktaPasskey.md
schema: 2.0.0
---

# Register-OktaPasskey

## SYNOPSIS
Registers a new passkey in Okta.

## SYNTAX

### New (Default)
```
Register-OktaPasskey -UserId <String> [-ChallengeTimeout <TimeSpan>] [<CommonParameters>]
```

### AttestationCredential
```
Register-OktaPasskey -UserId <String> -FactorId <String>
 -AttestationPublicKeyCredential <AttestationPublicKeyCredential> [<CommonParameters>]
```

### AttestationCredentialByLogin
```
Register-OktaPasskey -Login <String> -FactorId <String>
 -AttestationPublicKeyCredential <AttestationPublicKeyCredential> [<CommonParameters>]
```

### NewByLogin
```
Register-OktaPasskey -Login <String> [-ChallengeTimeout <TimeSpan>] [<CommonParameters>]
```

### Existing
```
Register-OktaPasskey -Passkey <OktaWebauthnAttestationResponse> [<CommonParameters>]
```

## DESCRIPTION
Registers a new passkey for the specified user in Okta by submitting the attestation that activates the corresponding webauthn factor.

The cmdlet supports three usage patterns:
- Pass only -UserId to perform the full ceremony end-to-end: request a challenge, drive the local authenticator, and submit the attestation.
- Pipe an attestation from a previous New-Passkey call against Okta options.
- Pass -UserId, -FactorId, and a raw -AttestationPublicKeyCredential when the challenge was issued and the credential ceremony was run separately.

Requires an active Okta connection (Connect-Okta).

## EXAMPLES

### EXAMPLE 1
```powershell
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
Register-OktaPasskey -UserId 00eDuihq64pgP1gVD0x7
```

Performs the full registration ceremony in one step: enrolls a webauthn factor, prompts the local authenticator, and activates the factor in Okta.

### EXAMPLE 2
```powershell
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
Register-OktaPasskey -Login 'user@example.com'
```

Resolves the Okta user by login and then performs the full registration ceremony, avoiding the need to look up the Okta id manually.

### EXAMPLE 3
```powershell
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
Register-OktaPasskey -UserId 00eDuihq64pgP1gVD0x7 -ChallengeTimeout (New-TimeSpan -Minutes 1)
```

Registers a passkey using a shorter 1-minute challenge timeout to tighten the registration window.

### EXAMPLE 4
```powershell
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 | New-Passkey | Register-OktaPasskey
```

Splits the registration into explicit pipeline stages: enroll the factor, create the credential locally, and activate.
Equivalent to the single-step form but lets the caller inspect intermediate values.

### EXAMPLE 5
```powershell
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
$options = Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7
$credential = New-Passkey -Options $options.PublicKeyOptions
Register-OktaPasskey -UserId 00eDuihq64pgP1gVD0x7 -FactorId $options.FactorId -AttestationPublicKeyCredential $credential
```

Drives the WebAuthn ceremony with a raw AttestationPublicKeyCredential and assembles the activation manually.
Useful when the credential was produced outside of an Okta-aware pipeline.

## PARAMETERS

### -AttestationPublicKeyCredential
The raw attestation credential produced by the local WebAuthn authenticator (e.g.
via New-Passkey), used together with -UserId and -FactorId as an alternative to -Passkey.

```yaml
Type: AttestationPublicKeyCredential
Parameter Sets: AttestationCredential, AttestationCredentialByLogin
Aliases: Attestation, Credential

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -ChallengeTimeout
Overrides the timeout of the server-generated challenge returned in the request.
The default value is 5 minutes, with the accepted range being between 1 second and 1 day.

```yaml
Type: TimeSpan
Parameter Sets: New, NewByLogin
Aliases: Timeout

Required: False
Position: Named
Default value: (New-TimeSpan -Minutes 5)
Accept pipeline input: False
Accept wildcard characters: False
```

### -FactorId
The Okta factor identifier returned by Get-OktaPasskeyRegistrationOptions, used together with -AttestationPublicKeyCredential as an alternative to -Passkey.

```yaml
Type: String
Parameter Sets: AttestationCredential, AttestationCredentialByLogin
Aliases: Factor

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Login
The Okta user login (typically an email address such as 'user@example.com').
Resolved to a UserId through an API call.

```yaml
Type: String
Parameter Sets: AttestationCredentialByLogin, NewByLogin
Aliases: UserPrincipalName, UPN, UserName, Email

Required: True
Position: Named
Default value: None
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

### -UserId
The unique identifier of the Okta user.

```yaml
Type: String
Parameter Sets: New, AttestationCredential
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
## NOTES

## RELATED LINKS

[Get-OktaPasskeyRegistrationOptions](Get-OktaPasskeyRegistrationOptions.md)

[New-Passkey](New-Passkey.md)

[Connect-Okta](Connect-Okta.md)

[Okta User Factors API](https://developer.okta.com/docs/api/openapi/okta-management/management/tag/UserFactor/#tag/UserFactor/operation/activateFactor)

