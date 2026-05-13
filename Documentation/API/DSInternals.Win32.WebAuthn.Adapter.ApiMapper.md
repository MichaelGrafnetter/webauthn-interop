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

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_EncodeCredentialId_System_Byte___"></a> EncodeCredentialId\(byte\[\]?\)

Encodes a credential identifier to its Base64Url string form.

```csharp
public static string EncodeCredentialId(byte[]? credentialId)
```

#### Parameters

`credentialId` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

The raw credential identifier bytes to encode.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The Base64Url-encoded credential identifier, or an empty string when <code class="paramref">credentialId</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or empty.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_Fido2NetLib_PublicKeyCredentialRpEntity_"></a> Translate\(PublicKeyCredentialRpEntity\)

Maps relying party metadata to the interop model.

```csharp
public static RelyingPartyInformation Translate(PublicKeyCredentialRpEntity relyingParty)
```

#### Parameters

`relyingParty` [PublicKeyCredentialRpEntity](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/CredentialCreateOptions.cs)

The Fido2NetLib relying party entity to map.

#### Returns

 [RelyingPartyInformation](DSInternals.Win32.WebAuthn.RelyingPartyInformation.md)

The equivalent interop relying party information.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_Fido2NetLib_Fido2User_"></a> Translate\(Fido2User\)

Maps user information to the interop model.

```csharp
public static UserInformation Translate(Fido2User user)
```

#### Parameters

`user` [Fido2User](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/CredentialCreateOptions.cs)

The Fido2NetLib user entity to map.

#### Returns

 [UserInformation](DSInternals.Win32.WebAuthn.UserInformation.md)

The equivalent interop user information.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_System_Collections_Generic_IReadOnlyList_Fido2NetLib_PubKeyCredParam__"></a> Translate\(IReadOnlyList<PubKeyCredParam\>\)

Maps credential parameter list to supported COSE algorithms.

```csharp
public static Algorithm[] Translate(IReadOnlyList<PubKeyCredParam> credParams)
```

#### Parameters

`credParams` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PubKeyCredParam](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/CredentialCreateOptions.cs)\>

The list of public key credential parameters whose algorithms should be extracted.

#### Returns

 [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)\[\]

An array of COSE algorithm identifiers in the order they were declared.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_System_Int64_"></a> Translate\(long\)

Maps numeric COSE algorithm identifier to the interop enum.

```csharp
public static Algorithm Translate(long algorithm)
```

#### Parameters

