using Windows.Win32;

namespace DSInternals.Win32.WebAuthn.COSE
{
    /// <summary>
    /// Well-known COSE algorithm specifying the algorithm to use for the credential. https://www.iana.org/assignments/cose/cose.xhtml#algorithms
    /// </summary>
#pragma warning disable CA1008 // Enums should have zero value
    public enum Algorithm : int
#pragma warning restore CA1008 // Enums should have zero value
    {
        /// <summary>
        /// ECDSA with SHA-256
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_ECDSA_P256_WITH_SHA256.</remarks>
        ES256 = PInvoke.WEBAUTHN_COSE_ALGORITHM_ECDSA_P256_WITH_SHA256,

        /// <summary>
        /// ECDSA with SHA-384
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_ECDSA_P384_WITH_SHA384.</remarks>
        ES384 = PInvoke.WEBAUTHN_COSE_ALGORITHM_ECDSA_P384_WITH_SHA384,

        /// <summary>
        /// ECDSA with SHA-512
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_ECDSA_P521_WITH_SHA512.</remarks>
        ES512 = PInvoke.WEBAUTHN_COSE_ALGORITHM_ECDSA_P521_WITH_SHA512,

        /// <summary>
        /// RSASSA-PKCS1-v1_5 with SHA-256
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_RSASSA_PKCS1_V1_5_WITH_SHA256.</remarks>
        RS256 = PInvoke.WEBAUTHN_COSE_ALGORITHM_RSASSA_PKCS1_V1_5_WITH_SHA256,

        /// <summary>
        /// RSASSA-PKCS1-v1_5 with SHA-384
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_RSASSA_PKCS1_V1_5_WITH_SHA384.</remarks>
        RS384 = PInvoke.WEBAUTHN_COSE_ALGORITHM_RSASSA_PKCS1_V1_5_WITH_SHA384,

        /// <summary>
        /// RSASSA-PKCS1-v1_5 with SHA-512
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_RSASSA_PKCS1_V1_5_WITH_SHA512.</remarks>
        RS512 = PInvoke.WEBAUTHN_COSE_ALGORITHM_RSASSA_PKCS1_V1_5_WITH_SHA512,

        /// <summary>
        /// RSASSA-PSS with SHA-256
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_RSA_PSS_WITH_SHA256.</remarks>
        PS256 = PInvoke.WEBAUTHN_COSE_ALGORITHM_RSA_PSS_WITH_SHA256,

        /// <summary>
        /// RSASSA-PSS with SHA-384
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_RSA_PSS_WITH_SHA384.</remarks>
        PS384 = PInvoke.WEBAUTHN_COSE_ALGORITHM_RSA_PSS_WITH_SHA384,

        /// <summary>
        /// RSASSA-PSS with SHA-512
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_ALGORITHM_RSA_PSS_WITH_SHA512.</remarks>
        PS512 = PInvoke.WEBAUTHN_COSE_ALGORITHM_RSA_PSS_WITH_SHA512,

        /// <summary>
        /// RSASSA-PKCS1-v1_5 with SHA-1
        /// </summary>
        RS1 = -65535,

        /// <summary>
        /// EdDSA
        /// </summary>
        EdDSA = -8
    }
}
