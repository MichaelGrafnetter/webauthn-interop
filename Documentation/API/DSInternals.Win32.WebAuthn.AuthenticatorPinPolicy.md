# <a id="DSInternals_Win32_WebAuthn_AuthenticatorPinPolicy"></a> Class AuthenticatorPinPolicy

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Describes an authenticator's PIN requirements, user-verification settings, and configuration limits.

```csharp
public sealed class AuthenticatorPinPolicy
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AuthenticatorPinPolicy](DSInternals.Win32.WebAuthn.AuthenticatorPinPolicy.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

A null property value means that the corresponding setting or limit was not reported.

## Properties

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPinPolicy_AlwaysUvEnabled"></a> AlwaysUvEnabled

Whether user verification is always required.

```csharp
public bool? AlwaysUvEnabled { get; init; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPinPolicy_AuthenticatorConfigSupported"></a> AuthenticatorConfigSupported

Whether authenticator configuration support was reported.

```csharp
public bool? AuthenticatorConfigSupported { get; init; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPinPolicy_EnterpriseAttestationEnabled"></a> EnterpriseAttestationEnabled

Whether enterprise attestation is enabled.

```csharp
public bool? EnterpriseAttestationEnabled { get; init; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPinPolicy_ForcePinChangeRequired"></a> ForcePinChangeRequired

Whether the PIN must be changed before obtaining a PIN authentication token.

```csharp
public bool? ForcePinChangeRequired { get; init; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPinPolicy_MaximumLargeBlobBytes"></a> MaximumLargeBlobBytes

Maximum serialized large-blob bytes, including checksum.

```csharp
public uint? MaximumLargeBlobBytes { get; init; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPinPolicy_MaximumRpIds"></a> MaximumRpIds

Maximum number of additional RP IDs accepted when configuring minimum PIN length disclosure.

```csharp
public uint? MaximumRpIds { get; init; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)?

#### Remarks

Zero means that no additional RP IDs can be configured; preconfigured RP IDs are not included.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPinPolicy_MinimumPinLength"></a> MinimumPinLength

Minimum PIN length in Unicode code points.

```csharp
public uint? MinimumPinLength { get; init; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPinPolicy_SetMinPinLengthSupported"></a> SetMinPinLengthSupported

Whether minimum PIN length configuration support was reported.

```csharp
public bool? SetMinPinLengthSupported { get; init; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

## Methods

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorPinPolicy_ToString"></a> ToString\(\)

Returns a single-line summary of minimum PIN length, forced PIN change, and required user verification.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

Named policy values, with unreported settings shown as N/A.

