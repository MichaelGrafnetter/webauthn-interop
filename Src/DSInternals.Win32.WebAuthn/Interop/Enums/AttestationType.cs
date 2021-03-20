namespace DSInternals.Win32.WebAuthn.Interop
{
    internal enum AttestationDecode : int
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_DECODE_NONE.
        /// </remarks>
        None = ApiConstants.AttestationDecodeNone,

        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_DECODE_COMMON.
        /// </remarks>
        Common = ApiConstants.AttestationDecodeCommon
    }
}
