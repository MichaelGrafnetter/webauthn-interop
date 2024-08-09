<#
.SYNOPSIS
Generates API documentation from XML files.

#>

#Requires -Version 5

# We are using the XmlDocMarkdown tool
# See https://ejball.com/XmlDocMarkdown/
[System.Management.Automation.ApplicationInfo] $command = Get-Command -Name xmldocmd -CommandType Application -ErrorAction SilentlyContinue

if($null -eq $command)
{
    # Install the required tool
    dotnet.exe tool install xmldocmd -g
}

[string] $outputPath = "$PSScriptRoot\..\Documentation\API"

[string] $namespace = 'DSInternals.Win32.WebAuthn'
[string] $assemblyPath = "$PSScriptRoot\..\Build\bin\Release\DSInternals.Win32.WebAuthn\net48\DSInternals.Win32.WebAuthn.dll"
[string] $sourceUri = 'https://github.com/MichaelGrafnetter/webauthn-interop/tree/master/Src/DSInternals.Win32.WebAuthn'
xmldocmd.exe $assemblyPath $outputPath --source $sourceUri --namespace $namespace --visibility public --clean

[string] $namespace = 'DSInternals.Win32.WebAuthnAdapter'
[string] $assemblyPath = "$PSScriptRoot\..\Build\bin\Release\DSInternals.Win32.WebAuthn.Adapter\net6.0\DSInternals.Win32.WebAuthn.Adapter.dll"
[string] $sourceUri = 'https://github.com/MichaelGrafnetter/webauthn-interop/tree/master/Src/DSInternals.Win32.WebAuthn.Adapter'
xmldocmd.exe $assemblyPath $outputPath --source $sourceUri --namespace $namespace --visibility public --clean
