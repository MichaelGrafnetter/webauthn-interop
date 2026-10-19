using DSInternals.Win32.WebAuthn.FIDO;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>Describes the selected authenticator's management capabilities, configuration, and available storage.</summary>
    /// <remarks>Values reflect the session's most recent context refresh, not a new device query.</remarks>
    public sealed class AuthenticatorManagementCapabilities
    {
        /// <summary>Creates an empty snapshot populated only from a reviewed native context.</summary>
        internal AuthenticatorManagementCapabilities() { }

        /// <summary>The private context layout version.</summary>
        public uint ContextVersion { get; internal set; }

        /// <summary>The selected provider's transport bits.</summary>
        public AuthenticatorTransport Transport { get; internal set; }

        /// <summary>The authenticator PIN configuration status.</summary>
        public AuthenticatorPinState PinState { get; internal set; }

        /// <summary>Raw remaining PIN attempts; zero can also mean unreported.</summary>
        public uint PinRetries { get; internal set; }

        /// <summary>The reported fingerprint enrollment state.</summary>
        public AuthenticatorFingerprintState FingerprintState { get; internal set; }

        /// <summary>The reported fingerprint sensor kind.</summary>
        public AuthenticatorFingerprintKind FingerprintKind { get; internal set; }

        /// <summary>The maximum number of good fingerprint samples required for enrollment.</summary>
        /// <remarks>Corresponds to CTAP2 maxCaptureSamplesRequiredForEnroll.</remarks>
        public uint MaximumFingerprintCaptureSamples { get; internal set; }

        /// <summary>The reported credential management availability.</summary>
        public AuthenticatorCredentialManagementState CredentialManagementState { get; internal set; }

        /// <summary>The authenticator's PIN and user-verification policy.</summary>
        public AuthenticatorPinPolicy PinPolicy { get; internal set; } = new AuthenticatorPinPolicy();

        /// <summary>Reported maximum CTAP message bytes, or null when unavailable.</summary>
        public uint? MaximumMessageSize { get; internal set; }

        /// <summary>The estimated number of additional discoverable credentials the authenticator can store, or null if unreported.</summary>
        /// <remarks>A zero estimate indicates that a new credential may not fit; a smaller credential or replacement may still succeed.</remarks>
        public uint? RemainingDiscoverableCredentials { get; internal set; }
    }

}
