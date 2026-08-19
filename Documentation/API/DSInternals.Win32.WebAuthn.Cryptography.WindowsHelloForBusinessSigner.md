# <a id="DSInternals_Win32_WebAuthn_Cryptography_WindowsHelloForBusinessSigner"></a> Class WindowsHelloForBusinessSigner

Namespace: [DSInternals.Win32.WebAuthn.Cryptography](DSInternals.Win32.WebAuthn.Cryptography.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Produces WebAuthn assertions for Microsoft Entra ID (login.microsoft.com) using the local
Windows Hello for Business (NGC) key that is registered as a platform passkey.

```csharp
public static class WindowsHelloForBusinessSigner
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[WindowsHelloForBusinessSigner](DSInternals.Win32.WebAuthn.Cryptography.WindowsHelloForBusinessSigner.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Fields

### <a id="DSInternals_Win32_WebAuthn_Cryptography_WindowsHelloForBusinessSigner_NgcKeyNameAuthorityMarker"></a> NgcKeyNameAuthorityMarker

Fixed substring found in the name of NGC keys created for Entra ID sign-in,
e.g. S-1-5-21-.../173889d3-51f8-4156-b0e0-14c705ca6430/login.windows.net/270968e9-f6d5-43aa-940c-d330e1d50d83/john@contoso.com.

```csharp
public const string NgcKeyNameAuthorityMarker = "login.windows.net"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_WindowsHelloForBusinessSigner_NgcKeyStorageProvider"></a> NgcKeyStorageProvider

The name of the CNG key storage provider ("NGC KSP") that hosts Windows Hello for Business keys.

```csharp
public const string NgcKeyStorageProvider = "Microsoft Passport Key Storage Provider"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_WindowsHelloForBusinessSigner_RelyingPartyId"></a> RelyingPartyId

The relying party ID used by Microsoft Entra ID passkey sign-in.

```csharp
public const string RelyingPartyId = "login.microsoft.com"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Methods

### <a id="DSInternals_Win32_WebAuthn_Cryptography_WindowsHelloForBusinessSigner_GetAssertions_System_Byte___System_UInt32_System_Boolean_"></a> GetAssertions\(byte\[\], uint, bool\)

Produces an assertion for every registered Microsoft Entra ID passkey that has a matching NGC key on this device.

```csharp
public static IReadOnlyList<AssertionPublicKeyCredential> GetAssertions(byte[] challenge, uint signatureCounter = 0, bool browserInPrivateMode = false)
```

#### Parameters

`challenge` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

The challenge to sign.

`signatureCounter` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

The signature counter value to embed in the authenticator data. Defaults to 0.

`browserInPrivateMode` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Indicates whether the request originates from a browser running in private/incognito mode.

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[AssertionPublicKeyCredential](DSInternals.Win32.WebAuthn.AssertionPublicKeyCredential.md)\>

Zero, one, or more assertions, depending on the number of registered credentials with a locally resolvable key.

### <a id="DSInternals_Win32_WebAuthn_Cryptography_WindowsHelloForBusinessSigner_ListCngKeys_System_String_"></a> ListCngKeys\(string?\)

Enumerates the keys stored in the given CNG key storage provider.

```csharp
public static IReadOnlyList<NCryptKeyName> ListCngKeys(string? providerName = null)
```

#### Parameters

`providerName` [string](https://learn.microsoft.com/dotnet/api/system.string)?

The provider name, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> for the default provider.

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[NCryptKeyName](DSInternals.Win32.WebAuthn.Interop.NCryptKeyName.md)\>

### <a id="DSInternals_Win32_WebAuthn_Cryptography_WindowsHelloForBusinessSigner_ListNgcKeys"></a> ListNgcKeys\(\)

Enumerates the names of all keys stored in the NGC key storage provider.

```csharp
public static IReadOnlyList<NCryptKeyName> ListNgcKeys()
```

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[NCryptKeyName](DSInternals.Win32.WebAuthn.Interop.NCryptKeyName.md)\>

