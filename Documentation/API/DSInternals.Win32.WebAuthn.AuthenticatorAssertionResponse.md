# <a id="DSInternals_Win32_WebAuthn_AuthenticatorAssertionResponse"></a> Class AuthenticatorAssertionResponse

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents an authenticator assertion response.

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
[JsonPropertyName("authenticatorData")]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[]? AuthenticatorData { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorAssertionResponse_AuthenticatorDataParsed"></a> AuthenticatorDataParsed

The parsed authenticator data structure.

```csharp
[JsonIgnore]
public AuthenticatorData? AuthenticatorDataParsed { get; }
```

#### Property Value

 [AuthenticatorData](DSInternals.Win32.WebAuthn.FIDO.AuthenticatorData.md)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorAssertionResponse_Signature"></a> Signature

The raw signature returned by the authenticator (Base64Url encoded).

```csharp
[JsonPropertyName("signature")]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[]? Signature { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorAssertionResponse_UserHandle"></a> UserHandle

The user handle returned by the authenticator (Base64Url encoded).

```csharp
[JsonPropertyName("userHandle")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[]? UserHandle { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

## Methods

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorAssertionResponse_FromJson_System_String_"></a> FromJson\(string\)

```csharp
public static AuthenticatorAssertionResponse? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 [AuthenticatorAssertionResponse](DSInternals.Win32.WebAuthn.AuthenticatorAssertionResponse.md)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorAssertionResponse_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

