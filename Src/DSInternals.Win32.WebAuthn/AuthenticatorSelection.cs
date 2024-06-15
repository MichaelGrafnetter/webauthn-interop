using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn
{
    public class AuthenticatorSelection
    {
        [JsonPropertyName("authenticatorAttachment")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public AuthenticatorAttachment AuthenticatorAttachment { get; set; }

        [JsonPropertyName("userVerification")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public UserVerificationRequirement UserVerificationRequirement { get; set; }

        [JsonPropertyName("requireResidentKey")]
        public bool RequireResidentKey { get; set; }
    }
}
