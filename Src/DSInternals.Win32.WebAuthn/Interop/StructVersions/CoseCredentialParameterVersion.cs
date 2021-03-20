namespace DSInternals.Win32.WebAuthn.Interop
{
    internal enum CoseCredentialParameterVersion : int
    {
        /// <summary>
        /// Current version
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_CREDENTIAL_PARAMETER_CURRENT_VERSION.</remarks>
        Current = ApiConstants.CoseCredentialParameterCurrentVersion
    }
}
