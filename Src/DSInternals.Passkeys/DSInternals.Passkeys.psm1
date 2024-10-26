# Load PS host-specific assemblies
if ($PSVersionTable.PSVersion.Major -ge 6) {
    # PowerShell Core
    Add-Type -Path "$PSScriptRoot/net6.0/DSInternals.Win32.WebAuthn.dll" -ErrorAction Stop
}
else {
    # PowerShell Desktop
    Add-Type -Path "$PSScriptRoot/net48/DSInternals.Win32.WebAuthn.dll" -ErrorAction Stop
}

<#
.SYNOPSIS
Retrieves creation options required to generate and register a Microsoft Entra ID-compatible passkey.

.PARAMETER UserId
The unique identifier of user.

.PARAMETER ChallengeTimeout
Overrides the timeout of the server-generated challenge returned in the request.
The default value is 5 minutes, with the accepted range being between 5 minutes and 30 days.

.EXAMPLE
PS \> Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
PS \> Get-PasskeyRegistrationOptions -UserId 'AdeleV@contoso.com'

.EXAMPLE
PS \> Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
PS \> Get-PasskeyRegistrationOptions -UserId 'AdeleV@contoso.com' -ChallengeTimeout (New-TimeSpan -Minutes 10)

.NOTES
Self-service operations aren't supported.
More info at https://learn.microsoft.com/en-us/graph/api/fido2authenticationmethod-creationoptions

