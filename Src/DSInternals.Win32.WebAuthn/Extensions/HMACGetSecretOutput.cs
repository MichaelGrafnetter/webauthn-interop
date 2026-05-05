using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// The response to a hmac get secret request.
    /// </summary>
    /// <see>https://fidoalliance.org/specs/fido-v2.1-ps-20210615/fido-client-to-authenticator-protocol-v2.1-ps-20210615.html#sctn-hmac-secret-extension</see>
    public class HMACGetSecretOutput
    {
        /// <summary>
        /// Output of HMAC-SHA-256(CredRandom, Salt1)
        /// </summary>
        [JsonPropertyName("output1")]
        [JsonConverter(typeof(Base64UrlConverter))]
        [JsonRequired]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public byte[]? Output1 { get; set; }

        /// <summary>
        /// Output of HMAC-SHA-256(CredRandom, Salt2)
        /// </summary>
        [JsonPropertyName("output2")]
        [JsonConverter(typeof(Base64UrlConverter))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public byte[]? Output2 { get; set; }
    }
}
