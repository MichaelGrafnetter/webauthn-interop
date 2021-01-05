using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn
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

        private int _signatureLength;

        private ByteArrayOut _signature;

        private int _certificatesLength;

        private IntPtr _certificates;

        // Following fields are also set for tpm
        public string TPMVersion { get; private set; }

        private int _certificateInfoLength;

        private ByteArrayOut _certificateInfo;

        private int _pubAreaLength;

        private ByteArrayOut _pubArea;

        /// <summary>
        /// Signature that was generated for this attestation.
        /// </summary>
        public byte[] Signature => _signature?.Read(_signatureLength);

        /// <summary>
        /// Array of X.509 DER encoded certificates.
        /// </summary>
        /// <remarks>
        /// The first certificate is the signer, leaf certificate.
        /// It is set for Full Basic Attestation. If not, set then, this is Self Attestation.
        /// </remarks>
        public Certificate[] Certificates => new Certificates(_certificatesLength, _certificates).Items;

        // TODO: Decode CertificateInfo
        public byte[] TPMCertificate => _certificateInfo?.Read(_certificateInfoLength);
        // TODO: Rename TPMPubArea to something more meaningful
        public byte[] TPMPubArea => _pubArea?.Read(_pubAreaLength);
    }
}
