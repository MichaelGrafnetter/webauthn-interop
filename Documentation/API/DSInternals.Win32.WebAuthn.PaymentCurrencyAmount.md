# <a id="DSInternals_Win32_WebAuthn_PaymentCurrencyAmount"></a> Class PaymentCurrencyAmount

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Currency and amount displayed during a Secure Payment Confirmation ceremony.

```csharp
public sealed class PaymentCurrencyAmount
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PaymentCurrencyAmount](DSInternals.Win32.WebAuthn.PaymentCurrencyAmount.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_PaymentCurrencyAmount_Currency"></a> Currency

ISO 4217 currency code.

```csharp
[JsonPropertyName("currency")]
[JsonRequired]
public required string Currency { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_PaymentCurrencyAmount_Value"></a> Value

Decimal amount string (e.g. "5.00").

```csharp
[JsonPropertyName("value")]
[JsonRequired]
public required string Value { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Methods

### <a id="DSInternals_Win32_WebAuthn_PaymentCurrencyAmount_FromJson_System_String_"></a> FromJson\(string\)

Deserializes a JSON string into a <xref href="DSInternals.Win32.WebAuthn.PaymentCurrencyAmount" data-throw-if-not-resolved="false"></xref>.

```csharp
public static PaymentCurrencyAmount? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 [PaymentCurrencyAmount](DSInternals.Win32.WebAuthn.PaymentCurrencyAmount.md)?

### <a id="DSInternals_Win32_WebAuthn_PaymentCurrencyAmount_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

