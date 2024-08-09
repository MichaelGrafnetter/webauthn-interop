---
external help file: DSInternals.Passkeys-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Register-Passkey.md
schema: 2.0.0
---

# Register-Passkey

## SYNOPSIS

Registers a new passkey in Microsoft Entra ID.

## SYNTAX

### New (Default)
```
Register-Passkey -UserId <String> -DisplayName <String> [-ChallengeTimeout <TimeSpan>] [<CommonParameters>]
```

### Existing
```
Register-Passkey -UserId <String> -Passkey <MicrosoftGraphWebauthnAttestationResponse> [<CommonParameters>]
```

## DESCRIPTION

{{ Fill in the Description }}

## EXAMPLES

### EXAMPLE 1

```powershell
Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
Register-Passkey -UserId 'AdeleV@contoso.com' -DisplayName 'YubiKey 5 Nano'
```

### EXAMPLE 2

```powershell
Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
Register-Passkey -UserId 'AdeleV@contoso.com' -DisplayName 'YubiKey 5 Nano' -ChallengeTimeout (New-TimeSpan -Minutes 10)
```

### EXAMPLE 3

```powershell
Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
Get-PasskeyRegistrationOptions -UserId 'AdeleV@contoso.com' | New-Passkey -DisplayName 'YubiKey 5 Nano' | Register-Passkey -UserId 'AdeleV@contoso.com'
```

## PARAMETERS

### -ChallengeTimeout
Overrides the timeout of the server-generated challenge returned in the request.
The default value is 5 minutes, with the accepted range being between 5 minutes and 30 days.

```yaml
Type: TimeSpan
Parameter Sets: New
Aliases:

Required: False
Position: Named
Default value: (New-TimeSpan -Minutes 5)
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayName
Custom name given to the registered passkey.

```yaml
Type: String
Parameter Sets: New
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
Type: MicrosoftGraphWebauthnAttestationResponse
Parameter Sets: Existing
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -UserId
The unique identifier of user.

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

### Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod
## NOTES
More info at https://learn.microsoft.com/en-us/graph/api/authentication-post-fido2methods

## RELATED LINKS
