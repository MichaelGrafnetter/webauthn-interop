using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Converts WebAuthn enum values with a uint underlying type to and from their JSON string representations.
/// </summary>
/// <remarks>
/// Non-flags enums are serialized as a single string value. Enums decorated with <see cref="FlagsAttribute"/> are
/// serialized as a JSON array containing the names of all set flags. Names are read from
/// <see cref="JsonStringEnumMemberNameAttribute"/>; enum fields without that attribute are treated as unmapped.
/// Unrecognized or unmapped JSON names deserialize to the default enum value, allowing for forward compatibility
/// when new WebAuthn values are added.
/// </remarks>
/// <typeparam name="TEnum">The enum type to convert. Must be an enum with uint as its underlying type.</typeparam>
public sealed class WebAuthnJsonEnumConverter<TEnum> : JsonConverter<TEnum>
        where TEnum : struct, Enum
{
    private static readonly Dictionary<TEnum, string?> valuesToNames = CreateValueToNameDictionary();
    private static readonly Dictionary<string, TEnum> namesToValues = CreateNameToValueDictionary();
    private static readonly bool isFlagsEnum = typeof(TEnum).IsDefined(typeof(FlagsAttribute), false);

    /// <summary>
    /// Reads and converts the JSON representation of an enum value to its corresponding <typeparamref name="TEnum"/> value.
    /// </summary>
    /// <param name="reader">The reader used to read the JSON data. The reader must be positioned at the value to convert.</param>
    /// <param name="typeToConvert">The type of the enum to convert to. This must be a valid enum type.</param>
    /// <param name="options">Options to control the behavior of the deserialization process.</param>
    /// <returns>
    /// The enum value read from the JSON input. Null and unrecognized string values return the default
    /// <typeparamref name="TEnum"/> value.
    /// </returns>
    /// <exception cref="JsonException">
    /// Thrown when the JSON token shape does not match the enum type: non-flags enums require a string value, while flags
    /// enums require an array of string values.
    /// </exception>
    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.Null => default,
            JsonTokenType.String => !isFlagsEnum ? WebAuthnJsonEnumConverter<TEnum>.ReadValue(ref reader) : throw new JsonException($"Invalid ({reader.TokenType}) value for flags enum"),
            JsonTokenType.StartArray => isFlagsEnum ? WebAuthnJsonEnumConverter<TEnum>.ReadValues(ref reader) : throw new JsonException($"Invalid ({reader.TokenType}) value for non-flags enum"),
            _ => throw new JsonException($"Invalid enum value ({reader.TokenType})")
        };
    }

    private static TEnum ReadValues(ref Utf8JsonReader reader)
    {
        TEnum result = default;

        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            // Combine the flags. All interop flags are defined as uint.
            TEnum arrayItem = WebAuthnJsonEnumConverter<TEnum>.ReadValue(ref reader);
            result = (TEnum)(object)(((uint)(object)result) | ((uint)(object)arrayItem));
        }

        return result;
    }

    private static TEnum ReadValue(ref Utf8JsonReader reader)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException($"Unexpected token parsing enum. Expected String, got {reader.TokenType}.");
        }

        string? text = reader.GetString();
        if (text is not null && namesToValues.TryGetValue(text, out TEnum value))
            return value;
        else
            // Ignore unrecognized values. This allows for forward compatibility if new flags are added to the standard in the future.
            return default;
    }

    /// <summary>
    /// Writes the specified enum value to JSON using its mapped WebAuthn string name.
    /// </summary>
    /// <remarks>
    /// Non-flags enums are written as a single string, or null when the value is unmapped. Flags enums are written as a
    /// JSON array containing only mapped, non-zero flags that are set on the value; unmapped flags are skipped.
    /// </remarks>
    /// <param name="writer">The <see cref="Utf8JsonWriter"/> to which the JSON value will be written.</param>
    /// <param name="value">The enum value to serialize.</param>
    /// <param name="options">The serialization options to use when writing the JSON value.</param>
    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(writer);

        if (isFlagsEnum)
        {
            // Serialize the flags enum as an array of strings, while skipping default or unmapped non-zero values.
            uint numericValue = (uint)(object)value;
            List<string> flagNames = [];

            writer.WriteStartArray();

            foreach (var kvp in valuesToNames)
            {
                uint flagValue = (uint)(object)kvp.Key;

                // Skip zero/unmapped values and check if the flag is set
                if (flagValue != 0 && kvp.Value is not null && (numericValue & flagValue) == flagValue)
                {
                    writer.WriteStringValue(kvp.Value);
                }
            }

            writer.WriteEndArray();
        }
        else
        {
            // Serialize the non-flags enum as a single string, while skipping default or unmapped non-zero values.
            if (valuesToNames.TryGetValue(value, out string? name) && name is not null)
            {
                writer.WriteStringValue(name);
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WebAuthnJsonEnumConverter{TEnum}"/> class.
    /// </summary>
    /// <remarks>
    /// This converter is intended for WebAuthn enums that have a uint underlying type. Flags and non-flags enums are both
    /// supported.
    /// </remarks>
    /// <exception cref="JsonException">Thrown if <typeparamref name="TEnum"/> does not use uint as its underlying type.</exception>
    public WebAuthnJsonEnumConverter()
    {
        // Apply additional constraints to TEnum beyond what is enforced by the generic type parameter constraints.
        if (Enum.GetUnderlyingType(typeof(TEnum)) != typeof(uint))
        {
            throw new JsonException("The converter only supports enums with uint underlying type.");
        }
    }

    /// <summary>
    /// Creates a dictionary that maps each value of the enum type parameter to its corresponding JSON string name, as
    /// specified by the JsonStringEnumMemberNameAttribute.
    /// </summary>
    /// <remarks>Only enum fields with the JsonStringEnumMemberNameAttribute are included with a non-null
    /// value. Fields without the attribute are mapped to null.</remarks>
    /// <returns>A dictionary containing enum values as keys and their associated JSON string names as values. The value is null
    /// if the enum field does not have a JsonStringEnumMemberNameAttribute.</returns>
    private static Dictionary<TEnum, string?> CreateValueToNameDictionary()
    {
        var dictionary = new Dictionary<TEnum, string?>();

        foreach (var field in typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var enumMemberNameAttribute = field.GetCustomAttribute<JsonStringEnumMemberNameAttribute>(false);
            TEnum value = (TEnum)field.GetValue(null)!;
            // Use the JsonStringEnumMemberName value. Ignore values without explicit mapping.
            dictionary[value] = enumMemberNameAttribute?.Name;
        }

        return dictionary;
    }

    /// <summary>
    /// Creates a dictionary that maps enum names to their corresponding values, using a case-insensitive string
    /// comparer.
    /// </summary>
    /// <remarks>The returned dictionary uses case-insensitive comparison for keys. Only enum values with
    /// explicit name mappings are added to the dictionary.</remarks>
    /// <returns>A dictionary containing enum names as keys and their associated values as values. Only names with explicit
    /// mappings are included.</returns>
    private static Dictionary<string, TEnum> CreateNameToValueDictionary()
    {
        var dictionary = new Dictionary<string, TEnum>(StringComparer.OrdinalIgnoreCase);

        // Revert the valuesToNames dictionary, but ignore values without explicit mapping.
        foreach (var kvp in valuesToNames)
        {
            if (kvp.Value is not null)
            {
                dictionary[kvp.Value] = kvp.Key;
            }
        }

        return dictionary;
    }
}
