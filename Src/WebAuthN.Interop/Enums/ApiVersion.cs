namespace WebAuthN.Interop
{
    /// <summary>
    /// WebAuthN API Version Information.
    /// </summary>
    public enum ApiVersion : int
    {
        /// <summary>
        /// Baseline Version
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_API_VERSION_1.</remarks>
        Version1 = 1,

        /// <summary>
        /// Delta From V1.
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_API_VERSION_2.</remarks>
        Version2 = 2,

        /// <summary>
        /// Current Version
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_API_CURRENT_VERSION.</remarks>
        Current = Version2
    }
}
