namespace DSInternals.Win32.WebAuthn
{
    internal enum UserVerificationRequirement : int
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_USER_VERIFICATION_REQUIREMENT_ANY.
        /// </remarks>
        Any = ApiConstants.UserVerificationRequirementAny,

        /// <remarks>
        /// Corresponds to WEBAUTHN_USER_VERIFICATION_REQUIREMENT_REQUIRED.
        /// </remarks>
        Required = ApiConstants.UserVerificationRequirementRequired,

        /// <remarks>
        /// Corresponds to WEBAUTHN_USER_VERIFICATION_REQUIREMENT_PREFERRED.
        /// </remarks>
        Preferred = ApiConstants.UserVerificationRequirementPreferred,

        /// <remarks>
        /// Corresponds to WEBAUTHN_USER_VERIFICATION_REQUIREMENT_DISCOURAGED.
        /// </remarks>
        Discouraged = ApiConstants.UserVerificationRequirementDiscouraged
    }
}
