# Needed for the [Microsoft.Graph.PowerShell.Models.IMicrosoftGraphWebauthnCredentialCreationOptions] type accepted by New-Passkey
Import-Module -Name Microsoft.Graph.Identity.SignIns -ErrorAction Stop

<#
.SYNOPSIS
Creates a new WebAuthn credential by driving the local authenticator.

.DESCRIPTION
Calls the Windows WebAuthn API to make a credential according to the provided PublicKeyCredentialCreationOptions, triggering the system passkey UI. Returns the resulting AttestationPublicKeyCredential, which can be wrapped into a provider-specific attestation response and submitted to Microsoft Entra ID, Okta, or any other relying party.

.PARAMETER Options
The WebAuthn public key credential creation options. Can be built locally by Get-PasskeyCreationOptions.

.PARAMETER HostName
Optional host name used to derive the WebAuthn origin when the server-issued options omit the relying party identifier.

.PARAMETER GraphOptions
The Microsoft Graph credential creation options. Returned by the Invoke-MgCreationUserAuthenticationFido2MethodOption cmdlet from the Microsoft.Graph.Identity.SignIns module.

.PARAMETER OktaOptions
The Okta-specific credential creation options. Returned by Get-OktaPasskeyRegistrationOptions.

.EXAMPLE
$credential = Invoke-MgCreationUserAuthenticationFido2MethodOption -UserId 'AdeleV@contoso.com' | New-Passkey

Retrieves creation options from Microsoft Entra ID and creates a new passkey on the local authenticator without registering it.

.EXAMPLE
Invoke-MgCreationUserAuthenticationFido2MethodOption -UserId 'AdeleV@contoso.com' | New-Passkey | Register-EntraPasskey -UserId 'AdeleV@contoso.com' -DisplayName 'YubiKey 5 Nano'

Performs end-to-end passkey registration in Microsoft Entra ID in a single pipeline.

.EXAMPLE
Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 | New-Passkey | Register-OktaPasskey

Performs end-to-end passkey registration in Okta in a single pipeline.

.LINK
Get-PasskeyCreationOptions

.LINK
Get-OktaPasskeyRegistrationOptions

.LINK
Register-EntraPasskey

.LINK
Register-OktaPasskey

.LINK
Test-Passkey

.LINK
https://learn.microsoft.com/en-us/graph/api/fido2authenticationmethod-creationoptions

#>
function New-Passkey
{
    [OutputType([DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential], ParameterSetName = ('Default', 'MicrosoftGraph'))]
    [OutputType([DSInternals.Win32.WebAuthn.Okta.OktaWebauthnAttestationResponse], ParameterSetName = 'Okta')]
    [CmdletBinding(DefaultParameterSetName = 'Default')]
    param(
        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ParameterSetName = 'Default')]
        [DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions]
        $Options,

        [Parameter(Mandatory = $false, ParameterSetName = 'Default')]
        [ValidateNotNullOrEmpty()]
        [string]
        $HostName,

        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ParameterSetName = 'MicrosoftGraph')]
        [Alias('CreationOptions')]
        [Microsoft.Graph.PowerShell.Models.IMicrosoftGraphWebauthnCredentialCreationOptions]
        $GraphOptions,

        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ParameterSetName = 'Okta')]
        [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnCredentialCreationOptions]
        $OktaOptions
    )

    process {
        try {
            if ($PSCmdlet.ParameterSetName -eq 'MicrosoftGraph') {
                if ($null -eq $GraphOptions.PublicKey) {
                    throw 'The Microsoft Graph credential creation options do not contain WebAuthn public key options.'
                }

                # Round-trip through JSON to convert the Graph SDK model into the WebAuthn interop model
                $Options = [DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions]::FromJson($GraphOptions.PublicKey.ToJsonString())

                if ($null -eq $Options) {
                    throw 'Unable to parse the WebAuthn public key options returned by Microsoft Graph.'
                }
            }

            # Call the WebAuthn API to create a new credential, which will trigger the Windows passkey UI.
            # The `hostName` argument derives the WebAuthn origin and stands in for the rpId when the
            # server-issued options omit it. For Okta, the tenant host is carried on the options object;
            # for the Default path, the caller can supply it via -HostName.
            [DSInternals.Win32.WebAuthn.WebAuthnApi] $api = [DSInternals.Win32.WebAuthn.WebAuthnApi]::new()
            [DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential] $credential =
                if ($PSCmdlet.ParameterSetName -eq 'Okta') {
                    $api.AuthenticatorMakeCredential($OktaOptions.PublicKeyOptions, $OktaOptions.Tenant)
                } else {
                    $api.AuthenticatorMakeCredential($Options, $HostName)
                }

            if ($PSCmdlet.ParameterSetName -eq 'Okta') {
                # Wrap the credential into an Okta-specific attestation response object
                return [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnAttestationResponse]::new(
                    $credential, $OktaOptions.PublicKeyOptions.User.Id, $OktaOptions.FactorId)
            }
            else {
                return $credential
            }
        }
        catch {
            $targetObject = switch ($PSCmdlet.ParameterSetName) {
                'Okta' { $OktaOptions }
                'MicrosoftGraph' { $GraphOptions }
                default { $Options }
            }
            $PSCmdlet.ThrowTerminatingError(
                [System.Management.Automation.ErrorRecord]::new(
                    $PSItem.Exception,
                    $PSItem.Exception.Message,
                    [System.Management.Automation.ErrorCategory]::InvalidArgument,
                    $targetObject))
        }
    }
}

<#
.SYNOPSIS
Builds a PublicKeyCredentialCreationOptions object for use with New-Passkey.

.DESCRIPTION
Constructs a WebAuthn PublicKeyCredentialCreationOptions structure locally, without contacting any relying party. The resulting object can be piped directly to New-Passkey to drive the local authenticator and produce a new credential.

This is useful for testing scenarios, custom relying parties, or when interacting with services that don't expose a registration-options endpoint.

.PARAMETER RelyingPartyId
The relying party identifier (e.g., 'login.microsoft.com').

.PARAMETER RelyingPartyName
The human-readable name of the relying party (e.g., 'Contoso').

.PARAMETER UserName
The user account name, typically an email address or username (e.g., 'john@contoso.com').

.PARAMETER UserDisplayName
The friendly name of the user (e.g., 'John Doe'). If not provided, the UserName is used.

.PARAMETER UserId
The user handle, an opaque identifier for the user. Accepts either a byte array or a Base64Url encoded string. If not provided, a random 32-byte identifier is generated.

.PARAMETER Challenge
The challenge bytes to be signed. Accepts either a byte array or a Base64Url encoded string. If not provided, a random challenge will be generated.

.PARAMETER Algorithm
The list of COSE algorithms acceptable to the relying party, in order of preference. Defaults to ES256 and RS256.

.PARAMETER UserVerification
Specifies the user verification requirement.

.PARAMETER AuthenticatorAttachment
Specifies the authenticator attachment type.

.PARAMETER ResidentKey
Specifies the resident key (discoverable credential) requirement. Defaults to Required for passkeys.

.PARAMETER Attestation
Specifies the attestation conveyance preference.

.PARAMETER Timeout
The timeout for the credential creation operation.

.PARAMETER Hint
An optional hint to the client about which authenticator type to use (e.g., SecurityKey, ClientDevice, Hybrid).

