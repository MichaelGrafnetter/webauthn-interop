# <a id="DSInternals_Win32_WebAuthn_PublicKeyCredentialHint"></a> Enum PublicKeyCredentialHint

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Known public key credential hints as defined in the WebAuthn specification.

```csharp
[JsonConverter(typeof(JsonStringEnumConverter<PublicKeyCredentialHint>))]
public enum PublicKeyCredentialHint
```

#### Extension Methods

[PublicKeyCredentialHintExtensions.ToJsonString\(PublicKeyCredentialHint\)](DSInternals.Win32.WebAuthn.PublicKeyCredentialHintExtensions.md\#DSInternals\_Win32\_WebAuthn\_PublicKeyCredentialHintExtensions\_ToJsonString\_DSInternals\_Win32\_WebAuthn\_PublicKeyCredentialHint\_)

## Fields

`None = 0` 

No hint specified.



`SecurityKey = 1` 

Indicates that the Relying Party believes that users will satisfy this request with a physical security key.



`ClientDevice = 2` 

Indicates that the Relying Party believes that users will satisfy this request with a platform authenticator built into the client device.



`Hybrid = 3` 

Indicates that the Relying Party believes that users will satisfy this request with a general-purpose authenticator such as smartphone (hybrid transport).



## Remarks

This enum is a convenience surface for callers that want the currently defined hint values. The WebAuthn data
model and Win32 API wrappers use raw strings so future and custom DOMString values are preserved.

