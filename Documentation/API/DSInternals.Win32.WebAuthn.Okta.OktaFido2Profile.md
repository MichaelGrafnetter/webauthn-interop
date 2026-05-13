# <a id="DSInternals_Win32_WebAuthn_Okta_OktaFido2Profile"></a> Class OktaFido2Profile

Namespace: [DSInternals.Win32.WebAuthn.Okta](DSInternals.Win32.WebAuthn.Okta.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents the Okta profile metadata associated with a FIDO2 authentication method.

```csharp
public class OktaFido2Profile
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[OktaFido2Profile](DSInternals.Win32.WebAuthn.Okta.OktaFido2Profile.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaFido2Profile__ctor_System_String_System_Object_System_Object_System_String_System_Object_System_Object_"></a> OktaFido2Profile\(string, object, object, string, object, object\)

Initializes a new instance of the <xref href="DSInternals.Win32.WebAuthn.Okta.OktaFido2Profile" data-throw-if-not-resolved="false"></xref> class.

```csharp
[JsonConstructor]
public OktaFido2Profile(string credentialId, object appId, object version, string authenticatorName, object presetPinAvailable, object fulfillmentProvider)
```

#### Parameters

`credentialId` [string](https://learn.microsoft.com/dotnet/api/system.string)

`appId` [object](https://learn.microsoft.com/dotnet/api/system.object)

`version` [object](https://learn.microsoft.com/dotnet/api/system.object)

`authenticatorName` [string](https://learn.microsoft.com/dotnet/api/system.string)

`presetPinAvailable` [object](https://learn.microsoft.com/dotnet/api/system.object)

`fulfillmentProvider` [object](https://learn.microsoft.com/dotnet/api/system.object)

## Properties

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaFido2Profile_AppId"></a> AppId

U2F appId string

```csharp
[JsonPropertyName("appId")]
public object AppId { get; }
```

#### Property Value

 [object](https://learn.microsoft.com/dotnet/api/system.object)

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaFido2Profile_AuthenticatorName"></a> AuthenticatorName

Human-readable name of the authenticator

```csharp
[JsonPropertyName("authenticatorName")]
public string AuthenticatorName { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaFido2Profile_CredentialId"></a> CredentialId

ID for the Factor credential

```csharp
[JsonPropertyName("credentialId")]
public string CredentialId { get; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaFido2Profile_FulfillmentProvider"></a> FulfillmentProvider

Undocumented

```csharp
[JsonPropertyName("fulfillmentProvider")]
public object FulfillmentProvider { get; }
```

#### Property Value

 [object](https://learn.microsoft.com/dotnet/api/system.object)

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaFido2Profile_PresetPinAvailable"></a> PresetPinAvailable

Undocumented

```csharp
[JsonPropertyName("presetPinAvailable")]
public object PresetPinAvailable { get; }
```

#### Property Value

 [object](https://learn.microsoft.com/dotnet/api/system.object)

### <a id="DSInternals_Win32_WebAuthn_Okta_OktaFido2Profile_Version"></a> Version

Undocumented

```csharp
[JsonPropertyName("version")]
public object Version { get; }
```

#### Property Value

 [object](https://learn.microsoft.com/dotnet/api/system.object)

