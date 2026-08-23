<#
.SYNOPSIS
    This script contains Pester tests for the internal ConvertTo-MicrosoftGraphFido2AuthenticationMethod function from the DSInternals.Passkeys PowerShell module.
.PARAMETER ModulePath
    Path to the compiled module directory.
.PARAMETER Configuration
    The build configuration of the module being tested.
#>

#Requires -Version 5.1
#Requires -Modules @{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

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

BeforeDiscovery {
    Import-Module -Name $ModulePath -ErrorAction Stop -Force
}

Describe 'ConvertTo-MicrosoftGraphFido2AuthenticationMethod' {
    BeforeAll {
        # The function is not exported, so it must be invoked in the module's scope
        $module = Get-Module -Name DSInternals.Passkeys

        # Sample WebAuthn attestation credential with Base64URL-specific characters ('-' and '_') in the binary fields
        [string] $credentialId = 'AQL6-_w'
        [string] $clientDataJson = 'eyJ0eXBlIjoid2ViYXV0aG4uY3JlYXRlIn0'
        [string] $attestationObject = 'o2NmbXRkbm9uZWdhdHRTdG10oGhhdXRoRGF0YVik-_x5'

        [string] $credentialJson = '{{"id":"{0}","rawId":"{0}","type":"public-key","response":{{"clientDataJSON":"{1}","attestationObject":"{2}"}}}}' -f
            $credentialId, $clientDataJson, $attestationObject

        [DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential] $passkey =
            [DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential]::FromJson($credentialJson)
    }

    It 'is not exported by the module' {
        Get-Command -Name ConvertTo-MicrosoftGraphFido2AuthenticationMethod -ErrorAction SilentlyContinue | Should -BeNullOrEmpty
    }

    It 'returns a Microsoft Graph SDK model' {
        $result = & $module { param($p) ConvertTo-MicrosoftGraphFido2AuthenticationMethod -Passkey $p -DisplayName 'YubiKey 5 Nano' } $passkey
        $result | Should -BeOfType [Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod]
        $result.DisplayName | Should -Be 'YubiKey 5 Nano'
    }

    It 'produces the JSON payload expected by the fido2Methods API' {
        $result = & $module { param($p) ConvertTo-MicrosoftGraphFido2AuthenticationMethod -Passkey $p -DisplayName 'YubiKey 5 Nano' } $passkey
        $payload = $result.ToJsonString() | ConvertFrom-Json

        $payload.displayName | Should -Be 'YubiKey 5 Nano'
        $payload.publicKeyCredential.id | Should -Be $credentialId
        $payload.publicKeyCredential.response.clientDataJSON | Should -Be $clientDataJson
        $payload.publicKeyCredential.response.attestationObject | Should -Be $attestationObject
    }

    It 'encodes binary fields as Base64URL without padding' {
        $result = & $module { param($p) ConvertTo-MicrosoftGraphFido2AuthenticationMethod -Passkey $p -DisplayName 'YubiKey 5 Nano' } $passkey

        # The sample values contain bytes that map to '-' and '_' in Base64URL, but to '+', '/', or '=' in standard Base64
        $result.PublicKeyCredential.Id | Should -Not -Match '[+/=]'
        $result.PublicKeyCredential.Response.AttestationObject | Should -Not -Match '[+/=]'
        $result.PublicKeyCredential.Id | Should -Be ([System.Buffers.Text.Base64Url]::EncodeToString($passkey.Id))
        $result.PublicKeyCredential.Response.ClientDataJson | Should -Be ([System.Buffers.Text.Base64Url]::EncodeToString($passkey.Response.ClientData))
        $result.PublicKeyCredential.Response.AttestationObject | Should -Be ([System.Buffers.Text.Base64Url]::EncodeToString($passkey.Response.AttestationObject))
    }

    It 'does not emit client extension results' {
        # The Graph API declares no properties on webauthnAuthenticationExtensionsClientOutputs, so extension outputs are intentionally not forwarded
        $result = & $module { param($p) ConvertTo-MicrosoftGraphFido2AuthenticationMethod -Passkey $p -DisplayName 'YubiKey 5 Nano' } $passkey
        $payload = $result.ToJsonString() | ConvertFrom-Json

        $payload.publicKeyCredential.PSObject.Properties.Name | Should -Not -Contain 'clientExtensionResults'
    }

    It 'rejects a null passkey' {
        { & $module { ConvertTo-MicrosoftGraphFido2AuthenticationMethod -Passkey $null -DisplayName 'YubiKey 5 Nano' } } | Should -Throw
    }

    It 'rejects an empty display name' {
        { & $module { param($p) ConvertTo-MicrosoftGraphFido2AuthenticationMethod -Passkey $p -DisplayName '' } $passkey } | Should -Throw
    }
}
