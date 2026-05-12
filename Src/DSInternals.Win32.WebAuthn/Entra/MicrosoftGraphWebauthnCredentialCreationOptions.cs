using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.Entra
{
    /// <summary>
    /// Defines the options for creating a new WebAuthn credential in Microsoft Graph's API.
    /// </summary>
    /// <remarks>OData Object: https://graph.microsoft.com/beta/$metadata#microsoft.graph.webauthnCredentialCreationOptions</remarks>
    public class MicrosoftGraphWebauthnCredentialCreationOptions
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
        public PublicKeyCredentialCreationOptions PublicKeyOptions { get; set; }

        /// <summary>
        /// Parses a JSON payload returned by Microsoft Graph into WebAuthn credential creation options.
        /// </summary>
        /// <param name="json">The Microsoft Graph JSON payload.</param>
        /// <returns>The parsed credential creation options.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="json"/> is null or empty.</exception>
        /// <exception cref="JsonException">Thrown when the payload cannot be deserialized.</exception>
        public static MicrosoftGraphWebauthnCredentialCreationOptions Create(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                throw new ArgumentNullException(nameof(json));
            }

            return JsonSerializer.Deserialize(json, WebAuthnJsonContext.Default.MicrosoftGraphWebauthnCredentialCreationOptions)
                ?? throw new JsonException("Unable to deserialize Microsoft Graph WebAuthn credential creation options.");
        }
    }
}
