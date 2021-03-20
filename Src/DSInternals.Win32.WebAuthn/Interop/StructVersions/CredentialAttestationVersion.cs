namespace DSInternals.Win32.WebAuthn.Interop
{
    internal enum CredentialAttestationVersion
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_1.
        /// </remarks>
        Version1 = ApiConstants.CredentialAttestationVersion1,

        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_2.
        /// </remarks>
        Version2 = ApiConstants.CredentialAttestationVersion2,

        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_3.
        /// </remarks>
        Version3 = ApiConstants.CredentialAttestationVersion3,

        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_ATTESTATION_CURRENT_VERSION.
        /// </remarks>
#pragma warning disable CA1069 // Enums values should not be duplicated
        Current = ApiConstants.CredentialAttestationCurrentVersion
#pragma warning restore CA1069 // Enums values should not be duplicated
    }
}
