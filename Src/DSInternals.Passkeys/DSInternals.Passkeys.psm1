# Load PS host-specific assemblies
if ($PSVersionTable.PSVersion.Major -ge 6) {
    # PowerShell Core
    Add-Type -Path "$PSScriptRoot/net6.0/DSInternals.Win32.WebAuthn.dll" -ErrorAction Stop
}
else {
    # PowerShell Desktop
    Add-Type -Path "$PSScriptRoot/net48/DSInternals.Win32.WebAuthn.dll" -ErrorAction Stop
}

# Needed for [Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod] type
Import-Module Microsoft.Graph.Identity.SignIns -ErrorAction Stop

<#
.SYNOPSIS
Retrieves creation options required to generate and register a Microsoft Entra ID or Okta compatible passkey.

.PARAMETER Tenant
The unique identifier of Okta tenant.

.PARAMETER UserId
The unique identifier of user.  For Entra ID, this is the object id (guid) or UPN.  For Okta, this is the unique identifier of Okta user.

.PARAMETER ChallengeTimeout
Overrides the timeout of the server-generated challenge returned in the request.  For Entra ID, the default value is 5 minutes, with the accepted range being between 5 minutes and 30 days.  For Okta, the default value is 300 second, with the accepted range being between 1 second and 1 day.

.PARAMETER Token
The SSWS or Bearer token from Okta with okta.users.manage permissions.

.EXAMPLE
PS \> Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
PS \> Get-PasskeyRegistrationOptions -UserId 'AdeleV@contoso.com'

.EXAMPLE
PS \> Connect-MgGraph -Scopes 'UserAuthenticationMethod.ReadWrite.All'
PS \> Get-PasskeyRegistrationOptions -UserId 'AdeleV@contoso.com' -ChallengeTimeout (New-TimeSpan -Minutes 10)

.EXAMPLE
PS \> Get-PasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -Tenant example.okta.com -Token your_okta_token

.EXAMPLE
PS \> Get-PasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -ChallengeTimeout (New-TimeSpan -Seconds 60) -Tenant example.okta.com -Token your_okta_token

.NOTES
Self-service operations aren't supported.
More info about Entra ID at https://learn.microsoft.com/en-us/graph/api/fido2authenticationmethod-creationoptions
More info about Okta at https://developer.okta.com/docs/api/openapi/okta-management/management/tag/UserFactor/#tag/UserFactor/operation/enrollFactor

