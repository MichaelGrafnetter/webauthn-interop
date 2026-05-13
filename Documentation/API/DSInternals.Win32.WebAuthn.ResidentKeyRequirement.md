# <a id="DSInternals_Win32_WebAuthn_ResidentKeyRequirement"></a> Enum ResidentKeyRequirement

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

This enumeration's values describe the Relying Party's requirements for client-side discoverable credentials
(formerly known as resident credentials or resident keys).

```csharp
[JsonConverter(typeof(WebAuthnJsonEnumConverter<ResidentKeyRequirement>))]
public enum ResidentKeyRequirement : uint
```

## Fields

`Discouraged = 0` 

The Relying Party prefers creating a server-side credential, but will accept a client-side discoverable credential.
The client and authenticator SHOULD create a server-side credential if possible.



`Preferred = 1` 

The Relying Party strongly prefers creating a client-side discoverable credential, but will accept a server-side credential.
The client and authenticator SHOULD create a discoverable credential if possible.



`Required = 2` 

The Relying Party requires a client-side discoverable credential.
The client MUST return an error if a client-side discoverable credential cannot be created.



