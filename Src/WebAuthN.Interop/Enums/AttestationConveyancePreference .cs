namespace WebAuthN.Interop
{
    internal enum AttestationConveyancePreference : int
    {
        /// <remarks>Corresponds to WEBAUTHN_ATTESTATION_CONVEYANCE_PREFERENCE_ANY.</remarks>
        Any = 0,

        /// <remarks>Corresponds to WEBAUTHN_ATTESTATION_CONVEYANCE_PREFERENCE_NONE.</remarks>
        None = 1,

        /// <remarks>Corresponds to WEBAUTHN_ATTESTATION_CONVEYANCE_PREFERENCE_INDIRECT.</remarks>
        Indirect = 2,

        /// <remarks>Corresponds to WEBAUTHN_ATTESTATION_CONVEYANCE_PREFERENCE_DIRECT.</remarks>
        Direct = 3
    }
}
