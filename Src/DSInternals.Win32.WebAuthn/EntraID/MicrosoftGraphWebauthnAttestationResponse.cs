using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.EntraID
{
    /// <summary>
    /// Microsoft Graph payload used to submit a WebAuthn attestation result.
    /// </summary>
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
        public override AttestationPublicKeyCredential PublicKeyCred { get; set; }

        /// <summary>
        /// Initializes a new Microsoft Graph attestation response payload.
        /// </summary>
        /// <param name="publicKeyCredential">WebAuthn credential returned by the authenticator.</param>
        /// <param name="displayName">User-provided passkey display name.</param>
        public MicrosoftGraphWebauthnAttestationResponse(AttestationPublicKeyCredential publicKeyCredential, string displayName)
        {
            PublicKeyCred = publicKeyCredential;
            DisplayName = displayName;
        }

        /// <summary>
        /// Serializes this payload to JSON.
        /// </summary>
        /// <returns>JSON representation expected by Microsoft Graph APIs.</returns>
        override public string ToString()
        {
            return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.MicrosoftGraphWebauthnAttestationResponse);
        }
    }
}
