using System;
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
}