`algorithm` [long](https://learn.microsoft.com/dotnet/api/system.int64)

The numeric COSE algorithm identifier.

#### Returns

 [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)

The corresponding interop <xref href="DSInternals.Win32.WebAuthn.COSE.Algorithm" data-throw-if-not-resolved="false"></xref> value.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_Fido2NetLib_Objects_COSE_Algorithm_"></a> Translate\(Algorithm\)

Maps Fido2NetLib COSE algorithm to the interop enum.

```csharp
public static Algorithm Translate(COSE.Algorithm algorithm)
```

#### Parameters

`algorithm` [COSE](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/COSETypes.cs).[Algorithm](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/COSETypes.cs)

The Fido2NetLib COSE algorithm value.

#### Returns

 [Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)

The corresponding interop <xref href="DSInternals.Win32.WebAuthn.COSE.Algorithm" data-throw-if-not-resolved="false"></xref> value.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_System_Collections_Generic_IEnumerable_Fido2NetLib_Objects_PublicKeyCredentialDescriptor__"></a> Translate\(IEnumerable<PublicKeyCredentialDescriptor\>\)

Maps allow/exclude credential descriptors to interop descriptors.

```csharp
public static IList<PublicKeyCredentialDescriptor> Translate(IEnumerable<PublicKeyCredentialDescriptor> credentials)
```

#### Parameters

`credentials` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[PublicKeyCredentialDescriptor](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/PublicKeyCredentialDescriptor.cs)\>

The Fido2NetLib credential descriptors to map. May be <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

#### Returns

 [IList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist\-1)<[PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)\>

A list of interop credential descriptors; empty when <code class="paramref">credentials</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_Fido2NetLib_Objects_PublicKeyCredentialDescriptor_"></a> Translate\(PublicKeyCredentialDescriptor\)

Maps a single credential descriptor to the interop descriptor model.

```csharp
public static PublicKeyCredentialDescriptor Translate(PublicKeyCredentialDescriptor credential)
```

#### Parameters

`credential` [PublicKeyCredentialDescriptor](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/PublicKeyCredentialDescriptor.cs)

The Fido2NetLib credential descriptor to map.

#### Returns

 [PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)

The equivalent interop credential descriptor.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_Fido2NetLib_Objects_AuthenticatorTransport___"></a> Translate\(AuthenticatorTransport\[\]\)

Maps transport hints to interop transport flags.

```csharp
public static AuthenticatorTransport Translate(AuthenticatorTransport[] transports)
```

#### Parameters

`transports` [AuthenticatorTransport](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/AuthenticatorTransport.cs)\[\]

The Fido2NetLib transport values to combine into a flag set.

#### Returns

 [AuthenticatorTransport](DSInternals.Win32.WebAuthn.AuthenticatorTransport.md)

The combined interop transport flags, or <xref href="DSInternals.Win32.WebAuthn.AuthenticatorTransport.NoRestrictions" data-throw-if-not-resolved="false"></xref> when <code class="paramref">transports</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_System_Nullable_Fido2NetLib_Objects_AuthenticatorTransport__"></a> Translate\(AuthenticatorTransport?\)

Maps a single transport hint to interop transport flags.

```csharp
public static AuthenticatorTransport Translate(AuthenticatorTransport? transport)
```

#### Parameters

`transport` [AuthenticatorTransport](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/AuthenticatorTransport.cs)?

The Fido2NetLib transport value to map.

#### Returns

 [AuthenticatorTransport](DSInternals.Win32.WebAuthn.AuthenticatorTransport.md)

The corresponding interop transport flag, or <xref href="DSInternals.Win32.WebAuthn.AuthenticatorTransport.NoRestrictions" data-throw-if-not-resolved="false"></xref> when <code class="paramref">transport</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_System_Nullable_Fido2NetLib_Objects_PublicKeyCredentialType__"></a> Translate\(PublicKeyCredentialType?\)

Maps credential type to the WebAuthn wire type string.

```csharp
public static string Translate(PublicKeyCredentialType? credentialType)
```

#### Parameters

`credentialType` [PublicKeyCredentialType](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/PublicKeyCredentialType.cs)?

The Fido2NetLib credential type to map. <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> is treated as <xref href="Fido2NetLib.Objects.PublicKeyCredentialType.PublicKey" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The WebAuthn wire-format credential type string.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_System_Nullable_Fido2NetLib_Objects_AuthenticatorAttachment__"></a> Translate\(AuthenticatorAttachment?\)

Maps authenticator attachment preference to interop attachment enum.

```csharp
public static AuthenticatorAttachment Translate(AuthenticatorAttachment? authenticatorAttachment)
```

#### Parameters

`authenticatorAttachment` [AuthenticatorAttachment](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/AuthenticatorAttachment.cs)?

The Fido2NetLib attachment preference. <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> is mapped to <xref href="DSInternals.Win32.WebAuthn.AuthenticatorAttachment.Any" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)

The equivalent interop attachment value.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_System_Nullable_Fido2NetLib_Objects_UserVerificationRequirement__"></a> Translate\(UserVerificationRequirement?\)

Maps user verification requirement to interop requirement enum.

```csharp
public static UserVerificationRequirement Translate(UserVerificationRequirement? userVerification)
```

#### Parameters

`userVerification` [UserVerificationRequirement](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/UserVerificationRequirement.cs)?

The Fido2NetLib user verification requirement. <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> is mapped to <xref href="DSInternals.Win32.WebAuthn.UserVerificationRequirement.Any" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [UserVerificationRequirement](DSInternals.Win32.WebAuthn.UserVerificationRequirement.md)

The equivalent interop user verification requirement.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_System_Nullable_Fido2NetLib_Objects_AttestationConveyancePreference__"></a> Translate\(AttestationConveyancePreference?\)

Maps attestation conveyance preference to interop preference enum.

