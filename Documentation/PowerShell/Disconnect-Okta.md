---
external help file: DSInternals.Passkeys.Okta.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Disconnect-Okta.md
schema: 2.0.0
---

# Disconnect-Okta

## SYNOPSIS
Revokes Okta access token.

## SYNTAX

```
Disconnect-Okta [<CommonParameters>]
```

## DESCRIPTION
Revokes the Okta access token cached from the call to Connect-Okta and clears it from the session.
For OAuth-issued Bearer tokens, this calls the /oauth2/v1/revoke endpoint using the same client authentication method that was used to obtain the token (client_assertion or client_secret).
For static SSWS API tokens, the cached token is simply discarded from the session because Okta does not expose a revoke endpoint for static tokens; revocation for those is managed in the Okta admin console.

If no token is cached, this cmdlet is a no-op.

## EXAMPLES

### EXAMPLE 1
```powershell
Disconnect-Okta
```

Revokes the cached OAuth access token (or clears the cached SSWS token) and removes any associated revocation state from the session.

## PARAMETERS

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS

[Connect-Okta](Connect-Okta.md)

[Register-OktaPasskey](Register-OktaPasskey.md)

[Revoke Tokens | Okta Developer](https://developer.okta.com/docs/guides/revoke-tokens/main/)

