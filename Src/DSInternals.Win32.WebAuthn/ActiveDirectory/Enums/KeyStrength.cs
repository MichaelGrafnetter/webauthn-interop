#pragma warning disable CA1028 // Enum Storage should be Int32

namespace DSInternals.Win32.WebAuthn.ActiveDirectory
{
    /// <summary>
    /// Specifies the strength of the NGC key.
    /// </summary>
    /// <see>https://msdn.microsoft.com/en-us/library/mt220496.aspx</see>
    public enum KeyStrength : byte
    {
        /// <summary>
        /// Key strength is unknown.
        /// </summary>
        Unknown = 0x00,

        /// <summary>
        /// Key strength is weak.
        /// </summary>
        Weak = 0x01,

        /// <summary>
        /// Key strength is normal.
        /// </summary>
        Normal = 0x02
    }
}
