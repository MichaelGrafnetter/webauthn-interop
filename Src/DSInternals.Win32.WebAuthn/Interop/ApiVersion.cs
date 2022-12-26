﻿namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// WebAuthn API Version Information.
    /// </summary>
    public enum ApiVersion : int
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
        /// Current Version
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_API_CURRENT_VERSION.</remarks>
#pragma warning disable CA1069 // Enums values should not be duplicated
        Current = ApiConstants.ApiCurrentVersion
#pragma warning restore CA1069 // Enums values should not be duplicated
    }
}
