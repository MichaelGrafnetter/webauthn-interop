using System;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Information about an authenticator.
/// </summary>
/// <remarks>Corresponds to WEBAUTHN_AUTHENTICATOR_DETAILS.</remarks>
public sealed class AuthenticatorDetails
{
    /// <summary>
    /// The Authenticator Attestation GUID (AAGUID) identifying the model of the authenticator.
    /// </summary>
    public Guid? AaGuid { get; set; }

    /// <summary>
    /// The authenticator name.
    /// </summary>
    public string? AuthenticatorName { get; set; }

    /// <summary>
    /// Authenticator logo (expected to be in SVG format).
    /// </summary>
    public string? AuthenticatorLogo { get; set; }

    /// <summary>
    /// Is the authenticator currently locked?
    /// When locked, this authenticator's credentials might not be present or updated in WebAuthNGetPlatformCredentialList.
    /// </summary>
    public bool Locked { get; set; }
}
