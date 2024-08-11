# <a id="DSInternals_Win32_WebAuthn_RelyingPartyInformation"></a> Class RelyingPartyInformation

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Information about an RP Entity

```csharp
public sealed class RelyingPartyInformation
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RelyingPartyInformation](DSInternals.Win32.WebAuthn.RelyingPartyInformation.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Corresponds to WEBAUTHN_RP_ENTITY_INFORMATION.

## Properties

### <a id="DSInternals_Win32_WebAuthn_RelyingPartyInformation_Icon"></a> Icon

Optional URL pointing to RP's logo.

```csharp
[JsonPropertyName("icon")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
public string Icon { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_RelyingPartyInformation_Id"></a> Id

Identifier for the RP.

```csharp
[JsonPropertyName("id")]
public string Id { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_RelyingPartyInformation_Name"></a> Name

Contains the friendly name of the Relying Party, such as "Acme Corporation", "Widgets Inc" or "Awesome Site".

```csharp
[JsonPropertyName("name")]
public string Name { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

