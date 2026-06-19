using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Aggregates raw WebAuthN events into operation models grouped by transaction ID.
/// </summary>
public static class WebAuthnOperationBuilder
{
    /// <summary>
    /// CTAP command name for obtaining assertions.
    /// </summary>
    private const string GetAssertionCommand = "GetAssertion";

    /// <summary>
    /// Builds aggregated WebAuthn operations from a list of raw WebAuthN events.
    /// </summary>
    /// <param name="events">The raw WebAuthn events to aggregate.</param>
    /// <returns>The aggregated WebAuthn operations sorted by descending start time.</returns>
    public static IReadOnlyList<WebAuthnOperation> Build(IEnumerable<WebAuthnEvent> events)
    {
        var operations = new List<WebAuthnOperation>();

        // Group events by TransactionId (skip events without one)
        var groups = events
            .Where(e => e.TransactionId.HasValue)
            .GroupBy(e => e.TransactionId!.Value);

        foreach (var group in groups)
        {
            var eventList = group.ToList();
            var transactionId = group.Key;
            var pluginCredentialEnumerationEvents = eventList.OfType<AddPluginAuthenticatorCredentialsEvent>();
            var platformCredentialEnumerationEvents = eventList.OfType<GetAllPlatformCredentialsEvent>();
            var getAssertionCredentialEvents = eventList.OfType<GetAssertionEvent>();

            // Determine if this is an attestation (MakeCredential) or assertion (GetAssertion) transaction
            bool hasAttestation = eventList.Any(e =>
                e.EventId == WebAuthnEventId.MakeCredentialStarted ||
                e.EventId == WebAuthnEventId.CborMakeCredentialRequest);

            bool hasAssertion = eventList.Any(e =>
                e.EventId == WebAuthnEventId.GetAssertionStarted ||
                e.EventId == WebAuthnEventId.CborGetAssertionRequest);

            foreach (var pluginCredentialEnumerationEvent in pluginCredentialEnumerationEvents)
            {
                operations.AddRange(BuildCredentialEnumerations(
                    transactionId,
                    eventList,
                    pluginCredentialEnumerationEvent,
                    pluginCredentialEnumerationEvent.Credentials,
                    pluginCredentialEnumerationEvent.PluginClassId,
                    pluginCredentialEnumerationEvent.FilterHybridTransport));
            }

            foreach (var platformCredentialEnumerationEvent in platformCredentialEnumerationEvents)
            {
                operations.AddRange(BuildCredentialEnumerations(
                    transactionId,
                    eventList,
                    platformCredentialEnumerationEvent,
                    platformCredentialEnumerationEvent.Credentials,
                    pluginClassId: null,
                    filterHybridTransport: null));
            }

            foreach (var getAssertionCredentialEvent in getAssertionCredentialEvents)
            {
                operations.AddRange(BuildCredentialEnumerations(
                    transactionId,
                    eventList,
                    getAssertionCredentialEvent,
                    getAssertionCredentialEvent.Credentials,
                    pluginClassId: null,
                    filterHybridTransport: null));
            }

            if (hasAttestation)
            {
                // Only include successful attestations
                bool hasAttestationError = eventList.Any(e => e.EventId == WebAuthnEventId.MakeCredentialError);
                bool hasAttestationSuccess = eventList.Any(e => e.EventId == WebAuthnEventId.MakeCredentialCompleted);

                if (hasAttestationSuccess && !hasAttestationError)
                {
                    operations.Add(BuildAttestation(transactionId, eventList));
                }
            }

            if (hasAssertion)
            {
                // Only include successful assertions
                bool hasAssertionError =
                    eventList.Any(e => e.EventId == WebAuthnEventId.GetAssertionError) ||
                    eventList.OfType<CtapCommandErrorEvent>().Any(e => string.Equals(e.Command, GetAssertionCommand, StringComparison.Ordinal));
                bool hasAssertionSuccess = eventList.Any(e => e.EventId == WebAuthnEventId.GetAssertionCompleted);

                if (hasAssertionSuccess && !hasAssertionError)
                {
                    operations.Add(BuildAssertion(transactionId, eventList));
                }
            }
        }

        return operations.OrderByDescending(o => o.TimeStarted ?? DateTime.MinValue).ToList();
    }

