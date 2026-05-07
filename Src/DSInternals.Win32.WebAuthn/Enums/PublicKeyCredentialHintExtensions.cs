using System;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Extension methods for known public key credential hints.
/// </summary>
public static class PublicKeyCredentialHintExtensions
{
    /// <summary>
    /// Converts a known credential hint enum value to the DOMString value accepted by WebAuthn.
    /// </summary>
    public static string? ToJsonString(this PublicKeyCredentialHint hint) => hint switch
    {
        PublicKeyCredentialHint.None => null,
        PublicKeyCredentialHint.SecurityKey => ApiConstants.CredentialHintSecurityKey,
        PublicKeyCredentialHint.ClientDevice => ApiConstants.CredentialHintClientDevice,
        PublicKeyCredentialHint.Hybrid => ApiConstants.CredentialHintHybrid,
        _ => throw new ArgumentOutOfRangeException(nameof(hint), hint, "Unknown public key credential hint.")
    };
}
