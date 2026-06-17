using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;

namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Reads events from the Microsoft-Windows-WebAuthN/Operational event log.
/// </summary>
public static class WebAuthnEventReader
{
    /// <summary>
    /// Name of the operational WebAuthn event log.
    /// </summary>
    private const string LogName = "Microsoft-Windows-WebAuthN/Operational";

    /// <summary>
    /// CTAP command name for adding plugin authenticator credentials.
    /// </summary>
    private const string AddPluginAuthenticatorCredentialsCommand = "AddPluginAuthenticatorCredentials";

    /// <summary>
    /// CTAP command name for enumerating all platform credentials.
    /// </summary>
    private const string GetAllPlatformCredentialsCommand = "GetAllPlatformCredentials";

    /// <summary>
    /// Reads all events from the local WebAuthN event log.
    /// </summary>
    /// <param name="maxEvents">Optional maximum number of parsed events to return.</param>
    /// <returns>The parsed WebAuthn events.</returns>
    public static IReadOnlyList<WebAuthnEvent> ReadEvents(int? maxEvents = null)
    {
        return ReadEvents(LogName, PathType.LogName, maxEvents);
    }

    /// <summary>
    /// Reads events from a saved .evtx file.
    /// </summary>
    /// <param name="filePath">Path to the saved event log file.</param>
    /// <param name="maxEvents">Optional maximum number of parsed events to return.</param>
    /// <returns>The parsed WebAuthn events.</returns>
    public static IReadOnlyList<WebAuthnEvent> ReadEventsFromFile(string filePath, int? maxEvents = null)
    {
        return ReadEvents(filePath, PathType.FilePath, maxEvents);
    }

    /// <summary>
    /// Reads events from an event log path or file path.
    /// </summary>
    /// <param name="path">The event log name or event log file path.</param>
    /// <param name="pathType">The type of path to read from.</param>
    /// <param name="maxEvents">Optional maximum number of parsed events to return.</param>
    /// <returns>The parsed WebAuthn events.</returns>
    private static IReadOnlyList<WebAuthnEvent> ReadEvents(string path, PathType pathType, int? maxEvents)
    {
        var events = new List<WebAuthnEvent>();

        using var logReader = new EventLogReader(new EventLogQuery(path, pathType) { ReverseDirection = true });

        EventRecord? record;
        int count = 0;

        while ((record = logReader.ReadEvent()) != null)
        {
            using (record)
            {
                var evt = ParseEvent(record);
                if (evt != null)
                {
                    events.Add(evt);
                    count++;

                    if (maxEvents.HasValue && count >= maxEvents.Value)
                    {
                        break;
                    }
                }
            }
        }

        return events;
    }

    /// <summary>
    /// Parses a single event log record.
    /// </summary>
    /// <param name="record">The event record to parse.</param>
    /// <returns>The parsed WebAuthn event, or <see langword="null"/> for empty records.</returns>
    private static WebAuthnEvent? ParseEvent(EventRecord record)
    {
        if (record.Id == 0)
        {
            return null;
        }

        var eventId = (WebAuthnEventId)record.Id;
        var properties = GetEventProperties(record);

        string? message = null;

        try
        {
            message = record.FormatDescription();
        }
        catch
        {
            // FormatDescription can throw if the event provider is not registered
        }

        return eventId switch
        {
            WebAuthnEventId.MakeCredentialCompleted =>
                ParseMakeCredentialCompleted(record, properties, eventId, message),

            WebAuthnEventId.CborMakeCredentialRequest =>
                ParseMakeCredentialRequest(record, properties, eventId, message),

            WebAuthnEventId.CborMakeCredentialResponse =>
                ParseCborMakeCredentialResponse(record, properties, eventId, message),

            WebAuthnEventId.NgcMakeCredentialResponse =>
                ParseNgcMakeCredentialResponse(record, properties, eventId, message),

            WebAuthnEventId.CborGetAssertionRequest =>
                ParseGetAssertionRequest(record, properties, eventId, message),

            WebAuthnEventId.CborGetAssertionResponse =>
                ParseGetAssertionResponse(record, properties, eventId, message),

            WebAuthnEventId.DeviceInfo =>
                ParseDeviceInfo(record, properties, eventId, message),

            WebAuthnEventId.UsbDeviceCompleted =>
                ParseUsbDeviceCompleted(record, properties, eventId, message),

            WebAuthnEventId.UsbDeviceStarted =>
                ParseUsbDeviceStarted(record, properties, eventId, message),

            WebAuthnEventId.UsbAddDevice =>
                ParseUsbAddDevice(record, properties, eventId, message),

            WebAuthnEventId.IsUserVerifyingPlatformAuthenticatorAvailable =>
                ParseIsUserVerifyingPlatformAuthenticatorAvailable(record, properties, eventId, message),

            WebAuthnEventId.ApiVersion =>
                ParseApiVersion(record, properties, eventId, message),

            WebAuthnEventId.CancelCurrentOperation =>
                ParseCancelCurrentOperation(record, properties, eventId, message),

            WebAuthnEventId.NameValue =>
                ParseNameValue(record, properties, eventId, message),

            WebAuthnEventId.CtapCommandStarted =>
                ParseCtapCommandStarted(record, properties, eventId, message),

            WebAuthnEventId.CtapCommandCompleted =>
                ParseCtapCommandCompleted(record, properties, eventId, message),

            WebAuthnEventId.CtapCommandError =>
                ParseCtapCommandError(record, properties, eventId, message),

            WebAuthnEventId.FunctionWarning =>
                ParseFunctionWarning(record, properties, eventId, message),

            WebAuthnEventId.BleFunctionWarning =>
                ParseBleFunctionWarning(record, properties, eventId, message),

            _ => ParseBaseEvent(record, properties, eventId, message)
        };
    }

