# <a id="DSInternals_Win32_WebAuthn_AuthenticatorSelection"></a> Class AuthenticatorSelection

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

```csharp
public class AuthenticatorSelection
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AuthenticatorSelection](DSInternals.Win32.WebAuthn.AuthenticatorSelection.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorSelection_AuthenticatorAttachment"></a> AuthenticatorAttachment

Preferred attachment modality.

```csharp
[JsonPropertyName("authenticatorAttachment")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public AuthenticatorAttachment AuthenticatorAttachment { get; set; }
```

#### Property Value

 [AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorSelection_RequireResidentKey"></a> RequireResidentKey

Preferred client-side credential discoverability.

```csharp
[JsonPropertyName("requireResidentKey")]
public bool RequireResidentKey { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorSelection_UserVerificationRequirement"></a> UserVerificationRequirement

Requirement to verify the user is present during credential provisioning.

```csharp
[JsonPropertyName("userVerification")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public UserVerificationRequirement UserVerificationRequirement { get; set; }
```

#### Property Value

 [UserVerificationRequirement](DSInternals.Win32.WebAuthn.UserVerificationRequirement.md)

