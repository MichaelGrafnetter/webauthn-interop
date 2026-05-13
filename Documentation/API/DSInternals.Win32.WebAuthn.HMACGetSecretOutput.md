# <a id="DSInternals_Win32_WebAuthn_HMACGetSecretOutput"></a> Class HMACGetSecretOutput

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

The response to a hmac get secret request.

```csharp
public class HMACGetSecretOutput
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[HMACGetSecretOutput](DSInternals.Win32.WebAuthn.HMACGetSecretOutput.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_HMACGetSecretOutput_Output1"></a> Output1

Output of HMAC-SHA-256(CredRandom, Salt1)

```csharp
[JsonPropertyName("output1")]
[JsonConverter(typeof(Base64UrlConverter))]
[JsonRequired]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public byte[]? Output1 { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_HMACGetSecretOutput_Output2"></a> Output2

Output of HMAC-SHA-256(CredRandom, Salt2)

```csharp
[JsonPropertyName("output2")]
[JsonConverter(typeof(Base64UrlConverter))]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public byte[]? Output2 { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

## Methods

### <a id="DSInternals_Win32_WebAuthn_HMACGetSecretOutput_FromJson_System_String_"></a> FromJson\(string\)

Deserializes a JSON string into hmac-secret extension outputs.

```csharp
public static HMACGetSecretOutput? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of hmac-secret extension outputs.

#### Returns

 [HMACGetSecretOutput](DSInternals.Win32.WebAuthn.HMACGetSecretOutput.md)?

hmac-secret extension outputs if deserialization is successful; otherwise, null.

### <a id="DSInternals_Win32_WebAuthn_HMACGetSecretOutput_ToString"></a> ToString\(\)

Serializes the hmac-secret extension outputs to JSON.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of these hmac-secret extension outputs.

