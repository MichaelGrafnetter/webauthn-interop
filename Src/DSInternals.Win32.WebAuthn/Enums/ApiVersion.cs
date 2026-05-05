#pragma warning disable CA1008 // Enums should have zero value
#pragma warning disable CA1069 // Enums values should not be duplicated

using System.Diagnostics.CodeAnalysis;
using Windows.Win32;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// WebAuthn API Version Information.
/// </summary>
[SuppressMessage("Design", "CA1028:Enum Storage should be Int32", Justification = "The version is defined as a DWORD in the WebAuthn API.")]
public enum ApiVersion : uint
{
    /// <summary>
    /// Baseline Version
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_API_VERSION_1.</remarks>
    Version1 = PInvoke.WEBAUTHN_API_VERSION_1,

    /// <summary>
    /// Delta From V1.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_API_VERSION_2.</remarks>
    Version2 = PInvoke.WEBAUTHN_API_VERSION_2,

    /// <summary>
    /// Delta From V2.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_API_VERSION_3.</remarks>
    Version3 = PInvoke.WEBAUTHN_API_VERSION_3,

    /// <summary>
    /// Delta From V3.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_API_VERSION_4.</remarks>
    Version4 = PInvoke.WEBAUTHN_API_VERSION_4,

    /// <summary>
    /// Delta From V4.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_API_VERSION_5.</remarks>
    Version5 = PInvoke.WEBAUTHN_API_VERSION_5,

    /// <summary>
    /// Delta From V5.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_API_VERSION_6.</remarks>
    Version6 = PInvoke.WEBAUTHN_API_VERSION_6,

    /// <summary>
    /// Delta From V6.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_API_VERSION_7.</remarks>
    Version7 = PInvoke.WEBAUTHN_API_VERSION_7,

    /// <summary>
    /// Delta From V7.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_API_VERSION_8.</remarks>
    Version8 = PInvoke.WEBAUTHN_API_VERSION_8,

    /// <summary>
    /// Delta From V8.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_API_VERSION_9.</remarks>
    Version9 = PInvoke.WEBAUTHN_API_VERSION_9,

    /// <summary>
    /// Current Version
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_API_CURRENT_VERSION.</remarks>
    Current = PInvoke.WEBAUTHN_API_CURRENT_VERSION
}
