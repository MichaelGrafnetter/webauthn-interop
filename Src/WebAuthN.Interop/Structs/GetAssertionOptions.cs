using System;
using System.Runtime.InteropServices;

// TODO: Create enum WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION.
/*
 * #define WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_1          1
#define WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_2          2
#define WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_3          3
#define WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_4          4
#define WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_CURRENT_VERSION    WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_4
*/

namespace WebAuthN.Interop
{
    /// <summary>
    /// Options.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    public class GetAssertionOptions
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        public int Version;

        /// <summary>
        /// Time that the operation is expected to complete within.
        /// </summary>
        /// <remarks>This is used as guidance, and can be overridden by the platform.</remarks>
        public int TimeoutMilliseconds;

        /// <summary>
        /// Allowed Credentials List.
        /// </summary>
        public CredentialList AllowedCredentials;

        /// <summary>
        /// Extensions to parse when performing the operation. (Optional)
        /// </summary>
        public ExtensionlList Extensions;

        /// <summary>
        /// Platform vs Cross-Platform Authenticators. (Optional)
        /// </summary>
        public int AuthenticatorAttachment;

        /// <summary>
        /// User Verification Requirement.
        /// </summary>
        public uint UserVerificationRequirement;

        /// <summary>
        /// Reserved for future Use.
        /// </summary>
        uint Flags = 0;

        // The following fields have been added in WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_2

        /// <summary>
        /// Optional identifier for the U2F AppId. Converted to UTF8 before being hashed. Not lower cased.
        /// </summary>
        string U2fAppId;

        // If the following is non-NULL, then, set to TRUE if the above U2fAppid was used instead of RpId
        IntPtr IsU2fAppIdUsed; //*bool

        //
        // The following fields have been added in WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_3
        //

        /// <summary>
        /// Cancellation Id (Optional)
        /// </summary>
        IntPtr CancellationId; //*Guid

        //
        // The following fields have been added in WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_4
        //

        /// <summary>
        /// Allow Credential List. If present, "CredentialList" will be ignored.
        /// </summary>
        CredentialList AllowCredentialList;
    }
}
