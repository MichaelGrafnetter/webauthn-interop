# <a id="DSInternals_Win32_WebAuthn_LargeBlobAttestationInputs"></a> Class LargeBlobAttestationInputs

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

largeBlob inputs for WebAuthn credential creation.

```csharp
public sealed class LargeBlobAttestationInputs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LargeBlobAttestationInputs](DSInternals.Win32.WebAuthn.LargeBlobAttestationInputs.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="DSInternals_Win32_WebAuthn_LargeBlobAttestationInputs__ctor"></a> LargeBlobAttestationInputs\(\)

```csharp
public LargeBlobAttestationInputs()
```

### <a id="DSInternals_Win32_WebAuthn_LargeBlobAttestationInputs__ctor_DSInternals_Win32_WebAuthn_LargeBlobSupport_"></a> LargeBlobAttestationInputs\(LargeBlobSupport\)

```csharp
public LargeBlobAttestationInputs(LargeBlobSupport support)
```

#### Parameters

`support` [LargeBlobSupport](DSInternals.Win32.WebAuthn.LargeBlobSupport.md)

## Properties

### <a id="DSInternals_Win32_WebAuthn_LargeBlobAttestationInputs_Support"></a> Support

Requested large-blob support for the credential.

```csharp
[JsonPropertyName("support")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public LargeBlobSupport Support { get; set; }
```

#### Property Value

 [LargeBlobSupport](DSInternals.Win32.WebAuthn.LargeBlobSupport.md)

## Methods

### <a id="DSInternals_Win32_WebAuthn_LargeBlobAttestationInputs_FromJson_System_String_"></a> FromJson\(string\)

Deserializes a JSON string into largeBlob attestation inputs.

```csharp
public static LargeBlobAttestationInputs? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of largeBlob attestation inputs.

#### Returns

 [LargeBlobAttestationInputs](DSInternals.Win32.WebAuthn.LargeBlobAttestationInputs.md)?

largeBlob attestation inputs if deserialization is successful; otherwise, null.

### <a id="DSInternals_Win32_WebAuthn_LargeBlobAttestationInputs_ToString"></a> ToString\(\)

Serializes the largeBlob attestation inputs to JSON.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of these largeBlob attestation inputs.

