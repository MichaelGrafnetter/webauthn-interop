using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using DSInternals.Win32.WebAuthn.FIDO;

namespace DSInternals.Win32.WebAuthn.Interop
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
        private ClientDataVersion _version = ClientDataVersion.Current;

        private int _clientDataLength;

        private ByteArrayIn _clientData;

        /// <summary>
        /// Hash algorithm ID used to hash the ClientDataJSON field.
        /// </summary>
        public string HashAlgId { get; set; }

        /// <summary>
        /// JSON serialization of the client data.
        /// </summary>
        public string ClientDataJson
        {
            set
            {
                ClientDataRaw = (value != null) ? Encoding.UTF8.GetBytes(value) : null;
            }
        }

        /// <summary>
        /// UTF-8 encoded JSON serialization of the client data.
        /// </summary>
        public byte[] ClientDataRaw
        {
            get
            {
                return _clientData?.Read(_clientDataLength);
            }
            set
            {
                // Get rid of any previous data first
                _clientData?.Dispose();

                // Now replace the previous value with a new one
                _clientDataLength = value?.Length ?? 0;
                _clientData = new ByteArrayIn(value);
            }
        }

        public ClientData(CollectedClientData clientData)
        {
            this.ClientDataJson = JsonSerializer.Serialize(clientData);
            // Note that SHA-256 is currently hardcoded in Chromium and Firefox.
            this.HashAlgId = ApiConstants.HashAlgorithmSha256;
        }

        public ClientData(byte[] clientDataJson)
        {
            this.ClientDataRaw = clientDataJson;
            // Note that SHA-256 is currently hardcoded in Chromium and Firefox.
            this.HashAlgId = ApiConstants.HashAlgorithmSha256;
        }

        public ClientData(string clientDataJson)
        {
            this.ClientDataJson = clientDataJson;
            // Note that SHA-256 is currently hardcoded in Chromium and Firefox.
            this.HashAlgId = ApiConstants.HashAlgorithmSha256;
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
