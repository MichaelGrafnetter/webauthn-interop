#if NET5_0_OR_GREATER

using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.Cryptography;

/// <summary>
/// Represents the FIDO2 largeBlob extension payload stored on a CXF passkey.
/// </summary>
public sealed class CredentialExchangeFido2LargeBlob
{
    /// <summary>
    /// The size of the uncompressed blob, in bytes.
    /// </summary>
    [JsonPropertyName("uncompressedSize")]
    public ulong UncompressedSize { get; set; }

    /// <summary>
    /// The DEFLATE-compressed largeBlob data.
    /// </summary>
    [JsonPropertyName("data")]
    [JsonConverter(typeof(Base64UrlConverter))]
    public byte[]? Data { get; set; }
}

#endif
