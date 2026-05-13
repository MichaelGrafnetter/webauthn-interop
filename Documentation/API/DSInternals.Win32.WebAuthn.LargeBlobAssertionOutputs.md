# <a id="DSInternals_Win32_WebAuthn_LargeBlobAssertionOutputs"></a> Class LargeBlobAssertionOutputs

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

largeBlob outputs from WebAuthn assertions.

```csharp
public sealed class LargeBlobAssertionOutputs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LargeBlobAssertionOutputs](DSInternals.Win32.WebAuthn.LargeBlobAssertionOutputs.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_LargeBlobAssertionOutputs_Blob"></a> Blob

Large blob bytes returned during assertion.

```csharp
[JsonPropertyName("blob")]
[JsonConverter(typeof(Base64UrlConverter))]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public byte[]? Blob { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_LargeBlobAssertionOutputs_Written"></a> Written

Indicates whether a large blob write operation succeeded.

```csharp
[JsonPropertyName("written")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public bool? Written { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

## Methods

### <a id="DSInternals_Win32_WebAuthn_LargeBlobAssertionOutputs_FromJson_System_String_"></a> FromJson\(string\)

Deserializes a JSON string into largeBlob assertion outputs.

```csharp
public static LargeBlobAssertionOutputs? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of largeBlob assertion outputs.

#### Returns

 [LargeBlobAssertionOutputs](DSInternals.Win32.WebAuthn.LargeBlobAssertionOutputs.md)?

largeBlob assertion outputs if deserialization is successful; otherwise, null.

### <a id="DSInternals_Win32_WebAuthn_LargeBlobAssertionOutputs_ToString"></a> ToString\(\)

Serializes the largeBlob assertion outputs to JSON.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of these largeBlob assertion outputs.

