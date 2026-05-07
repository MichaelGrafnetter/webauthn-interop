#if !NET7_0_OR_GREATER
using System.Security.Cryptography;

namespace DSInternals.Win32.WebAuthn.Cryptography;

public static class SHA256Polyfill
{
    extension(SHA256)
    {
        /// <summary>
        /// The hash size produced by the SHA-256 algorithm, in bytes.
        /// </summary>
        public static int HashSizeInBytes => 32;
    }
}
#endif
