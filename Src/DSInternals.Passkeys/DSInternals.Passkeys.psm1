# Load PS host-specific assemblies
if ($PSVersionTable.PSVersion.Major -ge 6) {
    # PowerShell Core
    Add-Type -Path "$PSScriptRoot/net6.0/DSInternals.Win32.WebAuthn.dll" -ErrorAction Stop
    Add-Type -Path "$PSScriptRoot/net6.0/Microsoft.Identity.Client.dll" -ErrorAction Stop
    Add-Type -Path "$PSScriptRoot/net6.0/System.IdentityModel.Tokens.Jwt.dll" -ErrorAction Stop
}
else {
    # PowerShell Desktop
    Add-Type -Path "$PSScriptRoot/net48/DSInternals.Win32.WebAuthn.dll" -ErrorAction Stop
    Add-Type -Path "$PSScriptRoot/net48/Microsoft.Identity.Client.dll" -ErrorAction Stop
    Add-Type -Path "$PSScriptRoot/net48/System.IdentityModel.Tokens.Jwt.dll" -ErrorAction Stop
}

# Needed for [Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod] type
Import-Module -Name Microsoft.Graph.Identity.SignIns -ErrorAction Stop

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
        if ($_ -is [timespan]) {
            $min = New-TimeSpan -Minutes 5
            $max = New-TimeSpan -Days 30
            return $_ -ge $min -and $_ -le $max
        }
        else {
            throw "Parameter must be a TimeSpan object."
        }
    })]
    [Alias('Timeout')]
    [timespan] $ChallengeTimeout = (New-TimeSpan -Minutes 5)
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
    $uriBuilder.Scheme = [uri]::UriSchemeHttps
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
        if ($_ -is [timespan]) {
            $min = New-TimeSpan -Seconds 1
            $max = New-TimeSpan -Days 1
            return $_ -ge $min -and $_ -le $max
        }
        else {
            throw "Parameter must be a TimeSpan object."
        }
    })]
    [Alias('Timeout')]
    [timespan] $ChallengeTimeout = (New-TimeSpan -Minutes 5)
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
PS \> Get-PasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -ChallengeTimeout (New-TimeSpan -Minutes 1)

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
            $max = New-TimeSpan -Days 30
            if ($ChallengeTimeout -gt $max -or $ChallengeTimeout -lt $min) {
                Write-Error "Cannot validate argument on parameter 'ChallengeTimeout' which must be a valid TimeSpan between 5 minutes and 30 days for $_." -ErrorAction Stop
            }
        }
        else
        {
            $min = New-TimeSpan -Seconds 1
            $max = New-TimeSpan -Days 1
            if ($ChallengeTimeout -gt $max -or $ChallengeTimeout -lt $min) {
                Write-Error "Cannot validate argument on parameter 'ChallengeTimeout' which must be a valid TimeSpan between 1 second and 1 day for $_." -ErrorAction Stop
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
            if ($_ -is [timespan]) {
                $min = New-TimeSpan -Seconds 1
                $max = New-TimeSpan -Days 1
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

.INPUTS
DSInternals.Win32.WebAuthn.WebauthnCredentialCreationOptions

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

        switch ($Options.GetType())
        {
            ([DSInternals.Win32.WebAuthn.EntraID.MicrosoftGraphWebauthnCredentialCreationOptions])
            {
                if ([string]::IsNullOrEmpty($DisplayName)) {
                    throw "Parameter 'DisplayName' may not be null or empty."
                }
                return [DSInternals.Win32.WebAuthn.EntraID.MicrosoftGraphWebauthnAttestationResponse]::new($credential, $DisplayName)
            }
            ([DSInternals.Win32.WebAuthn.Okta.OktaWebauthnCredentialCreationOptions])
            {
                return [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnAttestationResponse]::new($credential, $Options.PublicKeyOptions.User.Id, $Options.Id)
            }
        }
    }
    catch {
        $errorRecord = New-Object Management.Automation.ErrorRecord(
            $_,
            $_.Message,
            [Management.Automation.ErrorCategory]::InvalidArgument,
            $Options
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

New-Alias -Name Register-MgUserAuthenticationFido2Method -Value Register-Passkey

Export-ModuleMember -Function 'Get-PasskeyRegistrationOptions','New-Passkey','Register-Passkey','Connect-Okta','Disconnect-Okta','Invoke-OktaWebRequest' `
                    -Alias 'Register-MgUserAuthenticationFido2Method'
