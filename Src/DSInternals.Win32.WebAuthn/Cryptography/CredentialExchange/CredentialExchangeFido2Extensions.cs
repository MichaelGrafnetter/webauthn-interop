#if NET5_0_OR_GREATER

using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.Cryptography;

/// <summary>
/// Represents the FIDO2 extension state recorded for a CXF passkey credential.
/// </summary>
public sealed class CredentialExchangeFido2Extensions
{
    /// <summary>
    /// The hmac-secret extension state.
    /// </summary>
    [JsonPropertyName("hmacCredentials")]
    public CredentialExchangeFido2HmacCredentials? HmacCredentials { get; set; }

    /// <summary>
    /// The credBlob extension data.
    /// </summary>
    [JsonPropertyName("credBlob")]
    [JsonConverter(typeof(Base64UrlConverter))]
    public byte[]? CredBlob { get; set; }

    /// <summary>
    /// The largeBlob extension data.
    /// </summary>
    [JsonPropertyName("largeBlob")]
    public CredentialExchangeFido2LargeBlob? LargeBlob { get; set; }

    /// <summary>
    /// Indicates whether the passkey is enabled for the Secure Payment Confirmation extension.
    /// </summary>
    [JsonPropertyName("payments")]
    public bool? Payments { get; set; }
}

#endif
