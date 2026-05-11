using System;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Extension methods for known credential mediation requirements.
/// </summary>
public static class MediationExtensions
{
    /// <summary>
    /// Converts a known mediation enum value to the DOMString value accepted by the Credential Management API.
    /// </summary>
    public static string? ToJsonString(this Mediation mediation) => mediation switch
    {
        Mediation.None => null,
        Mediation.Silent => ApiConstants.MediationSilent,
        Mediation.Optional => ApiConstants.MediationOptional,
        Mediation.Conditional => ApiConstants.MediationConditional,
        Mediation.Required => ApiConstants.MediationRequired,
        _ => throw new ArgumentOutOfRangeException(nameof(mediation), mediation, "Unknown mediation requirement.")
    };
}
