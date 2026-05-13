# <a id="DSInternals_Win32_WebAuthn_PRFAttestationInputs"></a> Class PRFAttestationInputs

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

prf inputs for WebAuthn credential creation.

```csharp
public sealed class PRFAttestationInputs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PRFAttestationInputs](DSInternals.Win32.WebAuthn.PRFAttestationInputs.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_PRFAttestationInputs_Eval"></a> Eval

PRF inputs to evaluate for the created credential.

```csharp
[JsonPropertyName("eval")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public PRFValues? Eval { get; set; }
```

#### Property Value

 [PRFValues](DSInternals.Win32.WebAuthn.PRFValues.md)?

## Methods

### <a id="DSInternals_Win32_WebAuthn_PRFAttestationInputs_FromJson_System_String_"></a> FromJson\(string\)

Deserializes a JSON string into prf attestation inputs.

```csharp
public static PRFAttestationInputs? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of prf attestation inputs.

#### Returns

 [PRFAttestationInputs](DSInternals.Win32.WebAuthn.PRFAttestationInputs.md)?

prf attestation inputs if deserialization is successful; otherwise, null.

### <a id="DSInternals_Win32_WebAuthn_PRFAttestationInputs_ToString"></a> ToString\(\)

Serializes the prf attestation inputs to JSON.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of these prf attestation inputs.

