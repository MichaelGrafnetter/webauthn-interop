---
external help file: DSInternals.Passkeys.Core.psm1-help.xml
Module Name: DSInternals.Passkeys
online version: https://github.com/MichaelGrafnetter/webauthn-interop/tree/main/Documentation/PowerShell/Test-Passkey.md
schema: 2.0.0
---

# Test-Passkey

## SYNOPSIS
Tests a passkey by performing an authentication assertion.

## SYNTAX

```
Test-Passkey [-RelyingPartyId] <String> [[-Challenge] <Object>]
 [[-UserVerification] <UserVerificationRequirement>] [[-AuthenticatorAttachment] <AuthenticatorAttachment>]
 [[-Timeout] <TimeSpan>] [[-CredentialId] <Object>] [[-Hint] <PublicKeyCredentialHint>]
 [-BrowserInPrivateMode] [<CommonParameters>]
```

## DESCRIPTION
Performs a WebAuthn authentication assertion to test a passkey credential.
This triggers the authenticator to sign a challenge,
verifying that the passkey is working correctly.

## EXAMPLES

### EXAMPLE 1
```powershell
Test-Passkey -RelyingPartyId 'login.microsoft.com'
```

Tests any passkey registered for login.microsoft.com with a random challenge.

### EXAMPLE 2
```powershell
$challenge = Get-PasskeyRandomChallenge -Length 32
Test-Passkey -RelyingPartyId 'login.microsoft.com' -Challenge $challenge
```

Tests any passkey registered for login.microsoft.com with a specific challenge.

### EXAMPLE 3
```powershell
$credential = Get-PasskeyWindowsHello | Select-Object -First 1
Test-Passkey -RelyingPartyId $credential.RelyingPartyInformation.Id -CredentialId $credential.CredentialId
```

Tests a specific platform credential.

### EXAMPLE 4
```powershell
Test-Passkey -RelyingPartyId 'login.microsoft.com' -Hint SecurityKey
```

Tests a passkey with a hint that a security key should be used.

## PARAMETERS

### -AuthenticatorAttachment
Specifies the authenticator attachment type.

```yaml
Type: AuthenticatorAttachment
Parameter Sets: (All)
Aliases:
Accepted values: Any, Platform, CrossPlatform, CrossPlatformU2F

Required: False
Position: 4
Default value: Any
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -Challenge
The challenge bytes to be signed.
Accepts either a byte array or a Base64Url encoded string.
If not provided, a random challenge will be generated.

```yaml
Type: Object
Parameter Sets: (All)
Aliases:

Required: False
Position: 2
Default value: (New-PasskeyRandomChallenge -Length 32)
Accept pipeline input: False
Accept wildcard characters: False
```

### -CredentialId
An optional credential ID to test a specific credential.
Accepts either a byte array or a Base64Url encoded string.

```yaml
Type: Object
Parameter Sets: (All)
Aliases:

Required: False
Position: 6
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Hint
An optional hint to the client about which credential source to use (e.g., SecurityKey, ClientDevice, Hybrid).

```yaml
Type: PublicKeyCredentialHint
Parameter Sets: (All)
Aliases: AuthenticatorType, CredentialHint, PublicKeyCredentialHint
Accepted values: None, SecurityKey, ClientDevice, Hybrid

Required: False
Position: 7
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RelyingPartyId
The relying party identifier (e.g., 'login.microsoft.com').

```yaml
Type: String
Parameter Sets: (All)
Aliases: RpId, RelyingParty

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Timeout
The timeout for the operation.

```yaml
Type: TimeSpan
Parameter Sets: (All)
Aliases:

Required: False
Position: 5
Default value: (New-TimeSpan -Minutes 2)
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserVerification
Specifies the user verification requirement.

```yaml
Type: UserVerificationRequirement
Parameter Sets: (All)
Aliases:
Accepted values: Any, Required, Preferred, Discouraged

Required: False
Position: 3
Default value: Preferred
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

[New-PasskeyRandomChallenge](New-PasskeyRandomChallenge.md)

