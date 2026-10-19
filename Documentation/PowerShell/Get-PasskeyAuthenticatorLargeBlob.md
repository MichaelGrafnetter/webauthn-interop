---
external help file: DSInternals.Passkeys.Core.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Get-PasskeyAuthenticatorLargeBlob.md
schema: 2.0.0
---

# Get-PasskeyAuthenticatorLargeBlob

## SYNOPSIS
Reads the complete serialized large-blob store.

## SYNTAX

```
Get-PasskeyAuthenticatorLargeBlob [[-Session] <AuthenticatorManagementSession>] [<CommonParameters>]
```

## DESCRIPTION
Returns the entire raw store as one byte-array object. It does not extract, decrypt, repair, or return individual passkey blobs.

## EXAMPLES

## PARAMETERS

### -Session
An existing authenticator session. If omitted, a session is opened automatically and closed after the command.

```yaml
Type: AuthenticatorManagementSession
Parameter Sets: (All)
Aliases:

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

### System.Byte[]
## NOTES

## RELATED LINKS
