using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.EntraID
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>OData Object: https://graph.microsoft.com/beta/$metadata#microsoft.graph.webauthnCredentialCreationOptions</remarks>
    public class MicrosoftGraphWebauthnCredentialCreationOptions : WebauthnCredentialCreationOptions
    {
        /// <summary>
        /// Defines when the challenge in the creation options is no longer valid.
        /// </summary>
        [JsonPropertyName("challengeTimeoutDateTime")]
        public DateTime ChallengeTimeout { get; set; }

        /// <summary>
        /// Defines public key options for the creation of a new WebAuthn public key credential.
        /// </summary>
        [JsonPropertyName("publicKey")]
        public override PublicKeyCredentialCreationOptions PublicKeyOptions { get; set;  }

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
