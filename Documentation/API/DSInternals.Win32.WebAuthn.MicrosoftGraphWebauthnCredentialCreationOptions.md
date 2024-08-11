# <a id="DSInternals_Win32_WebAuthn_MicrosoftGraphWebauthnCredentialCreationOptions"></a> Class MicrosoftGraphWebauthnCredentialCreationOptions

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

```csharp
public class MicrosoftGraphWebauthnCredentialCreationOptions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[MicrosoftGraphWebauthnCredentialCreationOptions](DSInternals.Win32.WebAuthn.MicrosoftGraphWebauthnCredentialCreationOptions.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

OData Object: https://graph.microsoft.com/beta/$metadata#microsoft.graph.webauthnCredentialCreationOptions

## Properties

### <a id="DSInternals_Win32_WebAuthn_MicrosoftGraphWebauthnCredentialCreationOptions_ChallengeTimeout"></a> ChallengeTimeout

Defines when the challenge in the creation options is no longer valid.

```csharp
[JsonPropertyName("challengeTimeoutDateTime")]
public DateTime ChallengeTimeout { get; set; }
```

#### Property Value

 [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)

### <a id="DSInternals_Win32_WebAuthn_MicrosoftGraphWebauthnCredentialCreationOptions_PublicKeyOptions"></a> PublicKeyOptions

Defines public key options for the creation of a new WebAuthn public key credential.

```csharp
[JsonPropertyName("publicKey")]
public PublicKeyCredentialCreationOptions PublicKeyOptions { get; set; }
```

#### Property Value

 [PublicKeyCredentialCreationOptions](DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions.md)

## Methods

### <a id="DSInternals_Win32_WebAuthn_MicrosoftGraphWebauthnCredentialCreationOptions_Create_System_String_"></a> Create\(string\)

```csharp
public static MicrosoftGraphWebauthnCredentialCreationOptions Create(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 [MicrosoftGraphWebauthnCredentialCreationOptions](DSInternals.Win32.WebAuthn.MicrosoftGraphWebauthnCredentialCreationOptions.md)

