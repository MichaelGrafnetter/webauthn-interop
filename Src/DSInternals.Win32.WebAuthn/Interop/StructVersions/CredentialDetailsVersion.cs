namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// Credential Details Structure Version Information.
    /// </summary>
    internal enum CredentialDetailtVersion : int
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_DETAILS_VERSION_1.
        /// </remarks>
        Version1 = ApiConstants.CredentialDetailsVersion1,

        /// <summary>
        /// Current version
        /// </summary>
        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_DETAILS_CURRENT_VERSION.
        /// </remarks>
#pragma warning disable CA1069 // Enums values should not be duplicated
        Current = ApiConstants.CredentialDetailsCurrentVersion
#pragma warning restore CA1069 // Enums values should not be duplicated
    }
}
