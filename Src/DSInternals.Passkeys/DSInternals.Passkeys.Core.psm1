<#
.SYNOPSIS
Creates a new WebAuthn credential by driving the local authenticator.

.DESCRIPTION
Calls the Windows WebAuthn API to make a credential according to the provided PublicKeyCredentialCreationOptions, triggering the system passkey UI. Returns the resulting AttestationPublicKeyCredential, which can be wrapped into a provider-specific attestation response and submitted to Microsoft Entra ID, Okta, or any other relying party.

.PARAMETER Options
The WebAuthn public key credential creation options. Returned directly by Get-EntraPasskeyRegistrationOptions.

.PARAMETER HostName
Optional host name used to derive the WebAuthn origin when the server-issued options omit the relying party identifier.

.PARAMETER OktaOptions
The Okta-specific credential creation options. Returned by Get-OktaPasskeyRegistrationOptions.

.EXAMPLE
$credential = Get-EntraPasskeyRegistrationOptions -UserId 'AdeleV@contoso.com' | New-Passkey

Retrieves creation options from Microsoft Entra ID and creates a new passkey on the local authenticator without registering it.

.EXAMPLE
Get-EntraPasskeyRegistrationOptions -UserId 'AdeleV@contoso.com' | New-Passkey | Register-EntraPasskey -UserId 'AdeleV@contoso.com' -DisplayName 'YubiKey 5 Nano'

Performs end-to-end passkey registration in Microsoft Entra ID in a single pipeline.

.EXAMPLE
Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 | New-Passkey | Register-OktaPasskey

Performs end-to-end passkey registration in Okta in a single pipeline.

.LINK
Get-PasskeyCreationOptions

.LINK
Get-EntraPasskeyRegistrationOptions

.LINK
Get-OktaPasskeyRegistrationOptions

.LINK
Register-EntraPasskey

.LINK
Register-OktaPasskey

.LINK
Test-Passkey

#>
function New-Passkey
{
    [OutputType([DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential], ParameterSetName = 'Default')]
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

        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ParameterSetName = 'Okta')]
        [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnCredentialCreationOptions]
        $OktaOptions
    )

    process {
        try {
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
            $targetObject = if ($PSCmdlet.ParameterSetName -eq 'Okta') { $OktaOptions } else { $Options }
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
Get-PasskeyAuthenticator

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
Get-PasskeyAuthenticator

Lists all available authenticators.

.EXAMPLE
Get-PasskeyAuthenticator | Where-Object { -not $PSItem.Locked }

Lists only unlocked authenticators.

.EXAMPLE
Get-PasskeyAuthenticator | Format-Table -Prperty AuthenticatorName,Locked

Lists authenticator names and lock status in a table.

.LINK
Get-PasskeyAuthenticatorPlugin

.LINK
Get-PasskeyWindowsHello

#>
function Get-PasskeyAuthenticator
{
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
Get-PasskeyAuthenticator

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

# Functions and aliases are filtered by FunctionsToExport / AliasesToExport in the manifest.
