<#
.SYNOPSIS
Invokes Pester integration tests for the DSInternals.Passkeys PowerShell module.

.PARAMETER ModulePath
The path to the DSInternals.Passkeys module.

.PARAMETER MarkdownDocumentationPath
The path to the directory containing the project's markdown documentation.

#>

#Requires -Version 5.1
#Requires -Modules @{ ModuleName = 'Pester'; ModuleVersion = '5.7.0' }

param(
    [ValidateNotNullOrEmpty()]
    [string] $ModulePath = (Join-Path -Path $PSScriptRoot -ChildPath '..\..\Build\bin\PSModule\Release\DSInternals.Passkeys'),

    [ValidateNotNullOrEmpty()]
    [string] $MarkdownDocumentationPath = (Join-Path -Path $PSScriptRoot -ChildPath '..\..\Documentation')
)

[string] $moduleManifestPath = Join-Path -Path $ModulePath -ChildPath 'DSInternals.Passkeys.psd1'
[string] $moduleFilePath = Join-Path -Path $ModulePath -ChildPath 'DSInternals.Passkeys.psm1'
[hashtable] $manifest =  Import-PowerShellDataFile -Path $moduleManifestPath
[string] $aboutPagePath = Join-Path -Path $ModulePath -ChildPath 'en-US\about_DSInternals.Passkeys.help.txt'
[string] $modulePagePath = Join-Path -Path $MarkdownDocumentationPath -ChildPath 'PowerShell\README.md'
[string[]] $modulePage = Get-Content -Path $modulePagePath -ErrorAction Stop

Describe 'PowerShell Module' {
    Context 'Manifest' {
        BeforeDiscovery {
            [hashtable[]] $bundledFiles =
                Get-ChildItem -Path $ModulePath -Recurse -File -Exclude *.pdb,*.psd1 |
                ForEach-Object { @{ FileName = $PSItem.Name } }

            [hashtable[]] $moduleAliases =
                Select-String -Path $moduleFilePath -Pattern 'New-Alias -Name ([a-zA-Z\-]+) ' |
                ForEach-Object { @{ AliasName = $PSItem.Matches.Groups[1].Value } }
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
        It 'contains MAML help' {
            Join-Path $ModulePath 'en-US\DSInternals.Passkeys-help.xml' | Should -Exist
        }

        It 'contains an About topic' {
            $aboutPagePath | Should -Exist
        }

        It 'has a properly named About topic' {
            Select-String -Path $aboutPagePath -Pattern 'about_DSInternals.Passkeys' -CaseSensitive -SimpleMatch -Quiet | Should -BeTrue
        }

        It 'does not contain debug symbols' {
            # Only the DSInternals.Win32.WebAuthn.pdb framework-specific should be present to simplify troubleshooting.

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
            # Parse MAML
            [string] $mamlPath = Join-Path $ModulePath 'en-US\DSInternals.Passkeys-help.xml'
            [xml] $maml = Get-Content -Path $mamlPath -ErrorAction Stop
            [hashtable[]] $cmdlets = $maml.helpItems.command.details | ForEach-Object { @{
                Cmdlet = $PSItem.name;
                Description = $PSItem.description.para
            } }
        }

        It 'CHANGELOG should be up-to-date' {
            [string] $changeLogPath = Join-Path $MarkdownDocumentationPath 'CHANGELOG.md'

            # Check that the versions match
            [version] $moduleVersion = $manifest.ModuleVersion
            $changeLogPath | Should -FileContentMatch "## [$moduleVersion]"
        }

        It 'contains proper module description' {
            [string] $moduleDescription = $manifest.Description.Trim()
            $modulePagePath | Should -FileContentMatch $moduleDescription
        }

        It 'contains the <Cmdlet> cmdlet' -TestCases $cmdlets -Test {
            param([string] $Cmdlet, [string] $Description)

            # Sample link: ### [New-Passkey](New-Passkey.md#new-passkey)
            [string] $cmdletLinkFormat = '### \[{0}\]\({0}\.md#{1}\)' -f $Cmdlet,$Cmdlet.ToLower()
            $modulePagePath | Should -FileContentMatchExactly -ExpectedContent $cmdletLinkFormat
        }

        It 'contains proper description of the <Cmdlet> cmdlet' -TestCases $cmdlets -Test {
            param([string] $Cmdlet, [string] $Description)

            # Remove markdown links before searching
            $modulePage |
                ForEach-Object { $PSItem -replace '\[([a-zA-Z\-]+)\]\(([([a-zA-Z\-]+)\.md#[a-z\-]+\)','$1' } |
                Where-Object { $PSItem -ceq $Description } |
                Should -HaveCount 1
        }
    }
}
