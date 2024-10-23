using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn
{
    public class OktaWebauthnAttestationResponse
    {
        /// <summary>
        /// Contains the WebAuthn public key credential information being registered.
        /// </summary>
        [JsonIgnore]
        public PublicKeyCredential PublicKeyCredential { get; set; }

        /// <summary>
        /// ID of an existing Okta user.
        /// </summary>
        [JsonIgnore]
        public string UserId { get; set; }

        /// <summary>
        /// ID of an existing user Factor.
        /// </summary>
        [JsonIgnore]
        public string FactorId { get; set; }

        /// <summary>
        /// ID of an existing user Factor.
        /// </summary>
        [JsonPropertyName("attestation")]
        [JsonConverter(typeof(Base64UrlConverter))]
        public byte[] Attestation => PublicKeyCredential.AuthenticatorResponse.AttestationObject;

        /// <summary>
        /// ID of an existing user Factor.
        /// </summary>
        [JsonPropertyName("clientData")]
        [JsonConverter(typeof(Base64UrlConverter))]
        public byte[] ClientData => PublicKeyCredential.AuthenticatorResponse.ClientDataJson;

        public OktaWebauthnAttestationResponse(PublicKeyCredential publicKeyCredential, byte[] userId, string factorId)
        {
            PublicKeyCredential = publicKeyCredential;
            UserId = Base64UrlConverter.ToBase64UrlString(userId);
            FactorId = factorId;
        }

        override public string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
