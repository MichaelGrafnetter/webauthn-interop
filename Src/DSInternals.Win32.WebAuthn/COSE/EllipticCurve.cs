namespace DSInternals.Win32.WebAuthn.COSE
{
    /// <summary>
    /// COSE Elliptic Curves https://www.iana.org/assignments/cose/cose.xhtml#elliptic-curves
    /// </summary>
    public enum EllipticCurve
    {
        /// <summary>
        /// This value is reserved
        /// </summary>
        Reserved = 0,

        /// <summary>
        /// NIST P-256 also known as secp256r1
        /// </summary>
        P256 = 1,

        /// <summary>
        /// NIST P-384 also known as secp384r1
        /// </summary>
        P384 = 2,

        /// <summary>
        /// NIST P-521 also known as secp521r1
        /// </summary>
        P521 = 3,

        /// <summary>
        /// X25519 for use w/ ECDH only
        /// </summary>
        X25519 = 4,

        /// <summary>
        /// X448 for use with ECDH only
        /// </summary>
        X448 = 5,

        /// <summary>
        /// Ed25519 for use with EdDSA only
        /// </summary>
        Ed25519 = 6,

        /// <summary>
        /// Ed448 for use with EdDSA only
        /// </summary>
        Ed448 = 7,

        /// <summary>
        /// secp256k1 (pending IANA - requested assignment 8)
        /// </summary>
        P256K = 8
    }
}
