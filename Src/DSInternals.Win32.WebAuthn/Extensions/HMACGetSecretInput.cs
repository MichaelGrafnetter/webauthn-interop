using System.Text.Json.Serialization;

using System.Text.Json;

namespace DSInternals.Win32.WebAuthn
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
        [JsonPropertyName("salt1")]
        [JsonConverter(typeof(Base64UrlConverter))]
        public byte[]? Salt1 { get; set; }

        /// <summary>
        ///  Optional additional 32-byte random data. Used when the platform wants to roll over the symmetric secret in one operation.
        /// </summary>
        [JsonPropertyName("salt2")]
        [JsonConverter(typeof(Base64UrlConverter))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public byte[]? Salt2 { get; set; }

        /// <summary>
        /// Deserializes a JSON string into hmac-secret extension inputs.
        /// </summary>
        /// <param name="json">JSON representation of hmac-secret extension inputs.</param>
        /// <returns>hmac-secret extension inputs if deserialization is successful; otherwise, null.</returns>
        public static HMACGetSecretInput? FromJson(string json)
        {
            return WebAuthnJsonModel.FromJson(json, WebAuthnJsonContext.Default.HMACGetSecretInput);
        }

        /// <summary>
        /// Serializes the hmac-secret extension inputs to JSON.
        /// </summary>
        /// <returns>JSON representation of these hmac-secret extension inputs.</returns>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.HMACGetSecretInput);
        }
    }
}
