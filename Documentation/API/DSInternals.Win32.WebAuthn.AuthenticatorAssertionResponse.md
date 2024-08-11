# <a id="DSInternals_Win32_WebAuthn_AuthenticatorAssertionResponse"></a> Class AuthenticatorAssertionResponse

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

The AuthenticatorAssertionResponse interface represents an authenticator's response
to a client’s request for generation of a new authentication assertion given
the WebAuthn Relying Party's challenge and OPTIONAL list of credentials it is aware of.
This response contains a cryptographic signature proving possession of the credential private key,
and optionally evidence of user consent to a specific transaction.

```csharp
public class AuthenticatorAssertionResponse : AuthenticatorResponse
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AuthenticatorResponse](DSInternals.Win32.WebAuthn.AuthenticatorResponse.md) ← 
[AuthenticatorAssertionResponse](DSInternals.Win32.WebAuthn.AuthenticatorAssertionResponse.md)

#### Inherited Members

[AuthenticatorResponse.ClientDataJson](DSInternals.Win32.WebAuthn.AuthenticatorResponse.md\#DSInternals\_Win32\_WebAuthn\_AuthenticatorResponse\_ClientDataJson), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorAssertionResponse_AuthenticatorData"></a> AuthenticatorData

This attribute contains the authenticator data returned by the authenticator.

```csharp
[JsonConverter(typeof(Base64UrlConverter))]
public byte[] AuthenticatorData { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorAssertionResponse_Signature"></a> Signature

This attribute contains the raw signature returned from the authenticator.

```csharp
[JsonConverter(typeof(Base64UrlConverter))]
public byte[] Signature { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorAssertionResponse_UserHandle"></a> UserHandle

This attribute contains the user handle returned from the authenticator, or null if the authenticator did not return a user handle.

```csharp
[JsonPropertyName("userHandle")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[] UserHandle { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