```csharp
public static AttestationConveyancePreference Translate(AttestationConveyancePreference? attestation)
```

#### Parameters

`attestation` [AttestationConveyancePreference](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/AttestationConveyancePreference.cs)?

The Fido2NetLib attestation conveyance preference. <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> is mapped to <xref href="DSInternals.Win32.WebAuthn.AttestationConveyancePreference.Any" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [AttestationConveyancePreference](DSInternals.Win32.WebAuthn.AttestationConveyancePreference.md)

The equivalent interop attestation conveyance preference.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_System_Collections_Generic_IReadOnlyList_Fido2NetLib_Objects_PublicKeyCredentialHint__"></a> Translate\(IReadOnlyList<PublicKeyCredentialHint\>?\)

Maps a list of credential hints to their wire-format strings.

```csharp
public static string[]? Translate(IReadOnlyList<PublicKeyCredentialHint>? hints)
```

#### Parameters

`hints` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PublicKeyCredentialHint](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/PublicKeyCredentialHint.cs)\>?

The Fido2NetLib credential hints to map.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]?

An array of wire-format hint strings, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when <code class="paramref">hints</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or empty.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_Fido2NetLib_Objects_PublicKeyCredentialHint_"></a> Translate\(PublicKeyCredentialHint\)

Maps a single credential hint to its wire-format string.

```csharp
public static string Translate(PublicKeyCredentialHint hint)
```

#### Parameters

`hint` [PublicKeyCredentialHint](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/PublicKeyCredentialHint.cs)

The Fido2NetLib credential hint to map.

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The wire-format hint string.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_System_Nullable_Fido2NetLib_Objects_LargeBlobSupport__"></a> Translate\(LargeBlobSupport?\)

Maps a Fido2NetLib large blob support preference to the interop enum.

```csharp
public static LargeBlobSupport Translate(LargeBlobSupport? support)
```

#### Parameters

`support` [LargeBlobSupport](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/LargeBlobSupport.cs)?

The Fido2NetLib large blob support preference. <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> is mapped to <xref href="DSInternals.Win32.WebAuthn.LargeBlobSupport.None" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [LargeBlobSupport](DSInternals.Win32.WebAuthn.LargeBlobSupport.md)

The equivalent interop large blob support value.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_Fido2NetLib_Objects_AuthenticationExtensionsPRFValues_"></a> Translate\(AuthenticationExtensionsPRFValues?\)

Maps Fido2NetLib PRF values to the interop equivalent.

```csharp
public static PRFValues? Translate(AuthenticationExtensionsPRFValues? values)
```

#### Parameters

`values` [AuthenticationExtensionsPRFValues](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/AuthenticationExtensionsPRFValues.cs)?

The Fido2NetLib PRF input values to map.

#### Returns

 [PRFValues](DSInternals.Win32.WebAuthn.PRFValues.md)?

The equivalent interop PRF values, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when <code class="paramref">values</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_DSInternals_Win32_WebAuthn_PRFValues_"></a> Translate\(PRFValues?\)

Maps interop PRF values to the Fido2NetLib equivalent.

```csharp
public static AuthenticationExtensionsPRFValues? Translate(PRFValues? values)
```

#### Parameters

`values` [PRFValues](DSInternals.Win32.WebAuthn.PRFValues.md)?

The interop PRF values to map.

#### Returns

 [AuthenticationExtensionsPRFValues](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/AuthenticationExtensionsPRFValues.cs)?

The equivalent Fido2NetLib PRF values, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when <code class="paramref">values</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_DSInternals_Win32_WebAuthn_PRFAttestationOutputs_"></a> Translate\(PRFAttestationOutputs?\)

Maps interop PRF attestation outputs to the Fido2NetLib equivalent.

```csharp
public static AuthenticationExtensionsPRFOutputs? Translate(PRFAttestationOutputs? outputs)
```

#### Parameters

`outputs` [PRFAttestationOutputs](DSInternals.Win32.WebAuthn.PRFAttestationOutputs.md)?

The interop PRF outputs returned by the authenticator during credential creation.

#### Returns

 [AuthenticationExtensionsPRFOutputs](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/AuthenticationExtensionsPRFOutputs.cs)?

