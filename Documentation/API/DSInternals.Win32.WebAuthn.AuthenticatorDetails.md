# <a id="DSInternals_Win32_WebAuthn_AuthenticatorDetails"></a> Class AuthenticatorDetails

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Information about an authenticator.

```csharp
public sealed class AuthenticatorDetails
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AuthenticatorDetails](DSInternals.Win32.WebAuthn.AuthenticatorDetails.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Corresponds to WEBAUTHN_AUTHENTICATOR_DETAILS.

## Properties

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorDetails_AuthenticatorId"></a> AuthenticatorId

The authenticator Id.

```csharp
public byte[]? AuthenticatorId { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorDetails_AuthenticatorLogo"></a> AuthenticatorLogo

Authenticator logo (expected to be in SVG format).

```csharp
public string? AuthenticatorLogo { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorDetails_AuthenticatorName"></a> AuthenticatorName

The authenticator name.

```csharp
public string? AuthenticatorName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorDetails_Locked"></a> Locked

Is the authenticator currently locked?
When locked, this authenticator's credentials might not be present or updated in WebAuthNGetPlatformCredentialList.

```csharp
public bool Locked { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

