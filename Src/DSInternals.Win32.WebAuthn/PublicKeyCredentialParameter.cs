using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Describes an acceptable public key credential algorithm and type pair.
    /// </summary>
    public sealed class PublicKeyCredentialParameter
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

        /// <summary>
        /// Initializes a new credential parameter instance.
        /// </summary>
        /// <param name="algorithm">COSE signature algorithm.</param>
        /// <param name="type">Credential type. Must be <c>public-key</c>.</param>
        [JsonConstructor]
        public PublicKeyCredentialParameter(Algorithm algorithm, string type)
        {
            ArgumentOutOfRangeException.ThrowIfNotEqual(type, ApiConstants.PublicKeyCredentialType);

            this.Algorithm = algorithm;
            this.Type = type;
        }

        /// <summary>
        /// Serializes the credential parameter to JSON.
        /// </summary>
        /// <returns>JSON representation of this credential parameter.</returns>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.PublicKeyCredentialParameter);
        }
    }
}