The equivalent Fido2NetLib PRF extension outputs, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when <code class="paramref">outputs</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_DSInternals_Win32_WebAuthn_PRFAssertionOutputs_"></a> Translate\(PRFAssertionOutputs?\)

Maps interop PRF assertion outputs to the Fido2NetLib equivalent.

```csharp
public static AuthenticationExtensionsPRFOutputs? Translate(PRFAssertionOutputs? outputs)
```

#### Parameters

`outputs` [PRFAssertionOutputs](DSInternals.Win32.WebAuthn.PRFAssertionOutputs.md)?

The interop PRF outputs returned by the authenticator during an assertion.

#### Returns

 [AuthenticationExtensionsPRFOutputs](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/AuthenticationExtensionsPRFOutputs.cs)?

The equivalent Fido2NetLib PRF extension outputs, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when <code class="paramref">outputs</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_DSInternals_Win32_WebAuthn_LargeBlobAttestationOutputs_"></a> Translate\(LargeBlobAttestationOutputs?\)

Maps interop large-blob attestation outputs to the Fido2NetLib equivalent.

```csharp
public static AuthenticationExtensionsLargeBlobOutputs? Translate(LargeBlobAttestationOutputs? outputs)
```

#### Parameters

`outputs` [LargeBlobAttestationOutputs](DSInternals.Win32.WebAuthn.LargeBlobAttestationOutputs.md)?

The interop large-blob outputs returned by the authenticator during credential creation.

#### Returns

 [AuthenticationExtensionsLargeBlobOutputs](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/AuthenticationExtensionsLargeBlobOutputs.cs)?

The equivalent Fido2NetLib large-blob extension outputs, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when <code class="paramref">outputs</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_DSInternals_Win32_WebAuthn_LargeBlobAssertionOutputs_"></a> Translate\(LargeBlobAssertionOutputs?\)

Maps interop large-blob assertion outputs to the Fido2NetLib equivalent.

```csharp
public static AuthenticationExtensionsLargeBlobOutputs? Translate(LargeBlobAssertionOutputs? outputs)
```

#### Parameters

`outputs` [LargeBlobAssertionOutputs](DSInternals.Win32.WebAuthn.LargeBlobAssertionOutputs.md)?

The interop large-blob outputs returned by the authenticator during an assertion.

#### Returns

 [AuthenticationExtensionsLargeBlobOutputs](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/AuthenticationExtensionsLargeBlobOutputs.cs)?

The equivalent Fido2NetLib large-blob extension outputs, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when <code class="paramref">outputs</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAttestationOutputs_"></a> Translate\(AuthenticationExtensionsClientAttestationOutputs?\)

Maps interop attestation extension outputs to the Fido2NetLib equivalent.

```csharp
public static AuthenticationExtensionsClientOutputs? Translate(AuthenticationExtensionsClientAttestationOutputs? outputs)
```

#### Parameters

`outputs` [AuthenticationExtensionsClientAttestationOutputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAttestationOutputs.md)?

The interop client extension outputs reported after credential creation.

#### Returns

 [AuthenticationExtensionsClientOutputs](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/AuthenticationExtensionsClientOutputs.cs)?

The equivalent Fido2NetLib client extension outputs, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when <code class="paramref">outputs</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_Translate_DSInternals_Win32_WebAuthn_AuthenticationExtensionsClientAssertionOutputs_"></a> Translate\(AuthenticationExtensionsClientAssertionOutputs?\)

Maps interop assertion extension outputs to the Fido2NetLib equivalent.

```csharp
public static AuthenticationExtensionsClientOutputs? Translate(AuthenticationExtensionsClientAssertionOutputs? outputs)
```

#### Parameters

`outputs` [AuthenticationExtensionsClientAssertionOutputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAssertionOutputs.md)?

The interop client extension outputs reported after an assertion.

#### Returns

 [AuthenticationExtensionsClientOutputs](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/AuthenticationExtensionsClientOutputs.cs)?

The equivalent Fido2NetLib client extension outputs, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when <code class="paramref">outputs</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_TranslateAssertion_Fido2NetLib_Objects_AuthenticationExtensionsPRFInputs_"></a> TranslateAssertion\(AuthenticationExtensionsPRFInputs?\)

