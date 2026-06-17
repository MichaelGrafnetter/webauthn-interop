using System;

namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Aggregated data about one credential discovered during platform or plugin credential enumeration.
/// </summary>
public sealed class CredentialEnumeration : WebAuthnOperation
{
    /// <summary>
    /// Gets the operation type.
    /// </summary>
    public override string OperationType => "Credential Enumeration";

    /// <summary>
    /// The plugin authenticator COM class ID.
    /// </summary>
    public Guid? PluginClassId { get; set; }

    /// <summary>
    /// Whether hybrid transport credentials were filtered from the enumeration.
    /// </summary>
    public bool? FilterHybridTransport { get; set; }

    /// <summary>
    /// The relying party display name.
    /// </summary>
    public string? RelyingPartyName { get; set; }

    /// <summary>
    /// The user ID associated with the credential.
    /// </summary>
    public byte[]? UserId { get; set; }

    /// <summary>
    /// The user display name associated with the credential.
    /// </summary>
    public string? UserDisplayName { get; set; }

    /// <summary>
    /// Indicates whether the credential is removable.
    /// </summary>
    public bool? Removable { get; set; }

    /// <summary>
    /// Indicates whether the credential is backed up.
    /// </summary>
    public bool? BackedUp { get; set; }

    /// <summary>
    /// The authenticator name reported for the credential.
    /// </summary>
    public string? AuthenticatorName { get; set; }

    /// <summary>
    /// The authenticator logo reported for the credential.
    /// </summary>
    public string? AuthenticatorLogo { get; set; }

    /// <summary>
    /// Indicates whether the credential is a third-party payment credential.
    /// </summary>
    public bool? ThirdPartyPayment { get; set; }

    /// <summary>
    /// The transports reported for the credential.
    /// </summary>
    public AuthenticatorTransport? Transports { get; set; }
}
