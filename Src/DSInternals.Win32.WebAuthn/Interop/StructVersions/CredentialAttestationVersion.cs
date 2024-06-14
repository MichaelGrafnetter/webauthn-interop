using Windows.Win32;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// The version of the WEBAUTHN_CREDENTIAL_ATTESTATION structure, to allow for modifications in the future.
    /// </summary>
    internal enum CredentialAttestationVersion : uint
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_1.
        /// </remarks>
        Version1 = PInvoke.WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_1,

        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_2.
        /// </remarks>
        Version2 = PInvoke.WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_2,

        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_3.
        /// </remarks>
        Version3 = PInvoke.WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_3,

        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_4.
        /// </remarks>
        Version4 = PInvoke.WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_4,

        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_5.
        /// </remarks>
        Version5 = PInvoke.WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_5,

        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_6.
        /// </remarks>
        Version6 = PInvoke.WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_6,

        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_ATTESTATION_CURRENT_VERSION.
        /// </remarks>
#pragma warning disable CA1069 // Enums values should not be duplicated
        Current = PInvoke.WEBAUTHN_CREDENTIAL_ATTESTATION_CURRENT_VERSION
#pragma warning restore CA1069 // Enums values should not be duplicated
    }
}
