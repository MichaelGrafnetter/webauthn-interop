using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn.FIDO
{
    /// <summary>
    /// Large blob support options.
    /// </summary>
    public enum LargeBlobSupport
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_LARGE_BLOB_SUPPORT_NONE.
        /// </remarks>
        LargeBlobSupportNone = ApiConstants.LargeBlobSupportNone,

        /// <remarks>
        /// Corresponds to WEBAUTHN_LARGE_BLOB_SUPPORT_REQUIRED.
        /// </remarks>
        Required = ApiConstants.LargeBlobSupportRequired,

        /// <remarks>
        /// Corresponds to WEBAUTHN_LARGE_BLOB_SUPPORT_PREFERRED.
        /// </remarks>
        Preferred = ApiConstants.LargeBlobSupportPreferred
    }
}
