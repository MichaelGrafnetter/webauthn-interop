---
external help file: DSInternals.Passkeys.Core.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Get-PasskeyWindowsHello.md
schema: 2.0.0
---

# Get-PasskeyWindowsHello

## SYNOPSIS
Gets the list of platform credentials (passkeys) stored on the system.

## SYNTAX

```
Get-PasskeyWindowsHello [[-RelyingPartyId] <String>] [-BrowserInPrivateMode] [<CommonParameters>]
```

## DESCRIPTION
Retrieves the list of credentials stored on platform authenticators (such as Windows Hello).
This includes information about credential IDs, relying party information, user information,
and whether credentials are removable or backed up.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PasskeyWindowsHello
```

Lists all platform credentials.

### EXAMPLE 2
```powershell
Get-PasskeyWindowsHello -RelyingPartyId 'login.microsoft.com'
```

Lists credentials for a specific relying party.

## PARAMETERS

### -BrowserInPrivateMode
Indicates whether the request originates from a browser running in private/incognito mode.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: PrivateMode, Private

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -RelyingPartyId
Optional relying party ID to filter credentials.
If not specified, all credentials are returned.

```yaml
Type: String
Parameter Sets: (All)
Aliases: RpId

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### DSInternals.Win32.WebAuthn.CredentialDetails
## NOTES

## RELATED LINKS

[Remove-PasskeyWindowsHello](Remove-PasskeyWindowsHello.md)

[Test-Passkey](Test-Passkey.md)

[Get-PasskeyAuthenticator](Get-PasskeyAuthenticator.md)

