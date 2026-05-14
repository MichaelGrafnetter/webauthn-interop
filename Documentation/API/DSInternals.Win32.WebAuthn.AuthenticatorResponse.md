# <a id="DSInternals_Win32_WebAuthn_AuthenticatorResponse"></a> Class AuthenticatorResponse

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Authenticators respond to Relying Party requests by returning an object derived from the AuthenticatorResponse class.

```csharp
public abstract class AuthenticatorResponse
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AuthenticatorResponse](DSInternals.Win32.WebAuthn.AuthenticatorResponse.md)

#### Derived

[AuthenticatorAssertionResponse](DSInternals.Win32.WebAuthn.AuthenticatorAssertionResponse.md), 
[AuthenticatorAttestationResponse](DSInternals.Win32.WebAuthn.AuthenticatorAttestationResponse.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorResponse_ClientData"></a> ClientData

The UTF-8 encoded JSON-serialized client data passed to the authenticator (Base64Url encoded in JSON).

```csharp
[JsonPropertyName("clientDataJSON")]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[]? ClientData { get; init; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorResponse_ClientDataJson"></a> ClientDataJson

The clientDataJSON value decoded as a UTF-8 string.

```csharp
[JsonIgnore]
public string? ClientDataJson { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

