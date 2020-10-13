namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// COSE Algorithms
    /// </summary>
    internal enum CoseAlgorithm : int
    {
        // TODO: Add algorithm -65535 from TPM?

        /// <summary> 
        /// ECDSA with SHA-256
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_ECDSA_P256_WITH_SHA256.</remarks>
        ES256 = ApiConstants.CoseAlgorithmEcdsaP256WithSha256,

        /// <summary> 
        /// ECDSA with SHA-384
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_ECDSA_P384_WITH_SHA384.</remarks>
        ES384 = ApiConstants.CoseAlgorithmEcdsaP384WithSha384,

        /// <summary> 
        /// ECDSA with SHA-512
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_ECDSA_P521_WITH_SHA512.</remarks>
        ES512 = ApiConstants.CoseAlgorithmEcdsaP521WithSha512,

        /// <summary> 
        /// RSASSA-PKCS1-v1_5 with SHA-256
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_RSASSA_PKCS1_V1_5_WITH_SHA256.</remarks>
        RS256 = ApiConstants.CoseAlgorithmRsassaPkcs1V15WithSha256,

        /// <summary> 
        /// RSASSA-PKCS1-v1_5 with SHA-384
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_RSASSA_PKCS1_V1_5_WITH_SHA384.</remarks>
        RS384 = ApiConstants.CoseAlgorithmRsassaPkcs1V15WithSha384,

        /// <summary> 
        /// RSASSA-PKCS1-v1_5 with SHA-512
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_RSASSA_PKCS1_V1_5_WITH_SHA512.</remarks>
        RS512 = ApiConstants.CoseAlgorithmRsassaPkcs1V15WithSha512,

        /// <summary> 
        /// RSASSA-PSS with SHA-256
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_RSA_PSS_WITH_SHA256.</remarks>
        PS256 = ApiConstants.CoseAlgorithmRsaPssWithSha256,

        /// <summary> 
        /// RSASSA-PSS with SHA-384
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_RSA_PSS_WITH_SHA384.</remarks>
        PS384 = ApiConstants.CoseAlgorithmRsaPssWithSha384,

        /// <summary> 
        /// RSASSA-PSS with SHA-512
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_RSA_PSS_WITH_SHA512.</remarks>
        PS512 = ApiConstants.CoseAlgorithmRsaPssWithSha512,
    }
}
