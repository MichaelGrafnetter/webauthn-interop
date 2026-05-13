# <a id="DSInternals_Win32_WebAuthn_LargeBlobAttestationOutputs"></a> Class LargeBlobAttestationOutputs

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

largeBlob outputs from WebAuthn credential creation.

```csharp
public sealed class LargeBlobAttestationOutputs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LargeBlobAttestationOutputs](DSInternals.Win32.WebAuthn.LargeBlobAttestationOutputs.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_LargeBlobAttestationOutputs_Supported"></a> Supported

Indicates whether the created credential supports large blobs.

```csharp
[JsonPropertyName("supported")]
[JsonRequired]
public required bool Supported { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### <a id="DSInternals_Win32_WebAuthn_LargeBlobAttestationOutputs_FromJson_System_String_"></a> FromJson\(string\)

Deserializes a JSON string into largeBlob attestation outputs.

```csharp
public static LargeBlobAttestationOutputs? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of largeBlob attestation outputs.

#### Returns

 [LargeBlobAttestationOutputs](DSInternals.Win32.WebAuthn.LargeBlobAttestationOutputs.md)?

largeBlob attestation outputs if deserialization is successful; otherwise, null.

### <a id="DSInternals_Win32_WebAuthn_LargeBlobAttestationOutputs_ToString"></a> ToString\(\)

Serializes the largeBlob attestation outputs to JSON.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of these largeBlob attestation outputs.

