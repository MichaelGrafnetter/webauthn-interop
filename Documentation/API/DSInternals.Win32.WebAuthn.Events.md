# <a id="DSInternals_Win32_WebAuthn_Events"></a> Namespace DSInternals.Win32.WebAuthn.Events

### Classes

 [ApiVersionEvent](DSInternals.Win32.WebAuthn.Events.ApiVersionEvent.md)

Represents an API version query event (Event ID 1071).

 [BleFunctionWarningEvent](DSInternals.Win32.WebAuthn.Events.BleFunctionWarningEvent.md)

Represents a BLE function warning event (Event ID 2270).

 [CancelCurrentOperationEvent](DSInternals.Win32.WebAuthn.Events.CancelCurrentOperationEvent.md)

Represents the result of a WebAuthNCancelCurrentOperation call (Event ID 1072).

 [CborMakeCredentialResponseEvent](DSInternals.Win32.WebAuthn.Events.CborMakeCredentialResponseEvent.md)

Represents a CBOR-decoded MakeCredential response event (Event ID 1102).

 [CtapCommandCompletedEvent](DSInternals.Win32.WebAuthn.Events.CtapCommandCompletedEvent.md)

Represents a CTAP platform command completed event (Event ID 2102).

 [CtapCommandErrorEvent](DSInternals.Win32.WebAuthn.Events.CtapCommandErrorEvent.md)

Represents a CTAP platform command error event (Event ID 2103).

 [CtapCommandStartedEvent](DSInternals.Win32.WebAuthn.Events.CtapCommandStartedEvent.md)

Represents a CTAP platform command started event (Event ID 2100).

 [DeviceInfoEvent](DSInternals.Win32.WebAuthn.Events.DeviceInfoEvent.md)

Represents a CTAP device info event (Event ID 2104).

 [FunctionWarningEvent](DSInternals.Win32.WebAuthn.Events.FunctionWarningEvent.md)

Represents a CTAP function warning event (Event ID 2105).

 [GetAssertionRequestEvent](DSInternals.Win32.WebAuthn.Events.GetAssertionRequestEvent.md)

Represents a CBOR-encoded GetAssertion request event (Event ID 1103).

 [GetAssertionResponseEvent](DSInternals.Win32.WebAuthn.Events.GetAssertionResponseEvent.md)

Represents a CBOR-decoded GetAssertion response event (Event ID 1104).

 [IsUserVerifyingPlatformAuthenticatorAvailableEvent](DSInternals.Win32.WebAuthn.Events.IsUserVerifyingPlatformAuthenticatorAvailableEvent.md)

Represents a platform authenticator availability query event (Event ID 1070).

 [MakeCredentialCompletedEvent](DSInternals.Win32.WebAuthn.Events.MakeCredentialCompletedEvent.md)

Represents a credential creation operation completed event (Event ID 1001).

 [MakeCredentialRequestEvent](DSInternals.Win32.WebAuthn.Events.MakeCredentialRequestEvent.md)

Represents a CBOR-encoded MakeCredential request event (Event ID 1101).

 [MakeCredentialResponseEvent](DSInternals.Win32.WebAuthn.Events.MakeCredentialResponseEvent.md)

Base class for MakeCredential response events (<xref href="DSInternals.Win32.WebAuthn.Events.CborMakeCredentialResponseEvent" data-throw-if-not-resolved="false"></xref> and <xref href="DSInternals.Win32.WebAuthn.Events.NgcMakeCredentialResponseEvent" data-throw-if-not-resolved="false"></xref>).

 [NameValueEvent](DSInternals.Win32.WebAuthn.Events.NameValueEvent.md)

Represents a CTAP name-value pair event (Event ID 2106).
Contains JSON response data like authenticationResponseJSON or registrationResponseJSON.

 [NgcMakeCredentialResponseEvent](DSInternals.Win32.WebAuthn.Events.NgcMakeCredentialResponseEvent.md)

Represents an NGC MakeCredential response event (Event ID 1041).

 [UsbAddDeviceEvent](DSInternals.Win32.WebAuthn.Events.UsbAddDeviceEvent.md)

Represents a USB add device event (Event ID 2220).

 [UsbDeviceCompletedEvent](DSInternals.Win32.WebAuthn.Events.UsbDeviceCompletedEvent.md)

Represents a USB device completed event (Event ID 2211).

 [UsbDeviceStartedEvent](DSInternals.Win32.WebAuthn.Events.UsbDeviceStartedEvent.md)

Represents a USB device started event (Event ID 2210).

 [WebAuthnAssertionOperation](DSInternals.Win32.WebAuthn.Events.WebAuthnAssertionOperation.md)

Aggregated data about a WebAuthn authentication (GetAssertion) operation,
combining information from multiple related WebAuthN events.

 [WebAuthnAttestationOperation](DSInternals.Win32.WebAuthn.Events.WebAuthnAttestationOperation.md)

Aggregated data about a WebAuthn registration (MakeCredential) operation,
combining information from multiple related WebAuthN events.

 [WebAuthnEvent](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md)

Base class for all WebAuthN event log entries from the
<code>Microsoft-Windows-WebAuthN/Operational</code> event log.

 [WebAuthnEventReader](DSInternals.Win32.WebAuthn.Events.WebAuthnEventReader.md)

Reads events from the Microsoft-Windows-WebAuthN/Operational event log.

 [WebAuthnOperation](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md)

Base class for aggregated WebAuthn operations, combining data from multiple related events
grouped by transaction ID.

 [WebAuthnOperationBuilder](DSInternals.Win32.WebAuthn.Events.WebAuthnOperationBuilder.md)

Aggregates raw WebAuthN events into <xref href="DSInternals.Win32.WebAuthn.Events.WebAuthnAttestationOperation" data-throw-if-not-resolved="false"></xref> and <xref href="DSInternals.Win32.WebAuthn.Events.WebAuthnAssertionOperation" data-throw-if-not-resolved="false"></xref> operations
grouped by transaction ID.

### Enums

 [WebAuthnEventId](DSInternals.Win32.WebAuthn.Events.WebAuthnEventId.md)

Known event IDs from the Microsoft-Windows-WebAuthN/Operational event log.

