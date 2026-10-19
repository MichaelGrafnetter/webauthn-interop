namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>Indicates whether a reported authenticator setting is enabled, disabled, or unavailable.</summary>
    /// <remarks>The signed encoding belongs to the Windows management ABI; CTAP2 represents these settings as CBOR booleans.</remarks>
    internal enum ReportedBoolean
    {
        /// <summary>The setting is disabled.</summary>
        False = -1,

        /// <summary>The setting was not reported.</summary>
        Unreported = 0,

        /// <summary>The setting is enabled.</summary>
        True = 1
    }
}
