<#
.SYNOPSIS
Invokes Pester tests for the DSInternals.Passkeys PowerShell module.

#>

#Requires -Version 5.1
#Requires -Modules @{ ModuleName = 'Pester'; ModuleVersion = '5.7.0' }

Import-Module -Name Pester -Force -ErrorAction Stop

[string] $testsPath = Join-Path -Path $PSScriptRoot -ChildPath '..\Src\DSInternals.Passkeys.Tests'
[string] $outputDirectory = Join-Path -Path $PSScriptRoot -ChildPath '..\TestResults'
[string] $outputFile = Join-Path -Path $outputDirectory -ChildPath 'PesterTestResults.xml'

# Create output dir if it does not exist
New-Item -Path $outputDirectory -ItemType Directory -Force | Out-Null

[PesterConfiguration] $config = New-PesterConfiguration
$config.Run.Path = $testsPath
$config.Output.Verbosity = 'Detailed'
$config.TestResult.Enabled = $true
$config.TestResult.OutputFormat = 'NUnitXml'
$config.TestResult.OutputPath = $outputFile

Invoke-Pester -Configuration $config
