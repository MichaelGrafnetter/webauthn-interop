using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    // TODO: CredentialExIn should be derived from Credential.
    /// <summary>
    /// Information about credential with extra information, such as, Transports.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_EX.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class  CredentialExIn
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private protected CredentialExVersion Version = CredentialExVersion.Current;

        /// <summary>
        /// Unique ID for this particular credential.
        /// </summary>
        private VariableByteArrayIn _id;

        /// <summary>
        /// Well-known credential type specifying what this particular credential is.
        /// </summary>
        public string Type;

        /// <summary>
        /// Transports.
        /// </summary>
        public AuthenticatorTransport Transports;

        public byte[] Id
        {
            get => _id?.Data;
            set => _id = new VariableByteArrayIn(value);
        }
    }

    // TODO: CredentialExOut should be derived from Credential.

    /// <summary>
    /// Information about credential with extra information, such as, Transports.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_EX.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class CredentialExOut
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private protected CredentialExVersion Version = CredentialExVersion.Current;

        /// <summary>
        /// Unique ID for this particular credential.
        /// </summary>
        public VariableByteArrayOut Id;

        /// <summary>
        /// Well-known credential type specifying what this particular credential is.
        /// </summary>
        public string Type;

        /// <summary>
        /// Transports.
        /// </summary>
        AuthenticatorTransport Transports;
    }

    /// <summary>
    /// Information about credential list with extra information.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_LIST.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class CredentialExListOut : VariableArrayOut<CredentialExOut>
    {
        private CredentialExListOut() : base() { }
    }

    /// <summary>
    /// Information about credential list with extra information.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_LIST.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class CredentialExListIn : VariableArrayIn<CredentialExIn>
    {
        public CredentialExListIn(CredentialExIn[] credentials) : base(credentials) { }
    }
}
