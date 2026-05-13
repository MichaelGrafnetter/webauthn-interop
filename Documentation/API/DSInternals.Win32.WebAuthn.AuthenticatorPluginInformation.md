# <a id="DSInternals_Win32_WebAuthn_AuthenticatorPluginInformation"></a> Class AuthenticatorPluginInformation

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents information about an authenticator plugin registered in Windows.

```csharp
public sealed class AuthenticatorPluginInformation
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AuthenticatorPluginInformation](DSInternals.Win32.WebAuthn.AuthenticatorPluginInformation.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Authenticator plugins are registered under HKLM\SOFTWARE\Microsoft\FIDO\{UserSID}\Plugins\{PluginGuid}.
This class corresponds to data stored by third-party passkey providers like 1Password, Bitwarden, etc.

## Properties

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPluginInformation_AaGuid"></a> AaGuid

The authenticator attestation GUID (AAGUID).

```csharp
public Guid AaGuid { get; set; }
```

#### Property Value

 [Guid](https://learn.microsoft.com/dotnet/api/system.guid)

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPluginInformation_AuthenticatorInfo"></a> AuthenticatorInfo

The CTAP CBOR encoded authenticator information.

```csharp
public byte[]? AuthenticatorInfo { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

#### Remarks

This is the authenticatorGetInfo response encoded in CBOR format.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPluginInformation_DarkLogo"></a> DarkLogo

The plugin authenticator logo for dark themes (SVG format).

```csharp
public string? DarkLogo { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPluginInformation_Enabled"></a> Enabled

Indicates whether the authenticator plugin is enabled.

```csharp
public bool Enabled { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPluginInformation_LightLogo"></a> LightLogo

The plugin authenticator logo for light themes (SVG format).

```csharp
public string? LightLogo { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPluginInformation_Name"></a> Name

The display name of the authenticator plugin.

```csharp
public string? Name { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPluginInformation_PackageFamilyName"></a> PackageFamilyName

The family name of the Windows Store package.

```csharp
public string? PackageFamilyName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPluginInformation_PackageFullName"></a> PackageFullName

The full name of the Windows Store package.

```csharp
public string? PackageFullName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPluginInformation_PackageSignatureKind"></a> PackageSignatureKind

The kind of package signature.

```csharp
public PackageSignatureKind PackageSignatureKind { get; set; }
```

#### Property Value

 [PackageSignatureKind](DSInternals.Win32.WebAuthn.PackageSignatureKind.md)

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPluginInformation_PluginClsid"></a> PluginClsid

The unique identifier (CLSID) of the plugin.

```csharp
public Guid PluginClsid { get; set; }
```

#### Property Value

 [Guid](https://learn.microsoft.com/dotnet/api/system.guid)

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPluginInformation_PublisherDisplayName"></a> PublisherDisplayName

The display name of the publisher.

```csharp
public string? PublisherDisplayName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPluginInformation_RpId"></a> RpId

The Relying Party ID associated with the plugin.

```csharp
public string? RpId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPluginInformation_SigningKeyAlgorithm"></a> SigningKeyAlgorithm

The signing key algorithm used by the package (e.g., "ECDSA_P384").

```csharp
public string? SigningKeyAlgorithm { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPluginInformation_StateToggled"></a> StateToggled

Indicates whether the state has been toggled.

```csharp
public bool StateToggled { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPluginInformation_UserName"></a> UserName

The user name resolved from the SID.

```csharp
public string? UserName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPluginInformation_UserSid"></a> UserSid

The user SID for which this plugin is registered.

```csharp
public string? UserSid { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPluginInformation_UvCount"></a> UvCount

The number of user verifications performed.

```csharp
public uint UvCount { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

