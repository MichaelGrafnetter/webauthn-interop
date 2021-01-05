using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Options.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class AuthenticatorGetAssertionOptions : IDisposable
    {
        private AuthenticatorGetAssertionOptionsVersion _version = AuthenticatorGetAssertionOptionsVersion.Version4;

        /// <summary>
        /// Time that the operation is expected to complete within.
        /// </summary>
        /// <remarks>This is used as guidance, and can be overridden by the platform.</remarks>
        public int TimeoutMilliseconds { get; set; } = ApiConstants.DefaultTimeoutMilliseconds;

        private Credentials _allowCredentials;

        private ExtensionsIn _extensions;

        /// <summary>
        /// Platform vs Cross-Platform Authenticators. (Optional)
        /// </summary>
        public AuthenticatorAttachment AuthenticatorAttachment { get; set; }

        /// <summary>
        /// User Verification Requirement.
        /// </summary>
        public UserVerificationRequirement UserVerificationRequirement { get; set; }

        /// <summary>
        /// Reserved for future Use.
        /// </summary>
        private uint Flags;

        // The following fields have been added in WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_2

        [MarshalAs(UnmanagedType.LPUTF8Str)]
        private string _u2fAppId;

        // If the following is non-NULL, then, set to TRUE if the above U2fAppid was used instead of RpId
        // Note that this value is modified by WebAuthNAuthenticatorGetAssertion
        private IntPtr _isU2fAppIdUsed;

        //
        // The following fields have been added in WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_3
        //

        private IntPtr _cancellationId = IntPtr.Zero;

        //
        // The following fields have been added in WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_4
        //

        private IntPtr _allowCredentialList = IntPtr.Zero;

        /// <summary>
        /// Optional identifier for the U2F AppId. Converted to UTF8 before being hashed. Not lower cased.
        /// </summary>
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

        /// <summary>
        /// Cancellation Id (Optional)
        /// </summary>
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

        /// <summary>
        /// Allowed Credentials List.
        /// </summary>
        public Credentials AllowCredentials
        {
            set
            {
                _allowCredentials?.Dispose();
                _allowCredentials = value;
            }
        }

        /// <summary>
        /// Allow Credential List. If present, "AllowCredentials" will be ignored.
        /// </summary>
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

        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        /// <remarks>This is a V4 struct. If V5 arrives, new fields will need to be added.</remarks>
        public AuthenticatorGetAssertionOptionsVersion Version
        {
            get
            {
                return _version;
            }
            set
            {
                if (value > AuthenticatorGetAssertionOptionsVersion.Version4)
                {
                    // We only support older struct versions.
                    throw new ArgumentOutOfRangeException(nameof(value), "The requested data structure version is not yet supported.");
                }

                _version = value;
            }
        }

        /// <summary>
        /// Extensions to parse when performing the operation. (Optional)
        /// </summary>
        public ExtensionsIn Extensions
        {
            set
            {
                _extensions?.Dispose();
                _extensions = value;
            }
        }

        public void Dispose()
        {
            _extensions?.Dispose();
            _extensions = null;
            
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
