using System;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Information about credential with extra information, such as, Transports.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_EX.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class  CredentialExIn : IDisposable
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private protected CredentialExVersion Version = CredentialExVersion.Current;

        /// <summary>
        /// Unique ID for this particular credential.
        /// </summary>
        private VariableByteArrayIn _id = new VariableByteArrayIn(null);

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

        public void Dispose()
        {
            if(_id != null)
            {
                _id.Dispose();
                _id = null;
            }
        }
    }

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
    internal sealed class CredentialExListOut : VariableArray<CredentialExOut>
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