    /// <summary>
    /// Builds a registration operation from events sharing a transaction ID.
    /// </summary>
    /// <param name="transactionId">The transaction ID shared by the events.</param>
    /// <param name="events">The events in the transaction group.</param>
    /// <returns>The aggregated registration operation.</returns>
    private static WebAuthnAttestationOperation BuildAttestation(Guid transactionId, List<WebAuthnEvent> events)
    {
        var started = events.FirstOrDefault(e => e.EventId == WebAuthnEventId.MakeCredentialStarted);
        var completed = events.FirstOrDefault(e => e.EventId == WebAuthnEventId.MakeCredentialCompleted);
        var request = events.OfType<MakeCredentialRequestEvent>().FirstOrDefault();
        var response = events.OfType<MakeCredentialResponseEvent>().FirstOrDefault();
        var deviceInfo = events.OfType<DeviceInfoEvent>().FirstOrDefault();
        var usbDevice = events.OfType<UsbDeviceCompletedEvent>().FirstOrDefault();

        // Get device info from either DeviceInfoEvent or UsbDeviceCompletedEvent
        string? providerName = deviceInfo?.ProviderName;
        string? devicePath = deviceInfo?.DevicePath ?? usbDevice?.DevicePath;
        string? manufacturer = deviceInfo?.Manufacturer ?? usbDevice?.Manufacturer;
        string? product = deviceInfo?.Product ?? usbDevice?.Product;
        Guid? aaGuid = response?.AAGuid ?? deviceInfo?.AAGuid ?? usbDevice?.AAGuid;
        bool? u2fProtocol = deviceInfo?.U2fProtocol ?? usbDevice?.U2fProtocol;
        ParseVidPid(devicePath, out int? vendorId, out int? productId);
        CtapMakeCredentialRequestParser.ParseUserInfo(request?.Request, out string? userName, out string? userDisplayName);
        DateTime? endTime = completed?.TimeCreated ?? events.Where(e => e.TimeCreated.HasValue).Max(e => e.TimeCreated);

        return new WebAuthnAttestationOperation
        {
            TransactionId = transactionId,
            TimeStarted = started?.TimeCreated,
            TimeCompleted = endTime,
            ProcessId = started?.ProcessId ?? events.First().ProcessId,
            RpId = request?.RpId,
            RpIdHash = response?.RpIdHash,
            Flags = response?.AuthenticatorFlags,
            SignCount = response?.SignCount,
            CredentialId = response?.CredentialId,
            CredentialCount = request?.CredentialCount,
            AAGuid = aaGuid,
            ProviderName = providerName,
            DevicePath = devicePath,
            Manufacturer = manufacturer,
            Product = product,
            U2fProtocol = u2fProtocol,
            VendorId = vendorId,
            ProductId = productId,
            UserId = request?.UserId,
            UserName = userName,
            UserDisplayName = userDisplayName,
            RequireResidentKey = request?.RequireResidentKey,
            CredentialParameterCount = request?.CredentialParameterCount,
            AttestationFormatType = response?.AttestationFormatType,
            ClientDataHashAlgId = request?.ClientDataHashAlgId,
            U2fPublicKey = response?.U2fPublicKey,
            PublicKey = response?.PublicKey
        };
    }

    /// <summary>
    /// Builds an authentication operation from events sharing a transaction ID.
    /// </summary>
    /// <param name="transactionId">The transaction ID shared by the events.</param>
    /// <param name="events">The events in the transaction group.</param>
    /// <returns>The aggregated authentication operation.</returns>
    private static WebAuthnAssertionOperation BuildAssertion(Guid transactionId, List<WebAuthnEvent> events)
    {
        var started = events.FirstOrDefault(e => e.EventId == WebAuthnEventId.GetAssertionStarted);
        var completed = events.FirstOrDefault(e => e.EventId == WebAuthnEventId.GetAssertionCompleted);
        var request = events.OfType<GetAssertionRequestEvent>().FirstOrDefault();
        var response = events.OfType<GetAssertionResponseEvent>().FirstOrDefault();
        var deviceInfo = events.OfType<DeviceInfoEvent>().FirstOrDefault();
        var usbDevice = events.OfType<UsbDeviceCompletedEvent>().FirstOrDefault();

        // Get device info from either DeviceInfoEvent or UsbDeviceCompletedEvent
        string? providerName = deviceInfo?.ProviderName;
        string? devicePath = deviceInfo?.DevicePath ?? usbDevice?.DevicePath;
        string? manufacturer = deviceInfo?.Manufacturer ?? usbDevice?.Manufacturer;
        string? product = deviceInfo?.Product ?? usbDevice?.Product;
        Guid? aaGuid = deviceInfo?.AAGuid ?? usbDevice?.AAGuid;
        bool? u2fProtocol = deviceInfo?.U2fProtocol ?? usbDevice?.U2fProtocol;
        ParseVidPid(devicePath, out int? vendorId, out int? productId);
        DateTime? endTime = completed?.TimeCreated ?? events.Where(e => e.TimeCreated.HasValue).Max(e => e.TimeCreated);

        return new WebAuthnAssertionOperation
        {
            TransactionId = transactionId,
            TimeStarted = started?.TimeCreated,
            TimeCompleted = endTime,
            ProcessId = started?.ProcessId ?? events.First().ProcessId,
            RpId = request?.RpId,
            RpIdHash = response?.RpIdHash,
            Flags = response?.AuthenticatorFlags,
            SignCount = response?.SignCount,
            CredentialId = response?.CredentialId,
            CredentialCount = request?.CredentialCount,
            AAGuid = aaGuid,
            ProviderName = providerName,
            DevicePath = devicePath,
            Manufacturer = manufacturer,
            Product = product,
            U2fProtocol = u2fProtocol,
            VendorId = vendorId,
            ProductId = productId,
            ClientDataHashAlgId = request?.ClientDataHashAlgId
        };
    }

