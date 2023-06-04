﻿namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// The inputs to the hmac secret if it was created during registration.
    /// </summary>
    /// <see>https://fidoalliance.org/specs/fido-v2.1-ps-20210615/fido-client-to-authenticator-protocol-v2.1-ps-20210615.html#sctn-hmac-secret-extension</see>
    public class HMACGetSecretInput
    {
        /// <summary>
        /// 32-byte random data.
        /// </summary>
        public byte[] Salt1 { get; set; }

        /// <summary>
        ///  Optional additional 32-byte random data. Used when the platform wants to roll over the symmetric secret in one operation.
        /// </summary>
        public byte[] Salt2 { get; set; }
    }
}
