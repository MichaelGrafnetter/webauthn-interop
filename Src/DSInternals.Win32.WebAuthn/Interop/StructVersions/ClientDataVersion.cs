namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// Version of the WEBAUTHN_CLIENT_DATA structure, to allow for modifications in the future.
    /// </summary>
    internal enum ClientDataVersion : int
    {
        /// <remarks>Corresponds to WEBAUTHN_CLIENT_DATA_CURRENT_VERSION.</remarks>
        Current = ApiConstants.ClientDataCurrentVersion
    }
}
