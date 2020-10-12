using System;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Options.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal class AuthenticatorMakeCredentialOptions : IDisposable
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        // TODO: Configurable version
        private protected AuthenticatorMakeCredentialOptionsVersion Version;

        /// <summary>
        /// Time that the operation is expected to complete within.
        /// </summary>
        /// <remarks>This is used as guidance, and can be overridden by the platform.</remarks>
        // TODO: Constant for default timeout
        public int TimeoutMilliseconds = 60000;

        /// <summary>
        /// Credentials used for exclusion.
        /// </summary>
        public CredentialsIn ExcludeCredentials = new CredentialsIn(null);

        /// <summary>
        /// Extensions to parse when performing the operation. (Optional)
        /// </summary>
        public ExtensionsIn Extensions = new ExtensionsIn(null);

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
        private IntPtr _cancellationId = IntPtr.Zero;

        /// <summary>
        /// Exclude Credential List. 
        /// </summary>
        /// <remarks>
        /// If present, "CredentialList" will be ignored.
        /// This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_3.
        /// </remarks>
        private IntPtr _excludeCredentialsEx = IntPtr.Zero;

        public AuthenticatorMakeCredentialOptions()
        {
             Version = AuthenticatorMakeCredentialOptionsVersion.Current;
        }

        public Guid? CancellationId
        {
            set
            {
                if (value.HasValue)
                {
                    if(_cancellationId == IntPtr.Zero)
                    {
                        _cancellationId = Marshal.AllocHGlobal(Marshal.SizeOf<Guid>());
                    }

                    Marshal.StructureToPtr<Guid>(value.Value, _cancellationId, false);
                }
                else
                {
                    if(_cancellationId != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(_cancellationId);
                        _cancellationId = IntPtr.Zero;
                    }
                }
            }
        }

        public CredentialExListIn ExcludeCredentialsEx
        {
            set
            {
                if (value != null)
                {
                    if (_excludeCredentialsEx == IntPtr.Zero)
                    {
                        _excludeCredentialsEx = Marshal.AllocHGlobal(Marshal.SizeOf<CredentialExListIn>());
                    }

                    Marshal.StructureToPtr<CredentialExListIn>(value, _excludeCredentialsEx, false);
                }
                else
                {
                    if (_excludeCredentialsEx != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(_excludeCredentialsEx);
                        _excludeCredentialsEx = IntPtr.Zero;
                    }
                }
            }
        }

        public void Dispose()
        {
            if(Extensions != null)
            {
                Extensions.Dispose();
                Extensions = null;
            }

            if(ExcludeCredentials != null)
            {
                ExcludeCredentials.Dispose();
                ExcludeCredentials = null;
            }

            if(_excludeCredentialsEx != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_excludeCredentialsEx);
                _excludeCredentialsEx = IntPtr.Zero;
            }

            if (_cancellationId != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_cancellationId);
                _cancellationId = IntPtr.Zero;
            }
        }
    }
}
