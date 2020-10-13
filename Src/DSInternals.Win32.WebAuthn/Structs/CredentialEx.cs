using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Information about credential with extra information, such as, Transports.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_EX.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class  CredentialEx : IDisposable
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private protected CredentialExVersion Version = CredentialExVersion.Current;

        /// <summary>
        /// Unique ID for this particular credential.
        /// </summary>
        private SafeByteArrayIn _id;

        /// <summary>
        /// Well-known credential type specifying what this particular credential is.
        /// </summary>
        private string _type;

        /// <summary>
        /// Transports.
        /// </summary>
        private CtapTransport _transport;

        public CredentialEx(byte[] id, string type, CtapTransport transport)
        {
            _id = new SafeByteArrayIn(id);
            _type = type;
            _transport = transport;
        }

        public void Dispose()
        {
            _id?.Dispose();
            _id = null;
        }
    }
}
