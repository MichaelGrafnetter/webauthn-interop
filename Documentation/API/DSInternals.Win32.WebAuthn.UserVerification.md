# <a id="DSInternals_Win32_WebAuthn_UserVerification"></a> Enum UserVerification

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Defines the credential protection policy.

```csharp
[JsonConverter(typeof(JsonCustomEnumConverter<UserVerification>))]
public enum UserVerification : uint
```

## Fields

`Any = 0` 

Extension is not set.



`Optional = 1` 

This reflects "FIDO_2_0" semantics. In this configuration, user verification is optional with or without credentialID list. This is the default state of the credential if the extension is not specified and the authenticator does not report a defaultCredProtect value in the authenticatorGetInfo response.



`OptionalWithCredentialIDList = 2` 

In this configuration, credential is discovered only when its credentialID is provided by the platform or when user verification is performed.



`Required = 3` 

This reflects that discovery and usage of the credential MUST be preceeded by user verification.



