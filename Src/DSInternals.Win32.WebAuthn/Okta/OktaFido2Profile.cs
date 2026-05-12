using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.Okta
{
    /// <summary>
    /// Represents the Okta profile metadata associated with a FIDO2 authentication method.
    /// </summary>
    public class OktaFido2Profile
    {
        /// <summary>
        /// ID for the Factor credential
        /// </summary>
        [JsonPropertyName("credentialId")]
        public string CredentialId { get; }

        /// <summary>
        /// U2F appId string
        /// </summary>
        [JsonPropertyName("appId")]
        public object AppId { get; }

        /// <summary>
        /// Undocumented
        /// </summary>
        [JsonPropertyName("version")]
        public object Version { get; }

        /// <summary>
        /// Human-readable name of the authenticator
        /// </summary>
        [JsonPropertyName("authenticatorName")]
        public string AuthenticatorName { get; }

        /// <summary>
        /// Undocumented
        /// </summary>
        [JsonPropertyName("presetPinAvailable")]
        public object PresetPinAvailable { get; }

        /// <summary>
        /// Undocumented
        /// </summary>
        [JsonPropertyName("fulfillmentProvider")]
        public object FulfillmentProvider { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OktaFido2Profile"/> class.
        /// </summary>
        [JsonConstructor]
        public OktaFido2Profile(
            string credentialId,
            object appId,
            object version,
            string authenticatorName,
            object presetPinAvailable,
            object fulfillmentProvider
        )
        {
            this.CredentialId = credentialId;
            this.AppId = appId;
            this.Version = version;
            this.AuthenticatorName = authenticatorName;
            this.PresetPinAvailable = presetPinAvailable;
            this.FulfillmentProvider = fulfillmentProvider;
        }
    }
}
