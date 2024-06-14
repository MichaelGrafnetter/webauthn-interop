using Windows.Win32;

namespace DSInternals.Win32.WebAuthn.Interop
{
    internal enum AttestationDecode : uint
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_DECODE_NONE.
        /// </remarks>
        None = PInvoke.WEBAUTHN_ATTESTATION_DECODE_NONE,

        /// <summary>
        /// Supports the packed and fido-u2f format types.
        /// </summary>
        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_DECODE_COMMON.
        /// </remarks>
        Common = PInvoke.WEBAUTHN_ATTESTATION_DECODE_COMMON
    }
}
