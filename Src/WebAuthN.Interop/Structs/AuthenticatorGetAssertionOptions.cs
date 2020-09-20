using System;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Options.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal class AuthenticatorGetAssertionOptions
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private protected AuthenticatorGetAssertionOptionsVersion Version = AuthenticatorGetAssertionOptionsVersion.Current;

        /// <summary>
        /// Time that the operation is expected to complete within.
        /// </summary>
        /// <remarks>This is used as guidance, and can be overridden by the platform.</remarks>
        public int TimeoutMilliseconds;

        /// <summary>
        /// Allowed Credentials List.
        /// </summary>
        public VariableArray<Credential> AllowedCredentials;

        /// <summary>
        /// Extensions to parse when performing the operation. (Optional)
        /// </summary>
        public Extensions Extensions;

        /// <summary>
        /// Platform vs Cross-Platform Authenticators. (Optional)
        /// </summary>
        public AuthenticatorAttachment AuthenticatorAttachment;

        /// <summary>
        /// User Verification Requirement.
        /// </summary>
        public UserVerificationRequirement UserVerificationRequirement;

        /// <summary>
        /// Reserved for future Use.
        /// </summary>
        private uint Flags = 0;

        // The following fields have been added in WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_2

        /// <summary>
        /// Optional identifier for the U2F AppId. Converted to UTF8 before being hashed. Not lower cased.
        /// </summary>
        string U2fAppId;

        // If the following is non-NULL, then, set to TRUE if the above U2fAppid was used instead of RpId
        // TODO: *bool
        IntPtr IsU2fAppIdUsed;

        //
        // The following fields have been added in WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_3
        //

        /// <summary>
        /// Cancellation Id (Optional)
        /// </summary>
        // TODO: *Guid
        IntPtr CancellationId;

        //
        // The following fields have been added in WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_4
        //

        /// <summary>
        /// Allow Credential List. If present, "CredentialList" will be ignored.
        /// </summary>
        VariableArray<Credential> AllowCredentialList;
    }
}
