# <a id="DSInternals_Win32_WebAuthn_PaymentCredentialInstrument"></a> Class PaymentCredentialInstrument

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Payment instrument descriptor displayed during a Secure Payment Confirmation ceremony.

```csharp
public sealed class PaymentCredentialInstrument
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PaymentCredentialInstrument](DSInternals.Win32.WebAuthn.PaymentCredentialInstrument.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_PaymentCredentialInstrument_Details"></a> Details

Optional additional details for the instrument.

```csharp
[JsonPropertyName("details")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public string? Details { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_PaymentCredentialInstrument_DisplayName"></a> DisplayName

Human-readable instrument label.

```csharp
[JsonPropertyName("displayName")]
[JsonRequired]
public required string DisplayName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_PaymentCredentialInstrument_Icon"></a> Icon

URL of the instrument icon. May be a data: URL.

```csharp
[JsonPropertyName("icon")]
[JsonRequired]
public required string Icon { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_PaymentCredentialInstrument_IconMustBeShown"></a> IconMustBeShown

Whether the icon must be successfully fetched and shown for the ceremony to continue.

```csharp
[JsonPropertyName("iconMustBeShown")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public bool? IconMustBeShown { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

## Methods

### <a id="DSInternals_Win32_WebAuthn_PaymentCredentialInstrument_FromJson_System_String_"></a> FromJson\(string\)

Deserializes a JSON string into a <xref href="DSInternals.Win32.WebAuthn.PaymentCredentialInstrument" data-throw-if-not-resolved="false"></xref>.

```csharp
public static PaymentCredentialInstrument? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 [PaymentCredentialInstrument](DSInternals.Win32.WebAuthn.PaymentCredentialInstrument.md)?

### <a id="DSInternals_Win32_WebAuthn_PaymentCredentialInstrument_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

