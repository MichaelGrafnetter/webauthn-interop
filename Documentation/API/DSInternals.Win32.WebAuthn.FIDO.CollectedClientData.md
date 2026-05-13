# <a id="DSInternals_Win32_WebAuthn_FIDO_CollectedClientData"></a> Class CollectedClientData

Namespace: [DSInternals.Win32.WebAuthn.FIDO](DSInternals.Win32.WebAuthn.FIDO.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

The client data collected during a WebAuthn ceremony, serialized as the clientDataJSON field.
Property declaration order is significant: the WebAuthn spec requires type → challenge → origin → crossOrigin.

```csharp
public sealed class CollectedClientData
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CollectedClientData](DSInternals.Win32.WebAuthn.FIDO.CollectedClientData.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_FIDO_CollectedClientData_Challenge"></a> Challenge

This member contains the base64url encoding of the challenge provided by the Relying Party.

```csharp
[JsonPropertyName("challenge")]
[JsonConverter(typeof(Base64UrlConverter))]
[JsonPropertyOrder(1)]
public required byte[] Challenge { get; init; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_FIDO_CollectedClientData_CrossOrigin"></a> CrossOrigin

This member contains the inverse of the sameOriginWithAncestors argument value that was passed into the internal method.

```csharp
[JsonPropertyName("crossOrigin")]
[JsonPropertyOrder(3)]
public bool CrossOrigin { get; init; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_FIDO_CollectedClientData_Origin"></a> Origin

This member contains the fully qualified origin of the requester, as provided to the authenticator by the client, in the syntax defined by RFC6454.

```csharp
[JsonPropertyName("origin")]
[JsonPropertyOrder(2)]
public required string Origin { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_FIDO_CollectedClientData_TopOrigin"></a> TopOrigin

Contains the fully qualified top-level origin. It is set only if crossOrigin is true.

```csharp
[JsonPropertyName("topOrigin")]
[JsonPropertyOrder(4)]
public string? TopOrigin { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_FIDO_CollectedClientData_Type"></a> Type

This member contains the string "webauthn.create" when creating new credentials, and "webauthn.get" when getting an assertion from an existing credential.

```csharp
[JsonPropertyName("type")]
[JsonPropertyOrder(0)]
public required string Type { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Remarks

The purpose of this member is to prevent certain types of signature confusion attacks (where an attacker substitutes one legitimate signature for another).

## Methods

### <a id="DSInternals_Win32_WebAuthn_FIDO_CollectedClientData_Create_System_String_System_Byte___System_String_System_String_System_String_DSInternals_Win32_WebAuthn_RemoteDesktopClientOverride_"></a> Create\(string, byte\[\], string?, string?, string?, RemoteDesktopClientOverride?\)

```csharp
public static CollectedClientData Create(string type, byte[] challenge, string? hostName = null, string? relyingPartyId = null, string? remoteClientDataJson = null, RemoteDesktopClientOverride? remoteDesktopClientOverride = null)
```

#### Parameters

`type` [string](https://learn.microsoft.com/dotnet/api/system.string)

`challenge` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

`hostName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

`relyingPartyId` [string](https://learn.microsoft.com/dotnet/api/system.string)?

`remoteClientDataJson` [string](https://learn.microsoft.com/dotnet/api/system.string)?

`remoteDesktopClientOverride` [RemoteDesktopClientOverride](DSInternals.Win32.WebAuthn.RemoteDesktopClientOverride.md)?

#### Returns

 [CollectedClientData](DSInternals.Win32.WebAuthn.FIDO.CollectedClientData.md)

### <a id="DSInternals_Win32_WebAuthn_FIDO_CollectedClientData_FromJson_System_String_"></a> FromJson\(string\)

Parses clientDataJSON into collected client data.

```csharp
public static CollectedClientData FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

The clientDataJSON value.

#### Returns

 [CollectedClientData](DSInternals.Win32.WebAuthn.FIDO.CollectedClientData.md)

The parsed collected client data.

### <a id="DSInternals_Win32_WebAuthn_FIDO_CollectedClientData_GetOriginFromRelyingPartyId_System_String_"></a> GetOriginFromRelyingPartyId\(string\)

Constructs the WebAuthn origin from a relying party ID.

```csharp
public static string GetOriginFromRelyingPartyId(string relyingPartyId)
```

#### Parameters

`relyingPartyId` [string](https://learn.microsoft.com/dotnet/api/system.string)

The relying party identifier (e.g., "login.microsoft.com").

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The origin URL (e.g., "https://login.microsoft.com").

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when relyingPartyId is null.

### <a id="DSInternals_Win32_WebAuthn_FIDO_CollectedClientData_ToByteArray"></a> ToByteArray\(\)

```csharp
public byte[] ToByteArray()
```

#### Returns

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

