# <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionInputs"></a> Class AuthenticationExtensionsClientAssertionInputs

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Client extension inputs for WebAuthn assertions.

```csharp
public sealed class AuthenticationExtensionsClientAssertionInputs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AuthenticationExtensionsClientAssertionInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAssertionInputs.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionInputs_AppID"></a> AppID

Legacy FIDO AppID to use during assertion.

```csharp
[JsonPropertyName("appid")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public string? AppID { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionInputs_GetCredentialBlob"></a> GetCredentialBlob

Requests returning an existing credential blob during assertion.

```csharp
[JsonPropertyName("getCredBlob")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public bool GetCredentialBlob { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionInputs_HmacGetSecret"></a> HmacGetSecret

Input parameters for the legacy hmac-secret extension during assertion.

```csharp
[JsonPropertyName("hmacGetSecret")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public HMACGetSecretInput? HmacGetSecret { get; set; }
```

#### Property Value

 [HMACGetSecretInput](DSInternals.Win32.WebAuthn.HMACGetSecretInput.md)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionInputs_LargeBlob"></a> LargeBlob

Inputs for the WebAuthn largeBlob extension during assertion.

```csharp
[JsonPropertyName("largeBlob")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public LargeBlobAssertionInputs? LargeBlob { get; set; }
```

#### Property Value

 [LargeBlobAssertionInputs](DSInternals.Win32.WebAuthn.LargeBlobAssertionInputs.md)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionInputs_Payment"></a> Payment

Inputs for the Secure Payment Confirmation (SPC) payment extension during assertion.

```csharp
[JsonPropertyName("payment")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public PaymentAssertionInputs? Payment { get; set; }
```

#### Property Value

 [PaymentAssertionInputs](DSInternals.Win32.WebAuthn.PaymentAssertionInputs.md)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionInputs_Prf"></a> Prf

Inputs for the WebAuthn prf extension during assertion.

```csharp
[JsonPropertyName("prf")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public PRFAssertionInputs? Prf { get; set; }
```

#### Property Value

 [PRFAssertionInputs](DSInternals.Win32.WebAuthn.PRFAssertionInputs.md)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionInputs_RemoteClientDataJson"></a> RemoteClientDataJson

Remote clientDataJSON supplied by a remote desktop client.

```csharp
[JsonPropertyName("remoteClientDataJSON")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public string? RemoteClientDataJson { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionInputs_RemoteDesktopClientOverride"></a> RemoteDesktopClientOverride

Inputs for Chromium's remoteDesktopClientOverride extension.

```csharp
[JsonPropertyName("remoteDesktopClientOverride")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public RemoteDesktopClientOverride? RemoteDesktopClientOverride { get; set; }
```

#### Property Value

 [RemoteDesktopClientOverride](DSInternals.Win32.WebAuthn.RemoteDesktopClientOverride.md)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionInputs_Uvm"></a> Uvm

Requests the user verification method extension output.

```csharp
[JsonPropertyName("uvm")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public bool Uvm { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionInputs_FromJson_System_String_"></a> FromJson\(string\)

Deserializes a JSON string into assertion extension inputs.

```csharp
public static AuthenticationExtensionsClientAssertionInputs? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of assertion extension inputs.

#### Returns

 [AuthenticationExtensionsClientAssertionInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAssertionInputs.md)?

Assertion extension inputs if deserialization is successful; otherwise, null.

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionInputs_ToString"></a> ToString\(\)

Serializes the assertion extension inputs to JSON.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of these assertion extension inputs.

