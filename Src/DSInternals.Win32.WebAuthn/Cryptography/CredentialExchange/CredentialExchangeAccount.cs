#if NET5_0_OR_GREATER

using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.Cryptography;

/// <summary>
/// Represents a single user account in a FIDO Credential Exchange Format payload.
/// </summary>
public sealed class CredentialExchangeAccount
{
    /// <summary>
    /// The probabilistically-unique identifier of the account.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonConverter(typeof(Base64UrlConverter))]
    public byte[]? Id { get; set; }

    /// <summary>
    /// The username associated with the account.
    /// </summary>
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    /// <summary>
    /// The email address associated with the account.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// The full name associated with the account.
    /// </summary>
    [JsonPropertyName("fullName")]
    public string? FullName { get; set; }

    /// <summary>
    /// The vault items contained in the account.
    /// </summary>
    [JsonPropertyName("items")]
    public CredentialExchangeItem[]? Items { get; set; }
}

#endif
