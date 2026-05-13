# <a id="DSInternals_Win32_WebAuthn_PRFValues"></a> Class PRFValues

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

One or two WebAuthn PRF inputs or outputs.

```csharp
public sealed class PRFValues
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PRFValues](DSInternals.Win32.WebAuthn.PRFValues.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_PRFValues_First"></a> First

First PRF input or output value.

```csharp
[JsonPropertyName("first")]
[JsonConverter(typeof(Base64UrlConverter))]
[JsonRequired]
public required byte[] First { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_PRFValues_Second"></a> Second

Optional second PRF input or output value.

```csharp
[JsonPropertyName("second")]
[JsonConverter(typeof(Base64UrlConverter))]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public byte[]? Second { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

## Methods

### <a id="DSInternals_Win32_WebAuthn_PRFValues_FromJson_System_String_"></a> FromJson\(string\)

Deserializes a JSON string into PRF values.

```csharp
public static PRFValues? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of PRF values.

#### Returns

 [PRFValues](DSInternals.Win32.WebAuthn.PRFValues.md)?

PRF values if deserialization is successful; otherwise, null.

### <a id="DSInternals_Win32_WebAuthn_PRFValues_ToString"></a> ToString\(\)

Serializes the PRF values to JSON.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of these PRF values.

