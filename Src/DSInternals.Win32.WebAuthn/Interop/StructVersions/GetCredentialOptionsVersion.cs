using Windows.Win32;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// The version of the WEBAUTHN_GET_CREDENTIALS_OPTIONS structure, to allow for modifications in the future.
    /// </summary>
    internal enum GetCredentialOptionsVersion : uint
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_GET_CREDENTIALS_OPTIONS_VERSION_1.
        /// </remarks>
        Version1 = PInvoke.WEBAUTHN_GET_CREDENTIALS_OPTIONS_VERSION_1,

        /// <remarks>
        /// Corresponds to WEBAUTHN_GET_CREDENTIALS_OPTIONS_CURRENT_VERSION.
        /// </remarks>
#pragma warning disable CA1069 // Enums values should not be duplicated
        Current = PInvoke.WEBAUTHN_GET_CREDENTIALS_OPTIONS_CURRENT_VERSION
#pragma warning restore CA1069 // Enums values should not be duplicated
    }
}
