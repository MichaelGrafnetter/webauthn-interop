# <a id="DSInternals_Win32_WebAuthn_PRFAssertionInputs"></a> Class PRFAssertionInputs

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

prf inputs for WebAuthn assertions.

```csharp
public sealed class PRFAssertionInputs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PRFAssertionInputs](DSInternals.Win32.WebAuthn.PRFAssertionInputs.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_PRFAssertionInputs_Eval"></a> Eval

PRF inputs to evaluate for the selected credential.

```csharp
[JsonPropertyName("eval")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public PRFValues? Eval { get; set; }
```

#### Property Value

 [PRFValues](DSInternals.Win32.WebAuthn.PRFValues.md)?

### <a id="DSInternals_Win32_WebAuthn_PRFAssertionInputs_EvalByCredential"></a> EvalByCredential

PRF inputs keyed by base64url-encoded credential ID.

```csharp
[JsonPropertyName("evalByCredential")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public Dictionary<string, PRFValues>? EvalByCredential { get; set; }
```

#### Property Value

 [Dictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.dictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [PRFValues](DSInternals.Win32.WebAuthn.PRFValues.md)\>?

## Methods

### <a id="DSInternals_Win32_WebAuthn_PRFAssertionInputs_FromJson_System_String_"></a> FromJson\(string\)

Deserializes a JSON string into prf assertion inputs.

```csharp
public static PRFAssertionInputs? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of prf assertion inputs.

#### Returns

 [PRFAssertionInputs](DSInternals.Win32.WebAuthn.PRFAssertionInputs.md)?

prf assertion inputs if deserialization is successful; otherwise, null.

### <a id="DSInternals_Win32_WebAuthn_PRFAssertionInputs_ToString"></a> ToString\(\)

Serializes the prf assertion inputs to JSON.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of these prf assertion inputs.

