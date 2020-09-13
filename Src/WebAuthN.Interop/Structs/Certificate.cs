using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// X.509 Certificate
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_X5C.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct Certificate
    {
        /// <summary>
        /// // Length of X.509 encoded certificate.
        /// </summary>
        public int DataLength;

        /// <summary>
        /// X.509 encoded certificate bytes.
        /// </summary>
        public byte[] Data;
    }
}
