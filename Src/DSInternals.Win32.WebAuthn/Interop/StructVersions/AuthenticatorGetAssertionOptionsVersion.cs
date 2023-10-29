namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// The version of the WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS structure, to allow for modifications in the future.
    /// </summary>
    internal enum AuthenticatorGetAssertionOptionsVersion : int
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_1.
        /// </remarks>
        Version1 = ApiConstants.AuthenticatorGetAssertionOptionsVersion1,

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_2.
        /// </remarks>
        Version2 = ApiConstants.AuthenticatorGetAssertionOptionsVersion2,

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_3.
        /// </remarks>
        Version3 = ApiConstants.AuthenticatorGetAssertionOptionsVersion3,

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_4.
        /// </remarks>
        Version4 = ApiConstants.AuthenticatorGetAssertionOptionsVersion4,

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_5.
        /// </remarks>
        Version5 = ApiConstants.AuthenticatorGetAssertionOptionsVersion5,

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_6.
        /// </remarks>
        Version6 = ApiConstants.AuthenticatorGetAssertionOptionsVersion6,

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_7.
        /// </remarks>
        Version7 = ApiConstants.AuthenticatorGetAssertionOptionsVersion7,

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_CURRENT_VERSION.
        /// </remarks>
#pragma warning disable CA1069 // Enums values should not be duplicated
        Current = ApiConstants.AuthenticatorGetAssertionOptionsCurrentVersion
#pragma warning restore CA1069 // Enums values should not be duplicated
    }
}
