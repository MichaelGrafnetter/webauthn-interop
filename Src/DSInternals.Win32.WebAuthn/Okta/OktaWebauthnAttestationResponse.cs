using System;
using System.Buffers.Text;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.Okta
{
    /// <summary>
    /// Okta-specific payload used to submit a WebAuthn attestation result.
    /// </summary>
    public class OktaWebauthnAttestationResponse
    {
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
        /// Gets or sets the credential identifier returned by the authenticator.
        /// </summary>
        [JsonIgnore]
        public byte[] CredentialId { get; set; }

        /// <summary>
        /// Gets or sets the attestation object bytes.
        /// </summary>
        [JsonPropertyName("attestation")]
        [JsonConverter(typeof(Base64UrlConverter))]
        public byte[] Attestation { get; set; }

        /// <summary>
        /// Gets or sets the client data JSON bytes.
        /// </summary>
        [JsonPropertyName("clientData")]
        [JsonConverter(typeof(Base64UrlConverter))]
        public byte[]? ClientData { get; set; }

        /// <summary>
        /// The clientData value decoded as a UTF-8 string.
        /// </summary>
        [JsonIgnore]
        public string? ClientDataJson => ClientData is null ? null : Encoding.UTF8.GetString(ClientData);

        /// <summary>
        /// Initializes a new Okta attestation response payload.
        /// </summary>
        /// <param name="publicKeyCredential">WebAuthn credential returned by the authenticator.</param>
        /// <param name="userId">Okta user identifier (20-character Base64Url string).</param>
        /// <param name="factorId">Okta factor identifier.</param>
        public OktaWebauthnAttestationResponse(AttestationPublicKeyCredential publicKeyCredential, string userId, string factorId)
        {
            ArgumentNullException.ThrowIfNull(publicKeyCredential);
            ArgumentNullException.ThrowIfNull(userId);
            ArgumentNullException.ThrowIfNull(factorId);

            CredentialId = publicKeyCredential.Id;
            Attestation = publicKeyCredential.Response.AttestationObject;
            ClientData = publicKeyCredential.Response.ClientData;
            UserId = userId;
            FactorId = factorId;
        }

        /// <summary>
        /// Initializes a new Okta attestation response payload from a raw user identifier.
        /// </summary>
        /// <param name="publicKeyCredential">WebAuthn credential returned by the authenticator.</param>
        /// <param name="userId">Raw Okta user identifier bytes; encoded to a Base64Url string.</param>
        /// <param name="factorId">Okta factor identifier.</param>
        public OktaWebauthnAttestationResponse(AttestationPublicKeyCredential publicKeyCredential, byte[] userId, string factorId)
            : this(publicKeyCredential, Base64Url.EncodeToString(userId ?? throw new ArgumentNullException(nameof(userId))), factorId)
        {
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
