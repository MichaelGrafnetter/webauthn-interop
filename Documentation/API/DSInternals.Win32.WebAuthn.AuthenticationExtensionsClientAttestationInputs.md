# <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationInputs"></a> Class AuthenticationExtensionsClientAttestationInputs

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Client extension inputs for WebAuthn credential creation.

```csharp
public sealed class AuthenticationExtensionsClientAttestationInputs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AuthenticationExtensionsClientAttestationInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAttestationInputs.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationInputs_AppIDExclude"></a> AppIDExclude

Legacy FIDO AppID to exclude during credential creation.

```csharp
[JsonPropertyName("appidExclude")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public string? AppIDExclude { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationInputs_CredProtect"></a> CredProtect

Requested credential protection policy.

```csharp
[JsonPropertyName("credentialProtectionPolicy")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public UserVerification CredProtect { get; set; }
```

#### Property Value

 [UserVerification](DSInternals.Win32.WebAuthn.UserVerification.md)

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationInputs_CredentialBlob"></a> CredentialBlob

Optional credential blob to store with the created credential.

```csharp
[JsonPropertyName("credBlob")]
[JsonConverter(typeof(Base64UrlConverter))]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public byte[]? CredentialBlob { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationInputs_CredentialProperties"></a> CredentialProperties

Requests credential property outputs for the created credential.

```csharp
[JsonPropertyName("credProps")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public bool CredentialProperties { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationInputs_EnforceCredProtect"></a> EnforceCredProtect

Requests failing credential creation when the requested protection policy cannot be honored.

```csharp
[JsonPropertyName("enforceCredentialProtectionPolicy")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public bool EnforceCredProtect { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationInputs_HmacCreateSecret"></a> HmacCreateSecret

Requests creation of an authenticator-scoped HMAC secret.

```csharp
[JsonPropertyName("hmacCreateSecret")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public bool HmacCreateSecret { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationInputs_LargeBlob"></a> LargeBlob

Inputs for the WebAuthn largeBlob extension during credential creation.

```csharp
[JsonPropertyName("largeBlob")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public LargeBlobAttestationInputs? LargeBlob { get; set; }
```

#### Property Value

 [LargeBlobAttestationInputs](DSInternals.Win32.WebAuthn.LargeBlobAttestationInputs.md)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationInputs_MinimumPinLength"></a> MinimumPinLength

Requests the authenticator's current minimum PIN length.

```csharp
[JsonPropertyName("minPinLength")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public bool MinimumPinLength { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationInputs_Payment"></a> Payment

Inputs for the Secure Payment Confirmation (SPC) payment extension during credential creation.

```csharp
[JsonPropertyName("payment")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public PaymentAttestationInputs? Payment { get; set; }
```

#### Property Value

 [PaymentAttestationInputs](DSInternals.Win32.WebAuthn.PaymentAttestationInputs.md)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationInputs_Prf"></a> Prf

Inputs for the WebAuthn prf extension during credential creation.

```csharp
[JsonPropertyName("prf")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public PRFAttestationInputs? Prf { get; set; }
```

#### Property Value

 [PRFAttestationInputs](DSInternals.Win32.WebAuthn.PRFAttestationInputs.md)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationInputs_RemoteClientDataJson"></a> RemoteClientDataJson

Remote clientDataJSON supplied by a remote desktop client.

```csharp
[JsonPropertyName("remoteClientDataJSON")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public string? RemoteClientDataJson { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationInputs_RemoteDesktopClientOverride"></a> RemoteDesktopClientOverride

Inputs for Chromium's remoteDesktopClientOverride extension.

```csharp
[JsonPropertyName("remoteDesktopClientOverride")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public RemoteDesktopClientOverride? RemoteDesktopClientOverride { get; set; }
```

#### Property Value

 [RemoteDesktopClientOverride](DSInternals.Win32.WebAuthn.RemoteDesktopClientOverride.md)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationInputs_Uvm"></a> Uvm

Requests the user verification method extension output.

```csharp
[JsonPropertyName("uvm")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public bool Uvm { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationInputs_FromJson_System_String_"></a> FromJson\(string\)

Deserializes a JSON string into attestation extension inputs.

```csharp
public static AuthenticationExtensionsClientAttestationInputs? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of attestation extension inputs.

#### Returns

 [AuthenticationExtensionsClientAttestationInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAttestationInputs.md)?

Attestation extension inputs if deserialization is successful; otherwise, null.

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationInputs_ToString"></a> ToString\(\)

Serializes the attestation extension inputs to JSON.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of these attestation extension inputs.

