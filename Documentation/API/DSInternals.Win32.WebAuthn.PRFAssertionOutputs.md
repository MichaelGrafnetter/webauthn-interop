# <a id="DSInternals_Win32_WebAuthn_PRFAssertionOutputs"></a> Class PRFAssertionOutputs

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

prf outputs from WebAuthn assertions.

```csharp
public sealed class PRFAssertionOutputs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PRFAssertionOutputs](DSInternals.Win32.WebAuthn.PRFAssertionOutputs.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_PRFAssertionOutputs_Results"></a> Results

PRF evaluation results.

```csharp
[JsonPropertyName("results")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public PRFValues? Results { get; set; }
```

#### Property Value

 [PRFValues](DSInternals.Win32.WebAuthn.PRFValues.md)?

## Methods

### <a id="DSInternals_Win32_WebAuthn_PRFAssertionOutputs_FromJson_System_String_"></a> FromJson\(string\)

Deserializes a JSON string into prf assertion outputs.

```csharp
public static PRFAssertionOutputs? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of prf assertion outputs.

#### Returns

 [PRFAssertionOutputs](DSInternals.Win32.WebAuthn.PRFAssertionOutputs.md)?

prf assertion outputs if deserialization is successful; otherwise, null.

### <a id="DSInternals_Win32_WebAuthn_PRFAssertionOutputs_ToString"></a> ToString\(\)

Serializes the prf assertion outputs to JSON.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of these prf assertion outputs.

