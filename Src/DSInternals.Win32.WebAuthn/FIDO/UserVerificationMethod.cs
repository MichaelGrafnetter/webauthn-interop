using System;

namespace DSInternals.Win32.WebAuthn.FIDO;

/// <summary>
/// User verification methods reported by the authenticator in the WebAuthn uvm extension.
/// </summary>
/// <see href="https://fidoalliance.org/specs/fido-v2.0-id-20180227/fido-registry-v2.0-id-20180227.html#user-verification-methods"/>
[Flags]
public enum UserVerificationMethod : uint
{
    /// <summary>
    /// No user verification method is reported.
    /// </summary>
    None = 0x00000000,

    /// <summary>
    /// Test of user presence (USER_VERIFY_PRESENCE).
    /// </summary>
    Presence = 0x00000001,

    /// <summary>
    /// Fingerprint check (USER_VERIFY_FINGERPRINT).
    /// </summary>
    Fingerprint = 0x00000002,

    /// <summary>
    /// PIN or passcode (USER_VERIFY_PASSCODE).
    /// </summary>
    Passcode = 0x00000004,

    /// <summary>
    /// Voiceprint (USER_VERIFY_VOICEPRINT).
    /// </summary>
    Voiceprint = 0x00000008,

    /// <summary>
    /// Faceprint (USER_VERIFY_FACEPRINT).
    /// </summary>
    Faceprint = 0x00000010,

    /// <summary>
    /// Geolocation (USER_VERIFY_LOCATION).
    /// </summary>
    Location = 0x00000020,

    /// <summary>
    /// Eyeprint / iris (USER_VERIFY_EYEPRINT).
    /// </summary>
    Eyeprint = 0x00000040,

    /// <summary>
    /// Pattern (USER_VERIFY_PATTERN).
    /// </summary>
    Pattern = 0x00000080,

    /// <summary>
    /// Handprint (USER_VERIFY_HANDPRINT).
    /// </summary>
    Handprint = 0x00000100,

    /// <summary>
    /// Silent / no verification performed (USER_VERIFY_NONE).
    /// </summary>
    NoneVerified = 0x00000200,

    /// <summary>
    /// All registered user verification methods are required (USER_VERIFY_ALL).
    /// </summary>
    All = 0x00000400
}