Maps Fido2NetLib PRF inputs to the interop assertion PRF inputs.

```csharp
public static PRFAssertionInputs? TranslateAssertion(AuthenticationExtensionsPRFInputs? inputs)
```

#### Parameters

`inputs` [AuthenticationExtensionsPRFInputs](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/AuthenticationExtensionsPRFInputs.cs)?

The Fido2NetLib PRF extension inputs.

#### Returns

 [PRFAssertionInputs](DSInternals.Win32.WebAuthn.PRFAssertionInputs.md)?

The equivalent interop assertion PRF inputs, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when <code class="paramref">inputs</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_TranslateAssertion_Fido2NetLib_Objects_AuthenticationExtensionsLargeBlobInputs_"></a> TranslateAssertion\(AuthenticationExtensionsLargeBlobInputs?\)

Maps Fido2NetLib large-blob inputs to the interop assertion large-blob inputs.

```csharp
public static LargeBlobAssertionInputs? TranslateAssertion(AuthenticationExtensionsLargeBlobInputs? inputs)
```

#### Parameters

`inputs` [AuthenticationExtensionsLargeBlobInputs](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/AuthenticationExtensionsLargeBlobInputs.cs)?

The Fido2NetLib large-blob extension inputs.

#### Returns

 [LargeBlobAssertionInputs](DSInternals.Win32.WebAuthn.LargeBlobAssertionInputs.md)?

The equivalent interop assertion large-blob inputs, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when <code class="paramref">inputs</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or neither a read nor a write is requested.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_TranslateAssertionExtensions_Fido2NetLib_Objects_AuthenticationExtensionsClientInputs_"></a> TranslateAssertionExtensions\(AuthenticationExtensionsClientInputs?\)

Maps Fido2NetLib assertion extension inputs to the interop equivalent.

```csharp
public static AuthenticationExtensionsClientAssertionInputs? TranslateAssertionExtensions(AuthenticationExtensionsClientInputs? inputs)
```

#### Parameters

`inputs` [AuthenticationExtensionsClientInputs](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/AuthenticationExtensionsClientInputs.cs)?

The Fido2NetLib client extension inputs supplied for an assertion request.

#### Returns

 [AuthenticationExtensionsClientAssertionInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAssertionInputs.md)?

The equivalent interop assertion extension inputs, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when <code class="paramref">inputs</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_TranslateAttestation_Fido2NetLib_Objects_AuthenticationExtensionsPRFInputs_"></a> TranslateAttestation\(AuthenticationExtensionsPRFInputs?\)

Maps Fido2NetLib PRF inputs to the interop attestation PRF inputs.

```csharp
public static PRFAttestationInputs? TranslateAttestation(AuthenticationExtensionsPRFInputs? inputs)
```

#### Parameters

`inputs` [AuthenticationExtensionsPRFInputs](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/AuthenticationExtensionsPRFInputs.cs)?

The Fido2NetLib PRF extension inputs.

#### Returns

 [PRFAttestationInputs](DSInternals.Win32.WebAuthn.PRFAttestationInputs.md)?

The equivalent interop attestation PRF inputs, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when <code class="paramref">inputs</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_TranslateAttestation_Fido2NetLib_Objects_AuthenticationExtensionsLargeBlobInputs_"></a> TranslateAttestation\(AuthenticationExtensionsLargeBlobInputs?\)

Maps Fido2NetLib large-blob inputs to the interop attestation large-blob inputs.

```csharp
public static LargeBlobAttestationInputs? TranslateAttestation(AuthenticationExtensionsLargeBlobInputs? inputs)
```

#### Parameters

`inputs` [AuthenticationExtensionsLargeBlobInputs](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/AuthenticationExtensionsLargeBlobInputs.cs)?

The Fido2NetLib large-blob extension inputs.

#### Returns

 [LargeBlobAttestationInputs](DSInternals.Win32.WebAuthn.LargeBlobAttestationInputs.md)?

