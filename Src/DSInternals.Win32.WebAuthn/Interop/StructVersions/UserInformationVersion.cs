namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// User Information Structure Version Information.
    /// </summary>
    internal enum UserInformationVersion : int
    {
        /// <summary>
        /// Current version
        /// </summary>
        /// <remarks>
        /// Corresponds to WEBAUTHN_USER_ENTITY_INFORMATION_CURRENT_VERSION.
        /// </remarks>
        Current = ApiConstants.UserEntityInformationCurrentVersion
    }
}
