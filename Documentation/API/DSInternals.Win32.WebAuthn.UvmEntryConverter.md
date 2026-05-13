# <a id="DSInternals_Win32_WebAuthn_UvmEntryConverter"></a> Class UvmEntryConverter

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Converts a <xref href="DSInternals.Win32.WebAuthn.UvmEntry" data-throw-if-not-resolved="false"></xref> to and from a 3-element JSON array of unsigned numbers, as defined by the WebAuthn uvm extension.

```csharp
public sealed class UvmEntryConverter : JsonConverter<UvmEntry>
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[JsonConverter](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter) ← 
[JsonConverter<UvmEntry\>](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter\-1) ← 
[UvmEntryConverter](DSInternals.Win32.WebAuthn.UvmEntryConverter.md)

#### Inherited Members

[JsonConverter<UvmEntry\>.CanConvert\(Type\)](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter\-1.canconvert), 
[JsonConverter<UvmEntry\>.Read\(ref Utf8JsonReader, Type, JsonSerializerOptions\)](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter\-1.read), 
[JsonConverter<UvmEntry\>.ReadAsPropertyName\(ref Utf8JsonReader, Type, JsonSerializerOptions\)](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter\-1.readaspropertyname), 
[JsonConverter<UvmEntry\>.Write\(Utf8JsonWriter, UvmEntry, JsonSerializerOptions\)](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter\-1.write), 
[JsonConverter<UvmEntry\>.WriteAsPropertyName\(Utf8JsonWriter, UvmEntry, JsonSerializerOptions\)](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter\-1.writeaspropertyname), 
[JsonConverter<UvmEntry\>.HandleNull](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter\-1.handlenull), 
[JsonConverter<UvmEntry\>.Type](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter\-1.type), 
[JsonConverter.CanConvert\(Type\)](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter.canconvert), 
[JsonConverter.Type](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter.type), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="DSInternals_Win32_WebAuthn_UvmEntryConverter_Read_System_Text_Json_Utf8JsonReader__System_Type_System_Text_Json_JsonSerializerOptions_"></a> Read\(ref Utf8JsonReader, Type, JsonSerializerOptions\)

Reads a 3-element JSON array of numbers and returns the corresponding <xref href="DSInternals.Win32.WebAuthn.UvmEntry" data-throw-if-not-resolved="false"></xref>.

```csharp
public override UvmEntry Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
```

#### Parameters

`reader` [Utf8JsonReader](https://learn.microsoft.com/dotnet/api/system.text.json.utf8jsonreader)

`typeToConvert` [Type](https://learn.microsoft.com/dotnet/api/system.type)

`options` [JsonSerializerOptions](https://learn.microsoft.com/dotnet/api/system.text.json.jsonserializeroptions)

#### Returns

 [UvmEntry](DSInternals.Win32.WebAuthn.UvmEntry.md)

### <a id="DSInternals_Win32_WebAuthn_UvmEntryConverter_Write_System_Text_Json_Utf8JsonWriter_DSInternals_Win32_WebAuthn_UvmEntry_System_Text_Json_JsonSerializerOptions_"></a> Write\(Utf8JsonWriter, UvmEntry, JsonSerializerOptions\)

Writes the <xref href="DSInternals.Win32.WebAuthn.UvmEntry" data-throw-if-not-resolved="false"></xref> as a 3-element JSON array of numbers.

```csharp
public override void Write(Utf8JsonWriter writer, UvmEntry value, JsonSerializerOptions options)
```

#### Parameters

`writer` [Utf8JsonWriter](https://learn.microsoft.com/dotnet/api/system.text.json.utf8jsonwriter)

`value` [UvmEntry](DSInternals.Win32.WebAuthn.UvmEntry.md)

`options` [JsonSerializerOptions](https://learn.microsoft.com/dotnet/api/system.text.json.jsonserializeroptions)

