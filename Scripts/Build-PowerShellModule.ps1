<#
.SYNOPSIS
Builds the PowerShell module.

#>

#Requires -Version 5

[CmdletBinding()]
param(
    [Parameter(Mandatory = $false)]
    [ValidateSet('Release', 'Debug')]
    [string[]] $Configuration = @('Release', 'Debug')
)

[string] $framework = 'net48'
[string] $repositoryRoot = Split-Path -Path $PSScriptRoot -Parent
[string] $buildRoot = Join-Path -Path $repositoryRoot -ChildPath 'Build\bin'
[string] $moduleSourcePath = Join-Path -Path $repositoryRoot -ChildPath 'Src\DSInternals.Passkeys'

foreach($currentConfiguration in $Configuration) {
    [string] $configurationOutputPath = Join-Path -Path $buildRoot -ChildPath $currentConfiguration
    [string] $binariesParentPath = Join-Path -Path $configurationOutputPath -ChildPath 'DSInternals.Win32.WebAuthn'
    [string] $binariesPath = Join-Path -Path $binariesParentPath -ChildPath $framework
    [string] $moduleDestinationPath = Join-Path -Path $configurationOutputPath -ChildPath 'DSInternals.Passkeys'

    # Copy module files (*.psd1, *.psm1,...)
    Copy-Item -Path $moduleSourcePath -Destination $configurationOutputPath -Container -Recurse -Verbose -Force | Out-Null

    # Copy the compiled binaries
    Get-ChildItem -Path $binariesPath -Recurse -File -Include '*.dll','*.pdb' | Copy-Item -Destination $moduleDestinationPath -Verbose -Force | Out-Null
}
