using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn
{
    public class PublicKeyCredential
    {
        [JsonPropertyName("id")]
        [JsonConverter(typeof(Base64UrlConverter))]
        public byte[] Id { get; set; }

        [JsonPropertyName("response")]
        public AuthenticatorAttestationResponse AuthenticatorResponse { get; set; }

        [JsonPropertyName("clientExtensionResults")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AuthenticationExtensionsClientOutputs ClientExtensionResults { get; set; }
    }
}
