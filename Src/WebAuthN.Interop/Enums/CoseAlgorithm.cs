namespace WebAuthN.Interop
{
    /// <summary>
    /// COSE Algorithms
    /// </summary>
    internal enum CoseAlgorithm : int
    {
        /// <summary> 
        /// ECDSA with SHA-256
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_ECDSA_P256_WITH_SHA256.</remarks>
        ES256 = -7,

        /// <summary> 
        /// ECDSA with SHA-384
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_ECDSA_P384_WITH_SHA384.</remarks>
        ES384 = -35,

        /// <summary> 
        /// ECDSA with SHA-512
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_ECDSA_P521_WITH_SHA512.</remarks>
        ES512 = -36,

        /// <summary> 
        /// RSASSA-PKCS1-v1_5 with SHA-256
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_RSASSA_PKCS1_V1_5_WITH_SHA256.</remarks>
        RS256 = -257,

        /// <summary> 
        /// RSASSA-PKCS1-v1_5 with SHA-384
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_RSASSA_PKCS1_V1_5_WITH_SHA384.</remarks>
        RS384 = -258,

        /// <summary> 
        /// RSASSA-PKCS1-v1_5 with SHA-512
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_RSASSA_PKCS1_V1_5_WITH_SHA512.</remarks>
        RS512 = -259,

        /// <summary> 
        /// RSASSA-PSS with SHA-256
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_RSA_PSS_WITH_SHA256.</remarks>
        PS256 = -37,

        /// <summary> 
        /// RSASSA-PSS with SHA-384
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_RSA_PSS_WITH_SHA384.</remarks>
        PS384 = -38,

        /// <summary> 
        /// RSASSA-PSS with SHA-512
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_RSA_PSS_WITH_SHA512.</remarks>
        PS512 = -39,
    }
}
