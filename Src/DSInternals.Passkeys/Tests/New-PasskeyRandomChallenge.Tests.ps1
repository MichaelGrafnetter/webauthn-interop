 <#
.SYNOPSIS
    This script contains Pester tests for the New-PasskeyRandomChallenge cmdlet from the DSInternals.Passkeys PowerShell module.
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
    # No path has been provided, so use a the default value
    $ModulePath = Join-Path -Path $PSScriptRoot -ChildPath "..\..\..\Build\bin\PSModule\$Configuration\DSInternals.Passkeys" -Resolve -ErrorAction Stop
}

BeforeDiscovery {
    Import-Module -Name $ModulePath -ErrorAction Stop -Force
}

Describe 'New-PasskeyRandomChallenge' {
    It 'Should generate a byte array with default length of 32 bytes' {
        $result = New-PasskeyRandomChallenge
        $result | Should -BeOfType [byte]
        $result.Length | Should -Be 32
    }

    It 'Should generate a byte array with specified length' {
        $result = New-PasskeyRandomChallenge -Length 16
        $result | Should -BeOfType [byte]
        $result.Length | Should -Be 16

        $result = New-PasskeyRandomChallenge -Length 64
        $result | Should -BeOfType [byte]
        $result.Length | Should -Be 64
    }

    It 'Should generate different values on each call' {
        $result1 = New-PasskeyRandomChallenge -Length 32
        $result2 = New-PasskeyRandomChallenge -Length 32

        # Compare as Base64 strings for easier comparison
        $str1 = [Convert]::ToBase64String($result1)
        $str2 = [Convert]::ToBase64String($result2)
        $str1 | Should -Not -Be $str2
    }

    It 'Should reject length less than 16' {
        { New-PasskeyRandomChallenge -Length 15 } | Should -Throw
        { New-PasskeyRandomChallenge -Length 0 } | Should -Throw
        { New-PasskeyRandomChallenge -Length -1 } | Should -Throw
    }

    It 'Should reject length greater than 64' {
        { New-PasskeyRandomChallenge -Length 65 } | Should -Throw
        { New-PasskeyRandomChallenge -Length 100 } | Should -Throw
    }
}
