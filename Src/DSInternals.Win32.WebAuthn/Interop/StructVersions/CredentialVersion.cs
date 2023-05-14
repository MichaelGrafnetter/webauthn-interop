namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// The current version of the WEBAUTHN_CREDENTIAL structure, to allow for modifications in the future.
    /// </summary>
    internal enum CredentialVersion
    {
        /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_CURRENT_VERSION.</remarks>
        Current = ApiConstants.CredentialCurrentVersion
    }
}
