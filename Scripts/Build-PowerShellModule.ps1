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

[string[]] $frameworks = @('net48', 'net6.0')
[string] $repositoryRoot = Split-Path -Path $PSScriptRoot -Parent
[string] $buildRoot = Join-Path -Path $repositoryRoot -ChildPath 'Build\bin'
[string] $moduleSourcePath = Join-Path -Path $repositoryRoot -ChildPath 'Src\DSInternals.Passkeys'
[string] $libraryProject = Join-Path -Path $repositoryRoot -ChildPath 'Src\DSInternals.Win32.WebAuthn\DSInternals.Win32.WebAuthn.csproj'

foreach($currentConfiguration in $Configuration) {
    [string] $configurationOutputPath = Join-Path -Path $buildRoot -ChildPath $currentConfiguration
    [string] $moduleDestinationPath = Join-Path -Path $configurationOutputPath -ChildPath 'DSInternals.Passkeys'

    # Copy module files (*.psd1, *.psm1,...)
    Copy-Item -Path $moduleSourcePath -Destination $configurationOutputPath -Container -Recurse -Verbose -Force | Out-Null

    foreach($framework in $frameworks){
        [string] $frameworkSpecificPath = Join-Path -Path $moduleDestinationPath -ChildPath $framework

        # Copy the compiled binaries
        dotnet.exe publish $libraryProject --output $frameworkSpecificPath --nologo --framework $framework --configuration $currentConfiguration --property:GenerateDocumentationFile=false --no-restore --no-build --self-contained false
    }
}
