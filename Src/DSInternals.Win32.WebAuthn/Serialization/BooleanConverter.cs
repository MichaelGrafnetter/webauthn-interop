using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Custom converter for reading Boolean values from either JSON Boolean or string tokens.
/// </summary>
public sealed class BooleanConverter : JsonConverter<bool>
{
    /// <inheritdoc />
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.True => true,
            JsonTokenType.False => false,
            JsonTokenType.String => ReadBooleanString(ref reader),
            _ => throw new JsonException($"Expected a Boolean or Boolean string token, but found {reader.TokenType}.")
        };
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    {
        writer.WriteBooleanValue(value);
    }

    private static bool ReadBooleanString(ref Utf8JsonReader reader)
    {
        string? value = reader.GetString();
        if (bool.TryParse(value, out bool result))
        {
            return result;
        }

        throw new JsonException($"Expected a Boolean string value, but found '{value}'.");
    }
}