#>
function Get-PasskeyRegistrationOptions
{
    [CmdletBinding()]
    [OutputType([DSInternals.Win32.WebAuthn.MicrosoftGraphWebauthnCredentialCreationOptions])]
    param(
        [Parameter(Mandatory = $true)]
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
        [timespan] $ChallengeTimeout = (New-TimeSpan -Minutes 5)
    )
    try {
        # Generate the user-specific URL, e.g., https://graph.microsoft.com/beta/users/af4cf208-16e0-429d-b574-2a09c5f30dea/authentication/fido2Methods/creationOptions
        [string] $credentialOptionsUrl = '{0}/beta/users/{1}/authentication/fido2Methods/creationOptions' -f (Get-MgGraphEndpoint), [uri]::EscapeDataString($UserId)

        Write-Debug ('Credential options url: ' + $credentialOptionsUrl)

        [string] $response = Invoke-MgGraphRequest -Method GET `
                                                -Uri $credentialOptionsUrl `
                                                -Body @{ challengeTimeoutInMinutes = $ChallengeTimeout.TotalMinutes } `
                                                -OutputType Json

        Write-Debug ('Credential options response: ' + $response)

        # Parse JSON response
        return [DSInternals.Win32.WebAuthn.MicrosoftGraphWebauthnCredentialCreationOptions]::Create($response)
    }
    catch {
        # TODO: PS Error Record ($PSCmdlet.ThrowTerminatingError())
        throw
    }
}

<#
.SYNOPSIS
Retrieves creation options required to generate and register an Okta-compatible passkey.

.PARAMETER Tenant
The unique identifier of Okta tenant, like 'example.okta.com'

.PARAMETER UserId
The unique identifier of Okta user, like '00ub61wm1aqmawzRC5d7'.

.PARAMETER ChallengeTimeout
Overrides the timeout of the server-generated challenge returned in the request.
The default value is 300 seconds

.PARAMETER Token
The SSWS or Bearer token from Okta with okta.users.manage permissions.

.EXAMPLE
PS \> Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -Tenant example.okta.com -Token your_okta_token

.EXAMPLE
PS \> Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -ChallengeTimeout (New-TimeSpan -Seconds 60) -Tenant example.okta.com -Token your_okta_token

.NOTES
More info at https://developer.okta.com/docs/api/openapi/okta-management/management/tag/UserFactor/#tag/UserFactor/operation/enrollFactor

#>
function Get-OktaPasskeyRegistrationOptions
{
    [CmdletBinding()]
    [OutputType([DSInternals.Win32.WebAuthn.OktaWebauthnCredentialCreationOptions])]
    param(
        [Parameter(Mandatory = $true)]
        [ValidatePattern('^[a-zA-Z0-9-]+\.okta(?:-emea|preview|\.mil)?\.com$')]
        [string] $Tenant,

        [Parameter(Mandatory = $true)]
        [Alias('User')]
        [ValidatePattern('^[A-Za-z0-9_-]{20}$')]
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
        [timespan] $ChallengeTimeout = (New-TimeSpan -Seconds 300),

        [Parameter(Mandatory = $true)]
        [ValidateLength(42, 8192)]
        [string] $Token
    )
    try {
        [string] $credentialOptionsUrl = 'https://{0}/api/v1/users/{1}/factors?tokenLifetimeSeconds={2}&activate=true' -f $Tenant, $UserId, $ChallengeTimeout.TotalSeconds

        Write-Debug ('Credential options url: ' + $credentialOptionsUrl)

        $token_type = if ($Token -match "^[A-Za-z0-9-_=]+\.[A-Za-z0-9-_=]+\.[A-Za-z0-9-_=]+$") { "Bearer" } else { "SSWS" }

        $headers = @{
            "Accept" = "application/json"
            "Authorization" = "${token_type} ${Token}"
        }

        $body = @{
            factorType = "webauthn"
            provider = "FIDO"
        }
        $body = $body | ConvertTo-Json

        [string] $response = Invoke-WebRequest -Uri $credentialOptionsUrl `
                    -Method Post `
                    -Headers $headers `
                    -ContentType "application/json" `
                    -Body $body

        Write-Debug ('Credential options response: ' + $response)

        # Parse JSON response
        $options = [DSInternals.Win32.WebAuthn.OktaWebauthnCredentialCreationOptions]::Create($response)

        # Okta appears to omit relying party id in the options, but it is required for credential creation
        # So set default to the tenant we are talking to, which is probably what the user wants anyway
        if ($options.Embedded.PublicKeyOptions.RelyingParty.Id -eq $null)
        {
            $options.Embedded.PublicKeyOptions.RelyingParty.Id = $Tenant
        }
        return $options
    }
    catch {
        # TODO: PS Error Record ($PSCmdlet.ThrowTerminatingError())
        throw
    }
}

<#
.SYNOPSIS
Registers a new passkey in Microsoft Entra ID.

.PARAMETER UserId
The unique identifier of user.

.PARAMETER Passkey
The passkey to be registered.

.PARAMETER DisplayName
Custom name given to the registered passkey.

.PARAMETER ChallengeTimeout
Overrides the timeout of the server-generated challenge returned in the request.
The default value is 5 minutes, with the accepted range being between 5 minutes and 30 days.

.EXAMPLE
PS \> Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
PS \> Register-Passkey -UserId 'AdeleV@contoso.com' -DisplayName 'YubiKey 5 Nano'

.EXAMPLE
PS \> Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
PS \> Register-Passkey -UserId 'AdeleV@contoso.com' -DisplayName 'YubiKey 5 Nano' -ChallengeTimeout (New-TimeSpan -Minutes 10)

.EXAMPLE
PS \> Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
PS \> Get-PasskeyRegistrationOptions -UserId 'AdeleV@contoso.com' | New-Passkey -DisplayName 'YubiKey 5 Nano' | Register-Passkey -UserId 'AdeleV@contoso.com'

.NOTES
More info at https://learn.microsoft.com/en-us/graph/api/authentication-post-fido2methods

#>
function Register-Passkey
{
    [CmdletBinding(DefaultParameterSetName = 'New')]
    [OutputType([Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod])]
    param(
        [Parameter(Mandatory = $true, ParameterSetName = 'Existing')]
        [Parameter(Mandatory = $true, ParameterSetName = 'New')]
        [Alias('User')]
        [string] $UserId,

        [Parameter(Mandatory = $true, ParameterSetName = 'Existing', ValueFromPipeline = $true)]
        [DSInternals.Win32.WebAuthn.MicrosoftGraphWebauthnAttestationResponse]
        $Passkey,

        [Parameter(Mandatory = $true, ParameterSetName = 'New')]
        [string] $DisplayName,

        [Parameter(Mandatory = $false, ParameterSetName = 'New')]
        [Alias('Timeout')]
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
        [timespan] $ChallengeTimeout = (New-TimeSpan -Minutes 5)
    )
    process
    {
        # TODO: Write-Error
        switch ($PSCmdlet.ParameterSetName) {
            'Existing' {
                [string] $endpoint = Get-MgGraphEndpoint

                # Generate the user-specific URL, e.g., https://graph.microsoft.com/beta/users/af4cf208-16e0-429d-b574-2a09c5f30dea/authentication/fido2Methods
                [string] $registrationUrl = '{0}/beta/users/{1}/authentication/fido2Methods' -f $endpoint, [uri]::EscapeDataString($UserId)

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
            'New' {
                [DSInternals.Win32.WebAuthn.MicrosoftGraphWebauthnCredentialCreationOptions] $registrationOptions =
                    Get-PasskeyRegistrationOptions -UserId $UserId -ChallengeTimeout $ChallengeTimeout

                [DSInternals.Win32.WebAuthn.MicrosoftGraphWebauthnAttestationResponse] $passkey =
                    New-Passkey -Options $registrationOptions -DisplayName $DisplayName

                # Recursive call with the 'Existing' parameter set
                return Register-Passkey -UserId $UserId -Passkey $passkey
            }
        }
    }
}

<#
.SYNOPSIS
Registers a new passkey in Okta.

.PARAMETER UserId
The unique identifier of user.

.PARAMETER Passkey
The passkey to be registered.

.PARAMETER ChallengeTimeout
Overrides the timeout of the server-generated challenge returned in the request.
The default value is 5 minutes, with the accepted range being between 5 minutes and 30 days.

.PARAMETER Token
The SSWS or Bearer token from Okta with okta.users.manage permissions.

.EXAMPLE
PS \> Register-OktaPasskey -UserId 00eDuihq64pgP1gVD0x7 -Tenant example.okta.com -Token your_okta_token

.EXAMPLE
PS \> Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -Tenant example.okta.com -Token your_okta_token | New-OktaPasskey | Register-OktaPasskey -Tenant example.okta.com -Token your_okta_token

.NOTES
More info at https://developer.okta.com/docs/api/openapi/okta-management/management/tag/UserFactor/#tag/UserFactor/operation/activateFactor

#>
function Register-OktaPasskey
{
    [CmdletBinding(DefaultParameterSetName = 'New')]
    [OutputType([string])]
    param(
        [Parameter(Mandatory = $false, ParameterSetName = 'Existing')]
        [Parameter(Mandatory = $true, ParameterSetName = 'New')]
        [ValidatePattern('^[A-Za-z0-9_-]{20}$')]
        [Alias('User')]
        [string] $UserId,

        [Parameter(Mandatory = $true, ParameterSetName = 'Existing', ValueFromPipeline = $true)]
        [DSInternals.Win32.WebAuthn.OktaWebauthnAttestationResponse]
        $Passkey,

        [Parameter(Mandatory = $false, ParameterSetName = 'New')]
        [Alias('Timeout')]
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
        [timespan] $ChallengeTimeout = (New-TimeSpan -Seconds 300),

        [Parameter(Mandatory = $true, ParameterSetName = 'Existing')]
        [Parameter(Mandatory = $true, ParameterSetName = 'New')]
        [ValidatePattern('^[a-zA-Z0-9-]+\.okta(?:-emea|preview|\.mil)?\.com$')]
        [string] $Tenant,

        [Parameter(Mandatory = $true, ParameterSetName = 'Existing')]
        [Parameter(Mandatory = $true, ParameterSetName = 'New')]
        [ValidateLength(42, 8192)]
        [string] $Token
    )
    process
    {
        # TODO: Write-Error
        switch ($PSCmdlet.ParameterSetName) {
            'Existing' {
                [string] $registrationUrl = 'https://{0}/api/v1/users/{1}/factors/{2}/lifecycle/activate' -f $Tenant, $Passkey.UserId, $Passkey.FactorId

                Write-Debug ('Registration URL: ' + $registrationUrl)

                $token_type = if ($Token -match "^[A-Za-z0-9-_=]+\.[A-Za-z0-9-_=]+\.[A-Za-z0-9-_=]+$") { "Bearer" } else { "SSWS" }

                $headers = @{
                    "Accept" = "application/json"
                    "Authorization" = "${token_type} ${Token}"
                }
                
                [string] $response = Invoke-WebRequest -Uri $registrationUrl `
                            -Method Post `
                            -Headers $headers `
                            -ContentType "application/json" `
                            -Body $Passkey.ToString()

                Write-Debug ('Registration response: ' + $response)

                return $response
            }
            'New' {
                [DSInternals.Win32.WebAuthn.OktaWebauthnCredentialCreationOptions] $registrationOptions =
                    Get-OktaPasskeyRegistrationOptions -Tenant $Tenant -UserId $UserId -ChallengeTimeout $ChallengeTimeout -Token $Token

                [DSInternals.Win32.WebAuthn.OktaWebauthnAttestationResponse] $passkey =
                    New-OktaPasskey -Options $registrationOptions

                # Recursive call with the 'Existing' parameter set
                return Register-OktaPasskey -UserId $UserId -Passkey $passkey -Tenant $Tenant -Token $Token
            }
        }
    }
}

<#
.SYNOPSIS
Creates a new Microsoft Entra ID-compatible passkey.

.PARAMETER Options
Options required to generate a Microsoft Entra ID-compatible passkey.

.PARAMETER DisplayName
Custom name given to the registered passkey.

.EXAMPLE
PS \> Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
PS \> Get-PasskeyRegistrationOptions -UserId 'AdeleV@contoso.com' | New-Passkey -DisplayName 'YubiKey 5 Nano' | Register-Passkey -UserId 'AdeleV@contoso.com'

#>
function New-Passkey
{
    [CmdletBinding()]
    [OutputType([DSInternals.Win32.WebAuthn.MicrosoftGraphWebauthnAttestationResponse])]
    param(
        [Parameter(Mandatory = $true, ValueFromPipeline = $true)]
        [DSInternals.Win32.WebAuthn.MicrosoftGraphWebauthnCredentialCreationOptions]
        $Options,

        [Parameter(Mandatory = $true)]
        [string] $DisplayName
    )

    process
    {
        try {
            [DSInternals.Win32.WebAuthn.WebAuthnApi] $api = [DSInternals.Win32.WebAuthn.WebAuthnApi]::new()
            [DSInternals.Win32.WebAuthn.PublicKeyCredential] $credential = $api.AuthenticatorMakeCredential($Options.PublicKeyOptions)
            return [DSInternals.Win32.WebAuthn.MicrosoftGraphWebauthnAttestationResponse]::new($credential, $DisplayName)
        }
        catch {
            # TODO: PS Error Record (Write-Error)
            throw
        }
    }
}

<#
.SYNOPSIS
Creates a new Okta-compatible passkey.

.PARAMETER Options
Options required to generate an Okta-compatible passkey.

.EXAMPLE
PS \> New-OktaPasskey -Options $options

.EXAMPLE
PS \> Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -Tenant example.okta.com -Token your_okta_token | New-OktaPasskey 

#>
function New-OktaPasskey
{
    [CmdletBinding()]
    [OutputType([DSInternals.Win32.WebAuthn.OktaWebauthnAttestationResponse])]
    param(
        [Parameter(Mandatory = $true, ValueFromPipeline = $true)]
        [DSInternals.Win32.WebAuthn.OktaWebauthnCredentialCreationOptions]
        $Options
    )

    process
    {
        try {
            [DSInternals.Win32.WebAuthn.WebAuthnApi] $api = [DSInternals.Win32.WebAuthn.WebAuthnApi]::new()
            [DSInternals.Win32.WebAuthn.PublicKeyCredential] $credential = $api.AuthenticatorMakeCredential($Options.Embedded.PublicKeyOptions)
            return [DSInternals.Win32.WebAuthn.OktaWebauthnAttestationResponse]::new($credential, $Options.Embedded.PublicKeyOptions.User.Id, $Options.Id)
        }
        catch {
            # TODO: PS Error Record (Write-Error)
            throw
        }
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

    [Microsoft.Graph.PowerShell.Authentication.AuthContext] $context = Get-MgContext -ErrorAction Stop

    if($null -ne $context) {
        return (Get-MgEnvironment -Name $context.Environment -ErrorAction Stop).GraphEndpoint
    }
    else {
        # TODO: PS Error Record ($PSCmdlet.ThrowTerminatingError())
        throw 'Not connected to Microsoft Graph.'
    }
}

function Get-OktaAuthorizationCodeUrl
{
    [CmdletBinding()]
    [OutputType([string])]
    param(
        [Parameter(Mandatory = $true, ValueFromPipeline = $true)]
        [string]
        $Tenant,

        [Parameter(Mandatory = $true)]
        [string]
        $Client_Id,

        [Parameter(Mandatory = $true, ValueFromPipeline = $true)]
        [string]
        $State,

        [Parameter(Mandatory = $true, ValueFromPipeline = $true)]
        [string]
        $Code_Verifier        
    )

    try {
        # Start building uri for token endpoint
        $uriBuilder = [System.UriBuilder]::new("https",$Tenant,-1,"oauth2/v1/authorize")
        
        # Hash the code verifier UTF8 bytes with SHA256, convert the digest to base64url, and collect the first 44 characters as the code_challenge
        $hashAlgo = [System.Security.Cryptography.HashAlgorithm]::Create('sha256')
        $hash = $hashAlgo.ComputeHash([System.Text.Encoding]::UTF8.GetBytes($Code_Verifier))
        $b64Hash = [System.Convert]::ToBase64String($hash)
        $code_challenge = $b64Hash.Substring(0, 43).Replace("/","_").Replace("+","-").Replace("=","")

        $queryParams = @{
            client_id = $Client_Id
            response_type = "code"
            scope = "okta.users.manage"
            redirect_uri = "http://localhost:8080/login/callback/"
            state = $State
            code_challenge_method = "S256"
            code_challenge = $code_challenge
        }

        # Put together the query string from the hashtable
        $uriBuilder.Query = ($queryParams.GetEnumerator() | ForEach-Object { "$($_.Key)=$($_.Value)" }) -join "&"
        
        return $uriBuilder.ToString()
    }
    catch {
        # TODO: PS Error Record (Write-Error)
        throw
    }
}

function Invoke-AuthorizationCodeListener
{
    param (
        [Parameter(Mandatory = $true, ValueFromPipeline = $true)]
        [string]
        $State
    )

    process 
    {
        try {
            $listener = New-Object System.Net.HttpListener
            $listener.Prefixes.Add("http://localhost:8080/login/callback/")
            $listener.Start()
            $context = $listener.getContext()
            # Verify state matches the state we sent
            $valid = $context.Request.QueryString["state"] -eq $State
            # Save code
            $code = $context.Request.QueryString["code"]
            $response = $context.response
            $webPageResponse = "<!DOCTYPE html><html lang=`"en`"><head><title>Login Successful</title></head><body>It's now safe to close this browser window.</body></html>"
            $webPageResponseEncoded =  [System.Text.Encoding]::UTF8.GetBytes($webPageResponse)
            $webPageResponseLength = $webPageResponseEncoded.Length
            $response.ContentLength64 = $webPageResponseLength
            $response.ContentType = "text/html; charset=UTF-8"
            $response.StatusCode = 200
            $response.OutputStream.Write($webPageResponseEncoded, 0, $webPageResponseLength)
            $response.OutputStream.Close()
            $response.Close()
            $listener.Stop()
            # Check state validity
            if ($valid)
            {
                return $code
            }
            else 
            { 
                throw [System.InvalidOperationException] "Invalid state."
            } 
        }
        catch {
            # TODO: PS Error Record (Write-Error)
            throw
        }
    }
}

