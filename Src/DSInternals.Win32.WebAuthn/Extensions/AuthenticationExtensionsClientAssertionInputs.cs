using System.Text.Json;
using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.FIDO;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Client extension inputs for WebAuthn assertions.
/// </summary>
public sealed class AuthenticationExtensionsClientAssertionInputs
{
    /// <summary>
    /// Legacy FIDO AppID to use during assertion.
    /// </summary>
    [JsonPropertyName("appid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AppID { get; set; }

    /// <summary>
    /// Requests returning an existing credential blob during assertion.
    /// </summary>
    [JsonPropertyName("getCredBlob")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool GetCredentialBlob { get; set; }

    /// <summary>
    /// Input parameters for the legacy hmac-secret extension during assertion.
    /// </summary>
    [JsonPropertyName("hmacGetSecret")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public HMACGetSecretInput? HmacGetSecret { get; set; }

    /// <summary>
    /// Inputs for the WebAuthn largeBlob extension during assertion.
    /// </summary>
    [JsonPropertyName("largeBlob")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public LargeBlobAssertionInputs? LargeBlob { get; set; }

    /// <summary>
    /// Inputs for the WebAuthn prf extension during assertion.
    /// </summary>
    [JsonPropertyName("prf")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PRFAssertionInputs? Prf { get; set; }

    /// <summary>
    /// Remote clientDataJSON supplied by a remote desktop client.
    /// </summary>
    [JsonPropertyName("remoteClientDataJSON")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RemoteClientDataJson { get; set; }

    /// <summary>
    /// Inputs for Chromium's remoteDesktopClientOverride extension.
    /// </summary>
    [JsonPropertyName("remoteDesktopClientOverride")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public RemoteDesktopClientOverride? RemoteDesktopClientOverride { get; set; }

    /// <summary>
    /// Deserializes a JSON string into assertion extension inputs.
    /// </summary>
    /// <param name="json">JSON representation of assertion extension inputs.</param>
    /// <returns>Assertion extension inputs if deserialization is successful; otherwise, null.</returns>
    public static AuthenticationExtensionsClientAssertionInputs? FromJson(string json)
    {
        return WebAuthnJsonModel.FromJson(json, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionInputs);
    }

    /// <summary>
    /// Serializes the assertion extension inputs to JSON.
    /// </summary>
    /// <returns>JSON representation of these assertion extension inputs.</returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionInputs);
    }
}
