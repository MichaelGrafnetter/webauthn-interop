using Windows.Win32;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// User Information Structure Version Information.
    /// </summary>
    internal enum UserInformationVersion : uint
    {
        /// <summary>
        /// Current version
        /// </summary>
        /// <remarks>
        /// Corresponds to WEBAUTHN_USER_ENTITY_INFORMATION_CURRENT_VERSION.
        /// </remarks>
        Current = PInvoke.WEBAUTHN_USER_ENTITY_INFORMATION_CURRENT_VERSION
    }
}
