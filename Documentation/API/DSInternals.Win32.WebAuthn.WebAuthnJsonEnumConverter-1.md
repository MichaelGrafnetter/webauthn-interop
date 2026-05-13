# <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonEnumConverter_1"></a> Class WebAuthnJsonEnumConverter<TEnum\>

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Converts WebAuthn enum values with a uint underlying type to and from their JSON string representations.

```csharp
public sealed class WebAuthnJsonEnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
```

#### Type Parameters

`TEnum` 

The enum type to convert. Must be an enum with uint as its underlying type.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[JsonConverter](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter) ← 
[JsonConverter<TEnum\>](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter\-1) ← 
[WebAuthnJsonEnumConverter<TEnum\>](DSInternals.Win32.WebAuthn.WebAuthnJsonEnumConverter\-1.md)

#### Inherited Members

[JsonConverter<TEnum\>.CanConvert\(Type\)](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter\-1.canconvert), 
[JsonConverter<TEnum\>.Read\(ref Utf8JsonReader, Type, JsonSerializerOptions\)](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter\-1.read), 
[JsonConverter<TEnum\>.ReadAsPropertyName\(ref Utf8JsonReader, Type, JsonSerializerOptions\)](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter\-1.readaspropertyname), 
[JsonConverter<TEnum\>.Write\(Utf8JsonWriter, TEnum, JsonSerializerOptions\)](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter\-1.write), 
[JsonConverter<TEnum\>.WriteAsPropertyName\(Utf8JsonWriter, TEnum, JsonSerializerOptions\)](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter\-1.writeaspropertyname), 
[JsonConverter<TEnum\>.HandleNull](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter\-1.handlenull), 
[JsonConverter<TEnum\>.Type](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter\-1.type), 
[JsonConverter.CanConvert\(Type\)](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter.canconvert), 
[JsonConverter.Type](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter.type), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Non-flags enums are serialized as a single string value. Enums decorated with <xref href="System.FlagsAttribute" data-throw-if-not-resolved="false"></xref> are
serialized as a JSON array containing the names of all set flags. Names are read from
<xref href="System.Text.Json.Serialization.JsonStringEnumMemberNameAttribute" data-throw-if-not-resolved="false"></xref>; enum fields without that attribute are treated as unmapped.
Unrecognized or unmapped JSON names deserialize to the default enum value, allowing for forward compatibility
when new WebAuthn values are added.

## Constructors

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonEnumConverter_1__ctor"></a> WebAuthnJsonEnumConverter\(\)

Initializes a new instance of the <xref href="DSInternals.Win32.WebAuthn.WebAuthnJsonEnumConverter%601" data-throw-if-not-resolved="false"></xref> class.

```csharp
public WebAuthnJsonEnumConverter()
```

#### Remarks

This converter is intended for WebAuthn enums that have a uint underlying type. Flags and non-flags enums are both
supported.

#### Exceptions

 [JsonException](https://learn.microsoft.com/dotnet/api/system.text.json.jsonexception)

Thrown if <code class="typeparamref">TEnum</code> does not use uint as its underlying type.

## Methods

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonEnumConverter_1_Read_System_Text_Json_Utf8JsonReader__System_Type_System_Text_Json_JsonSerializerOptions_"></a> Read\(ref Utf8JsonReader, Type, JsonSerializerOptions\)

Reads and converts the JSON representation of an enum value to its corresponding <code class="typeparamref">TEnum</code> value.

```csharp
public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
```

#### Parameters

`reader` [Utf8JsonReader](https://learn.microsoft.com/dotnet/api/system.text.json.utf8jsonreader)

The reader used to read the JSON data. The reader must be positioned at the value to convert.

`typeToConvert` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The type of the enum to convert to. This must be a valid enum type.

`options` [JsonSerializerOptions](https://learn.microsoft.com/dotnet/api/system.text.json.jsonserializeroptions)

Options to control the behavior of the deserialization process.

#### Returns

 TEnum

The enum value read from the JSON input. Null and unrecognized string values return the default
<code class="typeparamref">TEnum</code> value.

#### Exceptions

 [JsonException](https://learn.microsoft.com/dotnet/api/system.text.json.jsonexception)

Thrown when the JSON token shape does not match the enum type: non-flags enums require a string value, while flags
enums require an array of string values.

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonEnumConverter_1_Write_System_Text_Json_Utf8JsonWriter__0_System_Text_Json_JsonSerializerOptions_"></a> Write\(Utf8JsonWriter, TEnum, JsonSerializerOptions\)

Writes the specified enum value to JSON using its mapped WebAuthn string name.

```csharp
public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
```

#### Parameters

`writer` [Utf8JsonWriter](https://learn.microsoft.com/dotnet/api/system.text.json.utf8jsonwriter)

The <xref href="System.Text.Json.Utf8JsonWriter" data-throw-if-not-resolved="false"></xref> to which the JSON value will be written.

`value` TEnum

The enum value to serialize.

`options` [JsonSerializerOptions](https://learn.microsoft.com/dotnet/api/system.text.json.jsonserializeroptions)

The serialization options to use when writing the JSON value.

#### Remarks

Non-flags enums are written as a single string, or null when the value is unmapped. Flags enums are written as a
JSON array containing only mapped, non-zero flags that are set on the value; unmapped flags are skipped.

