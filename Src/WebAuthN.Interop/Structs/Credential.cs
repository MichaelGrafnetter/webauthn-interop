using System;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Information about credential.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class  CredentialIn : IDisposable
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private protected CredentialVersion Version = CredentialVersion.Current;

        /// <summary>
        /// Unique ID for this particular credential.
        /// </summary>
        private VariableByteArrayIn _id;

        /// <summary>
        /// Well-known credential type specifying what this particular credential is.
        /// </summary>
        public string Type;

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
            }
        }
    }

    /// <summary>
    /// Information about credential.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class CredentialOut
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private protected CredentialVersion Version = CredentialVersion.Current;

        /// <summary>
        /// Unique ID for this particular credential.
        /// </summary>
        private VariableByteArrayOut Id;

        /// <summary>
        /// Well-known credential type specifying what this particular credential is.
        /// </summary>
        public string Type;
    }

    /// <summary>
    /// Information about credential list with extra information.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIALS.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class CredentialsOut : VariableArray<CredentialOut>
    {
        private CredentialsOut() : base() { }
    }

    /// <summary>
    /// Information about credential list with extra information.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIALS.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class CredentialsIn : VariableArrayIn<CredentialIn>
    {
        public CredentialsIn(CredentialIn[] credentials) : base(credentials) { }
    }
}
