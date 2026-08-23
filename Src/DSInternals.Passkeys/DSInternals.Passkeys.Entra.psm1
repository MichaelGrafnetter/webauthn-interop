# Needed for [Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod] type
Import-Module -Name Microsoft.Graph.Identity.SignIns -ErrorAction Stop

# Creation options are now retrieved using the native Invoke-MgCreationUserAuthenticationFido2MethodOption cmdlet
# from the Microsoft.Graph.Identity.SignIns module. These aliases keep pipelines written against older module
# versions working, as New-Passkey accepts the native cmdlet's output directly.
Set-Alias -Name Get-EntraPasskeyRegistrationOptions -Value Invoke-MgCreationUserAuthenticationFido2MethodOption
Set-Alias -Name Get-PasskeyRegistrationOptions -Value Invoke-MgCreationUserAuthenticationFido2MethodOption

<#
.SYNOPSIS
Converts a WebAuthn attestation credential into the Microsoft Graph SDK model expected by the fido2Methods API.

.DESCRIPTION
Helper function that is not exported by the module manifest.
The Microsoft Graph API contract requires the credential id, clientDataJSON, and attestationObject to be Base64URL-encoded without padding.
#>
function ConvertTo-MicrosoftGraphFido2AuthenticationMethod
{
    [OutputType([Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod])]
    param(
        [Parameter(Mandatory = $true)]
        [DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential] $Passkey,

        [Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string] $DisplayName
    )

    [Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod] $fido2Method =
        [Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod]::new()
    $fido2Method.DisplayName = $DisplayName
    $fido2Method.PublicKeyCredential = [Microsoft.Graph.PowerShell.Models.MicrosoftGraphWebauthnPublicKeyCredential]::new()
    $fido2Method.PublicKeyCredential.Id = [System.Buffers.Text.Base64Url]::EncodeToString($Passkey.Id)
    $fido2Method.PublicKeyCredential.Response = [Microsoft.Graph.PowerShell.Models.MicrosoftGraphWebauthnAuthenticatorAttestationResponse]::new()
    $fido2Method.PublicKeyCredential.Response.ClientDataJson = [System.Buffers.Text.Base64Url]::EncodeToString($Passkey.Response.ClientData)
    $fido2Method.PublicKeyCredential.Response.AttestationObject = [System.Buffers.Text.Base64Url]::EncodeToString($Passkey.Response.AttestationObject)

    return $fido2Method
}

<#
.SYNOPSIS
Registers a new passkey in Microsoft Entra ID.

.DESCRIPTION
Registers a new passkey for the specified user in Microsoft Entra ID.

When called without the -Passkey parameter, this cmdlet performs the full registration flow: it requests a challenge from Entra ID, drives the local authenticator (which prompts the system passkey UI), and submits the attestation to complete enrollment.
When called with -Passkey, it submits a previously produced attestation, which is useful when the credential ceremony was run separately (e.g. via New-Passkey in a pipeline).

Requires an active Microsoft Graph connection (Connect-MgGraph) with the UserAuthMethod-Passkey.ReadWrite.All scope (least privileged) or UserAuthenticationMethod.ReadWrite.All. Registering a passkey on behalf of another user additionally requires the Authentication Administrator or Privileged Authentication Administrator role.

The server-issued challenge has a fixed validity of 5 minutes, which cannot be changed.

.PARAMETER UserId
The unique identifier of the user. Either the object id (GUID) or UPN.

.PARAMETER Passkey
The attestation credential produced by the local WebAuthn authenticator (e.g. via New-Passkey). Wrapped into a Microsoft Graph attestation response before being submitted.

.PARAMETER DisplayName
Custom name given to the registered passkey.

.EXAMPLE
Connect-MgGraph -Scopes 'UserAuthMethod-Passkey.ReadWrite.All'
Register-EntraPasskey -UserId 'AdeleV@contoso.com' -DisplayName 'YubiKey 5 Nano'

Performs the full registration ceremony in one step: fetches creation options, prompts the local authenticator, and submits the attestation to Entra ID with the given display name.

.EXAMPLE
Connect-MgGraph -Scopes 'UserAuthMethod-Passkey.ReadWrite.All'
Invoke-MgCreationUserAuthenticationFido2MethodOption -UserId 'AdeleV@contoso.com' | New-Passkey | Register-EntraPasskey -UserId 'AdeleV@contoso.com' -DisplayName 'YubiKey 5 Nano'

Splits the registration into explicit pipeline stages: fetch options, create the credential locally, and submit the attestation. Equivalent to the single-step form but lets the caller inspect intermediate values.

.NOTES
The FIDO2 authentication method policy in Microsoft Entra ID must have the "Allow self-service setup" option enabled. This is a documented limitation of the underlying Microsoft Graph API.

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
        [string] $DisplayName
    )

    process {
        if ($PSCmdlet.ParameterSetName -eq 'New')
        {
            # Calls GET https://graph.microsoft.com/v1.0/users/{id}/authentication/fido2Methods/creationOptions
            [Microsoft.Graph.PowerShell.Models.IMicrosoftGraphWebauthnCredentialCreationOptions] $creationOptions =
                Invoke-MgCreationUserAuthenticationFido2MethodOption -UserId $UserId -ErrorAction Stop

            if ($null -eq $creationOptions.PublicKey) {
                throw 'The Microsoft Graph credential creation options do not contain WebAuthn public key options.'
            }

            # Round-trip through JSON to convert the Graph SDK model into the WebAuthn interop model
            [DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions] $options =
                [DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions]::FromJson($creationOptions.PublicKey.ToJsonString())

            if ($null -eq $options) {
                throw 'Unable to parse the WebAuthn public key options returned by Microsoft Graph.'
            }

            [DSInternals.Win32.WebAuthn.WebAuthnApi] $api = [DSInternals.Win32.WebAuthn.WebAuthnApi]::new()
            $Passkey = $api.AuthenticatorMakeCredential($options)
        }

        # Wrap the attestation into the Microsoft Graph SDK model
        [Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod] $fido2Method =
            ConvertTo-MicrosoftGraphFido2AuthenticationMethod -Passkey $Passkey -DisplayName $DisplayName

        # Generate the user-specific URL, e.g., https://graph.microsoft.com/v1.0/users/af4cf208-16e0-429d-b574-2a09c5f30dea/authentication/fido2Methods
        # Note: The Microsoft.Graph.Identity.SignIns module does not yet provide a native cmdlet for this POST operation.
        [string] $registrationUrl = '/v1.0/users/{0}/authentication/fido2Methods' -f [uri]::EscapeDataString($UserId)

        [string] $response = Invoke-MgGraphRequest `
                                -Method POST `
                                -Uri $registrationUrl `
                                -OutputType Json `
                                -ContentType 'application/json' `
                                -Body $fido2Method.ToJsonString()

        return [Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod]::FromJsonString($response)
    }
}

# Functions and aliases are filtered by FunctionsToExport / AliasesToExport in the parent manifest.
