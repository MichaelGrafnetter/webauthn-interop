<#
.SYNOPSIS
Generates API documentation from XML files.
#>

# We are using the XmlDocMarkdown tool
# See https://ejball.com/XmlDocMarkdown/
$command = Get-Command -Name xmldocmd -CommandType Application -ErrorAction SilentlyContinue
if($null -eq $command)
{
    # Install the required tool
    dotnet tool install xmldocmd -g
}

$outputPath = "$PSScriptRoot\..\Documentation\API"

$namespace = 'DSInternals.Win32.WebAuthn'
$assemblyPath = "$PSScriptRoot\..\Build\bin\Release\DSInternals.Win32.WebAuthn\net48\DSInternals.Win32.WebAuthn.dll"
$sourceUri = 'https://github.com/MichaelGrafnetter/webauthn-interop/tree/master/Src/DSInternals.Win32.WebAuthn'
xmldocmd $assemblyPath $outputPath --source $sourceUri --namespace $namespace --visibility public --clean

$namespace = 'DSInternals.Win32.WebAuthnAdapter'
$assemblyPath = "$PSScriptRoot\..\Build\bin\Release\DSInternals.Win32.WebAuthn.Adapter\net6.0\DSInternals.Win32.WebAuthn.Adapter.dll"
$sourceUri = 'https://github.com/MichaelGrafnetter/webauthn-interop/tree/master/Src/DSInternals.Win32.WebAuthn.Adapter'
xmldocmd $assemblyPath $outputPath --source $sourceUri --namespace $namespace --visibility public --clean
