# <a id="DSInternals_Win32_WebAuthn_DiscoverableCredentialDetails"></a> Class DiscoverableCredentialDetails

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Describes a discoverable credential's identifier, relying party, and user account.

```csharp
public sealed class DiscoverableCredentialDetails
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[DiscoverableCredentialDetails](DSInternals.Win32.WebAuthn.DiscoverableCredentialDetails.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

The private display API does not supply a user handle, RP display name, or backup flags.

## Properties

### <a id="DSInternals_Win32_WebAuthn_DiscoverableCredentialDetails_CredentialId"></a> CredentialId

The opaque credential identifier.

```csharp
public required byte[] CredentialId { get; init; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_DiscoverableCredentialDetails_RelyingPartyId"></a> RelyingPartyId

The relying party identifier, if supplied.

```csharp
public string? RelyingPartyId { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_DiscoverableCredentialDetails_UserDisplayName"></a> UserDisplayName

The account display name, if supplied.

```csharp
public string? UserDisplayName { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_DiscoverableCredentialDetails_UserName"></a> UserName

The account name, if supplied.

```csharp
public string? UserName { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?
