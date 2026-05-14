<#
.SYNOPSIS
    Invokes Pester tests for the DSInternals.Passkeys PowerShell module.
.PARAMETER ModulePath
    Path to the compiled PowerShell module
#>

#Requires -Version 5.1
#Requires -Modules @{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

[CmdletBinding()]
param(
    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [string] $ModulePath,

    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [ValidateSet('Debug', 'Release')]
    [string] $Configuration = 'Debug'
)

if ([string]::IsNullOrWhiteSpace($ModulePath)) {
    # No path has been provided, so use a the default value
    $ModulePath = Join-Path -Path $PSScriptRoot -ChildPath "..\..\Build\bin\PSModule\$Configuration\DSInternals.Passkeys" -Resolve -ErrorAction Stop
} else {
    [bool] $isFile = Test-Path -Path $ModulePath -PathType Leaf -ErrorAction SilentlyContinue
    if ($isFile) {
        # This is probably the module manifest path
        # Get the path to the module directory, without the trailing slash
        $ModulePath = Split-Path -Path $ModulePath -Parent -Resolve -ErrorAction Stop
    } else {
        # Translate possibly relative module directory path to an absolute one
        $ModulePath = Resolve-Path -Path $ModulePath -ErrorAction Stop
    }
}

# Load the required PowerShell modules
Import-Module -Name Pester -ErrorAction Stop

# Ensure the test results output directory exists; Pester writes the file directly and will not create it.
[string] $testResultPath = Join-Path -Path $PSScriptRoot -ChildPath '..\..\Build\TestResults\Pester.xml'
[string] $testResultDirectory = Split-Path -Path $testResultPath -Parent
New-Item -Path $testResultDirectory -ItemType Directory -Force | Out-Null

# Invoke the tests
[PesterConfiguration] $config = [PesterConfiguration]::Default
$config.Run.Container = New-PesterContainer -Path $PSScriptRoot -Data @{
    ModulePath    = $ModulePath # Compiled module directory
    Configuration = $Configuration # Build configuration (gates the strong-name check)
}
$config.Output.Verbosity = 'Detailed'
$config.Output.StackTraceVerbosity = 'None'
$config.TestResult.Enabled = $true
$config.TestResult.OutputFormat = 'NUnitXml'
$config.TestResult.OutputPath = $testResultPath

Invoke-Pester -Configuration $config
