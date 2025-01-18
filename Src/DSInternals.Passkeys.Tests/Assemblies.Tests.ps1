<#
.SYNOPSIS
Invokes Pester tests for the assemblies contained in the DSInternals.Passkeys PowerShell module.

.PARAMETER ModulePath
The path to the DSInternals.Passkeys module.

#>

#Requires -Version 5.1
#Requires -Modules @{ ModuleName = 'Pester'; ModuleVersion = '5.7.0' }

param(
    [ValidateNotNullOrEmpty()]
    [string] $ModulePath = (Join-Path -Path $PSScriptRoot -ChildPath '..\..\Build\bin\PSModule\Release\DSInternals.Passkeys')
)

Describe 'Assemblies' {
    BeforeDiscovery {
        [hashtable[]] $assemblies = Get-ChildItem $ModulePath -Recurse -Filter *.dll |
            ForEach-Object { @{ Assembly = $PSItem } }
        [hashtable[]] $ownedAssemblies = $assemblies | Where-Object { $PSItem.Assembly.Name -like 'DSInternals.*.dll' }
    }

    It '<Assembly.Name> has a strong name' -TestCases $assemblies -Test {
        param([System.IO.FileInfo] $Assembly)

        [System.Reflection.AssemblyName] $assemblyName = [System.Reflection.AssemblyName]::GetAssemblyName($Assembly.FullName)
        $assemblyName.Flags.HasFlag([System.Reflection.AssemblyNameFlags]::PublicKey) | Should -BeTrue
    }

    It '<Assembly.Name> has file details' -TestCases $ownedAssemblies -Test {
        param([System.IO.FileInfo] $Assembly)

        $Assembly.VersionInfo.ProductName | Should -Not -BeNullOrEmpty
    }

    It '<Assembly.Name> has up-to-date copyright information' -TestCases $ownedAssemblies -Test {
        param([System.IO.FileInfo] $Assembly)

        [string] $expectedCopyrightInfo = '*2021-{0}*' -f (Get-Date).Year

        $Assembly.VersionInfo.LegalCopyright | Should -BeLike $expectedCopyrightInfo
    }
}
