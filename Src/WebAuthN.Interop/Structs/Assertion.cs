using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Assertion
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_ASSERTION.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal class Assertion
    {
        /// <summary>
        /// // Version of this structure, to allow for modifications in the future.
        /// </summary>
        private protected AssertionVersion Version = AssertionVersion.Current;

        /// <summary>
        /// Authenticator data that was created for this assertion.
        /// </summary>
        private VariableByteArrayOut _authenticatorData;

        /// <summary>
        /// Signature that was generated for this assertion.
        /// </summary>
        private VariableByteArrayOut _signature;

        /// <summary>
        /// Credential that was used for this assertion.
        /// </summary>
        public CredentialOut Credential { get; private set; }

        /// <summary>
        /// UserId
        /// </summary>
        private VariableByteArrayOut _userId;

        public byte[] AuthenticatorData => _authenticatorData?.Data;
        public byte[] Signature => _signature?.Data;
        public byte[] UserId => _userId?.Data;
    }
}
