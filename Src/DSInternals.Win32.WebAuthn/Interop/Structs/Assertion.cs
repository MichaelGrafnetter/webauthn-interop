using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
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

        private int _authenticatorDataLength;

        private ByteArrayOut _authenticatorData;

        private int _signatureLength;

        private ByteArrayOut _signature;

        /// <summary>
        /// Credential that was used for this assertion.
        /// </summary>
        public CredentialOut Credential { get; private set; }

        private int _userIdLength;

        private ByteArrayOut _userId;

        /// <summary>
        /// Authenticator data that was created for this assertion.
        /// </summary>
        public byte[] AuthenticatorData => _authenticatorData?.Read(_authenticatorDataLength);

        /// <summary>
        /// Signature that was generated for this assertion.
        /// </summary>
        public byte[] Signature => _signature?.Read(_signatureLength);

        /// <summary>
        /// User Identifier
        /// </summary>
        public byte[] UserId => _userId?.Read(_userIdLength);
    }
}
