using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// Information about credential.
    /// </summary>
    /// <remarks>Corresponds to _WEBAUTHN_CREDENTIAL_DETAILS.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class CredentialDetailsOut

    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_DETAILS_VERSION_1.</remarks>
        private protected CredentialDetailsVersion Version = CredentialDetailsVersion.Version2;

        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_DETAILS_VERSION_1.</remarks>
        private int _credentialIdLength;
        
        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_DETAILS_VERSION_1.</remarks>
        private ByteArrayOut _credentialId;

        /// <summary>
        /// RP Info
        /// </summary>
        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_DETAILS_VERSION_1.</remarks>
        private IntPtr _relyingPartyInformation;

        /// <summary>
        /// User Info
        /// </summary>
        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_DETAILS_VERSION_1.</remarks>
        // public UserInformation UserInformation { get; set; }
        private IntPtr _userInformation;

        /// <summary>
        ///  Removable or not.
        /// </summary>
        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_DETAILS_VERSION_1.</remarks>
        public bool Removable { get; set; }

        /// <summary>
        /// Backed Up or not.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_CREDENTIAL_DETAILS_VERSION_2.</remarks>
        public bool BackedUp { get; set; }

        /// <summary>
        /// RP Info
        /// </summary>
        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_DETAILS_VERSION_1.</remarks>
        public RelyingPartyInformation RelyingPartyInformation => Marshal.PtrToStructure<RelyingPartyInformation>(_relyingPartyInformation);

        /// <summary>
        /// RP Info
        /// </summary>
        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_DETAILS_VERSION_1.</remarks>
        public UserInformationOut UserInformation => Marshal.PtrToStructure<UserInformationOut>(_userInformation);

        /// <summary>
        /// CredentialId bytes.
        /// </summary>
        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_DETAILS_VERSION_1.</remarks>
        public byte[] CredentialId => _credentialId?.Read(_credentialIdLength);
    }
}
