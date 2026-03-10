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
    private const string LogName = "Microsoft-Windows-WebAuthN/Operational";

    /// <summary>
    /// Reads all events from the local WebAuthN event log.
    /// </summary>
    public static IReadOnlyList<WebAuthnEvent> ReadEvents(int? maxEvents = null)
    {
        return ReadEvents(LogName, PathType.LogName, maxEvents);
    }

    /// <summary>
    /// Reads events from a saved .evtx file.
    /// </summary>
    public static IReadOnlyList<WebAuthnEvent> ReadEventsFromFile(string filePath, int? maxEvents = null)
    {
        return ReadEvents(filePath, PathType.FilePath, maxEvents);
    }

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
            WebAuthnEventId.CborMakeCredentialRequest =>
                ParseMakeCredentialRequest(record, properties, eventId, message),

            WebAuthnEventId.CborMakeCredentialResponse or WebAuthnEventId.NgcMakeCredentialResponse =>
                ParseMakeCredentialResponse(record, properties, eventId, message),

            WebAuthnEventId.CborGetAssertionRequest =>
                ParseGetAssertionRequest(record, properties, eventId, message),

            WebAuthnEventId.CborGetAssertionResponse =>
                ParseGetAssertionResponse(record, properties, eventId, message),

            WebAuthnEventId.DeviceInfo =>
                ParseDeviceInfo(record, properties, eventId, message),

            WebAuthnEventId.UsbDeviceCompleted =>
                ParseUsbDeviceCompleted(record, properties, eventId, message),

            WebAuthnEventId.UsbDeviceStarted or WebAuthnEventId.UsbAddDevice =>
                ParseUsbDeviceStarted(record, properties, eventId, message),

            WebAuthnEventId.IsUserVerifyingPlatformAuthenticatorAvailable or WebAuthnEventId.ApiVersion =>
                ParseApiInfo(record, properties, eventId, message),

            WebAuthnEventId.NameValue =>
                ParseNameValue(record, properties, eventId, message),

            WebAuthnEventId.CtapCommandStarted =>
                ParseCtapCommandStarted(record, properties, eventId, message),

            WebAuthnEventId.CtapCommandCompleted =>
                ParseCtapCommandCompleted(record, properties, eventId, message),

            WebAuthnEventId.CtapCommandError =>
                ParseCtapCommandError(record, properties, eventId, message),

            WebAuthnEventId.FunctionWarning or WebAuthnEventId.BleFunctionWarning =>
                ParseFunctionWarning(record, properties, eventId, message),

            _ => ParseBaseEvent(record, properties, eventId, message)
        };
    }

    private static StandardEventLevel? GetLevel(EventRecord record)
    {
        return record.Level.HasValue ? (StandardEventLevel)record.Level.Value : null;
    }

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
            case WebAuthnEventId.MakeCredentialCompleted:
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

    private static MakeCredentialResponseEvent ParseMakeCredentialResponse(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: TransactionId, AttestationFormatType, RpIdHashLength, RpIdHash, Flags, SignCount, AAGuid, CredentialIdLength, CredentialId, U2fPublicKey, PublicKeyLength, PublicKey, ResponseLength, Response
        return new MakeCredentialResponseEvent
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
            AuthenticatorFlags = GetByte(properties, 4),
            SignCount = GetUInt32(properties, 5),
            AAGuid = GetGuid(properties, 6),
            CredentialId = GetByteArray(properties, 8), // index 7 is CredentialIdLength
            U2fPublicKey = GetByteArray(properties, 9),
            PublicKey = GetByteArray(properties, 11), // index 10 is PublicKeyLength
            Response = GetByteArray(properties, 13) // index 12 is ResponseLength
        };
    }

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
            AuthenticatorFlags = GetByte(properties, 3),
            SignCount = GetUInt32(properties, 4),
            CredentialId = GetByteArray(properties, 6), // index 5 is CredentialIdLength
            Response = GetByteArray(properties, 8) // index 7 is ResponseLength
        };
    }

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

    private static UsbDeviceEvent ParseUsbDeviceCompleted(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: TransactionId, DevicePath, Manufacturer, Product, AAGuid, U2fProtocol
        return new UsbDeviceEvent
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

    private static UsbDeviceEvent ParseUsbDeviceStarted(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: TransactionId, DevicePath, Manufacturer, Product
        return new UsbDeviceEvent
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

    private static ApiInfoEvent ParseApiInfo(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: value, Error, HResult
        return new ApiInfoEvent
        {
            EventId = eventId,
            TimeCreated = record.TimeCreated?.ToLocalTime(),
            ProcessId = (int)(record.ProcessId ?? 0),
            ThreadId = (int)(record.ThreadId ?? 0),
            Level = GetLevel(record),
            Message = message,
            Value = GetString(properties, 0),
            Error = GetString(properties, 1),
            HResult = GetInt32(properties, 2)
        };
    }

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

    private static CtapCommandEvent ParseCtapCommandStarted(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: Command, TransactionId, Flags, TimeoutMilliseconds, TicketLength, Ticket, RequestLength, Request
        return new CtapCommandEvent
        {
            EventId = eventId,
            TimeCreated = record.TimeCreated?.ToLocalTime(),
            ProcessId = (int)(record.ProcessId ?? 0),
            ThreadId = (int)(record.ThreadId ?? 0),
            Level = GetLevel(record),
            Message = message,
            Command = GetInt32(properties, 0),
            TransactionId = GetGuid(properties, 1),
            Flags = GetInt32(properties, 2),
            TimeoutMilliseconds = GetInt32(properties, 3),
            Ticket = GetByteArray(properties, 5), // index 4 is TicketLength
            Request = GetByteArray(properties, 7) // index 6 is RequestLength
        };
    }

    private static CtapCommandEvent ParseCtapCommandCompleted(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: Command, TransactionId, ResponseLength, Response
        return new CtapCommandEvent
        {
            EventId = eventId,
            TimeCreated = record.TimeCreated?.ToLocalTime(),
            ProcessId = (int)(record.ProcessId ?? 0),
            ThreadId = (int)(record.ThreadId ?? 0),
            Level = GetLevel(record),
            Message = message,
            Command = GetInt32(properties, 0),
            TransactionId = GetGuid(properties, 1),
            Response = GetByteArray(properties, 3) // index 2 is ResponseLength
        };
    }

    private static CtapCommandEvent ParseCtapCommandError(EventRecord record, IList<EventProperty> properties, WebAuthnEventId eventId, string? message)
    {
        // Fields: Command, TransactionId, Error, Win32Error
        return new CtapCommandEvent
        {
            EventId = eventId,
            TimeCreated = record.TimeCreated?.ToLocalTime(),
            ProcessId = (int)(record.ProcessId ?? 0),
            ThreadId = (int)(record.ThreadId ?? 0),
            Level = GetLevel(record),
            Message = message,
            Command = GetInt32(properties, 0),
            TransactionId = GetGuid(properties, 1),
            Error = GetString(properties, 2),
            HResult = GetInt32(properties, 3)
        };
    }

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

    #region Property extraction helpers

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

    private static string? GetString(IList<EventProperty> properties, int index)
    {
        if (index >= properties.Count) return null;
        var value = properties[index].Value;
        return value?.ToString();
    }

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
