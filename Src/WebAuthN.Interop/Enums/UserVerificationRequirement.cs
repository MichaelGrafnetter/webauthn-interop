namespace WebAuthN.Interop
{
    internal enum UserVerificationRequirement : int
    {
        /// <remarks>Corresponds to WEBAUTHN_USER_VERIFICATION_REQUIREMENT_ANY.</remarks>
        Any = 0,

        /// <remarks>Corresponds to WEBAUTHN_USER_VERIFICATION_REQUIREMENT_REQUIRED.</remarks>
        Required = 1,

        /// <remarks>Corresponds to WEBAUTHN_USER_VERIFICATION_REQUIREMENT_PREFERRED.</remarks>
        Preferred = 2,

        /// <remarks>Corresponds to WEBAUTHN_USER_VERIFICATION_REQUIREMENT_DISCOURAGED.</remarks>
        Discouraged = 3
    }
}
