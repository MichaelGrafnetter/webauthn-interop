#if NET5_0_OR_GREATER

using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.Cryptography;

/// <summary>
/// Represents a single CXF item that may bundle one or more credentials.
/// </summary>
public sealed class CredentialExchangeItem
{
    /// <summary>
    /// The probabilistically-unique identifier of the item.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonConverter(typeof(Base64UrlConverter))]
    public byte[]? Id { get; set; }

    /// <summary>
    /// The Unix timestamp at which the item was created, in seconds.
    /// </summary>
    [JsonPropertyName("creationAt")]
    public ulong? CreatedAt { get; set; }

    /// <summary>
    /// The Unix timestamp of the last modification of the item, in seconds.
    /// </summary>
    [JsonPropertyName("modifiedAt")]
    public ulong? ModifiedAt { get; set; }

    /// <summary>
    /// The display title of the item.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// An optional secondary title for the item.
    /// </summary>
    [JsonPropertyName("subtitle")]
    public string? Subtitle { get; set; }

    /// <summary>
    /// Indicates whether the user marked this item as a favorite.
    /// </summary>
    [JsonPropertyName("favorite")]
    public bool Favorite { get; set; }

    /// <summary>
    /// The credentials bundled inside this item.
    /// </summary>
    [JsonPropertyName("credentials")]
    public CredentialExchangeCredential[]? Credentials { get; set; }

    /// <summary>
    /// Tags associated with this item.
    /// </summary>
    [JsonPropertyName("tags")]
    public string[]? Tags { get; set; }
}

#endif
