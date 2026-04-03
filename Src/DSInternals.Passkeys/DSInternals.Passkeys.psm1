# Needed for [Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod] type
Import-Module -Name Microsoft.Graph.Identity.SignIns -ErrorAction Stop

# Variables used for Okta connection lifecycle management
New-Variable -Name OktaToken -Value $null -Scope Script
New-Variable -Name OktaRevocationInfo -Value $null -Scope Script

function Get-EntraIDPasskeyRegistrationOptions
{
    [OutputType([DSInternals.Win32.WebAuthn.EntraID.MicrosoftGraphWebauthnCredentialCreationOptions])]
    param (
    [Parameter(Mandatory = $true)]
    [ValidateScript({
        return $_ -match "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$" -or $true -eq [guid]::TryParse($_, $([ref][guid]::Empty))
    })]
    [Alias('User')]
    [string] $UserId,

    [Parameter(Mandatory = $false)]
    [ValidateScript({
        if ($_ -is [TimeSpan]) {
            $min = New-TimeSpan -Minutes 5
            $max = New-TimeSpan -Minutes 43200
            return $_ -ge $min -and $_ -le $max
        }
        else {
            throw "Parameter must be a TimeSpan object."
        }
    })]
    [Alias('Timeout')]
    [TimeSpan] $ChallengeTimeout = (New-TimeSpan -Minutes 5)
    )
    try {
        Write-Debug "UserId ${UserId} TokenLifetimeSeconds ${ChallengeTimeout}"
        # Generate the user-specific URL, e.g., https://graph.microsoft.com/beta/users/af4cf208-16e0-429d-b574-2a09c5f30dea/authentication/fido2Methods/creationOptions
        [string] $credentialOptionsUrl = '/beta/users/{0}/authentication/fido2Methods/creationOptions' -f [uri]::EscapeDataString($UserId)

        Write-Debug ('Credential options url: ' + $credentialOptionsUrl)

        [string] $response = Invoke-MgGraphRequest -Method GET `
                                                -Uri $credentialOptionsUrl `
                                                -Body @{ challengeTimeoutInMinutes = $ChallengeTimeout.TotalMinutes } `
                                                -OutputType Json

        Write-Debug ('Credential options response: ' + $response)

        # Parse JSON response
        return [DSInternals.Win32.WebAuthn.EntraID.MicrosoftGraphWebauthnCredentialCreationOptions]::Create($response)
    }
    catch {
        throw $_
    }
}

function Invoke-OktaWebRequest
{
    param(
        $Path,
        $Query,
        $Method = [Microsoft.PowerShell.Commands.WebRequestMethod]::Post,
        $Body,
        $ContentType = "application/json"
    )

    Write-Debug "Path ${Path}"
    Write-Debug "Query ${Query}"
    Write-Debug "Method ${Method}"
    Write-Debug ('Body ' + ($Body | ConvertTo-Json))
    Write-Debug "Content type ${ContentType}"

    $tokenType = $Script:OktaToken.TokenType
    $token = $Script:OktaToken.AccessToken
    $headers = @{
        "Accept" = "application/json"
        "Authorization" = "${tokenType} ${token}"
    }

    $tenant = ([System.UriBuilder]($Script:OktaToken.AuthenticationResultMetadata.TokenEndpoint)).Host
    Write-Debug "Tenant ${Tenant}"

    $uriBuilder = New-Object System.UriBuilder
    $uriBuilder.Scheme = "https"
    $uriBuilder.Host = $tenant
    $uriBuilder.Path = $Path
    $uriBuilder.Query = $Query

    $uri = $uriBuilder.ToString()
    Write-Debug "Uri ${uri}"

    return Invoke-WebRequest -Uri $uri `
    -Method $Method `
    -Headers $headers `
    -ContentType $ContentType `
    -Body $Body
}

function Get-OktaPasskeyRegistrationOptions
{
    [OutputType([DSInternals.Win32.WebAuthn.Okta.OktaWebauthnCredentialCreationOptions])]
    param(
    [Parameter(Mandatory = $true)]
    [ValidatePattern("^[A-Za-z0-9_-]{20}$")]
    [Alias('User')]
    [string] $UserId,

    [Parameter(Mandatory = $false)]
    [ValidateScript({
        if ($_ -is [TimeSpan]) {
            $min = New-TimeSpan -Seconds 1
            $max = New-TimeSpan -Seconds 86400
            return $_ -ge $min -and $_ -le $max
        }
        else {
            throw "Parameter must be a TimeSpan object."
        }
    })]
    [Alias('Timeout')]
    [TimeSpan] $ChallengeTimeout = (New-TimeSpan -Minutes 5)
    )
    begin {
        if ($null -eq $Script:OktaToken)
        {
            throw 'Not connected to Okta, call Connnect-Okta to get started.'
        }
    }
    process {
        try
        {
            Write-Debug "In Get-OktaPasskeyRegistrationOptions with ${UserId} and ${ChallengeTimeout}"

            $TokenLifetimeSeconds = $ChallengeTimeout.TotalSeconds

            Write-Debug "TokenLifetimeSeconds ${TokenLifetimeSeconds}"

            [string] $credentialOptionsPath = "/api/v1/users/${UserId}/factors"
            [string] $credentialOptionsQuery = "tokenLifetimeSeconds=${TokenLifetimeSeconds}&activate=true"
            Write-Debug ('Credential options path: ' + $credentialOptionsPath)
            Write-Debug ('Credential options query: ' + $credentialOptionsQuery)

            $body = @{
                factorType = "webauthn"
                provider = "FIDO"
            } | ConvertTo-Json -Compress

            Write-Debug ('Credential options payload: ' + $body)

            [string] $response = Invoke-OktaWebRequest -Path $credentialOptionsPath `
                        -Query $credentialOptionsQuery `
                        -Body $body

            Write-Debug ('Credential options response: ' + $response)

            # Parse JSON response
            $options = [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnCredentialCreationOptions]::Create($response)

            # Okta appears to omit relying party id in the options, but it is required for credential creation
            # So set default to the tenant we are talking to, which is probably what the user wants anyway
            if ($null -eq $options.Embedded.PublicKeyOptions.RelyingParty.Id)
            {
                Write-Debug ('Setting relying party id to ' + ([System.UriBuilder]($Script:OktaToken.AuthenticationResultMetadata.TokenEndpoint)).Host)
                $options.Embedded.PublicKeyOptions.RelyingParty.Id = ([System.UriBuilder]($Script:OktaToken.AuthenticationResultMetadata.TokenEndpoint)).Host
            }
            Write-Debug ('Credential options: ' + ($options | Out-String))
            return $options
        }
        catch
        {
            throw $_
        }
    }
}

<#
.SYNOPSIS
Retrieves creation options required to generate and register a Microsoft Entra ID or Okta compatible passkey.

.PARAMETER UserId
The unique identifier of user.  For Entra ID, this is the object id (guid) or UPN.  For Okta, this is the unique identifier of Okta user.

.PARAMETER ChallengeTimeout
Overrides the timeout of the server-generated challenge returned in the request.  For Entra ID, the default value is 5 minutes, with the accepted range being between 5 minutes and 30 days.  For Okta, the default value is 300 second, with the accepted range being between 1 second and 1 day.

.EXAMPLE
PS \> Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
PS \> Get-PasskeyRegistrationOptions -UserId 'AdeleV@contoso.com'

.EXAMPLE
PS \> Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
PS \> Get-PasskeyRegistrationOptions -UserId 'AdeleV@contoso.com' -ChallengeTimeout (New-TimeSpan -Minutes 10)

.EXAMPLE
PS \> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
PS \> Get-PasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7

.EXAMPLE
PS \> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
PS \> Get-PasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -ChallengeTimeout (New-TimeSpan -Seconds 60)

.NOTES
Self-service operations aren't supported for Entra ID.
More info about Entra ID at https://learn.microsoft.com/en-us/graph/api/fido2authenticationmethod-creationoptions
More info about Okta at https://developer.okta.com/docs/api/openapi/okta-management/management/tag/UserFactor/#tag/UserFactor/operation/enrollFactor

#>
function Get-PasskeyRegistrationOptions
{
    [OutputType([DSInternals.Win32.WebAuthn.WebauthnCredentialCreationOptions])]
    param(
        [Parameter(Mandatory = $true)]
        [Alias('User')]
        [ValidateScript({
            if ($_ -match "^[A-Za-z0-9_-]{20}$" -or $true -eq [guid]::TryParse($_, $([ref][guid]::Empty)) -or $_ -match "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")
                {return $true}
                return $false
        })]
        [string] $UserId,

        [Parameter(Mandatory = $false)]
        [Alias('Timeout')]
        [timespan] $ChallengeTimeout = (New-TimeSpan -Minutes 5)
    )

    begin {
        Write-Debug "In Get-PasskeyRegistrationOptions with ${UserId} and ${ChallengeTimeout}"
        $IsEntraID = ($UserId -match "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$" -or $true -eq [guid]::TryParse($UserId, $([ref][guid]::Empty)))
        Write-Debug "IsEntraID: ${IsEntraId}"
        if ($IsEntraID)
        {
            $min = New-TimeSpan -Minutes 5
            $max = New-TimeSpan -Minutes 43200
            if ($ChallengeTimeout -gt $max -or $ChallengeTimeout -lt $min) {
                Write-Error "Cannot validate argument on parameter 'ChallengeTimeout' which must be a valid TimeSpan between 5 and 43200 minutes for $_." -ErrorAction Stop
            }
        }
        else
        {
            $min = New-TimeSpan -Seconds 1
            $max = New-TimeSpan -Seconds 86400
            if ($ChallengeTimeout -gt $max -or $ChallengeTimeout -lt $min) {
                Write-Error "Cannot validate argument on parameter 'ChallengeTimeout' which must be a valid TimeSpan between 1 and 86400 seconds for $_." -ErrorAction Stop
            }
            if ($UserId -notmatch "^[A-Za-z0-9_-]{20}$") {
                Write-Error "Cannot validate argument on parameter 'UserID' which must the unique idenitier for the user for Okta." -ErrorAction Stop
            }
            if ($null -eq $Script:OktaToken) {
                throw 'Not connected to Okta, call Connnect-Okta to get started.'
            }
        }
    }
    process {
        $Options = $null
        try {
            if ($IsEntraID) {
                Write-Debug "Calling Get-EntraIDPasskeyRegistrationOptions with ${UserId} and ${ChallengeTimeout}"
                $Options = Get-EntraIDPasskeyRegistrationOptions -UserId $UserId -ChallengeTimeout $ChallengeTimeout
            }
            else {
                Write-Debug "Calling Get-OktaPasskeyRegistrationOptions with ${UserId} and ${ChallengeTimeout}"
                $Options = Get-OktaPasskeyRegistrationOptions -UserId $UserId -ChallengeTimeout $ChallengeTimeout
            }
            return $Options
        }
        catch {
            $errorRecord = New-Object Management.Automation.ErrorRecord(
                $_.Exception,
                $_.Exception.Message,
                [Management.Automation.ErrorCategory]::InvalidArgument,
                $Options
            )

            $PSCmdlet.ThrowTerminatingError($errorRecord)
        }
    }
}

function Register-EntraIDPasskey
{
    [OutputType([Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod])]
    param(
        [Parameter(Mandatory = $true)]
        [ValidateScript({
            return $_ -match "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$" -or $true -eq [guid]::TryParse($_, $([ref][guid]::Empty))
        })]
        [Alias('User')]
        [string] $UserId,

        [ValidateScript({
            if ([string]::IsNullOrEmpty($_.DisplayName))
            {
                throw "Passkey 'DisplayName' field may not be null or empty."
            }
            return $true
        })]
        [DSInternals.Win32.WebAuthn.EntraID.MicrosoftGraphWebauthnAttestationResponse]
        $Passkey
    )
    try
    {
        # Generate the user-specific URL, e.g., https://graph.microsoft.com/beta/users/af4cf208-16e0-429d-b574-2a09c5f30dea/authentication/fido2Methods
        [string] $registrationUrl = '/beta/users/{0}/authentication/fido2Methods' -f [uri]::EscapeDataString($UserId)

        Write-Debug ('Registration URL: ' + $registrationUrl)

        [string] $response = Invoke-MgGraphRequest `
                                -Method POST `
                                -Uri $registrationUrl `
                                -OutputType Json `
                                -ContentType 'application/json' `
                                -Body $Passkey.ToString()

        Write-Debug ('Registration response: ' + $response)

        return [Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod]::FromJsonString($response)
    }
    catch
    {
        throw $_
    }
}

