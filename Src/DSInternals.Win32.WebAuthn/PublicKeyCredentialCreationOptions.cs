using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Represents the options for a WebAuthn credential creation (navigator.credentials.create()).
/// </summary>
public sealed class PublicKeyCredentialCreationOptions
{
    /// <summary>
    /// Relying party information.
    /// </summary>
    [JsonPropertyName("rp")]
    [JsonRequired]
    public required RelyingPartyInformation RelyingParty { get; init; }

    /// <summary>
    /// User account information.
    /// </summary>
    [JsonPropertyName("user")]
    [JsonRequired]
    public required UserInformation User { get; init; }

    /// <summary>
    /// A challenge from the relying party's server.
    /// </summary>
    [JsonPropertyName("challenge")]
    [JsonConverter(typeof(Base64UrlConverter))]
    [JsonRequired]
    public required byte[] Challenge { get; init; }

    /// <summary>
    /// A set of preferred credential properties for the creation of a new public key credential.
    /// </summary>
    [JsonPropertyName("pubKeyCredParams")]
    [JsonRequired]
    public required IReadOnlyList<PublicKeyCredentialParameter> PublicKeyCredentialParameters { get; init; }

    /// <summary>
    /// The time in milliseconds that the client is willing to wait for the credential creation operation to complete.
    /// </summary>
    [JsonPropertyName("timeout")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public uint? TimeoutMilliseconds { get; init; }

    /// <summary>
    /// List of credentials blocked for creation.
    /// </summary>
    [JsonPropertyName("excludeCredentials")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<PublicKeyCredentialDescriptor>? ExcludeCredentials { get; init; }

    /// <summary>
    /// Properties of WebAuthn authenticators allowed to be used for authentication.
    /// </summary>
    [JsonPropertyName("authenticatorSelection")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public AuthenticatorSelectionCriteria? AuthenticatorSelection { get; init; }

    /// <summary>
    /// Attestation preference for the relying party.
    /// </summary>
    [JsonPropertyName("attestation")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public AttestationConveyancePreference Attestation { get; init; } = AttestationConveyancePreference.Any;

    /// <summary>
    /// Additional processing required for the client and WebAuthn authenticator.
    /// </summary>
    [JsonPropertyName("extensions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public AuthenticationExtensionsClientAttestationInputs? Extensions { get; init; }

    /// <summary>
    /// UI hints for preferred authenticator types.
    /// </summary>
    [JsonPropertyName("hints")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<string>? Hints { get; init; }

    /// <summary>
    /// A list of attestation formats acceptable to the relying party.
    /// </summary>
    [JsonPropertyName("attestationFormats")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<string>? AttestationFormats { get; init; }

    public static PublicKeyCredentialCreationOptions? FromJson(string optionsJson)
    {
        try
        {
            return JsonSerializer.Deserialize(optionsJson, WebAuthnJsonContext.Default.PublicKeyCredentialCreationOptions);
        }
        catch (JsonException ex)
        {
            Debug.WriteLine($"PublicKeyCredentialCreationOptions JSON deserialization error: {ex.Message}");
            return null;
        }
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.PublicKeyCredentialCreationOptions);
    }
}
