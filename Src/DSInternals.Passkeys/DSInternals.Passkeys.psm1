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
The SSWS token from Okta with okta.users.manage permissions.

.EXAMPLE
PS \> Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -Tenant example.okta.com -Token your_ssws_token

.EXAMPLE
PS \> Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -ChallengeTimeout (New-TimeSpan -Seconds 60) -Tenant example.okta.com -Token your_ssws_token

.NOTES
More info at https://developer.okta.com/docs/api/openapi/okta-management/management/tag/UserFactor/#tag/UserFactor/operation/enrollFactor

#>
function Get-OktaPasskeyRegistrationOptions
{
    [CmdletBinding()]
    [OutputType([DSInternals.Win32.WebAuthn.OktaWebauthnCredentialCreationOptions])]
    param(
        [Parameter(Mandatory = $true)]
        [string] $Tenant,

        [Parameter(Mandatory = $true)]
        [Alias('User')]
        [string] $UserId,

        [Parameter(Mandatory = $false)]
        [Alias('Timeout')]
        [timespan] $ChallengeTimeout = (New-TimeSpan -Seconds 300),

        [Parameter(Mandatory = $true)]
        [string] $Token
    )
    try {
        [string] $credentialOptionsUrl = 'https://{0}/api/v1/users/{1}/factors?tokenLifetimeSeconds={2}&activate=true' -f $Tenant, $UserId, $ChallengeTimeout.TotalSeconds

        Write-Debug ('Credential options url: ' + $credentialOptionsUrl)

        $headers = @{
            "Accept" = "application/json"
            "Authorization" = "SSWS ${Token}"
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

.EXAMPLE
PS \> Register-OktaPasskey -UserId 00eDuihq64pgP1gVD0x7 -Tenant example.okta.com -Token your_ssws_token

.EXAMPLE
PS \> Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -Tenant example.okta.com -Token your_ssws_token | New-OktaPasskey | Register-OktaPasskey -Tenant example.okta.com -Token your_ssws_token

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
        [Alias('User')]
        [string] $UserId,

        [Parameter(Mandatory = $true, ParameterSetName = 'Existing', ValueFromPipeline = $true)]
        [DSInternals.Win32.WebAuthn.OktaWebauthnAttestationResponse]
        $Passkey,

        [Parameter(Mandatory = $false, ParameterSetName = 'New')]
        [Alias('Timeout')]
        [timespan] $ChallengeTimeout = (New-TimeSpan -Seconds 300),

        [Parameter(Mandatory = $true, ParameterSetName = 'Existing')]
        [Parameter(Mandatory = $true, ParameterSetName = 'New')]
        [string] $Tenant,

        [Parameter(Mandatory = $true, ParameterSetName = 'Existing')]
        [Parameter(Mandatory = $true, ParameterSetName = 'New')]
        [string] $Token
    )
    process
    {
        # TODO: Write-Error
        switch ($PSCmdlet.ParameterSetName) {
            'Existing' {
                [string] $registrationUrl = 'https://{0}/api/v1/users/{1}/factors/{2}/lifecycle/activate' -f $Tenant, $Passkey.UserId, $Passkey.FactorId

                Write-Debug ('Registration URL: ' + $registrationUrl)

                $headers = @{
                    "Accept" = "application/json"
                    "Authorization" = "SSWS ${Token}"
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
PS \> Get-OktaPasskeyRegistrationOptions -UserId 00eDuihq64pgP1gVD0x7 -Tenant example.okta.com -Token your_ssws_token | New-OktaPasskey 

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

New-Alias -Name Register-MgUserAuthenticationFido2Method -Value Register-Passkey

Export-ModuleMember -Function 'Get-PasskeyRegistrationOptions','Get-OktaPasskeyRegistrationOptions','New-Passkey','New-OktaPasskey','Register-Passkey','Register-OktaPasskey' `
                    -Alias 'Register-MgUserAuthenticationFido2Method'
