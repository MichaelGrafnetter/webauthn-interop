# <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientInputs"></a> Class AuthenticationExtensionsClientInputs

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Contains WebAuthn extensions that are actually supported by Windows 10.
These are currently defined in CTAP 2.1 Draft.

```csharp
public class AuthenticationExtensionsClientInputs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AuthenticationExtensionsClientInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientInputs.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientInputs_AppID"></a> AppID

This extension allows WebAuthn Relying Parties that have previously registered a credential using the legacy FIDO JavaScript APIs to request an assertion.

```csharp
[JsonPropertyName("appid")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public string AppID { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientInputs_CredProtect"></a> CredProtect

This extension indicates that the authenticator supports enhanced protection mode for the credentials created on the authenticator.
If present, verify that the credentialProtectionPolicy value is one of following values: userVerificationOptional, userVerificationOptionalWithCredentialIDList, userVerificationRequired

```csharp
[JsonPropertyName("credentialProtectionPolicy")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public UserVerification CredProtect { get; set; }
```

#### Property Value

 [UserVerification](DSInternals.Win32.WebAuthn.UserVerification.md)

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientInputs_CredentialBlob"></a> CredentialBlob

```csharp
[JsonPropertyName("credBlob")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public byte[] CredentialBlob { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientInputs_EnforceCredProtect"></a> EnforceCredProtect

Controls whether it is better to fail to create a credential rather than ignore the protection policy. When enforceCredentialProtectionPolicy is true, and credentialProtectionPolicy is either userVerificationOptionalWithCredentialIDList or userVerificationRequired, the platform SHOULD NOT create the credential in a way that does not implement the requested protection policy.

```csharp
[JsonPropertyName("enforceCredentialProtectionPolicy")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public bool EnforceCredProtect { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientInputs_GetCredentialBlob"></a> GetCredentialBlob

```csharp
[JsonPropertyName("getCredBlob")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public bool GetCredentialBlob { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientInputs_HmacCreateSecret"></a> HmacCreateSecret

This extension is used by the platform to retrieve a symmetric secret from the authenticator when it needs to encrypt or decrypt data using that symmetric secret. This symmetric secret is scoped to a credential. The authenticator and the platform each only have the part of the complete secret to prevent offline attacks. This extension can be used to maintain different secrets on different machines.

```csharp
[JsonPropertyName("hmacCreateSecret")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public bool HmacCreateSecret { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientInputs_HmacGetSecret"></a> HmacGetSecret

```csharp
[JsonPropertyName("hmacGetSecret")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public HMACGetSecretInput HmacGetSecret { get; set; }
```

#### Property Value

 [HMACGetSecretInput](DSInternals.Win32.WebAuthn.HMACGetSecretInput.md)

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientInputs_MinimumPinLength"></a> MinimumPinLength

This extension returns the current minimum PIN length value. This value does not decrease unless the authenticator is reset, in which case, all the credentials are reset. This extension is only applicable during credential creation.

```csharp
[JsonPropertyName("minPinLength")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public bool MinimumPinLength { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

