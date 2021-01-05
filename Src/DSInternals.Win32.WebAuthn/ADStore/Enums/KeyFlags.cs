#pragma warning disable CA1028 // Enum Storage should be Int32

using System;

namespace DSInternals.Win32.WebAuthn.ADStore
{
    /// <summary>
    /// Custom Key Flags
    /// </summary>
    /// <see>https://msdn.microsoft.com/en-us/library/mt220496.aspx</see>
    [Flags]
    public enum KeyFlags : byte
    {
        /// <summary>
        /// No flags specified.
        /// </summary>
        None = 0,

        /// <summary>
        /// Reserved for future use. (CUSTOMKEYINFO_FLAGS_ATTESTATION)
        /// </summary>
        Attestation = 0x01,

        /// <summary>
        /// During creation of this key, the requesting client authenticated using only a single credential. (CUSTOMKEYINFO_FLAGS_MFA_NOT_USED)
        /// </summary>
        MFANotUsed = 0x02,
    }
}
