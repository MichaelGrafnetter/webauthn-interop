using System;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Options.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct MakeCredentialOptions
    {
        // TODO: Add enum for WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_CURRENT_VERSION.</remarks>
        private const int CurrentVersion = 3;

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
        /// Credentials used for exclusion.
        /// </summary>
        public CredentialList ExcludeCredentials;

        /// <summary>
        /// Extensions to parse when performing the operation. (Optional)
        /// </summary>
        public ExtensionlList Extensions;

        // TODO: Create enum WEBAUTHN_AUTHENTICATOR_ATTACHMENT
        /// <summary>
        /// Platform vs Cross-Platform Authenticators. (Optional)
        /// </summary>
        public int AuthenticatorAttachment;

        /// <summary>
        /// Require key to be resident or not. Defaulting to false.
        /// </summary>
        public bool RequireResidentKey;

        // TODO: Create enum WEBAUTHN_USER_VERIFICATION.

        /// <summary>
        /// User Verification Requirement.
        /// </summary>
        public int UserVerificationRequirement;

        // TODO: Create enum WEBAUTHN_ATTESTATION_CONVEYANCE.

        /// <summary>
        /// Attestation Conveyance Preference.
        /// </summary>
        public int AttestationConveyancePreference;

        /// <summary>
        /// Reserved for future Use
        /// </summary>
        public int Flags;

        // TODO: Change to Guid
        /// <summary>
        /// Cancellation Id (Optional)
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_2.</remarks>
        public IntPtr CancellationId;

        /// <summary>
        /// Exclude Credential List. 
        /// </summary>
        /// <remarks>
        /// If present, "CredentialList" will be ignored.
        /// This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_3.
        /// </remarks>
        public CredentialExList ExcludeCredentialsEx;
    }
}
