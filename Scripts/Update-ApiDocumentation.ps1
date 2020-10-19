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

# Build command parameters
$namespace = 'DSInternals.Win32.WebAuthn'
$assemblyPath = "$PSScriptRoot\..\Build\bin\Release\DSInternals.Win32.WebAuthn\net472\DSInternals.Win32.WebAuthn.dll"
$outputPath = "$PSScriptRoot\..\Documentation\API"
$sourceUri = 'https://github.com/MichaelGrafnetter/webauthn-interop/tree/master/Src/DSInternals.Win32.WebAuthn'

# Generate the API documentation
xmldocmd $assemblyPath $outputPath --source $sourceUri --namespace $namespace --visibility public --clean