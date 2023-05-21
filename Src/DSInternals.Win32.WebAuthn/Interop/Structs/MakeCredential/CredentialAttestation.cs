using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml;

namespace DSInternals.Win32.WebAuthn.Interop
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
        public CredentialAttestationVersion Version { get; private set; }

        /// <summary>
        /// Attestation format type
        /// </summary>
        public string FormatType { get; private set; }

        private int _authenticatorDataLength;

        private ByteArrayOut _authenticatorData;

        private int _attestationLength;

        private ByteArrayOut _attestation;

        private AttestationDecode _attestationDecodeType;

        private IntPtr _attestationDecoded;

        private int _attestationObjectLength;

        private ByteArrayOut _attestationObject;

        private int _credentialIdLength;

        private ByteArrayOut _credentialId;

        /// <summary>
        /// WebAuthn Extensions
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_2.</remarks>
        public ExtensionsOut Extensions { get; private set; }

        /// <summary>
        /// The transport that was used.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_3.</remarks>
        public AuthenticatorTransport UsedTransport { get; private set; }

        /// <summary>
        /// Indicates whether the attestation statement contains uniquely identifying information (epAtt).
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_4.</remarks>
        public bool ContainsUniquelyIdentifyingInformation { get; private set; }

        /// <remarks>This field has been added in WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_4.</remarks>
        public bool LargeBlobSupported { get; private set; }

        /// <remarks>This field has been added in WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_4.</remarks>
        public bool ResidentKey { get; private set; }

        /// <summary>
        /// Indicates whether the Pseudo-random function (PRF) extension is enabled.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_5.</remarks>
        public bool PseudoRandomFunctionEnabled { get; private set; }

        /// <summary>
        /// Authenticator data that was created for this credential.
        /// </summary>
        public byte[] AuthenticatorData => _authenticatorData?.Read(_authenticatorDataLength);

        /// <summary>
        /// Encoded CBOR attestation information
        /// </summary>
        public byte[] Attestation => _attestation?.Read(_attestationLength);

        /// <summary>
        /// The CBOR encoded Attestation Object to be returned to the RP.
        /// </summary>
        public byte[] AttestationObject => _attestationObject?.Read(_attestationObjectLength);

        /// <summary>
        /// The CredentialId bytes extracted from the Authenticator Data.
        /// </summary>
        /// <remarks>Used by Edge to return to the RP.</remarks>
        public byte[] CredentialId => _credentialId?.Read(_credentialIdLength);

        /// <summary>
        /// CBOR attestation information.
        /// </summary>
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
