using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.FIDO;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Converts a <see cref="UvmEntry"/> to and from a 3-element JSON array of unsigned numbers, as defined by the WebAuthn uvm extension.
/// </summary>
/// <see href="https://www.w3.org/TR/webauthn-2/#sctn-uvm-extension"/>
public sealed class UvmEntryConverter : JsonConverter<UvmEntry>
{
    /// <summary>
    /// Reads a 3-element JSON array of numbers and returns the corresponding <see cref="UvmEntry"/>.
    /// </summary>
    public override UvmEntry Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException($"Expected start of uvmEntry array, got {reader.TokenType}.");
        }

        if (!reader.Read() || reader.TokenType != JsonTokenType.Number)
        {
            throw new JsonException("Expected userVerificationMethod number in uvmEntry.");
        }

        var userVerificationMethod = (UserVerificationMethod)reader.GetUInt32();

        if (!reader.Read() || reader.TokenType != JsonTokenType.Number)
        {
            throw new JsonException("Expected keyProtectionType number in uvmEntry.");
        }

        var keyProtectionType = (KeyProtectionType)reader.GetUInt16();

        if (!reader.Read() || reader.TokenType != JsonTokenType.Number)
        {
            throw new JsonException("Expected matcherProtectionType number in uvmEntry.");
        }

        var matcherProtectionType = (MatcherProtectionType)reader.GetUInt16();

        if (!reader.Read() || reader.TokenType != JsonTokenType.EndArray)
        {
            throw new JsonException("uvmEntry must contain exactly three elements.");
        }

        return new UvmEntry(userVerificationMethod, keyProtectionType, matcherProtectionType);
    }

    /// <summary>
    /// Writes the <see cref="UvmEntry"/> as a 3-element JSON array of numbers.
    /// </summary>
    public override void Write(Utf8JsonWriter writer, UvmEntry value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(writer);
        ArgumentNullException.ThrowIfNull(value);

        writer.WriteStartArray();
        writer.WriteNumberValue((uint)value.UserVerificationMethod);
        writer.WriteNumberValue((ushort)value.KeyProtectionType);
        writer.WriteNumberValue((ushort)value.MatcherProtectionType);
        writer.WriteEndArray();
    }
}
