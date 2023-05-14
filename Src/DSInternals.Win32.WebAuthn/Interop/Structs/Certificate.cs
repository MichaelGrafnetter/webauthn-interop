using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// X.509 Certificate
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_X5C.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class Certificate
    {
        private int _dataLength;
        private ByteArrayOut _data;

        private Certificate() { }

        public X509Certificate2 Cert
        {
            get
            {
                byte[] data = _data?.Read(_dataLength);
                return data != null ? new X509Certificate2(data) : null;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal sealed class Certificates : SafeStructArrayOut<Certificate>
    {
        internal Certificates(int length, IntPtr nativeCertificates)
        {
            _length = length;
            _nativeArray = nativeCertificates;
        }

        private Certificates() : base() { }
    }
}
