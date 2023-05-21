using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// This structure comes from an older version of the API and should only be used by Marshal.Sizeof().
    /// </summary>
    /// <see cref="DSInternals.Win32.WebAuthn.Interop.CredentialAttestation"/>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_ATTESTATION.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal sealed class CredentialAttestationV3
    {
        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_1.</remarks>
        private CredentialAttestationVersion _version;

        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_1.</remarks>
        private string _formatType;

        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_1.</remarks>
        private int _authenticatorDataLength;

        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_1.</remarks>
        private ByteArrayOut _authenticatorData;

        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_1.</remarks>
        private int _attestationLength;

        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_1.</remarks>
        private ByteArrayOut _attestation;

        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_1.</remarks>
        private AttestationDecode _attestationDecodeType;

        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_1.</remarks>
        private IntPtr _attestationDecoded;

        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_1.</remarks>
        private int _attestationObjectLength;

        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_1.</remarks>
        private ByteArrayOut _attestationObject;

        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_1.</remarks>
        private int _credentialIdLength;

        /// <remarks>This field has been present since WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_1.</remarks>
        private ByteArrayOut _credentialId;

        /// <remarks>This field has been added in WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_2.</remarks>
        private ExtensionsOut _extensions;

        /// <remarks>This field has been added in WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_3.</remarks>
        private AuthenticatorTransport _usedTransport;

        /// <summary>
        /// The instantiation of this class is blocked by this private constructor.
        /// </summary>
        private CredentialAttestationV3() { }
    }
}
