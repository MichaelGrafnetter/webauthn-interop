# <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionOutputs"></a> Class AuthenticationExtensionsClientAssertionOutputs

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Client extension outputs from WebAuthn assertions.

```csharp
public sealed class AuthenticationExtensionsClientAssertionOutputs : AuthenticationExtensionsClientOutputs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AuthenticationExtensionsClientOutputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientOutputs.md) ← 
[AuthenticationExtensionsClientAssertionOutputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAssertionOutputs.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionOutputs_AppID"></a> AppID

Indicates whether the FIDO AppID extension was used for authentication.

```csharp
[JsonPropertyName("appid")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public bool? AppID { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionOutputs_CredentialBlob"></a> CredentialBlob

Credential blob returned during assertion.

```csharp
[JsonPropertyName("credBlob")]
[JsonConverter(typeof(Base64UrlConverter))]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public byte[]? CredentialBlob { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionOutputs_HmacGetSecret"></a> HmacGetSecret

HMAC secret output returned during assertion.

```csharp
[JsonPropertyName("hmacGetSecret")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public HMACGetSecretOutput? HmacGetSecret { get; set; }
```

#### Property Value

 [HMACGetSecretOutput](DSInternals.Win32.WebAuthn.HMACGetSecretOutput.md)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionOutputs_LargeBlob"></a> LargeBlob

Outputs for the WebAuthn largeBlob extension during assertion.

```csharp
[JsonPropertyName("largeBlob")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public LargeBlobAssertionOutputs? LargeBlob { get; set; }
```

#### Property Value

 [LargeBlobAssertionOutputs](DSInternals.Win32.WebAuthn.LargeBlobAssertionOutputs.md)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionOutputs_Prf"></a> Prf

Outputs for the WebAuthn prf extension during assertion.

```csharp
[JsonPropertyName("prf")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public PRFAssertionOutputs? Prf { get; set; }
```

#### Property Value

 [PRFAssertionOutputs](DSInternals.Win32.WebAuthn.PRFAssertionOutputs.md)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionOutputs_RemoteClientDataJson"></a> RemoteClientDataJson

Indicates whether the remoteClientDataJSON extension was used.

```csharp
[JsonPropertyName("remoteClientDataJson")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public bool? RemoteClientDataJson { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionOutputs_Uvm"></a> Uvm

User verification method factors reported by the authenticator.

```csharp
[JsonPropertyName("uvm")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public IList<UvmEntry>? Uvm { get; set; }
```

#### Property Value

 [IList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist\-1)<[UvmEntry](DSInternals.Win32.WebAuthn.UvmEntry.md)\>?

## Methods

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionOutputs_FromJson_System_String_"></a> FromJson\(string\)

Deserializes a JSON string into assertion extension outputs.

```csharp
public static AuthenticationExtensionsClientAssertionOutputs? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of assertion extension outputs.

#### Returns

 [AuthenticationExtensionsClientAssertionOutputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAssertionOutputs.md)?

Assertion extension outputs if deserialization is successful; otherwise, null.

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionOutputs_ToString"></a> ToString\(\)

Serializes the assertion extension outputs to JSON.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of these assertion extension outputs.

