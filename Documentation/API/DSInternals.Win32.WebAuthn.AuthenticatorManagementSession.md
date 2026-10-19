# <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession"></a> Class AuthenticatorManagementSession

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Provides credential, PIN, configuration, and large blob management for a selected authenticator.

```csharp
public sealed class AuthenticatorManagementSession : IDisposable
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[AuthenticatorManagementSession](DSInternals.Win32.WebAuthn.AuthenticatorManagementSession.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Dispose this object to release the selection. Operations and disposal are serialized.
Authenticate explicitly before PIN-gated operations. No operation retries PINs or reselects a device.
Asynchronous management operations retain the synchronous session's serialization and event-based cancellation behavior.
Caller-owned input arrays must not be modified until the operation completes, including asynchronous operations.
Caller-owned PINs must not be modified or disposed until the operation completes.
These undocumented APIs are not guaranteed to remain compatible with future Windows updates.

## Methods

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession_AuthenticatePin_System_Security_SecureString_System_Threading_CancellationToken_"></a> AuthenticatePin\(SecureString, CancellationToken\)

Submits a PIN exactly once to unlock PIN-gated management operations.

```csharp
public (PinAuthenticationStatus Status, uint Retries) AuthenticatePin(SecureString pin, CancellationToken cancellationToken = default)
```

#### Parameters

`pin` [SecureString](https://learn.microsoft.com/dotnet/api/system.security.securestring)

Caller-owned PIN; copied to temporary writable memory and never retained.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Signals native cancellation.

#### Returns

 \([PinAuthenticationStatus](DSInternals.Win32.WebAuthn.PinAuthenticationStatus.md) [Status](https://learn.microsoft.com/dotnet/api/system.valuetuple\-dsinternals.win32.webauthn.pinauthenticationstatus,system.uint32\-.status), [uint](https://learn.microsoft.com/dotnet/api/system.uint32) [Retries](https://learn.microsoft.com/dotnet/api/system.valuetuple\-dsinternals.win32.webauthn.pinauthenticationstatus,system.uint32\-.retries)\)

The PIN diagnostic and remaining retry count; zero retries can also mean unreported.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession_AuthenticatePinAsync_System_Security_SecureString_System_Threading_CancellationToken_"></a> AuthenticatePinAsync\(SecureString, CancellationToken\)

Asynchronously authenticates the PIN to unlock authenticator management operations.

```csharp
public Task<(PinAuthenticationStatus Status, uint Retries)> AuthenticatePinAsync(SecureString pin, CancellationToken cancellationToken = default)
```

#### Parameters

`pin` [SecureString](https://learn.microsoft.com/dotnet/api/system.security.securestring)

Caller-owned PIN.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the operation.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<\([PinAuthenticationStatus](DSInternals.Win32.WebAuthn.PinAuthenticationStatus.md) [Status](https://learn.microsoft.com/dotnet/api/system.valuetuple\-dsinternals.win32.webauthn.pinauthenticationstatus,system.uint32\-.status), [uint](https://learn.microsoft.com/dotnet/api/system.uint32) [Retries](https://learn.microsoft.com/dotnet/api/system.valuetuple\-dsinternals.win32.webauthn.pinauthenticationstatus,system.uint32\-.retries)\)\>

A task producing the PIN diagnostic and remaining retry count; zero retries can also mean unreported.

#### Remarks

Keep the caller-owned PIN alive until completion. Authentication is attempted once without automatic retries.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession_ChangePin_System_Security_SecureString_System_Security_SecureString_System_Threading_CancellationToken_"></a> ChangePin\(SecureString, SecureString, CancellationToken\)

Changes a PIN directly without first submitting a separate authentication attempt.

```csharp
public (PinAuthenticationStatus Status, uint Retries) ChangePin(SecureString currentPin, SecureString newPin, CancellationToken cancellationToken = default)
```

#### Parameters

`currentPin` [SecureString](https://learn.microsoft.com/dotnet/api/system.security.securestring)

Caller-owned current PIN.

`newPin` [SecureString](https://learn.microsoft.com/dotnet/api/system.security.securestring)

Caller-owned replacement PIN.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Signals native cancellation.

#### Returns

 \([PinAuthenticationStatus](DSInternals.Win32.WebAuthn.PinAuthenticationStatus.md) [Status](https://learn.microsoft.com/dotnet/api/system.valuetuple\-dsinternals.win32.webauthn.pinauthenticationstatus,system.uint32\-.status), [uint](https://learn.microsoft.com/dotnet/api/system.uint32) [Retries](https://learn.microsoft.com/dotnet/api/system.valuetuple\-dsinternals.win32.webauthn.pinauthenticationstatus,system.uint32\-.retries)\)

The PIN diagnostic and remaining retry count; zero retries can also mean unreported.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession_ChangePinAsync_System_Security_SecureString_System_Security_SecureString_System_Threading_CancellationToken_"></a> ChangePinAsync\(SecureString, SecureString, CancellationToken\)

Asynchronously changes the authenticator PIN.

```csharp
public Task<(PinAuthenticationStatus Status, uint Retries)> ChangePinAsync(SecureString currentPin, SecureString newPin, CancellationToken cancellationToken = default)
```

#### Parameters

`currentPin` [SecureString](https://learn.microsoft.com/dotnet/api/system.security.securestring)

Caller-owned current PIN.

`newPin` [SecureString](https://learn.microsoft.com/dotnet/api/system.security.securestring)

Caller-owned replacement PIN.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the operation.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<\([PinAuthenticationStatus](DSInternals.Win32.WebAuthn.PinAuthenticationStatus.md) [Status](https://learn.microsoft.com/dotnet/api/system.valuetuple\-dsinternals.win32.webauthn.pinauthenticationstatus,system.uint32\-.status), [uint](https://learn.microsoft.com/dotnet/api/system.uint32) [Retries](https://learn.microsoft.com/dotnet/api/system.valuetuple\-dsinternals.win32.webauthn.pinauthenticationstatus,system.uint32\-.retries)\)\>

A task producing the PIN diagnostic and remaining retry count; zero retries can also mean unreported.

#### Remarks

Keep both caller-owned PINs alive until completion. The change is attempted once without automatic retries.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession_Create_DSInternals_Win32_WebAuthn_AuthenticatorTransport_System_Threading_CancellationToken_"></a> Create\(AuthenticatorTransport, CancellationToken\)

Selects an authenticator and transfers context ownership to a session.

```csharp
public static AuthenticatorManagementSession Create(AuthenticatorTransport transports = AuthenticatorTransport.NoRestrictions, CancellationToken cancellationToken = default)
```

#### Parameters

`transports` [AuthenticatorTransport](DSInternals.Win32.WebAuthn.AuthenticatorTransport.md)

Provider-selection mask passed to Windows for validation.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Signals the native selection cancellation event.

#### Returns

 [AuthenticatorManagementSession](DSInternals.Win32.WebAuthn.AuthenticatorManagementSession.md)

A session owning the selected context.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession_DeleteCredential_System_Byte___System_Threading_CancellationToken_"></a> DeleteCredential\(byte\[\], CancellationToken\)

Irreversibly deletes an exact credential ID. Authenticate explicitly first.

```csharp
public void DeleteCredential(byte[] credentialId, CancellationToken cancellationToken = default)
```

#### Parameters

`credentialId` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

Opaque credential ID bytes, not an enumeration index.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Signals native cancellation.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession_DeleteCredentialAsync_System_Byte___System_Threading_CancellationToken_"></a> DeleteCredentialAsync\(byte\[\], CancellationToken\)

Asynchronously deletes an exact credential ID, without retries.

```csharp
public Task DeleteCredentialAsync(byte[] credentialId, CancellationToken cancellationToken = default)
```

#### Parameters

`credentialId` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

Opaque credential ID.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the operation.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

A task completing after the native deletion attempt.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession_Dispose"></a> Dispose\(\)

Releases the selected native context after any in-progress operation finishes.

```csharp
public void Dispose()
```

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession_EnableEnterpriseAttestation_System_Threading_CancellationToken_"></a> EnableEnterpriseAttestation\(CancellationToken\)

Enables enterprise attestation; this API does not provide a disable operation.

```csharp
public void EnableEnterpriseAttestation(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Signals native cancellation.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession_EnableEnterpriseAttestationAsync_System_Threading_CancellationToken_"></a> EnableEnterpriseAttestationAsync\(CancellationToken\)

Asynchronously enables enterprise attestation.

```csharp
public Task EnableEnterpriseAttestationAsync(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the operation.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

A task completing after the enterprise attestation configuration attempt.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession_GetCapabilities_System_Threading_CancellationToken_"></a> GetCapabilities\(CancellationToken\)

Gets the selected authenticator's management capabilities and configuration.

```csharp
public AuthenticatorManagementCapabilities GetCapabilities(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the operation before dispatch.

#### Returns

 [AuthenticatorManagementCapabilities](DSInternals.Win32.WebAuthn.AuthenticatorManagementCapabilities.md)

A pointer-free capability snapshot independent of session lifetime.

#### Remarks

This copies the last native context refresh, not a fresh device query. Reopen a session after an uncertain mutation.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession_GetCredentials_System_Threading_CancellationToken_"></a> GetCredentials\(CancellationToken\)

Enumerates copied resident credential display records on the selected authenticator.

```csharp
public IReadOnlyList<DiscoverableCredentialDetails> GetCredentials(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Signals native cancellation.

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[DiscoverableCredentialDetails](DSInternals.Win32.WebAuthn.DiscoverableCredentialDetails.md)\>

Independent credential display records, or an empty list when none exist.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession_GetCredentialsAsync_System_Threading_CancellationToken_"></a> GetCredentialsAsync\(CancellationToken\)

Asynchronously enumerates copied discoverable credentials.

```csharp
public Task<IReadOnlyList<DiscoverableCredentialDetails>> GetCredentialsAsync(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the operation.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[DiscoverableCredentialDetails](DSInternals.Win32.WebAuthn.DiscoverableCredentialDetails.md)\>\>

A task producing independent credential display records.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession_GetLargeBlobs_System_Threading_CancellationToken_"></a> GetLargeBlobs\(CancellationToken\)

Copies the complete serialized large-blob store without validating its size or integrity.

```csharp
public byte[]? GetLargeBlobs(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Signals native cancellation.

#### Returns

 [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?

The complete store including its 16-byte checksum, without decryption, or null if Windows returns no data.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession_GetLargeBlobsAsync_System_Threading_CancellationToken_"></a> GetLargeBlobsAsync\(CancellationToken\)

Asynchronously reads the complete serialized large-blob store.

```csharp
public Task<byte[]?> GetLargeBlobsAsync(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the operation.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]?\>

A task producing the serialized store and its checksum, or null if Windows returns no data.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession_GetPinPolicy_System_Threading_CancellationToken_"></a> GetPinPolicy\(CancellationToken\)

Gets the selected authenticator's PIN and user-verification policy.

```csharp
public AuthenticatorPinPolicy GetPinPolicy(CancellationToken cancellationToken = default)
```

#### Parameters

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the operation before dispatch.

#### Returns

 [AuthenticatorPinPolicy](DSInternals.Win32.WebAuthn.AuthenticatorPinPolicy.md)

The copied nullable PIN/configuration policy.

#### Remarks

This reads the session's context snapshot. It does not independently refresh state from the device.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession_SetAlwaysUv_System_Boolean_System_Threading_CancellationToken_"></a> SetAlwaysUv\(bool, CancellationToken\)

Sets the state of Always Require User Verification.

```csharp
public void SetAlwaysUv(bool enabled, CancellationToken cancellationToken = default)
```

#### Parameters

`enabled` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

True to require user verification even when the relying party does not request it; false to disable Always Require User Verification.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Signals native cancellation.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession_SetAlwaysUvAsync_System_Boolean_System_Threading_CancellationToken_"></a> SetAlwaysUvAsync\(bool, CancellationToken\)

Asynchronously sets the state of Always Require User Verification.

```csharp
public Task SetAlwaysUvAsync(bool enabled, CancellationToken cancellationToken = default)
```

#### Parameters

`enabled` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

The desired state.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the operation.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

A task completing after the Always Require User Verification configuration attempt.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession_SetLargeBlobs_System_Byte___System_Threading_CancellationToken_"></a> SetLargeBlobs\(byte\[\], CancellationToken\)

Replaces the entire serialized large-blob store. Authenticate explicitly first.

```csharp
public void SetLargeBlobs(byte[] data, CancellationToken cancellationToken = default)
```

#### Parameters

`data` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

Serialized large-blob store; the caller is responsible for its schema and integrity.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Signals native cancellation.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession_SetLargeBlobsAsync_System_Byte___System_Threading_CancellationToken_"></a> SetLargeBlobsAsync\(byte\[\], CancellationToken\)

Asynchronously replaces the complete serialized large-blob store.

```csharp
public Task SetLargeBlobsAsync(byte[] data, CancellationToken cancellationToken = default)
```

#### Parameters

`data` [byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]

Serialized store including checksum.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the operation.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

A task completing after the whole-store replacement attempt.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession_SetPin_System_Security_SecureString_System_Threading_CancellationToken_"></a> SetPin\(SecureString, CancellationToken\)

Installs an initial PIN. This does not replace an existing PIN.

```csharp
public PinAuthenticationStatus SetPin(SecureString newPin, CancellationToken cancellationToken = default)
```

#### Parameters

`newPin` [SecureString](https://learn.microsoft.com/dotnet/api/system.security.securestring)

Caller-owned new PIN; at least four code points and at most 63 UTF-8 bytes.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Signals native cancellation.

#### Returns

 [PinAuthenticationStatus](DSInternals.Win32.WebAuthn.PinAuthenticationStatus.md)

The PIN diagnostic reported by the operation.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession_SetPinAsync_System_Security_SecureString_System_Threading_CancellationToken_"></a> SetPinAsync\(SecureString, CancellationToken\)

Asynchronously configures an initial authenticator PIN.

```csharp
public Task<PinAuthenticationStatus> SetPinAsync(SecureString newPin, CancellationToken cancellationToken = default)
```

#### Parameters

`newPin` [SecureString](https://learn.microsoft.com/dotnet/api/system.security.securestring)

Caller-owned new PIN.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the operation.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task\-1)<[PinAuthenticationStatus](DSInternals.Win32.WebAuthn.PinAuthenticationStatus.md)\>

A task producing the PIN diagnostic reported by the operation.

#### Remarks

Keep the caller-owned PIN alive until completion.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession_SetPinPolicy_System_Nullable_System_UInt32__System_Boolean_System_String___System_Threading_CancellationToken_"></a> SetPinPolicy\(uint?, bool, string\[\]?, CancellationToken\)

Updates minimum PIN length, authorized RP IDs, and/or a future PIN-change requirement.

```csharp
public void SetPinPolicy(uint? minimumPinLength = null, bool forcePinChange = false, string[]? relyingPartyIds = null, CancellationToken cancellationToken = default)
```

#### Parameters

`minimumPinLength` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)?

4 to 63 Unicode code points; null preserves the existing policy.

`forcePinChange` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

True requests a change; false cannot clear an existing requirement.

`relyingPartyIds` [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]?

Authorized RP IDs; null/empty preserves the existing list, not clears it.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Signals native cancellation.

### <a id="DSInternals_Win32_WebAuthn_AuthenticatorManagementSession_SetPinPolicyAsync_System_Nullable_System_UInt32__System_Boolean_System_String___System_Threading_CancellationToken_"></a> SetPinPolicyAsync\(uint?, bool, string\[\]?, CancellationToken\)

Asynchronously updates PIN policy without retries.

```csharp
public Task SetPinPolicyAsync(uint? minimumPinLength = null, bool forcePinChange = false, string[]? relyingPartyIds = null, CancellationToken cancellationToken = default)
```

#### Parameters

`minimumPinLength` [uint](https://learn.microsoft.com/dotnet/api/system.uint32)?

Null preserves the current minimum.

`forcePinChange` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Requests a PIN change without clearing an existing requirement.

`relyingPartyIds` [string](https://learn.microsoft.com/dotnet/api/system.string)\[\]?

Null or empty preserves authorized RP IDs.

`cancellationToken` [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken)

Cancels the operation.

#### Returns

 [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task)

A task completing after the PIN policy update attempt.

