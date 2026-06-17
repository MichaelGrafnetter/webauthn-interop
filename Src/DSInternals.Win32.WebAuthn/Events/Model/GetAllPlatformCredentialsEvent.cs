using System.Collections.Generic;

namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents a GetAllPlatformCredentials CTAP command completed event (Event ID 2102).
/// </summary>
public sealed class GetAllPlatformCredentialsEvent : CtapCommandCompletedEvent
{
    /// <summary>
    /// Credentials returned by the platform credential enumeration response.
    /// </summary>
    public IReadOnlyList<CredentialDetails>? Credentials { get; set; }
}
