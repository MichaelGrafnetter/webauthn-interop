<#
.SYNOPSIS
    This script contains Pester integration tests for the DSInternals.Passkeys PowerShell module.
.PARAMETER ModulePath
    Path to the compiled module directory.
.PARAMETER MarkdownDocumentationPath
    Path to the directory containing the project's markdown documentation.
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
    [string] $MarkdownDocumentationPath,

    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [ValidateSet('Debug', 'Release')]
    [string] $Configuration = 'Release'
)

if ([string]::IsNullOrWhiteSpace($ModulePath)) {
    # No path has been provided, so use the default value
    $ModulePath = Join-Path -Path $PSScriptRoot -ChildPath "..\..\..\Build\bin\PSModule\$Configuration\DSInternals.Passkeys" -Resolve -ErrorAction Stop
}

if ([string]::IsNullOrWhiteSpace($MarkdownDocumentationPath)) {
    # No path has been provided, so use the default value
    $MarkdownDocumentationPath = Join-Path -Path $PSScriptRoot -ChildPath '..\..\..\Documentation' -Resolve -ErrorAction Stop
}

Describe 'PowerShell Module' {
    BeforeAll {
        [string] $moduleManifestPath = Join-Path -Path $ModulePath -ChildPath 'DSInternals.Passkeys.psd1'
        # Import-PowerShellDataFile cannot evaluate the manifest's per-edition RequiredAssemblies expression,
        # so Test-ModuleManifest is used to obtain the parsed module info.
        [System.Management.Automation.PSModuleInfo] $manifest = Test-ModuleManifest -Path $moduleManifestPath -WarningAction SilentlyContinue -ErrorAction Stop
        [string] $aboutPagePath = Join-Path -Path $ModulePath -ChildPath 'en-US\about_DSInternals.Passkeys.help.txt'
        [string] $modulePagePath = Join-Path -Path $MarkdownDocumentationPath -ChildPath 'PowerShell\README.md'
    }

    Context 'Manifest' {
        BeforeDiscovery {
            [hashtable[]] $bundledFiles =
                Get-ChildItem -Path $ModulePath -Recurse -File -Exclude *.pdb,*.psd1 |
                ForEach-Object { @{ FileName = $PSItem.Name } }

            # Discover function-level [Alias(...)] declarations across every .psm1 in the built module.
            # Function-level attributes sit at 4-space indent; parameter-level ones sit deeper, so this
            # filter avoids picking up parameter aliases.
            [string[]] $modulePsm1Files = Get-ChildItem -Path $ModulePath -Filter *.psm1 |
                Select-Object -ExpandProperty FullName

            [hashtable[]] $moduleAliases =
                Select-String -Path $modulePsm1Files -Pattern '^\s{4}\[Alias\(([^)]+)\)\]' |
                ForEach-Object {
                    [regex]::Matches($PSItem.Matches.Groups[1].Value, "'([^']+)'") |
                        ForEach-Object { @{ AliasName = $PSItem.Groups[1].Value } }
                }
        }

        It 'exists' {
            $moduleManifestPath | Should -Exist
        }

        It 'is valid' {
            Test-ModuleManifest -Path $moduleManifestPath -ErrorAction Stop
        }

        It 'references the <FileName> file' -TestCases $bundledFiles -Test {
            param([string] $FileName)

            $moduleManifestPath | Should -FileContentMatch $FileName
        }

        It 'exports alias <AliasName>' -TestCases $moduleAliases -Test {
            param([string] $AliasName)

            $moduleManifestPath | Should -FileContentMatch "'$AliasName'"
        }
    }

    Context 'File Structure' {
        BeforeDiscovery {
            [hashtable[]] $mamlHelpFiles = @(
                @{ MamlFileName = 'DSInternals.Passkeys.Core.psm1-help.xml' },
                @{ MamlFileName = 'DSInternals.Passkeys.Entra.psm1-help.xml' },
                @{ MamlFileName = 'DSInternals.Passkeys.Okta.psm1-help.xml' }
            )
        }

        It 'contains the <MamlFileName> MAML help file' -TestCases $mamlHelpFiles -Test {
            param([string] $MamlFileName)

            Join-Path -Path $ModulePath -ChildPath "en-US\$MamlFileName" | Should -Exist
        }

        It 'contains an About topic' {
            $aboutPagePath | Should -Exist
        }

        It 'has a properly named About topic' {
            Select-String -Path $aboutPagePath -Pattern 'about_DSInternals.Passkeys' -CaseSensitive -SimpleMatch -Quiet | Should -BeTrue
        }

        It 'does not contain debug symbols' {
            # Only the framework-specific DSInternals.Win32.WebAuthn.pdb files should be present to simplify troubleshooting.

            Get-ChildItem -Path $ModulePath -Recurse -Filter *.pdb |
                Should -HaveCount 2
        }

        It 'does not contain unit tests' {
            Get-ChildItem -Path $ModulePath -Recurse -Filter *.Test.* | Should -BeNull
        }

        It 'does not contain .NET XML documentation' {
            Get-ChildItem -Path $ModulePath -Recurse -Filter *.xml -Exclude *help.xml | Should -BeNull
        }
    }

    Context 'Markdown Documentation' {
        BeforeDiscovery {
            # Parse every per-nested-module MAML file and aggregate the cmdlet metadata.
            [string[]] $mamlPaths = @(
                'DSInternals.Passkeys.Core.psm1-help.xml',
                'DSInternals.Passkeys.Entra.psm1-help.xml',
                'DSInternals.Passkeys.Okta.psm1-help.xml'
            ) | ForEach-Object { Join-Path -Path $ModulePath -ChildPath "en-US\$PSItem" }

            [hashtable[]] $cmdlets = $mamlPaths |
                Where-Object { Test-Path -Path $PSItem } |
                ForEach-Object {
                    [xml] $maml = Get-Content -Path $PSItem -ErrorAction Stop
                    $maml.helpItems.command.details | ForEach-Object { @{
                        Cmdlet = $PSItem.name
                        Description = $PSItem.description.para
                    } }
                }
        }

        It 'CHANGELOG should be up-to-date' {
            [string] $changeLogPath = Join-Path -Path $MarkdownDocumentationPath -ChildPath 'CHANGELOG.md'

            # Check that the versions match
            [version] $moduleVersion = $manifest.Version
            $changeLogPath | Should -FileContentMatch "## \[$moduleVersion\]"
        }

        It 'contains proper module description' {
            [string] $moduleDescription = $manifest.Description.Trim()
            $modulePagePath | Should -FileContentMatch $moduleDescription
        }

        It 'contains the <Cmdlet> cmdlet' -TestCases $cmdlets -Test {
            param([string] $Cmdlet, [string] $Description)

            # Sample link: ### [New-Passkey](New-Passkey.md)
            [string] $cmdletLinkFormat = '### \[{0}\]\({0}\.md\)' -f $Cmdlet
            $modulePagePath | Should -FileContentMatchExactly -ExpectedContent $cmdletLinkFormat
        }

        It 'contains proper description of the <Cmdlet> cmdlet' -TestCases $cmdlets -Test {
            param([string] $Cmdlet, [string] $Description)

            # FileContentMatchMultilineExactly treats the expected content as a regex, so any
            # regex metacharacters in the cmdlet description (e.g. parentheses) must be escaped
            # to ensure literal matching against the README.
            $modulePagePath | Should -FileContentMatchMultilineExactly -ExpectedContent ([regex]::Escape($Description))
        }
    }
}
