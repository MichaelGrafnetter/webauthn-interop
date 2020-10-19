# Strong Name Key Files

## Overview

The [DSInternals.Public.snk](./DSInternals.Public.snk) file contains the public key that can be used to verify
the strong name signature of the official binaries.

The corresponding private key, contained in the `DSInternals.Private.snk` file,
is not publicly available. If you want to strong sign the resulting assemblies,
you have to generate your own key pair.

## Signing Policy

The signing is configured in the [Directory.Build.props](../Src/Directory.Build.props) file with this logic:
- The strong name key signing is only performed during a *Release* build.
- If the private key is not present, signing is skipped.

## Generating a Custom Key Pair

A custom key pair can be generated using the `sn.exe` tool from Windows SDK:

```cmd
SET SN="%ProgramFiles(x86)%\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8 Tools\x64\sn.exe"
%SN% -k DSInternals.Private.snk
%SN% -p DSInternals.Private.snk DSInternals.Public.snk
```

## Validation

The following PowerShell code snippet can be used to check if an assembly is signed:
```powershell
$assembly = 'DSInternals.Win32.WebAuthn.dll'
$assemblyName = [System.Reflection.AssemblyName]::GetAssemblyName($assembly)
$assemblyName.Flags.HasFlag([System.Reflection.AssemblyNameFlags]::PublicKey) 
```