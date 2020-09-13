using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Information about credential.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct  Credential
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_CURRENT_VERSION.</remarks>
        private const int CurrentVersion = 1;

        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        public int Version;

        /// <summary>
        /// Size of ID.
        /// </summary>
        public int IdLength;

        /// <summary>
        /// Unique ID for this particular credential.
        /// </summary>
        public byte[] Id;

        /// <summary>
        /// Well-known credential type specifying what this particular credential is.
        /// </summary>
        public string Type;
    }

    /// <summary>
    /// Information about credentials.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIALS.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct CredentialList
    {
        int Count;
        Credential[] Values;
    }
}
