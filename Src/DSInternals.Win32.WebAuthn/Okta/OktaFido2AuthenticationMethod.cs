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

        [JsonPropertyName("credentialId")]
        public string CredentialId { get; }

        [JsonPropertyName("appId")]
        public object AppId { get; }

        [JsonPropertyName("version")]
        public object Version { get; }

        [JsonPropertyName("authenticatorName")]
        public string AuthenticatorName { get; }

        [JsonPropertyName("presetPinAvailable")]
        public object PresetPinAvailable { get; }

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

        [JsonPropertyName("id")]
        public string Id { get; }

        [JsonPropertyName("factorType")]
        public string FactorType { get; }

        [JsonPropertyName("provider")]
        public string Provider { get; }

        [JsonPropertyName("vendorName")]
        public string VendorName { get; }

        [JsonPropertyName("status")]
        public string Status { get; }

        [JsonPropertyName("created")]
        public DateTime? Created { get; }

        [JsonPropertyName("lastUpdated")]
        public DateTime? LastUpdated { get; }

        [JsonPropertyName("profile")]
        public OktaProfile Profile { get; }
    }
}
