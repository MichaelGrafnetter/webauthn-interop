# <a id="DSInternals_Win32_WebAuthn_Base64UrlConverter"></a> Class Base64UrlConverter

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Custom converter for encoding/decoding byte[] using Base64Url instead of default Base64.

```csharp
public sealed class Base64UrlConverter : JsonConverter<byte[]>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[JsonConverter](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter) ← 
[JsonConverter<byte\[\]\>](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter\-1) ← 
[Base64UrlConverter](DSInternals.Win32.WebAuthn.Base64UrlConverter.md)

#### Inherited Members

[JsonConverter<byte\[\]\>.CanConvert\(Type\)](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter\-1.canconvert), 
[JsonConverter<byte\[\]\>.Read\(ref Utf8JsonReader, Type, JsonSerializerOptions\)](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter\-1.read), 
[JsonConverter<byte\[\]\>.ReadAsPropertyName\(ref Utf8JsonReader, Type, JsonSerializerOptions\)](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter\-1.readaspropertyname), 
[JsonConverter<byte\[\]\>.Write\(Utf8JsonWriter, byte\[\], JsonSerializerOptions\)](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter\-1.write), 
[JsonConverter<byte\[\]\>.WriteAsPropertyName\(Utf8JsonWriter, byte\[\], JsonSerializerOptions\)](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter\-1.writeaspropertyname), 
[JsonConverter<byte\[\]\>.HandleNull](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter\-1.handlenull), 
[JsonConverter<byte\[\]\>.Type](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter\-1.type), 
[JsonConverter.CanConvert\(Type\)](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter.canconvert), 
[JsonConverter.Type](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter.type), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="DSInternals_Win32_WebAuthn_Base64UrlConverter_FromBase64UrlString_System_String_"></a> FromBase64UrlString\(string\)

Converts a Base64Url encoded string to a byte array

```csharp
public static byte[] FromBase64UrlString(string input)
```

#### Parameters

`input` [string](https://learn.microsoft.com/dotnet/api/system.string)

The Base64Url encoded string

#### Returns

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

The byte array represented by the encoded string

### <a id="DSInternals_Win32_WebAuthn_Base64UrlConverter_FromBase64UrlString_System_ReadOnlySpan_System_Byte__"></a> FromBase64UrlString\(ReadOnlySpan<byte\>\)

Converts a Base64Url or Base64 encoded string to a byte array

```csharp
public static byte[] FromBase64UrlString(ReadOnlySpan<byte> input)
```

#### Parameters

`input` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan\-1)<[byte](https://learn.microsoft.com/dotnet/api/system.byte)\>

The Base64Url encoded string

#### Returns

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

The byte array represented by the encoded string

### <a id="DSInternals_Win32_WebAuthn_Base64UrlConverter_Read_System_Text_Json_Utf8JsonReader__System_Type_System_Text_Json_JsonSerializerOptions_"></a> Read\(ref Utf8JsonReader, Type, JsonSerializerOptions\)

Reads a Base64Url-encoded JSON string into a byte array.

```csharp
public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
```

#### Parameters

`reader` [Utf8JsonReader](https://learn.microsoft.com/dotnet/api/system.text.json.utf8jsonreader)

JSON reader positioned on the value.

`typeToConvert` [Type](https://learn.microsoft.com/dotnet/api/system.type)

Target CLR type.

`options` [JsonSerializerOptions](https://learn.microsoft.com/dotnet/api/system.text.json.jsonserializeroptions)

Serializer options.

#### Returns

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

Decoded binary value.

### <a id="DSInternals_Win32_WebAuthn_Base64UrlConverter_Write_System_Text_Json_Utf8JsonWriter_System_Byte___System_Text_Json_JsonSerializerOptions_"></a> Write\(Utf8JsonWriter, byte\[\], JsonSerializerOptions\)

Writes a byte array as a Base64Url-encoded JSON string.

```csharp
public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options)
```

#### Parameters

`writer` [Utf8JsonWriter](https://learn.microsoft.com/dotnet/api/system.text.json.utf8jsonwriter)

JSON writer.

`value` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

Binary value to encode.

`options` [JsonSerializerOptions](https://learn.microsoft.com/dotnet/api/system.text.json.jsonserializeroptions)

Serializer options.

