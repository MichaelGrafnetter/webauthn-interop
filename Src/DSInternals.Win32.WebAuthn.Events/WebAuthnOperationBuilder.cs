using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Aggregates raw WebAuthN events into <see cref="WebAuthnAttestationOperation"/> and <see cref="WebAuthnAssertionOperation"/> operations
/// grouped by transaction ID.
/// </summary>
public static class WebAuthnOperationBuilder
{
    /// <summary>
    /// Builds aggregated WebAuthn operations from a list of raw WebAuthN events.
    /// </summary>
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

            // Determine if this is an attestation (MakeCredential) or assertion (GetAssertion) transaction
            bool hasAttestation = eventList.Any(e =>
                e.EventId == WebAuthnEventId.MakeCredentialStarted ||
                e.EventId == WebAuthnEventId.CborMakeCredentialRequest);

            bool hasAssertion = eventList.Any(e =>
                e.EventId == WebAuthnEventId.GetAssertionStarted ||
                e.EventId == WebAuthnEventId.CborGetAssertionRequest);

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
                bool hasAssertionError = eventList.Any(e => e.EventId == WebAuthnEventId.GetAssertionError);
                bool hasAssertionSuccess = eventList.Any(e => e.EventId == WebAuthnEventId.GetAssertionCompleted);

                if (hasAssertionSuccess && !hasAssertionError)
                {
                    operations.Add(BuildAssertion(transactionId, eventList));
                }
            }
        }

        return operations.OrderByDescending(o => o.TimeStarted ?? DateTime.MinValue).ToList();
    }

    private static WebAuthnAttestationOperation BuildAttestation(Guid transactionId, List<WebAuthnEvent> events)
    {
        var started = events.FirstOrDefault(e => e.EventId == WebAuthnEventId.MakeCredentialStarted);
        var completed = events.FirstOrDefault(e => e.EventId == WebAuthnEventId.MakeCredentialCompleted);
        var request = events.OfType<MakeCredentialRequestEvent>().FirstOrDefault();
        var response = events.OfType<MakeCredentialResponseEvent>().FirstOrDefault();
        var deviceInfo = events.OfType<DeviceInfoEvent>().FirstOrDefault();
        var usbDevice = events.OfType<UsbDeviceEvent>().FirstOrDefault(e => e.AAGuid.HasValue);

        // Get device info from either DeviceInfoEvent or UsbDeviceEvent
        string? providerName = deviceInfo?.ProviderName;
        string? devicePath = deviceInfo?.DevicePath ?? usbDevice?.DevicePath;
        string? manufacturer = deviceInfo?.Manufacturer ?? usbDevice?.Manufacturer;
        string? product = deviceInfo?.Product ?? usbDevice?.Product;
        Guid? aaGuid = response?.AAGuid ?? deviceInfo?.AAGuid ?? usbDevice?.AAGuid;
        bool? u2fProtocol = deviceInfo?.U2fProtocol ?? usbDevice?.U2fProtocol;
        ParseVidPid(devicePath, out int? vendorId, out int? productId);
        CtapMakeCredentialRequestParser.ParseUserInfo(request?.Request, out string? userName, out string? userDisplayName);
        DateTime? endTime = events.Where(e => e.TimeCreated.HasValue).Max(e => e.TimeCreated);

        return new WebAuthnAttestationOperation
        {
            TransactionId = transactionId,
            TimeStarted = started?.TimeCreated,
            EndTime = endTime,
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

    private static WebAuthnAssertionOperation BuildAssertion(Guid transactionId, List<WebAuthnEvent> events)
    {
        var started = events.FirstOrDefault(e => e.EventId == WebAuthnEventId.GetAssertionStarted);
        var completed = events.FirstOrDefault(e => e.EventId == WebAuthnEventId.GetAssertionCompleted);
        var request = events.OfType<GetAssertionRequestEvent>().FirstOrDefault();
        var response = events.OfType<GetAssertionResponseEvent>().FirstOrDefault();
        var deviceInfo = events.OfType<DeviceInfoEvent>().FirstOrDefault();
        var usbDevice = events.OfType<UsbDeviceEvent>().FirstOrDefault(e => e.AAGuid.HasValue);

        // Get device info from either DeviceInfoEvent or UsbDeviceEvent
        string? providerName = deviceInfo?.ProviderName;
        string? devicePath = deviceInfo?.DevicePath ?? usbDevice?.DevicePath;
        string? manufacturer = deviceInfo?.Manufacturer ?? usbDevice?.Manufacturer;
        string? product = deviceInfo?.Product ?? usbDevice?.Product;
        Guid? aaGuid = deviceInfo?.AAGuid ?? usbDevice?.AAGuid;
        bool? u2fProtocol = deviceInfo?.U2fProtocol ?? usbDevice?.U2fProtocol;
        ParseVidPid(devicePath, out int? vendorId, out int? productId);
        DateTime? endTime = events.Where(e => e.TimeCreated.HasValue).Max(e => e.TimeCreated);

        return new WebAuthnAssertionOperation
        {
            TransactionId = transactionId,
            TimeStarted = started?.TimeCreated,
            EndTime = endTime,
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

    private static readonly Regex VidRegex = new(@"vid_([0-9a-fA-F]{4})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private static readonly Regex PidRegex = new(@"pid_([0-9a-fA-F]{4})", RegexOptions.Compiled | RegexOptions.IgnoreCase);

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
