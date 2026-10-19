# <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementCapabilities"></a> Class AuthenticatorManagementCapabilities

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Describes the selected authenticator's management capabilities, configuration, and available storage.

```csharp
public sealed class AuthenticatorManagementCapabilities
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AuthenticatorManagementCapabilities](DSInternals.Win32.WebAuthn.AuthenticatorManagementCapabilities.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Values reflect the session's most recent context refresh, not a new device query.

## Properties

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementCapabilities_ContextVersion"></a> ContextVersion

The private context layout version.

```csharp
public uint ContextVersion { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementCapabilities_CredentialManagementState"></a> CredentialManagementState

The reported credential management availability.

```csharp
public AuthenticatorCredentialManagementState CredentialManagementState { get; }
```

#### Property Value

 [AuthenticatorCredentialManagementState](DSInternals.Win32.WebAuthn.AuthenticatorCredentialManagementState.md)

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementCapabilities_FingerprintKind"></a> FingerprintKind

The reported fingerprint sensor kind.

```csharp
public AuthenticatorFingerprintKind FingerprintKind { get; }
```

#### Property Value

 [AuthenticatorFingerprintKind](DSInternals.Win32.WebAuthn.FIDO.AuthenticatorFingerprintKind.md)

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementCapabilities_FingerprintState"></a> FingerprintState

The reported fingerprint enrollment state.

```csharp
public AuthenticatorFingerprintState FingerprintState { get; }
```

#### Property Value

 [AuthenticatorFingerprintState](DSInternals.Win32.WebAuthn.AuthenticatorFingerprintState.md)

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementCapabilities_MaximumFingerprintCaptureSamples"></a> MaximumFingerprintCaptureSamples

The maximum number of good fingerprint samples required for enrollment.

```csharp
public uint MaximumFingerprintCaptureSamples { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

#### Remarks

Corresponds to CTAP2 maxCaptureSamplesRequiredForEnroll.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementCapabilities_MaximumMessageSize"></a> MaximumMessageSize

Reported maximum CTAP message bytes, or null when unavailable.

```csharp
public uint? MaximumMessageSize { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)?

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementCapabilities_PinPolicy"></a> PinPolicy

The authenticator's PIN and user-verification policy.

```csharp
public AuthenticatorPinPolicy PinPolicy { get; }
```

#### Property Value

 [AuthenticatorPinPolicy](DSInternals.Win32.WebAuthn.AuthenticatorPinPolicy.md)

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementCapabilities_PinRetries"></a> PinRetries

Raw remaining PIN attempts; zero can also mean unreported.

```csharp
public uint PinRetries { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementCapabilities_PinState"></a> PinState

The authenticator PIN configuration status.

```csharp
public AuthenticatorPinState PinState { get; }
```

#### Property Value

 [AuthenticatorPinState](DSInternals.Win32.WebAuthn.AuthenticatorPinState.md)

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementCapabilities_RemainingDiscoverableCredentials"></a> RemainingDiscoverableCredentials

The estimated number of additional discoverable credentials the authenticator can store, or null if unreported.

```csharp
public uint? RemainingDiscoverableCredentials { get; }
```

#### Property Value

 [uint](https://learn.microsoft.com/dotnet/api/system.uint32)?

#### Remarks

A zero estimate indicates that a new credential may not fit; a smaller credential or replacement may still succeed.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementCapabilities_Transport"></a> Transport

The selected provider's transport bits.

```csharp
public AuthenticatorTransport Transport { get; }
```

#### Property Value

 [AuthenticatorTransport](DSInternals.Win32.WebAuthn.AuthenticatorTransport.md)
