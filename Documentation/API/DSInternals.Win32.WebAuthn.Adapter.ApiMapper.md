# <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper"></a> Class ApiMapper

Namespace: [DSInternals.Win32.WebAuthn.Adapter](DSInternals.Win32.WebAuthn.Adapter.md)  
Assembly: DSInternals.Win32.WebAuthn.Adapter.dll  

Performs mapping between Fido2NetLib objects and low-level data structures.

```csharp
public static class ApiMapper
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ApiMapper](DSInternals.Win32.WebAuthn.Adapter.ApiMapper.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_Fido2NetLib_PublicKeyCredentialRpEntity_"></a> Translate\(PublicKeyCredentialRpEntity\)

```csharp
public static RelyingPartyInformation Translate(PublicKeyCredentialRpEntity relyingParty)
```

#### Parameters

`relyingParty` [PublicKeyCredentialRpEntity](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/b6f76420ba74f2a6a0486816f2cf36094b12fe9d/Src/Fido2.Models/CredentialCreateOptions.cs)

#### Returns

 [RelyingPartyInformation](DSInternals.Win32.WebAuthn.RelyingPartyInformation.md)

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_Fido2NetLib_Fido2User_"></a> Translate\(Fido2User\)

```csharp
public static UserInformation Translate(Fido2User user)
```

#### Parameters

`user` [Fido2User](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/b6f76420ba74f2a6a0486816f2cf36094b12fe9d/Src/Fido2.Models/CredentialCreateOptions.cs)

#### Returns

 [UserInformation](DSInternals.Win32.WebAuthn.UserInformation.md)

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_System_Collections_Generic_IList_Fido2NetLib_PubKeyCredParam__"></a> Translate\(IList<PubKeyCredParam\>\)

```csharp
public static Algorithm[] Translate(IList<PubKeyCredParam> credParams)
```

#### Parameters

`credParams` [IList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist\-1)<[PubKeyCredParam](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/b6f76420ba74f2a6a0486816f2cf36094b12fe9d/Src/Fido2.Models/CredentialCreateOptions.cs)\>

#### Returns

 [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)\[\]

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_System_Int64_"></a> Translate\(long\)

```csharp
public static Algorithm Translate(long algorithm)
```

#### Parameters

`algorithm` [long](https://learn.microsoft.com/dotnet/api/system.int64)

#### Returns

 [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_Fido2NetLib_Objects_COSE_Algorithm_"></a> Translate\(Algorithm\)

```csharp
public static Algorithm Translate(COSE.Algorithm algorithm)
```

#### Parameters

`algorithm` [COSE](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/b6f76420ba74f2a6a0486816f2cf36094b12fe9d/Src/Fido2.Models/COSETypes.cs).[Algorithm](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/b6f76420ba74f2a6a0486816f2cf36094b12fe9d/Src/Fido2.Models/COSETypes.cs)

#### Returns

 [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_System_Collections_Generic_IEnumerable_Fido2NetLib_Objects_PublicKeyCredentialDescriptor__"></a> Translate\(IEnumerable<PublicKeyCredentialDescriptor\>\)

```csharp
public static IList<PublicKeyCredentialDescriptor> Translate(IEnumerable<PublicKeyCredentialDescriptor> credentials)
```

#### Parameters

`credentials` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[PublicKeyCredentialDescriptor](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/b6f76420ba74f2a6a0486816f2cf36094b12fe9d/Src/Fido2.Models/Objects/PublicKeyCredentialDescriptor.cs)\>

#### Returns

 [IList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist\-1)<[PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)\>

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_Fido2NetLib_Objects_PublicKeyCredentialDescriptor_"></a> Translate\(PublicKeyCredentialDescriptor\)

```csharp
public static PublicKeyCredentialDescriptor Translate(PublicKeyCredentialDescriptor credential)
```

#### Parameters

`credential` [PublicKeyCredentialDescriptor](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/b6f76420ba74f2a6a0486816f2cf36094b12fe9d/Src/Fido2.Models/Objects/PublicKeyCredentialDescriptor.cs)

#### Returns

 [PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_Fido2NetLib_Objects_AuthenticatorTransport___"></a> Translate\(AuthenticatorTransport\[\]\)

```csharp
public static AuthenticatorTransport Translate(AuthenticatorTransport[] transports)
```

#### Parameters

`transports` [AuthenticatorTransport](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/b6f76420ba74f2a6a0486816f2cf36094b12fe9d/Src/Fido2.Models/Objects/AuthenticatorTransport.cs)\[\]

#### Returns

 [AuthenticatorTransport](DSInternals.Win32.WebAuthn.AuthenticatorTransport.md)

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_System_Nullable_Fido2NetLib_Objects_AuthenticatorTransport__"></a> Translate\(AuthenticatorTransport?\)

```csharp
public static AuthenticatorTransport Translate(AuthenticatorTransport? transport)
```

#### Parameters

`transport` [AuthenticatorTransport](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/b6f76420ba74f2a6a0486816f2cf36094b12fe9d/Src/Fido2.Models/Objects/AuthenticatorTransport.cs)?

#### Returns

 [AuthenticatorTransport](DSInternals.Win32.WebAuthn.AuthenticatorTransport.md)

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_System_Nullable_Fido2NetLib_Objects_PublicKeyCredentialType__"></a> Translate\(PublicKeyCredentialType?\)

```csharp
public static string Translate(PublicKeyCredentialType? credentialType)
```

#### Parameters

`credentialType` [PublicKeyCredentialType](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/b6f76420ba74f2a6a0486816f2cf36094b12fe9d/Src/Fido2.Models/Objects/PublicKeyCredentialType.cs)?

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_System_Nullable_Fido2NetLib_Objects_AuthenticatorAttachment__"></a> Translate\(AuthenticatorAttachment?\)

```csharp
public static AuthenticatorAttachment Translate(AuthenticatorAttachment? authenticatorAttachment)
```

#### Parameters

`authenticatorAttachment` [AuthenticatorAttachment](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/b6f76420ba74f2a6a0486816f2cf36094b12fe9d/Src/Fido2.Models/Objects/AuthenticatorAttachment.cs)?

#### Returns

 [AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_System_Nullable_Fido2NetLib_Objects_UserVerificationRequirement__"></a> Translate\(UserVerificationRequirement?\)

```csharp
public static UserVerificationRequirement Translate(UserVerificationRequirement? userVerification)
```

#### Parameters

`userVerification` [UserVerificationRequirement](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/b6f76420ba74f2a6a0486816f2cf36094b12fe9d/Src/Fido2.Models/Objects/UserVerificationRequirement.cs)?

#### Returns

 [UserVerificationRequirement](DSInternals.Win32.WebAuthn.UserVerificationRequirement.md)

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_System_Nullable_Fido2NetLib_Objects_AttestationConveyancePreference__"></a> Translate\(AttestationConveyancePreference?\)

```csharp
public static AttestationConveyancePreference Translate(AttestationConveyancePreference? attestation)
```

#### Parameters

`attestation` [AttestationConveyancePreference](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/b6f76420ba74f2a6a0486816f2cf36094b12fe9d/Src/Fido2.Models/Objects/AttestationConveyancePreference.cs)?

#### Returns

 [AttestationConveyancePreference](DSInternals.Win32.WebAuthn.AttestationConveyancePreference.md)