function Get-OktaTokenUrl
{
    [CmdletBinding()]
    [OutputType([string])]
    param(
        [Parameter(Mandatory = $true, ValueFromPipeline = $true)]
        [string]
        $Tenant,

        [Parameter(Mandatory = $true)]
        [string]
        $Client_Id,

        [Parameter(Mandatory = $true, ValueFromPipeline = $true)]
        [string]
        $Code,

        [Parameter(Mandatory = $true, ValueFromPipeline = $true)]
        [string]
        $Code_Verifier        
    )

    try {
        # Start building uri for token endpoint
        $uriBuilder = [System.UriBuilder]::new("https",$Tenant,-1,"oauth2/v1/token")
        
        $queryParams = @{
            grant_type = "authorization_code"
            redirect_uri = "http://localhost:8080/login/callback/"
            client_id = $Client_Id
            code = $Code
            code_verifier = $Code_Verifier
        }

        # Put together the query string from the hashtable
        $uriBuilder.Query = ($queryParams.GetEnumerator() | ForEach-Object { "$($_.Key)=$($_.Value)" }) -join "&"
        
        return $uriBuilder.ToString()
    }
    catch {
        # TODO: PS Error Record (Write-Error)
        throw        
    }
}

<#
.SYNOPSIS
Authenticates user using the OAuth2 authorization code flow with PKCE, returning a bearer token with scope "okta.users.manage".

