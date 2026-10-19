namespace DSInternals.Win32.WebAuthn
{
    /// <summary>Describes the authenticator PIN configuration status.</summary>
    /// <remarks>Windows management status combining PIN configuration and blocking information, not a CTAP2 option value.</remarks>
    public enum AuthenticatorPinState
    {
        /// <summary>The state was not reported.</summary>
        Unavailable = 0,

        /// <summary>No PIN is configured.</summary>
        Unset = -1,

        /// <summary>A PIN is configured.</summary>
        Set = 1,

        /// <summary>PIN use is blocked.</summary>
        Blocked = 2
    }
}
