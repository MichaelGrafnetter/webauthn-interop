 <#
.SYNOPSIS
    This script contains Pester tests for the ConvertFrom-Base64UrlParameter cmdlet from the DSInternals.Passkeys PowerShell module.
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

Describe 'ConvertFrom-Base64UrlParameter' {
    BeforeAll {
        # Get access to the internal function by dot-sourcing the module
        # The function is not exported, so we need to access it via InvokeCommand
        $module = Get-Module -Name DSInternals.Passkeys
    }

    It 'Should return null when input is null' {
        $result = & $module { ConvertFrom-Base64UrlParameter -InputObject $null }
        $result | Should -BeNullOrEmpty
    }

    It 'Should pass through byte arrays unchanged' {
        $inputBytes = [byte[]]@(1, 2, 3, 4, 5)
        $result = & $module { param($bytes) ConvertFrom-Base64UrlParameter -InputObject $bytes } -bytes $inputBytes
        $result | Should -BeOfType [byte]
        $result.Length | Should -Be 5
        $result[0] | Should -Be 1
        $result[4] | Should -Be 5
    }

    It 'Should convert Base64Url strings to byte arrays' {
        # "Hello" in Base64Url is "SGVsbG8"
        $base64Url = 'SGVsbG8'
        $expected = [System.Text.Encoding]::UTF8.GetBytes('Hello')

        $result = & $module { param($str) ConvertFrom-Base64UrlParameter -InputObject $str } -str $base64Url
        $result | Should -BeOfType [byte]
        $result.Length | Should -Be $expected.Length
        [Convert]::ToBase64String($result) | Should -Be ([Convert]::ToBase64String($expected))
    }

    It 'Should handle Base64Url strings with URL-safe characters' {
        # Test string that would have + and / in standard Base64
        # Bytes [251, 239] encode to "++" in Base64 but "--" in Base64Url
        $testBytes = [byte[]]@(251, 239)
        $base64Url = [System.Buffers.Text.Base64Url]::EncodeToString($testBytes)

        $result = & $module { param($str) ConvertFrom-Base64UrlParameter -InputObject $str } -str $base64Url
        $result | Should -BeOfType [byte]
        $result.Length | Should -Be 2
        $result[0] | Should -Be 251
        $result[1] | Should -Be 239
    }

    It 'Should handle Base64Url strings without padding' {
        # Base64Url omits padding, test various lengths
        # 1 byte -> 2 chars (would need 2 padding in Base64)
        $oneByteInput = [byte[]]@(65)
        $base64Url1 = [System.Buffers.Text.Base64Url]::EncodeToString($oneByteInput)

        $result1 = & $module { param($str) ConvertFrom-Base64UrlParameter -InputObject $str } -str $base64Url1
        $result1.Length | Should -Be 1
        $result1[0] | Should -Be 65

        # 2 bytes -> 3 chars (would need 1 padding in Base64)
        $twoBytesInput = [byte[]]@(65, 66)
        $base64Url2 = [System.Buffers.Text.Base64Url]::EncodeToString($twoBytesInput)
        $result2 = & $module { param($str) ConvertFrom-Base64UrlParameter -InputObject $str } -str $base64Url2
        $result2.Length | Should -Be 2
        $result2[0] | Should -Be 65
        $result2[1] | Should -Be 66
    }

    It 'Should throw for unsupported types' {
        { & $module { ConvertFrom-Base64UrlParameter -InputObject 12345 } } | Should -Throw
        { & $module { ConvertFrom-Base64UrlParameter -InputObject @{ key = 'value' } } } | Should -Throw
        { & $module { ConvertFrom-Base64UrlParameter -InputObject (Get-Date) } } | Should -Throw
    }

    It 'Should throw for invalid Base64Url strings' {
        # Invalid characters
        { & $module { ConvertFrom-Base64UrlParameter -InputObject '!!!invalid!!!' } } | Should -Throw
    }

    It 'Should round-trip with byte array and Base64Url conversion' {
        [byte[]] $originalBytes = New-PasskeyRandomChallenge -Length 32
        [string] $base64Url = [System.Buffers.Text.Base64Url]::EncodeToString($originalBytes)

        [byte[]] $result = & $module { param($str) ConvertFrom-Base64UrlParameter -InputObject $str } -str $base64Url
        [Convert]::ToBase64String($result) | Should -Be ([Convert]::ToBase64String($originalBytes))
    }
}