.EXAMPLE
Get-PasskeyCreationOptions -RelyingPartyId 'example.com' -RelyingPartyName 'Example' -UserName 'john@example.com' -UserDisplayName 'John Doe'

Builds creation options for a new passkey using mostly default settings.

.EXAMPLE
Get-PasskeyCreationOptions -RelyingPartyId 'example.com' -RelyingPartyName 'Example' -UserName 'john@example.com' | New-Passkey

Builds creation options and immediately creates a new passkey on the local authenticator.

.EXAMPLE
Get-PasskeyCreationOptions -RelyingPartyId 'example.com' -RelyingPartyName 'Example' -UserName 'john@example.com' -Hint SecurityKey -ResidentKey Required -UserVerification Required

Builds creation options targeting a security key with required user verification and a discoverable credential.

.LINK
New-Passkey

.LINK
New-PasskeyRandomChallenge

.LINK
Test-Passkey

#>
function Get-PasskeyCreationOptions
{
    [OutputType([DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions])]
    [CmdletBinding()]
    param(
        [Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [Alias('RelyingParty')]
        [Alias('RpId')]
        [string] $RelyingPartyId,

        [Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [Alias('RpName')]
        [string] $RelyingPartyName,

        [Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string] $UserName,

        [Parameter(Mandatory = $false)]
        [string] $UserDisplayName,

        [Parameter(Mandatory = $false)]
        [Alias('User', 'UserHandle')]
        [object] $UserId,

        [Parameter(Mandatory = $false)]
        [object] $Challenge = (New-PasskeyRandomChallenge -Length 32),

        [Parameter(Mandatory = $false)]
        [ValidateNotNullOrEmpty()]
        [Alias('Algorithms', 'PubKeyCredParams')]
        [DSInternals.Win32.WebAuthn.COSE.Algorithm[]] $Algorithm = @([DSInternals.Win32.WebAuthn.COSE.Algorithm]::ES256, [DSInternals.Win32.WebAuthn.COSE.Algorithm]::RS256),

        [Parameter(Mandatory = $false)]
        [DSInternals.Win32.WebAuthn.UserVerificationRequirement] $UserVerification = [DSInternals.Win32.WebAuthn.UserVerificationRequirement]::Preferred,

        [Parameter(Mandatory = $false)]
        [DSInternals.Win32.WebAuthn.AuthenticatorAttachment] $AuthenticatorAttachment = [DSInternals.Win32.WebAuthn.AuthenticatorAttachment]::Any,

        [Parameter(Mandatory = $false)]
        [DSInternals.Win32.WebAuthn.ResidentKeyRequirement] $ResidentKey = [DSInternals.Win32.WebAuthn.ResidentKeyRequirement]::Required,

        [Parameter(Mandatory = $false)]
        [DSInternals.Win32.WebAuthn.AttestationConveyancePreference] $Attestation = [DSInternals.Win32.WebAuthn.AttestationConveyancePreference]::None,

        [Parameter(Mandatory = $false)]
        [ValidateScript({
            if ($PSItem -is [TimeSpan]) {
                [timespan] $min = New-TimeSpan -Seconds 1
                [timespan] $max = New-TimeSpan -Minutes 10
                return $PSItem -ge $min -and $PSItem -le $max
            }
            else {
                throw "Parameter must be a TimeSpan object."
            }
        })]
        [timespan] $Timeout = (New-TimeSpan -Minutes 2),

        [Parameter(Mandatory = $false)]
        [Alias("AuthenticatorType", "CredentialHint", "PublicKeyCredentialHint")]
        [DSInternals.Win32.WebAuthn.PublicKeyCredentialHint] $Hint = [DSInternals.Win32.WebAuthn.PublicKeyCredentialHint]::None
    )

    try {
        # Convert Challenge parameter (accepts byte[] or Base64Url string)
        [byte[]] $challengeBytes = ConvertFrom-Base64UrlParameter -InputObject $Challenge

        # Convert UserId parameter (accepts byte[] or Base64Url string); generate a random user handle if not provided
        [byte[]] $userIdBytes = ConvertFrom-Base64UrlParameter -InputObject $UserId
        if ($null -eq $userIdBytes -or $userIdBytes.Length -eq 0) {
            $userIdBytes = New-PasskeyRandomChallenge -Length 32
        }

        # Fall back to UserName when no UserDisplayName is provided
        if ([string]::IsNullOrEmpty($UserDisplayName)) {
            $UserDisplayName = $UserName
        }

        [uint32] $timeoutMilliseconds = $Timeout.TotalMilliseconds

        # Build the public key credential parameters list
        [System.Collections.Generic.List[DSInternals.Win32.WebAuthn.PublicKeyCredentialParameter]] $pubKeyCredParams =
            [System.Collections.Generic.List[DSInternals.Win32.WebAuthn.PublicKeyCredentialParameter]]::new()
        foreach ($alg in $Algorithm) {
            $pubKeyCredParams.Add([DSInternals.Win32.WebAuthn.PublicKeyCredentialParameter]::new(
                $alg,
                [DSInternals.Win32.WebAuthn.Interop.ApiConstants]::PublicKeyCredentialType))
        }

        [DSInternals.Win32.WebAuthn.RelyingPartyInformation] $relyingParty = [DSInternals.Win32.WebAuthn.RelyingPartyInformation]@{
            Id = $RelyingPartyId
            Name = $RelyingPartyName
        }

        [DSInternals.Win32.WebAuthn.UserInformation] $user = [DSInternals.Win32.WebAuthn.UserInformation]@{
            Id = $userIdBytes
            Name = $UserName
            DisplayName = $UserDisplayName
        }

        [DSInternals.Win32.WebAuthn.AuthenticatorSelectionCriteria] $authenticatorSelection = [DSInternals.Win32.WebAuthn.AuthenticatorSelectionCriteria]@{
            AuthenticatorAttachment = $AuthenticatorAttachment
            UserVerificationRequirement = $UserVerification
            ResidentKey = $ResidentKey
            RequireResidentKey = ($ResidentKey -eq [DSInternals.Win32.WebAuthn.ResidentKeyRequirement]::Required)
        }

        # Convert the credential hint to its DOMString form, if any
        [string] $credentialHint = [DSInternals.Win32.WebAuthn.PublicKeyCredentialHintExtensions]::ToJsonString($Hint)
        [string[]] $credentialHints = if (-not [string]::IsNullOrEmpty($credentialHint)) { @($credentialHint) } else { $null }

        [DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions] $options = [DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions]@{
            RelyingParty = $relyingParty
            User = $user
            Challenge = $challengeBytes
            PublicKeyCredentialParameters = $pubKeyCredParams
            TimeoutMilliseconds = $timeoutMilliseconds
            AuthenticatorSelection = $authenticatorSelection
            Attestation = $Attestation
            Hints = $credentialHints
        }

        return $options
    }
    catch {
        [System.Management.Automation.ErrorRecord] $errorRecord = [System.Management.Automation.ErrorRecord]::new(
            $PSItem.Exception,
            $PSItem.Exception.Message,
            [System.Management.Automation.ErrorCategory]::InvalidArgument,
            $RelyingPartyId
        )

        $PSCmdlet.ThrowTerminatingError($errorRecord)
    }
}

<#
.SYNOPSIS
Tests a passkey by performing an authentication assertion.

.DESCRIPTION
Performs a WebAuthn authentication assertion to test a passkey credential. This triggers the authenticator to sign a challenge,
verifying that the passkey is working correctly.

.PARAMETER RelyingPartyId
The relying party identifier (e.g., 'login.microsoft.com').

.PARAMETER Challenge
The challenge bytes to be signed. Accepts either a byte array or a Base64Url encoded string.
If not provided, a random challenge will be generated.

.PARAMETER UserVerification
Specifies the user verification requirement.

.PARAMETER AuthenticatorAttachment
Specifies the authenticator attachment type.

.PARAMETER Timeout
The timeout for the operation.

.PARAMETER CredentialId
An optional credential ID to test a specific credential. Accepts either a byte array or a Base64Url encoded string.

.PARAMETER Hint
An optional hint to the client about which credential source to use (e.g., SecurityKey, ClientDevice, Hybrid).

.PARAMETER BrowserInPrivateMode
Indicates whether the request originates from a browser running in private/incognito mode.

.EXAMPLE
Test-Passkey -RelyingPartyId 'login.microsoft.com'

Tests any passkey registered for login.microsoft.com with a random challenge.

.EXAMPLE
$challenge = Get-PasskeyRandomChallenge -Length 32
Test-Passkey -RelyingPartyId 'login.microsoft.com' -Challenge $challenge

Tests any passkey registered for login.microsoft.com with a specific challenge.

.EXAMPLE
$credential = Get-PasskeyWindowsHello | Select-Object -First 1
Test-Passkey -RelyingPartyId $credential.RelyingPartyInformation.Id -CredentialId $credential.CredentialId

Tests a specific platform credential.

.EXAMPLE
Test-Passkey -RelyingPartyId 'login.microsoft.com' -Hint SecurityKey

Tests a passkey with a hint that a security key should be used.

.LINK
Get-PasskeyWindowsHello

.LINK
New-PasskeyRandomChallenge

#>
function Test-Passkey
{
    [OutputType([DSInternals.Win32.WebAuthn.AssertionPublicKeyCredential])]
    [CmdletBinding()]
    param(
        [Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [Alias('RelyingParty')]
        [Alias('RpId')]
        [string] $RelyingPartyId,

        [Parameter(Mandatory = $false)]
        [object] $Challenge = (New-PasskeyRandomChallenge -Length 32),

        [Parameter(Mandatory = $false)]
        [DSInternals.Win32.WebAuthn.UserVerificationRequirement] $UserVerification = [DSInternals.Win32.WebAuthn.UserVerificationRequirement]::Preferred,

        [Parameter(Mandatory = $false)]
        [DSInternals.Win32.WebAuthn.AuthenticatorAttachment] $AuthenticatorAttachment = [DSInternals.Win32.WebAuthn.AuthenticatorAttachment]::Any,

        [Parameter(Mandatory = $false)]
        [timespan] $Timeout = (New-TimeSpan -Minutes 2),

        [Parameter(Mandatory = $false)]
        [object] $CredentialId,

        [Parameter(Mandatory = $false)]
        [Alias("AuthenticatorType", "CredentialHint", "PublicKeyCredentialHint")]
        [DSInternals.Win32.WebAuthn.PublicKeyCredentialHint] $Hint = [DSInternals.Win32.WebAuthn.PublicKeyCredentialHint]::None,

        [Parameter(Mandatory = $false)]
        [Alias("PrivateMode", "Private")]
        [switch] $BrowserInPrivateMode
    )

    try {
        # Convert Challenge parameter (accepts byte[] or Base64Url string)
        [byte[]] $challengeBytes = ConvertFrom-Base64UrlParameter -InputObject $Challenge

        # Convert CredentialId parameter (accepts byte[] or Base64Url string)
        [byte[]] $credentialIdBytes = ConvertFrom-Base64UrlParameter -InputObject $CredentialId

        # Build the AllowCredentials list if a specific CredentialId was provided
        [DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor[]] $allowCredentials = @()

        if ($null -ne $credentialIdBytes -and $credentialIdBytes.Length -gt 0) {
            $allowCredentials += [DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor]::new($credentialIdBytes)
        }

        # Convert TimeSpan to milliseconds, while capping to [1, 10 minutes]
        [int] $timeoutMilliseconds = [Math]::Max(1, [Math]::Min(10 * 60 * 1000, $Timeout.TotalMilliseconds))

        [DSInternals.Win32.WebAuthn.WebAuthnApi] $api = [DSInternals.Win32.WebAuthn.WebAuthnApi]::new()

        [string] $credentialHint = [DSInternals.Win32.WebAuthn.PublicKeyCredentialHintExtensions]::ToJsonString($Hint)
        [string[]] $credentialHints = if (![string]::IsNullOrEmpty($credentialHint)) { @($credentialHint) } else { $null }

        [DSInternals.Win32.WebAuthn.AssertionPublicKeyCredential] $response = $api.AuthenticatorGetAssertion(
            $RelyingPartyId,
            $challengeBytes,
            $UserVerification,
            $AuthenticatorAttachment,
            $timeoutMilliseconds,
            $allowCredentials,
            $null,  # extensions
            $BrowserInPrivateMode.IsPresent,
            $null,  # linkedDevice
            $false, # autoFill
            $credentialHints,
            $null,  # authenticatorId
            $null,  # publicKeyCredentialRequestOptionsJson
            [DSInternals.Win32.WebAuthn.WindowHandle]::ForegroundWindow
        )

        return $response
    }
    catch {
        [System.Management.Automation.ErrorRecord] $errorRecord = [System.Management.Automation.ErrorRecord]::new(
            $PSItem.Exception,
            $PSItem.Exception.Message,
            [System.Management.Automation.ErrorCategory]::InvalidOperation,
            $RelyingPartyId
        )

        $PSCmdlet.ThrowTerminatingError($errorRecord)
    }
}

<#
.SYNOPSIS
Tests a Windows Hello for Business (NGC) passkey registered for Microsoft Entra ID sign-in.

.DESCRIPTION
Locally signs a WebAuthn assertion for login.microsoft.com using the on-device Windows Hello for Business key,
without going through the Windows WebAuthn UI. The corresponding NGC (Next Generation Credentials) private key
is located in the Microsoft Passport Key Storage Provider and matched against the platform credential(s)
registered for login.microsoft.com by comparing public key hashes.

Unlike Test-Passkey, this cmdlet does not invoke the native WebAuthNAuthenticatorGetAssertion API and therefore
does not show any authenticator selection or consent UI. Windows Hello is expected to already be unlocked.

.PARAMETER Challenge
The challenge bytes to be signed. Accepts either a byte array or a Base64Url encoded string.
If not provided, a random challenge will be generated.

.PARAMETER SignatureCounter
The signature counter value to embed in the authenticator data. Defaults to 0.

.PARAMETER BrowserInPrivateMode
Indicates whether the request originates from a browser running in private/incognito mode. Passed through when
enumerating registered platform credentials, so that no information about the operation is written to the
Windows event log.

.EXAMPLE
Test-PasskeyWindowsHelloForBusiness

Tests all Windows Hello for Business passkeys registered for login.microsoft.com with a random challenge.

.EXAMPLE
$challenge = New-PasskeyRandomChallenge -Length 32
Test-PasskeyWindowsHelloForBusiness -Challenge $challenge

Tests the passkey(s) with a specific challenge.

.LINK
Test-Passkey

.LINK
New-PasskeyRandomChallenge

#>
function Test-PasskeyWindowsHelloForBusiness
{
    [OutputType([DSInternals.Win32.WebAuthn.AssertionPublicKeyCredential])]
    [CmdletBinding()]
    param(
        [Parameter(Mandatory = $false)]
        [object] $Challenge = (New-PasskeyRandomChallenge -Length 32),

        [Parameter(Mandatory = $false)]
        [uint32] $SignatureCounter = 0,

        [Parameter(Mandatory = $false)]
        [Alias("PrivateMode", "Private")]
        [switch] $BrowserInPrivateMode
    )

    try {
        # Convert Challenge parameter (accepts byte[] or Base64Url string)
        [byte[]] $challengeBytes = ConvertFrom-Base64UrlParameter -InputObject $Challenge

        [DSInternals.Win32.WebAuthn.AssertionPublicKeyCredential[]] $assertions = [DSInternals.Win32.WebAuthn.Cryptography.WindowsHelloForBusinessSigner]::GetAssertions(
            $challengeBytes,
            $SignatureCounter,
            $BrowserInPrivateMode.IsPresent
        )

        foreach ($assertion in $assertions) {
            Write-Output -InputObject $assertion
        }
    }
    catch {
        [System.Management.Automation.ErrorRecord] $errorRecord = [System.Management.Automation.ErrorRecord]::new(
            $PSItem.Exception,
            $PSItem.Exception.Message,
            [System.Management.Automation.ErrorCategory]::InvalidOperation,
            $null
        )

        $PSCmdlet.ThrowTerminatingError($errorRecord)
    }
}

<#
.SYNOPSIS
Gets the list of registered authenticator plugins from the Windows registry.

.DESCRIPTION
Retrieves information about third-party passkey providers (such as 1Password, Bitwarden, etc.) that are registered
as authenticator plugins in Windows. These plugins are registered under HKLM\SOFTWARE\Microsoft\FIDO.

.EXAMPLE
Get-PasskeyAuthenticatorPlugin

Lists all registered authenticator plugins.

.EXAMPLE
Get-PasskeyAuthenticatorPlugin | Where-Object Enabled -eq $true

Lists only enabled authenticator plugins.

.EXAMPLE
Get-PasskeyAuthenticatorPlugin | Select-Object -Property Name,PackageFamilyName,Enabled

Lists authenticator plugins with selected properties.

.LINK
Get-PasskeyPlatformAuthenticator

#>
function Get-PasskeyAuthenticatorPlugin
{
    [OutputType([DSInternals.Win32.WebAuthn.AuthenticatorPluginInformation])]
    [CmdletBinding()]
    param()

    try {
        [DSInternals.Win32.WebAuthn.AuthenticatorPluginInformation[]] $plugins = [DSInternals.Win32.WebAuthn.WebAuthnApi]::GetPluginAuthenticators()

        if ($null -eq $plugins -or $plugins.Count -eq 0) {
            Write-Verbose 'No authenticator plugins found.'
            return
        }

        foreach ($plugin in $plugins) {
            Write-Output $plugin
        }
    }
    catch {
        [System.Management.Automation.ErrorRecord] $errorRecord = [System.Management.Automation.ErrorRecord]::new(
            $PSItem.Exception,
            $PSItem.Exception.Message,
            [System.Management.Automation.ErrorCategory]::ReadError,
            $null
        )

        $PSCmdlet.ThrowTerminatingError($errorRecord)
    }
}

<#
.SYNOPSIS
Gets the list of available authenticators from the WebAuthn API.

.DESCRIPTION
Retrieves a list of authenticators available on the system using the Windows WebAuthn API.
This includes information about authenticator IDs, names, logos, and lock status.

.EXAMPLE
Get-PasskeyPlatformAuthenticator

Lists all available authenticators.

.EXAMPLE
Get-PasskeyPlatformAuthenticator | Where-Object { -not $PSItem.Locked }

Lists only unlocked authenticators.

.EXAMPLE
Get-PasskeyPlatformAuthenticator | Format-Table -Prperty AuthenticatorName,Locked

Lists authenticator names and lock status in a table.

.LINK
Get-PasskeyAuthenticatorPlugin

.LINK
Get-PasskeyWindowsHello

#>
function Get-PasskeyPlatformAuthenticator
{
    [Alias('Get-PasskeyAuthenticator', 'GetPasskeyWindowsHelloAuthenticator')]
    [OutputType([DSInternals.Win32.WebAuthn.AuthenticatorDetails])]
    [CmdletBinding()]
    param()

    try {
        [DSInternals.Win32.WebAuthn.AuthenticatorDetails[]] $authenticators = [DSInternals.Win32.WebAuthn.WebAuthnApi]::GetAuthenticatorList()

        if ($null -eq $authenticators -or $authenticators.Count -eq 0) {
            Write-Verbose 'No authenticators found.'
            return
        }

        foreach ($authenticator in $authenticators) {
            Write-Output $authenticator
        }
    }
    catch {
        [System.Management.Automation.ErrorRecord] $errorRecord = [System.Management.Automation.ErrorRecord]::new(
            $PSItem.Exception,
            $PSItem.Exception.Message,
            [System.Management.Automation.ErrorCategory]::ReadError,
            $null
        )

        $PSCmdlet.ThrowTerminatingError($errorRecord)
    }
}

<#
.SYNOPSIS
Gets the list of platform credentials (passkeys) stored on the system.

.DESCRIPTION
Retrieves the list of credentials stored on platform authenticators (such as Windows Hello).
This includes information about credential IDs, relying party information, user information,
and whether credentials are removable or backed up.

.PARAMETER RelyingPartyId
Optional relying party ID to filter credentials. If not specified, all credentials are returned.

.PARAMETER BrowserInPrivateMode
Indicates whether the request originates from a browser running in private/incognito mode.

.EXAMPLE
Get-PasskeyWindowsHello

Lists all platform credentials.

.EXAMPLE
Get-PasskeyWindowsHello -RelyingPartyId 'login.microsoft.com'

Lists credentials for a specific relying party.

.LINK
Remove-PasskeyWindowsHello

.LINK
Test-Passkey

.LINK
Get-PasskeyPlatformAuthenticator

.LINK
Get-PasskeySecurityKey

#>
function Get-PasskeyWindowsHello
{
    [OutputType([DSInternals.Win32.WebAuthn.CredentialDetails])]
    [CmdletBinding()]
    param(
        [Parameter(Mandatory = $false)]
        [Alias('RpId')]
        [string] $RelyingPartyId,

        [Parameter(Mandatory = $false)]
        [Alias("PrivateMode", "Private")]
        [switch] $BrowserInPrivateMode
    )

    try {
        [DSInternals.Win32.WebAuthn.CredentialDetails[]] $credentials = [DSInternals.Win32.WebAuthn.WebAuthnApi]::GetPlatformCredentialList($RelyingPartyId, $BrowserInPrivateMode.IsPresent)

        if ($null -eq $credentials -or $credentials.Count -eq 0) {
            Write-Verbose 'No platform credentials found.'
            return
        }

        foreach ($credential in $credentials) {
            Write-Output $credential
        }
    }
    catch {
        [System.Management.Automation.ErrorRecord] $errorRecord = [System.Management.Automation.ErrorRecord]::new(
            $PSItem.Exception,
            $PSItem.Exception.Message,
            [System.Management.Automation.ErrorCategory]::ReadError,
            $RelyingPartyId
        )

        $PSCmdlet.ThrowTerminatingError($errorRecord)
    }
}

<#
.SYNOPSIS
Removes a platform credential (passkey) from the system.

.DESCRIPTION
Removes a Public Key Credential stored on a platform authenticator (such as Windows Hello).
This operation is irreversible - once deleted, the credential cannot be recovered.

.PARAMETER CredentialId
The ID of the credential to be removed. This can be obtained from Get-PasskeyWindowsHello.
Accepts either a byte array or a Base64Url encoded string.

.PARAMETER Credential
A CredentialDetails object obtained from Get-PasskeyWindowsHello.

.EXAMPLE
$cred = Get-PasskeyWindowsHello | Select-Object -First 1
Remove-PasskeyWindowsHello -CredentialId $cred.CredentialId

Removes a specific platform credential by ID.

.EXAMPLE
Get-PasskeyWindowsHello | Where-Object { $PSItem.RelyingPartyInformation.Id -eq 'example.com' } | Remove-PasskeyWindowsHello

Removes all credentials for a specific relying party using pipeline input.

.EXAMPLE
Remove-PasskeyWindowsHello -CredentialId 'dGVzdC1jcmVkZW50aWFsLWlk'

Removes a credential using a Base64Url encoded credential ID.

.NOTES
Requires Windows with WebAuthn API version 4 or later (Windows 10 2004+).
This operation requires appropriate permissions and may trigger a Windows Security prompt.

.LINK
Get-PasskeyWindowsHello

.LINK
Remove-PasskeySecurityKey

#>
function Remove-PasskeyWindowsHello
{
    [CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'High', DefaultParameterSetName = 'ByCredentialId')]
    param(
        [Parameter(Mandatory = $true, ParameterSetName = 'ByCredentialId', Position = 0)]
        [ValidateNotNull()]
        [object] $CredentialId,

        [Parameter(Mandatory = $true, ParameterSetName = 'ByCredential', ValueFromPipeline = $true)]
        [ValidateNotNull()]
        [DSInternals.Win32.WebAuthn.CredentialDetails] $InputObject
    )
    try {
        # Get the credential ID from the appropriate parameter
        [byte[]] $credId = $null

        if ($PSCmdlet.ParameterSetName -eq 'ByCredential') {
            $credId = $InputObject.CredentialId
        }
        else {
            # Convert CredentialId from string (Base64Url) to byte[] if necessary
            $credId = ConvertFrom-Base64UrlParameter -InputObject $CredentialId
        }

        # Confirm the operation
        [string] $credentialIdString = [System.Buffers.Text.Base64Url]::EncodeToString($credId)
        if ($PSCmdlet.ShouldProcess($credentialIdString, 'Remove platform credential')) {
            [DSInternals.Win32.WebAuthn.WebAuthnApi]::DeletePlatformCredential($credId)
            Write-Verbose "Successfully removed credential $credentialIdString"
        }
    }
    catch {
        Write-Error -ErrorRecord ([System.Management.Automation.ErrorRecord]::new(
            $PSItem.Exception,
            $PSItem.Exception.Message,
            [System.Management.Automation.ErrorCategory]::WriteError,
            $credId
        ))
    }
}

<#
.SYNOPSIS
Generates a random challenge to be used by WebAuthn.

.DESCRIPTION
Returns a cryptographically random byte array of the requested length, suitable for use as a WebAuthn challenge during credential creation or assertion.

.PARAMETER Length
The length of the challenge in bytes.

.EXAMPLE
New-PasskeyRandomChallenge -Length 64

Generates a random 64-byte challenge.

.EXAMPLE
$challenge = New-PasskeyRandomChallenge
Test-Passkey -RelyingPartyId 'login.microsoft.com' -Challenge $challenge

Generates a default 32-byte challenge and uses it for a passkey assertion.

.LINK
New-Passkey

.LINK
Test-Passkey

.LINK
Get-PasskeyCreationOptions

#>
function New-PasskeyRandomChallenge
{
    [CmdletBinding()]
    [OutputType([byte[]])]
    param(
        [Parameter(Mandatory = $false)]
        [ValidateRange(16, 64)]
        [int] $Length = 32
    )

    [byte[]] $challenge = [byte[]]::new($Length)
    [System.Security.Cryptography.RandomNumberGenerator] $rng = [System.Security.Cryptography.RandomNumberGenerator]::Create()
    try {
        $rng.GetBytes($challenge)
        return $challenge
    }
    finally {
        $rng.Dispose()
    }
}

<#
.SYNOPSIS
Converts a Base64Url encoded string or byte array to a byte array.

.PARAMETER InputObject
The input object to convert. Can be a Base64Url encoded string or a byte array.

.NOTES
This is a helper function used internally for parameter conversion.
#>
function ConvertFrom-Base64UrlParameter
{
    [CmdletBinding()]
    [OutputType([byte[]])]
    param(
        [Parameter(Mandatory = $false, Position = 0)]
        [object] $InputObject
    )

    if ($null -eq $InputObject) {
        return $null
    } elseif ($InputObject -is [string]) {
        # Convert from Base64Url string to byte array
        return [DSInternals.Win32.WebAuthn.Base64UrlConverter]::FromBase64UrlString($InputObject)
    } elseif ($null -ne ($InputObject -as [byte[]])) {
        # Nothing to convert
        return $InputObject
    } else {
        throw [System.ArgumentException]::new("The value must be a byte array or a Base64Url encoded string.")
    }
}

<#
.SYNOPSIS
Opens a session with a roaming authenticator for management operations.

.DESCRIPTION
Selects an authenticator once and returns an explicitly disposable session. The cmdlet does not authenticate or prompt for a PIN. Management uses private Windows APIs which may change between Windows releases.

.PARAMETER Transports
Optional transport mask used to select the authenticator.

.EXAMPLE
$session = Open-PasskeyAuthenticatorSession
try { Get-PasskeyAuthenticatorCapabilities -Session $session } finally { Close-PasskeyAuthenticatorSession $session }
#>
function Open-PasskeyAuthenticatorSession
{
    [CmdletBinding()]
    [OutputType([DSInternals.Win32.WebAuthn.AuthenticatorManagementSession])]
    param(
        [Parameter(Position = 0)]
        [DSInternals.Win32.WebAuthn.AuthenticatorTransport] $Transports = [DSInternals.Win32.WebAuthn.AuthenticatorTransport]::NoRestrictions
    )

    try {
        return [DSInternals.Win32.WebAuthn.WebAuthnApi]::OpenAuthenticatorManagementSession(
            $Transports)
    }
    catch {
        $PSCmdlet.ThrowTerminatingError([System.Management.Automation.ErrorRecord]::new(
            $PSItem.Exception, $PSItem.Exception.Message,
            [System.Management.Automation.ErrorCategory]::ResourceUnavailable, $Transports))
    }
}

<#
.SYNOPSIS
Closes an authenticator management session.

.PARAMETER Session
The session returned by Open-PasskeyAuthenticatorSession.

.EXAMPLE
Close-PasskeyAuthenticatorSession -Session $session
#>
function Close-PasskeyAuthenticatorSession
{
    [CmdletBinding()]
    param(
        [Parameter(Mandatory = $true, Position = 0, ValueFromPipeline = $true)]
        [ValidateNotNull()]
        [DSInternals.Win32.WebAuthn.AuthenticatorManagementSession] $Session
    )

    process {
        $Session.Dispose()
    }
}

<#
.SYNOPSIS
Authenticates to the selected authenticator using a PIN.

.DESCRIPTION
Authentication is explicit and consumes an authenticator PIN retry on failure.
If a command reports an uncertain outcome, dispose and reopen the session to select the authenticator and obtain fresh state before deciding whether to retry. Capability and PIN-policy reads on the same session use its last native context refresh and may be stale.

.PARAMETER Session
An existing authenticator session. If omitted, a session is opened automatically and closed after the command.

.PARAMETER Pin
The PIN as a SecureString.

.EXAMPLE
Unlock-PasskeyAuthenticatorSession -Session $session -Pin (Read-Host 'Authenticator PIN' -AsSecureString)
#>
function Unlock-PasskeyAuthenticatorSession
{
    [CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'High')]
    param(
        [Parameter(Position = 0)]
        [ValidateNotNull()]
        [DSInternals.Win32.WebAuthn.AuthenticatorManagementSession] $Session,

        [Parameter(Mandatory = $true, Position = 1)]
        [ValidateNotNull()]
        [System.Security.SecureString] $Pin
    )

    process {
        [bool] $ownsSession = -not $PSBoundParameters.ContainsKey('Session')
        if ($ownsSession) {
            $Session = Open-PasskeyAuthenticatorSession -ErrorAction Stop
        }
        try {
            if ($PSCmdlet.ShouldProcess('Selected authenticator', 'Authenticate with PIN')) {
                try {
                    $null = $Session.AuthenticatePin($Pin)
                }
                catch {
                    $PSCmdlet.ThrowTerminatingError([System.Management.Automation.ErrorRecord]::new(
                        $PSItem.Exception, $PSItem.Exception.Message,
                        [System.Management.Automation.ErrorCategory]::SecurityError, $Session))
                }
            }
        }
        finally {
            if ($ownsSession) {
                Close-PasskeyAuthenticatorSession -Session $Session -ErrorAction Stop
            }
        }
    }
}

<#
.SYNOPSIS
Gets transports supported by authenticator management on this system.

.EXAMPLE
Get-PasskeySupportedTransport
#>
function Get-PasskeySupportedTransport
{
    [CmdletBinding()]
    [OutputType([DSInternals.Win32.WebAuthn.AuthenticatorTransport])]
    param()

    try {
        return [DSInternals.Win32.WebAuthn.WebAuthnApi]::GetSupportedManagementTransports()
    }
    catch {
        $PSCmdlet.ThrowTerminatingError([System.Management.Automation.ErrorRecord]::new(
            $PSItem.Exception, $PSItem.Exception.Message,
            [System.Management.Automation.ErrorCategory]::ReadError, $null))
    }
}

<#
.SYNOPSIS
Gets a snapshot of the selected authenticator's management capabilities and state.

.DESCRIPTION
Returns a pointer-free snapshot copied from the session's last native context refresh; it does not query the device independently. After a mutation with an uncertain outcome, dispose and reopen the session to select the authenticator and obtain fresh state before deciding whether to retry. Calling this cmdlet again on the same session can return stale state.

.PARAMETER Session
An existing authenticator session. If omitted, a session is opened automatically and closed after the command.

.EXAMPLE
Get-PasskeyAuthenticatorCapabilities -Session $session
#>
function Get-PasskeyAuthenticatorCapabilities
{
    [CmdletBinding()]
    [OutputType([DSInternals.Win32.WebAuthn.AuthenticatorManagementCapabilities])]
    param(
        [Parameter(Position = 0)]
        [ValidateNotNull()]
        [DSInternals.Win32.WebAuthn.AuthenticatorManagementSession] $Session
    )

    process {
        [bool] $ownsSession = -not $PSBoundParameters.ContainsKey('Session')
        if ($ownsSession) {
            $Session = Open-PasskeyAuthenticatorSession -ErrorAction Stop
        }
        try {
            try { $Session.GetCapabilities() }
            catch {
                $PSCmdlet.ThrowTerminatingError([System.Management.Automation.ErrorRecord]::new(
                    $PSItem.Exception, $PSItem.Exception.Message,
                    [System.Management.Automation.ErrorCategory]::ReadError, $Session))
            }
        }
        finally {
            if ($ownsSession) {
                Close-PasskeyAuthenticatorSession -Session $Session -ErrorAction Stop
            }
        }
    }
}

<#
.SYNOPSIS
Gets PIN and configuration policy for the selected authenticator.

.DESCRIPTION
Returns policy copied from the session's last native context refresh; it does not query the device independently. After a mutation with an uncertain outcome, dispose and reopen the session to select the authenticator and obtain fresh state before deciding whether to retry. Calling this cmdlet again on the same session can return stale state.

.PARAMETER Session
An existing authenticator session. If omitted, a session is opened automatically and closed after the command.

.EXAMPLE
Get-PasskeyAuthenticatorPinPolicy -Session $session
#>
function Get-PasskeyAuthenticatorPinPolicy
{
    [CmdletBinding()]
    [OutputType([DSInternals.Win32.WebAuthn.AuthenticatorPinPolicy])]
    param(
        [Parameter(Position = 0)]
        [ValidateNotNull()]
        [DSInternals.Win32.WebAuthn.AuthenticatorManagementSession] $Session
    )

    process {
        [bool] $ownsSession = -not $PSBoundParameters.ContainsKey('Session')
        if ($ownsSession) {
            $Session = Open-PasskeyAuthenticatorSession -ErrorAction Stop
        }
        try {
            try { $Session.GetPinPolicy() }
            catch {
                $PSCmdlet.ThrowTerminatingError([System.Management.Automation.ErrorRecord]::new(
                    $PSItem.Exception, $PSItem.Exception.Message,
                    [System.Management.Automation.ErrorCategory]::ReadError, $Session))
            }
        }
        finally {
            if ($ownsSession) {
                Close-PasskeyAuthenticatorSession -Session $Session -ErrorAction Stop
            }
        }
    }
}

<#
.SYNOPSIS
Lists resident (discoverable) credentials on the selected authenticator.

.PARAMETER Session
An existing authenticator session. If omitted, a session is opened automatically and closed after the command.

.EXAMPLE
Get-PasskeySecurityKey -Session $session

.LINK
Get-PasskeyWindowsHello

.LINK
Remove-PasskeySecurityKey
#>
function Get-PasskeySecurityKey
{
    [CmdletBinding()]
    [OutputType([DSInternals.Win32.WebAuthn.DiscoverableCredentialDetails])]
    param(
        [Parameter(Position = 0)]
        [ValidateNotNull()]
        [DSInternals.Win32.WebAuthn.AuthenticatorManagementSession] $Session
    )

    process {
        [bool] $ownsSession = -not $PSBoundParameters.ContainsKey('Session')
        if ($ownsSession) {
            $Session = Open-PasskeyAuthenticatorSession -ErrorAction Stop
        }
        try {
            try {
                foreach ($credential in $Session.GetCredentials()) {
                    Write-Output $credential
                }
            }
            catch {
                $PSCmdlet.ThrowTerminatingError([System.Management.Automation.ErrorRecord]::new(
                    $PSItem.Exception, $PSItem.Exception.Message,
                    [System.Management.Automation.ErrorCategory]::ReadError, $Session))
            }
        }
        finally {
            if ($ownsSession) {
                Close-PasskeyAuthenticatorSession -Session $Session -ErrorAction Stop
            }
        }
    }
}

<#
.SYNOPSIS
Deletes a resident credential by its exact credential ID.

.DESCRIPTION
Deletion is destructive and requires confirmation unless -Confirm:$false is specified. Credential IDs may be byte arrays or Base64Url strings. Credential objects from Get-PasskeySecurityKey can be piped directly.
If the deletion outcome is uncertain, dispose and reopen the session before deciding whether to retry; same-session capability and policy snapshots may be stale.

.PARAMETER Session
An existing authenticator session. If omitted, a session is opened automatically and closed after the command.

.PARAMETER CredentialId
Exact credential identifier as bytes or Base64Url text.

.PARAMETER InputObject
Resident credential object to delete.

.EXAMPLE
Get-PasskeySecurityKey -Session $session | Where-Object RelyingPartyId -eq 'example.com' | Remove-PasskeySecurityKey -Session $session

.LINK
Remove-PasskeyWindowsHello

.LINK
Get-PasskeySecurityKey
#>
function Remove-PasskeySecurityKey
{
    [CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'High', DefaultParameterSetName = 'ByCredentialId')]
    param(
        [Parameter(Position = 0, ParameterSetName = 'ByCredentialId')]
        [Parameter(Position = 0, ParameterSetName = 'ByCredential')]
        [ValidateNotNull()]
        [DSInternals.Win32.WebAuthn.AuthenticatorManagementSession] $Session,

        [Parameter(Mandatory = $true, Position = 1, ParameterSetName = 'ByCredentialId')]
        [ValidateNotNull()]
        [object] $CredentialId,

        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ParameterSetName = 'ByCredential')]
        [ValidateNotNull()]
        [DSInternals.Win32.WebAuthn.DiscoverableCredentialDetails] $InputObject
    )

    begin {
        [bool] $ownsSession = -not $PSBoundParameters.ContainsKey('Session')
        if ($ownsSession) {
            $Session = Open-PasskeyAuthenticatorSession -ErrorAction Stop
        }
    }

    process {
        [bool] $completed = $false
        try {
            [byte[]] $credentialBytes = $null
            try {
                if ($PSCmdlet.ParameterSetName -eq 'ByCredential') {
                    $credentialBytes = $InputObject.CredentialId
                }
                else {
                    $credentialBytes = ConvertFrom-Base64UrlParameter -InputObject $CredentialId
                }

                if ($null -eq $credentialBytes -or $credentialBytes.Length -eq 0) {
                    throw [System.ArgumentException]::new('A non-empty credential ID is required.')
                }

                [string] $credentialIdText = [System.Buffers.Text.Base64Url]::EncodeToString($credentialBytes)
                if ($PSCmdlet.ShouldProcess($credentialIdText, 'Delete resident credential')) {
                    $Session.DeleteCredential($credentialBytes)
                }
            }
            catch {
                $PSCmdlet.ThrowTerminatingError([System.Management.Automation.ErrorRecord]::new(
                    $PSItem.Exception, $PSItem.Exception.Message,
                    [System.Management.Automation.ErrorCategory]::WriteError, $credentialBytes))
            }
            $completed = $true
        }
        finally {
            if ($ownsSession -and -not $completed) {
                Close-PasskeyAuthenticatorSession -Session $Session -ErrorAction Stop
                $ownsSession = $false
            }
        }
    }

    end {
        if ($ownsSession) {
            Close-PasskeyAuthenticatorSession -Session $Session -ErrorAction Stop
        }
    }
}

