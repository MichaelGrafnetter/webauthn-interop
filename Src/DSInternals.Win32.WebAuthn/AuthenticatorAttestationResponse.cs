using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.FIDO;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Represents an authenticator attestation response.
    /// </summary>
    public class AuthenticatorAttestationResponse : AuthenticatorResponse
    {
        /// <summary>
        /// The attestation object returned by the authenticator (Base64Url encoded).
        /// </summary>
        [JsonPropertyName("attestationObject")]
        [JsonConverter(typeof(Base64UrlConverter))]
        public byte[] AttestationObject { get; set; }

        /// <summary>
        /// The authenticator data if provided separately (Base64Url encoded).
        /// </summary>
        [JsonPropertyName("authenticatorData")]
        [JsonConverter(typeof(Base64UrlConverter))]
        public byte[]? AuthenticatorData { get; set; }

        /// <summary>
        /// The parsed authenticator data structure.
        /// </summary>
        [JsonIgnore]
        public AuthenticatorData? AuthenticatorDataParsed => AuthenticatorData != null ? FIDO.AuthenticatorData.Parse(AuthenticatorData) : null;

        /// <summary>
        /// Optional transports hint provided by the authenticator.
        /// </summary>
        [JsonPropertyName("transports")]
        public string[]? Transports { get; set; }

        /// <summary>
        /// Optional public key (Base64Url encoded).
        /// </summary>
        [JsonPropertyName("publicKey")]
        [JsonConverter(typeof(Base64UrlConverter))]
        public byte[]? PublicKey { get; set; }

        /// <summary>
        /// Optional public key algorithm identifier.
        /// </summary>
        [JsonPropertyName("publicKeyAlgorithm")]
        public int? PublicKeyAlgorithm { get; set; }

        public static AuthenticatorAttestationResponse? FromJson(string json)
        {
            try
            {
                return JsonSerializer.Deserialize(json, WebAuthnJsonContext.Default.AuthenticatorAttestationResponse);
            }
            catch (JsonException ex)
            {
                Debug.WriteLine($"AuthenticatorAttestationResponse JSON deserialization error: {ex.Message}");
                return null;
            }
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.AuthenticatorAttestationResponse);
        }
    }
}