    /// <summary>
    /// Gets the standard event level from an event record.
    /// </summary>
    /// <param name="record">The event record to inspect.</param>
    /// <returns>The standard event level, or <see langword="null"/> when the level is unavailable.</returns>
    private static StandardEventLevel? GetLevel(EventRecord record)
    {
        return record.Level.HasValue ? (StandardEventLevel)record.Level.Value : null;
    }

    /// <summary>
    /// Parses a generic event using the known base field layouts.
    /// </summary>
    /// <param name="record">The source event record.</param>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="eventId">The WebAuthn event ID.</param>
    /// <param name="message">The formatted event message, if available.</param>
    /// <returns>The parsed base WebAuthn event.</returns>
    private static WebAuthnEvent ParseBaseEvent(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        Guid? transactionId = null;
        string? error = null;
        int? hResult = null;

        // Determine the field layout based on event ID
        switch (eventId)
        {
            // Events with TransactionId only
            case WebAuthnEventId.MakeCredentialStarted:
            case WebAuthnEventId.GetAssertionStarted:
            case WebAuthnEventId.GetAssertionCompleted:
            case WebAuthnEventId.SendCommandCompleted:
            case WebAuthnEventId.NgcMakeCredentialStarted:
            case WebAuthnEventId.NgcMakeCredentialCompleted:
            case WebAuthnEventId.NgcGetAssertionStarted:
            case WebAuthnEventId.NgcGetAssertionCompleted:
            case WebAuthnEventId.UsbProviderStarted:
            case WebAuthnEventId.UsbProviderCompleted:
            case WebAuthnEventId.UsbDeviceChanges:
            case WebAuthnEventId.BleProviderStarted:
            case WebAuthnEventId.NfcProviderStarted:
            case WebAuthnEventId.HybridProcessStarted:
            case WebAuthnEventId.HybridSetupStarted:
                if (properties.Count > 0)
                {
                    transactionId = GetGuid(properties, 0);
                }
                break;

            // Events with TransactionId + Error + HResult
            case WebAuthnEventId.MakeCredentialError:
            case WebAuthnEventId.GetAssertionError:
            case WebAuthnEventId.SendCommandError:
            case WebAuthnEventId.NgcMakeCredentialError:
            case WebAuthnEventId.NgcGetAssertionError:
            case WebAuthnEventId.HybridProcessError:
                if (properties.Count > 0) transactionId = GetGuid(properties, 0);
                if (properties.Count > 1) error = GetString(properties, 1);
                if (properties.Count > 2) hResult = GetInt32(properties, 2);
                break;

            // Events with TransactionId + Error + Win32Error
            case WebAuthnEventId.UsbProviderError:
            case WebAuthnEventId.UsbProviderWarning:
            case WebAuthnEventId.BleProviderWarning:
            case WebAuthnEventId.NfcProviderWarning:
                if (properties.Count > 0) transactionId = GetGuid(properties, 0);
                if (properties.Count > 1) error = GetString(properties, 1);
                if (properties.Count > 2) hResult = GetInt32(properties, 2);
                break;

            // SendCommand started: TransactionId, TicketLength, Ticket
            case WebAuthnEventId.SendCommandStarted:
                if (properties.Count > 0) transactionId = GetGuid(properties, 0);
                break;

            // Hybrid setup completed: TransactionId, Action, Error, HResult
            case WebAuthnEventId.HybridSetupCompleted:
                if (properties.Count > 0) transactionId = GetGuid(properties, 0);
                if (properties.Count > 2) error = GetString(properties, 2);
                if (properties.Count > 3) hResult = GetInt32(properties, 3);
                break;

            // NFC skip reader: TransactionId, Action, Reader, DeviceInstanceId
            case WebAuthnEventId.NfcSkipReader:
                if (properties.Count > 0) transactionId = GetGuid(properties, 0);
                break;

            // Remote RPC request: TransactionId, RemoteRpcRequestLength, RemoteRpcRequest
            case WebAuthnEventId.RemoteRpcRequest:
                if (properties.Count > 0) transactionId = GetGuid(properties, 0);
                break;

            // Remote RPC response: TransactionId, Error, HResult, ...
            case WebAuthnEventId.RemoteRpcResponse:
                if (properties.Count > 0) transactionId = GetGuid(properties, 0);
                if (properties.Count > 1) error = GetString(properties, 1);
                if (properties.Count > 2) hResult = GetInt32(properties, 2);
                break;

            // Device state info: Transport, WnfState, Error, Win32Error
            case WebAuthnEventId.DeviceStateInfo:
                if (properties.Count > 2) error = GetString(properties, 2);
                if (properties.Count > 3) hResult = GetInt32(properties, 3);
                break;
        }

        return new WebAuthnEvent
        {
            EventId = eventId,
            TimeCreated = record.TimeCreated?.ToLocalTime(),
            ProcessId = (int)(record.ProcessId ?? 0),
            ThreadId = (int)(record.ThreadId ?? 0),
            Level = GetLevel(record),
            Message = message,
            TransactionId = transactionId,
            Error = error,
            HResult = hResult
        };
    }

