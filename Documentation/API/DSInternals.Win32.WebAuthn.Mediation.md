# <a id="DSInternals_Win32_WebAuthn_Mediation"></a> Enum Mediation

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Known credential mediation requirements as defined by the Credential Management specification.

```csharp
[JsonConverter(typeof(JsonStringEnumConverter<Mediation>))]
public enum Mediation
```

#### Extension Methods

[MediationExtensions.ToJsonString\(Mediation\)](DSInternals.Win32.WebAuthn.MediationExtensions.md\#DSInternals\_Win32\_WebAuthn\_MediationExtensions\_ToJsonString\_DSInternals\_Win32\_WebAuthn\_Mediation\_)

## Fields

`None = 0` 

No mediation specified.



`Silent = 1` 

The user agent must not show any UI to the user. The operation will fail if user mediation is required.



`Optional = 2` 

The user agent may show UI to the user when needed. This is the default behavior.



`Conditional = 3` 

The user agent uses conditional mediation, where credentials are surfaced through autofill (e.g. passkey autofill).



`Required = 4` 

The user agent must always show UI to the user to confirm the operation.



## Remarks

This enum is a convenience surface for callers that want the currently defined mediation values. The WebAuthn data
model and Win32 API wrappers use raw strings so future and custom DOMString values are preserved.

