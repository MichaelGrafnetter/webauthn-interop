---
external help file: DSInternals.Passkeys-help.xml
Module Name: DSInternals.Passkeys
online version:
schema: 2.0.0
---

# New-OktaPasskey

## SYNOPSIS
Creates a new Okta-compatible passkey.

## SYNTAX

```
New-OktaPasskey [-Options] <OktaWebauthnCredentialCreationOptions> [-ProgressAction <ActionPreference>]
 [<CommonParameters>]
```

## DESCRIPTION
{{ Fill in the Description }}

## EXAMPLES

### EXAMPLE 1
```
New-OktaPasskey -Options $options
```

### EXAMPLE 2
```
Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -Tenant example.okta.com -Token your_okta_token | New-OktaPasskey
```

## PARAMETERS

### -Options
Options required to generate an Okta-compatible passkey.

```yaml
Type: OktaWebauthnCredentialCreationOptions
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### DSInternals.Win32.WebAuthn.OktaWebauthnAttestationResponse
## NOTES

## RELATED LINKS
