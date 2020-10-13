using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace WebAuthN.Interop
{
    /// <summary>
    /// X.509 Certificate
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_X5C.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class Certificate : SafeByteArrayOut
    {
        private Certificate() : base() { }

        public X509Certificate2 Cert => new X509Certificate2(Data);
    }

    [StructLayout(LayoutKind.Sequential)]
    internal sealed class Certificates : SafeStructArrayOut<Certificate>
    {
        private Certificates() : base() { }
    }
}
