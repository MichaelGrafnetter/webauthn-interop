# WebAuthn Interop Assembly
[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](../LICENSE.txt)
[![Windows 10 1903+](https://img.shields.io/badge/Windows%2010-1903%2B-007bb8.svg?logo=Windows)](#)
[![.NET Framework 4.7+](https://img.shields.io/badge/.NET%20Framework-4.7%2B-007FFF.svg)](#)
[![.NET Core 3+](https://img.shields.io/badge/.NET%20Core-3%2B-007FFF.svg)](#)

**FIDO2 / W3C Web Authentication .NET Library for Windows Desktop and CLI Applications**

## Introduction

This library allows .NET applications to directly interact with FIDO2 security keys (e.g. [YubiKey](https://www.yubico.com/products/), [Feitian](https://www.ftsafe.com/products/FIDO), or [Crayonic](https://www.crayonic.com/)) and with [Windows Hello](https://support.microsoft.com/en-us/windows/learn-about-windows-hello-and-set-it-up-dae28983-8242-bb2a-d3d1-87c9d265a5f0) / [Windows Hello for Business](https://docs.microsoft.com/en-us/windows/security/identity-protection/hello-for-business/hello-identity-verification). 
It provides a managed wrapper of the low-level [Windows 10 WebAuthn API](https://github.com/microsoft/webauthn/blob/master/webauthn.h)
(exposed through the `webauthn.dll` system library). This API is mainly used by browsers
(see the source code of [Chromium](https://chromium.googlesource.com/chromium/src/+/refs/heads/master/device/fido/win/webauthn_api.cc)
and [Firefox](https://searchfox.org/mozilla-central/source/dom/webauthn/WinWebAuthnManager.cpp)) to implement passwordless web authentication,
but it can also be used by any .NET desktop or CLI application.

As a front-end, this library uses classes defined in the [Fido2.Models](https://www.nuget.org/packages/Fido2.Models/) package, which it then translates to native C structures.
See the [project site](https://github.com/abergs/fido2-net-lib) for more details.

## Downloads

The `DSInternals.Win32.WebAuthn` library is published in the [NuGet Gallery](https://www.nuget.org/profiles/DSInternals).

## Usage

### Overview

The WebAuthn API is only supported on Windows 10 1903 and newer. It is exposed in the [DSInternals.Win32.WebAuthn](../Documentation/API/DSInternals.Win32.WebAuthn.md) namespace, with the [WebAuthnApi](../Documentation/API/DSInternals.Win32.WebAuthn/WebAuthnApi.md) class being the main entry point.

Following are code samples that mimic the behavior of [login.microsoftonline.com](https://login.microsoftonline.com). 
The samples are not ready for production use, as they are missing validation and contain many hardcoded values. Especially the `challenge` must be randomly generated in a cryptographically safe way.

### Registration (Attestation)

Credential registration is performed by calling the [AuthenticatorMakeCredential](../Documentation/API/DSInternals.Win32.WebAuthn/WebAuthnApi/AuthenticatorMakeCredential.md) or [AuthenticatorMakeCredentialAsync](../Documentation/API/DSInternals.Win32.WebAuthn/WebAuthnApi/AuthenticatorMakeCredentialAsync.md) method:

```cs
var config = new Fido2Configuration()
{
    Origin = "login.microsoft.com",
    ServerDomain = "login.microsoft.com",
    ServerName = "Microsoft"
};

var user = new Fido2User
{
    Name = "john.doe@outlook.com",
    DisplayName = "John Doe",
    Id = Base64Url.Decode("TUY65dH-Otl4jMdTRvlFQ1aApACYsuqGKSPQDQc1Bd4WVyw")
};

var authenticator = new AuthenticatorSelection
{
    AuthenticatorAttachment = Fido2NetLib.Objects.AuthenticatorAttachment.CrossPlatform,
    RequireResidentKey = true,
    UserVerification = Fido2NetLib.Objects.UserVerificationRequirement.Required,
};

byte[] challenge = Encoding.ASCII.GetBytes("CbWTU93Ppbgok1glyka*K9sZSWkqpK3qS1ldeLJxsI4k3jMLIi3dl8VDx10siTGd8U5SNj8yyMIbqXQH!apXGnrhWmYlg2GNdEGddIkO03cql!kKVgKi*MqEIl9aPqmJdYuRMjrEYlIyzi4*wP0YSyA$");

var excludedCredentials = new List<PublicKeyCredentialDescriptor>()
{
    new PublicKeyCredentialDescriptor(Base64Url.Decode("lz6_hw1jzaRNhhu9dt_M1Q=")),
    new PublicKeyCredentialDescriptor(Base64Url.Decode("Zod6YhgNV2dQeT3v8ekjRpU0nVlEkPlpXF5Vx6f4P9g=")),
    new PublicKeyCredentialDescriptor(Base64Url.Decode("sx2P4XkPO6TUoSf0pMEm3zi5gdwVrIRjiYvuTFRAkNMe_jVsntSgkyG5aV8er5GCA_G1X2idph-8lhhMFX3aaAyBCQIAAA="))
};

var options = CredentialCreateOptions.Create(
    config,
    challenge,
    user,
    authenticator,
    Fido2NetLib.Objects.AttestationConveyancePreference.Direct,
    excludedCredentials,
    null
);

options.PubKeyCredParams = new List<PubKeyCredParam>()
{
    new PubKeyCredParam() { Alg = -7, Type = PublicKeyCredentialType.PublicKey },
    new PubKeyCredParam() { Alg = -257, Type = PublicKeyCredentialType.PublicKey }
};

var webauthn = new WebAuthnApi();
var response = webauthn.AuthenticatorMakeCredential(options);
```

### Authentication (Assertion)

Authentication using a previously registered credential is performed by calling the [AuthenticatorGetAssertion](../Documentation/API/DSInternals.Win32.WebAuthn/WebAuthnApi/AuthenticatorGetAssertion.md) or [AuthenticatorGetAssertionAsync](../Documentation/API/DSInternals.Win32.WebAuthn/WebAuthnApi/AuthenticatorGetAssertionAsync.md) method:


```cs
var config = new Fido2Configuration()
{
    Origin = "login.microsoft.com",
    ServerDomain = "login.microsoft.com",
    ServerName = "Microsoft"
};

byte[] challenge = Encoding.ASCII.GetBytes("O.eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImtnMkxZczJUMENUaklmajRydDZKSXluZW4zOCJ9.eyJhdWQiOiJ1cm46bWljcm9zb2Z0OmZpZG86Y2hhbGxlbmdlIiwiaXNzIjoiaHR0cHM6Ly9sb2dpbi5taWNyb3NvZnQuY29tIiwiaWF0IjoxNjAyNDEzMzIxLCJuYmYiOjE2MDI0MTMzMjEsImV4cCI6MTYwMjQxMzYyMX0.ogwFJG0w6TX4QAGmwh-0aizApfdxtpQ_Kra9Bjk7LCuHxglV1rU1C5U9nPx4cKoNh09zucnTx9HpbvytgQtenOHiErZswGY_oi53EGL_ftfICm80agFcwMYLzHPH-yoIE9B5uORnLFOZmz98aZAFXcofZcK1E3-A1wZVcES9mjBN34G6iwG7-pcmBJge0Xc8nkRO-dei4RlFWnnYNc6iSPuJEknojGAvstmkfqSGnNgTpQXvn5eBVSHltY1C8jkk-qUzNxf1mfypcdXxfZxAIQnc50JRDz1QsD6bKV97crqhvv1ROpQ3L1V6dtYYiIQAUKQgm6FhYbS-3uj3ziK_fA");

var allowedCredentials = new List<PublicKeyCredentialDescriptor>()
{
    new PublicKeyCredentialDescriptor(Base64Url.Decode("sx2P4XkPO6TUoSf0pMEm3zi5gdwVrIRjiYvuTFRAkNMe_jVsntSgkyG5aV8er5GCA_G1X2idph-8lhhMFX3aaAyBCQIAAA")),
    new PublicKeyCredentialDescriptor(Base64Url.Decode("JmLEadmzHpm5K3i5gVFO-MJz43GukTKYkcRR8qO6Bp0")),
    new PublicKeyCredentialDescriptor(Base64Url.Decode("lz6_hw1jzaRNhhu9dt_M1Q"))
};

var options = AssertionOptions.Create(
    config,
    challenge,
    allowedCredentials,
    Fido2NetLib.Objects.UserVerificationRequirement.Required,
    null
);
           
var webauthn = new WebAuthnApi();
var response = webauthn.AuthenticatorGetAssertion(options);
```

See the [full API documentation](../Documentation/API/DSInternals.Win32.WebAuthn.md) for more information about using this library.

## Code Generation

The [APiConstants.cs](../Src/DSInternals.Win32.WebAuthn/APiConstants.cs) file is automatically generated from `#define` statements in [webauthn.h](../Src/webauthn/webauthn.h).
This is performed in the [DSInternals.Win32.WebAuthn.CodeGen](../Src/DSInternals.Win32.WebAuthn.CodeGen/Program.cs) helper application by leveraging the [CppAst.NET project](https://github.com/xoofx/CppAst.NET).

## Microsoft's Documentation
- [WebAuthn APIs for password-less authentication on Windows 10](https://docs.microsoft.com/en-us/windows/security/identity-protection/hello-for-business/webauthnapis)
- [C header file](https://github.com/microsoft/webauthn/blob/master/webauthn.h)

## Acknowledgements
- This project uses the [FIDO2 .NET Library](https://github.com/abergs/fido2-net-lib). Huge thanks to its contributors, mainly @abergs and @aseigler.
- There are at least 2 similar projects, in various stages of completeness:
  - [Yoq.Windows.WebAuthn](https://github.com/dbeinder/Yoq.Windows.WebAuthn)
  - [HelloSample](https://github.com/aseigler/HelloSample)
