# <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnEventId"></a> Enum WebAuthnEventId

Namespace: [DSInternals.Win32.WebAuthn.Events](DSInternals.Win32.WebAuthn.Events.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Known event IDs from the Microsoft-Windows-WebAuthN/Operational event log.

```csharp
public enum WebAuthnEventId
```

## Fields

`MakeCredentialStarted = 1000` 

Credential creation operation has started.



`MakeCredentialCompleted = 1001` 

Credential creation operation has completed.



`MakeCredentialError = 1002` 

Credential creation operation failed.



`GetAssertionStarted = 1003` 

Assertion operation has started.



`GetAssertionCompleted = 1004` 

Assertion operation has completed.



`GetAssertionError = 1005` 

Assertion operation failed.



`SendCommandStarted = 1006` 

Low-level command execution has started.



`SendCommandCompleted = 1007` 

Low-level command execution has completed.



`SendCommandError = 1008` 

Low-level command execution failed.



`NgcMakeCredentialStarted = 1020` 

NGC-backed credential creation has started.



`NgcMakeCredentialCompleted = 1021` 

NGC-backed credential creation has completed.



`NgcMakeCredentialError = 1022` 

NGC-backed credential creation failed.



`NgcGetAssertionStarted = 1023` 

NGC-backed assertion operation has started.



`NgcGetAssertionCompleted = 1024` 

NGC-backed assertion operation has completed.



`NgcGetAssertionError = 1025` 

NGC-backed assertion operation failed.



`NgcMakeCredentialResponse = 1041` 

NGC credential creation response payload.



`RemoteRpcRequest = 1050` 

Remote WebAuthn RPC request payload.



`RemoteRpcResponse = 1052` 

Remote WebAuthn RPC response payload.



`IsUserVerifyingPlatformAuthenticatorAvailable = 1070` 

Platform authenticator availability check result.



`ApiVersion = 1071` 

WebAuthn API version information event.



`CancelCurrentOperation = 1072` 

Result of a WebAuthNCancelCurrentOperation call, including the cancellation ID.



`CborMakeCredentialRequest = 1101` 

CBOR-encoded make-credential request.



`CborMakeCredentialResponse = 1102` 

CBOR-encoded make-credential response.



`CborGetAssertionRequest = 1103` 

CBOR-encoded get-assertion request.



`CborGetAssertionResponse = 1104` 

CBOR-encoded get-assertion response.



`ServiceStarted = 2000` 

WebAuthn service has started.



`ServiceStopped = 2001` 

WebAuthn service has stopped.



`CtapCommandStarted = 2100` 

CTAP command execution has started.



`CtapCommandCompleted = 2102` 

CTAP command execution has completed.



`CtapCommandError = 2103` 

CTAP command execution failed.



`DeviceInfo = 2104` 

Device capability and metadata information.



`FunctionWarning = 2105` 

Function-level warning emitted by the platform.



`NameValue = 2106` 

Generic name/value diagnostic payload.



`DeviceStateInfo = 2110` 

Device state transition information.



`UsbProviderStarted = 2200` 

USB provider operation has started.



`UsbProviderCompleted = 2201` 

USB provider operation has completed.



`UsbProviderError = 2202` 

USB provider operation failed.



`UsbProviderWarning = 2203` 

USB provider warning event.



`UsbDeviceStarted = 2210` 

USB device operation has started.



`UsbDeviceCompleted = 2211` 

USB device operation has completed.



`UsbAddDevice = 2220` 

USB authenticator has been added.



`UsbDeviceChanges = 2222` 

USB device change notification.



`UsbSendReceive = 2225` 

USB send/receive transport exchange.



`BleProviderStarted = 2250` 

BLE provider operation has started.



`BleProviderWarning = 2253` 

BLE provider warning event.



`BleFunctionWarning = 2270` 

BLE function-level warning event.



`NfcProviderStarted = 2300` 

NFC provider operation has started.



`NfcProviderWarning = 2303` 

NFC provider warning event.



`NfcSkipReader = 2321` 

NFC reader was skipped for the operation.



`HybridProcessStarted = 2329` 

Hybrid transport process has started.



`HybridProcessError = 2331` 

Hybrid transport process failed.



`HybridWriteMessage = 2332` 

Hybrid transport write-message operation.



`HybridReadMessage = 2333` 

Hybrid transport read-message operation.



`HybridSetupStarted = 2334` 

Hybrid transport setup has started.



`HybridSetupCompleted = 2335` 

Hybrid transport setup has completed.



