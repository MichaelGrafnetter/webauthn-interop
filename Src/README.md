# Directory Structure

| Directory                                | Contents                                                                   |
|:-----------------------------------------|:---------------------------------------------------------------------------|
| DSInternals.Passkeys                     | PowerShell module for interacting with Passkeys / FIDO2 / CTAP on Windows. |
| DSInternals.Win32.WebAuthn               | Passkeys / FIDO2 / W3C Web Authentication .NET Library.                    |
| DSInternals.Win32.WebAuthn.Tests         | Automated tests (VSTest) that can be executed as part of the buid process. |
| DSInternals.Win32.WebAuthn.Adapter       | Adapter classes for the FIDO2.NET library.                                 |
| DSInternals.Win32.WebAuthn.Adapter.Tests | Automated tests (VSTest) that can be executed as part of the buid process. |
| Fido2UI                                  | Windows GUI application (WPF) that utilizes the library above.             |
| Icons                                    | Contains application and NuGet package icons.                              |
