using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// The structure that contains the SALT values for the HMAC secret.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_HMAC_SECRET_SALT_VALUES.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal class HmacSecretSaltValuesIn : IDisposable
    {
        /// <summary>
        /// The global HMAC SALT.
        /// </summary>
        private HmacSecretSaltIn _globalHmacSalt;

        /// <summary>
        /// Size of _credWithHmacSecretSaltList.
        /// </summary>
        private int _credWithHmacSecretSaltListSize;

        /// <summary>
        /// List of credentials with HMAC secret SALT.
        /// </summary>
        private PWEBAUTHN_CRED_WITH_HMAC_SECRET_SALT _credWithHmacSecretSaltList;

        public HmacSecretSaltValuesIn() { }

        public void Dispose()
        {

        }
    }
}
