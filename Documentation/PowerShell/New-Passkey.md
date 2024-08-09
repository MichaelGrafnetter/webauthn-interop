---
external help file: DSInternals.Passkeys-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/New-Passkey.md
schema: 2.0.0
---

# New-Passkey

## SYNOPSIS

Creates a new Microsoft Entra ID-compatible passkey.

## SYNTAX

```
New-Passkey [-Options] <MicrosoftGraphWebauthnCredentialCreationOptions> [-DisplayName] <String>
 [<CommonParameters>]
```

## DESCRIPTION

{{ Fill in the Description }}

## EXAMPLES

### EXAMPLE 1

```powershell
Get-PasskeyRegistrationOptions -UserId 'AdeleV@contoso.com' | New-Passkey -DisplayName 'YubiKey 5 Nano' | Register-Passkey -UserId 'AdeleV@contoso.com'
Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
```

## PARAMETERS

### -DisplayName

Custom name given to the registered passkey.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Options

Options required to generate a Microsoft Entra ID-compatible passkey.

```yaml
Type: MicrosoftGraphWebauthnCredentialCreationOptions
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### DSInternals.Win32.WebAuthn.MicrosoftGraphWebauthnAttestationResponse
## NOTES

## RELATED LINKS
