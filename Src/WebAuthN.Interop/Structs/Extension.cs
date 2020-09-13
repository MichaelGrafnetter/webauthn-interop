using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    // TODO: Add WEBAUTHN_EXTENSIONS_IDENTIFIER_HMAC_SECRET
    // TODO: Add WEBAUTHN_CRED_PROTECT_EXTENSION_IN

    /// <summary>
    /// Information about Extension.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_EXTENSION.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct  Extension
    {
        public string Identifier;

        /// <summary>
        /// Size of extension data.
        /// </summary>
        public int DataLength;

        /// <summary>
        /// Extension data.
        /// </summary>
        public byte[] Data;
    }

    /// <summary>
    /// Information about Extensions.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_EXTENSIONS.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct ExtensionlList
    {
        int Count;
        Extension[] Values;
    }
}
