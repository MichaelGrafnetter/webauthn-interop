using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Base type for provider-specific WebAuthn attestation response payloads.
    /// </summary>
    public abstract class WebauthnAttestationResponse
    {
        /// <summary>
        /// Gets or sets the underlying WebAuthn public key credential.
        /// </summary>
        [JsonIgnore()]
        public abstract AttestationPublicKeyCredential PublicKeyCred { get; set; }
    }
}
