# <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialRequestOptions"></a> Class PublicKeyCredentialRequestOptions

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents the options for a WebAuthn credential request (navigator.credentials.get()).

```csharp
public sealed class PublicKeyCredentialRequestOptions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PublicKeyCredentialRequestOptions](DSInternals.Win32.WebAuthn.PublicKeyCredentialRequestOptions.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialRequestOptions_AllowCredentials"></a> AllowCredentials

A list of credentials acceptable to the caller.

```csharp
[JsonPropertyName("allowCredentials")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public IReadOnlyList<PublicKeyCredentialDescriptor>? AllowCredentials { get; init; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)\>?

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialRequestOptions_Challenge"></a> Challenge

A challenge from the relying party's server.

```csharp
[JsonPropertyName("challenge")]
[JsonConverter(typeof(Base64UrlConverter))]
[JsonRequired]
public required byte[] Challenge { get; init; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialRequestOptions_Extensions"></a> Extensions

Additional client extension inputs.

```csharp
[JsonPropertyName("extensions")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public AuthenticationExtensionsClientAssertionInputs? Extensions { get; init; }
```

#### Property Value

 [AuthenticationExtensionsClientAssertionInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAssertionInputs.md)?

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialRequestOptions_Hints"></a> Hints

UI hints for preferred authenticator types.

```csharp
[JsonPropertyName("hints")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public IReadOnlyList<string>? Hints { get; init; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialRequestOptions_RpId"></a> RpId

The relying party identifier.

```csharp
[JsonPropertyName("rpId")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public string? RpId { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialRequestOptions_Timeout"></a> Timeout

The time, in milliseconds, that the caller is willing to wait for the call to complete.

```csharp
[JsonPropertyName("timeout")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public uint? Timeout { get; init; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)?

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialRequestOptions_UserVerification"></a> UserVerification

Describes the relying party's requirements regarding user verification.

```csharp
[JsonPropertyName("userVerification")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public UserVerificationRequirement UserVerification { get; init; }
```

#### Property Value

 [UserVerificationRequirement](DSInternals.Win32.WebAuthn.UserVerificationRequirement.md)

## Methods

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialRequestOptions_FromJson_System_String_"></a> FromJson\(string\)

```csharp
public static PublicKeyCredentialRequestOptions? FromJson(string optionsJson)
```

#### Parameters

`optionsJson` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 [PublicKeyCredentialRequestOptions](DSInternals.Win32.WebAuthn.PublicKeyCredentialRequestOptions.md)?

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialRequestOptions_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

