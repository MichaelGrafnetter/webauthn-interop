using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.Interop;
using System;

namespace DSInternals.Win32.WebAuthn
{
    public class PublicKeyCredentialParameter
    {
        /// <summary>
        /// Specifies the cryptographic signature algorithm used for the new credential.
        /// </summary>
        [JsonPropertyName("alg")]
        public Algorithm Algorithm { get; private set; }

        /// <summary>
        /// Specifies the type of credential to be created. The only supported value is public-key.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; private set; }

        [JsonConstructor]

        public PublicKeyCredentialParameter(Algorithm algorithm, string type)
        {
            if (type != ApiConstants.CredentialTypePublicKey)
            {
                // Only public-key credentials are supported.
                throw new ArgumentOutOfRangeException(nameof(type));
            }

            this.Algorithm = algorithm;
            this.Type = type;
        }
    }
}
