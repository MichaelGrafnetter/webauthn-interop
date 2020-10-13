using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Information about client data.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CLIENT_DATA.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class ClientData : IDisposable
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private protected ClientDataVersion _version = ClientDataVersion.Current;

        /// <summary>
        /// UTF-8 encoded JSON serialization of the client data.
        /// </summary>
        private SafeByteArrayIn _clientData = new SafeByteArrayIn(null);

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
                byte[] binaryData = ClientDataRaw;
                return (binaryData != null) ? Encoding.UTF8.GetString(binaryData) : null;
            }

            set
            {
                ClientDataRaw = (value != null) ? Encoding.UTF8.GetBytes(value) : null;
            }
        }

        public byte[] ClientDataRaw
        {
            get
            {
                return _clientData?.Data;
            }

            set
            {
                // Get rid of any previous data first
                Dispose();

                // Now replace the previous value with a new one
                _clientData = new SafeByteArrayIn(value);
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