#>
function Get-PasskeyRegistrationOptions
{
    [CmdletBinding(DefaultParameterSetName = 'EntraID')]
    [OutputType([DSInternals.Win32.WebAuthn.EntraID.MicrosoftGraphWebauthnCredentialCreationOptions],ParameterSetName = 'EntraID')]
    [OutputType([DSInternals.Win32.WebAuthn.Okta.OktaWebauthnCredentialCreationOptions], ParameterSetName = 'Okta')]
    param(
        [Parameter(Mandatory = $true, ParameterSetName = 'Okta')]
        [ValidatePattern('^[a-zA-Z0-9-]+\.okta(?:-emea|preview|\.mil)?\.com$')]
        [string] $Tenant,
        
        [Parameter(Mandatory = $true, ParameterSetName = 'EntraID')]
        [Parameter(Mandatory = $true, ParameterSetName = 'Okta')]
        [Alias('User')]
        [string] $UserId,

        [Parameter(Mandatory = $false, ParameterSetName = 'EntraID')]
        [Parameter(Mandatory = $false, ParameterSetName = 'Okta')]
        [Alias('Timeout')]
        [timespan] $ChallengeTimeout = (New-TimeSpan -Minutes 5),

        [Parameter(Mandatory = $true, ParameterSetName = 'Okta')]
        [ValidateLength(42, 8192)]
        [string] $Token
    )
    begin {
        switch ($PSCmdlet.ParameterSetName)
        {
            'EntraID' {
                $min = New-TimeSpan -Minutes 5
                $max = New-TimeSpan -Minutes 43200
                if ($ChallengeTimeout -gt $max -or $ChallengeTimeout -lt $min) {
                    Write-Error "Cannot validate argument on parameter 'ChallengeTimeout' which must be a valid TimeSpan between 5 and 43200 minutes for $_." -ErrorAction Stop
                }
                if ($UserId -notmatch "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$" -and $false -eq [guid]::TryParse($UserId, $([ref][guid]::Empty))) {
                    Write-Error "Cannot validate argument on parameter 'UserID' which must be an object id or UPN for $_." -ErrorAction Stop
                }
            }
            'Okta'{
                $min = New-TimeSpan -Seconds 1
                $max = New-TimeSpan -Seconds 86400
                if ($ChallengeTimeout -gt $max -or $ChallengeTimeout -lt $min) {
                    Write-Error "Cannot validate argument on parameter 'ChallengeTimeout' which must be a valid TimeSpan between 1 and 86400 seconds for $_." -ErrorAction Stop
                }
                if ($UserId -notmatch "^[A-Za-z0-9_-]{20}$") {
                    Write-Error "Cannot validate argument on parameter 'UserID' which must the unique idenitier for the user for $_." -ErrorAction Stop
                }
            }
        }
    }
    process {
        try {
            switch ($PSCmdlet.ParameterSetName) {
                'EntraID' {
                    # Generate the user-specific URL, e.g., https://graph.microsoft.com/beta/users/af4cf208-16e0-429d-b574-2a09c5f30dea/authentication/fido2Methods/creationOptions
                    [string] $credentialOptionsUrl = '{0}/beta/users/{1}/authentication/fido2Methods/creationOptions' -f (Get-MgGraphEndpoint), [uri]::EscapeDataString($UserId)

                    Write-Debug ('Credential options url: ' + $credentialOptionsUrl)

                    [string] $response = Invoke-MgGraphRequest -Method GET `
                                                            -Uri $credentialOptionsUrl `
                                                            -Body @{ challengeTimeoutInMinutes = $ChallengeTimeout.TotalMinutes } `
                                                            -OutputType Json

                    Write-Debug ('Credential options response: ' + $response)

                    # Parse JSON response
                    return [DSInternals.Win32.WebAuthn.EntraID.MicrosoftGraphWebauthnCredentialCreationOptions]::Create($response)
                }
                'Okta' {
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
                    $options = [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnCredentialCreationOptions]::Create($response)
            
                    # Okta appears to omit relying party id in the options, but it is required for credential creation
                    # So set default to the tenant we are talking to, which is probably what the user wants anyway
                    if ($null -eq $options.Embedded.PublicKeyOptions.RelyingParty.Id)
                    {
                        $options.Embedded.PublicKeyOptions.RelyingParty.Id = $Tenant
                    }
                    return $options                    
                }
            }
        }
        catch {
            # TODO: PS Error Record ($PSCmdlet.ThrowTerminatingError())
            throw
        }
    }
}

<#
.SYNOPSIS
Registers a new passkey in Microsoft Entra ID, or Okta.

.PARAMETER Tenant
The unique identifier of Okta tenant.

.PARAMETER UserId
The unique identifier of user.  For Entra ID, this is the object id (guid) or UPN.  For Okta, this is the unique identifier of Okta user.

.PARAMETER ChallengeTimeout
Overrides the timeout of the server-generated challenge returned in the request.  For Entra ID, the default value is 5 minutes, with the accepted range being between 5 minutes and 30 days.  For Okta, the default value is 300 second, with the accepted range being between 1 second and 1 day.

.PARAMETER Token
The SSWS or Bearer token from Okta with okta.users.manage permissions.

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
PS \> Register-Passkey -UserId 00eDuihq64pgP1gVD0x7 -Tenant example.okta.com -Token your_okta_token

.EXAMPLE
PS \> Get-PasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -Tenant example.okta.com -Token your_okta_token | New-Passkey | Register-Passkey -Tenant example.okta.com -Token your_okta_token

.NOTES
More info for Entra ID at https://learn.microsoft.com/en-us/graph/api/authentication-post-fido2methods
More info for Okta at https://developer.okta.com/docs/api/openapi/okta-management/management/tag/UserFactor/#tag/UserFactor/operation/activateFactor

#>
function Register-Passkey
{
    [CmdletBinding(DefaultParameterSetName = 'EntraIDNew')]
    [OutputType([DSInternals.Win32.WebAuthn.Okta.OktaFido2AuthenticationMethod], ParameterSetName = 'OktaNew')]
    [OutputType([DSInternals.Win32.WebAuthn.Okta.OktaFido2AuthenticationMethod], ParameterSetName = 'OktaExisting')]
    [OutputType([Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod] , ParameterSetName = 'EntraIDExisting')]
    [OutputType([Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod] , ParameterSetName = 'EntraIDNew')]
    param(
        [Parameter(Mandatory = $true, ParameterSetName = 'EntraIDExisting')]
        [Parameter(Mandatory = $true, ParameterSetName = 'EntraIDNew')]
        [Parameter(Mandatory = $true, ParameterSetName = 'OktaExisting')]
        [Parameter(Mandatory = $true, ParameterSetName = 'OktaNew')]
        [Alias('User')]
        [string] $UserId,

        [Parameter(Mandatory = $true, ParameterSetName = 'EntraIDExisting', ValueFromPipeline = $true)]
        [Parameter(Mandatory = $true, ParameterSetName = 'OktaExisting', ValueFromPipeline = $true)]
        $Passkey,

        [Parameter(Mandatory = $true, ParameterSetName = 'EntraIDNew')]
        [string] $DisplayName,

        [Parameter(Mandatory = $false, ParameterSetName = 'EntraIDNew')]
        [Alias('Timeout')]
        [timespan] $ChallengeTimeout = (New-TimeSpan -Minutes 5),

        [Parameter(Mandatory = $true, ParameterSetName = 'OktaExisting')]
        [Parameter(Mandatory = $true, ParameterSetName = 'OktaNew')]
        [ValidatePattern('^[a-zA-Z0-9-]+\.okta(?:-emea|preview|\.mil)?\.com$')]
        [string] $Tenant,

        [Parameter(Mandatory = $true, ParameterSetName = 'OktaExisting')]
        [Parameter(Mandatory = $true, ParameterSetName = 'OktaNew')]
        [ValidateLength(42, 8192)]
        [string] $Token
    )
    begin {
        switch ($PSCmdlet.ParameterSetName)
        {
            'EntraIDExisting' {
                [DSInternals.Win32.WebAuthn.EntraID.MicrosoftGraphWebauthnAttestationResponse]$Passkey = $Passkey
            }
            'EntraIDNew' {
                $min = New-TimeSpan -Minutes 5
                $max = New-TimeSpan -Minutes 43200
                if ($ChallengeTimeout -gt $max -or $ChallengeTimeout -lt $min) {
                    Write-Error "Cannot validate argument on parameter 'ChallengeTimeout' which must be a valid TimeSpan between 5 and 43200 minutes for $_." -ErrorAction Stop
                }
                if ($UserId -notmatch "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$" -and $false -eq [guid]::TryParse($UserId, $([ref][guid]::Empty))) {
                    Write-Error "Cannot validate argument on parameter 'UserID' which must be an object id or UPN for $_." -ErrorAction Stop
                }
            }
            'OktaExisting' {
                [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnAttestationResponse]$Passkey = $Passkey
            }
            'OktaNew'{
                $min = New-TimeSpan -Seconds 1
                $max = New-TimeSpan -Seconds 86400
                if ($ChallengeTimeout -gt $max -or $ChallengeTimeout -lt $min) {
                    Write-Error "Cannot validate argument on parameter 'ChallengeTimeout' which must be a valid TimeSpan between 1 and 86400 seconds for $_." -ErrorAction Stop
                }
                if ($UserId -notmatch "^[A-Za-z0-9_-]{20}$") {
                    Write-Error "Cannot validate argument on parameter 'UserID' which must the unique idenitier for the user for $_." -ErrorAction Stop
                }
            }
        }
    }
    process
    {
        # TODO: Write-Error
        switch ($PSCmdlet.ParameterSetName) {
            'EntraIDExisting' {
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
            'EntraIDNew' {
                [DSInternals.Win32.WebAuthn.EntraID.MicrosoftGraphWebauthnCredentialCreationOptions] $registrationOptions =
                    Get-PasskeyRegistrationOptions -UserId $UserId -ChallengeTimeout $ChallengeTimeout

                [DSInternals.Win32.WebAuthn.EntraID.MicrosoftGraphWebauthnAttestationResponse] $passkey =
                    New-Passkey -Options $registrationOptions -DisplayName $DisplayName

                # Recursive call with the 'Existing' parameter set
                return Register-Passkey -UserId $UserId -Passkey $passkey
            }
            'OktaExisting' {
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

                return [DSInternals.Win32.WebAuthn.Okta.OktaFido2AuthenticationMethod]::FromJsonString($response)
            }
            'OktaNew' {
                [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnCredentialCreationOptions] $registrationOptions =
                    Get-PasskeyRegistrationOptions -Tenant $Tenant -UserId $UserId -ChallengeTimeout $ChallengeTimeout -Token $Token

                [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnAttestationResponse] $passkey =
                    New-Passkey -Options $registrationOptions

                # Recursive call with the 'Existing' parameter set
                return Register-Passkey -UserId $UserId -Passkey $passkey -Tenant $Tenant -Token $Token
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
PS \> Get-PasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -Tenant example.okta.com -Token your_okta_token | New-Passkey 

#>
function New-Passkey
{
    [CmdletBinding()]
    param(
        [Parameter(Mandatory = $true, ValueFromPipeline = $true)]
        [Validatescript({
            $ValidateSet = @('MicrosoftGraphWebauthnCredentialCreationOptions','OktaWebauthnCredentialCreationOptions')
            if ($ValidateSet -contains $PSItem.GetType().Name) { 
                return $true
            }
            else { 
                throw $ValidateSet
            }
        })]
        $Options,

        [Parameter(Mandatory = $false)]
        [string] $DisplayName
    )
    begin {
        if ($Options.GetType().Name -eq 'MicrosoftGraphWebauthnCredentialCreationOptions' -and [string]::IsNullOrEmpty($DisplayName)) {
            throw "Parameter 'DisplayName' may not be null or empty."
        }
    }
    process
    {
        try {
            [DSInternals.Win32.WebAuthn.WebAuthnApi] $api = [DSInternals.Win32.WebAuthn.WebAuthnApi]::new()
            [DSInternals.Win32.WebAuthn.PublicKeyCredential] $credential = $api.AuthenticatorMakeCredential($Options.PublicKeyOptions)

            switch ($Options.GetType().Name) {
                'MicrosoftGraphWebauthnCredentialCreationOptions' {
                    return [DSInternals.Win32.WebAuthn.EntraID.MicrosoftGraphWebauthnAttestationResponse]::new($credential, $DisplayName)
                }
                'OktaWebauthnCredentialCreationOptions' {
                    return [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnAttestationResponse]::new($credential, $Options.PublicKeyOptions.User.Id, $Options.Id)
                }
            }
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

New-Alias -Name Register-MgUserAuthenticationFido2Method -Value Register-Passkey

Export-ModuleMember -Function 'Get-PasskeyRegistrationOptions','New-Passkey','Register-Passkey' `
                    -Alias 'Register-MgUserAuthenticationFido2Method'
