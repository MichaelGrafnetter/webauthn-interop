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

        /// <summary>
        /// Attestation format type
        /// </summary>
        public string FormatType { get; private set; }

        /// <summary>
        /// Authenticator data that was created for this credential.
        /// </summary>
        private SafeByteArrayOut _authenticatorData;

        /// <summary>
        /// Encoded CBOR attestation information
        /// </summary>
        private SafeByteArrayOut _attestation;

        private AttestationDecode _attestationDecodeType;

        /// <summary>
        /// CBOR attestation information.
        /// </summary>
        private IntPtr _attestationDecoded;

        /// <summary>
        /// The CBOR encoded Attestation Object to be returned to the RP.
        /// </summary>
        private SafeByteArrayOut _attestationObject;

        /// <summary>
        /// The CredentialId bytes extracted from the Authenticator Data.
        /// </summary>
        /// <remarks>Used by Edge to return to the RP.</remarks>
        private SafeByteArrayOut _credentialId;

        /// <summary>
        /// WebAuthn Extensions
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_2.</remarks>
        private ExtensionsOut _extensions;

        /// <summary>
        /// The transport that was used.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_3.</remarks>
        public CtapTransport UsedTransport { get; private set; }

        public byte[] AuthenticatorData => _authenticatorData?.Data;
        public byte[] Attestation => _attestation?.Data;
        public byte[] AttestationObject => _attestationObject?.Data;
        public byte[] CredentialId => _credentialId?.Data;
        public ExtensionOut[] Extensions => _extensions?.Data;

        public CommonAttestation AttestationDecoded
        {
            get
            {
                if (_attestationDecodeType != AttestationDecode.Common)
                {
                    // Nothing else is currently supported by the API.
                    return null;
                }

                return Marshal.PtrToStructure<CommonAttestation>(_attestationDecoded);
            }
        }
    }
}
