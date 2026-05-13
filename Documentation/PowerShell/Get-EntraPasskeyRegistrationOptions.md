---
external help file: DSInternals.Passkeys.Entra.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Get-EntraPasskeyRegistrationOptions.md
schema: 2.0.0
---

# Get-EntraPasskeyRegistrationOptions

## SYNOPSIS
Retrieves creation options required to generate and register a Microsoft Entra ID compatible passkey.

## SYNTAX

```
Get-EntraPasskeyRegistrationOptions [-UserId] <String> [[-ChallengeTimeout] <TimeSpan>] [<CommonParameters>]
```

## DESCRIPTION
Retrieves a server-issued challenge and the associated WebAuthn parameters needed to register (attest) a new passkey for the specified Microsoft Entra ID user.
The returned object can be piped to New-Passkey to drive the local authenticator and then to Register-EntraPasskey to complete enrollment.

Requires an active Microsoft Graph connection (Connect-MgGraph) with the UserAuthenticationMethod.ReadWrite.All scope.
The caller must be an administrator; self-service is not supported.

## EXAMPLES

### EXAMPLE 1
```powershell
Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
Get-EntraPasskeyRegistrationOptions -UserId 'AdeleV@contoso.com'
```

Fetches default creation options for the specified user.

### EXAMPLE 2
```powershell
Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
Get-EntraPasskeyRegistrationOptions -UserId 'AdeleV@contoso.com' -ChallengeTimeout (New-TimeSpan -Minutes 10)
```

Fetches creation options with an extended 10-minute challenge timeout, useful when the user needs more time to complete the authenticator ceremony.

### EXAMPLE 3
```powershell
Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
Get-EntraPasskeyRegistrationOptions -UserId 'AdeleV@contoso.com' | New-Passkey | Register-EntraPasskey -UserId 'AdeleV@contoso.com' -DisplayName 'YubiKey 5 Nano'
```

Performs end-to-end passkey registration in Microsoft Entra ID in a single pipeline.

## PARAMETERS

### -ChallengeTimeout
Overrides the timeout of the server-generated challenge returned in the request.
The default value is 5 minutes, with the accepted range being between 5 minutes and 30 days.

```yaml
Type: TimeSpan
Parameter Sets: (All)
Aliases: Timeout

Required: False
Position: 2
Default value: (New-TimeSpan -Minutes 5)
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserId
The unique identifier of the user.
Either the object id (GUID) or UPN.

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions
## NOTES
Self-service operations aren't supported for Entra ID.

## RELATED LINKS

[Register-EntraPasskey](Register-EntraPasskey.md)

[New-Passkey](New-Passkey.md)

[fido2AuthenticationMethod: creationOptions](https://learn.microsoft.com/en-us/graph/api/fido2authenticationmethod-creationoptions)

