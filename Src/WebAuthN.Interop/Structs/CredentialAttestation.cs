using System;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Attestation Info
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_ATTESTATION.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class CredentialAttestation
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private protected CredentialAttestationVersion Version = CredentialAttestationVersion.Current;

        // TODO: Change FormatType to enum
        /*
        #define WEBAUTHN_ATTESTATION_TYPE_PACKED                                L"packed"
        #define WEBAUTHN_ATTESTATION_TYPE_U2F                                   L"fido-u2f"
        #define WEBAUTHN_ATTESTATION_TYPE_TPM                                   L"tpm"
        #define WEBAUTHN_ATTESTATION_TYPE_NONE                                  L"none"
        */
        /// <summary>
        /// Attestation format type
        /// </summary>
        public string FormatType;

        /// <summary>
        /// Authenticator data that was created for this credential.
        /// </summary>
        private VariableByteArrayOut _authenticatorData;

        /// <summary>
        /// Encoded CBOR attestation information
        /// </summary>
        private VariableByteArrayOut _attestation;

        public AttestationDecode AttestationDecodeType;

        /// <summary>
        /// CBOR attestation information.
        /// </summary>
        // TODO: Decode CommonAttestation
        private CommonAttestation[] AttestationDecoded;

        /// <summary>
        /// The CBOR encoded Attestation Object to be returned to the RP.
        /// </summary>
        private VariableByteArrayOut _attestationObject;

        /// <summary>
        /// The CredentialId bytes extracted from the Authenticator Data.
        /// </summary>
        /// <remarks>Used by Edge to return to the RP.</remarks>
        private VariableByteArrayOut _credentialId;

        /// <summary>
        /// WebAuthn Extensions
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_2.</remarks>
        private ExtensionsOut _extensions;

        /// <summary>
        /// The transport that was used.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_3.</remarks>
        public AuthenticatorTransport UsedTransport;

        public byte[] AuthenticatorData => _authenticatorData?.Data;
        public byte[] Attestation => _attestation?.Data;
        public byte[] AttestationObject => _attestationObject?.Data;
        public byte[] CredentialId => _credentialId?.Data;
        public ExtensionOut[] Extensions => _extensions?.Data;
    }
}
