using System;
using System.Buffers;
using System.Buffers.Text;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Custom converter for encoding/decoding byte[] using Base64Url instead of default Base64.
    /// </summary>
    public sealed class Base64UrlConverter : JsonConverter<byte[]>
    {
        /// <summary>
        /// Reads a Base64Url-encoded JSON string into a byte array.
        /// </summary>
        /// <param name="reader">JSON reader positioned on the value.</param>
        /// <param name="typeToConvert">Target CLR type.</param>
        /// <param name="options">Serializer options.</param>
        /// <returns>Decoded binary value.</returns>
        public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.HasValueSequence)
            {
                return FromBase64UrlString(reader.GetString());
            }
            else
            {
                try
                {
                    return FromBase64UrlString(reader.ValueSpan);
                }
                catch (FormatException)
                {
                    // If we could not decode the string from base64 url, perhaps this is one of the weird MSFT
                    // "formatted in Base64URL with a padding number suffix" strings.

                    // https://github.com/MichaelGrafnetter/webauthn-interop/issues/28#issuecomment-3529633518

                    // Checking to see if the last character is a digit and if we removed it, would there be anything left
                    if (reader.ValueSpan.Length > 1)
                    {
                        // Looking for last character to be 0, 1, or 2
                        char lastChar = (char)reader.ValueSpan[^1];

                        if (char.IsDigit(lastChar))
                        {
                            // If we removed the last character, calculate the padding required for the remaining string
                            int potentialPaddingLength = (reader.ValueSpan.Length - 1) % 4;

                            // If the last character matches the padding length of the remaining string, this is very likely the case we are looking for
                            if ((lastChar == '0' && potentialPaddingLength == 0) ||
                                (lastChar == '1' && potentialPaddingLength == 3) ||
                                (lastChar == '2' && potentialPaddingLength == 2))
                            {
                                // Try again this time removing the last character
                                return FromBase64UrlString(reader.ValueSpan[..^1]);
                            }
                        }
                    }
                    throw;
                }
            }
        }

        /// <summary>
        /// Writes a byte array as a Base64Url-encoded JSON string.
        /// </summary>
        /// <param name="writer">JSON writer.</param>
        /// <param name="value">Binary value to encode.</param>
        /// <param name="options">Serializer options.</param>
        public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options)
        {
            writer?.WriteStringValue(Base64Url.EncodeToString(value));
        }

        /// <summary>
        /// Converts a Base64Url encoded string to a byte array
        /// </summary>
        /// <param name="input">The Base64Url encoded string</param>
        /// <returns>The byte array represented by the encoded string</returns>
        public static byte[] FromBase64UrlString(string input)
        {
            ArgumentNullException.ThrowIfNull(input);

            // Remove any whitespace (newlines, spaces, tabs) that might have been introduced by copy-paste
            string cleanedInput = Regex.Replace(input, @"\s", string.Empty);

            return FromBase64UrlString(Encoding.ASCII.GetBytes(cleanedInput));
        }

        /// <summary>
        /// Converts a Base64Url or Base64 encoded string to a byte array
        /// </summary>
        /// <param name="input">The Base64Url encoded string</param>
        /// <returns>The byte array represented by the encoded string</returns>
        public static byte[] FromBase64UrlString(ReadOnlySpan<byte> input)
        {
            if (input.IsEmpty)
            {
                return [];
            }

            // Copy input to a mutable buffer for in-place normalization and decoding
            byte[] buffer = ArrayPool<byte>.Shared.Rent(input.Length);

            try
            {
                input.CopyTo(buffer);
                int length = input.Length;


                // Normalize standard Base64 to Base64URL: replace '+' with '-', '/' with '_'
                for (int i = 0; i < length; i++)
                {

                    if (buffer[i] == (byte)'+')
                    {
                        buffer[i] = (byte)'-';
                    }
                    else if (buffer[i] == (byte)'/')
                    {
                        buffer[i] = (byte)'_';
                    }
                }

                // Strip trailing Base64 '=' padding
                while (length > 0 && buffer[length - 1] == (byte)'=')
                {
                    length--;
                }

                int bytesWritten = Base64Url.DecodeFromUtf8InPlace(buffer.AsSpan(0, length));
                return buffer.AsSpan(0, bytesWritten).ToArray();
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }
    }
}