<#
.SYNOPSIS
Sets or changes the PIN for the selected authenticator.

.DESCRIPTION
The initial-PIN parameter set installs a PIN. The change-PIN parameter set changes an existing PIN. Both operations are sensitive and require confirmation. PINs must be supplied explicitly as SecureString values; no prompt is generated.
If a command reports an uncertain outcome, dispose and reopen the session to select the authenticator and obtain fresh state before deciding whether to retry. Capability and PIN-policy reads on the same session use its last native context refresh and may be stale.

.PARAMETER Session
An existing authenticator session. If omitted, a session is opened automatically and closed after the command.

.PARAMETER NewPin
New PIN as a SecureString.

.PARAMETER CurrentPin
Current PIN as a SecureString, required when changing an existing PIN.

.EXAMPLE
Set-PasskeyAuthenticatorPin -Session $session -NewPin (Read-Host 'New PIN' -AsSecureString)
#>
function Set-PasskeyAuthenticatorPin
{
    [CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'High', DefaultParameterSetName = 'Initial')]
    param(
        [Parameter(Position = 0)]
        [ValidateNotNull()]
        [DSInternals.Win32.WebAuthn.AuthenticatorManagementSession] $Session,

        [Parameter(Mandatory = $true, Position = 1, ParameterSetName = 'Initial')]
        [Parameter(Mandatory = $true, Position = 2, ParameterSetName = 'Change')]
        [ValidateNotNull()]
        [System.Security.SecureString] $NewPin,

        [Parameter(Mandatory = $true, Position = 1, ParameterSetName = 'Change')]
        [ValidateNotNull()]
        [System.Security.SecureString] $CurrentPin
    )

    process {
        [bool] $ownsSession = -not $PSBoundParameters.ContainsKey('Session')
        if ($ownsSession) {
            $Session = Open-PasskeyAuthenticatorSession -ErrorAction Stop
        }
        try {
            if ($PSCmdlet.ShouldProcess('Selected authenticator', 'Set authenticator PIN')) {
                try {
                    if ($PSCmdlet.ParameterSetName -eq 'Change') {
                        $null = $Session.ChangePin($CurrentPin, $NewPin)
                    }
                    else {
                        $null = $Session.SetPin($NewPin)
                    }
                }
                catch {
                    $PSCmdlet.ThrowTerminatingError([System.Management.Automation.ErrorRecord]::new(
                        $PSItem.Exception, $PSItem.Exception.Message,
                        [System.Management.Automation.ErrorCategory]::WriteError, $Session))
                }
            }
        }
        finally {
            if ($ownsSession) {
                Close-PasskeyAuthenticatorSession -Session $Session -ErrorAction Stop
            }
        }
    }
}