    /// <summary>
    /// Builds one credential enumeration operation for each credential in the event.
    /// </summary>
    /// <param name="transactionId">The transaction ID shared by the events.</param>
    /// <param name="events">The events in the transaction group.</param>
    /// <param name="enumerationEvent">The credential enumeration event.</param>
    /// <returns>The per-credential enumeration operations.</returns>
    private static IEnumerable<CredentialEnumeration> BuildCredentialEnumerations(
        Guid transactionId,
        List<WebAuthnEvent> events,
        WebAuthnEvent enumerationEvent,
        IReadOnlyList<CredentialDetails>? credentials,
        Guid? pluginClassId,
        bool? filterHybridTransport)
    {
        if (credentials == null || credentials.Count == 0)
        {
            yield break;
        }

        DateTime? endTime = GetLastEventTime(events) ?? enumerationEvent.TimeCreated;
        int credentialCount = credentials.Count;

        foreach (var credential in credentials)
        {
            yield return new CredentialEnumeration
            {
                TransactionId = transactionId,
                TimeStarted = enumerationEvent.TimeCreated,
                TimeCompleted = endTime,
                ProcessId = enumerationEvent.ProcessId,
                RpId = credential.RelyingPartyInformation?.Id,
                CredentialId = credential.CredentialId,
                CredentialCount = credentialCount,
                UserName = credential.UserInformation?.Name,
                PluginClassId = pluginClassId,
                FilterHybridTransport = filterHybridTransport,
                RelyingPartyName = credential.RelyingPartyInformation?.Name,
                UserId = credential.UserInformation?.Id,
                UserDisplayName = credential.UserInformation?.DisplayName,
                Removable = credential.Removable,
                BackedUp = credential.BackedUp,
                AuthenticatorName = credential.AuthenticatorName,
                AuthenticatorLogo = credential.AuthenticatorLogo,
                ThirdPartyPayment = credential.ThirdPartyPayment,
                Transports = credential.Transports
            };
        }
    }

    /// <summary>
    /// Regex used to parse USB vendor IDs from device paths.
    /// </summary>
    private static readonly Regex VidRegex = new(@"vid_([0-9a-fA-F]{4})", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    /// <summary>
    /// Regex used to parse USB product IDs from device paths.
    /// </summary>
    private static readonly Regex PidRegex = new(@"pid_([0-9a-fA-F]{4})", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    /// <summary>
    /// Gets the last event time in a transaction group.
    /// </summary>
    /// <param name="events">The events to inspect.</param>
    /// <returns>The latest event timestamp, or <see langword="null"/> when no event has a timestamp.</returns>
    private static DateTime? GetLastEventTime(IEnumerable<WebAuthnEvent> events)
    {
        DateTime? latest = null;

        foreach (var evt in events)
        {
            if (evt.TimeCreated.HasValue && (!latest.HasValue || evt.TimeCreated > latest))
            {
                latest = evt.TimeCreated;
            }
        }

        return latest;
    }

    /// <summary>
    /// Parses USB vendor and product IDs from a device path.
    /// </summary>
    /// <param name="devicePath">The authenticator device path.</param>
    /// <param name="vendorId">The parsed USB vendor ID, if present.</param>
    /// <param name="productId">The parsed USB product ID, if present.</param>
    private static void ParseVidPid(string? devicePath, out int? vendorId, out int? productId)
    {
        vendorId = null;
        productId = null;

        if (string.IsNullOrEmpty(devicePath))
            return;

        var vidMatch = VidRegex.Match(devicePath);
        if (vidMatch.Success && int.TryParse(vidMatch.Groups[1].Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int vid))
        {
            vendorId = vid;
        }

        var pidMatch = PidRegex.Match(devicePath);
        if (pidMatch.Success && int.TryParse(pidMatch.Groups[1].Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int pid))
        {
            productId = pid;
        }
    }
}
