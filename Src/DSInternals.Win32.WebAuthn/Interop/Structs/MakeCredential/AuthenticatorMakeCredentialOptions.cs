using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// The options for the WebAuthNAuthenticatorMakeCredential operation.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal class AuthenticatorMakeCredentialOptions : IDisposable
    {
        /// <summary>
        /// Version of this structure.
        /// </summary>
        private AuthenticatorMakeCredentialOptionsVersion _version = AuthenticatorMakeCredentialOptionsVersion.Version6;

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
        /// Optional extensions to parse when performing the operation.
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
        /// Optional cancellation Id.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_2.</remarks>
        private IntPtr _cancellationId = IntPtr.Zero;

        /// <summary>
        /// The exclude credential list. If present, CredentialList will be ignored.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_3.</remarks>
        private IntPtr _excludeCredentialList = IntPtr.Zero;

        /// <summary>
        /// Enterprise Attestation
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_4.</remarks>
        public EnterpriseAttestationType EnterpriseAttestation { get; set; }

        /// <summary>
        /// The requested large blob support.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_4.</remarks>
        public LargeBlobSupport LargeBlobSupport { get; set; }

        /// <summary>
        /// Prefer key to be resident. Defaulting to FALSE. When TRUE, overrides the RequireResidentKey option.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_4.</remarks>
        public bool PreferResidentKey { get; set; }

        /// <summary>
        /// Indicates whether the browser is in private mode. Defaulting to false.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_5.</remarks>
        public bool BrowserInPrivateMode { get; set; }

        /// <summary>
        /// Indicates whether the Pseudo-random function (PRF) extension should be enabled.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_6.</remarks>
        public bool EnablePseudoRandomFunction { get; set; }

        public AuthenticatorMakeCredentialOptions() { }

        /// <summary>
        /// Cancellation Id (Optional)
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_2.</remarks>
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

        /// <summary>
        /// Credentials used for exclusion.
        /// </summary>
        public Credentials ExcludeCredentials
        {
            set
            {
                _excludeCredentials?.Dispose();
                _excludeCredentials = value;
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

        /// <summary>
        /// Exclude Credential List.
        /// </summary>
        /// <remarks>
        /// If present, "ExcludeCredentials" will be ignored.
        /// This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_3.
        /// </remarks>
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

        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        /// <remarks>This is a V5 struct. If V6 arrives, new fields will need to be added.</remarks>
        public AuthenticatorMakeCredentialOptionsVersion Version
        {
            get
            {
                return _version;
            }
            set
            {
                if(value > AuthenticatorMakeCredentialOptionsVersion.Version5)
                {
                    // We only support older struct versions.
                    throw new ArgumentOutOfRangeException(nameof(value), "The requested data structure version is not yet supported.");
                }

                _version = value;
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
