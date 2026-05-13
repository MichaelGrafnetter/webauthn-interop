# <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationOutputs"></a> Class AuthenticationExtensionsClientAttestationOutputs

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Client extension outputs from WebAuthn credential creation.

```csharp
public sealed class AuthenticationExtensionsClientAttestationOutputs : AuthenticationExtensionsClientOutputs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AuthenticationExtensionsClientOutputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientOutputs.md) ← 
[AuthenticationExtensionsClientAttestationOutputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAttestationOutputs.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationOutputs_AppIDExclude"></a> AppIDExclude

Indicates whether the FIDO AppID exclusion extension was used.

```csharp
[JsonPropertyName("appidExclude")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public bool? AppIDExclude { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationOutputs_CredProtect"></a> CredProtect

Credential protection policy applied by the authenticator.

```csharp
[JsonPropertyName("credentialProtectionPolicy")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public UserVerification CredProtect { get; set; }
```

#### Property Value

 [UserVerification](DSInternals.Win32.WebAuthn.UserVerification.md)

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationOutputs_CredentialBlobCreated"></a> CredentialBlobCreated

Indicates whether a credential blob was successfully created.

```csharp
[JsonPropertyName("credBlob")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public bool? CredentialBlobCreated { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationOutputs_CredentialProperties"></a> CredentialProperties

Credential properties returned for the created credential.

```csharp
[JsonPropertyName("credProps")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public CredentialPropertiesOutputs? CredentialProperties { get; set; }
```

#### Property Value

 [CredentialPropertiesOutputs](DSInternals.Win32.WebAuthn.CredentialPropertiesOutputs.md)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationOutputs_HmacSecret"></a> HmacSecret

Indicates whether the authenticator created an HMAC secret.

```csharp
[JsonPropertyName("hmacCreateSecret")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public bool HmacSecret { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationOutputs_LargeBlob"></a> LargeBlob

Outputs for the WebAuthn largeBlob extension during credential creation.

```csharp
[JsonPropertyName("largeBlob")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public LargeBlobAttestationOutputs? LargeBlob { get; set; }
```

#### Property Value

 [LargeBlobAttestationOutputs](DSInternals.Win32.WebAuthn.LargeBlobAttestationOutputs.md)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationOutputs_MinimumPinLength"></a> MinimumPinLength

Reported minimum PIN length enforced by the authenticator.

```csharp
[JsonPropertyName("minPinLength")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public uint? MinimumPinLength { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationOutputs_Prf"></a> Prf

Outputs for the WebAuthn prf extension during credential creation.

```csharp
[JsonPropertyName("prf")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public PRFAttestationOutputs? Prf { get; set; }
```

#### Property Value

 [PRFAttestationOutputs](DSInternals.Win32.WebAuthn.PRFAttestationOutputs.md)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationOutputs_RemoteClientDataJson"></a> RemoteClientDataJson

Indicates whether the remoteClientDataJSON extension was used.

```csharp
[JsonPropertyName("remoteClientDataJson")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public bool? RemoteClientDataJson { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationOutputs_Uvm"></a> Uvm

User verification method factors reported by the authenticator.

```csharp
[JsonPropertyName("uvm")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public IList<UvmEntry>? Uvm { get; set; }
```

#### Property Value

 [IList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist\-1)<[UvmEntry](DSInternals.Win32.WebAuthn.UvmEntry.md)\>?

## Methods

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationOutputs_FromJson_System_String_"></a> FromJson\(string\)

Deserializes a JSON string into attestation extension outputs.

```csharp
public static AuthenticationExtensionsClientAttestationOutputs? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of attestation extension outputs.

#### Returns

 [AuthenticationExtensionsClientAttestationOutputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAttestationOutputs.md)?

Attestation extension outputs if deserialization is successful; otherwise, null.

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationOutputs_ToString"></a> ToString\(\)

Serializes the attestation extension outputs to JSON.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of these attestation extension outputs.

