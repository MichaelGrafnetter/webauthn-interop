# Needed for [Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod] type
Import-Module -Name Microsoft.Graph.Identity.SignIns -ErrorAction Stop

<#
.SYNOPSIS
Retrieves creation options required to generate and register a Microsoft Entra ID compatible passkey.

.DESCRIPTION
Retrieves a server-issued challenge and the associated WebAuthn parameters needed to register (attest) a new passkey for the specified Microsoft Entra ID user. The returned object can be piped to New-Passkey to drive the local authenticator and then to Register-EntraPasskey to complete enrollment.

Requires an active Microsoft Graph connection (Connect-MgGraph) with the UserAuthenticationMethod.ReadWrite.All scope. The caller must be an administrator; self-service is not supported.

.PARAMETER UserId
The unique identifier of the user. Either the object id (GUID) or UPN.

.PARAMETER ChallengeTimeout
Overrides the timeout of the server-generated challenge returned in the request. The default value is 5 minutes, with the accepted range being between 5 minutes and 30 days.

.EXAMPLE
Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
Get-EntraPasskeyRegistrationOptions -UserId 'AdeleV@contoso.com'

Fetches default creation options for the specified user.

.EXAMPLE
Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
Get-EntraPasskeyRegistrationOptions -UserId 'AdeleV@contoso.com' -ChallengeTimeout (New-TimeSpan -Minutes 10)

Fetches creation options with an extended 10-minute challenge timeout, useful when the user needs more time to complete the authenticator ceremony.

.EXAMPLE
Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
Get-EntraPasskeyRegistrationOptions -UserId 'AdeleV@contoso.com' | New-Passkey | Register-EntraPasskey -UserId 'AdeleV@contoso.com' -DisplayName 'YubiKey 5 Nano'

Performs end-to-end passkey registration in Microsoft Entra ID in a single pipeline.

.NOTES
Self-service operations aren't supported for Entra ID.

.LINK
Register-EntraPasskey

.LINK
New-Passkey

.LINK
https://learn.microsoft.com/en-us/graph/api/fido2authenticationmethod-creationoptions

#>
function Get-EntraPasskeyRegistrationOptions
{
    [Alias('Get-PasskeyRegistrationOptions')]
    [OutputType([DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions])]
    param (
        [Parameter(Mandatory = $true)]
        [ValidateScript({
            # Microsoft Graph accepts either a UPN (email-like) or an object ID (GUID) as the user identifier.
            # The regex follows RFC 5322's local-part character set, then '@', then a dot-separated domain of LDH labels.
            return $PSItem -match "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$" -or $true -eq [guid]::TryParse($PSItem, $([ref][guid]::Empty))
        })]
        [Alias('User')]
        [string] $UserId,

        [Parameter(Mandatory = $false)]
        [ValidateScript({
            if ($PSItem -is [TimeSpan]) {
                [timespan] $min = New-TimeSpan -Minutes 5
                [timespan] $max = New-TimeSpan -Days 30
                return $PSItem -ge $min -and $PSItem -le $max
            }
            else {
                throw "Parameter must be a TimeSpan object."
            }
        })]
        [Alias('Timeout')]
        [timespan] $ChallengeTimeout = (New-TimeSpan -Minutes 5)
    )
    try {
        # Generate the user-specific URL, e.g., https://graph.microsoft.com/beta/users/af4cf208-16e0-429d-b574-2a09c5f30dea/authentication/fido2Methods/creationOptions
        [string] $credentialOptionsUrl = '/beta/users/{0}/authentication/fido2Methods/creationOptions' -f [uri]::EscapeDataString($UserId)

        [string] $response = Invoke-MgGraphRequest `
            -Method GET `
            -Uri $credentialOptionsUrl `
            -Body @{ challengeTimeoutInMinutes = $ChallengeTimeout.TotalMinutes } `
            -OutputType Json

        # Parse JSON response and return the inner WebAuthn public-key options
        return [DSInternals.Win32.WebAuthn.Entra.MicrosoftGraphWebauthnCredentialCreationOptions]::Create($response).PublicKeyOptions
    }
    catch {
        throw $PSItem
    }
}

<#
.SYNOPSIS
Registers a new passkey in Microsoft Entra ID.

.DESCRIPTION
Registers a new passkey for the specified user in Microsoft Entra ID.

When called without the -Passkey parameter, this cmdlet performs the full registration flow: it requests a challenge from Entra ID, drives the local authenticator (which prompts the system passkey UI), and submits the attestation to complete enrollment.
When called with -Passkey, it submits a previously produced attestation, which is useful when the credential ceremony was run separately (e.g. via New-Passkey in a pipeline).

Requires an active Microsoft Graph connection (Connect-MgGraph) with the UserAuthenticationMethod.ReadWrite.All scope. The caller must be an administrator; self-service is not supported.

.PARAMETER UserId
The unique identifier of the user. Either the object id (GUID) or UPN.

