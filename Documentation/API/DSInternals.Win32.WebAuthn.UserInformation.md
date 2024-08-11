# <a id="DSInternals_Win32_WebAuthn_UserInformation"></a> Class UserInformation

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Information about a user rntity.

```csharp
public sealed class UserInformation
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[UserInformation](DSInternals.Win32.WebAuthn.UserInformation.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Corresponds to WEBAUTHN_USER_ENTITY_INFORMATION.

## Properties

### <a id="DSInternals_Win32_WebAuthn_UserInformation_DisplayName"></a> DisplayName

Contains the friendly name associated with the user account by the Relying Party, such as "John P. Smith".

```csharp
[JsonPropertyName("displayName")]
public string DisplayName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_UserInformation_Icon"></a> Icon

Optional URL that can be used to retrieve an image containing the user's current avatar, or a data URI that contains the image data.

```csharp
[JsonPropertyName("icon")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public string Icon { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_UserInformation_Id"></a> Id

Identifier for the user.

```csharp
[JsonPropertyName("id")]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[] Id { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_UserInformation_Name"></a> Name

Contains a detailed name for this account, such as "john.p.smith@example.com".

```csharp
[JsonPropertyName("name")]
public string Name { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