function Register-OktaPasskey
{
    [CmdletBinding(DefaultParameterSetName = 'New')]
    [OutputType([DSInternals.Win32.WebAuthn.Okta.OktaFido2AuthenticationMethod])]
    param (
        [Parameter(Mandatory = $true, ParameterSetName = 'Existing')]
        [Parameter(Mandatory = $true, ParameterSetName = 'New')]
        [Alias('User')]
        [string] $UserId,

        [Parameter(Mandatory = $true, ParameterSetName = 'Existing', ValueFromPipeline = $true)]
        [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnAttestationResponse]
        $Passkey,

        [Parameter(Mandatory = $false, ParameterSetName = 'New')]
        [ValidateScript({
            if ($_ -is [TimeSpan]) {
                $min = New-TimeSpan -Seconds 1
                $max = New-TimeSpan -Seconds 86400
                return $_ -ge $min -and $_ -le $max
            }
            else {
                throw "Parameter must be a TimeSpan object."
            }
        })]
        [Alias('Timeout')]
        [timespan] $ChallengeTimeout = (New-TimeSpan -Minutes 5)
    )
    try
    {
        if ($null -eq $Script:OktaToken)
        {
            throw 'Not connected to Okta, call Connnect-Okta to get started.'
        }

        $userId = $Passkey.UserId
        $factorId = $Passkey.FactorId
        [string] $registrationPath = "/api/v1/users/${userId}/factors/${factorId}/lifecycle/activate"

        Write-Debug ('Registration path: ' + $registrationPath)

        [string] $response = Invoke-OktaWebRequest -Path $registrationPath `
                    -Body $Passkey.ToString()

        Write-Debug ('Registration response: ' + $response)

        return [DSInternals.Win32.WebAuthn.Okta.OktaFido2AuthenticationMethod]::FromJsonString($response)
    }
    catch
    {
        throw $_
    }
}

<#
.SYNOPSIS
Registers a new passkey in Microsoft Entra ID, or Okta.

.PARAMETER UserId
The unique identifier of user.  For Entra ID, this is the object id (guid) or UPN.  For Okta, this is the unique identifier of Okta user.

.PARAMETER ChallengeTimeout
Overrides the timeout of the server-generated challenge returned in the request.  For Entra ID, the default value is 5 minutes, with the accepted range being between 5 minutes and 30 days.  For Okta, the default value is 300 second, with the accepted range being between 1 second and 1 day.

.PARAMETER Passkey
The passkey to be registered.

.PARAMETER DisplayName
Custom name given to the Entra ID registered passkey.

.EXAMPLE
PS \> Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
PS \> Register-Passkey -UserId 'AdeleV@contoso.com' -DisplayName 'YubiKey 5 Nano'

.EXAMPLE
PS \> Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
PS \> Register-Passkey -UserId 'AdeleV@contoso.com' -DisplayName 'YubiKey 5 Nano' -ChallengeTimeout (New-TimeSpan -Minutes 10)

.EXAMPLE
PS \> Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
PS \> Get-PasskeyRegistrationOptions -UserId 'AdeleV@contoso.com' | New-Passkey -DisplayName 'YubiKey 5 Nano' | Register-Passkey -UserId 'AdeleV@contoso.com'

.EXAMPLE
PS \> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
PS \> Register-Passkey -UserId 00eDuihq64pgP1gVD0x7

.EXAMPLE
PS \> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
PS \> Get-PasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 | New-Passkey | Register-Passkey

.NOTES
More info for Entra ID at https://learn.microsoft.com/en-us/graph/api/authentication-post-fido2methods
More info for Okta at https://developer.okta.com/docs/api/openapi/okta-management/management/tag/UserFactor/#tag/UserFactor/operation/activateFactor

#>
function Register-Passkey
{
    [OutputType([DSInternals.Win32.WebAuthn.Okta.OktaFido2AuthenticationMethod], ParameterSetName = 'OktaNew')]
    [OutputType([Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod], ParameterSetName = 'EntraIDNew')]
    param(
        [Parameter(Mandatory = $true, ParameterSetName = 'EntraIDNew')]
        [Parameter(Mandatory = $true, ParameterSetName = 'Existing')]
        [Parameter(Mandatory = $true, ParameterSetName = 'OktaNew')]
        [Alias('User')]
        [string] $UserId,

        [Parameter(Mandatory = $true, ParameterSetName = 'Existing', ValueFromPipeline = $true)]
        [DSInternals.Win32.WebAuthn.WebauthnAttestationResponse]
        $Passkey,

        [Parameter(Mandatory = $true, ParameterSetName = 'EntraIDNew')]
        [string] $DisplayName,

        [Parameter(Mandatory = $false, ParameterSetName = 'EntraIDNew')]
        [Parameter(Mandatory = $false, ParameterSetName = 'OktaNew')]
        [Alias('Timeout')]
        [timespan] $ChallengeTimeout = (New-TimeSpan -Minutes 5)
    )

    begin {
        if ($null -ne $Passkey -and $Passkey.GetType() -eq ([DSInternals.Win32.WebAuthn.EntraID.MicrosoftGraphWebauthnAttestationResponse]) -and [string]::IsNullOrEmpty($Passkey.displayName)) {
            throw "Parameter 'DisplayName' may not be null or empty."
        }
    }

    process {
        switch ($PSCmdlet.ParameterSetName) {
            'Existing' {
                switch ($Passkey.GetType())
                {
                    ([DSInternals.Win32.WebAuthn.EntraID.MicrosoftGraphWebauthnAttestationResponse])
                    {
                        return Register-EntraIDPasskey -UserId $UserId -Passkey $Passkey
                    }

                    ([DSInternals.Win32.WebAuthn.Okta.OktaWebauthnAttestationResponse])
                    {
                        if ($null -eq $Script:OktaToken) {
                            throw 'Not connected to Okta, call Connnect-Okta to get started.'
                        }
                        return Register-OktaPasskey -UserId $UserId -Passkey $Passkey
                    }
                }
            }
            default {
                [DSInternals.Win32.WebAuthn.WebauthnCredentialCreationOptions] $registrationOptions =
                Get-PasskeyRegistrationOptions -UserId $UserId -ChallengeTimeout $ChallengeTimeout

                [DSInternals.Win32.WebAuthn.WebauthnAttestationResponse] $passkey =
                New-Passkey -Options $registrationOptions -DisplayName $DisplayName

                # Recursive call with the 'Existing' parameter set
                return Register-Passkey -UserId $UserId -Passkey $passkey
            }
        }
    }
}

<#
.SYNOPSIS
Creates a new Microsoft Entra ID or Okta compatible passkey.

.PARAMETER Options
Options required to generate a Microsoft Entra ID or Okta compatible passkey.

.PARAMETER DisplayName
Custom name given to the Entra ID registered passkey.

.EXAMPLE
PS \> Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
PS \> Get-PasskeyRegistrationOptions -UserId 'AdeleV@contoso.com' | New-Passkey -DisplayName 'YubiKey 5 Nano' | Register-Passkey -UserId 'AdeleV@contoso.com'

.EXAMPLE
PS \> New-Passkey -Options $options

.EXAMPLE
PS \> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
PS \> Get-PasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 | New-Passkey

#>
function New-Passkey
{
    [OutputType([DSInternals.Win32.WebAuthn.WebauthnAttestationResponse])]
    [CmdletBinding()]
    param(
        [Parameter(Mandatory = $true, ValueFromPipeline = $true)]
        [DSInternals.Win32.WebAuthn.WebauthnCredentialCreationOptions]
        $Options,

        [Parameter(Mandatory = $false)]
        [ValidateLength(1, 30)]
        [string] $DisplayName
    )

    try {
        [DSInternals.Win32.WebAuthn.WebAuthnApi] $api = [DSInternals.Win32.WebAuthn.WebAuthnApi]::new()
        [DSInternals.Win32.WebAuthn.PublicKeyCredential] $credential = $api.AuthenticatorMakeCredential($Options.PublicKeyOptions)

        switch ($Options.GetType()) {
            ([DSInternals.Win32.WebAuthn.EntraID.MicrosoftGraphWebauthnCredentialCreationOptions]) {
                if ([string]::IsNullOrEmpty($DisplayName)) {
                    throw "Parameter 'DisplayName' may not be null or empty."
                }
                return [DSInternals.Win32.WebAuthn.EntraID.MicrosoftGraphWebauthnAttestationResponse]::new($credential, $DisplayName)
            }
            ([DSInternals.Win32.WebAuthn.Okta.OktaWebauthnCredentialCreationOptions]) {
                return [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnAttestationResponse]::new($credential, $Options.PublicKeyOptions.User.Id, $Options.Id)
            }
        }
    }
    catch {
        [System.Management.Automation.ErrorRecord] $errorRecord = [System.Management.Automation.ErrorRecord]::new(
            $_,
            $_.Message,
            [System.Management.Automation.ErrorCategory]::InvalidArgument,
                $Options
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

.EXAMPLE
PS \> Test-Passkey -RelyingPartyId 'login.microsoft.com'

Tests any passkey registered for login.microsoft.com with a random challenge.

.EXAMPLE
PS \> $challenge = Get-PasskeyRandomChallenge -Length 32
PS \> Test-Passkey -RelyingPartyId 'login.microsoft.com' -Challenge $challenge

Tests any passkey registered for login.microsoft.com with a specific challenge.

.EXAMPLE
PS \> $credential = Get-PasskeyWindowsHello | Select-Object -First 1
PS \> Test-Passkey -RelyingPartyId $credential.RelyingPartyInformation.Id -CredentialId $credential.CredentialId

Tests a specific platform credential.

#>
function Test-Passkey
{
    [OutputType([DSInternals.Win32.WebAuthn.PublicKeyCredential])]
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
        [object] $CredentialId
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

        $response = $api.AuthenticatorGetAssertion(
            $RelyingPartyId,
            $challengeBytes,
            $UserVerification,
            $AuthenticatorAttachment,
            $timeoutMilliseconds,
            $allowCredentials,
            $null,  # extensions
            [DSInternals.Win32.WebAuthn.CredentialLargeBlobOperation]::None,
            $null,  # largeBlob
            $false, # browserInPrivateMode
            $null,  # linkedDevice
            $false, # autoFill
            $null,  # credentialHints
            $null,  # remoteWebOrigin
            $null,  # authenticatorId
            $null,  # publicKeyCredentialRequestOptionsJson
            [DSInternals.Win32.WebAuthn.WindowHandle]::ForegroundWindow
        )

        return $response
    }
    catch {
        [System.Management.Automation.ErrorRecord] $errorRecord = [System.Management.Automation.ErrorRecord]::new(
            $_.Exception,
            $_.Exception.Message,
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
PS \> Get-PasskeyAuthenticatorPlugin

Lists all registered authenticator plugins.

.EXAMPLE
PS \> Get-PasskeyAuthenticatorPlugin | Where-Object Enabled -eq $true

Lists only enabled authenticator plugins.

.EXAMPLE
PS \> Get-PasskeyAuthenticatorPlugin | Select-Object -Property Name,PackageFamilyName,Enabled

Lists authenticator plugins with selected properties.

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
            $_.Exception,
            $_.Exception.Message,
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
PS \> Get-PasskeyAuthenticator

Lists all available authenticators.

.EXAMPLE
PS \> Get-PasskeyAuthenticator | Where-Object { -not $PSItem.Locked }

Lists only unlocked authenticators.

.EXAMPLE
PS \> Get-PasskeyAuthenticator | Format-Table -Prperty AuthenticatorName,Locked

Lists authenticator names and lock status in a table.

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
            $_.Exception,
            $_.Exception.Message,
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

.EXAMPLE
PS \> Get-PasskeyWindowsHello

Lists all platform credentials.

.EXAMPLE
PS \> Get-PasskeyWindowsHello -RelyingPartyId 'login.microsoft.com'

Lists credentials for a specific relying party.

#>
function Get-PasskeyWindowsHello
{
    [OutputType([DSInternals.Win32.WebAuthn.CredentialDetails])]
    [CmdletBinding()]
    param(
        [Parameter(Mandatory = $false)]
        [Alias('RpId')]
        [string] $RelyingPartyId
    )

    try {
        [DSInternals.Win32.WebAuthn.CredentialDetails[]] $credentials = [DSInternals.Win32.WebAuthn.WebAuthnApi]::GetPlatformCredentialList($RelyingPartyId)

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
            $_.Exception,
            $_.Exception.Message,
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
PS \> $cred = Get-PasskeyWindowsHello | Select-Object -First 1
PS \> Remove-PasskeyWindowsHello -CredentialId $cred.CredentialId

Removes a specific platform credential by ID.

.EXAMPLE
PS \> Get-PasskeyWindowsHello | Where-Object { $_.RelyingPartyInformation.Id -eq 'example.com' } | Remove-PasskeyWindowsHello

Removes all credentials for a specific relying party using pipeline input.

.EXAMPLE
PS \> Remove-PasskeyWindowsHello -CredentialId 'dGVzdC1jcmVkZW50aWFsLWlk'

Removes a credential using a Base64Url encoded credential ID.

.NOTES
Requires Windows with WebAuthn API version 4 or later (Windows 10 2004+).
This operation requires appropriate permissions and may trigger a Windows Security prompt.

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
        [string] $credentialIdString = [DSInternals.Win32.WebAuthn.Base64UrlConverter]::ToBase64UrlString($credId)
        if ($PSCmdlet.ShouldProcess($credentialIdString, 'Remove platform credential')) {
            [DSInternals.Win32.WebAuthn.WebAuthnApi]::DeletePlatformCredential($credId)
            Write-Verbose "Successfully removed credential $credentialIdString"
        }
    }
    catch {
        Write-Error -ErrorRecord ([System.Management.Automation.ErrorRecord]::new(
            $_.Exception,
            $_.Exception.Message,
            [System.Management.Automation.ErrorCategory]::WriteError,
            $credId
        ))
    }
}

<#
.SYNOPSIS
Retrieves the Microsoft Graph endpoint URL.

.NOTES
Dynamic URL retrieval is used to support Azure environments, like Azure Public, Azure Government, or Azure China.

#>
function Get-MgGraphEndpoint
{
    [CmdletBinding()]
    [OutputType([string])]
    param()

    try {
        [Microsoft.Graph.PowerShell.Authentication.AuthContext] $context = Get-MgContext -ErrorAction Stop

        if($null -ne $context) {
            return (Get-MgEnvironment -Name $context.Environment -ErrorAction Stop).GraphEndpoint
        }
    }
    catch {
        $errorRecord = New-Object Management.Automation.ErrorRecord(
            (New-Object Exception('Not connected to Microsoft Graph.')),
            'Not connected to Microsoft Graph.',
            [Management.Automation.ErrorCategory]::ConnectionError,
            $context
        )

        $PSCmdlet.ThrowTerminatingError($errorRecord)
    }
}

<#
.SYNOPSIS
Retrieves an access token to interact with Okta APIs.

.PARAMETER Tenant
The unique identifier of Okta tenant, like 'example.okta.com'.

.PARAMETER ClientId
The client id of the Okta application used to obtain an access token.

.PARAMETER Scopes
Scopes to request for the access token.  Defaults to 'okta.users.manage'.

.PARAMETER JsonWebKey
The JSON Web Key used to authenticate to the Okta application, in order to obtain access token using the client credentials OAuth flow.

.EXAMPLE
PS \> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7

.EXAMPLE
PS \> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7 -Scopes @('okta.users.manage','okta.something.else')

.EXAMPLE
PS \> $jwk = '{"kty":"RSA","kid":"EE3QB0WvhuOwR9DuR6717OERKbDrBemrDKOK4Xvbf8c","d":"TmljZSB0cnkhICBCdXQgdGhpcyBpc...'
PS \> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7 -Scopes @('okta.users.manage','okta.something.else') -JsonWebKey $jwk

#>

function Connect-Okta
{
    param(
        [Parameter(Mandatory = $true, ParameterSetName = 'ClientCredentials')]
        [Parameter(Mandatory = $true, ParameterSetName = 'AuthorizationCode')]
        [ValidatePattern('^[a-zA-Z0-9-]+\.okta(?:-emea|preview|\.mil)?\.com$')]
        [string] $Tenant,

        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ParameterSetName = 'ClientCredentials')]
        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ParameterSetName = 'AuthorizationCode')]
        [ValidatePattern('^[A-Za-z0-9_-]{20}$')]
        [string]
        $ClientId,

        [Parameter(Mandatory = $false, ParameterSetName = 'ClientCredentials')]
        [Parameter(Mandatory = $false, ParameterSetName = 'AuthorizationCode')]
        [string[]] $Scopes = @('okta.users.manage'),

        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ParameterSetName = 'ClientCredentials')]
        [Alias('jwk')]
        [string]
        $JsonWebKey
    )

    try {
        $Script:OktaRevocationInfo = [PSCustomObject] @{
            ClientId = $ClientId
            RevocationToken = $null
        }
        switch ($PSCmdlet.ParameterSetName){
            'AuthorizationCode'
            {
                Write-Debug "No JWK found, assuming public client intended"
                $publicClientApp = [Microsoft.Identity.Client.PublicClientApplicationBuilder]::Create($ClientId).
                    WithExperimentalFeatures().
                    WithOidcAuthority("https://${tenant}/").
                    WithRedirectUri("http://localhost:8080/login/callback").
                    Build()

                $Script:OktaToken = $publicClientApp.AcquireTokenInteractive($Scopes).ExecuteAsync().GetAwaiter().GetResult()
                if ($null -ne $Script:OktaToken)
                {
                    Write-Host 'Okta access token successfully retrieved.'
                }
            }
            'ClientCredentials'
            {
                Write-Debug "JWK found, assuming confidential client intended"
                $jwk = [Microsoft.IdentityModel.Tokens.JsonWebKey]::new($JsonWebKey)
                $signingCredentials = [Microsoft.IdentityModel.Tokens.SigningCredentials]::new($jwk,'RS256')
                $issuer = $ClientId
                $audience = "https://${tenant}/oauth2/v1/token"
                $subject = [System.Security.Claims.ClaimsIdentity]::new()
                $subject.Claims.Add([System.Security.Claims.Claim]::new('sub',$ClientId))
                $notBefore = (Get-Date)
                $expires = (Get-Date).AddMinutes(60)
                $issuedAt = $notBefore

                $tokenHandler = [System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler]::new()
                $securityToken = $tokenHandler.CreateJwtSecurityToken($issuer, $audience, $subject, $notBefore, $expires, $issuedAt, $signingCredentials)
                $revocationToken = $tokenHandler.CreateJwtSecurityToken($issuer, "https://${tenant}/oauth2/v1/revoke", $subject, $notBefore, $expires, $issuedAt, $signingCredentials)
                $assertion = $tokenHandler.WriteToken($securityToken)
                $Script:OktaRevocationInfo.RevocationToken = $tokenHandler.WriteToken($revocationToken)
                $confidentialClientApp = [Microsoft.Identity.Client.ConfidentialClientApplicationBuilder]::Create($ClientId).
                    WithClientAssertion($assertion).
                    WithOidcAuthority("https://${tenant}").
                    Build()

                $Script:OktaToken = $confidentialClientApp.AcquireTokenForClient($Scopes).ExecuteAsync().GetAwaiter().GetResult()
                if ($null -ne $Script:OktaToken -and $null -ne $Script:OktaRevocationInfo.RevocationToken)
                {
                    Write-Host 'Okta access and revocation tokens successfully retrieved.'
                }
            }
        }
    }
    catch {
        throw
     }
}

<#
.SYNOPSIS
Revokes Okta access token.

.EXAMPLE
PS \> Disconnect-Okta

.DESCRIPTION
Revokes the Okta access token cached from the call to `Connect-Okta`.

.LINK
https://developer.okta.com/docs/guides/revoke-tokens/main/

#>
function Disconnect-Okta
{
    if ($null -ne $Script:OktaToken)
    {
        [string] $revocationPath = "/oauth2/v1/revoke"

        Write-Debug ('Revocation path: ' + $revocationPath)

        $body = @{
            client_id = $Script:OktaRevocationInfo.ClientId
            token = $Script:OktaToken.AccessToken
            token_type_hint = "access_token"
        }

        if ($null -ne $Script:OktaRevocationInfo.RevocationToken)
        {
            $body.Add('client_assertion_type',"urn:ietf:params:oauth:client-assertion-type:jwt-bearer")
            $body.Add('client_assertion',$Script:OktaRevocationInfo.RevocationToken)
        }

        Write-Debug ('Revocation payload: ' + ($body | ConvertTo-Json))

        [string] $response = Invoke-OktaWebRequest -Uri $revocationPath `
                    -ContentType "application/x-www-form-urlencoded" `
                    -Body $body

        $Script:OktaToken = $null
        $Script:OktaRevocationInfo.RevocationToken = $null

        if ($response.Length -eq 0 -and $null -eq $Script:OktaToken)
        {
            Write-Host 'Okta access token successfully revoked.'
        }
    }
}

<#
.SYNOPSIS
Generates a random challenge to be used by WebAuthn.

.PARAMETER Length
The length of the challenge in bytes.

.EXAMPLE
PS \> New-PasskeyRandomChallenge -Length 32
Generates a random 32-byte challenge.

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
    $rng = [System.Security.Cryptography.RandomNumberGenerator]::Create()
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

New-Alias -Name Register-MgUserAuthenticationFido2Method -Value Register-Passkey

Export-ModuleMember -Function 'Get-PasskeyRegistrationOptions','New-Passkey','Register-Passkey','Test-Passkey','Connect-Okta','Disconnect-Okta','Invoke-OktaWebRequest','Get-PasskeyAuthenticatorPlugin','Get-PasskeyAuthenticator','Get-PasskeyWindowsHello','Remove-PasskeyWindowsHello', 'New-PasskeyRandomChallenge' `
                    -Alias 'Register-MgUserAuthenticationFido2Method'
