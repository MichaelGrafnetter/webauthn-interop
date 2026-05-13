# <a id="DSInternals_Win32_WebAuthn_PaymentAttestationInputs"></a> Class PaymentAttestationInputs

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Inputs for the Secure Payment Confirmation (SPC) <code>payment</code> WebAuthn extension during credential creation.

```csharp
public sealed class PaymentAttestationInputs
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[PaymentAttestationInputs](DSInternals.Win32.WebAuthn.PaymentAttestationInputs.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

During registration only <xref href="DSInternals.Win32.WebAuthn.PaymentAttestationInputs.IsPayment" data-throw-if-not-resolved="false"></xref> is meaningful. The other authentication-only fields documented in the
W3C Secure Payment Confirmation specification are exposed by <xref href="DSInternals.Win32.WebAuthn.PaymentAssertionInputs" data-throw-if-not-resolved="false"></xref>.

## Properties

### <a id="DSInternals_Win32_WebAuthn_PaymentAttestationInputs_IsPayment"></a> IsPayment

Indicates that the extension is active. When the credential is created with this flag,
the resulting credential is eligible to be used in a Secure Payment Confirmation flow by other origins.

```csharp
[JsonPropertyName("isPayment")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
public bool IsPayment { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### <a id="DSInternals_Win32_WebAuthn_PaymentAttestationInputs_FromJson_System_String_"></a> FromJson\(string\)

Deserializes a JSON string into a <xref href="DSInternals.Win32.WebAuthn.PaymentAttestationInputs" data-throw-if-not-resolved="false"></xref>.

```csharp
public static PaymentAttestationInputs? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

#### Returns

 [PaymentAttestationInputs](DSInternals.Win32.WebAuthn.PaymentAttestationInputs.md)?

### <a id="DSInternals_Win32_WebAuthn_PaymentAttestationInputs_ToString"></a> ToString\(\)

Returns a string that represents the current object.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

A string that represents the current object.

