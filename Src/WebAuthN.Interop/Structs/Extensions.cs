using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Information about Extensions.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_EXTENSIONS.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal class ExtensionsOut : SafeStructArrayOut<ExtensionOut>
    {
        private ExtensionsOut() : base() { }
    }

    /// <summary>
    /// Information about Extensions.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_EXTENSIONS.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal class ExtensionsIn : SafeStructArrayIn<ExtensionIn>
    {
        public ExtensionsIn(ExtensionIn[] extensions) : base(extensions)
        {
        }
    }
}
