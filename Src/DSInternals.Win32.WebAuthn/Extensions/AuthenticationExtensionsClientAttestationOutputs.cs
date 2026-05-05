using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Client extension outputs from WebAuthn credential creation.
/// </summary>
public sealed class AuthenticationExtensionsClientAttestationOutputs : AuthenticationExtensionsClientOutputs
{
    /// <summary>
    /// Indicates whether the FIDO AppID exclusion extension was used.
    /// </summary>
    [JsonPropertyName("appidExclude")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? AppIDExclude { get; set; }

    /// <summary>
    /// Credential properties returned for the created credential.
    /// </summary>
    [JsonPropertyName("credProps")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public CredentialPropertiesOutputs? CredentialProperties { get; set; }

    /// <summary>
    /// Indicates whether the authenticator created an HMAC secret.
    /// </summary>
    [JsonPropertyName("hmacCreateSecret")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool HmacSecret { get; set; }

    /// <summary>
    /// Credential protection policy applied by the authenticator.
    /// </summary>
    [JsonPropertyName("credentialProtectionPolicy")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public UserVerification CredProtect { get; set; }

    /// <summary>
    /// Reported minimum PIN length enforced by the authenticator.
    /// </summary>
    [JsonPropertyName("minPinLength")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public uint? MinimumPinLength { get; set; }

    /// <summary>
    /// Indicates whether a credential blob was successfully created.
    /// </summary>
    [JsonPropertyName("credBlob")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CredentialBlobCreated { get; set; }

    /// <summary>
    /// Outputs for the WebAuthn largeBlob extension during credential creation.
    /// </summary>
    [JsonPropertyName("largeBlob")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public LargeBlobAttestationOutputs? LargeBlob { get; set; }

    /// <summary>
    /// Outputs for the WebAuthn prf extension during credential creation.
    /// </summary>
    [JsonPropertyName("prf")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PRFAttestationOutputs? Prf { get; set; }

    /// <summary>
    /// Indicates whether the remoteClientDataJSON extension was used.
    /// </summary>
    [JsonPropertyName("remoteClientDataJson")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? RemoteClientDataJson { get; set; }

    [JsonIgnore]
    internal override bool IsEmpty =>
        AppIDExclude == null &&
        CredentialProperties == null &&
        HmacSecret == false &&
        CredProtect == UserVerification.Any &&
        MinimumPinLength == null &&
        CredentialBlobCreated == null &&
        LargeBlob == null &&
        Prf == null &&
        RemoteClientDataJson == null;
}
