namespace WebAuthN.Interop
{
    internal enum UserVerification : int
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_USER_VERIFICATION_ANY.
        /// </remarks>
        Any = ApiConstants.UserVerificationAny,

        /// <summary>
        /// This reflects "FIDO_2_0" semantics. In this configuration, user verification is optional with or without credentialID list. This is the default state of the credential if the extension is not specified and the authenticator does not report a defaultCredProtect value in the authenticatorGetInfo response.
        /// </summary>
        /// <remarks>
        /// Corresponds to WEBAUTHN_USER_VERIFICATION_OPTIONAL.
        /// </remarks>
        Optional = ApiConstants.UserVerificationOptional,

        /// <summary>
        /// In this configuration, credential is discovered only when its credentialID is provided by the platform or when user verification is performed.
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_USER_VERIFICATION_OPTIONAL_WITH_CREDENTIAL_ID_LIST.</remarks>
        OptionalWithCredentialIDList = ApiConstants.UserVerificationOptionalWithCredentialIdList,

        /// <summary>
        /// This reflects that discovery and usage of the credential MUST be preceeded by user verification.
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_USER_VERIFICATION_REQUIRED.</remarks>
        Required = ApiConstants.UserVerificationRequired
    }
}
