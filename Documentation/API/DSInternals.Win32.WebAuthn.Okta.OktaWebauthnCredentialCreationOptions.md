# <a id="DSInternals_Win32_WebAuthn_Okta_OktaWebauthnCredentialCreationOptions"></a> Class OktaWebauthnCredentialCreationOptions

Namespace: [DSInternals.Win32.WebAuthn.Okta](DSInternals.Win32.WebAuthn.Okta.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Defines the options for creating a new WebAuthn credential in Okta's API.

```csharp
public class OktaWebauthnCredentialCreationOptions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[OktaWebauthnCredentialCreationOptions](DSInternals.Win32.WebAuthn.Okta.OktaWebauthnCredentialCreationOptions.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaWebauthnCredentialCreationOptions_Embedded"></a> Embedded

A wrapper around the PublicKeyCredentialCreationOptions

```csharp
[JsonPropertyName("_embedded")]
public required Embedded Embedded { get; set; }
```

#### Property Value

 [Embedded](DSInternals.Win32.WebAuthn.Okta.Embedded.md)

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaWebauthnCredentialCreationOptions_FactorId"></a> FactorId

The factor id of the Okta factor being registered.

```csharp
[JsonPropertyName("id")]
public required string FactorId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaWebauthnCredentialCreationOptions_PublicKeyOptions"></a> PublicKeyOptions

Gets or sets the nested WebAuthn public key credential creation options.

```csharp
[JsonIgnore]
public PublicKeyCredentialCreationOptions PublicKeyOptions { get; set; }
```

#### Property Value

 [PublicKeyCredentialCreationOptions](DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions.md)

## Methods

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaWebauthnCredentialCreationOptions_Create_System_String_"></a> Create\(string\)

Parses a JSON payload returned by Okta into WebAuthn credential creation options.

```csharp
public static OktaWebauthnCredentialCreationOptions Create(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

The Okta JSON payload.

#### Returns

 [OktaWebauthnCredentialCreationOptions](DSInternals.Win32.WebAuthn.Okta.OktaWebauthnCredentialCreationOptions.md)

The parsed credential creation options.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">json</code> is null or empty.

