using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn
{
    public abstract class WebauthnAttestationResponse
    {
        [JsonIgnore()]
        public abstract PublicKeyCredential PublicKeyCred { get; set; }
    }
}
