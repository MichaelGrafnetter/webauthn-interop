using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// 
    /// </summary>
    public class OktaWebauthnCredentialCreationOptions
    {
        /// <summary>
        /// Defines public key options for the creation of a new WebAuthn public key credential.
        /// </summary>
        [JsonPropertyName("activation")]
        public PublicKeyCredentialCreationOptions PublicKeyOptions { get; set; }

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
