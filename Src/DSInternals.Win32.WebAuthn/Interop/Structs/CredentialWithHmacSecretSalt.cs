using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop.Structs
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
        private HmacSecretSaltIn _hmacSecretSalt;

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

        public HmacSecretSaltIn HmacSecretSalt
        {
            set
            {
                _hmacSecretSalt?.Dispose();
                _hmacSecretSalt = value;
            }
        }

        public CredentialWithHmacSecretSaltIn(byte[] credentialId, byte[] first, byte[] second)
        {
            // TODO: Validate that all parameters are present
            this.CredentialId = credentialId;
            this.HmacSecretSalt = new HmacSecretSaltIn(first, second);
            // TODO: Use tuples here
        }

        public void Dispose()
        {
            _credentialId?.Dispose();
            _credentialId = null;

            _hmacSecretSalt?.Dispose();
            _hmacSecretSalt = null;
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
