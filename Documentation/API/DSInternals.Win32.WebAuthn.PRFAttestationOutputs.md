# <a id="DSInternals_Win32_WebAuthn_PRFAttestationOutputs"></a> Class PRFAttestationOutputs

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

prf outputs from WebAuthn credential creation.

```csharp
public sealed class PRFAttestationOutputs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PRFAttestationOutputs](DSInternals.Win32.WebAuthn.PRFAttestationOutputs.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_PRFAttestationOutputs_Enabled"></a> Enabled

Indicates whether PRF is available for the created credential.

```csharp
[JsonPropertyName("enabled")]
[JsonRequired]
public required bool Enabled { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_PRFAttestationOutputs_Results"></a> Results

PRF evaluation results.

```csharp
[JsonPropertyName("results")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public PRFValues? Results { get; set; }
```

#### Property Value

 [PRFValues](DSInternals.Win32.WebAuthn.PRFValues.md)?

## Methods

### <a id="DSInternals_Win32_WebAuthn_PRFAttestationOutputs_FromJson_System_String_"></a> FromJson\(string\)

Deserializes a JSON string into prf attestation outputs.

```csharp
public static PRFAttestationOutputs? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of prf attestation outputs.

#### Returns

 [PRFAttestationOutputs](DSInternals.Win32.WebAuthn.PRFAttestationOutputs.md)?

prf attestation outputs if deserialization is successful; otherwise, null.

### <a id="DSInternals_Win32_WebAuthn_PRFAttestationOutputs_ToString"></a> ToString\(\)

Serializes the prf attestation outputs to JSON.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of these prf attestation outputs.

