using System;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Options.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal class AuthenticatorMakeCredentialOptions
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private protected AuthenticatorMakeCredentialOptionsVersion Version = AuthenticatorMakeCredentialOptionsVersion.Current;

        /// <summary>
        /// Time that the operation is expected to complete within.
        /// </summary>
        /// <remarks>This is used as guidance, and can be overridden by the platform.</remarks>
        // TODO: Constant for default timeout
        public int TimeoutMilliseconds = 60000;

        /// <summary>
        /// Credentials used for exclusion.
        /// </summary>
        public CredentialsIn ExcludeCredentials;

        /// <summary>
        /// Extensions to parse when performing the operation. (Optional)
        /// </summary>
        public ExtensionsIn Extensions;

        /// <summary>
        /// Platform vs Cross-Platform Authenticators. (Optional)
        /// </summary>
        public AuthenticatorAttachment AuthenticatorAttachment;

        /// <summary>
        /// Require key to be resident or not. Defaulting to false.
        /// </summary>
        public bool RequireResidentKey;

        /// <summary>
        /// User Verification Requirement.
        /// </summary>
        public UserVerificationRequirement UserVerificationRequirement;

        /// <summary>
        /// Attestation Conveyance Preference.
        /// </summary>
        public AttestationConveyancePreference AttestationConveyancePreference;

        /// <summary>
        /// Reserved for future Use
        /// </summary>
        private int Flags = 0;

        /// <summary>
        /// Cancellation Id (Optional)
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_2.</remarks>
        private Guid[] _cancellationId;

        /// <summary>
        /// Exclude Credential List. 
        /// </summary>
        /// <remarks>
        /// If present, "CredentialList" will be ignored.
        /// This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_3.
        /// </remarks>
        private CredentialExListIn[] _excludeCredentialsEx;

        public Guid? CancellationId
        {
            get => _cancellationId?[0];
            set => _cancellationId = value.HasValue ? new Guid[] { value.Value } : null;
        }

        public CredentialExListIn ExcludeCredentialsEx
        {
            get => _excludeCredentialsEx?[0];
            set => _excludeCredentialsEx = value != null ? new CredentialExListIn[] { value } : null;
        }
    }
}
