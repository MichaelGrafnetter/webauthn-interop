using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// X.509 Certificate
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_X5C.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class Certificate : VariableArray<byte>
    {
        // TODO: Certificate Conversion
        private Certificate() : base() { }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal sealed class Certificates : VariableArray<Certificate>
    {
        private Certificates() : base() { }
    }
}