    /// <summary>
    /// Parses a MakeCredentialCompleted event.
    /// </summary>
    /// <param name="record">The source event record.</param>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="eventId">The WebAuthn event ID.</param>
    /// <param name="message">The formatted event message, if available.</param>
    /// <returns>The parsed MakeCredentialCompleted event.</returns>
    private static MakeCredentialCompletedEvent ParseMakeCredentialCompleted(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: TransactionId
        return new MakeCredentialCompletedEvent
        {
            EventId = eventId,
            TimeCreated = record.TimeCreated?.ToLocalTime(),
            ProcessId = (int)(record.ProcessId ?? 0),
            ThreadId = (int)(record.ThreadId ?? 0),
            Level = GetLevel(record),
            Message = message,
            TransactionId = GetGuid(properties, 0)
        };
    }

    /// <summary>
    /// Parses a CBOR MakeCredential request event.
    /// </summary>
    /// <param name="record">The source event record.</param>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="eventId">The WebAuthn event ID.</param>
    /// <param name="message">The formatted event message, if available.</param>
    /// <returns>The parsed MakeCredential request event.</returns>
    private static MakeCredentialRequestEvent ParseMakeCredentialRequest(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: TransactionId, RpId, UserIdLength, UserId, ClientDataHashAlgId, ClientDataLength, ClientDataHashLength, ClientDataHash, RequireResidentKey, CredentialCount, CredentialParameterCount, RequestLength, Request
        return new MakeCredentialRequestEvent
        {
            EventId = eventId,
            TimeCreated = record.TimeCreated?.ToLocalTime(),
            ProcessId = (int)(record.ProcessId ?? 0),
            ThreadId = (int)(record.ThreadId ?? 0),
            Level = GetLevel(record),
            Message = message,
            TransactionId = GetGuid(properties, 0),
            RpId = GetString(properties, 1),
            UserId = GetByteArray(properties, 3), // index 2 is UserIdLength
            ClientDataHashAlgId = GetString(properties, 4),
            ClientDataLength = GetInt32(properties, 5),
            ClientDataHash = GetByteArray(properties, 7), // index 6 is ClientDataHashLength
            RequireResidentKey = GetBoolean(properties, 8),
            CredentialCount = GetInt32(properties, 9),
            CredentialParameterCount = GetInt32(properties, 10),
            Request = GetByteArray(properties, 12) // index 11 is RequestLength
        };
    }

