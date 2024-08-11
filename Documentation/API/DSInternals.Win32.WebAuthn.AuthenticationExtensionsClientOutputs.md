# <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientOutputs"></a> Class AuthenticationExtensionsClientOutputs

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Contains WebAuthn extensions that are actually supported by Windows 10.
These are currently defined in CTAP 2.1 Draft.

```csharp
public class AuthenticationExtensionsClientOutputs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AuthenticationExtensionsClientOutputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientOutputs.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientOutputs_CredProtect"></a> CredProtect

This extension indicates that the authenticator supports enhanced protection mode for the credentials created on the authenticator.
If present, verify that the credentialProtectionPolicy value is one of following values: userVerificationOptional, userVerificationOptionalWithCredentialIDList, userVerificationRequired

```csharp
[JsonPropertyName("credentialProtectionPolicy")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public UserVerification CredProtect { get; set; }
```

#### Property Value

 [UserVerification](DSInternals.Win32.WebAuthn.UserVerification.md)

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientOutputs_CredentialBlobCreated"></a> CredentialBlobCreated

```csharp
[JsonPropertyName("credBlob")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public bool CredentialBlobCreated { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientOutputs_HmacGetSecret"></a> HmacGetSecret

HMAC Secret Extension

```csharp
[JsonPropertyName("hmacGetSecret")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public HMACGetSecretOutput HmacGetSecret { get; set; }
```

#### Property Value

 [HMACGetSecretOutput](DSInternals.Win32.WebAuthn.HMACGetSecretOutput.md)

#### Remarks

This extension is only applicable during credential creation.

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientOutputs_HmacSecret"></a> HmacSecret

This extension is used by the platform to retrieve a symmetric secret from the authenticator when it needs to encrypt or decrypt data using that symmetric secret. This symmetric secret is scoped to a credential. The authenticator and the platform each only have the part of the complete secret to prevent offline attacks. This extension can be used to maintain different secrets on different machines.

```csharp
[JsonPropertyName("hmacCreateSecret")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public bool HmacSecret { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientOutputs_MinimumPinLength"></a> MinimumPinLength

```csharp
[JsonPropertyName("minPinLength")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public uint? MinimumPinLength { get; set; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)?

