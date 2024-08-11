# <a id="DSInternals_Win32_WebAuthn_MicrosoftGraphWebauthnAttestationResponse"></a> Class MicrosoftGraphWebauthnAttestationResponse

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

```csharp
public class MicrosoftGraphWebauthnAttestationResponse
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[MicrosoftGraphWebauthnAttestationResponse](DSInternals.Win32.WebAuthn.MicrosoftGraphWebauthnAttestationResponse.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="DSInternals_Win32_WebAuthn_MicrosoftGraphWebauthnAttestationResponse__ctor_DSInternals_Win32_WebAuthn_PublicKeyCredential_System_String_"></a> MicrosoftGraphWebauthnAttestationResponse\(PublicKeyCredential, string\)

```csharp
public MicrosoftGraphWebauthnAttestationResponse(PublicKeyCredential publicKeyCredential, string displayName)
```

#### Parameters

`publicKeyCredential` [PublicKeyCredential](DSInternals.Win32.WebAuthn.PublicKeyCredential.md)

`displayName` [string](https://learn.microsoft.com/dotnet/api/system.string)

## Properties

### <a id="DSInternals_Win32_WebAuthn_MicrosoftGraphWebauthnAttestationResponse_DisplayName"></a> DisplayName

The display name of the key as given by the user.

```csharp
[JsonPropertyName("displayName")]
public string DisplayName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_MicrosoftGraphWebauthnAttestationResponse_PublicKeyCredential"></a> PublicKeyCredential

Contains the WebAuthn public key credential information being registered.

```csharp
[JsonPropertyName("publicKeyCredential")]
public PublicKeyCredential PublicKeyCredential { get; set; }
```

#### Property Value

 [PublicKeyCredential](DSInternals.Win32.WebAuthn.PublicKeyCredential.md)

## Methods

### <a id="DSInternals_Win32_WebAuthn_MicrosoftGraphWebauthnAttestationResponse_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

