namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// The version of the WEBAUTHN_GET_CREDENTIALS_OPTIONS structure, to allow for modifications in the future.
    /// </summary>
    internal enum GetCredentialOptionsVersion : int
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_GET_CREDENTIALS_OPTIONS_VERSION_1.
        /// </remarks>
        Version1 = ApiConstants.GetCredentialsOptionsVersion1,

        /// <remarks>
        /// Corresponds to WEBAUTHN_GET_CREDENTIALS_OPTIONS_CURRENT_VERSION.
        /// </remarks>
#pragma warning disable CA1069 // Enums values should not be duplicated
        Current = ApiConstants.GetCredentialsOptionsCurrentVersion
#pragma warning restore CA1069 // Enums values should not be duplicated
    }
}