    /// <summary>
    /// Parses a CBOR MakeCredential response event.
    /// </summary>
    /// <param name="record">The source event record.</param>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="eventId">The WebAuthn event ID.</param>
    /// <param name="message">The formatted event message, if available.</param>
    /// <returns>The parsed CBOR MakeCredential response event.</returns>
    private static CborMakeCredentialResponseEvent ParseCborMakeCredentialResponse(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: TransactionId, AttestationFormatType, RpIdHashLength, RpIdHash, Flags, SignCount, AAGuid, CredentialIdLength, CredentialId, U2fPublicKey, PublicKeyLength, PublicKey, ResponseLength, Response
        return new CborMakeCredentialResponseEvent
        {
            EventId = eventId,
            TimeCreated = record.TimeCreated?.ToLocalTime(),
            ProcessId = (int)(record.ProcessId ?? 0),
            ThreadId = (int)(record.ThreadId ?? 0),
            Level = GetLevel(record),
            Message = message,
            TransactionId = GetGuid(properties, 0),
            AttestationFormatType = GetString(properties, 1),
            RpIdHash = GetByteArray(properties, 3), // index 2 is RpIdHashLength
            AuthenticatorFlags = (AuthenticatorFlags?)GetByte(properties, 4),
            SignCount = GetUInt32(properties, 5),
            AAGuid = GetGuid(properties, 6),
            CredentialId = GetByteArray(properties, 8), // index 7 is CredentialIdLength
            U2fPublicKey = GetByteArray(properties, 9),
            PublicKey = GetByteArray(properties, 11), // index 10 is PublicKeyLength
            Response = GetByteArray(properties, 13) // index 12 is ResponseLength
        };
    }

    /// <summary>
    /// Parses an NGC MakeCredential response event.
    /// </summary>
    /// <param name="record">The source event record.</param>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="eventId">The WebAuthn event ID.</param>
    /// <param name="message">The formatted event message, if available.</param>
    /// <returns>The parsed NGC MakeCredential response event.</returns>
    private static NgcMakeCredentialResponseEvent ParseNgcMakeCredentialResponse(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: TransactionId, AttestationFormatType, RpIdHashLength, RpIdHash, Flags, SignCount, AAGuid, CredentialIdLength, CredentialId, U2fPublicKey, PublicKeyLength, PublicKey, ResponseLength, Response
        return new NgcMakeCredentialResponseEvent
        {
            EventId = eventId,
            TimeCreated = record.TimeCreated?.ToLocalTime(),
            ProcessId = (int)(record.ProcessId ?? 0),
            ThreadId = (int)(record.ThreadId ?? 0),
            Level = GetLevel(record),
            Message = message,
            TransactionId = GetGuid(properties, 0),
            AttestationFormatType = GetString(properties, 1),
            RpIdHash = GetByteArray(properties, 3), // index 2 is RpIdHashLength
            AuthenticatorFlags = (AuthenticatorFlags?)GetByte(properties, 4),
            SignCount = GetUInt32(properties, 5),
            AAGuid = GetGuid(properties, 6),
            CredentialId = GetByteArray(properties, 8), // index 7 is CredentialIdLength
            U2fPublicKey = GetByteArray(properties, 9),
            PublicKey = GetByteArray(properties, 11), // index 10 is PublicKeyLength
            Response = GetByteArray(properties, 13) // index 12 is ResponseLength
        };
    }

    /// <summary>
    /// Parses a CBOR GetAssertion request event.
    /// </summary>
    /// <param name="record">The source event record.</param>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="eventId">The WebAuthn event ID.</param>
    /// <param name="message">The formatted event message, if available.</param>
    /// <returns>The parsed GetAssertion request event.</returns>
    private static GetAssertionRequestEvent ParseGetAssertionRequest(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: TransactionId, RpId, ClientDataHashAlgId, ClientDataLength, ClientDataHashLength, ClientDataHash, CredentialCount, RequestLength, Request
        return new GetAssertionRequestEvent
        {
            EventId = eventId,
            TimeCreated = record.TimeCreated?.ToLocalTime(),
            ProcessId = (int)(record.ProcessId ?? 0),
            ThreadId = (int)(record.ThreadId ?? 0),
            Level = GetLevel(record),
            Message = message,
            TransactionId = GetGuid(properties, 0),
            RpId = GetString(properties, 1),
            ClientDataHashAlgId = GetString(properties, 2),
            ClientDataLength = GetInt32(properties, 3),
            ClientDataHash = GetByteArray(properties, 5), // index 4 is ClientDataHashLength
            CredentialCount = GetInt32(properties, 6),
            Request = GetByteArray(properties, 8) // index 7 is RequestLength
        };
    }

    /// <summary>
    /// Parses a CBOR GetAssertion response event.
    /// </summary>
    /// <param name="record">The source event record.</param>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="eventId">The WebAuthn event ID.</param>
    /// <param name="message">The formatted event message, if available.</param>
    /// <returns>The parsed GetAssertion response event.</returns>
    private static GetAssertionResponseEvent ParseGetAssertionResponse(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: TransactionId, RpIdHashLength, RpIdHash, Flags, SignCount, CredentialIdLength, CredentialId, ResponseLength, Response
        return new GetAssertionResponseEvent
        {
            EventId = eventId,
            TimeCreated = record.TimeCreated?.ToLocalTime(),
            ProcessId = (int)(record.ProcessId ?? 0),
            ThreadId = (int)(record.ThreadId ?? 0),
            Level = GetLevel(record),
            Message = message,
            TransactionId = GetGuid(properties, 0),
            RpIdHash = GetByteArray(properties, 2), // index 1 is RpIdHashLength
            AuthenticatorFlags = (AuthenticatorFlags?)GetByte(properties, 3),
            SignCount = GetUInt32(properties, 4),
            CredentialId = GetByteArray(properties, 6), // index 5 is CredentialIdLength
            Response = GetByteArray(properties, 8) // index 7 is ResponseLength
        };
    }

