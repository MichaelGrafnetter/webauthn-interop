using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// Contains the SALT values for the Hmac-Secret.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_HMAC_SECRET_SALT.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal class HmacSecretSaltIn : IDisposable
    {
        /// <summary>
        /// Size of _first.
        /// </summary>
        private int _firstLength;

        /// <summary>
        /// The first SALT value.
        /// </summary>
        private ByteArrayIn _first;

        /// <summary>
        /// Size of _second.
        /// </summary>
        private int _secondLength;

        /// <summary>
        /// The second SALT value.
        /// </summary>
        private ByteArrayIn _second;

        public HmacSecretSaltIn(byte[] first, byte[] second)
        {
            _first = new ByteArrayIn(first);
            _firstLength = first?.Length ?? 0;

            _second = new ByteArrayIn(second);
            _secondLength = second?.Length ?? 0;
        }

        public void Dispose()
        {
            _first?.Dispose();
            _first = null;

            _second?.Dispose();
            _second = null;
        }
    }

    /// <summary>
    /// Contains the SALT values for the Hmac-Secret.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_HMAC_SECRET_SALT.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal class HmacSecretSaltOut
    {
        /// <summary>
        /// Size of _first.
        /// </summary>
        private int _firstLength;

        /// <summary>
        /// The first SALT value.
        /// </summary>
        private ByteArrayOut _first;

        /// <summary>
        /// Size of _second.
        /// </summary>
        private int _secondLength;

        /// <summary>
        /// The second SALT value.
        /// </summary>
        private ByteArrayOut _second;

        /// <summary>
        /// The first SALT value.
        /// </summary>
        public byte[] First => _first?.Read(_firstLength);

        /// <summary>
        /// The second SALT value.
        /// </summary>
        public byte[] Second => _second?.Read(_secondLength);

        private HmacSecretSaltOut() { }
    }
}
