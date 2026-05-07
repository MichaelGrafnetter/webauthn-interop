#if NET5_0_OR_GREATER

using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.Cryptography;

/// <summary>
/// Represents the FIDO2 hmac-secret extension state captured in a CXF passkey.
/// </summary>
public sealed class CredentialExchangeFido2HmacCredentials
{
    /// <summary>
    /// The HMAC algorithm identifier (e.g. <c>hmac-sha256</c>).
    /// </summary>
    [JsonPropertyName("algorithm")]
    public string? Algorithm { get; set; }

    /// <summary>
    /// The HMAC credential to use when user verification was performed.
    /// </summary>
    [JsonPropertyName("credWithUV")]
    [JsonConverter(typeof(Base64UrlConverter))]
    public byte[]? CredentialWithUserVerification { get; set; }

    /// <summary>
    /// The HMAC credential to use when user verification was not performed.
    /// </summary>
    [JsonPropertyName("credWithoutUV")]
    [JsonConverter(typeof(Base64UrlConverter))]
    public byte[]? CredentialWithoutUserVerification { get; set; }
}

#endif
