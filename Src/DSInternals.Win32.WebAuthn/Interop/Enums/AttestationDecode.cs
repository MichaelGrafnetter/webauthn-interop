namespace DSInternals.Win32.WebAuthn.Interop
{
    internal enum AttestationDecode : int
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_DECODE_NONE.
        /// </remarks>
        None = ApiConstants.AttestationDecodeNone,

        /// <summary>
        /// Supports the packed and fido-u2f format types.
        /// </summary>
        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_DECODE_COMMON.
        /// </remarks>
        Common = ApiConstants.AttestationDecodeCommon
    }
}
