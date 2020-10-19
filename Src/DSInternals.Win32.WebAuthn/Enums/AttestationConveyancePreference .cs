namespace DSInternals.Win32.WebAuthn
{
    internal enum AttestationConveyancePreference : int
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_CONVEYANCE_PREFERENCE_ANY.
        /// </remarks>
        Any = ApiConstants.AttestationConveyancePreferenceAny,

        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_CONVEYANCE_PREFERENCE_NONE.
        /// </remarks>
        None = ApiConstants.AttestationConveyancePreferenceNone,

        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_CONVEYANCE_PREFERENCE_INDIRECT.
        /// </remarks>
        Indirect = ApiConstants.AttestationConveyancePreferenceIndirect,

        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_CONVEYANCE_PREFERENCE_DIRECT.
        /// </remarks>
        Direct = ApiConstants.AttestationConveyancePreferenceDirect
    }
}