    /// <summary>
    /// Parses a device information event.
    /// </summary>
    /// <param name="record">The source event record.</param>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="eventId">The WebAuthn event ID.</param>
    /// <param name="message">The formatted event message, if available.</param>
    /// <returns>The parsed device information event.</returns>
    private static DeviceInfoEvent ParseDeviceInfo(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: TransactionId, ProviderName, DevicePath, Manufacturer, Product, AAGuid, U2fProtocol
        return new DeviceInfoEvent
        {
            EventId = eventId,
            TimeCreated = record.TimeCreated?.ToLocalTime(),
            ProcessId = (int)(record.ProcessId ?? 0),
            ThreadId = (int)(record.ThreadId ?? 0),
            Level = GetLevel(record),
            Message = message,
            TransactionId = GetGuid(properties, 0),
            ProviderName = GetString(properties, 1),
            DevicePath = GetString(properties, 2),
            Manufacturer = GetString(properties, 3),
            Product = GetString(properties, 4),
            AAGuid = GetGuid(properties, 5),
            U2fProtocol = GetBoolean(properties, 6)
        };
    }

    /// <summary>
    /// Parses a USB device completed event.
    /// </summary>
    /// <param name="record">The source event record.</param>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="eventId">The WebAuthn event ID.</param>
    /// <param name="message">The formatted event message, if available.</param>
    /// <returns>The parsed USB device completed event.</returns>
    private static UsbDeviceCompletedEvent ParseUsbDeviceCompleted(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: TransactionId, DevicePath, Manufacturer, Product, AAGuid, U2fProtocol
        return new UsbDeviceCompletedEvent
        {
            EventId = eventId,
            TimeCreated = record.TimeCreated?.ToLocalTime(),
            ProcessId = (int)(record.ProcessId ?? 0),
            ThreadId = (int)(record.ThreadId ?? 0),
            Level = GetLevel(record),
            Message = message,
            TransactionId = GetGuid(properties, 0),
            DevicePath = GetString(properties, 1),
            Manufacturer = GetString(properties, 2),
            Product = GetString(properties, 3),
            AAGuid = GetGuid(properties, 4),
            U2fProtocol = GetBoolean(properties, 5)
        };
    }

    /// <summary>
    /// Parses a USB device started event.
    /// </summary>
    /// <param name="record">The source event record.</param>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="eventId">The WebAuthn event ID.</param>
    /// <param name="message">The formatted event message, if available.</param>
    /// <returns>The parsed USB device started event.</returns>
    private static UsbDeviceStartedEvent ParseUsbDeviceStarted(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: TransactionId, DevicePath, Manufacturer, Product
        return new UsbDeviceStartedEvent
        {
            EventId = eventId,
            TimeCreated = record.TimeCreated?.ToLocalTime(),
            ProcessId = (int)(record.ProcessId ?? 0),
            ThreadId = (int)(record.ThreadId ?? 0),
            Level = GetLevel(record),
            Message = message,
            TransactionId = GetGuid(properties, 0),
            DevicePath = GetString(properties, 1),
            Manufacturer = GetString(properties, 2),
            Product = GetString(properties, 3)
        };
    }

    /// <summary>
    /// Parses a USB add device event.
    /// </summary>
    /// <param name="record">The source event record.</param>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="eventId">The WebAuthn event ID.</param>
    /// <param name="message">The formatted event message, if available.</param>
    /// <returns>The parsed USB add device event.</returns>
    private static UsbAddDeviceEvent ParseUsbAddDevice(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: TransactionId, DevicePath, Manufacturer, Product
        return new UsbAddDeviceEvent
        {
            EventId = eventId,
            TimeCreated = record.TimeCreated?.ToLocalTime(),
            ProcessId = (int)(record.ProcessId ?? 0),
            ThreadId = (int)(record.ThreadId ?? 0),
            Level = GetLevel(record),
            Message = message,
            TransactionId = GetGuid(properties, 0),
            DevicePath = GetString(properties, 1),
            Manufacturer = GetString(properties, 2),
            Product = GetString(properties, 3)
        };
    }

