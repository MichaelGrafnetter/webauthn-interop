using System;

namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Base class for aggregated WebAuthn operations, combining data from multiple related events
/// grouped by transaction ID.
/// </summary>
public abstract class WebAuthnOperation
{
    /// <summary>
    /// The type of operation: Attestation (registration) or Assertion (authentication).
    /// </summary>
    public abstract string OperationType { get; }

    /// <summary>
    /// The transaction ID correlating all events in this operation.
    /// </summary>
    public Guid TransactionId { get; set; }

    /// <summary>
    /// When the operation started.
    /// </summary>
    public DateTime? TimeStarted { get; set; }

    /// <summary>
    /// Timestamp of the last event with this correlation/transaction ID.
    /// </summary>
    public DateTime? TimeCompleted { get; set; }

    /// <summary>
    /// The process ID that initiated the WebAuthN operation (from the service).
    /// </summary>
    public int ProcessId { get; set; }

    /// <summary>
    /// The relying party ID.
    /// </summary>
    public string? RpId { get; set; }

    /// <summary>
    /// SHA-256 hash of the relying party ID.
    /// </summary>
    public byte[]? RpIdHash { get; set; }

    /// <summary>
    /// Authenticator data flags (raw byte).
    /// </summary>
    public AuthenticatorFlags? Flags { get; set; }

    /// <summary>
    /// User Present (UP) flag - user presence test completed successfully.
    /// </summary>
    public bool? UserPresent => Flags?.HasFlag(AuthenticatorFlags.UserPresent);

    /// <summary>
    /// User Verified (UV) flag - user verification completed successfully.
    /// </summary>
    public bool? UserVerified => Flags?.HasFlag(AuthenticatorFlags.UserVerified);

    /// <summary>
    /// Attested Credential Data (AT) flag - authenticator data includes attested credential data.
    /// </summary>
    public bool? AttestationData => Flags?.HasFlag(AuthenticatorFlags.AttestationData);

    /// <summary>
    /// Extension Data (ED) flag - authenticator data includes extension data.
    /// </summary>
    public bool? ExtensionData => Flags?.HasFlag(AuthenticatorFlags.ExtensionData);

    /// <summary>
    /// Signature counter.
    /// </summary>
    public uint? SignCount { get; set; }

    /// <summary>
    /// The credential ID.
    /// </summary>
    public byte[]? CredentialId { get; set; }

    /// <summary>
    /// Number of credentials involved in the request.
    /// </summary>
    public int? CredentialCount { get; set; }

    /// <summary>
    /// Authenticator Attestation GUID identifying the authenticator model.
    /// </summary>
    public Guid? AAGuid { get; set; }

    /// <summary>
    /// The name of the CTAP provider (e.g., MicrosoftPlatformProvider).
    /// </summary>
    public string? ProviderName { get; set; }

    /// <summary>
    /// The device path of the authenticator.
    /// </summary>
    public string? DevicePath { get; set; }

    /// <summary>
    /// The manufacturer of the authenticator device.
    /// </summary>
    public string? Manufacturer { get; set; }

    /// <summary>
    /// The product name of the authenticator device.
    /// </summary>
    public string? Product { get; set; }

    /// <summary>
    /// Whether the authenticator uses the U2F protocol.
    /// </summary>
    public bool? U2fProtocol { get; set; }

    /// <summary>
    /// USB Vendor ID parsed from the device path (e.g., 0x1050 for Yubico).
    /// </summary>
    public int? VendorId { get; set; }

    /// <summary>
    /// USB Product ID parsed from the device path.
    /// </summary>
    public int? ProductId { get; set; }

    /// <summary>
    /// The user name associated with the operation.
    /// Populated from the CTAP2 CBOR request for registration; null for authentication.
    /// </summary>
    public string? UserName { get; set; }

}
