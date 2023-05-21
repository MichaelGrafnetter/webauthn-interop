using System.Security.Cryptography.X509Certificates;
using DSInternals.Win32.WebAuthn.FIDO;
using Newtonsoft.Json;

namespace DSInternals.Win32.WebAuthn.ActiveDirectory
{
    /// <summary>
    /// Represents a FIDO2 key stored in AAD.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class FidoKeyMaterial
    {
        // Certificates must contain at least a 1024-bit RSA key.
        private const int MinCertificateSize = 128;

        /// <summary>
        /// Version is an integer that specifies the version of the structure.
        /// </summary>
        [JsonProperty("version")]
        public int Version
        {
            get;
            private set;
        }

        /// <summary>
        /// AuthData is a WebAuthn authenticator data structure.
        /// <see>https://www.w3.org/TR/webauthn/#sec-authenticator-data</see>
        /// </summary>
        [JsonProperty("authData")]
        public byte[] AuthenticatorDataRaw
        {
            get;
            private set;
        }

        /// <summary>
        /// X5c is an array of attestation certificates associated with the authenticator.
        /// </summary>
        [JsonProperty("x5c")]
        public byte[][] AttestationCertificatesRaw
        {
            get;
            private set;
        }

        /// <summary>
        /// Display name is a user provided string which can help the user differentiate between multiple registered authenticators.
        /// </summary>
        [JsonProperty("displayName")]
        public string DisplayName
        {
            get;
            private set;
        }

        /// <summary>
        /// Attestation certificates can be helpful for establishing a chain of trust.
        /// </summary>
        public X509Certificate2Collection AttestationCertificates
        {
            get
            {
                X509Certificate2Collection certificates = new X509Certificate2Collection();

                foreach (byte[] binaryCertificate in this.AttestationCertificatesRaw)
                {
                    // In AAD, some x5c values are not really certificates, so we need to filter them out.
                    // These invalid values typically have 32 bytes (SHA-256?), which is far less than the expected certificate size.
                    if (binaryCertificate?.Length > MinCertificateSize)
                    {
                        certificates.Add(new X509Certificate2(binaryCertificate));
                    }
                }

                return certificates;
            }
        }

        /// <summary>
        /// Authenticator data contains information about the registered authenticator device.
        /// </summary>
        public AuthenticatorData AuthenticatorData
        {
            get
            {
                return new AuthenticatorData(this.AuthenticatorDataRaw);
            }
        }
    }
}
