# <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialParameter"></a> Class PublicKeyCredentialParameter

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Describes an acceptable public key credential algorithm and type pair.

```csharp
public sealed class PublicKeyCredentialParameter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PublicKeyCredentialParameter](DSInternals.Win32.WebAuthn.PublicKeyCredentialParameter.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialParameter__ctor_DSInternals_Win32_WebAuthn_COSE_Algorithm_System_String_"></a> PublicKeyCredentialParameter\(Algorithm, string\)

Initializes a new credential parameter instance.

```csharp
[JsonConstructor]
public PublicKeyCredentialParameter(Algorithm algorithm, string type)
```

#### Parameters

`algorithm` [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)

COSE signature algorithm.

`type` [string](https://learn.microsoft.com/dotnet/api/system.string)

Credential type. Must be <code>public-key</code>.

## Properties

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialParameter_Algorithm"></a> Algorithm

Specifies the cryptographic signature algorithm used for the new credential.

```csharp
[JsonPropertyName("alg")]
public Algorithm Algorithm { get; }
```

#### Property Value

 [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialParameter_Type"></a> Type

Specifies the type of credential to be created. The only supported value is public-key.

```csharp
[JsonPropertyName("type")]
public string Type { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

