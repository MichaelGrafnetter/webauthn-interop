# <a id="DSInternals_Win32_WebAuthn_UvmEntry"></a> Class UvmEntry

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

A single user verification method factor reported by the authenticator in the WebAuthn uvm extension output.

```csharp
[JsonConverter(typeof(UvmEntryConverter))]
public sealed class UvmEntry
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[UvmEntry](DSInternals.Win32.WebAuthn.UvmEntry.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="DSInternals_Win32_WebAuthn_UvmEntry__ctor"></a> UvmEntry\(\)

Initializes a new empty <xref href="DSInternals.Win32.WebAuthn.UvmEntry" data-throw-if-not-resolved="false"></xref>.

```csharp
public UvmEntry()
```

### <a id="DSInternals_Win32_WebAuthn_UvmEntry__ctor_DSInternals_Win32_WebAuthn_FIDO_UserVerificationMethod_DSInternals_Win32_WebAuthn_FIDO_KeyProtectionType_DSInternals_Win32_WebAuthn_FIDO_MatcherProtectionType_"></a> UvmEntry\(UserVerificationMethod, KeyProtectionType, MatcherProtectionType\)

Initializes a new <xref href="DSInternals.Win32.WebAuthn.UvmEntry" data-throw-if-not-resolved="false"></xref> with the specified factor values.

```csharp
public UvmEntry(UserVerificationMethod userVerificationMethod, KeyProtectionType keyProtectionType, MatcherProtectionType matcherProtectionType)
```

#### Parameters

`userVerificationMethod` [UserVerificationMethod](DSInternals.Win32.WebAuthn.FIDO.UserVerificationMethod.md)

User verification method/factor.

`keyProtectionType` [KeyProtectionType](DSInternals.Win32.WebAuthn.FIDO.KeyProtectionType.md)

Key protection type.

`matcherProtectionType` [MatcherProtectionType](DSInternals.Win32.WebAuthn.FIDO.MatcherProtectionType.md)

Matcher protection type.

## Properties

### <a id="DSInternals_Win32_WebAuthn_UvmEntry_KeyProtectionType"></a> KeyProtectionType

The method used by the authenticator to protect the FIDO registration private key material.

```csharp
public KeyProtectionType KeyProtectionType { get; set; }
```

#### Property Value

 [KeyProtectionType](DSInternals.Win32.WebAuthn.FIDO.KeyProtectionType.md)

### <a id="DSInternals_Win32_WebAuthn_UvmEntry_MatcherProtectionType"></a> MatcherProtectionType

The method used by the authenticator to protect the matcher that performs user verification.

```csharp
public MatcherProtectionType MatcherProtectionType { get; set; }
```

#### Property Value

 [MatcherProtectionType](DSInternals.Win32.WebAuthn.FIDO.MatcherProtectionType.md)

### <a id="DSInternals_Win32_WebAuthn_UvmEntry_UserVerificationMethod"></a> UserVerificationMethod

The authentication method/factor used by the authenticator to verify the user.

```csharp
public UserVerificationMethod UserVerificationMethod { get; set; }
```

#### Property Value

 [UserVerificationMethod](DSInternals.Win32.WebAuthn.FIDO.UserVerificationMethod.md)

