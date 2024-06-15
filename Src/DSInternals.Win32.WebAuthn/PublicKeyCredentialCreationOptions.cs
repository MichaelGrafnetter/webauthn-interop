using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn
{
    public sealed class PublicKeyCredentialCreationOptions
    {
        [JsonPropertyName("attestation")]
        public AttestationConveyancePreference Attestation { get; set; } = AttestationConveyancePreference.None;

        [JsonPropertyName("authenticatorSelection")]
        public AuthenticatorSelection AuthenticatorSelection { get; set; }

        [JsonPropertyName("challenge")]
        [JsonConverter(typeof(Base64UrlConverter))]
        public byte[] Challenge { get; set; }

        [JsonPropertyName("excludeCredentials")]
        public IReadOnlyList<PublicKeyCredentialDescriptor> ExcludeCredentials { get; set; }

        [JsonPropertyName("extensions")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AuthenticationExtensionsClientInputs Extensions { get; set; }

        [JsonPropertyName("pubKeyCredParams")]
        public IReadOnlyList<PublicKeyCredentialParameter> PublicKeyCredentialParameters { get; set; }

        [JsonPropertyName("rp")]
        public RelyingPartyInformation RelyingParty { get; set; }

        /// <summary>
        /// A numerical hint, in milliseconds, which indicates the time the calling web app is willing to wait for the creation operation to complete.
        /// </summary>
        [JsonPropertyName("timeout")]
        public int TimeoutMilliseconds { get; set; }

        [JsonPropertyName("user")]
        public UserInformation User { get; set; }
    }
}