    /// <summary>
    /// Parses a platform authenticator availability result event.
    /// </summary>
    /// <param name="record">The source event record.</param>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="eventId">The WebAuthn event ID.</param>
    /// <param name="message">The formatted event message, if available.</param>
    /// <returns>The parsed platform authenticator availability event.</returns>
    private static IsUserVerifyingPlatformAuthenticatorAvailableEvent ParseIsUserVerifyingPlatformAuthenticatorAvailable(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: value, Error, HResult
        return new IsUserVerifyingPlatformAuthenticatorAvailableEvent
        {
            EventId = eventId,
            TimeCreated = record.TimeCreated?.ToLocalTime(),
            ProcessId = (int)(record.ProcessId ?? 0),
            ThreadId = (int)(record.ThreadId ?? 0),
            Level = GetLevel(record),
            Message = message,
            IsAvailable = GetBoolean(properties, 0),
            Error = GetString(properties, 1),
            HResult = GetInt32(properties, 2)
        };
    }

    /// <summary>
    /// Parses an API version event.
    /// </summary>
    /// <param name="record">The source event record.</param>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="eventId">The WebAuthn event ID.</param>
    /// <param name="message">The formatted event message, if available.</param>
    /// <returns>The parsed API version event.</returns>
    private static ApiVersionEvent ParseApiVersion(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: value, Error, HResult
        return new ApiVersionEvent
        {
            EventId = eventId,
            TimeCreated = record.TimeCreated?.ToLocalTime(),
            ProcessId = (int)(record.ProcessId ?? 0),
            ThreadId = (int)(record.ThreadId ?? 0),
            Level = GetLevel(record),
            Message = message,
            Value = GetUInt32(properties, 0),
            Error = GetString(properties, 1),
            HResult = GetInt32(properties, 2)
        };
    }

    /// <summary>
    /// Parses a cancel-current-operation result event.
    /// </summary>
    /// <param name="record">The source event record.</param>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="eventId">The WebAuthn event ID.</param>
    /// <param name="message">The formatted event message, if available.</param>
    /// <returns>The parsed cancel-current-operation event.</returns>
    private static CancelCurrentOperationEvent ParseCancelCurrentOperation(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: value (CancellationId), Error, HResult
        return new CancelCurrentOperationEvent
        {
            EventId = eventId,
            TimeCreated = record.TimeCreated?.ToLocalTime(),
            ProcessId = (int)(record.ProcessId ?? 0),
            ThreadId = (int)(record.ThreadId ?? 0),
            Level = GetLevel(record),
            Message = message,
            CancellationId = GetGuid(properties, 0),
            Error = GetString(properties, 1),
            HResult = GetInt32(properties, 2)
        };
    }

    /// <summary>
    /// Parses a generic name/value event.
    /// </summary>
    /// <param name="record">The source event record.</param>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="eventId">The WebAuthn event ID.</param>
    /// <param name="message">The formatted event message, if available.</param>
    /// <returns>The parsed name/value event.</returns>
    private static NameValueEvent ParseNameValue(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: Name, Value
        return new NameValueEvent
        {
            EventId = eventId,
            TimeCreated = record.TimeCreated?.ToLocalTime(),
            ProcessId = (int)(record.ProcessId ?? 0),
            ThreadId = (int)(record.ThreadId ?? 0),
            Level = GetLevel(record),
            Message = message,
            Name = GetString(properties, 0),
            Value = GetString(properties, 1)
        };
    }

    /// <summary>
    /// Parses a CTAP command started event.
    /// </summary>
    /// <param name="record">The source event record.</param>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="eventId">The WebAuthn event ID.</param>
    /// <param name="message">The formatted event message, if available.</param>
    /// <returns>The parsed CTAP command started event.</returns>
    private static CtapCommandStartedEvent ParseCtapCommandStarted(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: Command, TransactionId, Flags, TimeoutMilliseconds, TicketLength, Ticket, RequestLength, Request
        string? command = GetString(properties, 0);
        Guid? transactionId = GetGuid(properties, 1);
        int? flags = GetInt32(properties, 2);
        int? timeoutMilliseconds = GetInt32(properties, 3);
        byte[]? ticket = GetByteArray(properties, 5); // index 4 is TicketLength
        byte[]? request = GetByteArray(properties, 7); // index 6 is RequestLength

        if (string.Equals(command, AddPluginAuthenticatorCredentialsCommand, StringComparison.Ordinal))
        {
            AddPluginAuthenticatorCredentialsRequest? parsedRequest = AddPluginAuthenticatorCredentialsRequestParser.Parse(request);

            return new AddPluginAuthenticatorCredentialsEvent
            {
                EventId = eventId,
                TimeCreated = record.TimeCreated?.ToLocalTime(),
                ProcessId = (int)(record.ProcessId ?? 0),
                ThreadId = (int)(record.ThreadId ?? 0),
                Level = GetLevel(record),
                Message = message,
                Command = command,
                TransactionId = transactionId,
                Flags = flags,
                TimeoutMilliseconds = timeoutMilliseconds,
                Ticket = ticket,
                Request = request,
                FilterHybridTransport = parsedRequest?.FilterHybridTransport,
                Credentials = parsedRequest?.Credentials,
                PluginClassId = parsedRequest?.PluginClassId
            };
        }

        return new CtapCommandStartedEvent
        {
            EventId = eventId,
            TimeCreated = record.TimeCreated?.ToLocalTime(),
            ProcessId = (int)(record.ProcessId ?? 0),
            ThreadId = (int)(record.ThreadId ?? 0),
            Level = GetLevel(record),
            Message = message,
            Command = command,
            TransactionId = transactionId,
            Flags = flags,
            TimeoutMilliseconds = timeoutMilliseconds,
            Ticket = ticket,
            Request = request
        };
    }

