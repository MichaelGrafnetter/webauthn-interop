using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Information about client data.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CLIENT_DATA.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class ClientData : IDisposable
    {
        // TODO: Hash algorithm Id enum
        private const string SHA256 = "SHA-256";
        private const string SHA384 = "SHA-384";
        private const string SHA512 = "SHA-512";

        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private protected ClientDataVersion _version = ClientDataVersion.Current;

        /// <summary>
        /// UTF-8 encoded JSON serialization of the client data.
        /// </summary>
        private VariableByteArrayIn _clientData = new VariableByteArrayIn(null);

        /// <summary>
        /// Hash algorithm ID used to hash the ClientDataJSON field.
        /// </summary>
        public string HashAlgId;

        /// <summary>
        /// JSON serialization of the client data.
        /// </summary>
        public string ClientDataJson
        {
            get
            {
                if (_clientData == null)
                {
                    return null;
                }

                byte[] binaryData = _clientData.Data;
                return (binaryData != null) ? Encoding.UTF8.GetString(binaryData) : null;
            }

            set
            {
                byte[] binaryString = (value != null) ? Encoding.UTF8.GetBytes(value) : null;
                _clientData = new VariableByteArrayIn(binaryString);
            }
        }

        public void Dispose()
        {
            if (_clientData != null)
            {
                _clientData.Dispose();
                _clientData = null;
            }
        }
    }
}
