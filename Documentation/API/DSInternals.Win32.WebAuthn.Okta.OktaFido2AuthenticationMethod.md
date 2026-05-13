# <a id="DSInternals_Win32_WebAuthn_Okta_OktaFido2AuthenticationMethod"></a> Class OktaFido2AuthenticationMethod

Namespace: [DSInternals.Win32.WebAuthn.Okta](DSInternals.Win32.WebAuthn.Okta.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents an Okta FIDO2 authentication method.

```csharp
public class OktaFido2AuthenticationMethod
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[OktaFido2AuthenticationMethod](DSInternals.Win32.WebAuthn.Okta.OktaFido2AuthenticationMethod.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaFido2AuthenticationMethod__ctor_System_String_System_String_System_String_System_String_System_String_System_Nullable_System_DateTime__System_Nullable_System_DateTime__DSInternals_Win32_WebAuthn_Okta_OktaFido2Profile_"></a> OktaFido2AuthenticationMethod\(string, string, string, string, string, DateTime?, DateTime?, OktaFido2Profile\)

Initializes a new instance of the <xref href="DSInternals.Win32.WebAuthn.Okta.OktaFido2AuthenticationMethod" data-throw-if-not-resolved="false"></xref> class.

```csharp
[JsonConstructor]
public OktaFido2AuthenticationMethod(string id, string factorType, string provider, string vendorName, string status, DateTime? created, DateTime? lastUpdated, OktaFido2Profile profile)
```

#### Parameters

`id` [string](https://learn.microsoft.com/dotnet/api/system.string)

`factorType` [string](https://learn.microsoft.com/dotnet/api/system.string)

`provider` [string](https://learn.microsoft.com/dotnet/api/system.string)

`vendorName` [string](https://learn.microsoft.com/dotnet/api/system.string)

`status` [string](https://learn.microsoft.com/dotnet/api/system.string)

`created` [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)?

`lastUpdated` [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)?

`profile` [OktaFido2Profile](DSInternals.Win32.WebAuthn.Okta.OktaFido2Profile.md)

## Properties

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaFido2AuthenticationMethod_Created"></a> Created

Timestamp when the Factor was enrolled

```csharp
[JsonPropertyName("created")]
public DateTime? Created { get; }
```

#### Property Value

 [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)?

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaFido2AuthenticationMethod_FactorId"></a> FactorId

ID of the Factor

```csharp
[JsonPropertyName("id")]
public string FactorId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaFido2AuthenticationMethod_FactorType"></a> FactorType

Type of Factor

```csharp
[JsonPropertyName("factorType")]
public string FactorType { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaFido2AuthenticationMethod_LastUpdated"></a> LastUpdated

Timestamp when the Factor was last updated

```csharp
[JsonPropertyName("lastUpdated")]
public DateTime? LastUpdated { get; }
```

#### Property Value

 [DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)?

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaFido2AuthenticationMethod_Profile"></a> Profile

Specific attributes related to the Factor

```csharp
[JsonPropertyName("profile")]
public OktaFido2Profile Profile { get; }
```

#### Property Value

 [OktaFido2Profile](DSInternals.Win32.WebAuthn.Okta.OktaFido2Profile.md)

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaFido2AuthenticationMethod_Provider"></a> Provider

Provider for the Factor

```csharp
[JsonPropertyName("provider")]
public string Provider { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaFido2AuthenticationMethod_Status"></a> Status

Status of the Factor

```csharp
[JsonPropertyName("status")]
public string Status { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaFido2AuthenticationMethod_VendorName"></a> VendorName

Name of the Factor vendor. This is usually the same as the provider except for On-Prem MFA where it depends on administrator settings.

```csharp
[JsonPropertyName("vendorName")]
public string VendorName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Methods

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaFido2AuthenticationMethod_FromJsonString_System_String_"></a> FromJsonString\(string\)

Deserializes an Okta FIDO2 authentication method from JSON.

```csharp
public static OktaFido2AuthenticationMethod FromJsonString(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

The JSON payload returned by Okta.

#### Returns

 [OktaFido2AuthenticationMethod](DSInternals.Win32.WebAuthn.Okta.OktaFido2AuthenticationMethod.md)

The deserialized authentication method.

#### Exceptions

 [ArgumentNullException](https://learn.microsoft.com/dotnet/api/system.argumentnullexception)

Thrown when <code class="paramref">json</code> is null or empty.

