# HTTP Authorization scheme used by an Okta access token.
# 'Bearer' is used by both OAuth client-credentials and authorization-code flows;
# 'SSWS' is the scheme for static Okta API tokens.
enum OktaTokenScheme {
    Bearer
    SSWS
}

# Normalizes an Okta access token across all supported authentication flows
# so that downstream cmdlets do not need to care which flow produced it.
class OktaToken {
    [OktaTokenScheme] $Scheme
    [string] $AccessToken
    [Uri] $Tenant

    OktaToken([OktaTokenScheme] $scheme, [string] $accessToken, [Uri] $tenant) {
        $this.Scheme = $scheme
        $this.AccessToken = $accessToken
        $this.Tenant = $tenant
    }
}

# State needed to revoke an OAuth-issued access token via /oauth2/v1/revoke.
# Not used for static SSWS API tokens, which are managed in the Okta admin console.
class OktaRevocationInfo {
    [string] $ClientId
    # JWT client assertion used for the private_key_jwt flow.
    [string] $RevocationToken
    # Plaintext client secret used for the client_secret_post flow.
    [string] $ClientSecret

    OktaRevocationInfo([string] $clientId) {
        $this.ClientId = $clientId
    }
}

# Variables used for Okta connection lifecycle management
[OktaToken] $Script:OktaToken = $null
[OktaRevocationInfo] $Script:OktaRevocationInfo = $null

<#
.SYNOPSIS
Retrieves creation options required to generate and register an Okta compatible passkey.

.PARAMETER UserId
The unique identifier of the Okta user (20-character Okta id).

.PARAMETER ChallengeTimeout
Overrides the timeout of the server-generated challenge returned in the request. The default value is 5 minutes, with the accepted range being between 1 second and 1 day.

.EXAMPLE
PS \> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
PS \> Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7

.EXAMPLE
PS \> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
PS \> Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -ChallengeTimeout (New-TimeSpan -Minutes 1)

.NOTES
https://developer.okta.com/docs/api/openapi/okta-management/management/tag/UserFactor/#tag/UserFactor/operation/enrollFactor

