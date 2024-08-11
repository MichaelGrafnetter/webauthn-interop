# <a id="DSInternals_Win32_WebAuthn_FIDO_CollectedClientData"></a> Class CollectedClientData

Namespace: [DSInternals.Win32.WebAuthn.FIDO](DSInternals.Win32.WebAuthn.FIDO.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

```csharp
public class CollectedClientData
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CollectedClientData](DSInternals.Win32.WebAuthn.FIDO.CollectedClientData.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_FIDO_CollectedClientData_Challenge"></a> Challenge

This member contains the base64url encoding of the challenge provided by the Relying Party.

```csharp
[JsonPropertyName("challenge")]
[JsonRequired]
[JsonConverter(typeof(Base64UrlConverter))]
[JsonPropertyOrder(1)]
public byte[] Challenge { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_FIDO_CollectedClientData_CrossOrigin"></a> CrossOrigin

This member contains the inverse of the sameOriginWithAncestors argument value that was passed into the internal method.

```csharp
[JsonPropertyName("crossOrigin")]
[JsonPropertyOrder(3)]
public bool CrossOrigin { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_FIDO_CollectedClientData_Origin"></a> Origin

This member contains the fully qualified origin of the requester, as provided to the authenticator by the client, in the syntax defined by RFC6454.

```csharp
[JsonPropertyName("origin")]
[JsonRequired]
[JsonPropertyOrder(2)]
public string Origin { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_FIDO_CollectedClientData_Type"></a> Type

This member contains the string "webauthn.create" when creating new credentials, and "webauthn.get" when getting an assertion from an existing credential.

```csharp
[JsonPropertyName("type")]
[JsonRequired]
[JsonPropertyOrder(0)]
public string Type { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

The purpose of this member is to prevent certain types of signature confusion attacks (where an attacker substitutes one legitimate signature for another).

