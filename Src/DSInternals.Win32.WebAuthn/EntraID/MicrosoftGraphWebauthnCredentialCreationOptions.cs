using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>OData Object: https://graph.microsoft.com/beta/$metadata#microsoft.graph.webauthnCredentialCreationOptions</remarks>
    public class MicrosoftGraphWebauthnCredentialCreationOptions
    {
        [JsonPropertyName("challengeTimeoutDateTime")]
        public DateTime ChallengeTimeout { get; set; }

        [JsonPropertyName("publicKey")]
        public PublicKeyCredentialCreationOptions PublicKeyOptions { get; set; }

        public static MicrosoftGraphWebauthnCredentialCreationOptions Create(string json)
        {
            if(string.IsNullOrEmpty(json))
            {
                throw new ArgumentNullException(nameof(json));
            }

            return JsonSerializer.Deserialize<MicrosoftGraphWebauthnCredentialCreationOptions>(json);
        }
    }
}
