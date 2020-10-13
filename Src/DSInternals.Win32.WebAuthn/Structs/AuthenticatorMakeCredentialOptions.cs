using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn
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
        public int TimeoutMilliseconds { get; set; } = ApiConstants.DefaultTimeoutMilliseconds;

        /// <summary>
        /// Credentials used for exclusion.
        /// </summary>
        private Credentials _excludeCredentials;

        /// <summary>
        /// Extensions to parse when performing the operation. (Optional)
        /// </summary>
        private ExtensionsIn _extensions;

        /// <summary>
        /// Platform vs Cross-Platform Authenticators. (Optional)
        /// </summary>
        public AuthenticatorAttachment AuthenticatorAttachment { get; set; }

        /// <summary>
        /// Require key to be resident or not. Defaulting to false.
        /// </summary>
        public bool RequireResidentKey { get; set; }

        /// <summary>
        /// User Verification Requirement.
        /// </summary>
        public UserVerificationRequirement UserVerificationRequirement { get; set; }

        /// <summary>
        /// Attestation Conveyance Preference.
        /// </summary>
        public AttestationConveyancePreference AttestationConveyancePreference { get; set; }

        /// <summary>
        /// Reserved for future Use
        /// </summary>
        private int Flags { get; set; }

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
        private IntPtr _excludeCredentialList = IntPtr.Zero;

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

        public Credentials ExcludeCredentials
        {
            set
            {
                _excludeCredentials?.Dispose();
                _excludeCredentials = value;
            }
        }

        public ExtensionsIn Extensions
        {
            set
            {
                _extensions?.Dispose();
                _extensions = value;
            }
        }

        public CredentialList ExcludeCredentialsEx
        {
            set
            {
                if (value != null)
                {
                    if (_excludeCredentialList == IntPtr.Zero)
                    {
                        _excludeCredentialList = Marshal.AllocHGlobal(Marshal.SizeOf<CredentialList>());
                    }

                    Marshal.StructureToPtr<CredentialList>(value, _excludeCredentialList, false);
                }
                else
                {
                    if (_excludeCredentialList != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(_excludeCredentialList);
                        _excludeCredentialList = IntPtr.Zero;
                    }
                }
            }
        }

        public void Dispose()
        {
            _extensions?.Dispose();
            _extensions = null;

            _excludeCredentials?.Dispose();
            _excludeCredentials = null;

            if(_excludeCredentialList != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_excludeCredentialList);
                _excludeCredentialList = IntPtr.Zero;
            }

            if (_cancellationId != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_cancellationId);
                _cancellationId = IntPtr.Zero;
            }
        }
    }
}
