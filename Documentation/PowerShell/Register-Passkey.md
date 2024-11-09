---
external help file: DSInternals.Passkeys-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/New-Passkey.md
schema: 2.0.0
---

# New-Passkey

## SYNOPSIS
Creates a new Microsoft Entra ID or Okta compatible passkey.

## SYNTAX

```
New-Passkey [-Options] <WebauthnCredentialCreationOptions> [[-DisplayName] <String>]
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

## DESCRIPTION
Takes the MicrosoftGraphWebauthnCredentialCreationOptions or OktaWebauthnCredentialCreationOptions object from Get-PasskeyRegistrationOptions and uses them to create a credential using the system dialogs.

## EXAMPLES

### EXAMPLE 1 (Entra ID)
```
PS \> Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
PS \> Get-PasskeyRegistrationOptions -UserId 'AdeleV@contoso.com' | New-Passkey -DisplayName 'YubiKey 5 Nano' | Register-Passkey -UserId 'AdeleV@contoso.com'
```

### EXAMPLE 2 (Okta)
```
PS \> New-Passkey -Options $options
```

### EXAMPLE 3 (Okta)
```
PS \> Get-PasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -Tenant example.okta.com -Token your_okta_token | New-Passkey
```

## PARAMETERS

### -DisplayName
Custom name given to the Entra ID registered passkey.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Options
Options required to generate a Microsoft Entra ID or Okta compatible passkey.

```yaml
Type: WebauthnCredentialCreationOptions
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

### DSInternals.Win32.WebAuthn.WebauthnAttestationResponse
## NOTES

## RELATED LINKS

https://learn.microsoft.com/en-us/windows/win32/webauthn/-webauthn-portal