using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Represents a WebAuthn public key credential.
/// </summary>
/// <typeparam name="TResponse">Concrete authenticator response type.</typeparam>
/// <typeparam name="TClientExtensionResults">Concrete client extension output type.</typeparam>
public abstract class PublicKeyCredential<TResponse, TClientExtensionResults>
    where TResponse : AuthenticatorResponse
    where TClientExtensionResults : AuthenticationExtensionsClientOutputs
{
    /// <summary>
    /// The credential identifier (Base64Url encoded).
    /// </summary>
    [JsonPropertyName("id")]
    [JsonRequired]
    [JsonConverter(typeof(Base64UrlConverter))]
    public required byte[] Id { get; init; }

    /// <summary>
    /// The raw credential identifier (Base64Url encoded).
    /// </summary>
    [JsonPropertyName("rawId")]
    [JsonConverter(typeof(Base64UrlConverter))]
    public byte[]? RawId { get; init; }

    /// <summary>
    /// The type of the credential (always "public-key").
    /// </summary>
    [JsonPropertyName("type")]
    [JsonRequired]
    public required string Type { get; init; } = ApiConstants.PublicKeyCredentialType;

    /// <summary>
    /// The authenticator attachment modality.
    /// </summary>
    [JsonPropertyName("authenticatorAttachment")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public AuthenticatorAttachment AuthenticatorAttachment { get; init; }

    /// <summary>
    /// Authenticator response payload.
    /// </summary>
    [JsonPropertyName("response")]
    [JsonRequired]
    public required TResponse Response { get; init; }

    /// <summary>
    /// Outputs of client extension processing.
    /// </summary>
    [JsonPropertyName("clientExtensionResults")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TClientExtensionResults? ClientExtensionResults { get; init; }
}
