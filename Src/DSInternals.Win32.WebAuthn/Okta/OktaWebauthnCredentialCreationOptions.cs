using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn
{
    public class Embedded
    {
        [JsonPropertyName("activation")]
        public PublicKeyCredentialCreationOptions PublicKeyOptions { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class OktaWebauthnCredentialCreationOptions
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("factorType")]
        public string FactorType { get; set; }
        [JsonPropertyName("provider")]
        public string Provider { get; set; }
        [JsonPropertyName("vendorName")]
        public string VendorName { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("created")]
        public string Created { get; set; }
        [JsonPropertyName("lastUpdated")]
        public string LastUpdated { get; set; }
        
        /// <summary>
        /// Defines public key options for the creation of a new WebAuthn public key credential.
        /// </summary>
        [JsonPropertyName("_embedded")]
        public Embedded Embedded { get; set; }

        public static OktaWebauthnCredentialCreationOptions Create(string json)
        {
            if(string.IsNullOrEmpty(json))
            {
                throw new ArgumentNullException(nameof(json));
            }

            return JsonSerializer.Deserialize<OktaWebauthnCredentialCreationOptions>(json);
        }
    }
}
