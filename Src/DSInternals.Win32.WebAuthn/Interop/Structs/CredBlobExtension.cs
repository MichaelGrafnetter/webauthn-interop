using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// The structure containing the blob extension data for a credential.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CRED_BLOB_EXTENSION.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class CredBlobExtensionIn : IDisposable
    {
        /// <summary>
        /// Size of the credential blob.
        /// </summary>
        private int _credBlobLength;

        /// <summary>
        /// The credential blob.
        /// </summary>
        private ByteArrayIn _credBlob;

        public CredBlobExtensionIn(byte[] credBlob)
        {
            _credBlob = new ByteArrayIn(credBlob);
            _credBlobLength = credBlob?.Length ?? 0;
        }

        public void Dispose()
        {
            _credBlob?.Dispose();
            _credBlob = null;
        }
    }

    /// <summary>
    /// The structure containing the blob extension data for a credential.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CRED_BLOB_EXTENSION.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal class CredBlobExtensionOut
    {

        /// <summary>
        /// Size of the credential blob.
        /// </summary>
        private int _credBlobLength;

        /// <summary>
        /// The credential blob.
        /// </summary>
        private ByteArrayOut _credBlob;

        /// <summary>
        /// The credential blob.
        /// </summary>
        public byte[] CredBlob => _credBlob?.Read(_credBlobLength);

        private CredBlobExtensionOut() { }
    }
}
