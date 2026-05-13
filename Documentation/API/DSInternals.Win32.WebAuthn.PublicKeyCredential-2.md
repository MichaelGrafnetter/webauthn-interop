# <a id="DSInternals_Win32_WebAuthn_PublicKeyCredential_2"></a> Class PublicKeyCredential<TResponse, TClientExtensionResults\>

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents a WebAuthn public key credential.

```csharp
public abstract class PublicKeyCredential<TResponse, TClientExtensionResults> where TResponse : AuthenticatorResponse where TClientExtensionResults : AuthenticationExtensionsClientOutputs
```

#### Type Parameters

`TResponse` 

Concrete authenticator response type.

`TClientExtensionResults` 

Concrete client extension output type.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PublicKeyCredential<TResponse, TClientExtensionResults\>](DSInternals.Win32.WebAuthn.PublicKeyCredential\-2.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredential_2_AuthenticatorAttachment"></a> AuthenticatorAttachment

The authenticator attachment modality.

```csharp
[JsonPropertyName("authenticatorAttachment")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public AuthenticatorAttachment AuthenticatorAttachment { get; init; }
```

#### Property Value

 [AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredential_2_ClientExtensionResults"></a> ClientExtensionResults

Outputs of client extension processing.

```csharp
[JsonPropertyName("clientExtensionResults")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public TClientExtensionResults? ClientExtensionResults { get; init; }
```

#### Property Value

 TClientExtensionResults?

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredential_2_Id"></a> Id

The credential identifier (Base64Url encoded).

```csharp
[JsonPropertyName("id")]
[JsonRequired]
[JsonConverter(typeof(Base64UrlConverter))]
public required byte[] Id { get; init; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredential_2_RawId"></a> RawId

The raw credential identifier (Base64Url encoded).

```csharp
[JsonPropertyName("rawId")]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[]? RawId { get; init; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredential_2_Response"></a> Response

Authenticator response payload.

```csharp
[JsonPropertyName("response")]
[JsonRequired]
public required TResponse Response { get; init; }
```

#### Property Value

 TResponse

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredential_2_Type"></a> Type

The type of the credential (always "public-key").

```csharp
[JsonPropertyName("type")]
[JsonRequired]
public required string Type { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

