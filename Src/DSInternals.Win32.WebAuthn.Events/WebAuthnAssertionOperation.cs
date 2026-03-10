namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Aggregated data about a WebAuthn authentication (GetAssertion) operation,
/// combining information from multiple related WebAuthN events.
/// </summary>
public sealed class WebAuthnAssertionOperation : WebAuthnOperation
{
    public override string OperationType => "Authentication";

    /// <summary>
    /// The hash algorithm used for client data (e.g., SHA-256).
    /// </summary>
    public string? ClientDataHashAlgId { get; set; }
}
