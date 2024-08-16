using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <remarks>Corresponds to WEBAUTHN_CRED_WITH_HMAC_SECRET_SALT.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal class CredentialWithHmacSecretSaltIn : IDisposable
    {
        /// <summary>
        /// Size of the credential ID.
        /// </summary>
        private int _credentialIdLength;

        /// <summary>
        /// Credential ID.
        /// </summary>
        private ByteArrayIn _credentialId;

        /// <summary>
        /// PRF Values for above credential
        /// </summary>
        private IntPtr _hmacSecretSalt;

        /// <summary>
        /// Credential ID.
        /// </summary>
        public byte[] CredentialId
        {
            get
            {
                return _credentialId?.Read(_credentialIdLength);
            }
            set
            {
                // Get rid of any previous value first
                _credentialId?.Dispose();

                // Now replace the previous value with a new one
                _credentialIdLength = value?.Length ?? 0;
                _credentialId = new ByteArrayIn(value);
            }
        }

        /// <summary>
        /// PRF Values for above credential
        /// </summary>
        public HmacSecretSaltIn HmacSecretSalt
        {
            set
            {
                if(value != null)
                {
                    if (_hmacSecretSalt == IntPtr.Zero)
                    {
                        _hmacSecretSalt = Marshal.AllocHGlobal(Marshal.SizeOf<HmacSecretSaltIn>());
                    }

                    Marshal.StructureToPtr<HmacSecretSaltIn>(value, _hmacSecretSalt, false);
                }
                else
                {
                    FreeHmacSecretSalt();
                }
            }
        }

        public CredentialWithHmacSecretSaltIn(byte[] credentialId, HmacSecretSaltIn hmacSecretSalt)
        {
            this.CredentialId = credentialId;
            this.HmacSecretSalt = hmacSecretSalt;
        }

        private void FreeHmacSecretSalt()
        {
            if (_hmacSecretSalt != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_hmacSecretSalt);
                _hmacSecretSalt = IntPtr.Zero;
            }
        }

        public void Dispose()
        {
            _credentialId?.Dispose();
            _credentialId = null;

            FreeHmacSecretSalt();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal sealed class CredentialsWithHmacSecretSaltIn : SafeStructArrayIn<CredentialWithHmacSecretSaltIn>
    {
        public CredentialsWithHmacSecretSaltIn(CredentialWithHmacSecretSaltIn[] credsWithHmacSecretSalt) : base(credsWithHmacSecretSalt)
        {
        }
    }
}
