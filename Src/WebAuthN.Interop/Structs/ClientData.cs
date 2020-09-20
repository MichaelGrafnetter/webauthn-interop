using System.Runtime.InteropServices;
using System.Text;

namespace WebAuthN.Interop
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class ClientData
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
        private VariableByteArray _clientData;

        /// <summary>
        /// Hash algorithm ID used to hash the ClientDataJSON field.
        /// </summary>
        public string HashAlgId;

        /// <summary>
        /// JSON serialization of the client data.
        /// </summary>
        public string ClientDataJSON
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

            private set
            {
                byte[] binaryString = Encoding.UTF8.GetBytes(value);
                _clientData = new VariableByteArray(binaryString);
            }
        }
    }
}
