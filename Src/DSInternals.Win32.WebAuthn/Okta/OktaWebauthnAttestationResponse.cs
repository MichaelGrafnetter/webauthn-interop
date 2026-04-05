using System.Buffers.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.Okta
{
    /// <summary>
    /// Okta-specific payload used to submit a WebAuthn attestation result.
    /// </summary>
    public class OktaWebauthnAttestationResponse : WebauthnAttestationResponse
    {
        /// <summary>
        /// Contains the WebAuthn public key credential information being registered.
        /// </summary>
        [JsonIgnore]
        public override PublicKeyCredential PublicKeyCred { get; set; }

        /// <summary>
        /// ID of an existing Okta user.
        /// </summary>
        /// <summary>
        /// Gets or sets the Okta user identifier.
        /// </summary>
        [JsonIgnore]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the Okta factor identifier.
        /// </summary>
        [JsonIgnore]
        public string FactorId { get; set; }

        /// <summary>
        /// Gets the attestation object bytes.
        /// </summary>
        [JsonPropertyName("attestation")]
        [JsonConverter(typeof(Base64UrlConverter))]
        public byte[] Attestation => (PublicKeyCred.Response as AuthenticatorAttestationResponse)?.AttestationObject;

        /// <summary>
        /// Gets the client data JSON bytes.
        /// </summary>
        [JsonPropertyName("clientData")]
        [JsonConverter(typeof(Base64UrlConverter))]
        public byte[] ClientData => PublicKeyCred.Response.ClientDataJson;

        /// <summary>
        /// Initializes a new Okta attestation response payload.
        /// </summary>
        /// <param name="publicKeyCredential">WebAuthn credential returned by the authenticator.</param>
        /// <param name="userId">Raw Okta user identifier bytes.</param>
        /// <param name="factorId">Okta factor identifier.</param>
        public OktaWebauthnAttestationResponse(PublicKeyCredential publicKeyCredential, byte[] userId, string factorId)
        {
            PublicKeyCred = publicKeyCredential;
            UserId = Base64Url.EncodeToString(userId);
            FactorId = factorId;
        }

        /// <summary>
        /// Serializes this payload to JSON.
        /// </summary>
        /// <returns>JSON representation expected by Okta APIs.</returns>
        override public string ToString()
        {
            return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.OktaWebauthnAttestationResponse);
        }
    }
}
