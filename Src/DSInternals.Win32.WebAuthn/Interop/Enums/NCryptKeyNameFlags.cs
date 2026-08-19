using System;
using Windows.Win32.Security.Cryptography;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// Flags describing a key, as reported by <c>NCryptEnumKeys</c>.
    /// </summary>
    [Flags]
    public enum NCryptKeyNameFlags : uint
    {
        /// <summary>
        /// No flags.
        /// </summary>
        None = 0,

        /// <summary>
        /// The key is stored as a machine key.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>NCRYPT_MACHINE_KEY_FLAG</c> in <c>ncrypt.h</c>.
        /// </remarks>
        MachineKey = (uint)NCRYPT_FLAGS.NCRYPT_MACHINE_KEY_FLAG
    }
}
