using System.Text.Json;
using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.Serialization;

namespace DSInternals.Win32.WebAuthn.EntraID
{
    public class MicrosoftGraphWebauthnAttestationResponse : WebauthnAttestationResponse
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
        public override PublicKeyCredential PublicKeyCred { get; set; }

        public MicrosoftGraphWebauthnAttestationResponse(PublicKeyCredential publicKeyCredential, string displayName)
        {
            PublicKeyCred = publicKeyCredential;
            DisplayName = displayName;
        }

        override public string ToString()
        {
            #if NET7_0_OR_GREATER
            return JsonSerializer.Serialize(this, WebAuthnJsonSerializerContext.Default.MicrosoftGraphWebauthnAttestationResponse);
            #else
            return JsonSerializer.Serialize(this);
            #endif
        }
    }
}
