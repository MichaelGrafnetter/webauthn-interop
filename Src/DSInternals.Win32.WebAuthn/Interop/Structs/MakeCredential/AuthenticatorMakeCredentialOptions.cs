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
        private AuthenticatorMakeCredentialOptionsVersion _version = AuthenticatorMakeCredentialOptionsVersion.Version9;

        /// <summary>
        /// Time that the operation is expected to complete within.
        /// </summary>
        /// <remarks>This is used as guidance, and can be overridden by the platform.</remarks>
        public uint TimeoutMilliseconds { get; set; } = ApiConstants.DefaultTimeoutMilliseconds;

        /// <summary>
        /// Credentials used for exclusion.
        /// </summary>
        private Credentials? _excludeCredentials;

        /// <summary>
        /// Optional extensions to parse when performing the operation.
        /// </summary>
        private ExtensionsIn? _extensions;

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
        private ByteArrayIn? _jsonExt;

        //
        // The following fields have been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_8
        //

        /// <summary>
        /// PRF extension "eval" values which will be converted into HMAC-SECRET values according to WebAuthn Spec.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_8.</remarks>
        private IntPtr _prfGlobalEval = IntPtr.Zero;

        /// <summary>
        /// Public key credential hint strings (https://w3c.github.io/webauthn/#enum-hints).
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_8.</remarks>
        private SafeStringArrayIn? _credentialHints;

        /// <summary>
        /// Enable ThirdPartyPayment.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_8.</remarks>
        public bool ThirdPartyPayment { get; set; }

        //
        // The following fields have been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_9
        //

        /// <summary>
        /// Web Origin. For Remote Web App scenario.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_9.</remarks>
        [MarshalAs(UnmanagedType.LPWStr)]
        private string? _remoteWebOrigin;

        /// <summary>
        /// Size of PublicKeyCredentialCreationOptionsJSON.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_9.</remarks>
        private int _publicKeyCredentialCreationOptionsJsonLength = 0;

        /// <summary>
        /// UTF-8 encoded JSON serialization of the PublicKeyCredentialCreationOptions.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_9.</remarks>
        private ByteArrayIn? _publicKeyCredentialCreationOptionsJson;

        /// <summary>
        /// Size of AuthenticatorId.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_9.</remarks>
        private int _authenticatorIdLength = 0;

        /// <summary>
        /// Authenticator ID got from WebAuthNGetAuthenticatorList API.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_9.</remarks>
        private ByteArrayIn? _authenticatorId;

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
                    if (_cancellationId == IntPtr.Zero)
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
        public HybridStorageLinkedData? LinkedDevice
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
        public byte[]? JsonExt
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
        /// PRF extension "eval" values which will be converted into HMAC-SECRET values according to WebAuthn Spec.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_8.</remarks>
        public HmacSecretSaltIn? PrfGlobalEval
        {
            set
            {
                if (value != null)
                {
                    if (_prfGlobalEval == IntPtr.Zero)
                    {
                        _prfGlobalEval = Marshal.AllocHGlobal(Marshal.SizeOf<HmacSecretSaltIn>());
                    }

                    Marshal.StructureToPtr<HmacSecretSaltIn>(value, _prfGlobalEval, false);
                }
                else
                {
                    FreePrfGlobalEval();
                }
            }
        }

        /// <summary>
        /// Public key credential hint strings (https://w3c.github.io/webauthn/#enum-hints).
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_8.</remarks>
        public string[]? CredentialHints
        {
            set
            {
                // Dispose previous value
                _credentialHints?.Dispose();

                if (value != null && value.Length > 0)
                {
                    _credentialHints = new SafeStringArrayIn(value);
                }
                else
                {
                    _credentialHints = null;
                }
            }
        }

        /// <summary>
        /// Web Origin. For Remote Web App scenario.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_9.</remarks>
        public string? RemoteWebOrigin
        {
            get => _remoteWebOrigin;
            set => _remoteWebOrigin = value;
        }

        /// <summary>
        /// UTF-8 encoded JSON serialization of the PublicKeyCredentialCreationOptions.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_9.</remarks>
        public byte[]? PublicKeyCredentialCreationOptionsJson
        {
            get
            {
                return _publicKeyCredentialCreationOptionsJson?.Read(_publicKeyCredentialCreationOptionsJsonLength);
            }
            set
            {
                // Get rid of any previous blob first
                _publicKeyCredentialCreationOptionsJson?.Dispose();

                // Now replace the previous value with a new one
                _publicKeyCredentialCreationOptionsJsonLength = value?.Length ?? 0;
                _publicKeyCredentialCreationOptionsJson = new ByteArrayIn(value);
            }
        }

        /// <summary>
        /// Authenticator ID got from WebAuthNGetAuthenticatorList API.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_9.</remarks>
        public byte[]? AuthenticatorId
        {
            get
            {
                return _authenticatorId?.Read(_authenticatorIdLength);
            }
            set
            {
                // Get rid of any previous blob first
                _authenticatorId?.Dispose();

                // Now replace the previous value with a new one
                _authenticatorIdLength = value?.Length ?? 0;
                _authenticatorId = new ByteArrayIn(value);
            }
        }

        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        /// <remarks>This is a V9 struct. If V10 arrives, new fields will need to be added.</remarks>
        public AuthenticatorMakeCredentialOptionsVersion Version
        {
            get
            {
                return _version;
            }
            set
            {
                if (value > AuthenticatorMakeCredentialOptionsVersion.Version9)
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

            _publicKeyCredentialCreationOptionsJson?.Dispose();
            _publicKeyCredentialCreationOptionsJson = null;

            _authenticatorId?.Dispose();
            _authenticatorId = null;

            _credentialHints?.Dispose();
            _credentialHints = null;

            FreeExcludeCredentialList();
            FreeCancellationId();
            FreeLinkedDevice();
            FreePrfGlobalEval();
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

        private void FreePrfGlobalEval()
        {
            if (_prfGlobalEval != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_prfGlobalEval);
                _prfGlobalEval = IntPtr.Zero;
            }
        }
    }
}
