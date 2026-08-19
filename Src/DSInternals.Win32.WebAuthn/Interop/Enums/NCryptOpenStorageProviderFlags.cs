using System;
using Windows.Win32.Security.Cryptography;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// Flags for <c>NCryptOpenStorageProvider</c>.
    /// </summary>
    [Flags]
    internal enum NCryptOpenStorageProviderFlags : uint
    {
        /// <summary>
        /// No flags.
        /// </summary>
        None = 0,

        /// <summary>
        /// Skips key validation.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>NCRYPT_NO_KEY_VALIDATION</c> in <c>ncrypt.h</c>.
        /// </remarks>
        NoKeyValidation = (uint)NCRYPT_FLAGS.NCRYPT_NO_KEY_VALIDATION,

        /// <summary>
        /// Uses the machine key store.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>NCRYPT_MACHINE_KEY_FLAG</c> in <c>ncrypt.h</c>.
        /// </remarks>
        MachineKey = (uint)NCRYPT_FLAGS.NCRYPT_MACHINE_KEY_FLAG,

        /// <summary>
        /// Requests no UI prompts.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>NCRYPT_SILENT_FLAG</c> in <c>ncrypt.h</c>.
        /// </remarks>
        Silent = (uint)NCRYPT_FLAGS.NCRYPT_SILENT_FLAG
    }
}
