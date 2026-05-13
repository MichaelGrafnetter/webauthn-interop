# <a id="DSInternals_Win32_WebAuthn_AuthenticatorSelectionCriteria"></a> Class AuthenticatorSelectionCriteria

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Specifies the Relying Party's requirements regarding authenticator attributes.

```csharp
public class AuthenticatorSelectionCriteria
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AuthenticatorSelectionCriteria](DSInternals.Win32.WebAuthn.AuthenticatorSelectionCriteria.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Used by Relying Parties to communicate their requirements for authenticator attributes during credential creation.

## Properties

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorSelectionCriteria_AuthenticatorAttachment"></a> AuthenticatorAttachment

The preferred authenticator attachment modality.

```csharp
[JsonPropertyName("authenticatorAttachment")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public AuthenticatorAttachment AuthenticatorAttachment { get; set; }
```

#### Property Value

 [AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorSelectionCriteria_RequireResidentKey"></a> RequireResidentKey

This member is retained for backwards compatibility with WebAuthn Level 1.
Relying Parties SHOULD set it to true if, and only if, residentKey is set to required.

```csharp
[JsonPropertyName("requireResidentKey")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public bool RequireResidentKey { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorSelectionCriteria_ResidentKey"></a> ResidentKey

Specifies the extent to which the Relying Party desires to create a client-side discoverable credential.

```csharp
[JsonPropertyName("residentKey")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public ResidentKeyRequirement? ResidentKey { get; set; }
```

#### Property Value

 [ResidentKeyRequirement](DSInternals.Win32.WebAuthn.ResidentKeyRequirement.md)?

#### Remarks

This value is intentionally nullable so that it can default to Preferred when not specified.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorSelectionCriteria_UserVerificationRequirement"></a> UserVerificationRequirement

Requirement to verify the user is present during credential provisioning.

```csharp
[JsonPropertyName("userVerification")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public UserVerificationRequirement UserVerificationRequirement { get; set; }
```

#### Property Value

 [UserVerificationRequirement](DSInternals.Win32.WebAuthn.UserVerificationRequirement.md)