.PARAMETER Tenant
The unique identifier of Okta tenant, like 'example.okta.com'

.PARAMETER Client_Id
The unique identifier of an Okta native or single-page application within the supplied Okta tenant, with the (default) grant type "Authorization Code" enabled, Proof Key for Code Exchange (PKCE) enabled, the (default) sign-in redirect URI containing "http://localhost:8080/login/callback", the Okta API scope "okta.users.manage" granted, and the application assigned to the user running the script.

.EXAMPLE
PS \> $token = Get-OktaBearerToken -Tenant example.okta.com -Client_Id 0oakmj8hvxvtvCy3P5d7

.NOTES
More info at https://developer.okta.com/docs/guides/implement-grant-type/authcodepkce/main/

.RELATED LINKS
https://auth0.com/docs/get-started/authentication-and-authorization-flow/authorization-code-flow-with-pkce

#>
function Get-OktaBearerToken
{
    param(
        [Parameter(Mandatory = $true, ValueFromPipeline = $true)]
        [ValidatePattern('^[a-zA-Z0-9-]+\.okta(?:-emea|preview|\.mil)?\.com$')]
        [string]
        $Tenant,

        [Parameter(Mandatory = $true, ValueFromPipeline = $true)]
        [ValidatePattern('^[A-Za-z0-9_-]{20}$')]
        [string]
        $Client_Id
    )

    process 
    {
        try {
            # Used to verify the server returns the authorization code to the caller with matching state
            $state = (New-Guid).ToString()

            # Used to ensure the client retrieving the token is the same as the client who retrieved the authorization code
            $code_verifier = -join (((48..57) * 4) + ((65..90) * 4) + ((97..122) * 4) | Get-Random -Count 43 | ForEach-Object { [char]$_ })

            # Build url to obtain authorization code from
            $authorizationCodeUrl = Get-OktaAuthorizationCodeUrl -Tenant $tenant -Client_Id $Client_Id -State $state -Code_Verifier $code_verifier

            # TODO: Switch this to something better...webview 800x600?
            # Launch browser to authorization code url
            Start-Process $authorizationCodeUrl

            # Start http listener for the sign-in redirect on the callback uri, verify state and collect authorization code
            $code = Invoke-AuthorizationCodeListener -State $state

            # Build token url to obtain bearer token using authorization code, and code verifier
            $tokenUrl = Get-OktaTokenUrl -Tenant $tenant -Client_Id $Client_Id -Code $code -Code_Verifier $code_verifier

            # Retrieve bearer token
            $tokenResponse = Invoke-WebRequest -Method Post -Uri $tokenUrl

            # Verify token type
            $token = ($tokenResponse.Content | ConvertFrom-Json)
            if ($token.token_type -eq "Bearer")
            {
                return $token.access_token
            }
            else
            { 
                throw [System.InvalidOperationException] "Invalid token type."
            }
        }
        catch {
            # TODO: PS Error Record (Write-Error)
            throw
        }
    }
}

New-Alias -Name Register-MgUserAuthenticationFido2Method -Value Register-Passkey

Export-ModuleMember -Function 'Get-PasskeyRegistrationOptions','Get-OktaPasskeyRegistrationOptions','New-Passkey','New-OktaPasskey','Register-Passkey','Register-OktaPasskey','Get-OktaBearerToken' `
                    -Alias 'Register-MgUserAuthenticationFido2Method'
