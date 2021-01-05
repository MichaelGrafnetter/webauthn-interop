namespace DSInternals.Win32.WebAuthn.COSE
{
    /// <summary>
    /// COSE Algorithms https://www.iana.org/assignments/cose/cose.xhtml#algorithms
    /// </summary>
    public enum Algorithm
    {
        /// <summary> 
        /// RSASSA-PKCS1-v1_5 w/ SHA-1
        /// </summary>
        RS1 = -65535,

        /// <summary> 
        /// RSASSA-PKCS1-v1_5 w/ SHA-512
        /// </summary>
        RS512 = -259,

        /// <summary> 
        /// RSASSA-PKCS1-v1_5 w/ SHA-384
        /// </summary>
        RS384 = -258,

        /// <summary> 
        /// RSASSA-PKCS1-v1_5 w/ SHA-256
        /// </summary>
        RS256 = -257,

        /// <summary> 
        /// RSASSA-PSS w/ SHA-512
        /// </summary>
        PS512 = -39,

        /// <summary> 
        /// RSASSA-PSS w/ SHA-384
        /// </summary>
        PS384 = -38,

        /// <summary> 
        /// RSASSA-PSS w/ SHA-256
        /// </summary>
        PS256 = -37,

        /// <summary> 
        /// ECDSA w/ SHA-512
        /// </summary>
        ES512 = -36,

        /// <summary> 
        /// ECDSA w/ SHA-384
        /// </summary>
        ES384 = -35,

        /// <summary> 
        /// EdDSA
        /// </summary>
        EdDSA = -8,

        /// <summary> 
        /// ECDSA w/ SHA-256
        /// </summary>
        ES256 = -7
    }
}
