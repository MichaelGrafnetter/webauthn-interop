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
        private CredentialsWithHmacSecretSaltIn _credWithHmacSecretSaltList;

        public HmacSecretSaltValuesIn(HmacSecretSaltIn globalHmacSalt, CredentialWithHmacSecretSaltIn[] credsWithHmacSecretSalt) {
            this._globalHmacSalt = globalHmacSalt;
            this._credWithHmacSecretSaltListSize = credsWithHmacSecretSalt?.Length ?? 0;
            this._credWithHmacSecretSaltList = new CredentialsWithHmacSecretSaltIn(credsWithHmacSecretSalt);
        }

        public void Dispose()
        {
            _globalHmacSalt?.Dispose();
            _globalHmacSalt = null;

            _credWithHmacSecretSaltList?.Dispose();
            _credWithHmacSecretSaltList = null;
        }
    }
}
