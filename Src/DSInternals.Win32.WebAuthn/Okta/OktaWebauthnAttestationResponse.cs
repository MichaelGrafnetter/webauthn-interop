using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn
{
    public class OktaWebauthnAttestationResponse
    {
        /// <summary>
        /// The display name of the key as given by the user.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Contains the WebAuthn public key credential information being registered.
        /// </summary>
        [JsonPropertyName("publicKeyCredential")]
        public PublicKeyCredential PublicKeyCredential { get; set; }

        public OktaWebauthnAttestationResponse(PublicKeyCredential publicKeyCredential, string displayName)
        {
            PublicKeyCredential = publicKeyCredential;
            DisplayName = displayName;
        }

        override public string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
