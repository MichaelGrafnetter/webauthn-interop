---
external help file: DSInternals.Passkeys.Core.psm1-help.xml
Module Name: DSInternals.Passkeys
online version:
schema: 2.0.0
---

# Get-PasskeyCreationOptions

## SYNOPSIS
Builds a PublicKeyCredentialCreationOptions object for use with New-Passkey.

## SYNTAX

```
Get-PasskeyCreationOptions [-RelyingPartyId] <String> [-RelyingPartyName] <String> [-UserName] <String>
 [[-UserDisplayName] <String>] [[-UserId] <Object>] [[-Challenge] <Object>] [[-Algorithm] <Algorithm[]>]
 [[-UserVerification] <UserVerificationRequirement>] [[-AuthenticatorAttachment] <AuthenticatorAttachment>]
 [[-ResidentKey] <ResidentKeyRequirement>] [[-Attestation] <AttestationConveyancePreference>]
 [[-Timeout] <TimeSpan>] [[-Hint] <PublicKeyCredentialHint>] [<CommonParameters>]
```

## DESCRIPTION
Constructs a WebAuthn PublicKeyCredentialCreationOptions structure locally, without contacting any relying party.
The resulting object can be piped directly to New-Passkey to drive the local authenticator and produce a new credential.

This is useful for testing scenarios, custom relying parties, or when interacting with services that don't expose a registration-options endpoint.

## EXAMPLES

### EXAMPLE 1
```powershell
Get-PasskeyCreationOptions -RelyingPartyId 'example.com' -RelyingPartyName 'Example' -UserName 'john@example.com' -UserDisplayName 'John Doe'
```

Builds creation options for a new passkey using mostly default settings.

### EXAMPLE 2
```powershell
Get-PasskeyCreationOptions -RelyingPartyId 'example.com' -RelyingPartyName 'Example' -UserName 'john@example.com' | New-Passkey
```

Builds creation options and immediately creates a new passkey on the local authenticator.

### EXAMPLE 3
```powershell
Get-PasskeyCreationOptions -RelyingPartyId 'example.com' -RelyingPartyName 'Example' -UserName 'john@example.com' -Hint SecurityKey -ResidentKey Required -UserVerification Required
```

Builds creation options targeting a security key with required user verification and a discoverable credential.

## PARAMETERS

### -Algorithm
The list of COSE algorithms acceptable to the relying party, in order of preference.
Defaults to ES256 and RS256.

```yaml
Type: Algorithm[]
Parameter Sets: (All)
Aliases: Algorithms, PubKeyCredParams
Accepted values: RS1, RS512, RS384, RS256, ES256K, PS512, PS384, PS256, ES512, ES384, EdDSA, ES256

Required: False
Position: 7
Default value: @([DSInternals.Win32.WebAuthn.COSE.Algorithm]::ES256, [DSInternals.Win32.WebAuthn.COSE.Algorithm]::RS256)
Accept pipeline input: False
Accept wildcard characters: False
```

### -Attestation
Specifies the attestation conveyance preference.

```yaml
Type: AttestationConveyancePreference
Parameter Sets: (All)
Aliases:
Accepted values: Any, None, Indirect, Direct, Enterprise

Required: False
Position: 11
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AuthenticatorAttachment
Specifies the authenticator attachment type.

```yaml
Type: AuthenticatorAttachment
Parameter Sets: (All)
Aliases:
Accepted values: Any, Platform, CrossPlatform, CrossPlatformU2F

Required: False
Position: 9
Default value: Any
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
Position: 6
Default value: (New-PasskeyRandomChallenge -Length 32)
Accept pipeline input: False
Accept wildcard characters: False
```

### -Hint
An optional hint to the client about which authenticator type to use (e.g., SecurityKey, ClientDevice, Hybrid).

```yaml
Type: PublicKeyCredentialHint
Parameter Sets: (All)
Aliases: AuthenticatorType, CredentialHint, PublicKeyCredentialHint
Accepted values: None, SecurityKey, ClientDevice, Hybrid

Required: False
Position: 13
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

### -RelyingPartyName
The human-readable name of the relying party (e.g., 'Contoso').

```yaml
Type: String
Parameter Sets: (All)
Aliases: RpName

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResidentKey
Specifies the resident key (discoverable credential) requirement.
Defaults to Required for passkeys.

```yaml
Type: ResidentKeyRequirement
Parameter Sets: (All)
Aliases:
Accepted values: Discouraged, Preferred, Required

Required: False
Position: 10
Default value: Required
Accept pipeline input: False
Accept wildcard characters: False
```

### -Timeout
The timeout for the credential creation operation.

```yaml
Type: TimeSpan
Parameter Sets: (All)
Aliases:

Required: False
Position: 12
Default value: (New-TimeSpan -Minutes 2)
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserDisplayName
The friendly name of the user (e.g., 'John Doe').
If not provided, the UserName is used.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: 4
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserId
The user handle, an opaque identifier for the user.
Accepts either a byte array or a Base64Url encoded string.
If not provided, a random 32-byte identifier is generated.

```yaml
Type: Object
Parameter Sets: (All)
Aliases: User, UserHandle

Required: False
Position: 5
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UserName
The user account name, typically an email address or username (e.g., 'john@contoso.com').

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 3
Default value: None
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
Position: 8
Default value: Preferred
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions
## NOTES

## RELATED LINKS

[New-Passkey](New-Passkey.md)

[New-PasskeyRandomChallenge](New-PasskeyRandomChallenge.md)

[Test-Passkey](Test-Passkey.md)

