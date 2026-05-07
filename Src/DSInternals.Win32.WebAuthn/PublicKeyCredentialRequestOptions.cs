using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Represents the options for a WebAuthn credential request (navigator.credentials.get()).
/// </summary>
public sealed class PublicKeyCredentialRequestOptions
{
    /// <summary>
    /// A challenge from the relying party's server.
    /// </summary>
    [JsonPropertyName("challenge")]
    [JsonConverter(typeof(Base64UrlConverter))]
    [JsonRequired]
    public required byte[] Challenge { get; init; }

    /// <summary>
    /// The time, in milliseconds, that the caller is willing to wait for the call to complete.
    /// </summary>
    [JsonPropertyName("timeout")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public uint? Timeout { get; init; }

    /// <summary>
    /// The relying party identifier.
    /// </summary>
    [JsonPropertyName("rpId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RpId { get; init; }

    /// <summary>
    /// A list of credentials acceptable to the caller.
    /// </summary>
    [JsonPropertyName("allowCredentials")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<PublicKeyCredentialDescriptor>? AllowCredentials { get; init; }

    /// <summary>
    /// Describes the relying party's requirements regarding user verification.
    /// </summary>
    [JsonPropertyName("userVerification")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public UserVerificationRequirement UserVerification { get; init; }

    /// <summary>
    /// UI hints for preferred authenticator types.
    /// </summary>
    [JsonPropertyName("hints")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<string>? Hints { get; init; }

    /// <summary>
    /// Additional client extension inputs.
    /// </summary>
    [JsonPropertyName("extensions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public AuthenticationExtensionsClientAssertionInputs? Extensions { get; init; }

    public static PublicKeyCredentialRequestOptions? FromJson(string optionsJson)
    {
        try
        {
            return JsonSerializer.Deserialize(optionsJson, WebAuthnJsonContext.Default.PublicKeyCredentialRequestOptions);
        }
        catch (JsonException ex)
        {
            Debug.WriteLine($"PublicKeyCredentialRequestOptions JSON deserialization error: {ex.Message}");
            return null;
        }
    }

    override public string ToString()
    {
        return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.PublicKeyCredentialRequestOptions);
    }
}
