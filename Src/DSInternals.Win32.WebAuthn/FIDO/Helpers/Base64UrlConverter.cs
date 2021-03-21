using System;
using Newtonsoft.Json;

namespace DSInternals.Win32.WebAuthn.FIDO
{
    /// <summary>
    /// Custom converter for encoding/decoding byte[] using Base64Url instead of default Base64.
    /// </summary>
    public class Base64UrlConverter : JsonConverter<byte[]>
    {
        private readonly Required _required = Required.DisallowNull;

        public Base64UrlConverter() { }

        public Base64UrlConverter(Required required)
        {
            _required = required;
        }

        public override byte[] ReadJson(JsonReader reader, Type objectType, byte[] existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                if (_required == Required.AllowNull)
                {
                    return null;
                }
                else
                {
                    throw new JsonReaderException("A non-empty byte array is required.");
                }
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
        private static string ToBase64UrlString(byte[] input)
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
        private static byte[] FromBase64UrlString(string input)
        {
            if (string.IsNullOrEmpty(input))
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
            var count = 3 - ((input.Length + 3) % 4);

            if (count == 0 )
            {
                return input;
            }

            return input + new string('=',count);
        }
    }
}
