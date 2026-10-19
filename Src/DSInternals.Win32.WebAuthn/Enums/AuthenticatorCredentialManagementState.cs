namespace DSInternals.Win32.WebAuthn
{
    /// <summary>Describes the availability of credential management on an authenticator.</summary>
    /// <remarks>Windows management status combining availability and PIN-access information, not the CTAP2 credMgmt boolean.</remarks>
    public enum AuthenticatorCredentialManagementState
    {
        /// <summary>Availability was not reported.</summary>
        Unavailable = 0,

        /// <summary>Credential management is available.</summary>
        Available = 1,

        /// <summary>PIN authentication is required to access credential management.</summary>
        PinRequired = -2
    }
}
