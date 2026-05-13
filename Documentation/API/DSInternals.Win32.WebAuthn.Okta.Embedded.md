# <a id="DSInternals_Win32_WebAuthn_Okta_Embedded"></a> Class Embedded

Namespace: [DSInternals.Win32.WebAuthn.Okta](DSInternals.Win32.WebAuthn.Okta.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Wrapper object used by Okta to nest WebAuthn activation options.

```csharp
public class Embedded
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[Embedded](DSInternals.Win32.WebAuthn.Okta.Embedded.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="DSInternals_Win32_WebAuthn_Okta_Embedded__ctor_DSInternals_Win32_WebAuthn_PublicKeyCredentialCreationOptions_"></a> Embedded\(PublicKeyCredentialCreationOptions\)

Wrapper object used by Okta to nest WebAuthn activation options.

```csharp
[JsonConstructor]
public Embedded(PublicKeyCredentialCreationOptions publicKeyOptions)
```

#### Parameters

`publicKeyOptions` [PublicKeyCredentialCreationOptions](DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions.md)

## Properties

### <a id="DSInternals_Win32_WebAuthn_Okta_Embedded_PublicKeyOptions"></a> PublicKeyOptions

Defines public key options for the creation of a new WebAuthn public key credential.

```csharp
[JsonPropertyName("activation")]
public PublicKeyCredentialCreationOptions PublicKeyOptions { get; set; }
```

#### Property Value

 [PublicKeyCredentialCreationOptions](DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions.md)

