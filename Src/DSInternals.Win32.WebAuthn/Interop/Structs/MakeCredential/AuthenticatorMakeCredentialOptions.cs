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
        private AuthenticatorMakeCredentialOptionsVersion _version = AuthenticatorMakeCredentialOptionsVersion.Version7;

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

        /// <summary>
        /// Linked Device Connection Info.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_7.</remarks>
        private IntPtr _linkedDevice { get; set; }

        /// <summary>
        /// Size of JSON extension.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_7.</remarks>
        private int _jsonExtLength = 0;

        /// <summary>
        /// JSON extension.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_7.</remarks>
        private ByteArrayIn _jsonExt;

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
                    FreeCancellationId();
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
                    FreeExcludeCredentialList();
                }
            }
        }

        /// <summary>
        /// Linked Device Connection Info.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_7.</remarks>
        public HybridStorageLinkedData LinkedDevice
        {
            set
            {
                if (value != null)
                {
                    if (_linkedDevice == IntPtr.Zero)
                    {
                        _linkedDevice = Marshal.AllocHGlobal(Marshal.SizeOf<HybridStorageLinkedData>());
                    }

                    Marshal.StructureToPtr<HybridStorageLinkedData>(value, _linkedDevice, false);
                }
                else
                {
                    FreeLinkedDevice();
                }
            }
        }

        /// <summary>
        /// JSON extension.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_7.</remarks>
        public byte[] JsonExt
        {
            get
            {
                return _jsonExt?.Read(_jsonExtLength);
            }
            set
            {
                // Get rid of any previous blob first
                _jsonExt?.Dispose();

                // Now replace the previous value with a new one
                _jsonExtLength = value?.Length ?? 0;
                _jsonExt = new ByteArrayIn(value);
            }
        }

        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        /// <remarks>This is a V7 struct. If V8 arrives, new fields will need to be added.</remarks>
        public AuthenticatorMakeCredentialOptionsVersion Version
        {
            get
            {
                return _version;
            }
            set
            {
                if(value > AuthenticatorMakeCredentialOptionsVersion.Version7)
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

            _jsonExt?.Dispose();
            _jsonExt = null;

            FreeExcludeCredentialList();
            FreeCancellationId();
            FreeLinkedDevice();
        }

        private void FreeExcludeCredentialList()
        {
            if (_excludeCredentialList != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_excludeCredentialList);
                _excludeCredentialList = IntPtr.Zero;
            }
        }

        private void FreeCancellationId()
        {
            if (_cancellationId != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_cancellationId);
                _cancellationId = IntPtr.Zero;
            }
        }

        private void FreeLinkedDevice()
        {
            if (_linkedDevice != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_linkedDevice);
                _linkedDevice = IntPtr.Zero;
            }
        }
    }
}
