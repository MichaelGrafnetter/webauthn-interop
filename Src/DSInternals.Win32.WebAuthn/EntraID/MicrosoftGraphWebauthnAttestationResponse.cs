using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn
{
    public class MicrosoftGraphWebauthnAttestationResponse
    {
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("publicKeyCredential")]
        public PublicKeyCredential PublicKeyCredential { get; set; }

        public MicrosoftGraphWebauthnAttestationResponse(PublicKeyCredential publicKeyCredential, string displayName)
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
