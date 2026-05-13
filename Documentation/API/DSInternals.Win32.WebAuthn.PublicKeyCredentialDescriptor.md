# <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor"></a> Class PublicKeyCredentialDescriptor

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Identifies an existing public key credential.

```csharp
public sealed class PublicKeyCredentialDescriptor
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor__ctor_System_Byte___DSInternals_Win32_WebAuthn_AuthenticatorTransport_System_String_"></a> PublicKeyCredentialDescriptor\(byte\[\], AuthenticatorTransport, string\)

Initializes a new credential descriptor.

```csharp
[JsonConstructor]
[SetsRequiredMembers]
public PublicKeyCredentialDescriptor(byte[] id, AuthenticatorTransport transports = AuthenticatorTransport.NoRestrictions, string type = "public-key")
```

#### Parameters

`id` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

Credential identifier.

`transports` [AuthenticatorTransport](DSInternals.Win32.WebAuthn.AuthenticatorTransport.md)

Supported authenticator transports for this credential.

`type` [string](https://learn.microsoft.com/dotnet/api/system.string)

Credential type. Defaults to <code>public-key</code>.

## Properties

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor_Id"></a> Id

This member contains the credential ID of the public key credential the caller is referring to.

```csharp
[JsonPropertyName("id")]
[JsonConverter(typeof(Base64UrlConverter))]
public required byte[] Id { get; init; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor_Transports"></a> Transports

This member contains a hint as to how the client might communicate with the managing authenticator of the public key credential the caller is referring to.

```csharp
[JsonPropertyName("transports")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public AuthenticatorTransport Transports { get; init; }
```

#### Property Value

 [AuthenticatorTransport](DSInternals.Win32.WebAuthn.AuthenticatorTransport.md)

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialDescriptor_Type"></a> Type

This member contains the type of the public key credential the caller is referring to.

```csharp
[JsonPropertyName("type")]
public required string Type { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

