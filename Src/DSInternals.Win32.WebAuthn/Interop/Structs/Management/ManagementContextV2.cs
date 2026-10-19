using System;
using System.Runtime.InteropServices;
using DSInternals.Win32.WebAuthn.FIDO;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>Extends the authenticator management status with configuration policy and storage limits.</summary>
    /// <remarks>
    /// A private Windows memory layout, not the CTAP2 authenticatorGetInfo CBOR map.
    /// On 64-bit platforms, V1 tail padding is reused by the first policy field; embedding a complete V1 would shift the suffix.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct ManagementContextV2
    {
        #region V1 members

        /// <summary>Private context layout version.</summary>
        internal uint Version;

        /// <summary>Borrowed opaque context pointer; never free it independently.</summary>
        internal IntPtr InternalContext;

        /// <summary>The selected provider's transport bits.</summary>
        internal AuthenticatorTransport Transport;

        /// <summary>The authenticator PIN configuration status.</summary>
        internal AuthenticatorPinState PinState;

        /// <summary>Reported PIN retry count; zero can also mean unreported.</summary>
        internal uint PinRetries;

        /// <summary>The authenticator fingerprint enrollment status.</summary>
        internal AuthenticatorFingerprintState FingerprintState;

        /// <summary>Reported fingerprint sensor kind.</summary>
        internal AuthenticatorFingerprintKind FingerprintKind;

        /// <summary>Reported maximum good fingerprint samples required for enrollment.</summary>
        internal uint MaximumFingerprintCaptureSamples;

        /// <summary>The availability of credential management on the authenticator.</summary>
        internal AuthenticatorCredentialManagementState CredentialManagementState;

        #endregion

        #region V2 members

        /// <summary>Native support status for authenticator configuration.</summary>
        [MarshalAs(UnmanagedType.Bool)]
        private bool _authenticatorConfig;

        /// <summary>Native enterprise attestation setting status.</summary>
        private ReportedBoolean _enterpriseAttestation;

        /// <summary>Native Always Require User Verification setting status.</summary>
        private ReportedBoolean _alwaysUv;

        /// <summary>Native minimum PIN length in Unicode code points; zero denotes an unreported value.</summary>
        private uint _minimumPinLength;

        /// <summary>Native PIN-change requirement status.</summary>
        private ReportedBoolean _forcePinChange;

        /// <summary>Native support status for minimum PIN length configuration.</summary>
        [MarshalAs(UnmanagedType.Bool)]
        private bool _setMinPinLength;

        /// <summary>Native limit on RP IDs accepted by minimum PIN length configuration.</summary>
        private uint _maximumRpIds;

        /// <summary>Native serialized large-blob capacity including checksum; zero denotes an unreported value.</summary>
        private uint _maximumLargeBlobBytes;

        /// <summary>Native maximum CTAP message size in bytes; zero denotes an unreported value.</summary>
        private uint _maximumMessageSize;

        /// <summary>Indicates whether the native remaining credential capacity contains a reported value.</summary>
        [MarshalAs(UnmanagedType.Bool)]
        private bool _remainingCredentialsPresent;

        /// <summary>Native remaining credential capacity, qualified by its presence marker.</summary>
        private uint _remainingCredentials;

        /// <summary>Gets whether authenticator configuration is supported, or null if unreported.</summary>
        internal bool? AuthenticatorConfig => _authenticatorConfig ? true : null;

        /// <summary>Gets whether enterprise attestation is enabled, or null if unreported.</summary>
        internal bool? EnterpriseAttestation => _enterpriseAttestation.ToNullableBoolean();

        /// <summary>Gets whether user verification is always required, or null if unreported.</summary>
        internal bool? AlwaysUv => _alwaysUv.ToNullableBoolean();

        /// <summary>Gets the minimum PIN length in Unicode code points, or null if unreported.</summary>
        internal uint? MinimumPinLength => ReportedCount(_minimumPinLength);

        /// <summary>Gets whether a PIN change is required, or null if unreported.</summary>
        internal bool? ForcePinChange => _forcePinChange.ToNullableBoolean();

        /// <summary>Gets whether minimum PIN length configuration is supported, or null if unreported.</summary>
        internal bool? SetMinPinLength => _setMinPinLength ? true : null;

        /// <summary>Gets the maximum RP-ID count for minimum PIN length configuration, or null if unreported.</summary>
        /// <remarks>When configuration support is reported, zero means that no additional RP IDs are accepted.</remarks>
        internal uint? MaximumRpIds => SetMinPinLength == true ? _maximumRpIds : ReportedCount(_maximumRpIds);

        /// <summary>Gets the serialized large-blob capacity including checksum, or null if unreported.</summary>
        internal uint? MaximumLargeBlobBytes => ReportedCount(_maximumLargeBlobBytes);

        /// <summary>Gets the maximum CTAP message size in bytes, or null if unreported.</summary>
        internal uint? MaximumMessageSize => ReportedCount(_maximumMessageSize);

        /// <summary>Gets the estimated remaining credential capacity, including zero, or null if unreported.</summary>
        internal uint? RemainingCredentials => _remainingCredentialsPresent ? _remainingCredentials : null;

        /// <summary>Decodes a count whose native zero represents unreported information.</summary>
        /// <param name="value">The native count.</param>
        /// <returns>The reported count, or null when zero was supplied.</returns>
        private static uint? ReportedCount(uint value) => value == 0 ? null : value;

        #endregion
    }
}
