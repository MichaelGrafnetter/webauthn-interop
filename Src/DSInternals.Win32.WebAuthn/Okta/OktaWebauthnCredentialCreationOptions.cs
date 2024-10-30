using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.Okta
{
    public class Embedded
    {
        /// <summary>
        /// Defines public key options for the creation of a new WebAuthn public key credential.
        /// </summary>
        [JsonPropertyName("activation")]
        public PublicKeyCredentialCreationOptions PublicKeyOptions { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class OktaWebauthnCredentialCreationOptions
    {
        /// <summary>
        /// The factor id of the Okta factor being registered.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// A wrapper around the PublicKeyCredentialCreationOptions
        /// </summary>
        [JsonPropertyName("_embedded")]
        public Embedded Embedded { get; set; }

        public PublicKeyCredentialCreationOptions PublicKeyOptions => Embedded.PublicKeyOptions;

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
