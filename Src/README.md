# Directory Structure

## DSInternals.Win32.WebAuthn

FIDO2 / W3C Web Authentication .NET Library

## Fido2UI

Windows GUI application (WPF) that utilizes the library above.

## webauthn

Submodule containing the `webauthn.h` C header file. Fetched from Microsoft's official repository.

## DSInternals.Win32.WebAuthn.CodeGen

CLI application that was used to generate the `ApiConstants.cs` file from `webauthn.h`.
The app only needs to be executed when the API changes.

## Icons

Contains application and NuGet package icons.

## DSInternals.Win32.WebAuthn.Tests

Automated tests (VSTest) that can be executed as part of the buid process.

## DSInternals.Win32.WebAuthn.UITests

Tests that require user interaction. These are only intended to be executed manually one-by-one from Visual Studio.
