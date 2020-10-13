namespace WebAuthN.Interop
{
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
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_CURRENT_VERSION.
        /// </remarks>
        Current = ApiConstants.AuthenticatorGetAssertionOptionsCurrentVersion
    }
}