<#
.SYNOPSIS
Configures minimum PIN length and relying-party policy.

.DESCRIPTION
Changes authenticator security policy. -ForcePinChange requires the authenticator to request a PIN change at the next authentication. This operation requires explicit PIN authentication beforehand when the authenticator requires it. If the command reports an uncertain mutation outcome, do not retry based on a policy read from the same session: that session may hold stale context. Dispose and reopen it to select the authenticator and obtain fresh state first.

.PARAMETER Session
An existing authenticator session. If omitted, a session is opened automatically and closed after the command.

.PARAMETER MinimumPinLength
Requested minimum PIN length from 4 to 63 Unicode code points. Omit it to preserve the current minimum.

.PARAMETER RelyingPartyId
Optional relying-party identifiers allowed to use the minimum-length policy.

.PARAMETER ForcePinChange
Requires a PIN change at the next authentication.

.EXAMPLE
Set-PasskeyAuthenticatorPinPolicy -Session $session -MinimumPinLength 8 -RelyingPartyId 'example.com'
#>
function Set-PasskeyAuthenticatorPinPolicy
{
    [CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'High')]
    param(
        [Parameter(Position = 0)]
        [ValidateNotNull()]
        [DSInternals.Win32.WebAuthn.AuthenticatorManagementSession] $Session,

        [Parameter()]
        [ValidateRange(4, 63)]
        [Nullable[uint32]] $MinimumPinLength = $null,

        [Parameter()]
        [ValidateNotNullOrEmpty()]
        [string[]] $RelyingPartyId,

        [Parameter()]
        [switch] $ForcePinChange
    )

    process {
        [bool] $ownsSession = -not $PSBoundParameters.ContainsKey('Session')
        if ($ownsSession) {
            $Session = Open-PasskeyAuthenticatorSession -ErrorAction Stop
        }
        try {
            if ($PSCmdlet.ShouldProcess('Selected authenticator', 'Set authenticator PIN policy')) {
                try {
                    $Session.SetPinPolicy($MinimumPinLength, $ForcePinChange.IsPresent, $RelyingPartyId)
                }
                catch {
                    $PSCmdlet.ThrowTerminatingError([System.Management.Automation.ErrorRecord]::new(
                        $PSItem.Exception, $PSItem.Exception.Message,
                        [System.Management.Automation.ErrorCategory]::WriteError, $Session))
                }
            }
        }
        finally {
            if ($ownsSession) {
                Close-PasskeyAuthenticatorSession -Session $Session -ErrorAction Stop
            }
        }
    }
}

