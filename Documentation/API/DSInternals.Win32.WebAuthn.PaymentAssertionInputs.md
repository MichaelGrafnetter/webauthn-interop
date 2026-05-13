# <a id="DSInternals_Win32_WebAuthn_PaymentAssertionInputs"></a> Class PaymentAssertionInputs

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Inputs for the Secure Payment Confirmation (SPC) <code>payment</code> WebAuthn extension during assertion.

```csharp
public sealed class PaymentAssertionInputs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PaymentAssertionInputs](DSInternals.Win32.WebAuthn.PaymentAssertionInputs.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_PaymentAssertionInputs_Instrument"></a> Instrument

Instrument descriptor shown to the user.

```csharp
[JsonPropertyName("instrument")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public PaymentCredentialInstrument? Instrument { get; set; }
```

#### Property Value

 [PaymentCredentialInstrument](DSInternals.Win32.WebAuthn.PaymentCredentialInstrument.md)?

### <a id="DSInternals_Win32_WebAuthn_PaymentAssertionInputs_IsPayment"></a> IsPayment

Indicates that the extension is active.

```csharp
[JsonPropertyName("isPayment")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public bool IsPayment { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_PaymentAssertionInputs_PayeeName"></a> PayeeName

Display name of the payee shown to the user.

```csharp
[JsonPropertyName("payeeName")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public string? PayeeName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_PaymentAssertionInputs_PayeeOrigin"></a> PayeeOrigin

Origin of the payee shown to the user.

```csharp
[JsonPropertyName("payeeOrigin")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public string? PayeeOrigin { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_PaymentAssertionInputs_RpId"></a> RpId

Relying Party identifier of the credential being asserted.

```csharp
[JsonPropertyName("rpId")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public string? RpId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_PaymentAssertionInputs_TopOrigin"></a> TopOrigin

Origin of the top-level frame initiating the payment.

```csharp
[JsonPropertyName("topOrigin")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public string? TopOrigin { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_PaymentAssertionInputs_Total"></a> Total

Total amount of the transaction shown to the user.

```csharp
[JsonPropertyName("total")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public PaymentCurrencyAmount? Total { get; set; }
```

#### Property Value

 [PaymentCurrencyAmount](DSInternals.Win32.WebAuthn.PaymentCurrencyAmount.md)?

## Methods

### <a id="DSInternals_Win32_WebAuthn_PaymentAssertionInputs_FromJson_System_String_"></a> FromJson\(string\)

Deserializes a JSON string into a <xref href="DSInternals.Win32.WebAuthn.PaymentAssertionInputs" data-throw-if-not-resolved="false"></xref>.

```csharp
public static PaymentAssertionInputs? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 [PaymentAssertionInputs](DSInternals.Win32.WebAuthn.PaymentAssertionInputs.md)?

### <a id="DSInternals_Win32_WebAuthn_PaymentAssertionInputs_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

