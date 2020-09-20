namespace WebAuthN.Interop
{
    internal enum CredentialAttestationVersion
    {
        /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_1.</remarks>
        Version1 = 1,

        /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_2.</remarks>
        Version2 = 2,

        /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_3.</remarks>
        Version3 = 3,

        /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_ATTESTATION_CURRENT_VERSION.</remarks>
        Current = Version3
    }
}