<#
.SYNOPSIS
Enables Always Require User Verification or enterprise attestation on the selected authenticator.

.DESCRIPTION
This sensitive configuration change requires confirmation and may be irreversible. Authenticate explicitly with Unlock-PasskeyAuthenticatorSession first if required. If the command reports an uncertain mutation outcome, do not retry based on capability or policy reads from the same session: it may hold stale context. Dispose and reopen it to select the authenticator and obtain fresh state first.

.PARAMETER Session
An existing authenticator session. If omitted, a session is opened automatically and closed after the command.

.PARAMETER AlwaysUv
Enables Always Require User Verification.

.PARAMETER EnableEnterpriseAttestation
Enables enterprise attestation.

.EXAMPLE
Set-PasskeyAuthenticatorConfiguration -Session $session -AlwaysUv
#>
function Set-PasskeyAuthenticatorConfiguration
{
    [CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'High', DefaultParameterSetName = 'AlwaysUv')]
    param(
        [Parameter(Position = 0)]
        [ValidateNotNull()]
        [DSInternals.Win32.WebAuthn.AuthenticatorManagementSession] $Session,

        [Parameter(Mandatory = $true, ParameterSetName = 'AlwaysUv')]
        [switch] $AlwaysUv,

        [Parameter(Mandatory = $true, ParameterSetName = 'EnterpriseAttestation')]
        [switch] $EnableEnterpriseAttestation
    )

    process {
        [bool] $ownsSession = -not $PSBoundParameters.ContainsKey('Session')
        if ($ownsSession) {
            $Session = Open-PasskeyAuthenticatorSession -ErrorAction Stop
        }
        try {
            [string] $operation = if ($PSCmdlet.ParameterSetName -eq 'AlwaysUv') { 'Enable Always Require User Verification' } else { 'Enable enterprise attestation' }
            if ($PSCmdlet.ShouldProcess('Selected authenticator', $operation)) {
                try {
                    if ($PSCmdlet.ParameterSetName -eq 'AlwaysUv') {
                        $Session.SetAlwaysUv($true)
                    }
                    else {
                        $Session.EnableEnterpriseAttestation()
                    }
                }
                catch {
                    $PSCmdlet.ThrowTerminatingError([System.Management.Automation.ErrorRecord]::new(
                        $PSItem.Exception, $PSItem.Exception.Message,
                        [System.Management.Automation.ErrorCategory]::WriteError, $Session))
                }
            }
        }
        finally {
            if ($ownsSession) {
                Close-PasskeyAuthenticatorSession -Session $Session -ErrorAction Stop
            }
        }
    }
}

