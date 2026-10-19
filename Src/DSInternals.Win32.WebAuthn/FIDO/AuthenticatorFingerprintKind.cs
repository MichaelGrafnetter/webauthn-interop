using System.Diagnostics.CodeAnalysis;

namespace DSInternals.Win32.WebAuthn.FIDO
{
    /// <summary>Identifies how an authenticator's fingerprint sensor captures a fingerprint.</summary>
    /// <remarks>The nonzero values correspond to fingerprintKind in the authenticatorBioEnrollment response.</remarks>
    /// <seealso href="https://fidoalliance.org/specs/fido-v2.2-rd-20230321/fido-client-to-authenticator-protocol-v2.2-rd-20230321.html#getFingerprintSensorInfo">CTAP2 fingerprint sensor information</seealso>
    [SuppressMessage("Design", "CA1028:Enum Storage should be Int32", Justification = "Matches the native DWORD sensor kind.")]
    public enum AuthenticatorFingerprintKind : uint
    {
        /// <summary>No kind was reported.</summary>
        /// <remarks>A Windows management sentinel, not a CTAP2 fingerprintKind value.</remarks>
        Unavailable = 0,

        /// <summary>The fingerprint sensor captures a stationary finger.</summary>
        Touch = 1,

        /// <summary>The fingerprint sensor captures a swiping finger.</summary>
        Swipe = 2
    }
}
