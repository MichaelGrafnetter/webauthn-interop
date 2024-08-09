﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn
{
    public sealed class PublicKeyCredentialCreationOptions
    {
        /// <summary>
        /// Attestation preference for the relying party.
        /// </summary>
        [JsonPropertyName("attestation")]
        public AttestationConveyancePreference Attestation { get; set; } = AttestationConveyancePreference.None;

        /// <summary>
        /// Properties of WebAuthn authenticators allowed to be used for authentication.
        /// </summary>
        [JsonPropertyName("authenticatorSelection")]
        public AuthenticatorSelection AuthenticatorSelection { get; set; }

        /// <summary>
        /// A challenge generated by Microsoft Entra ID and sent back with the registration request to prevent replay attacks.
        /// </summary>
        [JsonPropertyName("challenge")]
        [JsonConverter(typeof(Base64UrlConverter))]
        public byte[] Challenge { get; set; }

        /// <summary>
        /// List of credentials blocked for creations.
        /// </summary>
        [JsonPropertyName("excludeCredentials")]
        public IReadOnlyList<PublicKeyCredentialDescriptor> ExcludeCredentials { get; set; }

        /// <summary>
        /// Additional processing required for the client and WebAuthn authenticator.
        /// </summary>
        [JsonPropertyName("extensions")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AuthenticationExtensionsClientInputs Extensions { get; set; }

        /// <summary>
        /// A set of preferred credential properties for the creation of a new public key credential.
        /// </summary>
        [JsonPropertyName("pubKeyCredParams")]
        public IReadOnlyList<PublicKeyCredentialParameter> PublicKeyCredentialParameters { get; set; }

        /// <summary>
        /// Information about the relying party responsible for the request.
        /// </summary>
        [JsonPropertyName("rp")]
        public RelyingPartyInformation RelyingParty { get; set; }

        /// <summary>
        /// The time in milliseconds that the client is willing to wait for the credential creation operation to complete.
        /// </summary>
        [JsonPropertyName("timeout")]
        public int TimeoutMilliseconds { get; set; }

        /// <summary>
        ///  Information about the user account for which the credential is generated.
        /// </summary>
        [JsonPropertyName("user")]
        public UserInformation User { get; set; }
    }
}
