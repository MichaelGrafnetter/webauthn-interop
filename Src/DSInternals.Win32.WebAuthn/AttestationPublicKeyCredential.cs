using System.Diagnostics;
using System.Text.Json;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Represents the public key credential returned by a WebAuthn attestation operation.
/// </summary>
public sealed class AttestationPublicKeyCredential :
    PublicKeyCredential<AuthenticatorAttestationResponse, AuthenticationExtensionsClientAttestationOutputs>
{
    /// <summary>
    /// Deserializes a JSON string into an attestation public key credential.
    /// </summary>
    /// <param name="json">JSON representation of an attestation public key credential.</param>
    /// <returns>An attestation credential if deserialization is successful; otherwise, null.</returns>
    public static AttestationPublicKeyCredential? FromJson(string json)
    {
        try
        {
            return JsonSerializer.Deserialize(json, WebAuthnJsonContext.Default.AttestationPublicKeyCredential);
        }
        catch (JsonException ex)
        {
            Debug.WriteLine($"AttestationPublicKeyCredential JSON deserialization error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Serializes the credential to JSON.
    /// </summary>
    /// <returns>JSON representation of this credential.</returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.AttestationPublicKeyCredential);
    }
}
