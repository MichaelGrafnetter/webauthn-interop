<#
.SYNOPSIS
Regenerates the API documentation in Documentation\API using DocFX.

.DESCRIPTION
DocFX must be installed as a global .NET tool before running this script:

    dotnet tool install --global docfx

.NOTES
DocFX is configured with noRestore=true, so packages must be restored beforehand

#>

#Requires -Version 5

[CmdletBinding()]
param(
    [Parameter(Mandatory = $false)]
    [ValidateSet('Release', 'Debug')]
    [string] $Configuration = 'Release'
)

[string] $repositoryRoot = Split-Path -Path $PSScriptRoot -Parent -ErrorAction Stop
[string] $docfxConfig = Join-Path -Path $repositoryRoot -ChildPath 'Src\docfx.json' -ErrorAction Stop

# Generate the Markdown API reference files
docfx.exe metadata $docfxConfig --property Configuration=$Configuration
if ($LASTEXITCODE -ne 0) {
    throw 'docfx metadata failed'
}
