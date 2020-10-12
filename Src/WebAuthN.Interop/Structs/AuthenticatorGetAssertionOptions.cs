using System;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Options.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class AuthenticatorGetAssertionOptions : IDisposable
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
        private Credentials _allowCredentials = new Credentials(null);

        /// <summary>
        /// Extensions to parse when performing the operation. (Optional)
        /// </summary>
        public ExtensionsIn Extensions = new ExtensionsIn(null);

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
        [MarshalAs(UnmanagedType.LPUTF8Str)]
        private string _u2fAppId;

        // If the following is non-NULL, then, set to TRUE if the above U2fAppid was used instead of RpId
        private IntPtr _isU2fAppIdUsed;

        //
        // The following fields have been added in WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_3
        //

        /// <summary>
        /// Cancellation Id (Optional)
        /// </summary>
        private IntPtr _cancellationId = IntPtr.Zero;

        //
        // The following fields have been added in WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_4
        //

        /// <summary>
        /// Allow Credential List. If present, "CredentialList" will be ignored.
        /// </summary>
        private IntPtr _allowCredentialList = IntPtr.Zero;

        public string U2fAppId
        {
            get
            {
                return _u2fAppId;
            }
            set
            {
                _u2fAppId = value;
                if(_isU2fAppIdUsed == IntPtr.Zero)
                {
                    _isU2fAppIdUsed = Marshal.AllocHGlobal(sizeof(int));
                }
                
                Marshal.WriteInt32(_isU2fAppIdUsed, Convert.ToInt32(value != null));
            }
        }

        public Guid? CancellationId
        {
            set
            {
                if (value.HasValue)
                {
                    if (_cancellationId == IntPtr.Zero)
                    {
                        _cancellationId = Marshal.AllocHGlobal(Marshal.SizeOf<Guid>());
                    }

                    Marshal.StructureToPtr<Guid>(value.Value, _cancellationId, false);
                }
                else
                {
                    if (_cancellationId != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(_cancellationId);
                        _cancellationId = IntPtr.Zero;
                    }
                }
            }
        }

        public Credentials AllowCredentials
        {
            set
            {
                _allowCredentials?.Dispose();
                _allowCredentials = value;
            }
        }

        public CredentialList AllowCredentialsEx
        {
            set
            {
                if (value != null)
                {
                    if (_allowCredentialList == IntPtr.Zero)
                    {
                        _allowCredentialList = Marshal.AllocHGlobal(Marshal.SizeOf<CredentialList>());
                    }

                    Marshal.StructureToPtr<CredentialList>(value, _allowCredentialList, false);
                }
                else
                {
                    if (_allowCredentialList != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(_allowCredentialList);
                        _allowCredentialList = IntPtr.Zero;
                    }
                }
            }
        }

        public void Dispose()
        {
            Extensions?.Dispose();
            Extensions = null;
            
            _allowCredentials?.Dispose();
            _allowCredentials = null;

            if (_allowCredentialList != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_allowCredentialList);
                _allowCredentialList = IntPtr.Zero;
            }

            if (_isU2fAppIdUsed != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_isU2fAppIdUsed);
                _isU2fAppIdUsed = IntPtr.Zero;
            }

            if (_cancellationId != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_cancellationId);
                _cancellationId = IntPtr.Zero;
            }
        }
    }
}
