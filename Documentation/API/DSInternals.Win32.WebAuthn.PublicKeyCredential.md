# <a id="DSInternals_Win32_WebAuthn_PublicKeyCredential"></a> Class PublicKeyCredential

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

```csharp
public class PublicKeyCredential
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PublicKeyCredential](DSInternals.Win32.WebAuthn.PublicKeyCredential.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredential_AuthenticatorResponse"></a> AuthenticatorResponse

```csharp
[JsonPropertyName("response")]
public AuthenticatorAttestationResponse AuthenticatorResponse { get; set; }
```

#### Property Value

 [AuthenticatorAttestationResponse](DSInternals.Win32.WebAuthn.AuthenticatorAttestationResponse.md)

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredential_ClientExtensionResults"></a> ClientExtensionResults

```csharp
[JsonPropertyName("clientExtensionResults")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public AuthenticationExtensionsClientOutputs ClientExtensionResults { get; set; }
```

#### Property Value

 [AuthenticationExtensionsClientOutputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientOutputs.md)

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredential_Id"></a> Id

```csharp
[JsonPropertyName("id")]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[] Id { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

