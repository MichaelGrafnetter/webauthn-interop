using System;
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
}
