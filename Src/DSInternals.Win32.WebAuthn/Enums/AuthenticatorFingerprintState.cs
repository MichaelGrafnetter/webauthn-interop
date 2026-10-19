namespace DSInternals.Win32.WebAuthn
{
    /// <summary>Describes the authenticator fingerprint enrollment status.</summary>
    /// <remarks>Windows management status combining enrollment and PIN-access information, not the CTAP2 bioEnroll boolean.</remarks>
    public enum AuthenticatorFingerprintState
    {
        /// <summary>Fingerprint enrollment information is unavailable.</summary>
        /// <remarks>This does not indicate whether the authenticator has a fingerprint sensor.</remarks>
        Unavailable = 0,

        /// <summary>No fingerprints are enrolled.</summary>
        Empty = -1,

        /// <summary>Fingerprints are enrolled.</summary>
        Enrolled = 1,

        /// <summary>A PIN is required to access fingerprint information.</summary>
        PinRequired = -2
    }
}
