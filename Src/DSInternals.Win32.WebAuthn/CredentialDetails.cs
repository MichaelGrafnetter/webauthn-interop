namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Information about credential.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_DETAILS.</remarks>
    public sealed class CredentialDetails
    {
        /// <summary>
        /// The credential Id.
        /// </summary>
        public byte[] CredentialId { get; set; }

        /// <summary>
        /// The relying party information.
        /// </summary>
        public RelyingPartyInformation RelyingPartyInformation { get; set; }

        /// <summary>
        /// The user information.
        /// </summary>
        public UserInformation UserInformation { get; set; }

        /// <summary>
        ///  Indicates if the credential is removable or not.
        /// </summary>
        public bool Removable { get; set; }

        /// <summary>
        /// Indicates if the credential is backed up or not.
        /// </summary>
        public bool BackedUp { get; set; }
    }
}
