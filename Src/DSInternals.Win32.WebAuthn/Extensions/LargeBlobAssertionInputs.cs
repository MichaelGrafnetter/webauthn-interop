using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// largeBlob inputs for WebAuthn assertions.
/// </summary>
public sealed class LargeBlobAssertionInputs
{
    /// <summary>
    /// Requests reading the large blob during assertion.
    /// </summary>
    [JsonPropertyName("read")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Read { get; init; }

    /// <summary>
    /// Large blob contents to write during assertion.
    /// </summary>
    [JsonPropertyName("write")]
    [JsonConverter(typeof(Base64UrlConverter))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public byte[]? Write { get; init; }

    [JsonIgnore]
    public CredentialLargeBlobOperation Operation => this switch
    {
        { Read: true, Write: null } => CredentialLargeBlobOperation.Get,
        { Read: false, Write: not null } => CredentialLargeBlobOperation.Set,
        _ => CredentialLargeBlobOperation.None
    };

    [JsonConstructor]
    public LargeBlobAssertionInputs(bool read = false, byte[]? write = null)
    {
        bool hasWrite = write != null;
        if (read == hasWrite)
        {
            throw new ArgumentException("The largeBlob extension must request either read or write, but not both.");
        }

        Read = read;
        Write = write;
    }

    /// <summary>
    /// Deserializes a JSON string into largeBlob assertion inputs.
    /// </summary>
    /// <param name="json">JSON representation of largeBlob assertion inputs.</param>
    /// <returns>largeBlob assertion inputs if deserialization is successful; otherwise, null.</returns>
    public static LargeBlobAssertionInputs? FromJson(string json)
    {
        return WebAuthnJsonModel.FromJson(json, WebAuthnJsonContext.Default.LargeBlobAssertionInputs);
    }

    /// <summary>
    /// Serializes the largeBlob assertion inputs to JSON.
    /// </summary>
    /// <returns>JSON representation of these largeBlob assertion inputs.</returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.LargeBlobAssertionInputs);
    }
}
