<#
.SYNOPSIS
    This script contains Pester tests for Okta passkey registration via the DSInternals.Passkeys PowerShell module.
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
    # Select framework based on PowerShell edition/version
    if ($PSVersionTable.PSEdition -eq 'Desktop' -or $PSVersionTable.PSVersion.Major -lt 7) {
        $framework = 'net48'
    }
    else {
        $framework = 'net8.0-windows'
    }

    # Probe for the assembly in common configurations (Release/Debug)
    $testAssemblyPath = $null
    foreach ($config in @('Release', 'Debug')) {
        $candidate = Join-Path -Path $PSScriptRoot -ChildPath "..\..\..\Build\bin\DSInternals.Win32.WebAuthn.Tests\$config\$framework\DSInternals.Win32.WebAuthn.Tests.dll"
        if (Test-Path -Path $candidate) {
            $testAssemblyPath = (Resolve-Path -Path $candidate -ErrorAction Stop).Path
            break
        }
    }

    if (-not $testAssemblyPath) {
        throw "Could not find DSInternals.Win32.WebAuthn.Tests.dll in any known configuration (Release/Debug) or framework for framework '$framework'."
    }

    Add-Type -Path $testAssemblyPath -ErrorAction Stop
    Import-Module -Name $ModulePath -ErrorAction Stop -Force
}

Describe "Okta Tests" {
    BeforeAll {
        Connect-Okta -Tenant $env:OktaTenantId -ClientId $env:OKtaClientId -Scopes @('okta.users.manage') -JsonWebKey $env:OktaJsonWebKey
    }

    It "Registers passkeys to Okta for each credential parameter option" {
        $options0 = Get-PasskeyRegistrationOptions -UserId $env:OktaUserId
        $options0 | Should -BeOfType [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnCredentialCreationOptions]
        $options0.PublicKeyOptions.RelyingParty.Id | Should -Be $env:OktaTenantId
        $options0.PublicKeyOptions.PublicKeyCredentialParameters.Count | Should -Be 2

        $factory0 = [DSInternals.Win32.WebAuthn.Tests.PasskeyFactory]::new()
        $factory0 | Should -BeOfType [DSInternals.Win32.WebAuthn.Tests.PasskeyFactory]

        $passkey0 = $factory0.MakePasskey($options0, 0)
        $passkey0 | Should -BeOfType [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnAttestationResponse]
        $passkey0 | Should -Not -BeNullOrEmpty
        $factorId0 = $passkey0.FactorId

        $result0 = Register-Passkey -Passkey $passkey0 -UserId $env:OktaUserId
        $result0 | Should -Not -BeNullOrEmpty
        $result0 | Should -BeOfType [DSInternals.Win32.WebAuthn.Okta.OktaFido2AuthenticationMethod]
        $result0.Id | Should -Be $factorId0
        $result0.FactorType | Should -Be "webauthn"
        $result0.Provider | Should -Be "FIDO"
        $result0.VendorName | Should -Be "FIDO"
        $result0.Status | Should -Be "ACTIVE"
        $result0.Created | Should -BeLessThan (Get-Date -AsUTC)
        $result0.Profile.CredentialId | Should -Not -BeNullOrEmpty
        $result0.Profile.CredentialId | Should -Be ([Convert]::ToBase64String($passkey0.PublicKeyCred.Id)).Replace('+', '-').Replace('/', '_').Replace('=', '')

        $OktaUserId = $env:OktaUserId
        [string] $credentialDeletionPath0 = "/api/v1/users/${OktaUserId}/factors/${factorId0}"
        $response0 = Invoke-OktaWebRequest -Method ([Microsoft.PowerShell.Commands.WebRequestMethod]::Delete) -Path $credentialDeletionPath0
        $response0.StatusCode | Should -Be 204
        $response0.RawContentLength | Should -Be 0

        $options1 = Get-PasskeyRegistrationOptions -UserId $env:OktaUserId
        $options1 | Should -BeOfType [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnCredentialCreationOptions]
        $options1.PublicKeyOptions.RelyingParty.Id | Should -Be $env:OktaTenantId
        $options1.PublicKeyOptions.PublicKeyCredentialParameters.Count | Should -Be 2

        $factory1 = [DSInternals.Win32.WebAuthn.Tests.PasskeyFactory]::new()
        $factory1 | Should -BeOfType [DSInternals.Win32.WebAuthn.Tests.PasskeyFactory]

        $passkey1 = $factory1.MakePasskey($options1, 1)
        $passkey1 | Should -BeOfType [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnAttestationResponse]
        $passkey1 | Should -Not -BeNullOrEmpty
        $factorId1 = $passkey1.FactorId

        $result1 = Register-Passkey -Passkey $passkey1 -UserId $env:OktaUserId
        $result1 | Should -Not -BeNullOrEmpty
        $result1 | Should -BeOfType [DSInternals.Win32.WebAuthn.Okta.OktaFido2AuthenticationMethod]
        $result1.Id | Should -Be $factorId1
        $result1.FactorType | Should -Be "webauthn"
        $result1.Provider | Should -Be "FIDO"
        $result1.VendorName | Should -Be "FIDO"
        $result1.Status | Should -Be "ACTIVE"
        $result1.Created | Should -BeLessThan (Get-Date -AsUTC)
        $result1.Profile.CredentialId | Should -Not -BeNullOrEmpty
        $result1.Profile.CredentialId | Should -Be ([Convert]::ToBase64String($passkey1.PublicKeyCred.Id)).Replace('+', '-').Replace('/', '_').Replace('=', '')

        [string] $credentialDeletionPath1 = "/api/v1/users/${OktaUserId}/factors/${factorId1}"
        $response1 = Invoke-OktaWebRequest -Method ([Microsoft.PowerShell.Commands.WebRequestMethod]::Delete) -Path $credentialDeletionPath1
        $response1.StatusCode | Should -Be 204
        $response1.RawContentLength | Should -Be 0

        $result0.Profile.CredentialId | Should -Not -Be $result1.Profile.CredentialId
    }

    AfterAll {
        Disconnect-Okta
    }
}
