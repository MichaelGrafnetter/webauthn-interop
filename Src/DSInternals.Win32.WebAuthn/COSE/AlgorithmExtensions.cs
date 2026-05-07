using System.Security.Cryptography;

namespace DSInternals.Win32.WebAuthn.COSE;

/// <summary>
/// Provides metadata for COSE algorithms.
/// </summary>
public static class AlgorithmExtensions
{
    extension(Algorithm algorithm)
    {
        /// <summary>
        /// Gets the cryptographic algorithm family name.
        /// </summary>
        public string AlgorithmName => algorithm switch
        {
            Algorithm.ES256 or Algorithm.ES384 or Algorithm.ES512 or Algorithm.ES256K => "ECDSA",
            Algorithm.EdDSA => "EdDSA",
            Algorithm.RS256 or Algorithm.RS384 or Algorithm.RS512 or Algorithm.RS1 => "RSASSA-PKCS1-v1_5",
            Algorithm.PS256 or Algorithm.PS384 or Algorithm.PS512 => "RSASSA-PSS",
            _ => algorithm.ToString()
        };

        /// <summary>
        /// Gets a value indicating whether the algorithm uses RSA.
        /// </summary>
        public bool IsRSA => algorithm switch
        {
            Algorithm.RS256 or Algorithm.RS384 or Algorithm.RS512 or Algorithm.RS1
                or Algorithm.PS256 or Algorithm.PS384 or Algorithm.PS512 => true,
            _ => false
        };

        /// <summary>
        /// Gets a value indicating whether the algorithm uses ECDSA.
        /// </summary>
        public bool IsECDSA => algorithm switch
        {
            Algorithm.ES256 or Algorithm.ES384 or Algorithm.ES512 or Algorithm.ES256K => true,
            _ => false
        };

        /// <summary>
        /// Gets the elliptic curve associated with the algorithm.
        /// </summary>
        public EllipticCurve? Curve => algorithm switch
        {
            Algorithm.ES256 => EllipticCurve.P256,
            Algorithm.ES384 => EllipticCurve.P384,
            Algorithm.ES512 => EllipticCurve.P521,
            Algorithm.ES256K => EllipticCurve.P256K,
            Algorithm.EdDSA => EllipticCurve.Ed25519,
            _ => null
        };

        /// <summary>
        /// Gets the key type or curve name associated with the algorithm.
        /// </summary>
        public string? KeyTypeName => algorithm switch
        {
            Algorithm.EdDSA => "Ed25519",
            Algorithm.ES256 => "P-256",
            Algorithm.ES384 => "P-384",
            Algorithm.ES512 => "P-521",
            Algorithm.RS256 or Algorithm.RS384 or Algorithm.RS512 or Algorithm.RS1
                or Algorithm.PS256 or Algorithm.PS384 or Algorithm.PS512 => "RSA",
            _ => null
        };

        /// <summary>
        /// Gets the key length associated with the algorithm.
        /// </summary>
        public int? KeyLength => algorithm switch
        {
            Algorithm.ES256 or Algorithm.ES256K or Algorithm.EdDSA => 256,
            Algorithm.ES384 => 384,
            Algorithm.ES512 => 521,
            Algorithm.RS256 or Algorithm.RS384 or Algorithm.RS512 or Algorithm.RS1
                or Algorithm.PS256 or Algorithm.PS384 or Algorithm.PS512 => 2048,
            _ => null
        };

        /// <summary>
        /// Gets the hash algorithm associated with the algorithm.
        /// </summary>
        public HashAlgorithmName? HashAlgorithm => algorithm switch
        {
            Algorithm.ES256 or Algorithm.RS256 or Algorithm.PS256 => HashAlgorithmName.SHA256,
            Algorithm.ES384 or Algorithm.RS384 or Algorithm.PS384 => HashAlgorithmName.SHA384,
            Algorithm.ES512 or Algorithm.RS512 or Algorithm.PS512 => HashAlgorithmName.SHA512,
            Algorithm.RS1 => HashAlgorithmName.SHA1,
            Algorithm.EdDSA => HashAlgorithmName.SHA512,
            _ => null
        };

        /// <summary>
        /// Gets the RSA signature padding associated with the algorithm.
        /// </summary>
        public RSASignaturePadding? RsaPadding => algorithm switch
        {
            Algorithm.PS256 or Algorithm.PS384 or Algorithm.PS512 => RSASignaturePadding.Pss,
            Algorithm.RS256 or Algorithm.RS384 or Algorithm.RS512 or Algorithm.RS1 => RSASignaturePadding.Pkcs1,
            _ => null
        };

        /// <summary>
        /// Gets the COSE key type associated with the algorithm.
        /// </summary>
        public KeyType KeyType => algorithm switch
        {
            Algorithm.EdDSA => COSE.KeyType.OKP,
            Algorithm.ES256 or Algorithm.ES384 or Algorithm.ES512 or Algorithm.ES256K => COSE.KeyType.EC2,
            Algorithm.RS256 or Algorithm.RS384 or Algorithm.RS512 or Algorithm.RS1
                or Algorithm.PS256 or Algorithm.PS384 or Algorithm.PS512 => COSE.KeyType.RSA,
            _ => throw new System.NotSupportedException($"Unsupported COSE algorithm: {algorithm}")
        };
    }
}
