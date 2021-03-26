using System;
using Newtonsoft.Json;

namespace DSInternals.Win32.WebAuthn.FIDO
{
    /// <summary>
    /// Custom converter for encoding/decoding byte[] using Base64Url instead of default Base64.
    /// </summary>
    public class Base64UrlConverter : JsonConverter<byte[]>
    {
        public Base64UrlConverter() { }

        public override byte[] ReadJson(JsonReader reader, Type objectType, byte[] existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                throw new JsonReaderException("A non-empty byte array is required.");
            }

            if (reader.ValueType != typeof(string))
            {
                throw new JsonReaderException("Unexpected value type.");
            }

            try
            {
                return FromBase64UrlString((string)reader.Value);
            }
            catch (Exception ex)
            {
                throw new JsonReaderException("Value must a BASE64 encoded string.", ex);
            }
        }

        public override void WriteJson(JsonWriter writer, byte[] value, JsonSerializer serializer)
        {
            writer.WriteValue(ToBase64UrlString(value));
        }

        /// <summary>
        /// Converts a byte array to a Base64Url encoded string
        /// </summary>
        /// <param name="input">The byte array to convert</param>
        /// <returns>The Base64Url encoded form of the input</returns>
        public static string ToBase64UrlString(byte[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return Convert.ToBase64String(input).
                TrimEnd('=').
                Replace('+', '-').
                Replace('/', '_');
        }

        /// <summary>
        /// Converts a Base64Url encoded string to a byte array
        /// </summary>
        /// <param name="input">The Base64Url encoded string</param>
        /// <returns>The byte array represented by the encoded string</returns>
        public static byte[] FromBase64UrlString(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return Convert.FromBase64String(Pad(input.Replace('-', '+').Replace('_', '/')));
        }

        /// <summary>
        /// Adds padding to the input
        /// </summary>
        /// <param name="input"> the input string </param>
        /// <returns> the padded string </returns>
        private static string Pad(string input)
        {
            if (input.TrimEnd().EndsWith("="))
            {
                throw new ArgumentException("Illegal Base64URL string!", nameof(input));
            }

            switch (input.Length % 4)
            {
                case 0:
                    // Padding is not needed
                    break;
                case 2:
                    input += "==";
                    break;
                case 3:
                    input += "=";
                    break;
                default:
                    throw new ArgumentException("Illegal Base64URL string!", nameof(input));
            }

            return input;
        }
    }
}
