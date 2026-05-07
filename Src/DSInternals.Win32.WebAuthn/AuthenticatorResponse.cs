using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Authenticators respond to Relying Party requests by returning an object derived from the AuthenticatorResponse class.
    /// </summary>
    public abstract class AuthenticatorResponse
    {
        /// <summary>
        /// The JSON-serialized client data passed to the authenticator (Base64Url encoded).
        /// </summary>
        [JsonPropertyName("clientDataJSON")]
        [JsonConverter(typeof(Base64UrlConverter))]
        public byte[]? ClientDataJson { get; init; }
    }
}
