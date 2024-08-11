# <a id="DSInternals_Win32_WebAuthn_AuthenticatorAttachment"></a> Enum AuthenticatorAttachment

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

This enumeration’s values describe authenticators' attachment modalities.

```csharp
[JsonConverter(typeof(JsonCustomEnumConverter<AuthenticatorAttachment>))]
public enum AuthenticatorAttachment : uint
```

## Fields

`Any = 0` 

No authenticator attachment filters are applied.

Corresponds to WEBAUTHN_AUTHENTICATOR_ATTACHMENT_ANY.

`Platform = 1` 

This value indicates platform attachment.

Corresponds to WEBAUTHN_AUTHENTICATOR_ATTACHMENT_PLATFORM.

`CrossPlatform = 2` 

This value indicates cross-platform attachment.

Corresponds to WEBAUTHN_AUTHENTICATOR_ATTACHMENT_CROSS_PLATFORM.

`CrossPlatformU2F = 3` 



Corresponds to WEBAUTHN_AUTHENTICATOR_ATTACHMENT_CROSS_PLATFORM_U2F_V2.

