#pragma warning disable CA1069 // Enums values should not be duplicated

namespace DSInternals.Win32.WebAuthn.COSE
{
    /// <summary>
    /// COSE Key Type Parameters
    /// </summary>
    /// <see>https://www.iana.org/assignments/cose/cose.xhtml#key-type-parameters</see>
#pragma warning disable CA1008 // Enums should have zero value
    public enum KeyTypeParameter
#pragma warning restore CA1008 // Enums should have zero value
    {
        /// <summary> 
        /// EC identifier
        /// </summary>
        Crv = -1,

        /// <summary> 
        /// Key Value	
        /// </summary>
        K = -1,

        /// <summary> 
        /// x-coordinate	
        /// </summary>
        X = -2,

        /// <summary> 
        /// y-coordinate	
        /// </summary>
        Y = -3,

        /// <summary> 
        /// RSA modulus n	
        /// </summary>
        N = -1,

        /// <summary> 
        /// RSA public exponent e	
        /// </summary>
        E = -2
    }
}
