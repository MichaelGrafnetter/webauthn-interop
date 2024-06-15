function Get-PasskeyRegistrationOptions
{
    [OutputType([DSInternals.Win32.WebAuthn.MicrosoftGraphWebauthnCredentialCreationOptions])]
    param(
        [Parameter(Mandatory = $true, ValueFromPipeline = $true)]
        [string] $UserId,

        [Parameter(Mandatory = $false)]
        [timespan] $ChallengeTimeout = (New-TimeSpan -Minutes 10)
    )

    try {
        # TODO: Validate/Encode the $userId format

        # Generate the user-specific URL, e.g., https://graph.microsoft.com/beta/users/af4cf208-16e0-429d-b574-2a09c5f30dea/authentication/fido2Methods/creationOptions
        [string] $credentialOptionsUrl = '{0}/beta/users/{1}/authentication/fido2Methods/creationOptions' -f (Get-MgGraphEndpoint),$UserId

        [string] $response = Invoke-MgGraphRequest -Method GET `
                                                   -Uri $credentialOptionsUrl `
                                                   -Body @{ challengeTimeoutInMinutes = $ChallengeTimeout.TotalMinutes } `
                                                   -OutputType Json

        # Parse JSON response
        return [DSInternals.Win32.WebAuthn.MicrosoftGraphWebauthnCredentialCreationOptions]::Create($response)
    }
    catch {
        # TODO: PS Error Record
        throw
    }
}

function Register-Passkey
{
    [OutputType([Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod])]
    param(
        [Parameter(Mandatory = $true, ParameterSetName = 'Existing')]
        [Parameter(Mandatory = $true, ParameterSetName = 'New')]
        [string] $UserId,

        [Parameter(Mandatory = $true, ParameterSetName = 'Existing')]
        [DSInternals.Win32.WebAuthn.MicrosoftGraphWebauthnAttestationResponse]
        $Passkey,

        [Parameter(Mandatory = $true, ParameterSetName = 'New')]
        [string] $DisplayName,

        [Parameter(Mandatory = $false, ParameterSetName = 'New')]
        [timespan] $ChallengeTimeout = (New-TimeSpan -Minutes 10)
    )

    switch ($PSCmdlet.ParameterSetName) {
        'Existing' {
            [string] $endpoint = Get-MgGraphEndpoint
            
            # TODO: Validate/Encode the $userId format

            # Generate the user-specific URL, e.g., https://graph.microsoft.com/beta/users/af4cf208-16e0-429d-b574-2a09c5f30dea/authentication/fido2Methods
            [string] $registrationUrl = '{0}/beta/users/{1}/authentication/fido2Methods' -f $endpoint,$UserId

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
                Get-PasskeyRegistrationOptions -UserId 'AdeleV@dev.dsinternals.com' -ChallengeTimeout $ChallengeTimeout

            [DSInternals.Win32.WebAuthn.MicrosoftGraphWebauthnAttestationResponse] $passkey =
                New-Passkey -Options $registrationOptions -DisplayName $DisplayName

            # Recursive call with the 'Existing' parameter set
            return Register-Passkey -UserId 'AdeleV@dev.dsinternals.com' -Passkey $passkey
        }
    }
}

function New-Passkey
{
    [OutputType([DSInternals.Win32.WebAuthn.MicrosoftGraphWebauthnAttestationResponse])]
    param(
        [Parameter(Mandatory = $true, ValueFromPipeline = $true)]
        [DSInternals.Win32.WebAuthn.MicrosoftGraphWebauthnCredentialCreationOptions]
        $Options,

        [Parameter(Mandatory = $true)]
        [string] $DisplayName
    )

    try {
        [DSInternals.Win32.WebAuthn.WebAuthnApi] $api = [DSInternals.Win32.WebAuthn.WebAuthnApi]::new()
        [DSInternals.Win32.WebAuthn.PublicKeyCredential] $credential = $api.AuthenticatorMakeCredential($Options.PublicKeyOptions)

        return [DSInternals.Win32.WebAuthn.MicrosoftGraphWebauthnAttestationResponse]::new($credential, $DisplayName);
    }
    catch {
        # TODO: PS Error Record
        throw
    }
}
  
function Get-MgGraphEndpoint
{
    [OutputType([string])]
    param()

    [Microsoft.Graph.PowerShell.Authentication.AuthContext] $context = Get-MgContext -ErrorAction Stop

    if($null -ne $context) {
        return (Get-MgEnvironment -Name $context.Environment -ErrorAction Stop).GraphEndpoint
    }
    else {
        # TODO: PS Error Record
        throw 'Not connected to graph'
    }
}

Export-ModuleMember -Function Get-PasskeyRegistrationOptions,New-Passkey,Register-Passkey
