using System.Collections.Generic;

namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Parses GetAllPlatformCredentials CBOR response payloads.
/// </summary>
internal static class GetAllPlatformCredentialsResponseParser
{
    /// <summary>
    /// Parses a CBOR-encoded GetAllPlatformCredentials response.
    /// </summary>
    /// <param name="response">The CBOR-encoded response payload from the event log.</param>
    /// <returns>The decoded credential list, or <see langword="null"/> when the payload is empty or invalid.</returns>
    public static IReadOnlyList<CredentialDetails>? Parse(byte[]? response)
    {
        return AddPluginAuthenticatorCredentialsRequestParser.ParseCredentialDetails(response);
    }
}
