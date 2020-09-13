using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct ClientData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CLIENT_DATA_CURRENT_VERSION.</remarks>
        private const int CurrentVersion = 1;

        private const string SHA256 = "SHA-256";
        private const string SHA384 = "SHA-384";
        private const string SHA512 = "SHA-512";

        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        public int Version;

        /// <summary>
        /// Size of the ClientDataJSON field.
        /// </summary>
        public int ClientDataJSONLength;

        /// <summary>
        /// UTF-8 encoded JSON serialization of the client data.
        /// </summary>
        [MarshalAs(UnmanagedType.LPUTF8Str)]
        public string ClientDataJSON;

        /// <summary>
        /// Hash algorithm ID used to hash the ClientDataJSON field.
        /// </summary>
        public string HashAlgId;
    }
}
