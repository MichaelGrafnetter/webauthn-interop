#
# Module manifest for module 'DSInternals.Passkeys'
#

@{

# Script module or binary module file associated with this manifest.
RootModule = 'DSInternals.Passkeys.psm1'

# Version number of this module.
ModuleVersion = '2.1.0'

# Supported PSEditions
CompatiblePSEditions = @('Desktop','Core')

# ID used to uniquely identify this module
GUID = '2561bb28-b9cd-4cf1-b35c-96ea07e13ca1'

# Author of this module
Author = 'Michael Grafnetter'

# Company or vendor of this module
CompanyName = 'DSInternals'

# Copyright statement for this module
Copyright = 'Copyright (c) 2021-2026 Michael Grafnetter. All rights reserved.'

# Description of the functionality provided by this module
Description = 'Allows administrative registration of passkeys (i.e. FIDO2 security keys and Microsoft Authenticator mobile app) in Microsoft Entra ID (formerly Azure Active Directory) through the Microsoft Graph API.'

# Minimum version of the PowerShell engine required by this module
PowerShellVersion = '5.1'

# Name of the PowerShell host required by this module
# PowerShellHostName = ''

# Minimum version of the PowerShell host required by this module
# PowerShellHostVersion = ''

# Minimum version of Microsoft .NET Framework required by this module. This prerequisite is valid for the PowerShell Desktop edition only.
# DotNetFrameworkVersion = ''

# Minimum version of the common language runtime (CLR) required by this module. This prerequisite is valid for the PowerShell Desktop edition only.
# ClrVersion = ''

# Processor architecture (None, X86, Amd64) required by this module
ProcessorArchitecture = 'None'

# Modules that must be imported into the global environment prior to importing this module
RequiredModules = @('Microsoft.Graph.Authentication','Microsoft.Graph.Identity.SignIns')

# Assemblies that must be loaded prior to importing this module
RequiredAssemblies = if ($PSEdition -eq 'Core') {
    @(
        'net8.0-windows\Microsoft.Bcl.Memory.dll',
        'net8.0-windows\System.Formats.Cbor.dll',
        'net8.0-windows\Microsoft.Extensions.Logging.Abstractions.dll',
        'net8.0-windows\Microsoft.Extensions.DependencyInjection.Abstractions.dll',
        'net8.0-windows\Microsoft.IdentityModel.Abstractions.dll',
        'net8.0-windows\Microsoft.IdentityModel.Logging.dll',
        'net8.0-windows\Microsoft.IdentityModel.Tokens.dll',
        'net8.0-windows\Microsoft.IdentityModel.JsonWebTokens.dll',
        'net8.0-windows\Microsoft.Identity.Client.dll',
        'net8.0-windows\System.IdentityModel.Tokens.Jwt.dll',
        'net8.0-windows\DSInternals.Win32.WebAuthn.dll'
    )
}
else {
    @(
        'net48\Microsoft.Bcl.AsyncInterfaces.dll',
        'net48\Microsoft.Bcl.HashCode.dll',
        'net48\Microsoft.Bcl.Memory.dll',
        'net48\Microsoft.Bcl.TimeProvider.dll',
        'net48\System.Buffers.dll',
        'net48\System.Memory.dll',
        'net48\System.Numerics.Vectors.dll',
        'net48\System.Runtime.CompilerServices.Unsafe.dll',
        'net48\System.Threading.Tasks.Extensions.dll',
        'net48\System.Text.Encodings.Web.dll',
        'net48\System.Text.Json.dll',
        'net48\System.IO.Pipelines.dll',
        'net48\System.Diagnostics.DiagnosticSource.dll',
        'net48\System.Formats.Asn1.dll',
        'net48\System.Formats.Cbor.dll',
        'net48\Microsoft.Extensions.Logging.Abstractions.dll',
        'net48\Microsoft.IdentityModel.Abstractions.dll',
        'net48\Microsoft.IdentityModel.Logging.dll',
        'net48\Microsoft.IdentityModel.Tokens.dll',
        'net48\Microsoft.IdentityModel.JsonWebTokens.dll',
        'net48\Microsoft.Identity.Client.dll',
        'net48\System.IdentityModel.Tokens.Jwt.dll',
        'net48\DSInternals.Win32.WebAuthn.dll'
    )
}

# Script files (.ps1) that are run in the caller's environment prior to importing this module.
# ScriptsToProcess = @()

# Type files (.ps1xml) to be loaded when importing this module
# TypesToProcess = @()

# Format files (.ps1xml) to be loaded when importing this module
FormatsToProcess = @('DSInternals.Passkeys.Format.ps1xml')

# Modules to import as nested modules of the module specified in RootModule/ModuleToProcess
# NestedModules = @()

# Functions to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no functions to export.
FunctionsToExport = @(
    'Get-PasskeyRegistrationOptions',
    'New-Passkey',
    'Register-Passkey',
    'Test-Passkey',
    'Connect-Okta',
    'Disconnect-Okta',
    'Get-PasskeyAuthenticatorPlugin',
    'Get-PasskeyAuthenticator',
    'Get-PasskeyWindowsHello',
    'Remove-PasskeyWindowsHello',
    'New-PasskeyRandomChallenge'
)

# Cmdlets to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no cmdlets to export.
CmdletsToExport = @()

# Variables to export from this module
VariablesToExport = @()

# Aliases to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no aliases to export.
AliasesToExport = @('Register-MgUserAuthenticationFido2Method')

# DSC resources to export from this module
# DscResourcesToExport = @()

# List of all modules packaged with this module
# ModuleList = @()

# List of all files packaged with this module
FileList = @(
    'DSInternals.Passkeys.Format.ps1xml',
    'en-US/about_DSInternals.Passkeys.help.txt',
    'en-US/DSInternals.Passkeys-help.xml',
    'net48/DSInternals.Win32.WebAuthn.dll',
    'net48/Microsoft.Bcl.AsyncInterfaces.dll',
    'net48/Microsoft.Bcl.HashCode.dll',
    'net48/Microsoft.Bcl.Memory.dll',
    'net48/Microsoft.Bcl.TimeProvider.dll',
    'net48/Microsoft.Extensions.Logging.Abstractions.dll',
    'net48/Microsoft.Identity.Client.dll',
    'net48/Microsoft.IdentityModel.Abstractions.dll',
    'net48/Microsoft.IdentityModel.JsonWebTokens.dll',
    'net48/Microsoft.IdentityModel.Logging.dll',
    'net48/Microsoft.IdentityModel.Tokens.dll',
    'net48/System.Buffers.dll',
    'net48/System.Diagnostics.DiagnosticSource.dll',
    'net48/System.Formats.Asn1.dll',
    'net48/System.Formats.Cbor.dll',
    'net48/System.IdentityModel.Tokens.Jwt.dll',
    'net48/System.IO.Pipelines.dll',
    'net48/System.Memory.dll',
    'net48/System.Numerics.Vectors.dll',
    'net48/System.Runtime.CompilerServices.Unsafe.dll',
    'net48/System.Text.Encodings.Web.dll',
    'net48/System.Text.Json.dll',
    'net48/System.Threading.Tasks.Extensions.dll',
    'net8.0-windows/DSInternals.Win32.WebAuthn.dll',
    'net8.0-windows/Microsoft.Bcl.Memory.dll',
    'net8.0-windows/Microsoft.Extensions.DependencyInjection.Abstractions.dll',
    'net8.0-windows/Microsoft.Extensions.Logging.Abstractions.dll',
    'net8.0-windows/Microsoft.Identity.Client.dll',
    'net8.0-windows/Microsoft.IdentityModel.Abstractions.dll',
    'net8.0-windows/Microsoft.IdentityModel.JsonWebTokens.dll',
    'net8.0-windows/Microsoft.IdentityModel.Logging.dll',
    'net8.0-windows/Microsoft.IdentityModel.Tokens.dll',
    'net8.0-windows/System.Diagnostics.EventLog.dll',
    'net8.0-windows/System.Formats.Cbor.dll',
    'net8.0-windows/System.IdentityModel.Tokens.Jwt.dll'
)

# Private data to pass to the module specified in RootModule/ModuleToProcess. This may also contain a PSData hashtable with additional module metadata used by PowerShell.
PrivateData = @{

    PSData = @{

        # Tags applied to this module. These help with module discovery in online galleries.
        Tags = @('Windows', 'PSEdition_Desktop', 'PSEdition_Core', 'PSModule', 'FIDO2', 'Passkeys', 'EntraID', 'AzureAD', 'Azure', 'WebAuthn', 'CTAP', 'Security','2FA','MFA','Authentication','Identity','Passwordless','Okta')

        # A URL to the license for this module.
        LicenseUri = 'https://github.com/MichaelGrafnetter/webauthn-interop/blob/main/LICENSE'

        # A URL to the main website for this project.
        ProjectUri = 'https://github.com/MichaelGrafnetter/webauthn-interop'

        # A URL to an icon representing this module.
        IconUri = 'https://raw.githubusercontent.com/MichaelGrafnetter/DSInternals/master/Src/Icons/module_black.png'

        # ReleaseNotes of this module
        ReleaseNotes = @'
- Added the -Hint parameter to the Test-Passkey cmdlet.
'@

        # Prerelease string of this module
        # Prerelease = ''

        # Flag to indicate whether the module requires explicit user acceptance for install/update/save
        RequireLicenseAcceptance = $false

        # External dependent modules of this module
        # ExternalModuleDependencies = @()

    } # End of PSData hashtable
} # End of PrivateData hashtable

# HelpInfo URI of this module
# HelpInfoURI = ''

# Default prefix for commands exported from this module. Override the default prefix using Import-Module -Prefix.
# DefaultCommandPrefix = ''

}

