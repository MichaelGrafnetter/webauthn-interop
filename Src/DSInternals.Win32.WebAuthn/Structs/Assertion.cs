using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Assertion
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_ASSERTION.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class Assertion
    {
        /// <summary>
        /// // Version of this structure, to allow for modifications in the future.
        /// </summary>
        public AssertionVersion Version { get; private set; }

        /// <summary>
        /// Authenticator data that was created for this assertion.
        /// </summary>
        private SafeByteArrayOut _authenticatorData;

        /// <summary>
        /// Signature that was generated for this assertion.
        /// </summary>
        private SafeByteArrayOut _signature;

        /// <summary>
        /// Credential that was used for this assertion.
        /// </summary>
        public CredentialOut Credential { get; private set; }

        /// <summary>
        /// UserId
        /// </summary>
        private SafeByteArrayOut _userId;

        public byte[] AuthenticatorData => _authenticatorData?.Data;

        public byte[] Signature => _signature?.Data;

        public byte[] UserId => _userId?.Data;
    }
}
