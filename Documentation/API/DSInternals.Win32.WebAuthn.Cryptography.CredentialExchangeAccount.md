# <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeAccount"></a> Class CredentialExchangeAccount

Namespace: [DSInternals.Win32.WebAuthn.Cryptography](DSInternals.Win32.WebAuthn.Cryptography.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents a single user account in a FIDO Credential Exchange Format payload.

```csharp
public sealed class CredentialExchangeAccount
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CredentialExchangeAccount](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeAccount.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeAccount_Email"></a> Email

The email address associated with the account.

```csharp
[JsonPropertyName("email")]
public string? Email { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeAccount_FullName"></a> FullName

The full name associated with the account.

```csharp
[JsonPropertyName("fullName")]
public string? FullName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeAccount_Id"></a> Id

The probabilistically-unique identifier of the account.

```csharp
[JsonPropertyName("id")]
[JsonConverter(typeof(Base64UrlConverter))]
public byte[]? Id { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeAccount_Items"></a> Items

The vault items contained in the account.

```csharp
[JsonPropertyName("items")]
public CredentialExchangeItem[]? Items { get; set; }
```

#### Property Value

 [CredentialExchangeItem](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeItem.md)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeAccount_Username"></a> Username

The username associated with the account.

```csharp
[JsonPropertyName("username")]
public string? Username { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

