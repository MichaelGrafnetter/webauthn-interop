using System.Text.Json;
using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.FIDO;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Client extension inputs for WebAuthn credential creation.
/// </summary>
public sealed class AuthenticationExtensionsClientAttestationInputs
{
    /// <summary>
    /// Requests creation of an authenticator-scoped HMAC secret.
    /// </summary>
    [JsonPropertyName("hmacCreateSecret")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool HmacCreateSecret { get; set; }

    /// <summary>
    /// Requested credential protection policy.
    /// </summary>
    [JsonPropertyName("credentialProtectionPolicy")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public UserVerification CredProtect { get; set; }

    /// <summary>
    /// Requests failing credential creation when the requested protection policy cannot be honored.
    /// </summary>
    [JsonPropertyName("enforceCredentialProtectionPolicy")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool EnforceCredProtect
    {
        get => field && CredProtect != UserVerification.Any;
        set;
    }

    /// <summary>
    /// Legacy FIDO AppID to exclude during credential creation.
    /// </summary>
    [JsonPropertyName("appidExclude")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AppIDExclude { get; set; }

    /// <summary>
    /// Requests credential property outputs for the created credential.
    /// </summary>
    [JsonPropertyName("credProps")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CredentialProperties { get; set; }

    /// <summary>
    /// Requests the authenticator's current minimum PIN length.
    /// </summary>
    [JsonPropertyName("minPinLength")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool MinimumPinLength { get; set; }

    /// <summary>
    /// Optional credential blob to store with the created credential.
    /// </summary>
    [JsonPropertyName("credBlob")]
    [JsonConverter(typeof(Base64UrlConverter))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public byte[]? CredentialBlob { get; set; }

    /// <summary>
    /// Inputs for the WebAuthn largeBlob extension during credential creation.
    /// </summary>
    [JsonPropertyName("largeBlob")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public LargeBlobAttestationInputs? LargeBlob { get; set; }

    /// <summary>
    /// Inputs for the WebAuthn prf extension during credential creation.
    /// </summary>
    [JsonPropertyName("prf")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PRFAttestationInputs? Prf { get; set; }

    /// <summary>
    /// Requests the user verification method extension output.
    /// </summary>
    [JsonPropertyName("uvm")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Uvm { get; set; }

    /// <summary>
    /// Inputs for the Secure Payment Confirmation (SPC) payment extension during credential creation.
    /// </summary>
    [JsonPropertyName("payment")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PaymentAttestationInputs? Payment { get; set; }

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
    /// Deserializes a JSON string into attestation extension inputs.
    /// </summary>
    /// <param name="json">JSON representation of attestation extension inputs.</param>
    /// <returns>Attestation extension inputs if deserialization is successful; otherwise, null.</returns>
    public static AuthenticationExtensionsClientAttestationInputs? FromJson(string json)
    {
        return WebAuthnJsonModel.FromJson(json, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationInputs);
    }

    /// <summary>
    /// Serializes the attestation extension inputs to JSON.
    /// </summary>
    /// <returns>JSON representation of these attestation extension inputs.</returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationInputs);
    }
}
