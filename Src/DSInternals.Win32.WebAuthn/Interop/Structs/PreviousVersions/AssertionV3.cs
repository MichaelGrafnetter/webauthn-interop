using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// This structure comes from an older version of the API and should only be used by Marshal.Sizeof().
    /// </summary>
    /// <see cref="DSInternals.Win32.WebAuthn.Interop.Assertion"/>
    /// <remarks>Corresponds to WEBAUTHN_ASSERTION.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class AssertionV3
    {
        /// <remarks>This field has been present since WEBAUTHN_ASSERTION_VERSION_1.</remarks>
        private AssertionVersion _version;

        /// <remarks>This field has been present since WEBAUTHN_ASSERTION_VERSION_1.</remarks>
        private int _authenticatorDataLength;

        /// <remarks>This field has been present since WEBAUTHN_ASSERTION_VERSION_1.</remarks>
        private ByteArrayOut _authenticatorData;

        /// <remarks>This field has been present since WEBAUTHN_ASSERTION_VERSION_1.</remarks>
        private int _signatureLength;

        /// <remarks>This field has been present since WEBAUTHN_ASSERTION_VERSION_1.</remarks>
        private ByteArrayOut _signature;

        /// <remarks>This field has been present since WEBAUTHN_ASSERTION_VERSION_1.</remarks>
        private CredentialOut _credential;

        /// <remarks>This field has been present since WEBAUTHN_ASSERTION_VERSION_1.</remarks>
        private int _userIdLength;

        /// <remarks>This field has been present since WEBAUTHN_ASSERTION_VERSION_1.</remarks>
        private ByteArrayOut _userId;

        /// <remarks>This field has been added in WEBAUTHN_ASSERTION_VERSION_2.</remarks>
        private ExtensionsOut _extensions;

        /// <remarks>This field has been added in WEBAUTHN_ASSERTION_VERSION_2.</remarks>
        private int _largeBlobLength;

        /// <remarks>This field has been added in WEBAUTHN_ASSERTION_VERSION_2.</remarks>
        private ByteArrayOut _largeBlob;

        /// <remarks>This field has been added in WEBAUTHN_ASSERTION_VERSION_2.</remarks>
        private CredentialLargeBlobStatus _largeBlobStatus;

        /// <remarks>This field has been added in WEBAUTHN_ASSERTION_VERSION_3.</remarks>
        private IntPtr _hmacSecret;

        /// <summary>
        /// The instantiation of this class is blocked by this private constructor.
        /// </summary>
        private AssertionV3() { }
    }
}
