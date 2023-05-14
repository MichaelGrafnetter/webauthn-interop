using System;
using Newtonsoft.Json;

namespace DSInternals.Win32.WebAuthn.ActiveDirectory
{
    /// <summary>
    /// Converts the CustomKeyInformation class to and from a base 64 string value.
    /// </summary>
    public class CustomKeyInformationConverter : JsonConverter<CustomKeyInformation>
    {
        public override CustomKeyInformation ReadJson(JsonReader reader, Type objectType, CustomKeyInformation existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            if(reader.TokenType == JsonToken.String)
            {
                try
                {
                    byte[] blob = Convert.FromBase64String((string)reader.Value);
                    return new CustomKeyInformation(blob);
                }
                catch(Exception e)
                {
                    throw new JsonSerializationException("Cannot convert invalid value to CustomKeyInformation.", e);
                }
            }
            else
            {
                throw new JsonSerializationException("Unexpected token parsing CustomKeyInformation.");
            }
        }

        public override void WriteJson(JsonWriter writer, CustomKeyInformation value, JsonSerializer serializer)
        {
            if(writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if(value != null)
            {
                writer.WriteValue(value.ToByteArray());
            }
            else
            {
                writer.WriteNull();
            }
        }
    }
}
