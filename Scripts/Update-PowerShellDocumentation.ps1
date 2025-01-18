<#
.SYNOPSIS
Refreshes MD documentation files and builds MAML files.

#>

#Requires -Version 5 -Module platyPS

# Set directory paths
[string] $rootDir = Split-Path -Path $PSScriptRoot -Parent -ErrorAction Stop
[string] $locale = 'en-US'
[string] $modulePath = Join-Path -Path $rootDir -ChildPath 'Src\DSInternals.Passkeys'
[string] $mdHelpPath = Join-Path -Path $rootDir -ChildPath 'Documentation\PowerShell' -ErrorAction Stop
[string] $modulePagePath = Join-Path -Path $mdHelpPath -ChildPath 'README.md' -ErrorAction Stop
[string] $xmlHelpPath = Join-Path -Path $modulePath -ChildPath $locale -ErrorAction Stop

# Import dependencies
Import-Module -Name platyPS -ErrorAction Stop

# Load the freshly compiled module to generate the help for
Import-Module -Name $modulePath -ErrorAction Stop

# Note: This code has been used to create the initial version of the help files:
# New-MarkdownHelp -Module 'DSInternals.Passkeys' -AlphabeticParamsOrder -Locale $locale -WithModulePage -HelpVersion 1.0 -OutputFolder $mdHelpPath -ModulePagePath $modulePagePath -
# New-MarkdownAboutHelp -AboutName 'DSInternals.Passkeys' -OutputFolder $mdHelpPath

# Update MD files
Update-MarkdownHelpModule -Path $mdHelpPath -ModulePagePath $modulePagePath -RefreshModulePage:$false -AlphabeticParamsOrder -UpdateInputOutput

# Generate the MAML file
New-ExternalHelp -Path $mdHelpPath -OutputPath $xmlHelpPath -Force -ShowProgress
