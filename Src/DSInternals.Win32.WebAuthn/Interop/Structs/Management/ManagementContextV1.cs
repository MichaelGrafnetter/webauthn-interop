using System;
using System.Runtime.InteropServices;
using DSInternals.Win32.WebAuthn.FIDO;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>Contains the selected authenticator's PIN, fingerprint, and credential-management status.</summary>
    /// <remarks>A private Windows memory layout, not a CTAP2 CBOR response structure.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct ManagementContextV1
    {
        /// <summary>Private context layout version, independent of the public WebAuthn API version.</summary>
        internal uint Version;

        /// <summary>Borrowed opaque context pointer owned by the outer management handle.</summary>
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
    }
}
