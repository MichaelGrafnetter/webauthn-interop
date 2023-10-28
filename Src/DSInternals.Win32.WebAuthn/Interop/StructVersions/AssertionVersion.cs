namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// The version of the WEBAUTHN_ASSERTION structure, to allow for modifications in the future.
    /// </summary>
    internal enum AssertionVersion
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_ASSERTION_VERSION_1.
        /// </remarks>
        Version1 = ApiConstants.AssertionVersion1,

        /// <remarks>
        /// Corresponds to WEBAUTHN_ASSERTION_VERSION_2.
        /// </remarks>
        Version2 = ApiConstants.AssertionVersion2,

        /// <remarks>
        /// Corresponds to WEBAUTHN_ASSERTION_VERSION_3.
        /// </remarks>
        Version3 = ApiConstants.AssertionVersion3,

        /// <remarks>
        /// Corresponds to WEBAUTHN_ASSERTION_VERSION_4.
        /// </remarks>
        Version4 = ApiConstants.AssertionVersion4,

        /// <remarks>
        /// Corresponds to WEBAUTHN_ASSERTION_VERSION_5.
        /// </remarks>
        Version5 = ApiConstants.AssertionVersion5,

        /// <remarks>
        /// Corresponds to WEBAUTHN_ASSERTION_CURRENT_VERSION.
        /// </remarks>
#pragma warning disable CA1069 // Enums values should not be duplicated
        Current = ApiConstants.AssertionCurrentVersion
#pragma warning restore CA1069 // Enums values should not be duplicated
    }
}