#>
function Get-OktaPasskeyRegistrationOptions
{
    [OutputType([DSInternals.Win32.WebAuthn.Okta.OktaWebauthnCredentialCreationOptions])]
    param(
        [Parameter(Mandatory = $true)]
        [ValidatePattern('^[A-Za-z0-9_-]{20}$')]
        [Alias('User')]
        [string] $UserId,

        [Parameter(Mandatory = $false)]
        [ValidateScript({
            if ($PSItem -is [TimeSpan]) {
                [timespan] $min = New-TimeSpan -Seconds 1
                [timespan] $max = New-TimeSpan -Days 1
                return $PSItem -ge $min -and $PSItem -le $max
            }
            else {
                throw 'Parameter must be a TimeSpan object.'
            }
        })]
        [Alias('Timeout')]
        [TimeSpan] $ChallengeTimeout = (New-TimeSpan -Minutes 5)
    )
    begin {
        if ($null -eq $Script:OktaToken) {
            throw 'Not connected to Okta, call Connect-Okta to get started.'
        }
    }
    process {
        try
        {
            Write-Debug "In Get-OktaPasskeyRegistrationOptions with ${UserId} and ${ChallengeTimeout}"

            [int] $TokenLifetimeSeconds = $ChallengeTimeout.TotalSeconds

            Write-Debug "TokenLifetimeSeconds ${TokenLifetimeSeconds}"

            [string] $credentialOptionsPath = "/api/v1/users/${UserId}/factors"
            [string] $credentialOptionsQuery = "tokenLifetimeSeconds=${TokenLifetimeSeconds}&activate=true"
            Write-Debug ('Credential options path: ' + $credentialOptionsPath)
            Write-Debug ('Credential options query: ' + $credentialOptionsQuery)

            [string] $body = @{
                factorType = 'webauthn'
                provider = 'FIDO'
            } | ConvertTo-Json -Compress

            Write-Debug ('Credential options payload: ' + $body)

            [string] $response = Invoke-OktaWebRequest -Path $credentialOptionsPath `
                        -Query $credentialOptionsQuery `
                        -Body $body `
                        -ErrorAction Stop

            Write-Debug ('Credential options response: ' + $response)

            # Parse JSON response
            [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnCredentialCreationOptions] $options =
                [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnCredentialCreationOptions]::Create($response)

            # Okta appears to omit relying party id in the options, but it is required for credential creation
            # So set default to the tenant we are talking to, which is probably what the user wants anyway
            if ($null -eq $options.Embedded.PublicKeyOptions.RelyingParty.Id) {
                [string] $relyingPartyId = $Script:OktaToken.Tenant.Host
                Write-Debug ('Setting relying party id to ' + $relyingPartyId)
                $options.Embedded.PublicKeyOptions.RelyingParty.Id = $relyingPartyId
            }

            Write-Debug ('Credential options: ' + ($options | Out-String))
            return $options
        }
        catch {
            throw $PSItem
        }
    }
}

<#
.SYNOPSIS
Registers a new passkey in Okta.

.PARAMETER UserId
The unique identifier of the Okta user (20-character Okta id).

.PARAMETER ChallengeTimeout
Overrides the timeout of the server-generated challenge returned in the request. The default value is 5 minutes, with the accepted range being between 1 second and 1 day.

.PARAMETER Passkey
The passkey to be registered.

.PARAMETER FactorId
The 20-character Okta factor identifier returned by Get-OktaPasskeyRegistrationOptions, used together with -AttestationPublicKeyCredential as an alternative to -Passkey.

.PARAMETER AttestationPublicKeyCredential
The raw attestation credential produced by the local WebAuthn authenticator (e.g. via New-Passkey), used together with -UserId and -FactorId as an alternative to -Passkey.

.EXAMPLE
PS \> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
PS \> Register-OktaPasskey -UserId 00eDuihq64pgP1gVD0x7

.EXAMPLE
PS \> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
PS \> Register-OktaPasskey -UserId 00eDuihq64pgP1gVD0x7 -ChallengeTimeout (New-TimeSpan -Minutes 1)

.EXAMPLE
PS \> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
PS \> Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 | New-Passkey | Register-OktaPasskey

.EXAMPLE
PS \> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
PS \> $options = Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7
PS \> $credential = New-Passkey -Options $options.PublicKeyOptions
PS \> Register-OktaPasskey -UserId 00eDuihq64pgP1gVD0x7 -FactorId $options.FactorId -AttestationPublicKeyCredential $credential

.NOTES
https://developer.okta.com/docs/api/openapi/okta-management/management/tag/UserFactor/#tag/UserFactor/operation/activateFactor

#>
function Register-OktaPasskey
{
    [CmdletBinding(DefaultParameterSetName = 'New')]
    [OutputType([DSInternals.Win32.WebAuthn.Okta.OktaFido2AuthenticationMethod])]
    param (
        [Parameter(Mandatory = $true, ParameterSetName = 'New')]
        [Parameter(Mandatory = $true, ParameterSetName = 'AttestationCredential')]
        [ValidatePattern('^[A-Za-z0-9_-]{20}$')]
        [Alias('User')]
        [string] $UserId,

        [Parameter(Mandatory = $true, ParameterSetName = 'Existing', ValueFromPipeline = $true)]
        [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnAttestationResponse]
        $Passkey,

        [Parameter(Mandatory = $true, ParameterSetName = 'AttestationCredential')]
        [ValidatePattern('^[A-Za-z0-9_-]{20}$')]
        [Alias('Factor')]
        [string] $FactorId,

        [Parameter(Mandatory = $true, ParameterSetName = 'AttestationCredential', ValueFromPipeline = $true)]
        [Alias('Attestation','Credential')]
        [DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential]
        $AttestationPublicKeyCredential,

        [Parameter(Mandatory = $false, ParameterSetName = 'New')]
        [ValidateScript({
            if ($PSItem -is [TimeSpan]) {
                [timespan] $min = New-TimeSpan -Seconds 1
                [timespan] $max = New-TimeSpan -Days 1
                return $PSItem -ge $min -and $PSItem -le $max
            }
            else {
                throw 'Parameter must be a TimeSpan object.'
            }
        })]
        [Alias('Timeout')]
        [timespan] $ChallengeTimeout = (New-TimeSpan -Minutes 5)
    )

    process {
        try {
            if ($null -eq $Script:OktaToken) {
                throw 'Not connected to Okta, call Connect-Okta to get started.'
            }

            switch ($PSCmdlet.ParameterSetName) {
                'New' {
                    [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnCredentialCreationOptions] $options =
                        Get-OktaPasskeyRegistrationOptions -UserId $UserId -ChallengeTimeout $ChallengeTimeout -ErrorAction Stop

                    [DSInternals.Win32.WebAuthn.WebAuthnApi] $api = [DSInternals.Win32.WebAuthn.WebAuthnApi]::new()
                    [DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential] $credential =
                        $api.AuthenticatorMakeCredential($options.PublicKeyOptions)

                    $Passkey = [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnAttestationResponse]::new(
                        $credential, $options.PublicKeyOptions.User.Id, $options.FactorId)
                }
                'AttestationCredential' {
                    $Passkey = [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnAttestationResponse]::new(
                        $AttestationPublicKeyCredential, $UserId, $FactorId)
                }
            }

            [string] $resolvedUserId = $Passkey.UserId
            [string] $resolvedFactorId = $Passkey.FactorId
            [string] $registrationPath = "/api/v1/users/${resolvedUserId}/factors/${resolvedFactorId}/lifecycle/activate"

            Write-Debug ('Registration path: ' + $registrationPath)

            [string] $response = Invoke-OktaWebRequest -Path $registrationPath `
                        -Body $Passkey.ToString() `
                        -ErrorAction Stop

            Write-Debug ('Registration response: ' + $response)

            return [DSInternals.Win32.WebAuthn.Okta.OktaFido2AuthenticationMethod]::FromJsonString($response)
        }
        catch {
            throw $PSItem
        }
    }
}

<#
.SYNOPSIS
Resolves an Okta user login to its 20-character Okta user id.

.PARAMETER Login
The Okta user login (typically an email address such as 'user@example.com').

.EXAMPLE
PS \> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
PS \> Get-OktaUserId -Login 'user@example.com'

.EXAMPLE
PS \> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
PS \> $userId = Get-OktaUserId -Login 'user@example.com'
PS \> Register-OktaPasskey -UserId $userId

.NOTES
https://developer.okta.com/docs/api/openapi/okta-management/management/tag/User/#tag/User/operation/listUsers

#>
function Get-OktaUserId
{
    [OutputType([string])]
    param(
        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ValueFromPipelineByPropertyName = $true)]
        [ValidateNotNullOrEmpty()]
        [Alias('UserPrincipalName','UPN','UserName','Email')]
        [string] $Login
    )

    begin {
        if ($null -eq $Script:OktaToken) {
            throw 'Not connected to Okta, call Connect-Okta to get started.'
        }
    }

    process {
        try {
            Write-Debug "In Get-OktaUserId with ${Login}"

            # `filter` targets the indexed profile.login property for an exact, server-side match,
            # and `limit=1` caps the response to a single user record. ConvertTo-Json wraps the login
            # in a SCIM-compatible JSON string literal (surrounding quotes plus standard escapes).
            [string] $filterExpression = 'profile.login eq {0}' -f (ConvertTo-Json -InputObject $Login -Compress)
            [string] $usersQuery = 'filter={0}&limit=1' -f [uri]::EscapeDataString($filterExpression)
            [string] $usersPath = '/api/v1/users'

            Write-Debug ('User lookup path: ' + $usersPath)
            Write-Debug ('User lookup query: ' + $usersQuery)

            [string] $response = Invoke-OktaWebRequest -Path $usersPath `
                        -Query $usersQuery `
                        -Method ([Microsoft.PowerShell.Commands.WebRequestMethod]::Get) `
                        -ErrorAction Stop

            Write-Debug ('User lookup response: ' + $response)

            [object[]] $users = ConvertFrom-Json -InputObject $response
            if ($null -eq $users -or $users.Count -eq 0) {
                throw ('Okta user not found: {0}' -f $Login)
            }

            return [string] $users[0].id
        }
        catch {
            throw $PSItem
        }
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
The JSON Web Key used to authenticate to the Okta application, in order to obtain access token using the client credentials OAuth flow (private_key_jwt).

.PARAMETER ClientSecret
The client secret used to authenticate to the Okta application, in order to obtain access token using the client credentials OAuth flow (client_secret_post).

.PARAMETER ApiToken
A static Okta API token (SSWS). Issued from the Okta admin console under Security > API > Tokens.

.EXAMPLE
PS \> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7

.EXAMPLE
PS \> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7 -Scopes @('okta.users.manage','okta.something.else')

.EXAMPLE
PS \> $jwk = '{"kty":"RSA","kid":"EE3QB0WvhuOwR9DuR6717OERKbDrBemrDKOK4Xvbf8c","d":"TmljZSB0cnkhICBCdXQgdGhpcyBpc...'
PS \> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7 -Scopes @('okta.users.manage','okta.something.else') -JsonWebKey $jwk

.EXAMPLE
PS \> $secret = Read-Host -AsSecureString -Prompt 'Client secret'
PS \> Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7 -Scopes @('okta.users.manage') -ClientSecret $secret

.EXAMPLE
PS \> $apiToken = Read-Host -AsSecureString -Prompt 'API token'
PS \> Connect-Okta -Tenant example.okta.com -ApiToken $apiToken

.LINK
https://developer.okta.com/docs/guides/create-an-api-token/main/

.LINK
https://developer.okta.com/docs/api/openapi/okta-oauth/guides/client-auth

#>

function Connect-Okta
{
    param(
        [Parameter(Mandatory = $true, ParameterSetName = 'ClientCredentials')]
        [Parameter(Mandatory = $true, ParameterSetName = 'ClientSecret')]
        [Parameter(Mandatory = $true, ParameterSetName = 'AuthorizationCode')]
        [Parameter(Mandatory = $true, ParameterSetName = 'ApiToken')]
        [ValidatePattern('^[a-zA-Z0-9-]+\.okta(?:-emea|preview|\.mil)?\.com$')]
        [Alias('Organization','OktaOrganization','OktaDomain')]
        [string] $Tenant,

        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ParameterSetName = 'ClientCredentials')]
        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ParameterSetName = 'ClientSecret')]
        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ParameterSetName = 'AuthorizationCode')]
        [ValidatePattern('^[A-Za-z0-9_-]{20}$')]
        [string]
        $ClientId,

        [Parameter(Mandatory = $false, ParameterSetName = 'ClientCredentials')]
        [Parameter(Mandatory = $false, ParameterSetName = 'ClientSecret')]
        [Parameter(Mandatory = $false, ParameterSetName = 'AuthorizationCode')]
        [string[]] $Scopes = @('okta.users.manage'),

        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ParameterSetName = 'ClientCredentials')]
        [Alias('jwk')]
        [string]
        $JsonWebKey,

        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ParameterSetName = 'ClientSecret')]
        [ValidateNotNull()]
        [Alias('Secret')]
        [securestring]
        $ClientSecret,

        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ParameterSetName = 'ApiToken')]
        [ValidateNotNull()]
        [Alias('ApiKey','SswsToken','SSWS')]
        [securestring]
        $ApiToken
    )

    try {
        [Uri] $tenantUri = [System.UriBuilder]::new([System.Uri]::UriSchemeHttps, $Tenant).Uri
        $Script:OktaRevocationInfo = [OktaRevocationInfo]::new($ClientId)

        switch ($PSCmdlet.ParameterSetName){
            'ApiToken'
            {
                Write-Debug 'SSWS API token provided, using static API token authentication'
                [string] $plaintextApiToken = ConvertFrom-SecureStringPlainText -SecureString $ApiToken
                $Script:OktaToken = [OktaToken]::new([OktaTokenScheme]::SSWS, $plaintextApiToken, $tenantUri)
            }
            'AuthorizationCode'
            {
                Write-Debug 'No JWK found, assuming public client intended'

                [Microsoft.Identity.Client.IPublicClientApplication] $publicClientApp = [Microsoft.Identity.Client.PublicClientApplicationBuilder]::Create($ClientId).
                    WithExperimentalFeatures().
                    WithOidcAuthority($tenantUri.ToString()).
                    WithRedirectUri('http://localhost:8080/login/callback').
                    Build()

                [Microsoft.Identity.Client.AuthenticationResult] $authResult =
                    $publicClientApp.AcquireTokenInteractive($Scopes).ExecuteAsync().GetAwaiter().GetResult()
                if ($null -ne $authResult) {
                    $Script:OktaToken = [OktaToken]::new([OktaTokenScheme]::Bearer, $authResult.AccessToken, $tenantUri)
                    Write-Verbose 'Okta access token successfully retrieved.'
                }
            }
            'ClientSecret'
            {
                Write-Debug 'Client secret provided, using confidential client with client_secret_post'

                [string] $plaintextSecret = ConvertFrom-SecureStringPlainText -SecureString $ClientSecret
                $Script:OktaRevocationInfo.ClientSecret = $plaintextSecret

                [Microsoft.Identity.Client.IConfidentialClientApplication] $confidentialClientApp = [Microsoft.Identity.Client.ConfidentialClientApplicationBuilder]::Create($ClientId).
                    WithClientSecret($plaintextSecret).
                    WithOidcAuthority($tenantUri.ToString()).
                    Build()

                [Microsoft.Identity.Client.AuthenticationResult] $authResult =
                    $confidentialClientApp.AcquireTokenForClient($Scopes).ExecuteAsync().GetAwaiter().GetResult()
                if ($null -ne $authResult) {
                    $Script:OktaToken = [OktaToken]::new([OktaTokenScheme]::Bearer, $authResult.AccessToken, $tenantUri)
                    Write-Verbose 'Okta access token successfully retrieved.'
                }
            }
            'ClientCredentials'
            {
                Write-Debug 'JWK found, assuming confidential client intended'
                [Microsoft.IdentityModel.Tokens.JsonWebKey] $jwk = [Microsoft.IdentityModel.Tokens.JsonWebKey]::new($JsonWebKey)
                [Microsoft.IdentityModel.Tokens.SigningCredentials] $signingCredentials = [Microsoft.IdentityModel.Tokens.SigningCredentials]::new($jwk, 'RS256')
                [string] $issuer = $ClientId

                [System.UriBuilder] $audienceUri = [System.UriBuilder]::new([System.Uri]::UriSchemeHttps, $Tenant, -1, '/oauth2/v1/token')
                [System.UriBuilder] $revocationAudienceUri = [System.UriBuilder]::new([System.Uri]::UriSchemeHttps, $Tenant, -1, '/oauth2/v1/revoke')

                [string] $audience = $audienceUri.ToString()
                [System.Security.Claims.ClaimsIdentity] $subject = [System.Security.Claims.ClaimsIdentity]::new()
                $subject.Claims.Add([System.Security.Claims.Claim]::new('sub', $ClientId))
                [datetime] $notBefore = Get-Date
                [datetime] $expires = $notBefore.AddMinutes(60)
                [datetime] $issuedAt = $notBefore

                [System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler] $tokenHandler = [System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler]::new()
                [System.IdentityModel.Tokens.Jwt.JwtSecurityToken] $securityToken = $tokenHandler.CreateJwtSecurityToken($issuer, $audience, $subject, $notBefore, $expires, $issuedAt, $signingCredentials)
                [System.IdentityModel.Tokens.Jwt.JwtSecurityToken] $revocationToken = $tokenHandler.CreateJwtSecurityToken($issuer, $revocationAudienceUri.ToString(), $subject, $notBefore, $expires, $issuedAt, $signingCredentials)
                [string] $assertion = $tokenHandler.WriteToken($securityToken)
                $Script:OktaRevocationInfo.RevocationToken = $tokenHandler.WriteToken($revocationToken)
                [Microsoft.Identity.Client.IConfidentialClientApplication] $confidentialClientApp = [Microsoft.Identity.Client.ConfidentialClientApplicationBuilder]::Create($ClientId).
                    WithClientAssertion($assertion).
                    WithOidcAuthority($tenantUri.ToString()).
                    Build()

                [Microsoft.Identity.Client.AuthenticationResult] $authResult =
                    $confidentialClientApp.AcquireTokenForClient($Scopes).ExecuteAsync().GetAwaiter().GetResult()
                if ($null -ne $authResult -and $null -ne $Script:OktaRevocationInfo.RevocationToken) {
                    $Script:OktaToken = [OktaToken]::new([OktaTokenScheme]::Bearer, $authResult.AccessToken, $tenantUri)
                    Write-Verbose 'Okta access and revocation tokens successfully retrieved.'
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
        if ($Script:OktaToken.Scheme -eq [OktaTokenScheme]::SSWS) {
            # Static SSWS API tokens are managed in the Okta admin console; the OAuth revoke endpoint does not accept them.
            $Script:OktaToken = $null
            $Script:OktaRevocationInfo = $null
            Write-Verbose 'Okta SSWS API token cleared from the session.'
            return
        }

        [string] $revocationPath = '/oauth2/v1/revoke'

        Write-Debug ('Revocation path: ' + $revocationPath)

        [hashtable] $body = @{
            client_id = $Script:OktaRevocationInfo.ClientId
            token = $Script:OktaToken.AccessToken
            token_type_hint = 'access_token'
        }

        if ($null -ne $Script:OktaRevocationInfo.RevocationToken) {
            $body.Add('client_assertion_type','urn:ietf:params:oauth:client-assertion-type:jwt-bearer')
            $body.Add('client_assertion',$Script:OktaRevocationInfo.RevocationToken)
        }
        elseif (-not [string]::IsNullOrEmpty($Script:OktaRevocationInfo.ClientSecret)) {
            $body.Add('client_secret',$Script:OktaRevocationInfo.ClientSecret)
        }

        Write-Debug ('Revocation payload: ' + ($body | ConvertTo-Json))

        [string] $response = Invoke-OktaWebRequest -Path $revocationPath `
                    -ContentType 'application/x-www-form-urlencoded' `
                    -Body $body `
                    -ErrorAction Stop

        $Script:OktaToken = $null
        $Script:OktaRevocationInfo.RevocationToken = $null
        $Script:OktaRevocationInfo.ClientSecret = $null

        if ($response.Length -eq 0 -and $null -eq $Script:OktaToken) {
            Write-Verbose 'Okta access token successfully revoked.'
        }
    }
}


function Invoke-OktaWebRequest
{
    [OutputType([Microsoft.PowerShell.Commands.WebResponseObject])]
    param(
        [Parameter(Mandatory = $true)]
        [string] $Path,

        [Parameter(Mandatory = $false)]
        [ValidateNotNullOrEmpty()]
        [string] $Query,

        [Parameter(Mandatory = $false)]
        [Microsoft.PowerShell.Commands.WebRequestMethod] $Method = [Microsoft.PowerShell.Commands.WebRequestMethod]::Post,

        [Parameter(Mandatory = $false)]
        [ValidateNotNull()]
        $Body,

        [Parameter(Mandatory = $false)]
        [ValidateNotNullOrEmpty()]
        [string] $ContentType = 'application/json'
    )

    [hashtable] $headers = @{
        'Accept' = 'application/json'
        'Authorization' = '{0} {1}' -f $Script:OktaToken.Scheme, $Script:OktaToken.AccessToken
    }

    [System.UriBuilder] $requestUri = [System.UriBuilder]::new([System.Uri]::UriSchemeHttps, $Script:OktaToken.Tenant.Host, -1, $Path)
    if (-not [string]::IsNullOrEmpty($Query)) {
        $requestUri.Query = $Query
    }

    [hashtable] $extraParams = @{}
    if ($PSVersionTable.PSEdition -eq 'Desktop') {
        # Avoid dependency on Internet Explorer's DOM parser on Windows PowerShell.
        $extraParams['UseBasicParsing'] = $true
    }

    return Invoke-WebRequest -Uri $requestUri.ToString() `
        -Method $Method `
        -Headers $headers `
        -ContentType $ContentType `
        -Body $Body `
        @extraParams
}

# Unwraps a SecureString to a plaintext value via unmanaged memory.
# The unmanaged buffer is always zeroed, so the secret only lives in the returned managed string.
function ConvertFrom-SecureStringPlainText
{
    [OutputType([string])]
    param(
        [Parameter(Mandatory = $true)]
        [ValidateNotNull()]
        [securestring] $SecureString
    )

    [IntPtr] $ptr = [System.Runtime.InteropServices.Marshal]::SecureStringToGlobalAllocUnicode($SecureString)
    try {
        return [System.Runtime.InteropServices.Marshal]::PtrToStringUni($ptr)
    }
    finally {
        [System.Runtime.InteropServices.Marshal]::ZeroFreeGlobalAllocUnicode($ptr)
    }
}

# Functions are filtered by FunctionsToExport in the parent manifest.
