# <a id="DSInternals_Win32_WebAuthn_HexConverter"></a> Class HexConverter

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

```csharp
[Obsolete("In .NET 5+, use System.Convert.ToHexString instead.")]
public static class HexConverter
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[HexConverter](DSInternals.Win32.WebAuthn.HexConverter.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="DSInternals_Win32_WebAuthn_HexConverter_HexToBinary_System_String_"></a> HexToBinary\(string\)

```csharp
public static byte[] HexToBinary(this string hex)
```

#### Parameters

`hex` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_HexConverter_HexToBinary_System_String_System_Int32_System_Int32_"></a> HexToBinary\(string, int, int\)

```csharp
public static byte[] HexToBinary(this string hex, int startIndex, int length)
```

#### Parameters

`hex` [string](https://learn.microsoft.com/dotnet/api/system.string)

`startIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

`length` [int](https://learn.microsoft.com/dotnet/api/system.int32)

#### Returns

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_HexConverter_ToHex_System_Byte___System_Boolean_"></a> ToHex\(byte\[\], bool\)

```csharp
public static string ToHex(this byte[] bytes, bool caps = false)
```

#### Parameters

`bytes` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

`caps` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

