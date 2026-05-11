using System.ComponentModel;
using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Known credential mediation requirements as defined by the Credential Management specification.
/// </summary>
/// <remarks>
/// This enum is a convenience surface for callers that want the currently defined mediation values. The WebAuthn data
/// model and Win32 API wrappers use raw strings so future and custom DOMString values are preserved.
/// </remarks>
[JsonConverter(typeof(JsonStringEnumConverter<Mediation>))]
public enum Mediation
{
    /// <summary>
    /// No mediation specified.
    /// </summary>
    [Description("None")]
    None = 0,

    /// <summary>
    /// The user agent must not show any UI to the user. The operation will fail if user mediation is required.
    /// </summary>
    [JsonStringEnumMemberName(ApiConstants.MediationSilent)]
    [Description("Silent")]
    Silent,

    /// <summary>
    /// The user agent may show UI to the user when needed. This is the default behavior.
    /// </summary>
    [JsonStringEnumMemberName(ApiConstants.MediationOptional)]
    [Description("Optional")]
    Optional,

    /// <summary>
    /// The user agent uses conditional mediation, where credentials are surfaced through autofill (e.g. passkey autofill).
    /// </summary>
    [JsonStringEnumMemberName(ApiConstants.MediationConditional)]
    [Description("Conditional (Autofill)")]
    Conditional,

    /// <summary>
    /// The user agent must always show UI to the user to confirm the operation.
    /// </summary>
    [JsonStringEnumMemberName(ApiConstants.MediationRequired)]
    [Description("Required")]
    Required
}
