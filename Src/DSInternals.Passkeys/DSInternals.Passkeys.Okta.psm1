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

.DESCRIPTION
Retrieves a server-issued challenge and the associated WebAuthn parameters needed to register (attest) a new passkey for the specified Okta user. The returned object can be piped to New-Passkey to drive the local authenticator and then to Register-OktaPasskey to complete enrollment.

For end-to-end passkey registration in Okta, calling Register-OktaPasskey directly is recommended; it performs the challenge request, authenticator ceremony, and activation in a single step. Use Get-OktaPasskeyRegistrationOptions only when you need to inspect or customize the intermediate options.

Requires an active Okta connection (Connect-Okta).

.PARAMETER UserId
The unique identifier of the Okta user.

.PARAMETER Login
The Okta user login (typically an email address such as 'user@example.com'). Resolved to a UserId through an API call.

.PARAMETER ChallengeTimeout
Overrides the timeout of the server-generated challenge returned in the request. The default value is 5 minutes, with the accepted range being between 1 second and 1 day.

.EXAMPLE
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7

Fetches default creation options for the specified Okta user, identified by their Okta id.

.EXAMPLE
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
Get-OktaPasskeyRegistrationOptions -Login 'user@example.com'

Resolves the Okta user by login and then fetches creation options, avoiding the need to look up the Okta id manually.

.EXAMPLE
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -ChallengeTimeout (New-TimeSpan -Minutes 1)

Fetches creation options with a shorter 1-minute challenge timeout to tighten the registration window.

.EXAMPLE
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
Get-OktaPasskeyRegistrationOptions -Login 'user@example.com' | New-Passkey | Register-OktaPasskey

Performs end-to-end passkey registration in Okta in a single pipeline.

.LINK
Register-OktaPasskey

.LINK
New-Passkey

.LINK
Connect-Okta

.LINK
https://developer.okta.com/docs/api/openapi/okta-management/management/tag/UserFactor/#tag/UserFactor/operation/enrollFactor

