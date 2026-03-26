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

[string[]] $frameworks = @('net48', 'net8.0-windows')
[string] $repositoryRoot = Split-Path -Path $PSScriptRoot -Parent
[string] $buildRoot = Join-Path -Path $repositoryRoot -ChildPath 'Build\bin\PSModule'
[string] $moduleSourcePath = Join-Path -Path $repositoryRoot -ChildPath 'Src\DSInternals.Passkeys'
[string] $libraryProject = Join-Path -Path $repositoryRoot -ChildPath 'Src\DSInternals.Win32.WebAuthn\DSInternals.Win32.WebAuthn.csproj'

foreach($currentConfiguration in $Configuration) {
    [string] $configurationOutputPath = Join-Path -Path $buildRoot -ChildPath $currentConfiguration
    [string] $moduleDestinationPath = Join-Path -Path $configurationOutputPath -ChildPath 'DSInternals.Passkeys'

    # Copy module files (*.psd1, *.psm1,...) excluding test files and directories
    Get-ChildItem -Path $moduleSourcePath -Recurse |
        Where-Object Name -notlike '*Test*' |
        ForEach-Object {
            [string] $destinationPath = $PSItem.FullName.Replace($moduleSourcePath, $moduleDestinationPath)
            if ($PSItem.PSIsContainer) {
                # Create the directory
                New-Item -ItemType Directory -Path $destinationPath -Force | Out-Null
            } else {
                # Copy the file
                Copy-Item -Path $PSItem.FullName -Destination $destinationPath -Verbose -Force | Out-Null
            }
        }

    foreach($framework in $frameworks){
        [string] $frameworkSpecificPath = Join-Path -Path $moduleDestinationPath -ChildPath $framework

        # Copy the compiled binaries
        dotnet.exe publish $libraryProject --output $frameworkSpecificPath --nologo --framework $framework --configuration $currentConfiguration --no-restore --no-build
    }
}
