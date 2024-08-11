# <a id="DSInternals_Win32_WebAuthn_CredentialDetails"></a> Class CredentialDetails

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Information about credential.

```csharp
public sealed class CredentialDetails
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CredentialDetails](DSInternals.Win32.WebAuthn.CredentialDetails.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Corresponds to WEBAUTHN_CREDENTIAL_DETAILS.

## Properties

### <a id="DSInternals_Win32_WebAuthn_CredentialDetails_BackedUp"></a> BackedUp

Indicates if the credential is backed up or not.

```csharp
public bool BackedUp { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_CredentialDetails_CredentialId"></a> CredentialId

The credential Id.

```csharp
public byte[] CredentialId { get; set; }
```

#### Property Value

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

### <a id="DSInternals_Win32_WebAuthn_CredentialDetails_RelyingPartyInformation"></a> RelyingPartyInformation

The relying party information.

```csharp
public RelyingPartyInformation RelyingPartyInformation { get; set; }
```

#### Property Value

 [RelyingPartyInformation](DSInternals.Win32.WebAuthn.RelyingPartyInformation.md)

### <a id="DSInternals_Win32_WebAuthn_CredentialDetails_Removable"></a> Removable

Indicates if the credential is removable or not.

```csharp
public bool Removable { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_CredentialDetails_UserInformation"></a> UserInformation

The user information.

```csharp
public UserInformation UserInformation { get; set; }
```

#### Property Value

 [UserInformation](DSInternals.Win32.WebAuthn.UserInformation.md)

