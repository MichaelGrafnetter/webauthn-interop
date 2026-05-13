<#
.SYNOPSIS
    This script contains Pester tests for the assemblies bundled in the DSInternals.Passkeys PowerShell module.
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

Describe 'Assemblies' {
    BeforeDiscovery {
        [hashtable[]] $assemblies = Get-ChildItem -Path $ModulePath -Recurse -Filter *.dll |
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
