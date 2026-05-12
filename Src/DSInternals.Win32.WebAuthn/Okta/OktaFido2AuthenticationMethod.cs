using System;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace DSInternals.Win32.WebAuthn.Okta
{
    /// <summary>
    /// Represents an Okta FIDO2 authentication method.
    /// </summary>
    public class OktaFido2AuthenticationMethod
    {
        /// <summary>
        /// ID of the Factor
        /// </summary>
        [JsonPropertyName("id")]
        public string FactorId { get; }

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
        public OktaFido2Profile Profile { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OktaFido2AuthenticationMethod"/> class.
        /// </summary>
        [JsonConstructor]
        public OktaFido2AuthenticationMethod(
            string id,
            string factorType,
            string provider,
            string vendorName,
            string status,
            DateTime? created,
            DateTime? lastUpdated,
            OktaFido2Profile profile
        )
        {
            this.FactorId = id;
            this.FactorType = factorType;
            this.Provider = provider;
            this.VendorName = vendorName;
            this.Status = status;
            this.Created = created;
            this.LastUpdated = lastUpdated;
            this.Profile = profile;
        }

        /// <summary>
        /// Deserializes an Okta FIDO2 authentication method from JSON.
        /// </summary>
        /// <param name="json">The JSON payload returned by Okta.</param>
        /// <returns>The deserialized authentication method.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="json"/> is null or empty.</exception>
        public static OktaFido2AuthenticationMethod FromJsonString(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                throw new ArgumentNullException(nameof(json));
            }

            return JsonSerializer.Deserialize(json, WebAuthnJsonContext.Default.OktaFido2AuthenticationMethod);
        }
    }
}