#>
function Get-OktaPasskeyRegistrationOptions
{
    [CmdletBinding(DefaultParameterSetName = 'UserId')]
    [OutputType([DSInternals.Win32.WebAuthn.Okta.OktaWebauthnCredentialCreationOptions])]
    param(
        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ParameterSetName = 'UserId')]
        [ValidatePattern('^[A-Za-z0-9_-]{20}$')]
        [Alias('User')]
        [string] $UserId,

        [Parameter(Mandatory = $true, ParameterSetName = 'Login')]
        [ValidateNotNullOrEmpty()]
        [Alias('UserPrincipalName','UPN','UserName','Email')]
        [string] $Login,

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
            $PSCmdlet.ThrowTerminatingError(
                [System.Management.Automation.ErrorRecord]::new(
                    [System.InvalidOperationException]::new('Not connected to Okta, call Connect-Okta to get started.'),
                    'NotConnectedToOkta',
                    [System.Management.Automation.ErrorCategory]::ConnectionError,
                    $null))
        }
    }
    process {
        try
        {
            if ($PSCmdlet.ParameterSetName -eq 'Login') {
                $UserId = Get-OktaUserId -Login $Login -ErrorAction Stop
            }

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

.DESCRIPTION
Registers a new passkey for the specified user in Okta by submitting the attestation that activates the corresponding webauthn factor.

The cmdlet supports three usage patterns:
- Pass only -UserId to perform the full ceremony end-to-end: request a challenge, drive the local authenticator, and submit the attestation.
- Pipe an attestation from a previous New-Passkey call against Okta options.
- Pass -UserId, -FactorId, and a raw -AttestationPublicKeyCredential when the challenge was issued and the credential ceremony was run separately.

Requires an active Okta connection (Connect-Okta).

.PARAMETER UserId
The unique identifier of the Okta user.

.PARAMETER Login
The Okta user login (typically an email address such as 'user@example.com'). Resolved to a UserId through an API call.

.PARAMETER ChallengeTimeout
Overrides the timeout of the server-generated challenge returned in the request. The default value is 5 minutes, with the accepted range being between 1 second and 1 day.

.PARAMETER Passkey
The passkey to be registered.

.PARAMETER FactorId
The Okta factor identifier returned by Get-OktaPasskeyRegistrationOptions, used together with -AttestationPublicKeyCredential as an alternative to -Passkey.

.PARAMETER AttestationPublicKeyCredential
The raw attestation credential produced by the local WebAuthn authenticator (e.g. via New-Passkey), used together with -UserId and -FactorId as an alternative to -Passkey.

.EXAMPLE
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
Register-OktaPasskey -UserId 00eDuihq64pgP1gVD0x7

Performs the full registration ceremony in one step: enrolls a webauthn factor, prompts the local authenticator, and activates the factor in Okta.

.EXAMPLE
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
Register-OktaPasskey -Login 'user@example.com'

Resolves the Okta user by login and then performs the full registration ceremony, avoiding the need to look up the Okta id manually.

.EXAMPLE
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
Register-OktaPasskey -UserId 00eDuihq64pgP1gVD0x7 -ChallengeTimeout (New-TimeSpan -Minutes 1)

Registers a passkey using a shorter 1-minute challenge timeout to tighten the registration window.

.EXAMPLE
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 | New-Passkey | Register-OktaPasskey

Splits the registration into explicit pipeline stages: enroll the factor, create the credential locally, and activate. Equivalent to the single-step form but lets the caller inspect intermediate values.

.EXAMPLE
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
$options = Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7
$credential = New-Passkey -Options $options.PublicKeyOptions
Register-OktaPasskey -UserId 00eDuihq64pgP1gVD0x7 -FactorId $options.FactorId -AttestationPublicKeyCredential $credential

Drives the WebAuthn ceremony with a raw AttestationPublicKeyCredential and assembles the activation manually. Useful when the credential was produced outside of an Okta-aware pipeline.

.LINK
Get-OktaPasskeyRegistrationOptions

.LINK
New-Passkey

.LINK
Connect-Okta

.LINK
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

        [Parameter(Mandatory = $true, ParameterSetName = 'NewByLogin')]
        [Parameter(Mandatory = $true, ParameterSetName = 'AttestationCredentialByLogin')]
        [ValidateNotNullOrEmpty()]
        [Alias('UserPrincipalName','UPN','UserName','Email')]
        [string] $Login,

        [Parameter(Mandatory = $true, ParameterSetName = 'Existing', ValueFromPipeline = $true)]
        [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnAttestationResponse]
        $Passkey,

        [Parameter(Mandatory = $true, ParameterSetName = 'AttestationCredential')]
        [Parameter(Mandatory = $true, ParameterSetName = 'AttestationCredentialByLogin')]
        [ValidatePattern('^[A-Za-z0-9_-]{20}$')]
        [Alias('Factor')]
        [string] $FactorId,

        [Parameter(Mandatory = $true, ParameterSetName = 'AttestationCredential', ValueFromPipeline = $true)]
        [Parameter(Mandatory = $true, ParameterSetName = 'AttestationCredentialByLogin', ValueFromPipeline = $true)]
        [Alias('Attestation','Credential')]
        [DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential]
        $AttestationPublicKeyCredential,

        [Parameter(Mandatory = $false, ParameterSetName = 'New')]
        [Parameter(Mandatory = $false, ParameterSetName = 'NewByLogin')]
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

    begin {
        if ($null -eq $Script:OktaToken) {
            $PSCmdlet.ThrowTerminatingError(
                [System.Management.Automation.ErrorRecord]::new(
                    [System.InvalidOperationException]::new('Not connected to Okta, call Connect-Okta to get started.'),
                    'NotConnectedToOkta',
                    [System.Management.Automation.ErrorCategory]::ConnectionError,
                    $null))
        }
    }

    process {
        try {
            # Translate login to ID
            if ($PSCmdlet.ParameterSetName -in 'NewByLogin','AttestationCredentialByLogin') {
                $UserId = Get-OktaUserId -Login $Login -ErrorAction Stop
            }


            if ($PSCmdlet.ParameterSetName -in 'New','NewByLogin') {
                # Fetch the challenge
                [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnCredentialCreationOptions] $options =
                    Get-OktaPasskeyRegistrationOptions -UserId $UserId -ChallengeTimeout $ChallengeTimeout -ErrorAction Stop

                # Display the passkey registration prompt
                [DSInternals.Win32.WebAuthn.WebAuthnApi] $api = [DSInternals.Win32.WebAuthn.WebAuthnApi]::new()
                [DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential] $credential =
                    $api.AuthenticatorMakeCredential($options.PublicKeyOptions)

                $FactorId = $options.FactorId
                $Passkey = [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnAttestationResponse]::new(
                    $credential, $UserId, $FactorId)
            }
            elseif ($PSCmdlet.ParameterSetName -in 'AttestationCredential','AttestationCredentialByLogin') {
                $Passkey = [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnAttestationResponse]::new(
                    $AttestationPublicKeyCredential, $UserId, $FactorId)
            }
            else {
                # 'Existing' parameter set: $Passkey was bound from the pipeline
                $UserId = $Passkey.UserId
                $FactorId = $Passkey.FactorId
            }

            # Activate the factor with the attestation response
            [string] $registrationPath = "/api/v1/users/${UserId}/factors/${FactorId}/lifecycle/activate"

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
Resolves an Okta user login to its Okta user id.

.DESCRIPTION
Queries the Okta users endpoint with a server-side filter on profile.login to return the first matching user's Okta id. Throws if no user matches the supplied login.

Useful as a bridge when callers know the user's login (email) but the downstream cmdlets, like Register-OktaPasskey, require the Okta id. Requires an active Okta connection (Connect-Okta).

.PARAMETER Login
The Okta user login (typically an email address such as 'user@example.com').

.EXAMPLE
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
Get-OktaUserId -Login 'user@example.com'

Looks up the Okta id for the user with the given login and returns it as a string.

.EXAMPLE
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7
$userId = Get-OktaUserId -Login 'user@example.com'
Register-OktaPasskey -UserId $userId

Resolves the login to an Okta id and reuses it to register a passkey.

.LINK
Connect-Okta

.LINK
Get-OktaPasskeyRegistrationOptions

.LINK
Register-OktaPasskey

.LINK
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
            $PSCmdlet.ThrowTerminatingError(
                [System.Management.Automation.ErrorRecord]::new(
                    [System.InvalidOperationException]::new('Not connected to Okta, call Connect-Okta to get started.'),
                    'NotConnectedToOkta',
                    [System.Management.Automation.ErrorCategory]::ConnectionError,
                    $null))
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

.DESCRIPTION
Acquires an Okta access token via one of four authentication flows, depending on which parameters are supplied, and caches it for subsequent cmdlets in this module:
- Interactive authorization code (public client) when only -Tenant and -ClientId are supplied.
- Client credentials with private_key_jwt when -JsonWebKey is supplied.
- Client credentials with client_secret_post when -ClientSecret is supplied.
- Static API token (SSWS) when -ApiToken is supplied.

The cached token is reused by Get-OktaPasskeyRegistrationOptions, Register-OktaPasskey, and Disconnect-Okta. Call Disconnect-Okta to revoke the token (for OAuth flows) or clear it from the session (for SSWS).

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
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7

Connects to the `example.okta.com` tenant using the application with client id `0oakmj8hvxvtvCy3P5d7` via the authorization code flow with PKCE.

.EXAMPLE
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7 -Scopes @('okta.users.manage','okta.something.else')

Connects to the `example.okta.com` tenant using the application with client id `0oakmj8hvxvtvCy3P5d7` via the authorization code flow with PKCE, requesting scopes `'okta.users.manage'` and `'okta.something.else'`.

.EXAMPLE
$jwk = '{"kty":"RSA","kid":"EE3QB0WvhuOwR9DuR6717OERKbDrBemrDKOK4Xvbf8c","d":"TmljZSB0cnkhICBCdXQgdGhpcyBpc...'
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7 -Scopes @('okta.users.manage','okta.something.else') -JsonWebKey $jwk

Connects to the `example.okta.com` tenant using the application with client id `0oakmj8hvxvtvCy3P5d7` via the client credentials flow with private_key_jwt, signing the client assertion with `$jwk` and requesting scopes `'okta.users.manage'` and `'okta.something.else'`.

.EXAMPLE
$secret = Read-Host -AsSecureString -Prompt 'Client secret'
Connect-Okta -Tenant example.okta.com -ClientId 0oakmj8hvxvtvCy3P5d7 -Scopes @('okta.users.manage') -ClientSecret $secret

Connects to the `example.okta.com` tenant using the application with client id `0oakmj8hvxvtvCy3P5d7` via the client credentials flow with client_secret_post, authenticating with the SecureString-protected `$secret`.

.EXAMPLE
$apiToken = Read-Host -AsSecureString -Prompt 'API token'
Connect-Okta -Tenant example.okta.com -ApiToken $apiToken

Connects to the `example.okta.com` tenant using a static SSWS API token issued in the Okta admin console, bypassing the OAuth flow entirely.

.LINK
Disconnect-Okta

.LINK
Register-OktaPasskey

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

.DESCRIPTION
Revokes the Okta access token cached from the call to Connect-Okta and clears it from the session. For OAuth-issued Bearer tokens, this calls the /oauth2/v1/revoke endpoint using the same client authentication method that was used to obtain the token (client_assertion or client_secret). For static SSWS API tokens, the cached token is simply discarded from the session because Okta does not expose a revoke endpoint for static tokens; revocation for those is managed in the Okta admin console.

If no token is cached, this cmdlet is a no-op.

.EXAMPLE
Disconnect-Okta

Revokes the cached OAuth access token (or clears the cached SSWS token) and removes any associated revocation state from the session.

.LINK
Connect-Okta

.LINK
Register-OktaPasskey

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
