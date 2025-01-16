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
Import-Module -Name Microsoft.Graph.Identity.SignIns -ErrorAction Stop

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
        [string] $credentialOptionsUrl = '/beta/users/{0}/authentication/fido2Methods/creationOptions' -f [uri]::EscapeDataString($UserId)

        [string] $response = Invoke-MgGraphRequest -Method GET `
                                                   -Uri $credentialOptionsUrl `
                                                   -Body @{ challengeTimeoutInMinutes = $ChallengeTimeout.TotalMinutes } `
                                                   -OutputType Json

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
                # Generate the user-specific URL, e.g., https://graph.microsoft.com/beta/users/af4cf208-16e0-429d-b574-2a09c5f30dea/authentication/fido2Methods
                [string] $registrationUrl = '/beta/users/{0}/authentication/fido2Methods' -f [uri]::EscapeDataString($UserId)

                [string] $response = Invoke-MgGraphRequest `
                                        -Method POST `
                                        -Uri $registrationUrl `
                                        -OutputType Json `
                                        -ContentType 'application/json' `
                                        -Body $Passkey.ToString()

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

New-Alias -Name Register-MgUserAuthenticationFido2Method -Value Register-Passkey

Export-ModuleMember -Function 'Get-PasskeyRegistrationOptions','New-Passkey','Register-Passkey' `
                    -Alias 'Register-MgUserAuthenticationFido2Method'
