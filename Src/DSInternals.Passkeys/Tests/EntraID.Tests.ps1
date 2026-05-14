<#
.SYNOPSIS
    This script contains Pester tests for Entra ID passkey registration via the DSInternals.Passkeys PowerShell module.
.PARAMETER ModulePath
    Path to the compiled module directory.
.PARAMETER Configuration
    The build configuration of the module being tested.
#>

#Requires -Version 5.1
#Requires -Modules @{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

# Secret is read from an environment variable, not hardcoded
[System.Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSAvoidUsingConvertToSecureStringWithPlainText', '')]
param(
    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [string] $ModulePath,

    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [ValidateSet('Debug', 'Release')]
    [string] $Configuration = 'Release'
)

if ([string]::IsNullOrWhiteSpace($ModulePath)) {
    # No path has been provided, so use the default value
    $ModulePath = Join-Path -Path $PSScriptRoot -ChildPath "..\..\..\Build\bin\PSModule\$Configuration\DSInternals.Passkeys" -Resolve -ErrorAction Stop
}

BeforeAll {
    # Select framework based on PowerShell version
    if ($PSVersionTable.PSVersion.Major -lt 6) {
        [string] $framework = 'net48'
    }
    else {
        [string] $framework = 'net8.0-windows'
    }

    [string] $testAssemblyPath = Join-Path -Path $PSScriptRoot -ChildPath ("..\..\..\Build\bin\DSInternals.Win32.WebAuthn.Tests\{0}_{1}\DSInternals.Win32.WebAuthn.Tests.dll" -f $Configuration.ToLowerInvariant(), $framework) -Resolve -ErrorAction Stop

    Add-Type -Path $testAssemblyPath -ErrorAction Stop
    Import-Module -Name $ModulePath -ErrorAction Stop -Force
}

Describe 'EntraID Tests' -Skip {
    BeforeAll {
        $SecureClientSecret = ConvertTo-SecureString -String $env:EntraIdClientSecret -AsPlainText -Force
        $ClientSecretCredential = New-Object -TypeName System.Management.Automation.PSCredential -ArgumentList $env:EntraIdClientId, $SecureClientSecret
        Connect-MgGraph -TenantId $env:EntraIdTenantId -ClientSecretCredential $ClientSecretCredential
    }

    It "Registers passkeys to Entra ID for each credential parameter option" {
        [DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions] $options = Get-EntraPasskeyRegistrationOptions -UserId $env:EntraIdUserId
        $options | Should -BeOfType [DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions]
        $options.RelyingParty.Id | Should -Be "login.microsoft.com"
        $options.PublicKeyCredentialParameters.Count | Should -Be 2

        [DSInternals.Win32.WebAuthn.Tests.PasskeyFactory] $factory0 = [DSInternals.Win32.WebAuthn.Tests.PasskeyFactory]::new()
        $factory0 | Should -BeOfType [DSInternals.Win32.WebAuthn.Tests.PasskeyFactory]
        [DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential] $passkey0 = $factory0.MakePasskey($options, 0)
        $passkey0 | Should -BeOfType [DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential]

        [DSInternals.Win32.WebAuthn.Tests.PasskeyFactory] $factory1 = [DSInternals.Win32.WebAuthn.Tests.PasskeyFactory]::new()
        $factory1 | Should -BeOfType [DSInternals.Win32.WebAuthn.Tests.PasskeyFactory]
        [DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential] $passkey1 = $factory1.MakePasskey($options, 1)
        $passkey1 | Should -BeOfType [DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential]

        $result0 = Register-EntraPasskey -Passkey $passkey0 -UserId $env:EntraIdUserId -DisplayName 'DSInternals.Passkeys 0'
        $result0 | Should -Not -BeNullOrEmpty
        $result0 | Should -BeOfType [Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod]
        $result0.AaGuid | Should -Be "4453496e-7465-726e-616c-730000000000"
        $result0.AttestationCertificates.Count | Should -Be 1
        [string] $credentialId0 = $result0.Id

        $result1 = Register-EntraPasskey -Passkey $passkey1 -UserId $env:EntraIdUserId -DisplayName 'DSInternals.Passkeys 1'
        $result1 | Should -Not -BeNullOrEmpty
        $result1 | Should -BeOfType [Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod]
        $result1.AaGuid | Should -Be "4453496e-7465-726e-616c-730000000000"
        $result1.AttestationCertificates.Count | Should -Be 1
        [string] $credentialId1 = $result1.Id

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