The equivalent interop attestation large-blob inputs, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when <code class="paramref">inputs</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> or no support level is requested.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_TranslateAttestationExtensions_Fido2NetLib_Objects_AuthenticationExtensionsClientInputs_"></a> TranslateAttestationExtensions\(AuthenticationExtensionsClientInputs?\)

Maps Fido2NetLib attestation extension inputs to the interop equivalent.

```csharp
public static AuthenticationExtensionsClientAttestationInputs? TranslateAttestationExtensions(AuthenticationExtensionsClientInputs? inputs)
```

#### Parameters

`inputs` [AuthenticationExtensionsClientInputs](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/AuthenticationExtensionsClientInputs.cs)?

The Fido2NetLib client extension inputs supplied for credential creation.

#### Returns

 [AuthenticationExtensionsClientAttestationInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAttestationInputs.md)?

The equivalent interop attestation extension inputs, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when <code class="paramref">inputs</code> is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a>.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_TranslateCredProtect_System_Nullable_Fido2NetLib_Objects_CredentialProtectionPolicy__"></a> TranslateCredProtect\(CredentialProtectionPolicy?\)

Maps a Fido2NetLib credential protection policy to the interop user verification value.

```csharp
public static UserVerification TranslateCredProtect(CredentialProtectionPolicy? policy)
```

#### Parameters

`policy` [CredentialProtectionPolicy](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/CredentialProtectionPolicy.cs)?

The Fido2NetLib credential protection policy. <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> is mapped to <xref href="DSInternals.Win32.WebAuthn.UserVerification.Any" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [UserVerification](DSInternals.Win32.WebAuthn.UserVerification.md)

The equivalent interop user verification value used by the credProtect extension.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_TranslateCredProtect_DSInternals_Win32_WebAuthn_UserVerification_"></a> TranslateCredProtect\(UserVerification\)

Maps an interop user verification value back to a Fido2NetLib credential protection policy.

```csharp
public static CredentialProtectionPolicy? TranslateCredProtect(UserVerification verification)
```

#### Parameters

`verification` [UserVerification](DSInternals.Win32.WebAuthn.UserVerification.md)

The interop user verification value reported by the credProtect extension.

#### Returns

 [CredentialProtectionPolicy](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/CredentialProtectionPolicy.cs)?

The equivalent Fido2NetLib credential protection policy, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> when no policy applies.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_TranslateResidentKey_System_Nullable_System_Boolean__"></a> TranslateResidentKey\(bool?\)

Maps Level 1 resident key boolean to interop resident key requirement.

```csharp
public static ResidentKeyRequirement TranslateResidentKey(bool? requireResidentKey)
```

#### Parameters

`requireResidentKey` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

The WebAuthn Level 1 <code>requireResidentKey</code> flag.

#### Returns

 [ResidentKeyRequirement](DSInternals.Win32.WebAuthn.ResidentKeyRequirement.md)

<xref href="DSInternals.Win32.WebAuthn.ResidentKeyRequirement.Required" data-throw-if-not-resolved="false"></xref> when the flag is <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a>; otherwise <xref href="DSInternals.Win32.WebAuthn.ResidentKeyRequirement.Preferred" data-throw-if-not-resolved="false"></xref>.

### <a id="DSInternals_Win32_WebAuthn_Adapter_ApiMapper_TranslateResidentKey_System_Nullable_Fido2NetLib_Objects_ResidentKeyRequirement__"></a> TranslateResidentKey\(ResidentKeyRequirement?\)

Maps resident key requirement to interop resident key requirement.

```csharp
public static ResidentKeyRequirement TranslateResidentKey(ResidentKeyRequirement? residentKey)
```

#### Parameters

`residentKey` [ResidentKeyRequirement](https://github.com/passwordless\-lib/fido2\-net\-lib/blob/567965baac00fb58987d0812f8da1ea4a2245eac/Src/Fido2.Models/Objects/ResidentKeyRequirement.cs)?

The Fido2NetLib resident key requirement. <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> is mapped to <xref href="DSInternals.Win32.WebAuthn.ResidentKeyRequirement.Preferred" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [ResidentKeyRequirement](DSInternals.Win32.WebAuthn.ResidentKeyRequirement.md)

The equivalent interop resident key requirement.

