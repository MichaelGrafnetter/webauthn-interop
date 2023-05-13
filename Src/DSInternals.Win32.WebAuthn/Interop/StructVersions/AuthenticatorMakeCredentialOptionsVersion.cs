namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// The version of the WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS structure, to allow for modifications in the future.
    /// </summary>
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
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_4.
        /// </remarks>
        Version4 = ApiConstants.AuthenticatorMakeCredentialOptionsVersion4,

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_5.
        /// </remarks>
        Version5 = ApiConstants.AuthenticatorMakeCredentialOptionsVersion5,

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_6.
        /// </remarks>
        Version6 = ApiConstants.AuthenticatorMakeCredentialOptionsVersion6,

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_CURRENT_VERSION.
        /// </remarks>
#pragma warning disable CA1069 // Enums values should not be duplicated
        Current = ApiConstants.AuthenticatorMakeCredentialOptionsCurrentVersion
#pragma warning restore CA1069 // Enums values should not be duplicated
    }
}
