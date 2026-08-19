---
external help file: DSInternals.Passkeys.Core.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Test-PasskeyWindowsHelloForBusiness.md
schema: 2.0.0
---

# Test-PasskeyWindowsHelloForBusiness

## SYNOPSIS
Tests a Windows Hello for Business (WHfB) NGC-based passkey registered for Microsoft Entra ID sign-in.

## SYNTAX

```
Test-PasskeyWindowsHelloForBusiness [[-Challenge] <Object>] [[-SignatureCounter] <UInt32>]
 [-BrowserInPrivateMode] [<CommonParameters>]
```

## DESCRIPTION
Locally signs a WebAuthn assertion for login.microsoft.com using the on-device Windows Hello for Business key,
without going through the Windows WebAuthn UI.
The corresponding NGC (Next Generation Credentials) private key
is located in the Microsoft Passport Key Storage Provider and matched against the platform credential(s)
registered for login.microsoft.com by comparing public key hashes.

Unlike Test-Passkey, this cmdlet does not invoke the native WebAuthNAuthenticatorGetAssertion API and therefore
does not show any authenticator selection or consent UI.
Windows Hello is expected to already be unlocked.

## EXAMPLES

### EXAMPLE 1
```powershell
Test-PasskeyWindowsHelloForBusiness
```

Tests all Windows Hello for Business passkeys registered for login.microsoft.com with a random challenge.

### EXAMPLE 2
```powershell
$challenge = New-PasskeyRandomChallenge -Length 32
Test-PasskeyWindowsHelloForBusiness -Challenge $challenge
```

Tests the passkey(s) with a specific challenge.

## PARAMETERS

### -BrowserInPrivateMode
Indicates whether the request originates from a browser running in private/incognito mode.
Passed through when enumerating registered platform credentials,
so that no information about the operation is written to the Windows event log.

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

### -Challenge
The challenge bytes to be signed.
Accepts either a byte array or a Base64Url encoded string.
If not provided, a random challenge will be generated.

```yaml
Type: Object
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
Default value: (New-PasskeyRandomChallenge -Length 32)
Accept pipeline input: False
Accept wildcard characters: False
```

### -SignatureCounter
The signature counter value to embed in the authenticator data.
Defaults to 0.

```yaml
Type: UInt32
Parameter Sets: (All)
Aliases:

Required: False
Position: 2
Default value: 0
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### DSInternals.Win32.WebAuthn.AssertionPublicKeyCredential
## NOTES

## RELATED LINKS

[Get-PasskeyWindowsHello](Get-PasskeyWindowsHello.md)

[Test-Passkey](Test-Passkey.md)
