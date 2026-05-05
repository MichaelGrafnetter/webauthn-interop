using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// largeBlob outputs from WebAuthn credential creation.
/// </summary>
public sealed class LargeBlobAttestationOutputs
{
    /// <summary>
    /// Indicates whether the created credential supports large blobs.
    /// </summary>
    [JsonPropertyName("supported")]
    [JsonRequired]
    public required bool Supported { get; set; }
}