    /// <summary>
    /// Parses a CTAP command completed event.
    /// </summary>
    /// <param name="record">The source event record.</param>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="eventId">The WebAuthn event ID.</param>
    /// <param name="message">The formatted event message, if available.</param>
    /// <returns>The parsed CTAP command completed event.</returns>
    private static CtapCommandCompletedEvent ParseCtapCommandCompleted(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: Command, TransactionId, ResponseLength, Response
        string? command = GetString(properties, 0);
        Guid? transactionId = GetGuid(properties, 1);
        byte[]? response = GetByteArray(properties, 3); // index 2 is ResponseLength

        if (string.Equals(command, GetAllPlatformCredentialsCommand, StringComparison.Ordinal))
        {
            return new GetAllPlatformCredentialsEvent
            {
                EventId = eventId,
                TimeCreated = record.TimeCreated?.ToLocalTime(),
                ProcessId = (int)(record.ProcessId ?? 0),
                ThreadId = (int)(record.ThreadId ?? 0),
                Level = GetLevel(record),
                Message = message,
                Command = command,
                TransactionId = transactionId,
                Response = response,
                Credentials = GetAllPlatformCredentialsResponseParser.Parse(response)
            };
        }

        return new CtapCommandCompletedEvent
        {
            EventId = eventId,
            TimeCreated = record.TimeCreated?.ToLocalTime(),
            ProcessId = (int)(record.ProcessId ?? 0),
            ThreadId = (int)(record.ThreadId ?? 0),
            Level = GetLevel(record),
            Message = message,
            Command = command,
            TransactionId = transactionId,
            Response = response
        };
    }

    /// <summary>
    /// Parses a CTAP command error event.
    /// </summary>
    /// <param name="record">The source event record.</param>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="eventId">The WebAuthn event ID.</param>
    /// <param name="message">The formatted event message, if available.</param>
    /// <returns>The parsed CTAP command error event.</returns>
    private static CtapCommandErrorEvent ParseCtapCommandError(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: Command, TransactionId, Error, Win32Error
        return new CtapCommandErrorEvent
        {
            EventId = eventId,
            TimeCreated = record.TimeCreated?.ToLocalTime(),
            ProcessId = (int)(record.ProcessId ?? 0),
            ThreadId = (int)(record.ThreadId ?? 0),
            Level = GetLevel(record),
            Message = message,
            Command = GetString(properties, 0),
            TransactionId = GetGuid(properties, 1),
            Error = GetString(properties, 2),
            HResult = GetInt32(properties, 3)
        };
    }

    /// <summary>
    /// Parses a function warning event.
    /// </summary>
    /// <param name="record">The source event record.</param>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="eventId">The WebAuthn event ID.</param>
    /// <param name="message">The formatted event message, if available.</param>
    /// <returns>The parsed function warning event.</returns>
    private static FunctionWarningEvent ParseFunctionWarning(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: Function, Location, Error, Win32Error
        return new FunctionWarningEvent
        {
            EventId = eventId,
            TimeCreated = record.TimeCreated?.ToLocalTime(),
            ProcessId = (int)(record.ProcessId ?? 0),
            ThreadId = (int)(record.ThreadId ?? 0),
            Level = GetLevel(record),
            Message = message,
            Function = GetString(properties, 0),
            Location = GetString(properties, 1),
            Error = GetString(properties, 2),
            HResult = GetInt32(properties, 3)
        };
    }

