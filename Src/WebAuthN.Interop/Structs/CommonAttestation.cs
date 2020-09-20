using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Attestation Info
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_COMMON_ATTESTATION.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class CommonAttestation
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        protected private CommonAttestationVersion Version = CommonAttestationVersion.Current;

        /// <summary>
        /// Hash and Padding Algorithm
        /// </summary>
        /// <remarks>The following won't be set for "fido-u2f" which assumes "ES256".</remarks>
        string Algorithm;

        /// <summary>
        /// COSE algorithm
        /// </summary>
        CoseAlgorithm CoseAlgorithm;

        /// <summary>
        /// Signature that was generated for this attestation.
        /// </summary>
        VariableByteArray Signature;

        /// <summary>
        /// Array of X.509 DER encoded certificates.
        /// </summary>
        /// <remarks>
        /// The first certificate is the signer, leaf certificate.
        /// It is set for Full Basic Attestation. If not, set then, this is Self Attestation.
        /// </remarks>
        Certificates Certificates;

        // TODO: #define WEBAUTHN_ATTESTATION_VER_TPM_2_0   L"2.0"
        // Following are also set for tpm
        string TPMVersion;

        VariableByteArray CertificateInfo;

        VariableByteArray PubArea;
    }
}