<#
.SYNOPSIS
Reads the complete serialized large-blob store from an authenticator.

.DESCRIPTION
Returns the raw store as one byte-array object. The store is not parsed, repaired, or decrypted by this cmdlet.

.PARAMETER Session
An existing authenticator session. If omitted, a session is opened automatically and closed after the command.

.EXAMPLE
[byte[]] $store = Get-PasskeyAuthenticatorLargeBlob -Session $session
#>
function Get-PasskeyAuthenticatorLargeBlob
{
    [CmdletBinding()]
    [OutputType([byte[]])]
    param(
        [Parameter(Position = 0)]
        [ValidateNotNull()]
        [DSInternals.Win32.WebAuthn.AuthenticatorManagementSession] $Session
    )

    process {
        [bool] $ownsSession = -not $PSBoundParameters.ContainsKey('Session')
        if ($ownsSession) {
            $Session = Open-PasskeyAuthenticatorSession -ErrorAction Stop
        }
        try {
            try {
                [byte[]] $data = $Session.GetLargeBlobs()
                if ($null -ne $data) {
                    Write-PasskeyManagementByteArray -Data $data
                }
            }
            catch {
                $PSCmdlet.ThrowTerminatingError([System.Management.Automation.ErrorRecord]::new(
                    $PSItem.Exception, $PSItem.Exception.Message,
                    [System.Management.Automation.ErrorCategory]::ReadError, $Session))
            }
        }
        finally {
            if ($ownsSession) {
                Close-PasskeyAuthenticatorSession -Session $Session -ErrorAction Stop
            }
        }
    }
}

