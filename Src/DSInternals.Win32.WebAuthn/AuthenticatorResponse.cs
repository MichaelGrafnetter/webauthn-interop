using System.Text;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Authenticators respond to Relying Party requests by returning an object derived from the AuthenticatorResponse class.
    /// </summary>
    public abstract class AuthenticatorResponse
    {
        /// <summary>
        /// The UTF-8 encoded JSON-serialized client data passed to the authenticator (Base64Url encoded in JSON).
        /// </summary>
        [JsonPropertyName("clientDataJSON")]
        [JsonConverter(typeof(Base64UrlConverter))]
        public byte[]? ClientData { get; init; }

        /// <summary>
        /// The clientDataJSON value decoded as a UTF-8 string.
        /// </summary>
        [JsonIgnore]
        public string? ClientDataJson => ClientData is null ? null : Encoding.UTF8.GetString(ClientData);
    }
}
