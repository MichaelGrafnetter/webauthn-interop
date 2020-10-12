﻿using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Attestation Info
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_COMMON_ATTESTATION.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class CommonAttestation
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        protected private CommonAttestationVersion Version = CommonAttestationVersion.Current;

        /// <summary>
        /// Hash and Padding Algorithm
        /// </summary>
        /// <remarks>The following won't be set for "fido-u2f" which assumes "ES256".</remarks>
        public string Algorithm { get; private set; }

        /// <summary>
        /// COSE algorithm
        /// </summary>
        public CoseAlgorithm CoseAlgorithm { get; private set; }

        /// <summary>
        /// Signature that was generated for this attestation.
        /// </summary>
        private VariableByteArrayOut _signature;

        /// <summary>
        /// Array of X.509 DER encoded certificates.
        /// </summary>
        /// <remarks>
        /// The first certificate is the signer, leaf certificate.
        /// It is set for Full Basic Attestation. If not, set then, this is Self Attestation.
        /// </remarks>
        private Certificates _certificates;

        // TODO: #define WEBAUTHN_ATTESTATION_VER_TPM_2_0   L"2.0"
        // Following are also set for tpm
        public string TPMVersion { get; private set; }

        private VariableByteArrayOut _certificateInfo;

        private VariableByteArrayOut _pubArea;

        public byte[] Signature => _signature?.Data;
        // TODO: Change data type of Certificates
        public Certificate[] Certificates => _certificates?.Data;
        // TODO: Decode CertificateInfo
        public byte[] CertificateInfo => _certificateInfo?.Data;
        // TODO: Rename PubArea
        public byte[] PubArea => _pubArea?.Data;
    }
}
