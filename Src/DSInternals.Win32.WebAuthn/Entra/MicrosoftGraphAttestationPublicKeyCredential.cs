using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.Entra;

/// <summary>
/// Microsoft Graph variant of the attestation public key credential.
/// Only carries the fields accepted by the fido2AuthenticationMethod schema
/// (id, response, clientExtensionResults); the standard WebAuthn type,
/// rawId, and authenticatorAttachment fields are intentionally omitted.
/// </summary>
public sealed class MicrosoftGraphAttestationPublicKeyCredential
{
    /// <summary>
    /// The credential identifier (Base64Url encoded).
    /// </summary>
    [JsonPropertyName("id")]
    [JsonRequired]
    [JsonConverter(typeof(Base64UrlConverter))]
    public required byte[] Id { get; init; }

    /// <summary>
    /// Authenticator response payload.
    /// </summary>
    [JsonPropertyName("response")]
    [JsonRequired]
    public required AuthenticatorAttestationResponse Response { get; init; }

    /// <summary>
    /// Outputs of client extension processing.
    /// </summary>
    [JsonPropertyName("clientExtensionResults")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public AuthenticationExtensionsClientAttestationOutputs? ClientExtensionResults { get; init; }

    /// <summary>
    /// Initializes a new Microsoft Graph attestation public key credential.
    /// Used by the JSON deserializer.
    /// </summary>
    /// <param name="id">Credential identifier.</param>
    /// <param name="response">Authenticator attestation response.</param>
    /// <param name="clientExtensionResults">Outputs of client extension processing.</param>
    [JsonConstructor]
    [SetsRequiredMembers]
    public MicrosoftGraphAttestationPublicKeyCredential(
        byte[] id,
        AuthenticatorAttestationResponse response,
        AuthenticationExtensionsClientAttestationOutputs? clientExtensionResults)
    {
        Id = id;
        Response = response;
        ClientExtensionResults = clientExtensionResults;
    }

    /// <summary>
    /// Initializes a new Microsoft Graph attestation public key credential by copying values
    /// from a standard <see cref="AttestationPublicKeyCredential"/> produced by the WebAuthn API.
    /// Only the response fields defined by the Microsoft Graph
    /// <c>webauthnAuthenticatorAttestationResponse</c> schema (attestationObject, clientDataJSON)
    /// are forwarded; other WebAuthn-standard fields are dropped.
    /// </summary>
    /// <param name="source">The source credential returned by the authenticator.</param>
    [SetsRequiredMembers]
    public MicrosoftGraphAttestationPublicKeyCredential(AttestationPublicKeyCredential source)
    {
        ArgumentNullException.ThrowIfNull(source);

        Id = source.Id;
        Response = new AuthenticatorAttestationResponse
        {
            ClientDataJson = source.Response.ClientDataJson,
            AttestationObject = source.Response.AttestationObject
        };
        ClientExtensionResults = source.ClientExtensionResults;
    }

    /// <summary>
    /// Deserializes a JSON string into a Microsoft Graph attestation public key credential.
    /// </summary>
    /// <param name="json">JSON representation of an attestation public key credential.</param>
    /// <returns>An attestation credential if deserialization is successful; otherwise, null.</returns>
    public static MicrosoftGraphAttestationPublicKeyCredential? FromJson(string json)
    {
        try
        {
            return JsonSerializer.Deserialize(json, WebAuthnJsonContext.Default.MicrosoftGraphAttestationPublicKeyCredential);
        }
        catch (JsonException ex)
        {
            Debug.WriteLine($"MicrosoftGraphAttestationPublicKeyCredential JSON deserialization error: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Serializes the credential to JSON in the shape accepted by Microsoft Graph.
    /// </summary>
    /// <returns>JSON representation of this credential.</returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.MicrosoftGraphAttestationPublicKeyCredential);
    }
}