    /// <summary>
    /// Parses a BLE function warning event.
    /// </summary>
    /// <param name="record">The source event record.</param>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="eventId">The WebAuthn event ID.</param>
    /// <param name="message">The formatted event message, if available.</param>
    /// <returns>The parsed BLE function warning event.</returns>
    private static BleFunctionWarningEvent ParseBleFunctionWarning(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: Function, Location, Error, Win32Error
        return new BleFunctionWarningEvent
        {
            EventId = eventId,
            TimeCreated = record.TimeCreated?.ToLocalTime(),
            ProcessId = (int)(record.ProcessId ?? 0),
            ThreadId = (int)(record.ThreadId ?? 0),
            Level = GetLevel(record),
            Message = message,
            Function = GetString(properties, 0),
            Location = GetString(properties, 1),
            Error = GetString(properties, 2),
            HResult = GetInt32(properties, 3)
        };
    }

    #region Property extraction helpers

    /// <summary>
    /// Gets event payload properties from a record.
    /// </summary>
    /// <param name="record">The event record to inspect.</param>
    /// <returns>The event properties, or an empty collection when they cannot be read.</returns>
    private static IList<EventProperty> GetEventProperties(EventRecord record)
    {
        try
        {
            return record.Properties;
        }
        catch
        {
            return Array.Empty<EventProperty>();
        }
    }

    /// <summary>
    /// Gets a string property value.
    /// </summary>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="index">The property index.</param>
    /// <returns>The property converted to a string, or <see langword="null"/> when unavailable.</returns>
    private static string? GetString(IList<EventProperty> properties, int index)
    {
        if (index >= properties.Count) return null;
        var value = properties[index].Value;
        return value?.ToString();
    }

    /// <summary>
    /// Gets a signed 32-bit integer property value.
    /// </summary>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="index">The property index.</param>
    /// <returns>The property converted to an <see cref="int"/>, or <see langword="null"/> when unavailable.</returns>
    private static int? GetInt32(IList<EventProperty> properties, int index)
    {
        if (index >= properties.Count) return null;
        var value = properties[index].Value;

        return value switch
        {
            int i => i,
            uint u => unchecked((int)u),
            long l => (int)l,
            ulong ul => (int)ul,
            string s when int.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out int parsed) => parsed,
            _ => null
        };
    }

    /// <summary>
    /// Gets an unsigned 32-bit integer property value.
    /// </summary>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="index">The property index.</param>
    /// <returns>The property converted to a <see cref="uint"/>, or <see langword="null"/> when unavailable.</returns>
    private static uint? GetUInt32(IList<EventProperty> properties, int index)
    {
        if (index >= properties.Count) return null;
        var value = properties[index].Value;

        return value switch
        {
            uint u => u,
            int i => unchecked((uint)i),
            long l => (uint)l,
            ulong ul => (uint)ul,
            string s when uint.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out uint parsed) => parsed,
            _ => null
        };
    }

    /// <summary>
    /// Gets a byte property value.
    /// </summary>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="index">The property index.</param>
    /// <returns>The property converted to a <see cref="byte"/>, or <see langword="null"/> when unavailable.</returns>
    private static byte? GetByte(IList<EventProperty> properties, int index)
    {
        if (index >= properties.Count) return null;
        var value = properties[index].Value;

        return value switch
        {
            byte b => b,
            int i => (byte)i,
            uint u => (byte)u,
            long l => (byte)l,
            _ => null
        };
    }

    /// <summary>
    /// Gets a Boolean property value.
    /// </summary>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="index">The property index.</param>
    /// <returns>The property converted to a <see cref="bool"/>, or <see langword="null"/> when unavailable.</returns>
    private static bool? GetBoolean(IList<EventProperty> properties, int index)
    {
        if (index >= properties.Count) return null;
        var value = properties[index].Value;

        return value switch
        {
            bool b => b,
            string s => bool.TryParse(s, out bool parsed) ? parsed : null,
            int i => i != 0,
            _ => null
        };
    }

    /// <summary>
    /// Gets a GUID property value.
    /// </summary>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="index">The property index.</param>
    /// <returns>The property converted to a non-empty <see cref="Guid"/>, or <see langword="null"/> when unavailable.</returns>
    private static Guid? GetGuid(IList<EventProperty> properties, int index)
    {
        if (index >= properties.Count) return null;
        var value = properties[index].Value;

        return value switch
        {
            Guid g => g == Guid.Empty ? null : g,
            string s when Guid.TryParse(s, out Guid parsed) => parsed == Guid.Empty ? null : parsed,
            _ => null
        };
    }

    /// <summary>
    /// Gets a byte array property value.
    /// </summary>
    /// <param name="properties">The event payload properties.</param>
    /// <param name="index">The property index.</param>
    /// <returns>The property byte array, or <see langword="null"/> when unavailable or empty.</returns>
    private static byte[]? GetByteArray(IList<EventProperty> properties, int index)
    {
        if (index >= properties.Count) return null;
        var value = properties[index].Value;

        return value switch
        {
            byte[] bytes when bytes.Length > 0 => bytes,
            byte[] => null,
            _ => null
        };
    }

    #endregion
}
