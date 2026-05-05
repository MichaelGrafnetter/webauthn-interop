using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// largeBlob inputs for WebAuthn credential creation.
/// </summary>
public sealed class LargeBlobAttestationInputs
{
    /// <summary>
    /// Requested large-blob support for the credential.
    /// </summary>
    [JsonPropertyName("support")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public LargeBlobSupport Support
    {
        get;
        set
        {
            if (value == LargeBlobSupport.None)
            {
                throw new ArgumentException("The largeBlob support value must be required or preferred.", nameof(value));
            }

            field = value;
        }
    } = LargeBlobSupport.Preferred;

    public LargeBlobAttestationInputs()
    {
    }

    public LargeBlobAttestationInputs(LargeBlobSupport support)
    {
        Support = support;
    }

    /// <summary>
    /// Deserializes a JSON string into largeBlob attestation inputs.
    /// </summary>
    /// <param name="json">JSON representation of largeBlob attestation inputs.</param>
    /// <returns>largeBlob attestation inputs if deserialization is successful; otherwise, null.</returns>
    public static LargeBlobAttestationInputs? FromJson(string json)
    {
        return WebAuthnJsonModel.FromJson(json, WebAuthnJsonContext.Default.LargeBlobAttestationInputs);
    }

    /// <summary>
    /// Serializes the largeBlob attestation inputs to JSON.
    /// </summary>
    /// <returns>JSON representation of these largeBlob attestation inputs.</returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.LargeBlobAttestationInputs);
    }
}
