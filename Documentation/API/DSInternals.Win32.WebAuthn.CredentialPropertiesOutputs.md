# <a id="DSInternals_Win32_WebAuthn_CredentialPropertiesOutputs"></a> Class CredentialPropertiesOutputs

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Outputs for the WebAuthn credProps client extension.

```csharp
public sealed class CredentialPropertiesOutputs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CredentialPropertiesOutputs](DSInternals.Win32.WebAuthn.CredentialPropertiesOutputs.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_CredentialPropertiesOutputs_ResidentKey"></a> ResidentKey

Indicates whether the created credential is a discoverable credential.

```csharp
[JsonPropertyName("rk")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public bool? ResidentKey { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

## Methods

### <a id="DSInternals_Win32_WebAuthn_CredentialPropertiesOutputs_FromJson_System_String_"></a> FromJson\(string\)

Deserializes a JSON string into credential properties extension outputs.

```csharp
public static CredentialPropertiesOutputs? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of credential properties extension outputs.

#### Returns

 [CredentialPropertiesOutputs](DSInternals.Win32.WebAuthn.CredentialPropertiesOutputs.md)?

Credential properties extension outputs if deserialization is successful; otherwise, null.

### <a id="DSInternals_Win32_WebAuthn_CredentialPropertiesOutputs_ToString"></a> ToString\(\)

Serializes the credential properties extension outputs to JSON.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of these credential properties extension outputs.

