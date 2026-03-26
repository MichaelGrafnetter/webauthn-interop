<#
.SYNOPSIS
    This script contains Pester tests for Entra ID passkey registration via the DSInternals.Passkeys PowerShell module.
.PARAMETER ModulePath
    Path to the compiled module directory.
#>

#Requires -Version 5.1
#Requires -Modules @{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

param(
    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [string] $ModulePath
)

if ([string]::IsNullOrWhiteSpace($ModulePath)) {
    # No path has been provided, so use the default value
    $ModulePath = Join-Path -Path $PSScriptRoot -ChildPath '..\..\..\Build\bin\PSModule\Release\DSInternals.Passkeys' -Resolve -ErrorAction Stop
}

BeforeAll {
    # Derive build configuration (e.g., Release/Debug) from the module path
    $moduleConfigDirectory = Split-Path -Path (Split-Path -Path $ModulePath -Parent) -Leaf

    # Select framework based on PowerShell version
    if ($PSVersionTable.PSVersion.Major -lt 6) {
        $framework = 'net48'
    }
    else {
        $framework = 'net8.0-windows'
    }

    $testAssemblyPath = Join-Path -Path $PSScriptRoot -ChildPath ("..\..\..\Build\bin\DSInternals.Win32.WebAuthn.Tests\{0}\{1}\DSInternals.Win32.WebAuthn.Tests.dll" -f $moduleConfigDirectory, $framework) -Resolve -ErrorAction Stop

    Add-Type -Path $testAssemblyPath -ErrorAction Stop
    Import-Module -Name $ModulePath -ErrorAction Stop -Force
}

Describe 'EntraID Tests' {
    BeforeAll {
        $SecureClientSecret = ConvertTo-SecureString -String $env:EntraIdClientSecret -AsPlainText -Force
        $ClientSecretCredential = New-Object -TypeName System.Management.Automation.PSCredential -ArgumentList $env:EntraIdClientId, $SecureClientSecret
        Connect-MgGraph -TenantId $env:EntraIdTenantId -ClientSecretCredential $ClientSecretCredential
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

    AfterAll {
        Disconnect-MgGraph
    }
}
