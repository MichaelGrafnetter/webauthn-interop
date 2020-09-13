using System.Runtime.InteropServices;

namespace WebAuthN.Interop.Structs
{
    /// <summary>
    /// Attestation Info
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_COMMON_ATTESTATION.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct CommonAttestation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COMMON_ATTESTATION_CURRENT_VERSION.</remarks>
        private const int CurrentVersion = 1;

        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        uint Version;

        /// <summary>
        /// Hash and Padding Algorithm
        /// </summary>
        /// <remarks>The following won't be set for "fido-u2f" which assumes "ES256".</remarks>
        //
        // 
        string Algorithm;

        // TODO: Fix type and comment.
        /// <summary>
        /// COSE algorithm
        /// </summary>
        int CoseAlgorithm;

        /// <summary>
        /// Length of the signature.
        /// </summary>
        uint SignatureLength;

        /// <summary>
        /// Signature that was generated for this attestation.
        /// </summary>
        byte[] Signature;

        int CertificateCount;
        
        /// <summary>
        /// Array of X.509 DER encoded certificates.
        /// </summary>
        /// <remarks>
        /// The first certificate is the signer, leaf certificate.
        /// It is set for Full Basic Attestation. If not, set then, this is Self Attestation.
        /// </remarks>
        Certificate[] Certificates;

        // TODO: Refactor the TPM part.
        // TODO: #define WEBAUTHN_ATTESTATION_VER_TPM_2_0   L"2.0"
        // Following are also set for tpm
        string TPMVersion; // "2.0"
        
        uint CertificateInfoLength;
        
        byte[] CertificateInfo;

        uint PubAreaLength;
        
        byte[] PubArea;
    }
}
