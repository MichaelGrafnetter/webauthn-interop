namespace WebAuthN.Interop
{
    internal enum AuthenticatorMakeCredentialOptionsVersion
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_1.
        /// </remarks>
        Version1 = ApiConstants.AuthenticatorMakeCredentialOptionsVersion1,

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_2.
        /// </remarks>
        Version2 = ApiConstants.AuthenticatorMakeCredentialOptionsVersion2,

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_3.
        /// </remarks>
        Version3 = ApiConstants.AuthenticatorMakeCredentialOptionsVersion3,

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_CURRENT_VERSION.
        /// </remarks>
        Current = ApiConstants.AuthenticatorMakeCredentialOptionsCurrentVersion
    }
}
