using System;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace DSInternals.Win32.WebAuthn.Okta
{
    public class OktaProfile
    {
        [JsonConstructor]
        public OktaProfile(
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
    }

    public class OktaFido2AuthenticationMethod
    {
        [JsonConstructor]
        public OktaFido2AuthenticationMethod(
            string id,
            string factorType,
            string provider,
            string vendorName,
            string status,
            DateTime? created,
            DateTime? lastUpdated,
            OktaProfile profile
        )
        {
            this.Id = id;
            this.FactorType = factorType;
            this.Provider = provider;
            this.VendorName = vendorName;
            this.Status = status;
            this.Created = created;
            this.LastUpdated = lastUpdated;
            this.Profile = profile;
        }

        public static OktaFido2AuthenticationMethod FromJsonString(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                throw new ArgumentNullException(nameof(json));
            }

            return JsonSerializer.Deserialize<OktaFido2AuthenticationMethod>(json);
        }
        /// <summary>
        /// ID of the Factor
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// Type of Factor
        /// </summary>
        [JsonPropertyName("factorType")]
        public string FactorType { get; }

        /// <summary>
        /// Provider for the Factor
        /// </summary>
        [JsonPropertyName("provider")]
        public string Provider { get; }

        /// <summary>
        /// Name of the Factor vendor. This is usually the same as the provider except for On-Prem MFA where it depends on administrator settings.
        /// </summary>
        [JsonPropertyName("vendorName")]
        public string VendorName { get; }

        /// <summary>
        /// Status of the Factor
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; }

        /// <summary>
        /// Timestamp when the Factor was enrolled
        /// </summary>
        [JsonPropertyName("created")]
        public DateTime? Created { get; }

        /// <summary>
        /// Timestamp when the Factor was last updated
        /// </summary>
        [JsonPropertyName("lastUpdated")]
        public DateTime? LastUpdated { get; }

        /// <summary>
        /// Specific attributes related to the Factor
        /// </summary>
        [JsonPropertyName("profile")]
        public OktaProfile Profile { get; }
    }
}
