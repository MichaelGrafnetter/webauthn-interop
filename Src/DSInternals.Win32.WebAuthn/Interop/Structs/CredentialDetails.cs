using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// Information about credential.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class CredentialDetails
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private protected CredentialDetailtVersion Version = CredentialDetailtVersion.Version1;

        private int _credentialIdLength;

        private ByteArrayOut _credentialId;

        /// <summary>
        /// RP Info
        /// </summary>
        public RelyingPartyInformation RelyingPartyInformation { get; set; }

        /// <summary>
        /// User Info
        /// </summary>
        public UserInformation UserInformation { get; set; }


        /// <summary>
        ///  Removable or not.
        /// </summary>
        public bool Removable { get; set; }

        /// <summary>
        /// CredentialId bytes.
        /// </summary>
        public byte[] CredentialId => _credentialId?.Read(_credentialIdLength);
    }
}
