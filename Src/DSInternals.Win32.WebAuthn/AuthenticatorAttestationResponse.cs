using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// The AuthenticatorAttestationResponse class represents the authenticator's response
    /// to a client’s request for the creation of a new public key credential.
    /// It contains information about the new credential that can be used to identify it for later use,
    /// and metadata that can be used by the WebAuthn Relying Party to assess the characteristics
    /// of the credential during registration.
    /// </summary>
    public class AuthenticatorAttestationResponse : AuthenticatorResponse
    {
        /// <summary>
        /// This attribute contains an attestation object, which is opaque to,
        /// and cryptographically protected against tampering by, the client.
        /// The attestation object contains both authenticator data and an attestation statement.
        /// </summary>
        [JsonPropertyName("attestationObject")]
        [JsonConverter(typeof(Base64UrlConverter))]
        public byte[] AttestationObject { get; set; }
    }
}
