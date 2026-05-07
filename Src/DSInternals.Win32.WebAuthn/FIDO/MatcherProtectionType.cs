using System;

namespace DSInternals.Win32.WebAuthn.FIDO;

/// <summary>
/// Methods used by the authenticator to protect the matcher that performs user verification, as reported in the WebAuthn uvm extension.
/// </summary>
/// <see href="https://fidoalliance.org/specs/fido-v2.0-id-20180227/fido-registry-v2.0-id-20180227.html#matcher-protection-types"/>
[Flags]
public enum MatcherProtectionType : ushort
{
    /// <summary>
    /// No matcher protection type is reported.
    /// </summary>
    None = 0x0000,

    /// <summary>
    /// Software-based matcher protection (MATCHER_PROTECTION_SOFTWARE).
    /// </summary>
    Software = 0x0001,

    /// <summary>
    /// Trusted Execution Environment (MATCHER_PROTECTION_TEE).
    /// </summary>
    TEE = 0x0002,

    /// <summary>
    /// On-chip matcher protection (MATCHER_PROTECTION_ON_CHIP).
    /// </summary>
    OnChip = 0x0004
}