function Write-PasskeyManagementByteArray
{
    [CmdletBinding()]
    [OutputType([byte[]])]
    param(
        [Parameter(Mandatory = $true)]
        [ValidateNotNull()]
        [byte[]] $Data
    )

    Write-Output -NoEnumerate $Data
}

<#
.SYNOPSIS
Replaces the complete serialized large-blob store on an authenticator.

.DESCRIPTION
This replaces all serialized large-blob store data and is destructive. The caller is responsible for store schema, integrity, and device capacity validation. Authenticate explicitly first if required. If the write outcome is uncertain, do not blindly retry: dispose and reopen the session to select the authenticator and obtain fresh state first. Same-session capability and PIN-policy snapshots may be stale.

.PARAMETER Session
An existing authenticator session. If omitted, a session is opened automatically and closed after the command.

.PARAMETER Data
Complete serialized large-blob store as bytes.

.EXAMPLE
Set-PasskeyAuthenticatorLargeBlob -Session $session -Data $store
#>
function Set-PasskeyAuthenticatorLargeBlob
{
    [CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'High')]
    param(
        [Parameter(Position = 0)]
        [ValidateNotNull()]
        [DSInternals.Win32.WebAuthn.AuthenticatorManagementSession] $Session,

        [Parameter(Mandatory = $true, Position = 1)]
        [ValidateNotNull()]
        [byte[]] $Data
    )

    process {
        [bool] $ownsSession = -not $PSBoundParameters.ContainsKey('Session')
        if ($ownsSession) {
            $Session = Open-PasskeyAuthenticatorSession -ErrorAction Stop
        }
        try {
            if ($PSCmdlet.ShouldProcess('Selected authenticator', 'Replace complete large-blob store')) {
                try {
                    $Session.SetLargeBlobs($Data)
                }
                catch {
                    $PSCmdlet.ThrowTerminatingError([System.Management.Automation.ErrorRecord]::new(
                        $PSItem.Exception, $PSItem.Exception.Message,
                        [System.Management.Automation.ErrorCategory]::WriteError, $Session))
                }
            }
        }
        finally {
            if ($ownsSession) {
                Close-PasskeyAuthenticatorSession -Session $Session -ErrorAction Stop
            }
        }
    }
}

# Functions and aliases are filtered by FunctionsToExport / AliasesToExport in the manifest.
