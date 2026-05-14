using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.FIDO;

/// <summary>
/// The client data collected during a WebAuthn ceremony, serialized as the clientDataJSON field.
/// Property declaration order is significant: the WebAuthn spec requires type → challenge → origin → crossOrigin.
/// </summary>
public sealed class CollectedClientData
{
    /// <summary>
    /// This member contains the string "webauthn.create" when creating new credentials, and "webauthn.get" when getting an assertion from an existing credential.
    /// </summary>
    /// <remarks>
    /// The purpose of this member is to prevent certain types of signature confusion attacks (where an attacker substitutes one legitimate signature for another).
    /// </remarks>
    /// <see>https://www.w3.org/TR/webauthn-2/#dom-collectedclientdata-type</see>
    [JsonPropertyName("type")]
    [JsonPropertyOrder(0)]
    public required string Type { get; init; }

    /// <summary>
    /// This member contains the base64url encoding of the challenge provided by the Relying Party.
    /// </summary>
    /// <see>https://www.w3.org/TR/webauthn-2/#dom-collectedclientdata-challenge</see>
    [JsonPropertyName("challenge")]
    [JsonConverter(typeof(Base64UrlConverter))]
    [JsonPropertyOrder(1)]
    public required byte[] Challenge { get; init; }

    /// <summary>
    /// This member contains the fully qualified origin of the requester, as provided to the authenticator by the client, in the syntax defined by RFC6454.
    /// </summary>
    /// <see>https://www.w3.org/TR/webauthn-2/#dom-collectedclientdata-origin</see>
    [JsonPropertyName("origin")]
    [JsonPropertyOrder(2)]
    public required string Origin { get; init; }

    /// <summary>
    /// This member contains the inverse of the sameOriginWithAncestors argument value that was passed into the internal method.
    /// </summary>
    [JsonPropertyName("crossOrigin")]
    [JsonPropertyOrder(3)]
    public bool CrossOrigin { get; init; }

    /// <summary>
    /// Contains the fully qualified top-level origin. It is set only if crossOrigin is true.
    /// </summary>
    [JsonPropertyName("topOrigin")]
    [JsonPropertyOrder(4)]
    public string? TopOrigin { get; init; }

    public byte[] ToByteArray()
    {
        return JsonSerializer.SerializeToUtf8Bytes(this, WebAuthnJsonContext.Default.CollectedClientData);
    }

    public static CollectedClientData Create(
        string type,
        byte[] challenge,
        string? hostName = null,
        string? relyingPartyId = null,
        string? remoteClientDataJson = null,
        RemoteDesktopClientOverride? remoteDesktopClientOverride = null)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(challenge);

        if (remoteClientDataJson != null)
        {
            if (remoteDesktopClientOverride != null)
            {
                throw new ArgumentException("The remoteClientDataJSON and remoteDesktopClientOverride extensions cannot be requested together.", nameof(remoteClientDataJson));
            }

            CollectedClientData remoteClientData = FromJson(remoteClientDataJson);
            if (!string.Equals(remoteClientData.Type, type, StringComparison.Ordinal))
            {
                throw new ArgumentException("The remoteClientDataJSON type must match the WebAuthn operation type.", nameof(remoteClientDataJson));
            }

            if (!remoteClientData.Challenge.SequenceEqual(challenge))
            {
                throw new ArgumentException("The remoteClientDataJSON challenge must match the requested challenge.", nameof(remoteClientDataJson));
            }

            return remoteClientData;
        }

        // Normalize empty/whitespace to null so the fallback below works for callers
        // (notably PowerShell, which binds an unset [string] parameter to "" rather than $null).
        if (string.IsNullOrWhiteSpace(hostName)) hostName = null;
        if (string.IsNullOrWhiteSpace(relyingPartyId)) relyingPartyId = null;

        if (hostName is null && relyingPartyId is null)
        {
            throw new ArgumentException("Either hostname or relyingPartyId must be provided.");
        }

        relyingPartyId ??= hostName;
        hostName ??= relyingPartyId;

        string hostOrigin = GetOriginFromRelyingPartyId(hostName!);
        string origin = remoteDesktopClientOverride?.Origin ?? hostOrigin;
        bool crossOrigin = remoteDesktopClientOverride != null && !remoteDesktopClientOverride.SameOriginWithAncestors;
        string? topOrigin = crossOrigin && !string.Equals(hostOrigin, origin, StringComparison.OrdinalIgnoreCase)
            ? hostOrigin
            : null;

        return new CollectedClientData
        {
            Type = type,
            Challenge = challenge,
            Origin = origin,
            CrossOrigin = crossOrigin,
            TopOrigin = topOrigin
        };
    }

    /// <summary>
    /// Constructs the WebAuthn origin from a relying party ID.
    /// </summary>
    /// <param name="relyingPartyId">The relying party identifier (e.g., "login.microsoft.com").</param>
    /// <returns>The origin URL (e.g., "https://login.microsoft.com").</returns>
    /// <exception cref="ArgumentNullException">Thrown when relyingPartyId is null.</exception>
    public static string GetOriginFromRelyingPartyId(string relyingPartyId)
    {
        ArgumentNullException.ThrowIfNull(relyingPartyId);

        if (Uri.TryCreate(relyingPartyId, UriKind.Absolute, out Uri? origin))
        {
            return origin.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped);
        }

        return new UriBuilder(Uri.UriSchemeHttps, relyingPartyId).Uri.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped);
    }

    /// <summary>
    /// Parses clientDataJSON into collected client data.
    /// </summary>
    /// <param name="json">The clientDataJSON value.</param>
    /// <returns>The parsed collected client data.</returns>
    public static CollectedClientData FromJson(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            throw new ArgumentException("The clientDataJSON must not be empty.", nameof(json));
        }

        try
        {
            CollectedClientData? clientData = JsonSerializer.Deserialize(json, WebAuthnJsonContext.Default.CollectedClientData);
            if (clientData == null)
            {
                throw new ArgumentException("The clientDataJSON must be a JSON object.", nameof(json));
            }

            return clientData;
        }
        catch (Exception ex) when (ex is JsonException or FormatException)
        {
            throw new ArgumentException("The clientDataJSON must be valid JSON.", nameof(json), ex);
        }
    }
}
