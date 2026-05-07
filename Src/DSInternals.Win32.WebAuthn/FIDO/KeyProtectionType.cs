using System;

namespace DSInternals.Win32.WebAuthn.FIDO;

/// <summary>
/// Methods used by the authenticator to protect FIDO registration private key material, as reported in the WebAuthn uvm extension.
/// </summary>
/// <see href="https://fidoalliance.org/specs/fido-v2.0-id-20180227/fido-registry-v2.0-id-20180227.html#key-protection-types"/>
[Flags]
public enum KeyProtectionType : ushort
{
    /// <summary>
    /// No key protection type is reported.
    /// </summary>
    None = 0x0000,

    /// <summary>
    /// Software-based key protection (KEY_PROTECTION_SOFTWARE).
    /// </summary>
    Software = 0x0001,

    /// <summary>
    /// Hardware-based key protection (KEY_PROTECTION_HARDWARE).
    /// </summary>
    Hardware = 0x0002,

    /// <summary>
    /// Trusted Execution Environment (KEY_PROTECTION_TEE).
    /// </summary>
    TEE = 0x0004,

    /// <summary>
    /// Secure Element (KEY_PROTECTION_SECURE_ELEMENT).
    /// </summary>
    SecureElement = 0x0008,

    /// <summary>
    /// Remote handle key protection (KEY_PROTECTION_REMOTE_HANDLE).
    /// </summary>
    RemoteHandle = 0x0010
}
