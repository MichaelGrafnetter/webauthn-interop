using System.Text.Json.Serialization;

using System.Text.Json;

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

        /// <summary>
        /// Deserializes a JSON string into hmac-secret extension outputs.
        /// </summary>
        /// <param name="json">JSON representation of hmac-secret extension outputs.</param>
        /// <returns>hmac-secret extension outputs if deserialization is successful; otherwise, null.</returns>
        public static HMACGetSecretOutput? FromJson(string json)
        {
            return WebAuthnJsonModel.FromJson(json, WebAuthnJsonContext.Default.HMACGetSecretOutput);
        }

        /// <summary>
        /// Serializes the hmac-secret extension outputs to JSON.
        /// </summary>
        /// <returns>JSON representation of these hmac-secret extension outputs.</returns>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.HMACGetSecretOutput);
        }
    }
}
