using System;
using System.Diagnostics.CodeAnalysis;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Authenticator data flags
/// </summary>
/// <see>https://www.w3.org/TR/webauthn/#flags</see>
[Flags]
[SuppressMessage("Naming", "CA1711:Identifiers should not have incorrect suffix", Justification = "AuthenticatorFlags is the standard WebAuthn term.")]
[SuppressMessage("Design", "CA1028:Enum Storage should be Int32", Justification = "The flags are defined as a byte in the WebAuthn specification.")]
public enum AuthenticatorFlags : byte
{
    /// <summary>
    /// User Present (UP) - Bit 0.
    /// </summary>
    UserPresent = 0x01,

    /// <summary>
    /// User Verified (UV) - Bit 2.
    /// </summary>
    UserVerified = 0x04,

    /// <summary>
    /// Backup Eligibility (BE) - Bit 3.
    /// </summary>
    BackupEligible = 0x08,

    /// <summary>
    /// Backup State (BS) - Bit 4.
    /// </summary>
    BackedUp = 0x10,

    /// <summary>
    /// Attested Credential Data (AT) - Bit 6.
    /// </summary>
    AttestationData = 0x40,

    /// <summary>
    /// Extension Data (ED) - Bit 7.
    /// </summary>
    ExtensionData = 0x80
}
