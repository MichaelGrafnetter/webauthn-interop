using System.Collections.Generic;
using System.Text.Json;
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

    /// <summary>
    /// User verification method factors reported by the authenticator.
    /// </summary>
    /// <see href="https://www.w3.org/TR/webauthn-2/#sctn-uvm-extension"/>
    [JsonPropertyName("uvm")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IList<UvmEntry>? Uvm { get; set; }

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
        RemoteClientDataJson == null &&
        Uvm == null;

    /// <summary>
    /// Deserializes a JSON string into attestation extension outputs.
    /// </summary>
    /// <param name="json">JSON representation of attestation extension outputs.</param>
    /// <returns>Attestation extension outputs if deserialization is successful; otherwise, null.</returns>
    public static AuthenticationExtensionsClientAttestationOutputs? FromJson(string json)
    {
        return WebAuthnJsonModel.FromJson(json, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationOutputs);
    }

    /// <summary>
    /// Serializes the attestation extension outputs to JSON.
    /// </summary>
    /// <returns>JSON representation of these attestation extension outputs.</returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationOutputs);
    }
}
