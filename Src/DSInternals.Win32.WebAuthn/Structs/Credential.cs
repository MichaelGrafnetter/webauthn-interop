using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Information about credential.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class CredentialIn : IDisposable
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private protected CredentialVersion Version = CredentialVersion.Current;

        /// <summary>
        /// Unique ID for this particular credential.
        /// </summary>
        private SafeByteArrayIn _id;

        /// <summary>
        /// Well-known credential type specifying what this particular credential is.
        /// </summary>
        private string _type;

        public CredentialIn(byte[] id, string type)
        {
            _id = new SafeByteArrayIn(id);
            _type = type;
        }

        public void Dispose()
        {
            _id?.Dispose();
            _id = null;
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
        private SafeByteArrayOut _id;

        /// <summary>
        /// Well-known credential type specifying what this particular credential is.
        /// </summary>
        public string Type { get; private set; }

        public byte[] Id => _id?.Data;

        private CredentialOut() { }
    }
}
