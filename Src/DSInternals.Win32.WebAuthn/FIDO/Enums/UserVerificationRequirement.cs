using System.Runtime.Serialization;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn.FIDO
{
    /// <summary>
    /// This enumeration describes the Relying Party's requirements regarding user verification for the create() operation.
    /// Eligible authenticators are filtered to only those capable of satisfying this requirement.
    /// </summary>
    public enum UserVerificationRequirement : int
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_USER_VERIFICATION_REQUIREMENT_ANY.
        /// </remarks>
        [EnumMember(Value = "NULL")]
        Any = ApiConstants.UserVerificationRequirementAny,

        /// <summary>
        /// This value indicates that the Relying Party requires user verification for the operation
        /// and will fail the operation if the response does not have the UV flag set.
        /// </summary>
        /// <remarks>
        /// Corresponds to WEBAUTHN_USER_VERIFICATION_REQUIREMENT_REQUIRED.
        /// </remarks>
        [EnumMember(Value = "required")]
        Required = ApiConstants.UserVerificationRequirementRequired,

        /// <summary>
        /// This value indicates that the Relying Party prefers user verification for the operation if possible,
        /// but will not fail the operation if the response does not have the UV flag set.
        /// </summary>
        /// <remarks>
        /// Corresponds to WEBAUTHN_USER_VERIFICATION_REQUIREMENT_PREFERRED.
        /// </remarks>
        [EnumMember(Value = "preferred")]
        Preferred = ApiConstants.UserVerificationRequirementPreferred,

        /// <summary>
        /// This value indicates that the Relying Party does not want user verification employed during the operation
        /// (e.g., in the interest of minimizing disruption to the user interaction flow).
        /// </summary>
        /// <remarks>
        /// Corresponds to WEBAUTHN_USER_VERIFICATION_REQUIREMENT_DISCOURAGED.
        /// </remarks>
        [EnumMember(Value = "discouraged")]
        Discouraged = ApiConstants.UserVerificationRequirementDiscouraged
    }
}
