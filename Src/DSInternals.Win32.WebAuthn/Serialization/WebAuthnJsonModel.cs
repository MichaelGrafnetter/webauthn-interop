using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace DSInternals.Win32.WebAuthn;

internal static class WebAuthnJsonModel
{
    internal static T? FromJson<T>(string json, JsonTypeInfo<T> jsonTypeInfo)
        where T : class
    {
        try
        {
            return JsonSerializer.Deserialize(json, jsonTypeInfo);
        }
        catch (JsonException ex)
        {
            Debug.WriteLine($"{typeof(T).Name} JSON deserialization error: {ex.Message}");
            return null;
        }
    }
}
