using System;
using System.Collections.Generic;

namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Represents an AddPluginAuthenticatorCredentials CTAP command started event (Event ID 2100).
/// </summary>
public sealed class AddPluginAuthenticatorCredentialsEvent : CtapCommandStartedEvent
{
    /// <summary>
    /// Whether hybrid transport credentials are filtered.
    /// </summary>
    public bool? FilterHybridTransport { get; set; }

    /// <summary>
    /// Credentials included in the plugin authenticator add request.
    /// </summary>
    public IReadOnlyList<CredentialDetails>? Credentials { get; set; }

    /// <summary>
    /// The plugin authenticator COM class ID.
    /// </summary>
    public Guid? PluginClassId { get; set; }
}
