using System.Text.Json;
using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Represents a WebAuthn public key credential.
    /// </summary>
    public class PublicKeyCredential
    {
        /// <summary>
        /// Authenticator attachment modality used for this credential.
        /// </summary>
        [JsonPropertyName("authenticatorAttachment")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AuthenticatorAttachment? AuthenticatorAttachment { get; set; }

        /// <summary>
        /// Base64Url-encoded credential identifier.
        /// </summary>
        [JsonPropertyName("id")]
        [JsonConverter(typeof(Base64UrlConverter))]
        public byte[] Id { get; set; }

        /// <summary>
        /// Raw credential identifier bytes.
        /// </summary>
        [JsonPropertyName("rawId")]
        [JsonConverter(typeof(Base64UrlConverter))]
        public byte[] RawId { get; set; }

        /// <summary>
        /// Authenticator response payload.
        /// </summary>
        [JsonPropertyName("response")]
        public AuthenticatorResponse Response { get; set; }

        /// <summary>
        /// Credential type string, typically <c>public-key</c>.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = ApiConstants.PublicKeyCredentialType;

        /// <summary>
        /// Outputs of client extension processing.
        /// </summary>
        [JsonPropertyName("clientExtensionResults")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AuthenticationExtensionsClientOutputs? ClientExtensionResults { get; set; }

        /// <summary>
        /// Serializes the credential to JSON.
        /// </summary>
        /// <returns>JSON representation of this credential.</returns>
        public override string ToString() => JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.PublicKeyCredential);
    }
}
