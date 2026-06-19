using System;
using System.Collections.Generic;

namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents a GetAssertion CTAP command completed event (Event ID 2102).
/// </summary>
public sealed class GetAssertionEvent : CtapCommandCompletedEvent
{
    /// <summary>
    /// Status value from the platform response wrapper.
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// CTAP status byte from the nested authenticator response.
    /// </summary>
    public int? CtapStatus { get; set; }

    /// <summary>
    /// SHA-256 hash of the relying party ID from shared authenticator data.
    /// </summary>
    public byte[]? RpIdHash { get; set; }

    /// <summary>
    /// Authenticator data flags from shared authenticator data.
    /// </summary>
    public AuthenticatorFlags? AuthenticatorFlags { get; set; }

    /// <summary>
    /// The CTAP provider type, such as Hid.
    /// </summary>
    public string? ProviderType { get; set; }

    /// <summary>
    /// The name of the CTAP provider.
    /// </summary>
    public string? ProviderName { get; set; }

    /// <summary>
    /// The manufacturer of the authenticator device.
    /// </summary>
    public string? Manufacturer { get; set; }

    /// <summary>
    /// The product name of the authenticator device.
    /// </summary>
    public string? Product { get; set; }

    /// <summary>
    /// Authenticator Attestation GUID identifying the authenticator model.
    /// </summary>
    public Guid? AAGuid { get; set; }

    /// <summary>
    /// Indicates whether the assertion is for a third-party payment credential.
    /// </summary>
    public bool? ThirdPartyPayment { get; set; }

    /// <summary>
    /// Applicable authenticator transports.
    /// </summary>
    public AuthenticatorTransport? Transports { get; set; }

    /// <summary>
    /// Credentials returned by the CTAP GetAssertion response.
    /// </summary>
    public IReadOnlyList<CredentialDetails>? Credentials { get; set; }
}
