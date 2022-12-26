namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// The status of the large blob operation.
    /// </summary>
    internal enum CredentialLargeBlobStatus : int
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_CRED_LARGE_BLOB_STATUS_NONE.
        /// </remarks>
        None = ApiConstants.CredLargeBlobStatusNone,

        /// <remarks>
        /// Corresponds to WEBAUTHN_CRED_LARGE_BLOB_STATUS_SUCCESS.
        /// </remarks>
        Success = ApiConstants.CredLargeBlobStatusSuccess,

        /// <remarks>
        /// Corresponds to WEBAUTHN_CRED_LARGE_BLOB_STATUS_NOT_SUPPORTED.
        /// </remarks>
        NotSupported = ApiConstants.CredLargeBlobStatusNotSupported,

        /// <remarks>
        /// Corresponds to WEBAUTHN_CRED_LARGE_BLOB_STATUS_INVALID_DATA.
        /// </remarks>
        InvalidData = ApiConstants.CredLargeBlobStatusInvalidData,

        /// <remarks>
        /// Corresponds to WEBAUTHN_CRED_LARGE_BLOB_STATUS_INVALID_PARAMETER.
        /// </remarks>
        InvalidParameter = ApiConstants.CredLargeBlobStatusInvalidParameter,

        /// <remarks>
        /// Corresponds to WEBAUTHN_CRED_LARGE_BLOB_STATUS_NOT_FOUND.
        /// </remarks>
        NotFound = ApiConstants.CredLargeBlobStatusNotFound,

        /// <remarks>
        /// Corresponds to WEBAUTHN_CRED_LARGE_BLOB_STATUS_MULTIPLE_CREDENTIALS.
        /// </remarks>
        MultipleCredentials = ApiConstants.CredLargeBlobStatusMultipleCredentials,

        /// <remarks>
        /// Corresponds to WEBAUTHN_CRED_LARGE_BLOB_STATUS_LACK_OF_SPACE.
        /// </remarks>
        LackOfSpace = ApiConstants.CredLargeBlobStatusLackOfSpace,

        /// <remarks>
        /// Corresponds to WEBAUTHN_CRED_LARGE_BLOB_STATUS_PLATFORM_ERROR.
        /// </remarks>
        PlatformError = ApiConstants.CredLargeBlobStatusPlatformError,

        /// <remarks>
        /// Corresponds to WEBAUTHN_CRED_LARGE_BLOB_STATUS_AUTHENTICATOR_ERROR.
        /// </remarks>
        AuthenticatorError = ApiConstants.CredLargeBlobStatusAuthenticatorError
    }
}
