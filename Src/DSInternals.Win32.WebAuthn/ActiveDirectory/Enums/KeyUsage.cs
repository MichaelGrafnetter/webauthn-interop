#pragma warning disable CA1028 // Enum Storage should be Int32

namespace DSInternals.Win32.WebAuthn.ActiveDirectory
{
    /// <summary>
    /// Key Usage
    /// </summary>
    /// <see>https://msdn.microsoft.com/en-us/library/mt220501.aspx</see>
#pragma warning disable CA1027 // Mark enums with FlagsAttribute
    public enum KeyUsage : byte
#pragma warning restore CA1027 // Mark enums with FlagsAttribute
    {
        /// <summary>
        /// Admin key (pin-reset key)
        /// </summary>
        AdminKey = 0,

        /// <summary>
        /// NGC key attached to a user object (KEY_USAGE_NGC)
        /// </summary>
        NGC = 0x01,

        /// <summary>
        /// Transport key attached to a device object
        /// </summary>
        STK = 0x02,

        /// <summary>
        /// BitLocker recovery key
        /// </summary>
        BitlockerRecovery = 0x03,

        /// <summary>
        /// Unrecognized key usage
        /// </summary>
        Other = 0x04,

        /// <summary>
        /// Fast IDentity Online Key (KEY_USAGE_FIDO)
        /// </summary>
        FIDO = 0x07,

        /// <summary>
        /// File Encryption Key (KEY_USAGE_FEK)
        /// </summary>
        FEK = 0x08,

        /// <summary>
        /// DPAPI Key
        /// </summary>
        DPAPI // TODO: The DPAPI enum needs to be mapped to a proper integer value.
    }
}
