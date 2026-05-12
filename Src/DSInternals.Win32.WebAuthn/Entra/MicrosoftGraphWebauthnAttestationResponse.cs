using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.Entra
{
    /// <summary>
    /// Microsoft Graph payload used to submit a WebAuthn attestation result.
    /// </summary>
    public class MicrosoftGraphWebauthnAttestationResponse
    {
        /// <summary>
        /// The display name of the key as given by the user.
        /// </summary>
        [JsonPropertyName("displayName")]
        [JsonRequired]
        public required string DisplayName { get; init; }

        /// <summary>
        /// Graph-shaped credential emitted in the serialized payload.
        /// </summary>
        [JsonPropertyName("publicKeyCredential")]
        [JsonRequired]
        public required MicrosoftGraphAttestationPublicKeyCredential PublicKeyCred { get; init; }

        /// <summary>
        /// Initializes a new Microsoft Graph attestation response payload from a Graph-shaped credential.
        /// </summary>
        /// <param name="publicKeyCredential">Graph-shaped WebAuthn credential.</param>
        /// <param name="displayName">User-provided passkey display name.</param>
        [SetsRequiredMembers]
        [JsonConstructor]
        public MicrosoftGraphWebauthnAttestationResponse(MicrosoftGraphAttestationPublicKeyCredential publicKeyCredential, string displayName)
        {
            PublicKeyCred = publicKeyCredential;
            DisplayName = displayName;
        }

        /// <summary>
        /// Initializes a new Microsoft Graph attestation response payload by adapting a standard
        /// <see cref="AttestationPublicKeyCredential"/> returned by the WebAuthn API.
        /// </summary>
        /// <param name="publicKeyCredential">WebAuthn credential returned by the authenticator.</param>
        /// <param name="displayName">User-provided passkey display name.</param>
        [SetsRequiredMembers]
        public MicrosoftGraphWebauthnAttestationResponse(AttestationPublicKeyCredential publicKeyCredential, string displayName)
            : this(new MicrosoftGraphAttestationPublicKeyCredential(publicKeyCredential), displayName)
        {
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
