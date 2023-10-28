using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// WebAuthn API Version Information.
    /// </summary>
#pragma warning disable CA1027 // Mark enums with FlagsAttribute
#pragma warning disable CA1008 // Enums should have zero value
    public enum ApiVersion : int
#pragma warning restore CA1027 // Mark enums with FlagsAttribute
#pragma warning restore CA1008 // Enums should have zero value
    {
        /// <summary>
        /// Baseline Version
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_API_VERSION_1.</remarks>
        Version1 = ApiConstants.ApiVersion1,

        /// <summary>
        /// Delta From V1.
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_API_VERSION_2.</remarks>
        Version2 = ApiConstants.ApiVersion2,

        /// <summary>
        /// Delta From V2.
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_API_VERSION_3.</remarks>
        Version3 = ApiConstants.ApiVersion3,

        /// <summary>
        /// Delta From V3.
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_API_VERSION_4.</remarks>
        Version4 = ApiConstants.ApiVersion4,

        /// <summary>
        /// Delta From V4.
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_API_VERSION_5.</remarks>
        Version5 = ApiConstants.ApiVersion5,

        /// <summary>
        /// Delta From V5.
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_API_VERSION_6.</remarks>
        Version6 = ApiConstants.ApiVersion6,

        /// <summary>
        /// Delta From V6.
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_API_VERSION_6.</remarks>
        Version7 = ApiConstants.ApiVersion7,

        /// <summary>
        /// Current Version
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_API_CURRENT_VERSION.</remarks>
#pragma warning disable CA1069 // Enums values should not be duplicated
        Current = ApiConstants.ApiCurrentVersion
#pragma warning restore CA1069 // Enums values should not be duplicated
    }
}
