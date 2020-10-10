using System;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Options.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
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
        public CredentialsIn AllowedCredentials;

        /// <summary>
        /// Extensions to parse when performing the operation. (Optional)
        /// </summary>
        public ExtensionsIn Extensions;

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
        private string _u2fAppId;

        // If the following is non-NULL, then, set to TRUE if the above U2fAppid was used instead of RpId
        private bool[] _isU2fAppIdUsed;

        //
        // The following fields have been added in WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_3
        //

        /// <summary>
        /// Cancellation Id (Optional)
        /// </summary>
        private Guid[] _cancellationId;

        //
        // The following fields have been added in WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_4
        //

        /// <summary>
        /// Allow Credential List. If present, "CredentialList" will be ignored.
        /// </summary>
        public CredentialExListIn[] AllowCredentialList;

        public string U2fAppId
        {
            get
            {
                return _u2fAppId;
            }
            set
            {
                _u2fAppId = value;
                _isU2fAppIdUsed = value != null ? new bool[] { true } : null;
            }
        }

        public Guid? CancellationId
        {
            get
            {
                return _cancellationId?[0];
            }
            set
            {
                _cancellationId = value.HasValue ? new Guid[] { value.Value } : null;
            }
        }
    }
}
