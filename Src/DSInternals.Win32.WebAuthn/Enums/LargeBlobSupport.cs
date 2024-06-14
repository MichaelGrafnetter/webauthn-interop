using Windows.Win32;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Large blob support options.
    /// </summary>
    public enum LargeBlobSupport :uint
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_LARGE_BLOB_SUPPORT_NONE.
        /// </remarks>
        None = PInvoke.WEBAUTHN_LARGE_BLOB_SUPPORT_NONE,

        /// <remarks>
        /// Corresponds to WEBAUTHN_LARGE_BLOB_SUPPORT_REQUIRED.
        /// </remarks>
        Required = PInvoke.WEBAUTHN_LARGE_BLOB_SUPPORT_REQUIRED,

        /// <remarks>
        /// Corresponds to WEBAUTHN_LARGE_BLOB_SUPPORT_PREFERRED.
        /// </remarks>
        Preferred = PInvoke.WEBAUTHN_LARGE_BLOB_SUPPORT_PREFERRED
    }
}