.PARAMETER ChallengeTimeout
Overrides the timeout of the server-generated challenge returned in the request. The default value is 5 minutes, with the accepted range being between 5 minutes and 30 days.

.PARAMETER Passkey
The attestation credential produced by the local WebAuthn authenticator (e.g. via New-Passkey). Wrapped into a Microsoft Graph attestation response before being submitted.

.PARAMETER DisplayName
Custom name given to the registered passkey.

.EXAMPLE
Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
Register-EntraPasskey -UserId 'AdeleV@contoso.com' -DisplayName 'YubiKey 5 Nano'

Performs the full registration ceremony in one step: fetches creation options, prompts the local authenticator, and submits the attestation to Entra ID with the given display name.

.EXAMPLE
Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
Register-EntraPasskey -UserId 'AdeleV@contoso.com' -DisplayName 'YubiKey 5 Nano' -ChallengeTimeout (New-TimeSpan -Minutes 10)

Registers a passkey using an extended 10-minute challenge timeout, giving the user more time to complete the authenticator ceremony.

.EXAMPLE
Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
Get-EntraPasskeyRegistrationOptions -UserId 'AdeleV@contoso.com' | New-Passkey | Register-EntraPasskey -UserId 'AdeleV@contoso.com' -DisplayName 'YubiKey 5 Nano'

Splits the registration into explicit pipeline stages: fetch options, create the credential locally, and submit the attestation. Equivalent to the single-step form but lets the caller inspect intermediate values.

.NOTES
Self-service operations aren't supported for Entra ID.

.LINK
Get-EntraPasskeyRegistrationOptions

.LINK
New-Passkey

.LINK
https://learn.microsoft.com/en-us/graph/api/authentication-post-fido2methods

#>
function Register-EntraPasskey
{
    [CmdletBinding(DefaultParameterSetName = 'New')]
    [Alias('Register-Passkey', 'Register-MgUserAuthenticationFido2Method')]
    [OutputType([Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod])]
    param(
        [Parameter(Mandatory = $true, ParameterSetName = 'New')]
        [Parameter(Mandatory = $true, ParameterSetName = 'Existing')]
        [ValidateScript({
            # Microsoft Graph accepts either a UPN (email-like) or an object ID (GUID) as the user identifier.
            # The regex follows RFC 5322's local-part character set, then '@', then a dot-separated domain of LDH labels.
            return $PSItem -match "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$" -or $true -eq [guid]::TryParse($PSItem, $([ref][guid]::Empty))
        })]
        [Alias('User')]
        [string] $UserId,

        [Parameter(Mandatory = $true, ParameterSetName = 'Existing', ValueFromPipeline = $true)]
        [Alias('Attestation')]
        [DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential]
        $Passkey,

        [Parameter(Mandatory = $true, ParameterSetName = 'New')]
        [Parameter(Mandatory = $true, ParameterSetName = 'Existing')]
        [ValidateLength(1, 30)]
        [string] $DisplayName,

        [Parameter(Mandatory = $false, ParameterSetName = 'New')]
        [ValidateScript({
            if ($PSItem -is [TimeSpan]) {
                [timespan] $min = New-TimeSpan -Minutes 5
                [timespan] $max = New-TimeSpan -Minutes 43200
                return $PSItem -ge $min -and $PSItem -le $max
            }
            else {
                throw "Parameter must be a TimeSpan object."
            }
        })]
        [Alias('Timeout')]
        [TimeSpan] $ChallengeTimeout = (New-TimeSpan -Minutes 5)
    )

    process {
        try
        {
            if ($PSCmdlet.ParameterSetName -eq 'New')
            {
                [DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions] $options =
                    Get-EntraPasskeyRegistrationOptions -UserId $UserId -ChallengeTimeout $ChallengeTimeout -ErrorAction Stop

                [DSInternals.Win32.WebAuthn.WebAuthnApi] $api = [DSInternals.Win32.WebAuthn.WebAuthnApi]::new()
                $Passkey = $api.AuthenticatorMakeCredential($options)
            }

            [DSInternals.Win32.WebAuthn.Entra.MicrosoftGraphWebauthnAttestationResponse] $attestationResponse =
                [DSInternals.Win32.WebAuthn.Entra.MicrosoftGraphWebauthnAttestationResponse]::new($Passkey, $DisplayName)

            # Generate the user-specific URL, e.g., https://graph.microsoft.com/beta/users/af4cf208-16e0-429d-b574-2a09c5f30dea/authentication/fido2Methods
            [string] $registrationUrl = '/beta/users/{0}/authentication/fido2Methods' -f [uri]::EscapeDataString($UserId)

            [string] $response = Invoke-MgGraphRequest `
                                    -Method POST `
                                    -Uri $registrationUrl `
                                    -OutputType Json `
                                    -ContentType 'application/json' `
                                    -Body $attestationResponse.ToString()

            return [Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod]::FromJsonString($response)
        }
        catch
        {
            throw $PSItem
        }
    }
}

# Functions and aliases are filtered by FunctionsToExport / AliasesToExport in the parent manifest.
