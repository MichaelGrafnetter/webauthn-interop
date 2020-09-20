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
    public class  Extension
    {
        /// <summary>
        /// Extension identifier.
        /// </summary>
        public string Identifier;

        /// <summary>
        /// Extension data.
        /// </summary>
        public VariableByteArray Data;
    }

    /// <summary>
    /// Information about Extensions.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_EXTENSIONS.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    public class Extensions : VariableArray<Extension>
    {

    }
}
