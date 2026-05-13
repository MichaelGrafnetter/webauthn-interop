---
external help file: DSInternals.Passkeys.Core.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/New-PasskeyRandomChallenge.md
schema: 2.0.0
---

# New-PasskeyRandomChallenge

## SYNOPSIS
Generates a random challenge to be used by WebAuthn.

## SYNTAX

```
New-PasskeyRandomChallenge [[-Length] <Int32>] [<CommonParameters>]
```

## DESCRIPTION
Returns a cryptographically random byte array of the requested length, suitable for use as a WebAuthn challenge during credential creation or assertion.

## EXAMPLES

### EXAMPLE 1
```powershell
New-PasskeyRandomChallenge -Length 64
```

Generates a random 64-byte challenge.

### EXAMPLE 2
```powershell
$challenge = New-PasskeyRandomChallenge
Test-Passkey -RelyingPartyId 'login.microsoft.com' -Challenge $challenge
```

Generates a default 32-byte challenge and uses it for a passkey assertion.

## PARAMETERS

### -Length
The length of the challenge in bytes.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
Default value: 32
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### System.Byte[]
## NOTES

## RELATED LINKS

[New-Passkey](New-Passkey.md)

[Test-Passkey](Test-Passkey.md)

[Get-PasskeyCreationOptions](Get-PasskeyCreationOptions.md)

