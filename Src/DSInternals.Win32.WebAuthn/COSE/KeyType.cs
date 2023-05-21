namespace DSInternals.Win32.WebAuthn.COSE
{
    /// <summary>
    /// COSE Key Types https://www.iana.org/assignments/cose/cose.xhtml#key-type
    /// </summary>
    public enum KeyType
    {
        /// <summary> 
        /// This value is reserved
        /// </summary>
#pragma warning disable CA1700 // Do not name enum values 'Reserved'
        Reserved = 0,
#pragma warning restore CA1700 // Do not name enum values 'Reserved'

        /// <summary> 
        /// Octet Key Pair
        /// </summary>
        OKP = 1,

        /// <summary> 
        /// Elliptic Curve Keys w/ x- and y-coordinate pair
        /// </summary>
        EC2 = 2,

        /// <summary> 
        /// RSA Key
        /// </summary>
        RSA = 3,

        /// <summary> 
        /// Symmetric Keys
        /// </summary>
        Symmetric = 4
    }
}
