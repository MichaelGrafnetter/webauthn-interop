using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Information about Extension.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_EXTENSION.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class  ExtensionIn
    {
        /// <summary>
        /// Extension identifier.
        /// </summary>
        public string _identifier;

        /// <summary>
        /// Extension data.
        /// </summary>
        private SafeByteArrayIn _data;

        public ExtensionIn(string id, byte[] data)
        {
            _identifier = id;
            _data = new SafeByteArrayIn(data);
        }
    }

    /// <summary>
    /// Information about Extension.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_EXTENSION.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class ExtensionOut
    {
        /// <summary>
        /// Extension identifier.
        /// </summary>
        public string Identifier { get; private set; }

        /// <summary>
        /// Extension data.
        /// </summary>
        private SafeByteArrayOut _data;

        public byte[] Data => _data?.Data;

        private ExtensionOut() { }
    }
}
