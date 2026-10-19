using System;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>Owns a writable UTF-16 PIN allocation and erases it before release.</summary>
    internal sealed class ManagementPinBuffer : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>Copies a SecureString into writable native memory.</summary>
        /// <param name="pin">Caller-owned PIN, left undisposed and unchanged.</param>
        /// <remarks>The caller must not modify or dispose the PIN during conversion.</remarks>
        internal unsafe ManagementPinBuffer(SecureString pin) : base(true)
        {
            ArgumentNullException.ThrowIfNull(pin);
            int length = pin.Length;
            SetHandle(Marshal.SecureStringToGlobalAllocUnicode(pin));
            try
            {
                var characters = new ReadOnlySpan<char>(handle.ToPointer(), length);
                for (int i = 0; i < length; i++)
                {
                    char value = characters[i];
                    if (value == '\0' || char.IsLowSurrogate(value))
                    {
                        throw new ArgumentException("A PIN must not contain NUL or invalid Unicode.", nameof(pin));
                    }
                    if (char.IsHighSurrogate(value))
                    {
                        if (++i >= length || !char.IsLowSurrogate(characters[i]))
                        {
                            throw new ArgumentException("A PIN must contain valid Unicode.", nameof(pin));
                        }
                    }
                }
            }
            catch
            {
                Dispose();
                throw;
            }
        }

        /// <summary>Erases and frees the null-terminated native PIN allocation.</summary>
        /// <returns>True after freeing the erased PIN allocation.</returns>
        protected override bool ReleaseHandle()
        {
            Marshal.ZeroFreeGlobalAllocUnicode(handle);
            return true;
        }
    }
}
