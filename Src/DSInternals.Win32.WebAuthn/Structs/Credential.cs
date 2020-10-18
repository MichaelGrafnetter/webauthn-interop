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

        private int _idLength;

        /// <summary>
        /// Unique ID for this particular credential.
        /// </summary>
        private ByteArrayIn _id;

        /// <summary>
        /// Well-known credential type specifying what this particular credential is.
        /// </summary>
        private string _type;

        public CredentialIn(byte[] id, string type)
        {
            _id = new ByteArrayIn(id);
            _idLength = id?.Length ?? 0;
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
        private protected CredentialVersion Version { get; set; } = CredentialVersion.Current;

        private int _idLength;


        private ByteArrayOut _id;

        /// <summary>
        /// Well-known credential type specifying what this particular credential is.
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Unique ID for this particular credential.
        /// </summary>
        public byte[] Id => _id?.Read(_idLength);

        private CredentialOut() { }
    }
}
