using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.Interop;
using System;

namespace DSInternals.Win32.WebAuthn
{
    public class PublicKeyCredentialParameter
    {
        [JsonPropertyName("alg")]
        public Algorithm Algorithm { get; private set; }

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
