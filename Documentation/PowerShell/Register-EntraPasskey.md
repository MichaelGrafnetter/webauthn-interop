---
external help file: DSInternals.Passkeys.Entra.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Register-EntraPasskey.md
schema: 2.0.0
---

# Register-EntraPasskey

## SYNOPSIS
Registers a new passkey in Microsoft Entra ID.

## SYNTAX

### New (Default)
```
Register-EntraPasskey -UserId <String> -DisplayName <String> [<CommonParameters>]
```

### Existing
```
Register-EntraPasskey -UserId <String> -Passkey <AttestationPublicKeyCredential> -DisplayName <String>
 [<CommonParameters>]
```

## DESCRIPTION
Registers a new passkey for the specified user in Microsoft Entra ID.

When called without the -Passkey parameter, this cmdlet performs the full registration flow: it requests a challenge from Entra ID, drives the local authenticator (which prompts the system passkey UI), and submits the attestation to complete enrollment.
When called with -Passkey, it submits a previously produced attestation, which is useful when the credential ceremony was run separately (e.g.
via New-Passkey in a pipeline).

Requires an active Microsoft Graph connection (Connect-MgGraph) with the UserAuthMethod-Passkey.ReadWrite.All scope (least privileged) or UserAuthenticationMethod.ReadWrite.All.
Registering a passkey on behalf of another user additionally requires the Authentication Administrator or Privileged Authentication Administrator role.

The server-issued challenge has a fixed validity of 5 minutes, which cannot be changed.

## EXAMPLES

### EXAMPLE 1
```powershell
Connect-MgGraph -Scopes 'UserAuthMethod-Passkey.ReadWrite.All'
Register-EntraPasskey -UserId 'AdeleV@contoso.com' -DisplayName 'YubiKey 5 Nano'
```

Performs the full registration ceremony in one step: fetches creation options, prompts the local authenticator, and submits the attestation to Entra ID with the given display name.

### EXAMPLE 2
```powershell
Connect-MgGraph -Scopes 'UserAuthMethod-Passkey.ReadWrite.All'
Invoke-MgCreationUserAuthenticationFido2MethodOption -UserId 'AdeleV@contoso.com' | New-Passkey | Register-EntraPasskey -UserId 'AdeleV@contoso.com' -DisplayName 'YubiKey 5 Nano'
```

Splits the registration into explicit pipeline stages: fetch options, create the credential locally, and submit the attestation.
Equivalent to the single-step form but lets the caller inspect intermediate values.

## PARAMETERS

### -DisplayName
Custom name given to the registered passkey.

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

### -Passkey
The attestation credential produced by the local WebAuthn authenticator (e.g.
via New-Passkey).
Wrapped into a Microsoft Graph attestation response before being submitted.

```yaml
Type: AttestationPublicKeyCredential
Parameter Sets: Existing
Aliases: Attestation

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
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
The FIDO2 authentication method policy in Microsoft Entra ID must have the "Allow self-service setup" option enabled.
This is a documented limitation of the underlying Microsoft Graph API.

## RELATED LINKS

[New-Passkey](New-Passkey.md)

[Create fido2AuthenticationMethod](https://learn.microsoft.com/en-us/graph/api/authentication-post-fido2methods)
