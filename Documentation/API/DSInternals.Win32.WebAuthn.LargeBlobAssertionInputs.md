# <a id="DSInternals_Win32_WebAuthn_LargeBlobAssertionInputs"></a> Class LargeBlobAssertionInputs

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

largeBlob inputs for WebAuthn assertions.

```csharp
public sealed class LargeBlobAssertionInputs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[LargeBlobAssertionInputs](DSInternals.Win32.WebAuthn.LargeBlobAssertionInputs.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="DSInternals_Win32_WebAuthn_LargeBlobAssertionInputs__ctor_System_Boolean_System_Byte___"></a> LargeBlobAssertionInputs\(bool, byte\[\]?\)

```csharp
[JsonConstructor]
public LargeBlobAssertionInputs(bool read = false, byte[]? write = null)
```

#### Parameters

`read` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

`write` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

## Properties

### <a id="DSInternals_Win32_WebAuthn_LargeBlobAssertionInputs_Operation"></a> Operation

```csharp
[JsonIgnore]
public CredentialLargeBlobOperation Operation { get; }
```

#### Property Value

 [CredentialLargeBlobOperation](DSInternals.Win32.WebAuthn.CredentialLargeBlobOperation.md)

### <a id="DSInternals_Win32_WebAuthn_LargeBlobAssertionInputs_Read"></a> Read

Requests reading the large blob during assertion.

```csharp
[JsonPropertyName("read")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public bool Read { get; init; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_LargeBlobAssertionInputs_Write"></a> Write

Large blob contents to write during assertion.

```csharp
[JsonPropertyName("write")]
[JsonConverter(typeof(Base64UrlConverter))]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public byte[]? Write { get; init; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

## Methods

### <a id="DSInternals_Win32_WebAuthn_LargeBlobAssertionInputs_FromJson_System_String_"></a> FromJson\(string\)

Deserializes a JSON string into largeBlob assertion inputs.

```csharp
public static LargeBlobAssertionInputs? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of largeBlob assertion inputs.

#### Returns

 [LargeBlobAssertionInputs](DSInternals.Win32.WebAuthn.LargeBlobAssertionInputs.md)?

largeBlob assertion inputs if deserialization is successful; otherwise, null.

### <a id="DSInternals_Win32_WebAuthn_LargeBlobAssertionInputs_ToString"></a> ToString\(\)

Serializes the largeBlob assertion inputs to JSON.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of these largeBlob assertion inputs.

