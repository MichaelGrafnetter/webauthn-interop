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
        private IntPtr _globalHmacSalt;

        /// <summary>
        /// List of credentials with HMAC secret SALT.
        /// </summary>
        private CredentialsWithHmacSecretSaltIn _credWithHmacSecretSaltList;

        public HmacSecretSaltValuesIn(HmacSecretSaltIn globalHmacSalt, CredentialWithHmacSecretSaltIn[] credsWithHmacSecretSalt)
        {
            if (globalHmacSalt != null)
            {
                _globalHmacSalt = Marshal.AllocHGlobal(Marshal.SizeOf<HmacSecretSaltIn>());
                Marshal.StructureToPtr(globalHmacSalt, _globalHmacSalt, false);
            }

            _credWithHmacSecretSaltList = new CredentialsWithHmacSecretSaltIn(credsWithHmacSecretSalt);
        }

        public bool HasGlobalHmacSalt => _globalHmacSalt != IntPtr.Zero;

        public void Dispose()
        {
            if(this._globalHmacSalt != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(this._globalHmacSalt);
                this._globalHmacSalt = IntPtr.Zero;
            }

            _credWithHmacSecretSaltList?.Dispose();
            _credWithHmacSecretSaltList = null;
        }
    }
}
