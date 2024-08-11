# <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor"></a> Class PublicKeyCredentialDescriptor

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

```csharp
public class PublicKeyCredentialDescriptor
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor__ctor_System_Byte___DSInternals_Win32_WebAuthn_AuthenticatorTransport_System_String_"></a> PublicKeyCredentialDescriptor\(byte\[\], AuthenticatorTransport, string\)

```csharp
[JsonConstructor]
public PublicKeyCredentialDescriptor(byte[] id, AuthenticatorTransport transports = AuthenticatorTransport.NoRestrictions, string type = "public-key")
```

#### Parameters

`id` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

`transports` [AuthenticatorTransport](DSInternals.Win32.WebAuthn.AuthenticatorTransport.md)

`type` [string](https://learn.microsoft.com/dotnet/api/system.string)

## Properties

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor_Id"></a> Id

This member contains the credential ID of the public key credential the caller is referring to.

```csharp
[JsonPropertyName("id")]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[] Id { get; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor_Transports"></a> Transports

This member contains a hint as to how the client might communicate with the managing authenticator of the public key credential the caller is referring to.

```csharp
[JsonPropertyName("transports")]
public AuthenticatorTransport Transports { get; }
```

#### Property Value

 [AuthenticatorTransport](DSInternals.Win32.WebAuthn.AuthenticatorTransport.md)

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor_Type"></a> Type

This member contains the type of the public key credential the caller is referring to.

```csharp
[JsonPropertyName("type")]
public string Type { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

