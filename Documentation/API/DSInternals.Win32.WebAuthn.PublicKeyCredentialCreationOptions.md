# <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialCreationOptions"></a> Class PublicKeyCredentialCreationOptions

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents the options for a WebAuthn credential creation (navigator.credentials.create()).

```csharp
public sealed class PublicKeyCredentialCreationOptions
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PublicKeyCredentialCreationOptions](DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialCreationOptions_Attestation"></a> Attestation

Attestation preference for the relying party.

```csharp
[JsonPropertyName("attestation")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public AttestationConveyancePreference Attestation { get; init; }
```

#### Property Value

 [AttestationConveyancePreference](DSInternals.Win32.WebAuthn.AttestationConveyancePreference.md)

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialCreationOptions_AttestationFormats"></a> AttestationFormats

A list of attestation formats acceptable to the relying party.

```csharp
[JsonPropertyName("attestationFormats")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public IReadOnlyList<string>? AttestationFormats { get; init; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialCreationOptions_AuthenticatorSelection"></a> AuthenticatorSelection

Properties of WebAuthn authenticators allowed to be used for authentication.

```csharp
[JsonPropertyName("authenticatorSelection")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public AuthenticatorSelectionCriteria? AuthenticatorSelection { get; init; }
```

#### Property Value

 [AuthenticatorSelectionCriteria](DSInternals.Win32.WebAuthn.AuthenticatorSelectionCriteria.md)?

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialCreationOptions_Challenge"></a> Challenge

A challenge from the relying party's server.

```csharp
[JsonPropertyName("challenge")]
[JsonConverter(typeof(Base64UrlConverter))]
[JsonRequired]
public required byte[] Challenge { get; init; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialCreationOptions_ExcludeCredentials"></a> ExcludeCredentials

List of credentials blocked for creation.

```csharp
[JsonPropertyName("excludeCredentials")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public IReadOnlyList<PublicKeyCredentialDescriptor>? ExcludeCredentials { get; init; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)\>?

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialCreationOptions_Extensions"></a> Extensions

Additional processing required for the client and WebAuthn authenticator.

```csharp
[JsonPropertyName("extensions")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public AuthenticationExtensionsClientAttestationInputs? Extensions { get; init; }
```

#### Property Value

 [AuthenticationExtensionsClientAttestationInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAttestationInputs.md)?

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialCreationOptions_Hints"></a> Hints

UI hints for preferred authenticator types.

```csharp
[JsonPropertyName("hints")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public IReadOnlyList<string>? Hints { get; init; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>?

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialCreationOptions_PublicKeyCredentialParameters"></a> PublicKeyCredentialParameters

A set of preferred credential properties for the creation of a new public key credential.

```csharp
[JsonPropertyName("pubKeyCredParams")]
[JsonRequired]
public required IReadOnlyList<PublicKeyCredentialParameter> PublicKeyCredentialParameters { get; init; }
```

#### Property Value

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PublicKeyCredentialParameter](DSInternals.Win32.WebAuthn.PublicKeyCredentialParameter.md)\>

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialCreationOptions_RelyingParty"></a> RelyingParty

Relying party information.

```csharp
[JsonPropertyName("rp")]
[JsonRequired]
public required RelyingPartyInformation RelyingParty { get; init; }
```

#### Property Value

 [RelyingPartyInformation](DSInternals.Win32.WebAuthn.RelyingPartyInformation.md)

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialCreationOptions_TimeoutMilliseconds"></a> TimeoutMilliseconds

The time in milliseconds that the client is willing to wait for the credential creation operation to complete.

```csharp
[JsonPropertyName("timeout")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public uint? TimeoutMilliseconds { get; init; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)?

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialCreationOptions_User"></a> User

User account information.

```csharp
[JsonPropertyName("user")]
[JsonRequired]
public required UserInformation User { get; init; }
```

#### Property Value

 [UserInformation](DSInternals.Win32.WebAuthn.UserInformation.md)

## Methods

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialCreationOptions_FromJson_System_String_"></a> FromJson\(string\)

```csharp
public static PublicKeyCredentialCreationOptions? FromJson(string optionsJson)
```

#### Parameters

`optionsJson` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 [PublicKeyCredentialCreationOptions](DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions.md)?

### <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialCreationOptions_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

