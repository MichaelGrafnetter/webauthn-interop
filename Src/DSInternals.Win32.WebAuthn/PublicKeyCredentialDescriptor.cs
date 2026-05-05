using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Identifies an existing public key credential.
    /// </summary>
    public sealed class PublicKeyCredentialDescriptor
    {
        /// <summary>
        /// This member contains the type of the public key credential the caller is referring to.
        /// </summary>
        [JsonPropertyName("type")]
        public required string Type { get; init; } = ApiConstants.PublicKeyCredentialType;

        /// <summary>
        /// This member contains the credential ID of the public key credential the caller is referring to.
        /// </summary>
        [JsonPropertyName("id")]
        [JsonConverter(typeof(Base64UrlConverter))]
        public required byte[] Id { get; init; }

        /// <summary>
        /// This member contains a hint as to how the client might communicate with the managing authenticator of the public key credential the caller is referring to.
        /// </summary>
        [JsonPropertyName("transports")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public AuthenticatorTransport Transports { get; init; }

        /// <summary>
        /// Initializes a new credential descriptor.
        /// </summary>
        /// <param name="id">Credential identifier.</param>
        /// <param name="transports">Supported authenticator transports for this credential.</param>
        /// <param name="type">Credential type. Defaults to <c>public-key</c>.</param>
        [JsonConstructor]
        [SetsRequiredMembers]
        public PublicKeyCredentialDescriptor(
            byte[] id,
            AuthenticatorTransport transports = AuthenticatorTransport.NoRestrictions,
            string type = ApiConstants.PublicKeyCredentialType)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Transports = transports;
            Type = type;
        }
    }
}
