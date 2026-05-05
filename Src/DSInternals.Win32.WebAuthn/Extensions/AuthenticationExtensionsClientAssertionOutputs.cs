using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Client extension outputs from WebAuthn assertions.
/// </summary>
public sealed class AuthenticationExtensionsClientAssertionOutputs : AuthenticationExtensionsClientOutputs
{
    /// <summary>
    /// Indicates whether the FIDO AppID extension was used for authentication.
    /// </summary>
    [JsonPropertyName("appid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? AppID { get; set; }

    /// <summary>
    /// HMAC secret output returned during assertion.
    /// </summary>
    [JsonPropertyName("hmacGetSecret")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public HMACGetSecretOutput? HmacGetSecret { get; set; }

    /// <summary>
    /// Credential blob returned during assertion.
    /// </summary>
    [JsonPropertyName("credBlob")]
    [JsonConverter(typeof(Base64UrlConverter))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public byte[]? CredentialBlob { get; set; }

    /// <summary>
    /// Outputs for the WebAuthn largeBlob extension during assertion.
    /// </summary>
    [JsonPropertyName("largeBlob")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public LargeBlobAssertionOutputs? LargeBlob { get; set; }

    /// <summary>
    /// Outputs for the WebAuthn prf extension during assertion.
    /// </summary>
    [JsonPropertyName("prf")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PRFAssertionOutputs? Prf { get; set; }

    /// <summary>
    /// Indicates whether the remoteClientDataJSON extension was used.
    /// </summary>
    [JsonPropertyName("remoteClientDataJson")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? RemoteClientDataJson { get; set; }

    [JsonIgnore]
    internal override bool IsEmpty =>
        AppID == null &&
        HmacGetSecret == null &&
        CredentialBlob == null &&
        LargeBlob == null &&
        Prf == null &&
        RemoteClientDataJson == null;
}
