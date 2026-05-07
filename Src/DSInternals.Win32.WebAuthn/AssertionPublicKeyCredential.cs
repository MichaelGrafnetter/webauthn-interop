using System.Diagnostics;
using System.Text.Json;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Represents the public key credential returned by a WebAuthn assertion operation.
/// </summary>
public sealed class AssertionPublicKeyCredential :
    PublicKeyCredential<AuthenticatorAssertionResponse, AuthenticationExtensionsClientAssertionOutputs>
{
    /// <summary>
    /// Deserializes a JSON string into an assertion public key credential.
    /// </summary>
    /// <param name="json">JSON representation of an assertion public key credential.</param>
    /// <returns>An assertion credential if deserialization is successful; otherwise, null.</returns>
    public static AssertionPublicKeyCredential? FromJson(string json)
    {
        try
        {
            return JsonSerializer.Deserialize(json, WebAuthnJsonContext.Default.AssertionPublicKeyCredential);
        }
        catch (JsonException ex)
        {
            Debug.WriteLine($"AssertionPublicKeyCredential JSON deserialization error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Serializes the credential to JSON.
    /// </summary>
    /// <returns>JSON representation of this credential.</returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.AssertionPublicKeyCredential);
    }
}
