namespace DSInternals.Win32.WebAuthn.COSE
{
    /// <summary>
    /// COSE Key Common Parameters https://www.iana.org/assignments/cose/cose.xhtml#key-common-parameters
    /// </summary>
    public enum KeyCommonParameter
    {
        /// <summary> 
        /// This value is reserved
        /// </summary>
        Reserved = 0,

        /// <summary> 
        /// Identification of the key type	
        /// </summary>
        KeyType = 1,

        /// <summary> 
        /// Key identification value - match to kid in message	
        /// </summary>
        KeyId = 2,

        /// <summary> 
        /// Key usage restriction to this algorithm	
        /// </summary>
        Alg = 3,

        /// <summary> 
        /// Restrict set of permissible operations	
        /// </summary>
        KeyOps = 4,

        /// <summary> 
        /// Base IV to be XORed with Partial IVs	
        /// </summary>
        BaseIV = 5
    }
}
