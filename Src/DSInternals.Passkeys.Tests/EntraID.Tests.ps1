<#
.SYNOPSIS
Invokes Pester tests for the DSInternals.Passkeys PowerShell module that are related to Entra ID support.

.PARAMETER ModulePath
The path to the DSInternals.Passkeys module.

.PARAMETER HelperAssemblyPath


.PARAMETER MicrosoftGraphAccessToken

#>

#Requires -Version 5.1
#Requires -Modules @{ ModuleName = 'Pester'; ModuleVersion = '5.7.0' }

param(
    [ValidateNotNullOrEmpty()]
    [string] $ModulePath = (Join-Path -Path $PSScriptRoot -ChildPath '..\..\Build\bin\PSModule\Release\DSInternals.Passkeys'),

    [ValidateNotNullOrEmpty()]
    [string] $HelperAssemblyPath = (Join-Path -Path $PSScriptRoot -ChildPath '..\..\Build\bin\DSInternals.Win32.WebAuthn.Tests\release\DSInternals.Win32.WebAuthn.Tests.dll'),

    [ValidateNotNullOrEmpty()]
    [securestring] $MicrosoftGraphAccessToken = (ConvertTo-SecureString -AsPlainText -Force -String (az account get-access-token --resource-type ms-graph | ConvertFrom-Json | Select-Object -ExpandProperty accessToken))
)

Describe 'Entra ID' {
    BeforeAll {
        Import-Module -Name $ModulePath -Force -ErrorAction Stop

        Add-Type -Path $HelperAssemblyPath -ErrorAction Stop

        Connect-MgGraph -AccessToken $MicrosoftGraphAccessToken
    }

    AfterAll {
        Disconnect-MgGraph
    }

    It "Registers passkeys to Entra ID for each credential parameter option" {
        $options = Get-PasskeyRegistrationOptions -UserId $env:EntraIdUserId
        $options | Should -BeOfType [DSInternals.Win32.WebAuthn.EntraID.MicrosoftGraphWebauthnCredentialCreationOptions]
        $options.ChallengeTimeout | Should -BeGreaterThan (Get-Date)
        $options.PublicKeyOptions.RelyingParty.Id | Should -Be "login.microsoft.com"
        $options.PublicKeyOptions.PublicKeyCredentialParameters.Count | Should -Be 2

        $factory0 = [DSInternals.Win32.WebAuthn.Tests.PasskeyFactory]::new()
        $factory0 | Should -BeOfType [DSInternals.Win32.WebAuthn.Tests.PasskeyFactory]
        $passkey0 = $factory0.MakePasskey($options, 0)
        $passkey0 | Should -BeOfType [DSInternals.Win32.WebAuthn.EntraID.MicrosoftGraphWebauthnAttestationResponse]
        $passkey0.DisplayName | Should -BeLike 'DSInternals.Passkeys*'

        $factory1 = [DSInternals.Win32.WebAuthn.Tests.PasskeyFactory]::new()
        $factory1 | Should -BeOfType [DSInternals.Win32.WebAuthn.Tests.PasskeyFactory]
        $passkey1 = $factory1.MakePasskey($options, 1)
        $passkey1 | Should -BeOfType [DSInternals.Win32.WebAuthn.EntraID.MicrosoftGraphWebauthnAttestationResponse]
        $passkey1.DisplayName | Should -BeLike 'DSInternals.Passkeys*'

        $result0 = Register-Passkey -Passkey $passkey0 -UserId $env:EntraIdUserId
        $result0 | Should -Not -BeNullOrEmpty
        $result0 | Should -BeOfType [Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod]
        $result0.AaGuid | Should -Be "4453496e-7465-726e-616c-730000000000"
        $result0.CreatedDateTime | Should -BeLessThan $options.ChallengeTimeout
        $result0.AttestationCertificates.Count | Should -Be 1
        $credentialId0 = $result0.Id

        $result1 = Register-Passkey -Passkey $passkey1 -UserId $env:EntraIdUserId
        $result1 | Should -Not -BeNullOrEmpty
        $result1 | Should -BeOfType [Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod]
        $result1.AaGuid | Should -Be "4453496e-7465-726e-616c-730000000000"
        $result1.CreatedDateTime | Should -BeLessThan $options.ChallengeTimeout
        $result1.AttestationCertificates.Count | Should -Be 1
        $credentialId1 = $result1.Id

        $result0.AttestationCertificates[0] | Should -Not -Be $result1.AttestationCertificates[0]
        $result0.Id | Should -Not -Be $result1.Id

        [string] $credentialDeletionUrl0 = '/beta/users/{0}/authentication/fido2Methods/{1}' -f [uri]::EscapeDataString($env:EntraIdUserId), $credentialId0
        $response0 = Invoke-MgGraphRequest -Method Delete -Uri $credentialDeletionUrl0
        $response0 | Should -BeNullOrEmpty

        [string] $credentialDeletionUrl1 = '/beta/users/{0}/authentication/fido2Methods/{1}' -f [uri]::EscapeDataString($env:EntraIdUserId), $credentialId1
        $response1 = Invoke-MgGraphRequest -Method Delete -Uri $credentialDeletionUrl1
        $response1 | Should -BeNullOrEmpty
    }
}
