using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.Okta
{
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
        public byte[] Attestation => PublicKeyCred.AuthenticatorResponse.AttestationObject;

        /// <summary>
        /// ID of an existing user Factor.
        /// </summary>
        [JsonPropertyName("clientData")]
        [JsonConverter(typeof(Base64UrlConverter))]
        public byte[] ClientData => PublicKeyCred.AuthenticatorResponse.ClientDataJson;

        public OktaWebauthnAttestationResponse(PublicKeyCredential publicKeyCredential, byte[] userId, string factorId)
        {
            PublicKeyCred = publicKeyCredential;
            UserId = Base64UrlConverter.ToBase64UrlString(userId);
            FactorId = factorId;
        }

        override public string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
