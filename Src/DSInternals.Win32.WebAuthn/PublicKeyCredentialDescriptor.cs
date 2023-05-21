using System;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn
{
    public class PublicKeyCredentialDescriptor
    {
        /// <summary>
        /// This member contains the type of the public key credential the caller is referring to.
        /// </summary>
        public string Type
        {
            get;
        }

        /// <summary>
        /// This member contains the credential ID of the public key credential the caller is referring to.
        /// </summary>
        public byte[] Id
        {
            get;
        }

        /// <summary>
        /// This member contains a hint as to how the client might communicate with the managing authenticator of the public key credential the caller is referring to.
        /// </summary>
        public AuthenticatorTransport Transports
        {
            get;
        }

        public PublicKeyCredentialDescriptor(
            byte[] id,
            AuthenticatorTransport transports = AuthenticatorTransport.NoRestrictions,
            string type = ApiConstants.CredentialTypePublicKey)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Transports = transports;
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }
    }
}
