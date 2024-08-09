using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn
{
    public class AuthenticatorSelection
    {
        /// <summary>
        /// Preferred attachment modality.
        /// </summary>
        [JsonPropertyName("authenticatorAttachment")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public AuthenticatorAttachment AuthenticatorAttachment { get; set; }

        /// <summary>
        /// Requirement to verify the user is present during credential provisioning.
        /// </summary>
        [JsonPropertyName("userVerification")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public UserVerificationRequirement UserVerificationRequirement { get; set; }

        /// <summary>
        /// Preferred client-side credential discoverability.
        /// </summary>
        [JsonPropertyName("requireResidentKey")]
        public bool RequireResidentKey { get; set; }
    }
}
