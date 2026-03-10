namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Known event IDs from the Microsoft-Windows-WebAuthN/Operational event log.
/// </summary>
public enum WebAuthnEventId
{
    // MakeCredential lifecycle
    MakeCredentialStarted = 1000,
    MakeCredentialCompleted = 1001,
    MakeCredentialError = 1002,

    // GetAssertion lifecycle
    GetAssertionStarted = 1003,
    GetAssertionCompleted = 1004,
    GetAssertionError = 1005,

    // SendCommand lifecycle
    SendCommandStarted = 1006,
    SendCommandCompleted = 1007,
    SendCommandError = 1008,

    // NGC MakeCredential lifecycle
    NgcMakeCredentialStarted = 1020,
    NgcMakeCredentialCompleted = 1021,
    NgcMakeCredentialError = 1022,

    // NGC GetAssertion lifecycle
    NgcGetAssertionStarted = 1023,
    NgcGetAssertionCompleted = 1024,
    NgcGetAssertionError = 1025,

    // NGC MakeCredential response
    NgcMakeCredentialResponse = 1041,

    // Remote RPC
    RemoteRpcRequest = 1050,
    RemoteRpcResponse = 1052,

    // API information
    IsUserVerifyingPlatformAuthenticatorAvailable = 1070,
    ApiVersion = 1071,

    // CBOR encode/decode
    CborMakeCredentialRequest = 1101,
    CborMakeCredentialResponse = 1102,
    CborGetAssertionRequest = 1103,
    CborGetAssertionResponse = 1104,

    // Service lifecycle
    ServiceStarted = 2000,
    ServiceStopped = 2001,

    // CTAP platform operations
    CtapCommandStarted = 2100,
    CtapCommandCompleted = 2102,
    CtapCommandError = 2103,

    // Device info
    DeviceInfo = 2104,
    FunctionWarning = 2105,
    NameValue = 2106,
    DeviceStateInfo = 2110,

    // USB transport
    UsbProviderStarted = 2200,
    UsbProviderCompleted = 2201,
    UsbProviderError = 2202,
    UsbProviderWarning = 2203,
    UsbDeviceStarted = 2210,
    UsbDeviceCompleted = 2211,
    UsbAddDevice = 2220,
    UsbDeviceChanges = 2222,
    UsbSendReceive = 2225,

    // BLE transport
    BleProviderStarted = 2250,
    BleProviderWarning = 2253,
    BleFunctionWarning = 2270,

    // NFC transport
    NfcProviderStarted = 2300,
    NfcProviderWarning = 2303,
    NfcSkipReader = 2321,

    // Hybrid transport
    HybridProcessStarted = 2329,
    HybridProcessError = 2331,
    HybridWriteMessage = 2332,
    HybridReadMessage = 2333,
    HybridSetupStarted = 2334,
    HybridSetupCompleted = 2335
}
